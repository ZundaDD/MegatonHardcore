using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Megaton.Web
{
    /// <summary>
    /// 从OSU镜像站下载谱面
    /// </summary>
    public class OSUHandler : MonoBehaviour
    {
        #region Fields
        /// <summary>
        /// 下载状态
        /// </summary>
        public string Status { get; private set; } = "";

        /// <summary>
        /// 是否发生错误，包括取消和网络错误
        /// </summary>
        public bool Error { get; private set; } = false;

        /// <summary>
        /// 进度条
        /// </summary>
        public float Progress { get; private set; } = 0f;

        /// <summary>
        /// 剩余任务数
        /// </summary>
        public int LeftTasks { get => isDownloading ? downLoadList.Count: 0; }
        #endregion

        [SerializeField] private float downloadSpace = 2f;

        private List<int> downLoadList = new();
        private bool isDownloading = false;
        private CancellationTokenSource cancelSource;

        private string baseurl = "https://txy1.sayobot.cn/beatmaps/download/full/{0}?server=0";
        private string savePath;
        private string tempPath;

        private void Awake()
        {
            tempPath = Path.Combine(Application.persistentDataPath, "OSU");
            savePath = Path.Combine(Application.persistentDataPath, "Data", "Charts", "OSU");
        }

        private void OnDestroy()
        {
            cancelSource?.Cancel();
            cancelSource?.Dispose();
        }

        private void Update()
        {
            // 工作原理是：
            // 1. 每帧检查是否有下载任务
            // 2. 如果有，开始下载
            // 3. 下载完成后，检查是否还有任务
            // 4. 如果有，间隔短时间后继续下载
            // 5. 直到队列中没有任务
            // 6. 如果没有任务，设置状态为“空闲”
            if (!isDownloading && downLoadList.Count > 0)
            {
                isDownloading = true;
                DownLoadList().Forget();
            }
        }

        public void DownLoadChart(int id) => downLoadList.Add(id);

        #region 下载异步函数
        /// <summary>
        /// 下载队列中的所有谱面
        /// </summary>
        /// <param name="id">谱面ID</param>
        async private UniTask DownLoadList()
        {
            //设置取消状态
            cancelSource?.Cancel();
            cancelSource?.Dispose();
            cancelSource = new();

            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                cancelSource.Token,
                this.GetCancellationTokenOnDestroy());
            CancellationToken combinedToken = linkedCts.Token;
            
            while (downLoadList.Count > 0)
            {
                Progress = 0;

                //获取ID
                int id = downLoadList[0];

                //取消设置状态位并结束
                if (combinedToken.IsCancellationRequested)
                {
                    Error = true;
                    break;
                }

                //执行流程
                try
                {
                    //下载谱面
                    await DownloadChart(id, combinedToken);

                    //如果下载出错，跳过至下一次，如果下载取消在循环开始会处理
                    if (Error)
                    {
                        downLoadList.RemoveAt(0);
                        continue;
                    }

                    Progress = 1f;
                    Status = $"下载谱面{id}成功！转化文件中";
                    
                    //处理谱面，不可取消
                    string tempFullpath = Path.Combine(tempPath, id + ".osz");
                    await OSUConverter.Path2Path(tempFullpath, savePath);

                    Status = $"谱面{id}转化完成！";

                    //等待一段时间，避免过于频繁的请求
                    await UniTask.Delay(System.TimeSpan.FromSeconds(downloadSpace),
                        cancellationToken: combinedToken);

                    downLoadList.RemoveAt(0);
                }
                catch (OperationCanceledException)
                {
                    Error = true;
                    break;
                }
                catch (Exception ex)
                {
                    Status = $"处理谱面 {id} 时发生意外错误: {ex.Message}";
                    Error = true;
                    if (downLoadList.Count > 0 && downLoadList[0] == id) downLoadList.RemoveAt(0);
                    continue;
                }
            }

            //下载完成，设置结束状态，在下一次执行前不会改变
            isDownloading = false;
            Progress = 0;
            if(Error && combinedToken.IsCancellationRequested) Status = "下载被取消...";
            else Status = "当前没有下载任务！";

            linkedCts.Dispose();
        }

        /// <summary>
        /// 仅处理下载的逻辑
        /// </summary>
        async private UniTask DownloadChart(int id,CancellationToken cancellation)
        {
            Error = false;

            //计算路径
            string fullurl = string.Format(baseurl, id);
            string tempFullpath = Path.Combine(tempPath, id + ".osz");

            string downloadMsg = $"正在下载谱面: {id}" + "({0}%)";

            try
            {
                if (cancellation.IsCancellationRequested)
                {
                    Error = true;
                    return;
                }

                using (UnityWebRequest request = UnityWebRequest.Get(fullurl))
                {
                    request.downloadHandler = new DownloadHandlerFile(tempFullpath);

                    var reporter = new System.Progress<float>(p =>
                    {
                        if (!cancellation.IsCancellationRequested)
                        {
                            Progress = p;
                            Status = string.Format(downloadMsg, (int)(request.downloadProgress * 100));
                        }
                    });

                    Status = string.Format(downloadMsg, 0);

                    //等待下载完成，同时设置进度
                    await request.SendWebRequest().ToUniTask(reporter, cancellationToken: cancellation);

                    //处理错误
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Status = $"下载错误: {request.error} {request.responseCode}";
                        Error = true;
                        if (File.Exists(tempFullpath)) File.Delete(tempFullpath);
                    }
                }
            }
            catch(System.OperationCanceledException)
            {
                Error = true;
                if (File.Exists(tempFullpath)) File.Delete(tempFullpath);
                return;
            }
            catch (System.Exception ex)
            {
                Status = $"下载错误: {ex.Message}";
                Error = true;
                if (File.Exists(tempFullpath)) File.Delete(tempFullpath);
                return;
            }
        }
        #endregion
    }
}
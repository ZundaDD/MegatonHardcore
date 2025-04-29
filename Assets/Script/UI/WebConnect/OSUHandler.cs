using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        /// <summary>
        /// 下载状态
        /// </summary>
        public string Status { get; private set; } = "";

        /// <summary>
        /// 进度条
        /// </summary>
        public float Progress { get; private set; } = 0f;

        [SerializeField] private float downloadSpace = 2f;
        
        private List<int> downLoadList = new();
        private bool isDownloading = false;
        private string baseurl = "https://txy1.sayobot.cn/beatmaps/download/full/{0}?server=0";
        private string savePath;
        private string tempPath;

        void Awake()
        {
            tempPath = Path.Combine(Application.persistentDataPath, "OSU");
            savePath = Path.Combine(Application.persistentDataPath, "Data", "Charts", "OSU");
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
            if(!isDownloading && downLoadList.Count > 0)
            {
                isDownloading = true;
                StartCoroutine(DownLoadCharts());
            }
        }

        public void DownLoadChart(int id) => downLoadList.Add(id);

        /// <summary>
        /// 下载zip形式的谱面
        /// </summary>
        /// <param name="id">谱面ID</param>
        private IEnumerator DownLoadCharts()
        {
            while (downLoadList.Count > 0)
            {
                //获取ID
                int id = downLoadList[0];
                downLoadList.RemoveAt(0);

                //下载谱面
                yield return StartCoroutine(DownLoadZipChart(id));

                //等待一段时间，避免过于频繁的请求
                yield return new WaitForSeconds(downloadSpace);

                //重置进度条
                if (downLoadList.Count != 0) Progress = 0;
            }

            //下载完成，设置状态
            isDownloading = false;
            Status = "当前没有下载任务！";
        }

        private IEnumerator DownLoadZipChart(int id)
        {
            //计算路径
            string fullurl = string.Format(baseurl, id);
            string tempFullpath = Path.Combine(tempPath, id + ".osz");

            string downloadMsg = $"正在下载谱面: {id}" + "({0}%)";
            using (UnityWebRequest request = UnityWebRequest.Get(fullurl))
            {
                request.downloadHandler = new DownloadHandlerFile(tempFullpath);
                var asyncOperation =  request.SendWebRequest();

                //等待下载完成，同时设置进度
                while (!asyncOperation.isDone)
                {
                    Status = string.Format(downloadMsg, (int)(request.downloadProgress * 100));
                    Progress = request.downloadProgress;
                    yield return null;
                }

                //处理错误
                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.ProtocolError)
                {
                    string errorMsg = $"下载错误: {request.error} (HTTP Code: {request.responseCode})";
                    Status = errorMsg;

                    //删除已下载的文件
                    if (File.Exists(tempFullpath))
                    {
                        File.Delete(tempFullpath);
                        Debug.Log($"删除已下载的文件: {tempFullpath}");
                    }
                }
                //处理成功
                else
                {
                    string successMsg = $"下载谱面{id}成功！转化文件中......";
                    Status = successMsg;
                    Progress = 1f;
                }
            }
        }
    }
}
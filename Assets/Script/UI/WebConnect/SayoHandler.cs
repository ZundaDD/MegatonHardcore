using Cysharp.Threading.Tasks;
using System;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using Megaton.Web.Sayo;

namespace Megaton.Web
{
    /// <summary>
    /// sayo镜像站访问
    /// </summary>
    public class SayoHandler : MonoBehaviour
    {    
        private string coverUrl = "https://a.sayobot.cn/beatmaps/{0}/covers/cover.jpg";
        private string baseListUrl = "https://api.sayobot.cn/beatmaplist?M=8&O={0}&L={1}";
        private string baseInfoUrl = "https://api.sayobot.cn/v2/beatmapinfo?K={0}";

        /// <summary>
        /// 获取Mania谱面列表
        /// </summary>
        /// <param name="offset">从offset处开始</param>
        /// <param name="limit">数量限制</param>
        public async UniTask<BeatmapListResponse> GetManiaList(int offset, int limit)
        {
            string fullurl = string.Format(baseListUrl, offset, limit);
            Debug.Log(fullurl);

            try
            {
                using (UnityWebRequest request = UnityWebRequest.Get(fullurl))
                {
                    await request.SendWebRequest().ToUniTask();

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        string responseText = request.downloadHandler.text;
                        var response = JsonUtility.FromJson<BeatmapListResponse>(responseText);

                        Debug.Log($"获取到{response.data.Length}张谱面，共有{response.results}个结果！");
                        Debug.Log($"下次搜索从{response.endid}开始！");
                        return response;
                    }
                    else
                    {
                        Debug.Log($"获取谱面列表失败: {request.error} {request.responseCode}");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log($"获取谱面列表失败: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 获取单个谱面的信息
        /// </summary>
        /// <param name="id">谱面ID</param>
        /// <returns></returns>
        public async UniTask<BeatmapInfoResponse> GetManiaInfo(int id)
        {
            string fullurl = string.Format(baseInfoUrl, id);
            Debug.Log(fullurl);

            try
            {
                using (UnityWebRequest request = UnityWebRequest.Get(fullurl))
                {
                    await request.SendWebRequest().ToUniTask();

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        string responseText = request.downloadHandler.text;
                        var response = JsonUtility.FromJson<BeatmapInfoResponse>(responseText);

                        return response;
                    }
                    else
                    {
                        Debug.Log($"获取谱面信息失败: {request.error} {request.responseCode}");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log($"获取谱面信息失败: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 获取封面
        /// </summary>
        /// <param name="id">sid</param>
        /// <returns>Texture2D</returns>
        public async UniTask<Texture2D> GetCover(int id)
        {
            string fullurl = string.Format(coverUrl, id);
            Debug.Log(fullurl);
            Texture2D result = null;

            try
            {
                using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(fullurl))
                {
                    await request.SendWebRequest().ToUniTask();

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        result = DownloadHandlerTexture.GetContent(request);
                        return result;
                    }
                    else
                    {
                        Debug.Log($"获取谱面信息失败: {request.error} {request.responseCode}");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log($"获取谱面信息失败: {e.Message}");
                return null;
            }
        }
    }
}

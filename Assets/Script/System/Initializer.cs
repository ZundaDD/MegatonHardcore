using TMPro;
using UnityEngine;
using MikanLab;
using System.IO;

namespace Megaton
{
    /// <summary>
    /// 游戏初始化脚本执行器，只在Init场景中使用
    /// </summary>
    public class Initializer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI loadingText;
        TaskProgress initProgress;

        void Start()
        {
            initProgress = new("初始化完成...");
            //initProgress.AddTask(LoadAllChartInfo, "加载谱面中...");
            //initProgress.Start();
        }

        /// <summary>
        /// 加载全部谱面信息
        /// </summary>
        private void LoadAllChartInfo()
        {
            string chartPath = Path.Combine(Application.dataPath, "../", "Data");
            if(!Directory.Exists(chartPath)) Directory.CreateDirectory(chartPath);
            
            //遍历版本文件夹
            foreach(var versionDir in Directory.GetDirectories(chartPath))
            {
                if(versionDir.StartsWith("V"))
                {
                    string version = versionDir.Substring(1);
                    string relativeDir = Path.Combine(chartPath, versionDir, "Charts");
                    foreach (var chartDir in Directory.GetDirectories(relativeDir))
                    {
                        ChartLoader loader = new(Path.Combine(relativeDir,chartDir));
                        var info = loader.Path2Info();
                        info.Version = version;
                        GameVar.Ins.ChartInfos.Add(info);
                    }
                }
            }
        }
    }
}
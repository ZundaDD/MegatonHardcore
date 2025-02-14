using TMPro;
using UnityEngine;
using MikanLab;
using System.IO;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Megaton
{
    /// <summary>
    /// 游戏初始化脚本执行器，只在Init场景中使用
    /// </summary>
    public class InitialController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI loadingText;
        TaskProgress initProgress;

        void Update()
        {
            loadingText.text = $"{initProgress.CurrentTaskDescription} ({initProgress.Alltask-initProgress.LeftTask}/{initProgress.Alltask})";    
        }

        void Start()
        {
            
            initProgress = new("初始化完成...");
            initProgress.OnFinished += () => 
            {
                GameVar.Ins.IfInitialed = true;
                loadScene(1);
            };
            initProgress.AddTask(LoadAllChartInfo, "加载谱面中...");
            initProgress.Start();
            
        }

        public async void loadScene(int index)
        {
            await SceneManager.LoadSceneAsync(index);
        }

        /// <summary>
        /// 加载全部谱面信息
        /// </summary>
        private void LoadAllChartInfo()
        {
            string chartPath = Path.Combine(Application.dataPath, "../", "Data", "Charts");
            if(!Directory.Exists(chartPath)) Directory.CreateDirectory(chartPath);

            int total = 0;
            //遍历版本文件夹
            foreach(var packDir in Directory.GetDirectories(chartPath))
            {
                string relativeDir = Path.Combine(chartPath, packDir);
                foreach (var chartDir in Directory.GetDirectories(relativeDir))
                {
                    var info = ChartLoader.Path2Info(Path.Combine(relativeDir, chartDir));
                    total++;
                    GameVar.Ins.ChartInfos.Add(info);
                }
            }
            Debug.Log($"{total} Charts Loaded!");
        }
    }
}
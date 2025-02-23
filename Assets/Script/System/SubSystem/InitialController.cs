using TMPro;
using UnityEngine;
using MikanLab;
using System.IO;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace Megaton
{
    /// <summary>
    /// 游戏初始化脚本执行器，只在Init场景中使用
    /// </summary>
    public class InitialController : MonoBehaviour
    {
        [SerializeField] Text loadingText;
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
                GameVar.IfInitialed = true;
                loadScene(1);
            };
            
            initProgress.AddTask(CheckDirectory, "验证目录中...");
            initProgress.AddTask(LoadAllScore, "加载分数中...");
            initProgress.AddTask(LoadAllChartInfo, "加载谱面中...");
            initProgress.Start();
            
        }

        public async void loadScene(int index)
        {
            await SceneManager.LoadSceneAsync(index);
        }

        private void LoadAllScore()
        {
            GameVar.ChartScores = ScoreLoader.Path2Score();
        }

        /// <summary>
        /// 验证所需目录是否被创建
        /// </summary>
        private void CheckDirectory()
        {
            Directory.CreateDirectory(GameVar.DataRootDir);
            Directory.CreateDirectory(Path.Combine(GameVar.DataRootDir, "Charts"));
            Directory.CreateDirectory(Path.Combine(GameVar.DataRootDir, "Setting"));
            if (!File.Exists(ScoreLoader.PathName)) ScoreLoader.SaveScore();
        }
        
        /// <summary>
        /// 加载全部谱面信息
        /// </summary>
        private void LoadAllChartInfo()
        {
            string chartPath = Path.Combine(GameVar.DataRootDir, "Charts");
            if(!Directory.Exists(chartPath)) Directory.CreateDirectory(chartPath);

            int total = 0;
            //遍历版本文件夹
            foreach(var packDir in Directory.GetDirectories(chartPath))
            {
                string relativeDir = Path.Combine(chartPath, packDir);
                foreach (var chartDir in Directory.GetDirectories(relativeDir))
                {
                    var info = ChartLoader.Path2Info(Path.Combine(relativeDir, chartDir));
                    if (info != null)
                    {
                        total++;
                        GameVar.ChartInfos.Add(info);
                    }
                }
            }
            Debug.Log($"{total} Charts Loaded!");
        }
    }
}
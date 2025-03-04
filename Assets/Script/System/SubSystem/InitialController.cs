using UnityEngine;
using MikanLab;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

namespace Megaton
{
    /// <summary>
    /// 游戏初始化脚本执行器，只在Init场景中使用
    /// </summary>
    public class InitialController : MonoBehaviour
    {
        [SerializeField] Text loadingText;
        [SerializeField] GameObject loadingIcon;
        TaskProgress initProgress;

        void Update()
        {
            if (!GameVar.IfInitialed) loadingText.text = $"{initProgress.CurrentTaskDescription} ({initProgress.Alltask - initProgress.LeftTask}/{initProgress.Alltask})";
            else loadingText.text = initProgress.CurrentTaskDescription;    
        }

        /// <summary>
        /// 部分GameVar初始化于此由于时间的原因
        /// </summary>
        private void Awake()
        {
            GameVar.DataRootDir = Path.Combine(Application.persistentDataPath, "Data");
        }

        void Start()
        {
            initProgress = new("加载场景中...");
            initProgress.OnFinished += () => 
            {
                GameVar.IfInitialed = true;
                loadingIcon.SetActive(false);
                StartCoroutine(loadScene());
            };
            
            initProgress.AddTask(CheckDirectory, "验证目录中...");
            initProgress.AddTask(LoadAllScore, "加载分数中...");
            initProgress.AddTask(LoadAllChartInfo, "加载谱面中...");
            initProgress.Start();
        }

        public IEnumerator loadScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
            while (!asyncLoad.isDone) yield return null;
        }

        #region 初始化任务
        /// <summary>
        /// 加载全部分数
        /// </summary>
        private void LoadAllScore() => 
            GameVar.ChartScores = ScoreLoader.Path2Score();

        /// <summary>
        /// 验证所需目录是否被创建
        /// </summary>
        private void CheckDirectory()
        {
            Directory.CreateDirectory(GameVar.DataRootDir);
            Directory.CreateDirectory(Path.Combine(GameVar.DataRootDir, "Charts"));
            Directory.CreateDirectory(Path.Combine(GameVar.DataRootDir, "Setting"));
            if (!File.Exists(ScoreLoader.PathName)) ScoreLoader.SaveScore();
            var a = Setting.Ins.Input_Offset.Value;
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
        #endregion
    }
}
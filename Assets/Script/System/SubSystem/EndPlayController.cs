using Megaton.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Megaton
{
    public class EndPlayController : MonoBehaviour
    {
        private static EndPlayController ins;
        public static EndPlayController Ins => ins;


        [SerializeField] private Button exitButton;

        void Awake()
        {
            if (!GameVar.IfInitialed)
            {
                SceneManager.LoadScene(0);
                return;
            }

            ins = this;
        }

        private void Start()
        {
            exitButton.onClick.AddListener(() => SceneSwitch.Ins.Ending(2));
            UpdateScore();
        }

        /// <summary>
        /// 更新游玩成绩
        /// </summary>
        private void UpdateScore()
        {
            var info = GameVar.CurPlay.Info;
            string key = $"{info.Pack}/{info.Folder}";

            //如果之前没有过游玩记录的话，分数字典中并不会存在对应项
            //那么ChartInfo.Score初始化时是默认的new()
            //而存在记录的话ChartInfo.Score应该是分数字典项的引用
            //除非这次游玩为0分，否则在之后必定刷新记录改变Rank
            if (!GameVar.ChartScores.ContainsKey(key))
            {
                GameVar.ChartScores.Add(key, new());
                GameVar.ChartScores[key].Update(ScoreBoard.Ins.Score);
                info.Score = GameVar.ChartScores[key];
                ScoreLoader.SaveScore();
            }

            //更新则写入文件，请不要频繁更新
            else if (GameVar.ChartScores[key].Update(ScoreBoard.Ins.Score)) ScoreLoader.SaveScore();
        }
    }
}

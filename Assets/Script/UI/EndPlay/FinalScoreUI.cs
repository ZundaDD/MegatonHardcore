using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class FinalScoreUI : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text bestScoreText;
        [SerializeField] private Text gapScoreText;
        [SerializeField] private Text critical;
        [SerializeField] private Text perfect;
        [SerializeField] private Text great;
        [SerializeField] private Text good;
        [SerializeField] private Text miss;
        [SerializeField] private Text fast;
        [SerializeField] private Text late;
        [SerializeField] private Text rank;

        void Start()
        {
            var info = GameVar.CurPlay.Info;
            string key = $"{info.Pack}/{info.Folder}";
            
            //根据数据显示
            scoreText.text = ScoreBoard.Ins.Score.ToString("00000000");
            bestScoreText.text = "Best:" + info.Score.BestScore.ToString("00000000");
            int gap = ScoreBoard.Ins.Score - info.Score.BestScore;
            gapScoreText.text = (gap >= 0 ? "+" : "") + gap.ToString();
            fast.text = ScoreBoard.Ins.Fast.ToString();
            late.text = ScoreBoard.Ins.Late.ToString();
            critical.text = ScoreBoard.Ins.Scores[SimplifyJudgeEnum.CRITICAL].ToString();
            perfect.text = ScoreBoard.Ins.Scores[SimplifyJudgeEnum.PERFECT].ToString();
            great.text = ScoreBoard.Ins.Scores[SimplifyJudgeEnum.PERFECT].ToString();
            good.text = ScoreBoard.Ins.Scores[SimplifyJudgeEnum.PERFECT].ToString();
            miss.text = ScoreBoard.Ins.Scores[SimplifyJudgeEnum.MISS].ToString();
            rank.text = ChartScore.GetRank(ScoreBoard.Ins.Score);

            //如果之前没有过游玩记录的话，分数字典中并不会存在对应项
            //那么ChartInfo.Score初始化时是默认的new()
            //而存在记录的话ChartInfo.Score应该是分数字典项的引用
            //除非这次游玩为0分，否则在之后必定刷新记录改变Rank
            if (!GameVar.ChartScores.ContainsKey(key))
            {
                GameVar.ChartScores.Add(key, new());
                GameVar.ChartScores[key].Update(0);
                info.Score = GameVar.ChartScores[key];
            }

            //更新则写入文件，请不要频繁更新
            if(GameVar.ChartScores[key].Update(ScoreBoard.Ins.Score)) ScoreLoader.SaveScore();
        }

    }
}
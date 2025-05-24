using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class FinalScoreUI : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text bestScoreText;
        [SerializeField] private Text gapScoreText;
        [SerializeField] private Text combo;
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
            critical.text = ScoreBoard.QWeight(SimplifyJudgeEnum.CRITICAL).ToString();
            perfect.text = ScoreBoard.QWeight(SimplifyJudgeEnum.PERFECT).ToString();
            great.text = ScoreBoard.QWeight(SimplifyJudgeEnum.GREAT).ToString();
            good.text = ScoreBoard.QWeight(SimplifyJudgeEnum.GOOD).ToString();
            miss.text = ScoreBoard.QWeight(SimplifyJudgeEnum.MISS).ToString();
            rank.text = ChartScore.GetRank(ScoreBoard.Ins.Score);
            combo.text = $"{ScoreBoard.Ins.MaxCombo}/{ScoreBoard.Ins.ComboSum}";
        }

    }
}
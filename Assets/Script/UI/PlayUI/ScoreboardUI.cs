using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class ScoreboardUI : MonoBehaviour
    {
        [SerializeField] private LerpNumber score;
        [SerializeField] private Text critical_judge;
        [SerializeField] private Text perfect_judge;
        [SerializeField] private Text great_judge;
        [SerializeField] private Text good_judge;
        [SerializeField] private Text miss_judge;
        [SerializeField] private Text fast_judge;
        [SerializeField] private Text late_judge;
        [SerializeField] private Text combo;

        public void Bind() => ScoreBoard.Ins.onAdded += AddJudge;

        public void UnBind() => ScoreBoard.Ins.onAdded -= AddJudge;

        public void AddJudge()
        {
            fast_judge.text = ScoreBoard.Ins.Fast.ToString();
            late_judge.text = ScoreBoard.Ins.Late.ToString();
            critical_judge.text = ScoreBoard.Ins.Scores[JudgeEnum.CRITICAL].ToString();
            perfect_judge.text = (ScoreBoard.Ins.Scores[JudgeEnum.S_PERFECT] + ScoreBoard.Ins.Scores[JudgeEnum.F_PERFECT]).ToString();
            great_judge.text = (ScoreBoard.Ins.Scores[JudgeEnum.S_GREAT] + ScoreBoard.Ins.Scores[JudgeEnum.F_GREAT]).ToString();
            good_judge.text = (ScoreBoard.Ins.Scores[JudgeEnum.S_GOOD] + ScoreBoard.Ins.Scores[JudgeEnum.F_GOOD]).ToString();
            miss_judge.text = ScoreBoard.Ins.Scores[JudgeEnum.MISS].ToString();
            score.SetNumer(ScoreBoard.Ins.Score);

            int combo_n = ScoreBoard.Ins.CurCombo;
            combo.text = combo_n == 0 ? "" : combo_n.ToString();
        }
    }
}
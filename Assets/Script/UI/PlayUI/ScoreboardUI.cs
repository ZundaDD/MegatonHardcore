using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class ScoreboardUI : MonoBehaviour
    {
        [SerializeField] private Text score;
        [SerializeField] private Text critical_judge;
        [SerializeField] private Text perfect_judge;
        [SerializeField] private Text great_judge;
        [SerializeField] private Text good_judge;
        [SerializeField] private Text miss_judge;
        [SerializeField] private Text fast_judge;
        [SerializeField] private Text late_judge;

        private void OnEnable()
        {
            ScoreBoard.Ins.onAdded += AddJudge;
        }

        private void OnDisable()
        {
            ScoreBoard.Ins.onAdded -= AddJudge;
        }

        public void AddJudge()
        {
            fast_judge.text = ScoreBoard.Ins.Fast.ToString();
            late_judge.text = ScoreBoard.Ins.Late.ToString();
            critical_judge.text = ScoreBoard.Ins.Scores[JudgeEnum.CRITICAL].ToString();
            perfect_judge.text = (ScoreBoard.Ins.Scores[JudgeEnum.S_PERFECT] + ScoreBoard.Ins.Scores[JudgeEnum.F_PERFECT]).ToString();
            great_judge.text = (ScoreBoard.Ins.Scores[JudgeEnum.S_GREAT] + ScoreBoard.Ins.Scores[JudgeEnum.F_GREAT]).ToString();
            good_judge.text = (ScoreBoard.Ins.Scores[JudgeEnum.S_GOOD] + ScoreBoard.Ins.Scores[JudgeEnum.F_GOOD]).ToString();
            miss_judge.text = ScoreBoard.Ins.Scores[JudgeEnum.MISS].ToString();
            score.text = ScoreBoard.Ins.Score.ToString("00000000");
        }
    }
}
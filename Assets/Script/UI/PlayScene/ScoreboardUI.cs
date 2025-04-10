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
        [SerializeField] private Text combo;
        [SerializeField] private Text floatScore;

        public void Bind()
        {
            combo.text = "";
            floatScore.text = GetFloatScoreText();
            ScoreBoard.Ins.onAdded += AddJudge;
        }

        public void UnBind() => ScoreBoard.Ins.onAdded -= AddJudge;

        public void AddJudge()
        {
            critical_judge.text = ScoreBoard.QWeight(SimplifyJudgeEnum.CRITICAL).ToString();
            perfect_judge.text = ScoreBoard.QWeight(SimplifyJudgeEnum.PERFECT).ToString();
            great_judge.text = ScoreBoard.QWeight(SimplifyJudgeEnum.GREAT).ToString();
            good_judge.text = ScoreBoard.QWeight(SimplifyJudgeEnum.GOOD).ToString();
            miss_judge.text = ScoreBoard.QWeight(SimplifyJudgeEnum.MISS).ToString();
            score.SetNumer(ScoreBoard.Ins.Score);
            floatScore.text = GetFloatScoreText();

            int combo_n = ScoreBoard.Ins.CurCombo;
            combo.text = combo_n == 0 ? "" : $"COMBO  {combo_n}";
        }

        private string GetFloatScoreText()
        {
            int score = ScoreBoard.GetFloatScore();
            switch (Setting.Ins.Float_Score_Type.Value)
            {
                case ScoreType.Add0:
                case ScoreType.Minus101:
                case ScoreType.Minus100:
                    return $"Score  {score}";
                case ScoreType.Gap1008:
                    return score < 0 ? "" : $"EX+  {ScoreBoard.GetFloatScore()}";
                case ScoreType.Gap1005:
                    return score < 0 ? "" : $"EX  {ScoreBoard.GetFloatScore()}";
                case ScoreType.Gap1000:
                    return score < 0 ? "" : $"FUL  {ScoreBoard.GetFloatScore()}";
                default:
                    return "";
            }
        }
    }
}
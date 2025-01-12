using MikanLab;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 计分板,保存判定详细信息
    /// </summary>
    public class ScoreBoard
    {
        static ScoreBoard ins = new();
        public static ScoreBoard Ins => ins;

        #region 分数项
        public EnumArray<JudgeEnum, int> Scores  = new();
        public int Fast = 0;
        public int Slow = 0;
        public int All = 0;
        public int MaxCombo = 0;
        public int CurCombo = 0;
        #endregion


        /// <summary>
        /// 清空
        /// </summary>
        public static void Clear() => ins = new();

        /// <summary>
        /// 添加判定
        /// </summary>
        public static void AddJudge(JudgeEnum judge)
        {
            Ins.Scores[judge]++;
            Ins.All++;
            if(judge > 0) Ins.Fast++;
            else if(judge < 0) Ins.Slow++;

            if (judge != JudgeEnum.MISS) Ins.CurCombo++;
            else Ins.CurCombo = 0;

            Ins.MaxCombo = Mathf.Max(Ins.MaxCombo, Ins.CurCombo);
        }
    }
}
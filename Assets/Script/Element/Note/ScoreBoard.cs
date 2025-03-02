using MikanLab;
using System;
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

        /// <summary>
        /// 添加判定回调
        /// </summary>
        public Action onAdded;

        //分数项
        public EnumArray<JudgeEnum, int> Scores  = new();
        public int Fast = 0;
        public int Late = 0;
        public int All = 0;
        public int MaxCombo = 0;
        public int CurCombo = 0;
        public int Score = 0;

        /// <summary>
        /// 清空
        /// </summary>
        public static void Clear(int newQ)
        {
            ins = new();
            ins.All = newQ;
        }

        /// <summary>
        /// 添加判定
        /// </summary>
        public static void AddJudge(JudgeEnum judge)
        {
            var dict = Ins.Scores;

            dict[judge]++;
            if(judge > 0) Ins.Fast++;
            else if(judge < 0 && judge != JudgeEnum.MISS) Ins.Late++;

            if (judge != JudgeEnum.MISS) Ins.CurCombo++;
            else Ins.CurCombo = 0;

            Ins.MaxCombo = Mathf.Max(Ins.MaxCombo, Ins.CurCombo);
            if (dict[JudgeEnum.CRITICAL] == Ins.All) Ins.Score = 10100000;
            else Ins.Score = (int)Math.Round(1e7f / Ins.All *
                (
                1.01f * dict[JudgeEnum.CRITICAL] +
                1f * (dict[JudgeEnum.S_PERFECT] + dict[JudgeEnum.F_PERFECT]) +
                0.75f * (dict[JudgeEnum.S_GREAT] + dict[JudgeEnum.F_GREAT]) +
                0.45f * (dict[JudgeEnum.S_GOOD] + dict[JudgeEnum.F_GOOD])
                ));

            GlobalEffectPlayer.PlayEffect(AudioEffect.OnJudge);
            Ins.onAdded?.Invoke();
        }
    }
}
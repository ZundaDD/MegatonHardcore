using JetBrains.Annotations;
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

        public static int QWeight(SimplifyJudgeEnum key) => Ins.Weights[key];

        //分数项
        public EnumArray<SimplifyJudgeEnum, int> Combos  = new();
        private EnumArray<SimplifyJudgeEnum, int> Weights = new();
        public int Fast = 0;
        public int Late = 0;
        public int ComboSum = 0;
        public int WeightSum = 0;
        public int MaxCombo = 0;
        public int CurCombo = 0;
        public int Score = 0;

        /// <summary>
        /// 清空
        /// </summary>
        public static void Clear(int comboSum,int weightSum)
        {
            ins = new();
            ins.ComboSum = comboSum;
            ins.WeightSum = weightSum;
        }

        public static int GetFloatScore()
        {
            var dict = Ins.Weights;

            int score101 = 10100000 - (int)Math.Round(1e7f / Ins.WeightSum *
                (
                0.01f * (dict[SimplifyJudgeEnum.PERFECT]) +
                0.26f * (dict[SimplifyJudgeEnum.GREAT]) +
                0.56f * (dict[SimplifyJudgeEnum.GOOD]) +
                1.01f * (dict[SimplifyJudgeEnum.MISS])
                ));
            switch (Setting.Ins.Float_Score_Type.Value)
            {
                case ScoreType.Minus101:
                    return score101;
                case ScoreType.Minus100:
                    return 10000000 - (int)Math.Round(1e7f / Ins.WeightSum *
                (
                -0.01f * (dict[SimplifyJudgeEnum.CRITICAL]) +
                0.26f * (dict[SimplifyJudgeEnum.GREAT]) +
                0.56f * (dict[SimplifyJudgeEnum.GOOD]) +
                1.01f * (dict[SimplifyJudgeEnum.MISS])
                ));
                case ScoreType.Add0:
                    return (int)Math.Round(1e7f / Ins.WeightSum *
                (
                1.01f * dict[SimplifyJudgeEnum.CRITICAL] +
                1f * (dict[SimplifyJudgeEnum.PERFECT]) +
                0.75f * (dict[SimplifyJudgeEnum.GREAT]) +
                0.45f * (dict[SimplifyJudgeEnum.GOOD])
                ));
                case ScoreType.Gap1008:
                    return score101 - 10080000;
                case ScoreType.Gap1005:
                    return score101 - 10050000;
                case ScoreType.Gap1000:
                    return score101 - 10000000;
                default: return 0;
            }

        }

        /// <summary>
        /// 添加判定
        /// </summary>
        public static void AddJudge(JudgeEnum judge,int weight)
        {
            var dict = Ins.Weights;

            //判定累计
            var sjudge = (SimplifyJudgeEnum)Mathf.Abs((int)judge);
            dict[sjudge] += weight;
            if(judge > 0) Ins.Fast++;
            else if(judge < 0 && judge != JudgeEnum.MISS) Ins.Late++;

            //快慢累计
            if (judge != JudgeEnum.MISS) Ins.CurCombo++;
            else Ins.CurCombo = 0;
            Ins.MaxCombo = Mathf.Max(Ins.MaxCombo, Ins.CurCombo);

            //分数累计
            if (dict[SimplifyJudgeEnum.CRITICAL] == Ins.WeightSum) Ins.Score = 10100000;
            else Ins.Score = (int)Math.Round(1e7f / Ins.WeightSum *
                (
                1.01f * dict[SimplifyJudgeEnum.CRITICAL] +
                1f * (dict[SimplifyJudgeEnum.PERFECT]) +
                0.75f * (dict[SimplifyJudgeEnum.GREAT]) +
                0.45f * (dict[SimplifyJudgeEnum.GOOD])
                ));

            //反馈
            if(judge != JudgeEnum.MISS) GlobalEffectPlayer.PlayEffect(AudioEffect.OnJudge);
            Ins.onAdded?.Invoke();
        }
    }
}
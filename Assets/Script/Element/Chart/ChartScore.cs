using System;
using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 谱面分数
    /// </summary>
    [Serializable]
    public class ChartScore
    {
        public int BestScore = 0;
        public string BestRank = "";

        public static string GetRank(int score)
        {
            if (score == 10100000) return "INF";
            else if (score > 10800000) return "EX+";
            else if (score > 10500000) return "EX";
            else if (score > 10000000) return "FUL";
            else if (score > 9800000) return "S";
            else if (score > 9600000) return "A";
            else if (score > 9300000) return "B";
            else if (score > 9000000) return "C";
            else if (score > 8000000) return "D";
            return "X";
        }

        /// <summary>
        /// 自动更新计算分数
        /// </summary>
        /// <param name="newScore">新的分数</param>
        public bool Update(int newScore)
        {
            if (BestRank != "" && newScore <= BestScore) return false;
            BestScore = newScore;
            BestRank = GetRank(newScore);

            return true;
        }
    }
}
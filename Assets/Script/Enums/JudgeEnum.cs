using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 判定枚举，表示判定细则
    /// </summary>
    public enum JudgeEnum
    {
        CRITICAL = 0,
        F_PERFECT = 1, S_PERFECT = -1,
        F_GREAT = 2, S_GREAT = -2,
        F_GOOD = 3, S_GOOD = -3,
        MISS = -4
    }
}
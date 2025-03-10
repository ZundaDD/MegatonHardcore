using Megaton.Abstract;
using UnityEngine;

namespace Megaton.Classic
{
    /// <summary>
    /// 单点音符
    /// </summary>
    public class Tap : Note
    {
        public override float JudgeStart => 0.12f;

        public override float JudgeEnd => 0.12f;

        public override JudgeEnum GetResult()
        {
            float Offset = MusicPlayer.ExactTime - ExactTime;
            return TapJudge(Offset);
        }

        public static JudgeEnum TapJudge(float Offset)
        {
            if (Mathf.Abs(Offset) < 0.03) return JudgeEnum.CRITICAL;
            else if (Mathf.Abs(Offset) < 0.06) return Offset > 0 ? JudgeEnum.S_PERFECT : JudgeEnum.F_PERFECT;
            else if (Mathf.Abs(Offset) < 0.09) return Offset > 0 ? JudgeEnum.S_GREAT : JudgeEnum.F_GREAT;
            else if (Mathf.Abs(Offset) < 0.12) return Offset > 0 ? JudgeEnum.S_GOOD : JudgeEnum.F_GOOD;
            else return JudgeEnum.MISS;
        }

        public override (bool success,bool ifcontinue) Judge(bool railState, bool formState)
        {
            //从Off状态变为On状态是为一次判定
            if (railState && !formState) return (true, false);
            //未点击到强制MISS判定
            if (MusicPlayer.ExactTime - ExactTime > JudgeEnd) return (true, false);
            //其它时刻不构成判定
            return (false, false);
        }
    }
}
using Megaton.Abstract;

namespace Megaton.Classic
{
    /// <summary>
    /// 单点音符
    /// </summary>
    public class Tap : Note
    {
        public static RangeCompare<float, JudgeEnum> judgeQuery = new(
            new() { -0.1f, -0.075f, -0.05f, -0.25f, 0.25f, 0.5f, 0.75f, 0.1f },
            new() {JudgeEnum.MISS,JudgeEnum.F_GOOD,JudgeEnum.F_GREAT,JudgeEnum.F_PERFECT,
            JudgeEnum.CRITICAL,JudgeEnum.S_PERFECT,JudgeEnum.S_GREAT,JudgeEnum.S_GOOD},
            JudgeEnum.MISS);
        
        public override float JudgeStart => 0.1f;

        public override float JudgeEnd => 0.1f;

        public override JudgeEnum GetResult()
        {
            float Offset = MusicPlayer.ExactTime - ExactTime;
            return judgeQuery.Query(Offset);
            
            /*if (Mathf.Abs(Offset) < 0.025) return JudgeEnum.CRITICAL;
            else if (Mathf.Abs(Offset) < 0.050) return Offset > 0 ? JudgeEnum.S_PERFECT : JudgeEnum.F_PERFECT;
            else if (Mathf.Abs(Offset) < 0.075) return Offset > 0 ? JudgeEnum.S_GREAT : JudgeEnum.F_GREAT;
            else if (Mathf.Abs(Offset) < 0.1) return Offset > 0 ? JudgeEnum.S_GOOD : JudgeEnum.F_GOOD;
            else return JudgeEnum.MISS;*/
        }


        public override bool Judge(bool railState, bool formState)
        {
            //从Off状态变为On状态是为一次判定
            if (railState && !formState) return true;
            //未点击到强制MISS判定
            if (MusicPlayer.ExactTime - ExactTime > JudgeEnd) return true;
            //其它时刻不构成判定
            return false;
        }
    }
}
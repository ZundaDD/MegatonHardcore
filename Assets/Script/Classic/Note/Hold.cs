using UnityEngine;
using Megaton.Abstract;

namespace Megaton.Classic
{
    /// <summary>
    /// 长按音符
    /// </summary>
    public class Hold : Note
    {
        private JudgeEnum headJudge = JudgeEnum.MISS;
        private float holdTime;
        public bool ifStart { get; private set; } = false;

        public float ExactLength;

        public override float JudgeStart => 0.12f;
        public override float JudgeEnd => ExactLength;

        public override JudgeEnum GetResult()
        {
            float holdRate = holdTime / ExactLength;

            //CRITICAL下降
            if (headJudge == JudgeEnum.CRITICAL && holdRate < 0.9f) 
                headJudge = JudgeEnum.F_PERFECT;

            //PERFECT下降
            if(headJudge == JudgeEnum.S_PERFECT || headJudge == JudgeEnum.F_PERFECT && holdRate < 0.55f)
                headJudge = headJudge > 0 ? JudgeEnum.F_GREAT : JudgeEnum.S_GREAT;

            //GREAT下降
            if(headJudge == JudgeEnum.S_GREAT || headJudge == JudgeEnum.F_GREAT && holdRate < 0.25f)
                headJudge = headJudge > 0 ? JudgeEnum.F_GOOD : JudgeEnum.S_GOOD;

            //MISS补偿
            if (headJudge == JudgeEnum.MISS && holdRate > 0.1f)
                headJudge = JudgeEnum.S_GOOD;

            return headJudge;
        }

        public override (bool success,bool ifcontinue) Judge(bool railState, bool formState)
        {
            float Offset = MusicPlayer.ExactTime - ExactTime;

            //从Off状态变为On状态进行头判
            if (railState && !formState && !ifStart && Offset < JudgeStart && Offset > -JudgeStart)
            {
                headJudge = Tap.TapJudge(Offset);
                ifStart = true;
                return (false, false);
            }

            //Hold积累按下时长
            if (Offset - ExactLength < -0.1f && Offset > 0.1f && railState)
            {
                holdTime += Time.deltaTime;
                return (false, true);
            }
            //Hold结束时得到判定
            if (Offset > ExactLength) return (true, true);

            //其余时刻不构成判定
            return (false, false);
        }
    }
}
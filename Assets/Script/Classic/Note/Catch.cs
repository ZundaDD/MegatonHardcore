using Megaton.Abstract;
using UnityEngine;

namespace Megaton.Classic
{
    /// <summary>
    /// 接音符
    /// </summary>
    public class Catch : Note
    {
        public override float JudgeStart => 0.12f;

        public override float JudgeEnd => 0.12f;

        public override JudgeEnum GetResult()
        {
            float Offset = MusicPlayer.ExactTime - ExactTime;
            if (Mathf.Abs(Offset) < 0.12f) return JudgeEnum.CRITICAL;
            else return JudgeEnum.MISS;
        }


        public override bool Judge(bool railState, bool formState)
        {
            //在On状态就进行判定
            if (railState) return true;
            //未点击到强制MISS判定
            if (MusicPlayer.ExactTime - ExactTime > JudgeEnd) return true;
            //其它时刻不构成判定
            return false;
        }
    }
}
using Megaton.UI;
using UnityEngine;

namespace Megaton.Abstract
{
    /// <summary>
    /// Note的实际GO，负责显示外形，反馈状态
    /// 并且提供判定文字的映射位置
    /// </summary>
    public abstract class NoteSO : MonoBehaviour
    {
        protected Note note;

        /// <summary>
        /// 与数据note绑定，定位时间轴
        /// </summary>
        /// <param name="note">实际note</param>
        public virtual void Bind(Note note)
        {
            this.note = note;
            transform.position = new(0, 0, GameVar.Velocity * (GameVar.PrepareFrame * Time.fixedDeltaTime + note.ExactTime));
            note.OnJudge += Judge;
            note.OnResult += (judge) => JudgeFeedBack.Ins.SummonAt(judge, this.transform.position);
        }

        /// <summary>
        /// 对应Note判定时触发的SO变化
        /// </summary>
        /// <param name="railState"></param>
        /// <param name="formState"></param>
        public abstract void Judge(bool railState,bool formState);
    }
}

using Megaton.UI;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Megaton.Abstract
{
    /// <summary>
    /// 轨道实物的动画反馈
    /// </summary>
    public abstract class RailFeedback : MonoBehaviour
    {
        protected Rail rail;

        public void Awake()
        {
            rail = GetComponent<Rail>();
            rail.OnJudge += JudgeFeedback;
            rail.OnStateChange += TapFeedback;
        }

        /// <summary>
        /// 按键反馈
        /// </summary>
        /// <param name="holdDown">轨道是否按下</param>
        public abstract void TapFeedback(bool holdDown);

        /// <summary>
        /// 判定反馈
        /// </summary>
        /// <param name="success">是否成功判定</param>
        /// <param name="continue">是否持续判定</param>
        public abstract void JudgeFeedback(bool success,bool @continue);
    }
}

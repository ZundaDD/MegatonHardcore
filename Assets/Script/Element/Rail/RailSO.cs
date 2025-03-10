
using Megaton.UI;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Megaton.Abstract
{
    /// <summary>
    /// 轨道实物的抽象基类
    /// </summary>
    public abstract class RailSO : MonoBehaviour
    {
        protected Rail rail;

        public void Awake()
        {
            rail = GetComponent<Rail>();
            rail.OnJudge += Feedback;
        }

        /// <summary>
        /// 根据note种类来进行反馈判断
        /// </summary>
        /// <param name="note">note</param>
        /// <param name="state">状态</param>
        public abstract void Feedback(bool success,bool ifcontinue);
    }
}

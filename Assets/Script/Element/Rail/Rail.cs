using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Megaton.Abstract
{
    /// <summary>
    /// 轨道的抽象类
    /// </summary>
    public abstract class Rail : MonoBehaviour
    {
        public List<Note> Notes;
        public List<Command> Commands;
        public RailEnum Id;

        /// <summary>
        /// 按下
        /// </summary>
        public abstract void Tap(InputAction.CallbackContext ctx);

        /// <summary>
        /// 按住
        /// </summary>
        /// <param name="ctx"></param>
        public abstract void Hold(InputAction.CallbackContext ctx);

        /// <summary>
        /// 松开
        /// </summary>
        public abstract void Release(InputAction.CallbackContext ctx);
    }
}
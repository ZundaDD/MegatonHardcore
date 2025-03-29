using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Megaton
{
    /// <summary>
    /// 场景栈，控制场景的输入绑定
    /// </summary>
    public class BindStack
    {
        public class Record
        { }

        public void BindCallback(System.Action<InputAction.CallbackContext> holder, Func<InputAction.CallbackContext> callback)
        {

        }
    }
}
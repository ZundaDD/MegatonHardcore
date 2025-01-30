using UnityEngine;

namespace Megaton
{
    /// <summary>
    /// 游玩模式，负责进行场景的初始化和流程指引
    /// </summary>
    public abstract class Mode
    {
        public abstract void InputBinding(InputMap inputActions,RailCollection rails);
    }
}
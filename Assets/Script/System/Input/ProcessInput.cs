
using Unity.VisualScripting;
using UnityEngine.InputSystem;

namespace Megaton
{
    public class ProcessInput
    {
        static ProcessInput ins = new();
        public static ProcessInput Ins => ins;


        public InputMap input;

        private ProcessInput()
        {
            input = new();
            input.Player.Disable();
            input.UI.Enable();
        }

        /// <summary>
        /// 修改输入模式
        /// </summary>
        /// <param name="isGame">是否采用游戏模式</param>
        public static void SwitchInputMode(bool isGame)
        {
            if (isGame)
            {
                Ins.input.Player.Enable();
                Ins.input.UI.Disable();
            }
            else
            {
                Ins.input.Player.Disable();
                Ins.input.UI.Enable();
            }
        }

        /// <summary>
        /// 释放轨道，构建新输入
        /// </summary>
        public static void ReleaseRail()
        {
            Ins.input.Dispose();
            Ins.input = new();
            SwitchInputMode(false);
        }

        /// <summary>
        /// 绑定轨道
        /// </summary>
        public static void BindRail(RailCollection rails)
        {
            GameVar.PlayMode.InputBinding(Ins.input, rails);
            SwitchInputMode(true);
        }
    }
}


using UnityEngine.InputSystem;

namespace Megaton
{
    public class ProcessInput
    {
        static ProcessInput ins = new();
        public static ProcessInput Ins => ins;


        public InputMap input;

        public ProcessInput()
        {
            input = new();
            input.Player.Enable();
            BindRail();
        }

        /// <summary>
        /// 绑定轨道
        /// </summary>
        public void BindRail()
        {
            switch(GameVar.Ins.PlayMode)
            {
                case PlayMode._2L2R:
                    input.Player.Left1.started += RailCollection.Ins.Rails[RailEnum.Left1].Tap;
                    input.Player.Left1.canceled += RailCollection.Ins.Rails[RailEnum.Left1].Release;
                    input.Player.Left2.started += RailCollection.Ins.Rails[RailEnum.Left2].Tap;
                    input.Player.Left2.started += RailCollection.Ins.Rails[RailEnum.Left2].Release;
                    input.Player.Right1.started += RailCollection.Ins.Rails[RailEnum.Right1].Tap;
                    input.Player.Right1.started += RailCollection.Ins.Rails[RailEnum.Right1].Release;
                    input.Player.Right2.started += RailCollection.Ins.Rails[RailEnum.Right2].Tap;
                    input.Player.Right2.started += RailCollection.Ins.Rails[RailEnum.Right2].Release;
                    break;
            }
        }
    }
}

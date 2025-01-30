using UnityEngine;

namespace Megaton.Classic
{
    public class L2R2 : Mode
    {
        public override void InputBinding(InputMap inputActions,RailCollection rails)
        {
            inputActions.Player.Left1.started += rails[RailEnum.Left1].Tap;
            inputActions.Player.Left1.canceled += rails[RailEnum.Left1].Release;
            inputActions.Player.Left2.started += rails[RailEnum.Left2].Tap;
            inputActions.Player.Left2.started += rails[RailEnum.Left2].Release;
            inputActions.Player.Right1.started += rails[RailEnum.Right1].Tap;
            inputActions.Player.Right1.started += rails[RailEnum.Right1].Release;
            inputActions.Player.Right2.started += rails[RailEnum.Right2].Tap;
            inputActions.Player.Right2.started += rails[RailEnum.Right2].Release;
        }

    }
}
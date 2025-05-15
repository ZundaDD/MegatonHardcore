using Megaton.Abstract;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Megaton.Classic
{
    [IdentityString("L2R2")]
    public class L2R2 : Mode
    {
        public override void InputBinding(InputMap inputActions,RailCollection rails)
        {
            InputManager.LoadBindingOverride(inputActions.Player.Left1.name);
            InputManager.LoadBindingOverride(inputActions.Player.Left2.name);
            InputManager.LoadBindingOverride(inputActions.Player.Right1.name);
            InputManager.LoadBindingOverride(inputActions.Player.Right2.name);

            inputActions.Player.Left1.started += rails[RailEnum.Left1].Tap;
            inputActions.Player.Left1.canceled += rails[RailEnum.Left1].Release;
            inputActions.Player.Left2.started += rails[RailEnum.Left2].Tap;
            inputActions.Player.Left2.canceled += rails[RailEnum.Left2].Release;
            inputActions.Player.Right1.started += rails[RailEnum.Right1].Tap;
            inputActions.Player.Right1.canceled += rails[RailEnum.Right1].Release;
            inputActions.Player.Right2.started += rails[RailEnum.Right2].Tap;
            inputActions.Player.Right2.canceled += rails[RailEnum.Right2].Release;
        }

        public override void InputRelease(InputMap inputActions, RailCollection rails)
        {
            inputActions.Player.Left1.started -= rails[RailEnum.Left1].Tap;
            inputActions.Player.Left1.canceled -= rails[RailEnum.Left1].Release;
            inputActions.Player.Left2.started -= rails[RailEnum.Left2].Tap;
            inputActions.Player.Left2.canceled -= rails[RailEnum.Left2].Release;
            inputActions.Player.Right1.started -= rails[RailEnum.Right1].Tap;
            inputActions.Player.Right1.canceled -= rails[RailEnum.Right1].Release;
            inputActions.Player.Right2.started -= rails[RailEnum.Right2].Tap;
            inputActions.Player.Right2.canceled -= rails[RailEnum.Right2].Release;
        }

        public override Command ParseCommand(string token,int bpm)
        {
            switch (token[0])
            {
                case 'T':
                    return new Tap();
                case 'C':
                    return new Catch();
                case 'H':
                    if(token.Length > 1)
                    {
                        float length;
                        try
                        {
                            //带$表示直接给出时值
                            if (token[0] == '$')
                            {
                                length = int.Parse(token.Substring(1)) / 1000;
                            }
                            //否则表示四分音的个数
                            else
                            {
                                length = float.Parse(token) * 60 / bpm;
                            }
                        }
                        catch
                        {
                            return null;
                        }
                        return new Hold() {  ExactLength = length };
                    }
                    return null;
                default:
                    return null;
            }
        }

        public override RailEnum ParseRailRelection(string id)
        {
            switch(id)
            {
                case "1":
                    return RailEnum.Left1;
                case "2":
                    return RailEnum.Left2;
                case "3":
                    return RailEnum.Right2;
                case "4":
                    return RailEnum.Right1;
                default:
                    return RailEnum.Undefined;
            }
        }
    }
}
using Megaton.Abstract;

namespace Megaton.Classic
{
    [IdentityString("L2R2",3)]
    public class L2R2 : Mode
    {
        public override void InputBinding(InputMap inputActions,RailCollection rails)
        {
            inputActions.Player.Left1.started += rails[RailEnum.Left1].Tap;
            inputActions.Player.Left1.canceled += rails[RailEnum.Left1].Release;
            inputActions.Player.Left2.started += rails[RailEnum.Left2].Tap;
            inputActions.Player.Left2.canceled += rails[RailEnum.Left2].Release;
            inputActions.Player.Right1.started += rails[RailEnum.Right1].Tap;
            inputActions.Player.Right1.canceled += rails[RailEnum.Right1].Release;
            inputActions.Player.Right2.started += rails[RailEnum.Right2].Tap;
            inputActions.Player.Right2.canceled += rails[RailEnum.Right2].Release;
        }

        public override Command ParseCommand(string[] token,int bpm)
        {
            switch (token[2])
            {
                case "T":
                    return new Tap();
                case "C":
                    return new Catch();
                case "H":
                    if(token.Length > 3)
                    {
                        int length = int.Parse(token[3]);
                        int divide = int.Parse(token[0]);
                        var obj = new Hold();
                        obj.ExactLength = length * 60 / (bpm * divide / 8);
                        return obj;
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
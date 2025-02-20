using UnityEngine;
using UnityEngine.InputSystem;
using Megaton.Abstract;

namespace Megaton.Classic
{
    /// <summary>
    /// 直轨
    /// </summary>
    public class StraightRail : Rail
    {
        private bool on = false;
        private bool[] sample = new bool[2];

        public override void Hold(InputAction.CallbackContext ctx) { }

        public override bool[] QueryNoteState(Note note) => sample;

        public override void Release(InputAction.CallbackContext ctx)
        {
            
            on = false;
        }

        public override void Tap(InputAction.CallbackContext ctx)
        {
            
            on = true;
        }

        public override void Sample()
        {
            //采样
            sample[1] = sample[0];
            sample[0] = on;
        }
    }
}
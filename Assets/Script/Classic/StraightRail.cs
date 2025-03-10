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
        private (bool current, bool form) sample = new(false, false);

        public override void Hold(InputAction.CallbackContext ctx) { }

        public override (bool current,bool form) QueryNoteState(Note note) => sample;

        public override void Release(InputAction.CallbackContext ctx) => on = false;

        public override void Tap(InputAction.CallbackContext ctx) => on = true;

        public override void Sample()
        {
            sample.form = sample.current;
            sample.current = on;
        }
    }
}
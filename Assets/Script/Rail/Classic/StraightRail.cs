using UnityEngine;
using UnityEngine.InputSystem;

namespace Megaton.Classic
{
    /// <summary>
    /// 直轨
    /// </summary>
    public class StraightRail : Rail
    {
        private bool on = false;

        public override void Hold(InputAction.CallbackContext ctx) { }

        public override void Release(InputAction.CallbackContext ctx) => on = false;

        public override void Tap(InputAction.CallbackContext ctx) => on = true;

    }
}
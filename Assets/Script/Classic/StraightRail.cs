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

        public override void Hold(InputAction.CallbackContext ctx) { }

        public override void Release(InputAction.CallbackContext ctx)
        {
            Debug.Log(Id + "Released!");
            on = false;
        }

        public override void Tap(InputAction.CallbackContext ctx)
        {
            Debug.Log(Id + "Taped!");
            on = true;
        }
    }
}
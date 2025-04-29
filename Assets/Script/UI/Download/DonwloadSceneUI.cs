using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Megaton.UI
{
    public class DonwloadSceneUI : BottomUI
    {
        #region 交互这一块
        protected override void EnableInteract()
        {
            base.EnableInteract();
            InputManager.Input.UI.Escape.performed += ReturnToMainMenu;
        }

        protected override void DisableInteract()
        {
            base.DisableInteract();
            InputManager.Input.UI.Escape.performed -= ReturnToMainMenu;
        }

        private void ReturnToMainMenu(InputAction.CallbackContext ctx) => SceneSwitch.Ending(1);
        #endregion
    }
}

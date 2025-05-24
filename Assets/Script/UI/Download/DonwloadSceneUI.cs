using Megaton.Web;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Megaton.UI
{
    public class DonwloadSceneUI : BottomUI
    {
        [SerializeField] private ResponseStateUI responseStateUI;

        private void Start()
        {
            DownloadSceneController.OnStateChanged += SetResponseState;
        }

        private void OnDestroy()
        {
            DownloadSceneController.OnStateChanged -= SetResponseState;
        }


        public void SetResponseState(int state)
        {
            switch (state)
            {
                case 0:
                    responseStateUI.LoadingState();
                    break;
                case 1:
                    responseStateUI.Hide();
                    break;
                case 2:
                    responseStateUI.ErrorState();
                    break;
            }
        }

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

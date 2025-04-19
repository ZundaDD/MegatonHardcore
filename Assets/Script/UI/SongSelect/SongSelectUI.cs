using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Megaton.UI
{
    /// <summary>
    /// 选歌页面的总控制器
    /// </summary>
    public class SongSelectUI : BottomUI
    {
        [SerializeField] private SettingUI settingCanvas;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button settingButton;
        //[SerializeField] private SelectChartInfoUI selectedHandler;

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

        protected override bool Open()
        {
            settingButton.onClick.AddListener(() => Push(settingCanvas));
            exitButton.onClick.AddListener(() => SceneSwitch.Ending(1));

            return true;
        }

        private void ReturnToMainMenu(InputAction.CallbackContext ctx) => SceneSwitch.Ending(1);
    }
}
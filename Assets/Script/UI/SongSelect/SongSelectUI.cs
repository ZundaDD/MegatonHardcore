using DanielLochner.Assets.SimpleScrollSnap;
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
        [SerializeField] private SongList songList;
        [SerializeField] private SimpleScrollSnap scroller;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button settingButton;
        //[SerializeField] private SelectChartInfoUI selectedHandler;

        #region 交互这一块
        protected override void EnableInteract()
        {
            base.EnableInteract();
            InputManager.Input.UI.Escape.performed += ReturnToMainMenu;
            InputManager.Input.UI.Scroll.performed += songList.OnScroll;
            InputManager.Input.UI.Navigation.performed += songList.OnNavigation;
        }

        protected override void DisableInteract()
        {
            base.DisableInteract();
            InputManager.Input.UI.Escape.performed -= ReturnToMainMenu;
            InputManager.Input.UI.Scroll.performed -= songList.OnScroll;
            InputManager.Input.UI.Navigation.performed -= songList.OnNavigation;
        }

        protected override bool Open()
        {
            settingButton.onClick.AddListener(() => Push(settingCanvas));
            exitButton.onClick.AddListener(() => SceneSwitch.Ending(1));

            return true;
        }

        private void ReturnToMainMenu(InputAction.CallbackContext ctx) => SceneSwitch.Ending(1);
        #endregion
    }
}
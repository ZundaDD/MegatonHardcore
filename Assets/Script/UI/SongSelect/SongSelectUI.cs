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
        [Header("组件引用")]
        [SerializeField] private SettingUI settingCanvas;
        [SerializeField] private SongList songList;
        [SerializeField] private SimpleScrollSnap scroller;
        [SerializeField] private SelectChartInfoUI selectInfo;
        [Header("按钮引用")]
        [SerializeField] private Button exitButton;
        [SerializeField] private Button settingButton;

        private void Start()
        {
            if (GameVar.ChartInfos.Count == 0) selectInfo.SetToNull();
            else selectInfo.ChangeSelected(GameVar.ChartInfos[songList.firstSelectPanel % GameVar.ChartInfos.Count]);    
        }

        /// <summary>
        /// 更改当前选择的歌曲
        /// </summary>
        /// <param name="select"></param>
        private void ChangeSelect(int select) => selectInfo.ChangeSelected(GameVar.ChartInfos[scroller.RealIndex]);

        #region 交互这一块
        protected override void EnableInteract()
        {
            base.EnableInteract();
            scroller.OnPanelSelected.AddListener(ChangeSelect);
            InputManager.Input.UI.Escape.performed += ReturnToMainMenu;
            InputManager.Input.UI.Scroll.performed += songList.OnScroll;
            InputManager.Input.UI.Navigation.performed += songList.OnNavigation;
        }

        protected override void DisableInteract()
        {
            base.DisableInteract();
            scroller.OnPanelSelected.RemoveListener(ChangeSelect);
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
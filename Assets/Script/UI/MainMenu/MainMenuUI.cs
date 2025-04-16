using UnityEngine;

namespace Megaton.UI
{
    public class MainMenuUI : BottomUI
    {
        [SerializeField] private SettingUI settingCanvas;

        public void OpenSetting() => Push(settingCanvas);
    }
}

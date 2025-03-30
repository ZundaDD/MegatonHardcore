using Megaton.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class ConfigCellView : MonoBehaviour
    {
        [SerializeField] private Button addButton;
        [SerializeField] private Button minusButton;
        [SerializeField] private Text configName;
        [SerializeField] private Text configValue;
        private SettingVarible config;

        public void Bind(SettingVarible config,string name,string syntax = "$")
        {
            this.config = config;
            configName.text = name;
            configValue.text = syntax.Replace("$",config.Display);
            CheckButtonState();
            addButton.onClick.AddListener(() =>
            {
                config.Add();
                GlobalEffectPlayer.PlayEffect(AudioEffect.OnSongSelect);
                configValue.text = syntax.Replace("$", config.Display);
                CheckButtonState();
            });
            minusButton.onClick.AddListener(() =>
            {
                config.Minus();
                GlobalEffectPlayer.PlayEffect(AudioEffect.OnSongSelect);
                configValue.text = syntax.Replace("$", config.Display);
                CheckButtonState();
            });
        }

        private void CheckButtonState()
        {
            var state = config.SwitchState();
            addButton.interactable = state[0];
            minusButton.interactable = state[1];
        }
    }
}
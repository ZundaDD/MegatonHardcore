using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Megaton.UI
{
    public class Rebinder : MonoBehaviour
    {
        [Header("绑定配置")]
        [SerializeField] private InputActionReference inputRef;
        [SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;
        [SerializeField] private InputBinding inputBinding;
        [Header("UI组件")]
        [SerializeField] private Text keyName;
        [SerializeField] private Text keyValue;
        [SerializeField] private Button resetButton;
        [SerializeField] private Button bindButton;
        private string actionName;
        private int bindingIndex;

        public void SelectTarget(string keyName,InputActionReference inputRef)
        {
            this.keyName.text = keyName;
            this.inputRef = inputRef;
            GetBindingInfo();
            InputManager.LoadBindingOverride(actionName);
            UpdateUI();
        }

        private void OnEnable()
        {
            bindButton.onClick.AddListener(() => DoRebind());
            resetButton.onClick.AddListener(() => ResetBinding());

            InputManager.rebindComplete += UpdateUI;
            InputManager.rebindCanceled += UpdateUI;
        }

        private void OnDisable()
        {
            InputManager.rebindComplete -= UpdateUI;
            InputManager.rebindCanceled -= UpdateUI;
        }

        private void GetBindingInfo()
        {
            if (inputRef.action != null)
                actionName = inputRef.action.name;

            if (inputRef.action.bindings.Count > 0)
            {
                inputBinding = inputRef.action.bindings[0];
                bindingIndex = 0;
            }
        }

        private void UpdateUI()
        {
            if (keyValue != null)
            {
                if (Application.isPlaying)
                {
                    keyValue.text = InputManager.GetBindingName(actionName, bindingIndex);
                }
                else
                    keyValue.text = inputRef.action.GetBindingDisplayString(bindingIndex);
            }
        }

        private void DoRebind()
        {
            InputManager.StartRebind(actionName, bindingIndex, keyName, true);
        }

        private void ResetBinding()
        {
            InputManager.ResetBinding(actionName, bindingIndex);
            UpdateUI();
        }
    }
}
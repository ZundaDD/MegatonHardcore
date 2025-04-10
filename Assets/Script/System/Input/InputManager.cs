
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Megaton
{
    public class InputManager
    {
        public static InputMap Input = new();

        static InputManager ins = new();
        public static InputManager Ins => ins;

        public static event Action rebindComplete;
        public static event Action rebindCanceled;
        public static event Action<InputAction, int> rebindStarted;

        public static InputActionRebindingExtensions.RebindingOperation rebind;

        /// <summary>
        /// 修改输入模式
        /// </summary>
        /// <param name="isGame">是否采用游戏模式</param>
        public static void SwitchInputMode(bool isGame)
        {
            if (isGame)
            {
                Input.Player.Enable();
                Input.UI.Disable();
            }
            else
            {
                Input.Player.Disable();
                Input.UI.Enable();
            }
        }

        public static void StartRebind(string actionName, int bindingIndex, Text statusText, bool excludeMouse)
        {
            InputAction action = Input.asset.FindAction(actionName);
            if (action == null || action.bindings.Count <= bindingIndex)
            {
                Debug.Log("Couldn't find action or binding");
                return;
            }

            if (action.bindings[bindingIndex].isComposite)
            {
                var firstPartIndex = bindingIndex + 1;
                if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isComposite)
                    DoRebind(action, bindingIndex, statusText, true, excludeMouse);
            }
            else
                DoRebind(action, bindingIndex, statusText, false, excludeMouse);
        }

        private static void DoRebind(InputAction actionToRebind, int bindingIndex, Text statusText, bool allCompositeParts, bool excludeMouse)
        {
            if (rebind != null || actionToRebind == null || bindingIndex < 0)
                return;

            statusText.text = $"Press";

            actionToRebind.Disable();

            rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

            rebind.OnComplete(operation =>
            {
                actionToRebind.Enable();
                operation.Dispose();

                if (allCompositeParts)
                {
                    var nextBindingIndex = bindingIndex + 1;
                    if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                        DoRebind(actionToRebind, nextBindingIndex, statusText, allCompositeParts, excludeMouse);
                }

                SaveBindingOverride(actionToRebind);
                rebindComplete?.Invoke();
                rebind = null;
            });

            rebind.OnCancel(operation =>
            {
                actionToRebind.Enable();
                operation.Dispose();

                rebindCanceled?.Invoke();
                rebind = null;
            });

            rebind.WithCancelingThrough("<Keyboard>/escape");

            if (excludeMouse)
                rebind.WithControlsExcluding("Mouse");

            rebindStarted?.Invoke(actionToRebind, bindingIndex);
            rebind.Start(); //actually starts the rebinding process
        }

        public static string GetBindingName(string actionName, int bindingIndex)
        {
            InputAction action = Input.asset.FindAction(actionName);
            return action.GetBindingDisplayString(bindingIndex);
        }

        private static void SaveBindingOverride(InputAction action)
        {
            for (int i = 0; i < action.bindings.Count; i++)
            {
                PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
            }
        }

        public static void LoadBindingOverride(string actionName)
        {
            InputAction action = Input.asset.FindAction(actionName);

            for (int i = 0; i < action.bindings.Count; i++)
            {
                if (!string.IsNullOrEmpty(PlayerPrefs.GetString(action.actionMap + action.name + i)))
                    action.ApplyBindingOverride(i, PlayerPrefs.GetString(action.actionMap + action.name + i));
            }
        }

        public static void ResetBinding(string actionName, int bindingIndex)
        {
            InputAction action = Input.asset.FindAction(actionName);

            if (action == null || action.bindings.Count <= bindingIndex)
            {
                Debug.Log("Could not find action or binding");
                return;
            }

            if (action.bindings[bindingIndex].isComposite)
            {
                for (int i = bindingIndex; i < action.bindings.Count && action.bindings[i].isComposite; i++)
                    action.RemoveBindingOverride(i);
            }
            else
                action.RemoveBindingOverride(bindingIndex);

            SaveBindingOverride(action);
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Script/System/Input/InputMap.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputMap: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""df70fa95-8a34-4494-b137-73ab6b9c7d37"",
            ""actions"": [
                {
                    ""name"": ""Left-1"",
                    ""type"": ""Button"",
                    ""id"": ""f70065c9-e175-4bf3-8bf8-ea775392de69"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left-2"",
                    ""type"": ""Button"",
                    ""id"": ""f9b8c7e3-63ce-4b1c-8cba-7f440145c89a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left-3"",
                    ""type"": ""Button"",
                    ""id"": ""1b787971-96ad-4661-b886-957da4aa13f6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left-4"",
                    ""type"": ""Button"",
                    ""id"": ""98e00e1c-465e-420b-a9e0-7bc9d756a5f8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right-1"",
                    ""type"": ""Button"",
                    ""id"": ""cab5ea28-91ab-40dd-b617-54c6c04f94dd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right-2"",
                    ""type"": ""Button"",
                    ""id"": ""69c7e079-b7af-4ead-b979-e913facf084a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Right-3"",
                    ""type"": ""Button"",
                    ""id"": ""3800aa56-f991-4c27-af9f-6fce1ea08bd8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""05af18ae-3127-4688-b14b-1c9fd9803d4a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse-L"",
                    ""type"": ""Button"",
                    ""id"": ""7eb83f3f-ea5d-4f74-bed1-f2ef4949e5f5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse-M"",
                    ""type"": ""Button"",
                    ""id"": ""8f0e8fad-888d-4ace-b042-d86aefa41df6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Mouse-R"",
                    ""type"": ""Button"",
                    ""id"": ""3a5fb205-fcec-4547-8c06-d476812d5839"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse-P"",
                    ""type"": ""Value"",
                    ""id"": ""57169dcb-ca2e-4da1-a02e-a3487832d0d5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""8ca9f708-2ff1-4afb-a25e-7019e2f27434"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""abaf61c2-900a-47a7-8e4e-8a7ddfdf32e2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left-1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b177be01-bd17-482e-8f72-3b01b04de7cf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left-2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7db9eb6-4168-4b1e-b927-5314aae8a626"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left-3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""737506f8-98ee-4b51-91de-0b9ad999c437"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left-4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7128a531-19a8-4c22-95da-b41eebaebe4c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse-L"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5e85799-b3dc-4715-b629-883c85914186"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse-R"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ed2eab1-c8cb-4e00-977d-3cd567c55181"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right-1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9facccc3-0d67-43b0-a22d-f8f857619464"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right-2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""311adcc2-6c67-4d39-8f15-cef8582a97af"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Right-3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09ca0aaf-34f0-4588-8c8e-f7bbf207a784"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse-M"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b36201aa-eaac-4577-9593-e47fd9eafd26"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e70b419-2964-4f5f-ac85-22545f883e68"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse-P"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf66e42f-56e6-4c6a-a552-beff6095f902"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""272f6d14-89ba-496f-b7ff-215263d3219f"",
            ""actions"": [
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""3a5e3637-f046-4a66-84f7-75095acc99f0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""b0029786-7b0b-4d94-95e0-77e6ea2205c1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a5306760-4b64-44f5-b702-08ce4a858b6d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""330a42ba-8beb-4063-ba5f-cdf17509d0b7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Left1 = m_Player.FindAction("Left-1", throwIfNotFound: true);
        m_Player_Left2 = m_Player.FindAction("Left-2", throwIfNotFound: true);
        m_Player_Left3 = m_Player.FindAction("Left-3", throwIfNotFound: true);
        m_Player_Left4 = m_Player.FindAction("Left-4", throwIfNotFound: true);
        m_Player_Right1 = m_Player.FindAction("Right-1", throwIfNotFound: true);
        m_Player_Right2 = m_Player.FindAction("Right-2", throwIfNotFound: true);
        m_Player_Right3 = m_Player.FindAction("Right-3", throwIfNotFound: true);
        m_Player_Escape = m_Player.FindAction("Escape", throwIfNotFound: true);
        m_Player_MouseL = m_Player.FindAction("Mouse-L", throwIfNotFound: true);
        m_Player_MouseM = m_Player.FindAction("Mouse-M", throwIfNotFound: true);
        m_Player_MouseR = m_Player.FindAction("Mouse-R", throwIfNotFound: true);
        m_Player_MouseP = m_Player.FindAction("Mouse-P", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Space = m_UI.FindAction("Space", throwIfNotFound: true);
        m_UI_LeftMouse = m_UI.FindAction("Left Mouse", throwIfNotFound: true);
    }

    ~@InputMap()
    {
        UnityEngine.Debug.Assert(!m_Player.enabled, "This will cause a leak and performance issues, InputMap.Player.Disable() has not been called.");
        UnityEngine.Debug.Assert(!m_UI.enabled, "This will cause a leak and performance issues, InputMap.UI.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Left1;
    private readonly InputAction m_Player_Left2;
    private readonly InputAction m_Player_Left3;
    private readonly InputAction m_Player_Left4;
    private readonly InputAction m_Player_Right1;
    private readonly InputAction m_Player_Right2;
    private readonly InputAction m_Player_Right3;
    private readonly InputAction m_Player_Escape;
    private readonly InputAction m_Player_MouseL;
    private readonly InputAction m_Player_MouseM;
    private readonly InputAction m_Player_MouseR;
    private readonly InputAction m_Player_MouseP;
    private readonly InputAction m_Player_Pause;
    public struct PlayerActions
    {
        private @InputMap m_Wrapper;
        public PlayerActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Left1 => m_Wrapper.m_Player_Left1;
        public InputAction @Left2 => m_Wrapper.m_Player_Left2;
        public InputAction @Left3 => m_Wrapper.m_Player_Left3;
        public InputAction @Left4 => m_Wrapper.m_Player_Left4;
        public InputAction @Right1 => m_Wrapper.m_Player_Right1;
        public InputAction @Right2 => m_Wrapper.m_Player_Right2;
        public InputAction @Right3 => m_Wrapper.m_Player_Right3;
        public InputAction @Escape => m_Wrapper.m_Player_Escape;
        public InputAction @MouseL => m_Wrapper.m_Player_MouseL;
        public InputAction @MouseM => m_Wrapper.m_Player_MouseM;
        public InputAction @MouseR => m_Wrapper.m_Player_MouseR;
        public InputAction @MouseP => m_Wrapper.m_Player_MouseP;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Left1.started += instance.OnLeft1;
            @Left1.performed += instance.OnLeft1;
            @Left1.canceled += instance.OnLeft1;
            @Left2.started += instance.OnLeft2;
            @Left2.performed += instance.OnLeft2;
            @Left2.canceled += instance.OnLeft2;
            @Left3.started += instance.OnLeft3;
            @Left3.performed += instance.OnLeft3;
            @Left3.canceled += instance.OnLeft3;
            @Left4.started += instance.OnLeft4;
            @Left4.performed += instance.OnLeft4;
            @Left4.canceled += instance.OnLeft4;
            @Right1.started += instance.OnRight1;
            @Right1.performed += instance.OnRight1;
            @Right1.canceled += instance.OnRight1;
            @Right2.started += instance.OnRight2;
            @Right2.performed += instance.OnRight2;
            @Right2.canceled += instance.OnRight2;
            @Right3.started += instance.OnRight3;
            @Right3.performed += instance.OnRight3;
            @Right3.canceled += instance.OnRight3;
            @Escape.started += instance.OnEscape;
            @Escape.performed += instance.OnEscape;
            @Escape.canceled += instance.OnEscape;
            @MouseL.started += instance.OnMouseL;
            @MouseL.performed += instance.OnMouseL;
            @MouseL.canceled += instance.OnMouseL;
            @MouseM.started += instance.OnMouseM;
            @MouseM.performed += instance.OnMouseM;
            @MouseM.canceled += instance.OnMouseM;
            @MouseR.started += instance.OnMouseR;
            @MouseR.performed += instance.OnMouseR;
            @MouseR.canceled += instance.OnMouseR;
            @MouseP.started += instance.OnMouseP;
            @MouseP.performed += instance.OnMouseP;
            @MouseP.canceled += instance.OnMouseP;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Left1.started -= instance.OnLeft1;
            @Left1.performed -= instance.OnLeft1;
            @Left1.canceled -= instance.OnLeft1;
            @Left2.started -= instance.OnLeft2;
            @Left2.performed -= instance.OnLeft2;
            @Left2.canceled -= instance.OnLeft2;
            @Left3.started -= instance.OnLeft3;
            @Left3.performed -= instance.OnLeft3;
            @Left3.canceled -= instance.OnLeft3;
            @Left4.started -= instance.OnLeft4;
            @Left4.performed -= instance.OnLeft4;
            @Left4.canceled -= instance.OnLeft4;
            @Right1.started -= instance.OnRight1;
            @Right1.performed -= instance.OnRight1;
            @Right1.canceled -= instance.OnRight1;
            @Right2.started -= instance.OnRight2;
            @Right2.performed -= instance.OnRight2;
            @Right2.canceled -= instance.OnRight2;
            @Right3.started -= instance.OnRight3;
            @Right3.performed -= instance.OnRight3;
            @Right3.canceled -= instance.OnRight3;
            @Escape.started -= instance.OnEscape;
            @Escape.performed -= instance.OnEscape;
            @Escape.canceled -= instance.OnEscape;
            @MouseL.started -= instance.OnMouseL;
            @MouseL.performed -= instance.OnMouseL;
            @MouseL.canceled -= instance.OnMouseL;
            @MouseM.started -= instance.OnMouseM;
            @MouseM.performed -= instance.OnMouseM;
            @MouseM.canceled -= instance.OnMouseM;
            @MouseR.started -= instance.OnMouseR;
            @MouseR.performed -= instance.OnMouseR;
            @MouseR.canceled -= instance.OnMouseR;
            @MouseP.started -= instance.OnMouseP;
            @MouseP.performed -= instance.OnMouseP;
            @MouseP.canceled -= instance.OnMouseP;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_Space;
    private readonly InputAction m_UI_LeftMouse;
    public struct UIActions
    {
        private @InputMap m_Wrapper;
        public UIActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Space => m_Wrapper.m_UI_Space;
        public InputAction @LeftMouse => m_Wrapper.m_UI_LeftMouse;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @Space.started += instance.OnSpace;
            @Space.performed += instance.OnSpace;
            @Space.canceled += instance.OnSpace;
            @LeftMouse.started += instance.OnLeftMouse;
            @LeftMouse.performed += instance.OnLeftMouse;
            @LeftMouse.canceled += instance.OnLeftMouse;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @Space.started -= instance.OnSpace;
            @Space.performed -= instance.OnSpace;
            @Space.canceled -= instance.OnSpace;
            @LeftMouse.started -= instance.OnLeftMouse;
            @LeftMouse.performed -= instance.OnLeftMouse;
            @LeftMouse.canceled -= instance.OnLeftMouse;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnLeft1(InputAction.CallbackContext context);
        void OnLeft2(InputAction.CallbackContext context);
        void OnLeft3(InputAction.CallbackContext context);
        void OnLeft4(InputAction.CallbackContext context);
        void OnRight1(InputAction.CallbackContext context);
        void OnRight2(InputAction.CallbackContext context);
        void OnRight3(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnMouseL(InputAction.CallbackContext context);
        void OnMouseM(InputAction.CallbackContext context);
        void OnMouseR(InputAction.CallbackContext context);
        void OnMouseP(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnSpace(InputAction.CallbackContext context);
        void OnLeftMouse(InputAction.CallbackContext context);
    }
}

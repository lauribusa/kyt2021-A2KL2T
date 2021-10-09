// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""2c6b7599-f90c-4853-a15a-c89be82c8ad9"",
            ""actions"": [
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""56ab836e-e508-44da-b1b1-99bf1c65c7dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a304f823-5cab-47f5-b129-199aa21e8a75"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aiming"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d9d1c6cd-46e9-404c-a1bf-2384ca064f56"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""93f62b98-3335-4cff-83ca-bb157017e114"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XboxController"",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""RightStick"",
                    ""id"": ""79ae6171-23e5-4af5-bd1a-bc23f2d5681e"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e24d0955-fe5a-4871-be89-5a0df2ced90a"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""60e65796-a8e0-4baa-8f77-2037536afaa8"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2696a135-5900-4627-a88b-a89c9b659ccf"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""827f9a19-e1f4-403d-8e4c-e4197c616c94"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aiming"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""b45f9d3f-1762-431e-aa2b-c0d994f7cd2b"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""943c61e0-7a34-4a55-a298-29419249c69e"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""270011e6-9440-4361-9014-337c64151d22"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2892b977-1b67-4545-880a-849faac5a826"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""71625db6-5dd4-4c54-84c1-75df347f7b7c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""XboxController"",
            ""bindingGroup"": ""XboxController"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_Action = m_Player1.FindAction("Action", throwIfNotFound: true);
        m_Player1_Movement = m_Player1.FindAction("Movement", throwIfNotFound: true);
        m_Player1_Aiming = m_Player1.FindAction("Aiming", throwIfNotFound: true);
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

    // Player1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_Action;
    private readonly InputAction m_Player1_Movement;
    private readonly InputAction m_Player1_Aiming;
    public struct Player1Actions
    {
        private @PlayerInput m_Wrapper;
        public Player1Actions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Action => m_Wrapper.m_Player1_Action;
        public InputAction @Movement => m_Wrapper.m_Player1_Movement;
        public InputAction @Aiming => m_Wrapper.m_Player1_Aiming;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @Action.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAction;
                @Movement.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMovement;
                @Aiming.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAiming;
                @Aiming.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAiming;
                @Aiming.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAiming;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Aiming.started += instance.OnAiming;
                @Aiming.performed += instance.OnAiming;
                @Aiming.canceled += instance.OnAiming;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);
    private int m_XboxControllerSchemeIndex = -1;
    public InputControlScheme XboxControllerScheme
    {
        get
        {
            if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("XboxController");
            return asset.controlSchemes[m_XboxControllerSchemeIndex];
        }
    }
    public interface IPlayer1Actions
    {
        void OnAction(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnAiming(InputAction.CallbackContext context);
    }
}

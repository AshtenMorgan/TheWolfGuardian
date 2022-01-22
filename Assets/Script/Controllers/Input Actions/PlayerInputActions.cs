//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Script/Controllers/Input Actions/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerHuman"",
            ""id"": ""9f38a19a-9ff0-4aac-a089-a5cc8dedbed1"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8c929325-7d25-4982-87f7-4ae38069caaa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ff7ca14b-56ab-4f4e-8504-e0e1062825fd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SprintStart"",
                    ""type"": ""Button"",
                    ""id"": ""da7f6a79-f0f4-4057-929d-52cd96456aad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SprintEnd"",
                    ""type"": ""Button"",
                    ""id"": ""b8b75f2a-faac-46fb-a04e-c6ce471cae70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b2e71648-b59b-4992-9925-7f1817b93c72"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dbe45bb-c969-4854-9ddd-509acd27fbe6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""99b641d1-205d-4270-9f84-e4441cd93094"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""359c9240-fa7e-4865-bb25-44426fefc444"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""95810c69-3b57-4ef4-9899-888ce61c9487"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5ac8db82-0be3-4d4a-bbcf-ee3c1d867490"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SprintStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd8c0862-8229-41c5-ac29-a307b2673593"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SprintEnd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerHuman
        m_PlayerHuman = asset.FindActionMap("PlayerHuman", throwIfNotFound: true);
        m_PlayerHuman_Jump = m_PlayerHuman.FindAction("Jump", throwIfNotFound: true);
        m_PlayerHuman_Move = m_PlayerHuman.FindAction("Move", throwIfNotFound: true);
        m_PlayerHuman_SprintStart = m_PlayerHuman.FindAction("SprintStart", throwIfNotFound: true);
        m_PlayerHuman_SprintEnd = m_PlayerHuman.FindAction("SprintEnd", throwIfNotFound: true);
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

    // PlayerHuman
    private readonly InputActionMap m_PlayerHuman;
    private IPlayerHumanActions m_PlayerHumanActionsCallbackInterface;
    private readonly InputAction m_PlayerHuman_Jump;
    private readonly InputAction m_PlayerHuman_Move;
    private readonly InputAction m_PlayerHuman_SprintStart;
    private readonly InputAction m_PlayerHuman_SprintEnd;
    public struct PlayerHumanActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerHumanActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_PlayerHuman_Jump;
        public InputAction @Move => m_Wrapper.m_PlayerHuman_Move;
        public InputAction @SprintStart => m_Wrapper.m_PlayerHuman_SprintStart;
        public InputAction @SprintEnd => m_Wrapper.m_PlayerHuman_SprintEnd;
        public InputActionMap Get() { return m_Wrapper.m_PlayerHuman; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerHumanActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerHumanActions instance)
        {
            if (m_Wrapper.m_PlayerHumanActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnMove;
                @SprintStart.started -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnSprintStart;
                @SprintStart.performed -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnSprintStart;
                @SprintStart.canceled -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnSprintStart;
                @SprintEnd.started -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnSprintEnd;
                @SprintEnd.performed -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnSprintEnd;
                @SprintEnd.canceled -= m_Wrapper.m_PlayerHumanActionsCallbackInterface.OnSprintEnd;
            }
            m_Wrapper.m_PlayerHumanActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @SprintStart.started += instance.OnSprintStart;
                @SprintStart.performed += instance.OnSprintStart;
                @SprintStart.canceled += instance.OnSprintStart;
                @SprintEnd.started += instance.OnSprintEnd;
                @SprintEnd.performed += instance.OnSprintEnd;
                @SprintEnd.canceled += instance.OnSprintEnd;
            }
        }
    }
    public PlayerHumanActions @PlayerHuman => new PlayerHumanActions(this);
    public interface IPlayerHumanActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSprintStart(InputAction.CallbackContext context);
        void OnSprintEnd(InputAction.CallbackContext context);
    }
}

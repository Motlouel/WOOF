//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/Scripts/Inputs/InputTactile.inputactions
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

public partial class @InputTactile : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputTactile()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputTactile"",
    ""maps"": [
        {
            ""name"": ""Basic"",
            ""id"": ""fbeb9c0f-c6a5-4c67-af3e-504dadcb502c"",
            ""actions"": [
                {
                    ""name"": ""TouchAction"",
                    ""type"": ""PassThrough"",
                    ""id"": ""32ceb171-64b8-4b92-b383-466895f25b43"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TouchLocation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ee4a012d-820e-4f15-9600-97fc277f890b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0ea213f7-4d12-4004-8bbb-a50de2a2fee6"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44c8cb5e-12cd-45a7-95ff-14ae1d57fe4d"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchLocation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Basic
        m_Basic = asset.FindActionMap("Basic", throwIfNotFound: true);
        m_Basic_TouchAction = m_Basic.FindAction("TouchAction", throwIfNotFound: true);
        m_Basic_TouchLocation = m_Basic.FindAction("TouchLocation", throwIfNotFound: true);
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

    // Basic
    private readonly InputActionMap m_Basic;
    private IBasicActions m_BasicActionsCallbackInterface;
    private readonly InputAction m_Basic_TouchAction;
    private readonly InputAction m_Basic_TouchLocation;
    public struct BasicActions
    {
        private @InputTactile m_Wrapper;
        public BasicActions(@InputTactile wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchAction => m_Wrapper.m_Basic_TouchAction;
        public InputAction @TouchLocation => m_Wrapper.m_Basic_TouchLocation;
        public InputActionMap Get() { return m_Wrapper.m_Basic; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BasicActions set) { return set.Get(); }
        public void SetCallbacks(IBasicActions instance)
        {
            if (m_Wrapper.m_BasicActionsCallbackInterface != null)
            {
                @TouchAction.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnTouchAction;
                @TouchAction.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnTouchAction;
                @TouchAction.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnTouchAction;
                @TouchLocation.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnTouchLocation;
                @TouchLocation.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnTouchLocation;
                @TouchLocation.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnTouchLocation;
            }
            m_Wrapper.m_BasicActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchAction.started += instance.OnTouchAction;
                @TouchAction.performed += instance.OnTouchAction;
                @TouchAction.canceled += instance.OnTouchAction;
                @TouchLocation.started += instance.OnTouchLocation;
                @TouchLocation.performed += instance.OnTouchLocation;
                @TouchLocation.canceled += instance.OnTouchLocation;
            }
        }
    }
    public BasicActions @Basic => new BasicActions(this);
    public interface IBasicActions
    {
        void OnTouchAction(InputAction.CallbackContext context);
        void OnTouchLocation(InputAction.CallbackContext context);
    }
}

/*
using System;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.Touch;

[DefaultExecutionOrder((-1))]
public class InputManager : Singleton<InputManager>
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    
    TouchControls touchControls;

    void Awake()
    {
        touchControls = new TouchControls();
    }

    void OnEnable()
    {
        touchControls.Enable();
        TouchSimulation.Enable();
        
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    void OnDisable()
    {
        touchControls.Disable();
        TouchSimulation.Disable();
        
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.started += ctx => EndTouch(ctx);

        
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touching" + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if (OnStartTouch != null)
        {
            OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }
    void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Not Touching");
        if (OnEndTouch != null)
        {
            OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }

    } 
    void FingerDown(Finger finger)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(finger.);
        }
    }
    
}
*/

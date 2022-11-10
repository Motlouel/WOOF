using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("KH/PlayerInteractions")]
[DefaultExecutionOrder(-10)]
public class PlayerInteractions : MonoBehaviour
{

    private static PlayerInteractions _Instance;
    public static PlayerInteractions Instance
    {
        get
        {
            if(_Instance == null)
            {

            }
            return _Instance;
        }
    }

    private InputTactile inputManager
    {
        get
        {
            if(_inputManager == null)
            {
                _inputManager = new InputTactile();
            }
            return _inputManager;
        }
        set
        {
            _inputManager = value;
        }
    }
    private InputTactile _inputManager;

    [SerializeField] private Camera cam;

    public event Action<Vector2, float> ev_StartTouch; //Where tuch, time touch
    public event Action<Vector2, float> ev_EndTouch;

    public event Action<RaycastHit> ev_RayTouching;

    public Vector3 whereMousePoint;

    public bool inTouched = false;

    private Ray lastRay;

    private void Awake()
    {
        if (Instance == null || !Instance.gameObject.activeInHierarchy)
        {
            _Instance = this;
            print("Instance set");
        }
        else
        {
            Destroy(this);
        }
        _inputManager = new InputTactile();
        SetCamera();
    }

    public void SetCamera(Camera newCam = null)
    {
        cam = newCam ? newCam : FindObjectOfType<Camera>();
        // cam = Camera.main;

    }

    public void SetMainCamera()
    {
        cam = Camera.main;
    }

    public Camera GetCamera()
    {
        return cam;
    }

    private void OnEnable()
    {
        _Instance = this;
        inputManager.Enable();
        //TouchSimulation.Enable();
        inputManager.Basic.TouchAction.started += ctx => CheckWhatClicked(ctx);
        inputManager.Basic.TouchAction.canceled += ctx => EndClick(ctx);
    }

    private void CheckWhatClicked(InputAction.CallbackContext ctx)
    {
        inTouched = true;
        ev_StartTouch?.Invoke(ScreenToWorld(cam, inputManager.Basic.TouchLocation.ReadValue<Vector2>()), (float)ctx.startTime);

        MakeLineTrace();
    }

    private void EndClick(InputAction.CallbackContext ctx)
    {
        inTouched = false;
        ev_EndTouch?.Invoke(ScreenToWorld(cam, inputManager.Basic.TouchLocation.ReadValue<Vector2>()), (float)ctx.time);

    }

    private void MakeLineTrace()
    {
        Ray ray = cam.ScreenPointToRay(inputManager.Basic.TouchLocation.ReadValue<Vector2>());
        lastRay = ray;
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.25f, out hit))
        {
            //Check si c'est du sol 
            ev_RayTouching?.Invoke(hit);

        }


    }

    private void Update()
    {
        //Debug.Log(inputManager.Basic.TouchLocation.ReadValue<Vector2>());
        Debug.DrawRay(lastRay.origin, lastRay.direction * 1000, Color.gray);
    }

    public Vector2 GetTouchPos()
    {
        return inputManager.Basic.TouchLocation.ReadValue<Vector2>();
    }

    public Vector3 WorldToScreen(Vector3 position)
    {
        return cam.WorldToScreenPoint(position);
    }

    public Vector3 ScreenToWorld(Vector3 position)
    {
        return cam.ScreenToWorldPoint(position);
    }

    public static Vector3 ScreenToWorld(Camera cam, Vector3 position)
    {
        position.z = cam.nearClipPlane;
        return cam.ScreenToWorldPoint(position);
    }

    public Vector2 PrimaryPosition()
    {
        return ScreenToWorld(cam, inputManager.Basic.TouchLocation.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        inputManager?.Disable();
        //TouchSimulation.Disable();
    }

}

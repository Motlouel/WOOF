using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinnable : MonoBehaviour
{

    private bool isDragging = false;
    private Vector2 touchPoint = Vector2.zero;
    private float angleOffset = 0f;
    private float angleVelocity = 0f;
    private float lastDragAngle = 0f;
    public float rotation = 0f;
    public float rotationMin = 0f;
    public float rotationMax = 360f * 10f;
    public float friction = 3.3f;
    public float maxSpinVelocity = 4f;
    public float maxMomentaryDragRotation = 6f;
    public Transform placeHolderMeter = null;
    public SubSceneManager subSceneManager = null;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        PlayerInteractions.Instance.ev_StartTouch += TouchStart;
        PlayerInteractions.Instance.ev_RayTouching += TouchMove;
        PlayerInteractions.Instance.ev_EndTouch += TouchEnd;
    }

    private void OnDisable()
    {
        PlayerInteractions.Instance.ev_StartTouch -= TouchStart;
        PlayerInteractions.Instance.ev_RayTouching += TouchMove;
        PlayerInteractions.Instance.ev_EndTouch -= TouchEnd;
    }

    public void TouchMove(RaycastHit hit)
    {
        
    }

    public void TouchStart(Vector2 arg0, float arg1)
    {
        isDragging = true;
        touchPoint = PlayerInteractions.Instance.GetTouchPos();
        Camera cam = Camera.main;
        Vector2 screenAnchor = cam.WorldToScreenPoint(transform.position);
        Vector2 p = touchPoint - screenAnchor;
        lastDragAngle = Vector2.SignedAngle(new Vector2(0f, 1f), p);
    }

    public void TouchEnd(Vector2 arg0, float arg1)
    {
        isDragging = false;
    }

    void Start()
    {
        
    }

    
    
    void Update()
    {

        if (isDragging)
        {
            var currTouchPoint = PlayerInteractions.Instance.GetTouchPos();
            touchPoint = currTouchPoint;
            Camera cam = Camera.main;
            Vector2 screenAnchor = cam.WorldToScreenPoint(transform.position);
            Vector2 p = touchPoint - screenAnchor;
            float dragAngle = Vector2.SignedAngle(new Vector2(0f, 1f), p);

            float rotationDiff = Mathf.DeltaAngle(lastDragAngle, dragAngle);
            float maxMomentaryAngleDiff = 45f;
            rotationDiff = Mathf.Clamp(rotationDiff, -maxMomentaryAngleDiff, maxMomentaryAngleDiff);

            lastDragAngle = dragAngle;

            float newRotation = rotation + rotationDiff;

            float diff = Mathf.DeltaAngle(rotation, newRotation);
            angleVelocity = Mathf.Lerp(angleVelocity, diff / Time.deltaTime, 1 - Mathf.Pow(2, -Time.deltaTime * 100.0f));
            // angleVelocity = Mathf.MoveTowards(angleVelocity, diff, 180f * Time.deltaTime);
            rotation = newRotation;
        }
        else
        {
            rotation += angleVelocity * Time.deltaTime;
            angleVelocity = Mathf.MoveTowards(angleVelocity, 0f, friction * 360f * Time.deltaTime);
        }

        if (rotation < rotationMin)
        {
            rotation = rotationMin;
            angleVelocity = 0f;
        }
        else if (rotation > rotationMax)
        {
            rotation = rotationMax;
            angleVelocity = 0f;
            subSceneManager?.GetComponent<SubSceneManager>().LeaveWithState(true);
        }

        angleVelocity = Mathf.Clamp(angleVelocity, -360f * maxSpinVelocity, 360f * maxSpinVelocity);
        transform.eulerAngles = new Vector3(0f, 0f, rotation);

        if (placeHolderMeter)
        {
            var scale = placeHolderMeter.transform.localScale;
            scale.y = (rotation - rotationMin) / (rotationMax - rotationMin);
            placeHolderMeter.transform.localScale = scale;
        }

    }

}
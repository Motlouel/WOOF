using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[AddComponentMenu("KH/CameraRig")]
public class CameraRig : MonoBehaviour
{
    [SerializeField] private GameObject cameraRig;

    [Tooltip("La curve permet de décider de comment la caméra va se comporter entre sa position de départ et d'arrivée")]
    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private float rotationStepDegrees = 90.0f;
    [SerializeField] private float rotationOffset = -45.0f;
    [SerializeField] private int rotationStepStart = 1;
    [SerializeField] private int rotationSteps = 2;
    [Tooltip("Animation duration in seconds of the camera turning animation")]
    [SerializeField] private float rotateDuration = 1.0f;
    [SerializeField] private bool invertStepRotation = true;

    private int rotSteps = 0;

    private bool inRotation = false;

    private void Awake()
    {
        rotSteps = rotationStepStart;
    }

    private void Start()
    {
        OnRotationComplete();
    }

    public int GetRotSteps()
    {
        return rotSteps;
    }

    public void ToggleCameraTurn()
    {
        if (rotSteps == 0)
        {
            TurnCameraRight();
        }
        else if (rotSteps == 1)
        {
            TurnCameraLeft();
        }
    }

    public void TurnCameraLeft()
    {
        if (inRotation) return;
        if (rotSteps <= 0) return;

        rotSteps--;

        LeanTween.rotateY(cameraRig, StepToRotation(rotSteps), rotateDuration).setEase(moveCurve).setOnComplete(OnRotationComplete);
        inRotation = true;

    }

    void OnRotationComplete()
    {
        var e = cameraRig.transform.eulerAngles;
        e.y = StepToRotation(rotSteps);
        cameraRig.transform.eulerAngles = e;
        inRotation = false;
    }

    public void TurnCameraRight()
    {
        if (inRotation) return;
        if (rotSteps >= rotationSteps - 1) return;

        rotSteps++;

        LeanTween.rotateY(cameraRig, StepToRotation(rotSteps), rotateDuration).setEase(moveCurve).setOnComplete(OnRotationComplete);
        inRotation = true;

    }

    public float StepToRotation(int step)
    {
        return rotationOffset + step * rotationStepDegrees * (invertStepRotation ? -1 : 1);
    }
    public bool CanTurnLeft()
    {
        return rotSteps > 0;
    }
    public bool CanTurnRight()
    {
        return rotSteps < rotationSteps - 1;
    }

    private void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            rotationStepStart = Mathf.Clamp(rotationStepStart, 0, rotationSteps - 1);
            rotSteps = rotationStepStart;
            OnRotationComplete();
        }
    }

    public void ResetCamera()
    {
        rotSteps = rotationStepStart;
        OnRotationComplete();
        LeanTween.cancel(gameObject);
    }

}

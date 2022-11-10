using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LeverHandle : MonoBehaviour
{

    public List<float> positions = new List<float> { -60.0f, 60.0f };
    public TouchAnchor touchAnchor;
    bool isDragging = false;
    Vector2 touchOffset;
    float angleVelocity = 0.0f;
    float lastDragAngle = 0.0f;
    public float maxSpinVelocity = 135.0f;
    public float friction = 4f;
    float _rotata = 0.0f;
    float rotation
    {
        set { _rotata = value; }
        get { return _rotata; }
    }
    float graceTimeCounter = 0f;
    SubSceneManager subSceneManager = null;
    bool interacted = false;

    void Awake()
    {
        rotation = positions[0];
    }

    public void OnSubSceneInit(SubSceneManager man)
    {
        subSceneManager = man;
        if (man.state)
        {
            rotation = positions[positions.Count - 1];
        }
        else
        {
            rotation = positions[0];
        }
        Debug.Log("LeverHandle initiaialsd");
        Debug.Log(man.state);
        Debug.Log(rotation);
    }

    private void OnEnable()
    {
        if (!Application.isPlaying) return;
        PlayerInteractions.Instance.ev_StartTouch += TouchStart;
        PlayerInteractions.Instance.ev_RayTouching += TouchMove;
        PlayerInteractions.Instance.ev_EndTouch += TouchEnd;
    }

    private void OnDisable()
    {
        if (!Application.isPlaying) return;
        PlayerInteractions.Instance.ev_StartTouch -= TouchStart;
        PlayerInteractions.Instance.ev_RayTouching += TouchMove;
        PlayerInteractions.Instance.ev_EndTouch -= TouchEnd;
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            if (positions.Count > 0)
                transform.localEulerAngles = new Vector3(0, 0, positions[0]);
            return;
        }

        if (isDragging)
        {
            Vector2 touchPos = PlayerInteractions.Instance.GetTouchPos();
            Camera cam = Camera.main;

            Vector3 projectedTouchPos = cam.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Mathf.Abs(cam.transform.position.z - transform.position.z)));
            Vector2 localTouchPos = projectedTouchPos - transform.position;
            float dragAngle = Vector2.SignedAngle(new Vector2(0f, 1f), localTouchPos);

            float rotationDiff = Mathf.DeltaAngle(lastDragAngle, dragAngle);
            float maxMomentaryAngleDiff = 10f;
            rotationDiff = Mathf.Clamp(rotationDiff, -maxMomentaryAngleDiff, maxMomentaryAngleDiff);
            lastDragAngle = dragAngle;

            float newRotation = rotation + rotationDiff;
            float diff = Mathf.DeltaAngle(rotation, newRotation);
            angleVelocity = Mathf.Lerp(angleVelocity, diff / Time.deltaTime, 1 - Mathf.Pow(2, -Time.deltaTime * 100.0f));
            rotation = newRotation;
        }
        else
        {
            angleVelocity = Mathf.MoveTowards(angleVelocity, 0f, friction * 360f * Time.deltaTime);
            float dist;
            int posIndex;
            float nearest = ClosestPosition(rotation, out dist, out posIndex);

            float diff = nearest - rotation;
            if (dist > 1.5f)
            {
                angleVelocity += (1 / (1 + Mathf.Pow(diff / 360.0f, 2))) * Time.deltaTime * 600.0f * (diff > 0 ? 1.0f : -1.0f);
            }

            rotation += angleVelocity * Time.deltaTime;

            if (dist < 4.0f)
            {
                graceTimeCounter += Time.deltaTime;
                if (graceTimeCounter > 0.5f && interacted)
                {
                    Debug.Log("Lever leave");
                    subSceneManager?.LeaveWithState(posIndex > 0);
                }
            }
            else
            {
                graceTimeCounter = 0.0f;
            }
        }

        float p0 = positions[0];
        float pN = positions[positions.Count - 1];
        float posMax = Mathf.Max(p0, pN);
        float posMin = Mathf.Min(p0, pN);

        if (rotation > posMax)
        {
            angleVelocity = 0f;
            rotation = posMax;
        }
        else if (rotation < posMin)
        {
            angleVelocity = 0f;
            rotation = posMin;
        }

        angleVelocity = Mathf.Clamp(angleVelocity, -360f * maxSpinVelocity, 360f * maxSpinVelocity);
        transform.eulerAngles = new Vector3(0f, 0f, rotation);
    }

    public float ClosestPosition(float pos, out float outDiff, out int index)
    {
        int _i = 0;
        float foundPos = positions[_i];
        float diff = Mathf.Abs(pos - foundPos);
        for (int i = 0; i < positions.Count; ++i)
        {
            float p = positions[i];
            float _diff = Mathf.Abs(p - pos);
            if (_diff < diff)
            {
                diff = _diff;
                foundPos = p;
                _i = i;
            }
        }
        outDiff = diff;
        index = _i;
        return foundPos;
    }

    public float ClosestPosition(float pos)
    {
        float diff;
        int i;
        return ClosestPosition(pos, out diff, out i);
    }

    public void TouchStart(Vector2 vec, float f)
    {
        if (touchAnchor)
        {
            Camera cam = Camera.main;
            Vector2 touchPos = PlayerInteractions.Instance.GetTouchPos();
            if (touchAnchor.IntersectsScreenPoint(Camera.main, touchPos))
            {
                isDragging = true;
                interacted = true;
                Vector3 projectedTouchPos = cam.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Mathf.Abs(cam.transform.position.z - transform.position.z)));
                Vector2 localTouchPos = projectedTouchPos - transform.position;
                lastDragAngle = Vector2.SignedAngle(new Vector2(0f, 1f), localTouchPos);
            }
        }
    }

    public void TouchMove(RaycastHit hit)
    {

    }

    public void TouchEnd(Vector2 vec, float f)
    {
        isDragging = false;
    }

}

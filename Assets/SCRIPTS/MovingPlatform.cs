using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KH/MovingPlatform")]
public class MovingPlatform : MonoBehaviour
{

    public Vector3 moveOffset = Vector3.up;
    public bool paused = true;
    public bool startAtOffset = true;
    public float movementSpeed = 1.5f;
    public float goalDir = 0.0f;
    private Vector3 startPos = Vector3.zero;


    void Start()
    {
        startPos = transform.position;
        if (startAtOffset)
        {
            goalDir = 1.0f;
            transform.position = startPos + moveOffset;
        }
    }

    void Update()
    {
        if (!paused)
        {
            Vector3 target = CalcPosition(goalDir);
            Vector3 toTargetDiff = (target - transform.position);
            float dist = toTargetDiff.magnitude;
            if (dist > 0.0001f)
            {
                transform.position += toTargetDiff.normalized * Mathf.Min(movementSpeed * Time.deltaTime, dist);
            }
            else
            {
                transform.position = target;
            }
        }
    }

    public Vector3 CalcPosition(float goal)
    {
        return Vector3.Lerp(startPos, startPos + moveOffset, goalDir);
    }

    public void SetMoveTo(float pos)
    {
        paused = false;
        goalDir = pos;
    }

    public void SetPos(float pos)
    {
        paused = true;
        goalDir = pos;
        transform.position = CalcPosition(pos);
    }

    public void ToggleMoveTo()
    {
        SetMoveTo(goalDir > 0.5f ? 0f : 1f);
    }

}

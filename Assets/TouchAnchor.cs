using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAnchor : MonoBehaviour
{
    public float radius = 0.5f;
    private Collider[] colList;

    private void Awake()
    {
        colList = GetComponentsInChildren<Collider>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public Vector2 ToScreenCoords(Camera cam)
    {
        return cam.WorldToScreenPoint(transform.position);
    }

    public bool IntersectsScreenPoint(Camera cam, Vector2 screenPoint, out RaycastHit hit)
    {
        Ray ray = cam.ScreenPointToRay(screenPoint);
        RaycastHit _hit;
        foreach (Collider col in colList)
        {
            if (col.Raycast(ray, out _hit, 10000.0f))
            {
                hit = _hit;
                return true;
            }
        }
        hit = new RaycastHit();
        return false;
    }

    public bool IntersectsScreenPoint(Camera cam, Vector2 screenPoint)
    {
        RaycastHit hit;
        return IntersectsScreenPoint(cam, screenPoint, out hit);
    }
}

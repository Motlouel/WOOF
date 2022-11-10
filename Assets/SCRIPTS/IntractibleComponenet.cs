using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("KH/IntractibleComponent")]
public class IntractibleComponenet : MonoBehaviour
{

    private Vector3 offset;

    private float zCoord;

    private bool onTouch = false;


    private void Awake()
    {
    }

    private void OnEnable()
    {
        PlayerInteractions.Instance.ev_StartTouch += StarTouching;
        PlayerInteractions.Instance.ev_RayTouching += IsDraggingSelf;
    }

    private void OnDisable()
    {
        PlayerInteractions.Instance.ev_StartTouch -= StarTouching;
        PlayerInteractions.Instance.ev_RayTouching -= IsDraggingSelf;
    }

    private void IsDraggingSelf(RaycastHit obj)
    {
        //print(obj.collider);
        if (obj.collider.gameObject == this.gameObject)
        {
            onTouch = true;
        }
        else
        {
            onTouch = false;
        }
    }

    private void StarTouching(Vector2 arg1, float arg2)
    {
        onTouch = false;
        print("Stat touch");

        zCoord = PlayerInteractions.Instance.WorldToScreen(transform.position).z;

        offset = gameObject.transform.position - GetTouchWorldPos();
    }

    private Vector3 GetTouchWorldPos()
    {
        Vector3 touchPoint = PlayerInteractions.Instance.GetTouchPos();
        touchPoint.z = zCoord;

        return PlayerInteractions.Instance.ScreenToWorld(touchPoint);
    }

   
    private void Update()
    {
        if (onTouch)
        {
            if (PlayerInteractions.Instance.inTouched)
            {
                transform.position = new Vector3(transform.position.x, GetTouchWorldPos().y + offset.y, transform.position.z);
            }
        }
    }

  
}

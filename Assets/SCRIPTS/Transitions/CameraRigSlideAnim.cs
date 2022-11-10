using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraRigSlideAnim : MonoBehaviour
{

    public UnityEvent onFinishIn = new UnityEvent();
    public UnityEvent onFinishOut = new UnityEvent();

    public float yyyy;
    public bool yCached = false;
    public void Start()
    {
        var rig = FindObjectOfType<CameraRig>();
        if (rig)
        {
            yyyy = rig.gameObject.transform.localPosition.y;
        }
    }

    public void SlideIn()
    {
        var rig = FindObjectOfType<CameraRig>();
        if (rig)
        {
            var lPos = rig.gameObject.transform.localPosition;
            rig.gameObject.transform.localPosition = lPos + new Vector3(0.0f, 16.0f, 0.0f);
            LeanTween.moveLocalY(rig.gameObject, yyyy, 1.5f).setEaseOutQuint().setOnComplete(onCompleteIn);
        }
    }

    public void SlideOut()
    {
        var rig = FindObjectOfType<CameraRig>();
        if (rig)
        {
            var lPos = rig.gameObject.transform.localPosition;
            LeanTween.moveLocalY(rig.gameObject, lPos.y + 16.0f, 1.5f).setEaseInQuint().setOnComplete(onCompleteOut);
        }
    }

    void onCompleteIn()
    {
        onFinishIn.Invoke();
    }

    void onCompleteOut()
    {
        onFinishOut.Invoke();
    }
}

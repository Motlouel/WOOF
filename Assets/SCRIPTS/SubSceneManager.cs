using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KH/SubSceneManager")]
public class SubSceneManager : MonoBehaviour
{

    public UnityEvent onAfterAdd;
    public UnityEvent onBeforeRemove;
    public UnityEvent<bool> onLeaveRequest;

    public bool state = false;

    public void InvokeBeforeRemove()
    {
        onBeforeRemove.Invoke();
    }

    public void InvokeAfterAdded()
    {
        onAfterAdd.Invoke();
        BroadcastMessage("OnSubSceneInit", this);
    }

    public void LeaveWithState(bool newState)
    {
        state = newState;
        InvokeLeave();
    }

    public void InvokeLeave()
    {
        onLeaveRequest.Invoke(state);
    }

}
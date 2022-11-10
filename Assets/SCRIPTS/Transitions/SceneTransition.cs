using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneTransition : MonoBehaviour
{

    public UnityEvent onPlayLeave = new UnityEvent();

    [HideInInspector]
    public UnityEvent onPlayLeaveFinish = new UnityEvent();

    public UnityEvent onPlayEnter = new UnityEvent();

    [HideInInspector]
    public UnityEvent onPlayEnterFinish = new UnityEvent();

    public bool enterFinished { get; private set; } = false;
    public bool leaveFinished { get; private set; } = false;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    /// <summary>
    /// End of transition, entering next scene.
    /// </summary>
    public void PlayEnter()
    {
        onPlayEnter.Invoke();
    }

    /// <summary>
    /// Start of transition, leaving current scene.
    /// </summary>
    public void PlayLeave()
    {
        onPlayLeave.Invoke();
    }

    public void TriggerPlayEnterFinished()
    {
        enterFinished = true;
        onPlayEnterFinish.Invoke();
    }

    public void TriggerPlayLeaveFinished()
    {
        leaveFinished = true;
        onPlayLeaveFinish.Invoke();
    }
}

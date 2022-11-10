using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseStateReceiver : MonoBehaviour
{

    public enum PauseStateActivity
    {
        IGNORE = 0,
        DISABLE_WHEN_PAUSED = 1,
        ENABLE_WHEN_PAUSED = 2,
    }

    [Serializable]
    public class PauseTargetEntry
    {
        public GameObject target;
        public PauseStateActivity pauseStateActivity = PauseStateActivity.IGNORE;
    }

    public List<PauseTargetEntry> targets;


    private void OnEnable()
    {
        GameStateManager.stateChange.AddListener(CheckPause);
    }

    private void OnDisable()
    {
        GameStateManager.stateChange.RemoveListener(CheckPause);
    }

    private void Start()
    {
        CheckPause();
    }

    void CheckPause()
    {
        bool paused = GameStateManager.isPaused;
        foreach (var target in targets)
        {
            switch (target.pauseStateActivity)
            {
                case PauseStateActivity.DISABLE_WHEN_PAUSED:
                    target.target.SetActive(!paused);
                    break;
                case PauseStateActivity.ENABLE_WHEN_PAUSED:
                    target.target.SetActive(paused);
                    break;
            }
        }
    }
}

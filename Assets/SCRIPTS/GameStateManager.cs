using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KH/Managers/GameStateManager")]
public class GameStateManager : MonoBehaviour
{

    // Paused
    // Cutscene

    public GameState gameState { get; private set; } = null;

    static private GameStateManager _instance;
    public static bool isPaused { get; private set; } = false;

    /// <summary>
    /// On pause/unpause
    /// </summary>
    public static UnityEvent pauseStateChange = new UnityEvent();

    /// <summary>
    /// On GameState change
    /// </summary>
    public static UnityEvent gameStateChange = new UnityEvent();

    /// <summary>
    /// On both pause or GameState change
    /// </summary>
    public static UnityEvent stateChange = new UnityEvent();

    static public GameStateManager instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance.gameObject.activeInHierarchy ? _instance : null;
            }
            return null;
        }
    }

    private void Awake()
    {
        if (_instance == null || !_instance.gameObject.activeInHierarchy)
        {
            _instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        gameState?.Update();
    }

    public void Pause()
    {
        if (isPaused) return;
        isPaused = true;
        Time.timeScale = 0.0f;
        pauseStateChange.Invoke();
        stateChange.Invoke();
    }

    public void Unpause()
    {
        if (!isPaused) return;
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseStateChange.Invoke();
        stateChange.Invoke();
    }

    public void SetPause(bool newPause)
    {
        if (newPause)
            Pause();
        else
            Unpause();
    }

    public void TogglePause()
    {
        SetPause(!isPaused);
    }

    public void SetState(GameState newState)
    {
        gameState?.OnLeave();
        gameState = newState;
        gameState.OnEnter();
        gameStateChange.Invoke();
        stateChange.Invoke();
    }

    public void ResetState()
    {
        gameState?.OnLeave();
        gameState = null;
        gameStateChange.Invoke();
        stateChange.Invoke();
    }

}

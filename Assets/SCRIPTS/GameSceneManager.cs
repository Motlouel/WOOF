using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("KH/Managers/GameSceneManager")]
public class GameSceneManager : MonoBehaviour
{
    public enum SceneTransitionType
    {
        NONE = -1,
        CURTAIN = 0,
        SLIDE = 1,
    };

    public enum LoadState
    {
        /// <summary>
        /// Doing nothing.
        /// </summary>
        IDLE = 0,
        /// <summary>
        /// Delaying transition.
        /// </summary>
        DELAY = 1,
        /// <summary>
        /// Playing transition animation.
        /// </summary>
        ANIMATION = 2,
        /// <summary>
        /// Unloading / loading a scene.
        /// </summary>
        LOADING_ASYNC = 3,
    };

    [HideInInspector] public string currentScene;
    [HideInInspector] public string nextScene;
    [HideInInspector] public SceneTransitionType nextTransition;
    public SceneLoader initialSceneLoad;
    public LoadState state
    {
        get; private set;
    } = LoadState.IDLE;
    private IEnumerator delayCoroutine = null;
    private AsyncOperation loadOperation = null;
    private SceneTransition transitionObject = null;

    [Header("Transition Prefabs")]
    public SceneTransition transitionSlide = null;
    //public 

    private void Awake()
    {
    }

    void Start()
    {
        if (currentScene == null || currentScene == "")
        {
            bool sceneAlreadyLoaded = false;
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.path != gameObject.scene.path)
                {
                    sceneAlreadyLoaded = true;
                    currentScene = scene.path;
                }
            }
            if (!sceneAlreadyLoaded)
            {
                initialSceneLoad.InvokeSceneLoad();
            }
        }
        Application.targetFrameRate = 60; // TODO: Move somewhere else
    }

    private void OnDisable()
    {
        CancelTransition();
    }

    public bool IsPlayingTransition()
    {
        return state == LoadState.ANIMATION;
    }

    public bool IsSwitchingScenes()
    {
        return state == LoadState.LOADING_ASYNC || state == LoadState.DELAY || state == LoadState.ANIMATION;
    }

    public bool SwitchScenes(string scene, SceneTransitionType transition = SceneTransitionType.CURTAIN, float delay = 0)
    {
        if (state == LoadState.LOADING_ASYNC) return false;
        nextScene = scene;
        nextTransition = transition;
        if (delay > 0.0f)
        {
            delayCoroutine = DelayTransition(delay);
            // TODO: Replace Coroutine approach. Respect pause state and timescale.
            StartCoroutine(delayCoroutine);
        }
        else
        {
            DoAnimationStart();
        }
        return true;
    }

    IEnumerator DelayTransition(float delay)
    {
        state = LoadState.DELAY;
        yield return new WaitForSeconds(delay);
        DoAnimationStart();
    }

    void DoAnimationStart()
    {
        state = LoadState.ANIMATION;
        switch (nextTransition)
        {
            case SceneTransitionType.SLIDE:
                transitionObject = Instantiate(transitionSlide);
                transitionObject.onPlayLeaveFinish.AddListener(DoSceneSwitchUnload);
                transitionObject.PlayLeave();
                break;
            case SceneTransitionType.CURTAIN:
            // TODO: Implement animation
            case SceneTransitionType.NONE:
            default:
                DoSceneSwitchUnload();
                break;
        }
    }

    void DoAnimationEnd()
    {
        // TODO: Finish animation
        transitionObject.onPlayEnterFinish.AddListener(DoAfterAnimationEnd);
        transitionObject.PlayEnter();
    }

    void DoAfterAnimationEnd()
    {
        if (transitionObject != null)
        {
            Destroy(transitionObject);
        }
    }

    void DoSceneSwitchUnload()
    {
        state = LoadState.LOADING_ASYNC;

        if (currentScene != null && currentScene != "")
        {
            AsyncOperation unloadOp = UnloadScene(currentScene);
            unloadOp.completed += (op) =>
            {
                DoSceneSwitchLoad();
            };
        }
        else
        {
            DoSceneSwitchLoad();
        }
    }

    void DoSceneSwitchLoad()
    {
        AsyncOperation loadOp = LoadScene(nextScene);
        loadOp.completed += (op) =>
        {
            currentScene = nextScene;
            nextScene = null;
            state = LoadState.IDLE;
            var camRig = FindObjectOfType<CameraRig>();
            camRig?.ResetCamera();
            DoAnimationEnd();
        };
    }

    private AsyncOperation UnloadScene(string scene)
    {
        Debug.Log("Unloading scene");
        var operation = SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        operation.completed += (AsyncOperation result) =>
        {
            Debug.Log("Scene unloaded");
        };
        return operation;
    }

    private AsyncOperation LoadScene(string scene)
    {
        Debug.Log("Loading scene");
        var operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        //operation.allowSceneActivation = false;
        operation.completed += (AsyncOperation result) =>
        {
            Debug.Log("Scene loaded");
        };
        return operation;
    }

    /// <summary>
    /// Cancels a running scene transition. Can only cancel scene transitions during delay and animation states.
    /// </summary>
    /// <returns>Was a transition cancelled</returns>
    public bool CancelTransition()
    {
        if (state != LoadState.DELAY && state != LoadState.ANIMATION) return false;
        if (delayCoroutine != null)
        {
            StopCoroutine(delayCoroutine);
            delayCoroutine = null;
        }
        // TODO: cancel transition animation
        state = LoadState.IDLE;
        return true;
    }

}

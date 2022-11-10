using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//[Serializable]
//public enum GPEType
//{
//    Guinde,
//    Lever,
//};

[AddComponentMenu("KH/SubSceneLoader")]
public class SubSceneLoader : MonoBehaviour
{
    [SerializeField] SubSceneManager type;
    [SerializeField] private Camera oldCam;
    [SerializeField] public bool completableOnce = true;
    [SerializeField][HideInInspector] public bool wasCompleted = false;
    [SerializeField] UnityEvent<bool> onLeave;
    [SerializeField] UnityEvent onComplete;
    [SerializeField] UnityEvent onLeave0;
    [SerializeField] UnityEvent onLeave1;
    [SerializeField][HideInInspector] SubSceneManager loadedSub;
    [SerializeField] public bool state = false;


    public void UnloadGPEScene()
    {
        if (loadedSub == null) return;
        oldCam.gameObject.SetActive(true);
        loadedSub.InvokeBeforeRemove();
        Destroy(loadedSub.gameObject);
        loadedSub = null;
        PlayerInteractions.Instance.SetCamera(oldCam);
    }

    public void LoadGPEScene()
    {
        LoadGPEScene(false);
    }

    public void LoadGPEScene(bool force)
    {
        if (!force && wasCompleted && completableOnce) return;
        oldCam = PlayerInteractions.Instance.GetCamera();
        loadedSub = Instantiate(type);
        loadedSub.onLeaveRequest.AddListener((bool leaveState) =>
        {
            state = leaveState;
            UnloadGPEScene();
            if (isActiveAndEnabled)
            {
                onLeave.Invoke(state);
                if (state)
                {
                    wasCompleted = true;
                    onLeave1.Invoke();
                    onComplete.Invoke();
                }
                else
                {
                    onLeave0.Invoke();
                }
            }
        });
        loadedSub.gameObject.transform.position = new Vector3(1000f, 100f, 0f);
        loadedSub.gameObject.GetComponent<SubSceneManager>().state = state;
        loadedSub.gameObject.SetActive(true);
        oldCam.gameObject.SetActive(false);
        PlayerInteractions.Instance.SetMainCamera();
        loadedSub.InvokeAfterAdded();



        //switch (type)
        //{
        //    case GPEType.Guinde:
        //        oldCam.gameObject.SetActive(false);
        //        // SceneManager.LoadScene("Sc_GuindeForTests");
        //        //SceneManager.LoadScene("Sc_Guinde",LoadSceneMode.Additive);
        //        AsyncOperation op = SceneManager.LoadSceneAsync("Sc_Guinde", LoadSceneMode.Additive);
        //        op.completed += (AsyncOperation result) =>
        //        {
        //            Debug.Log("Completed");
        //        };


        //        break;
        //}
    }
}

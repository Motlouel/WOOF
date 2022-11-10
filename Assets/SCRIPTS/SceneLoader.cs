using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AddComponentMenu("KH/SceneLoader")]
public class SceneLoader : MonoBehaviour
{
    [HideInInspector] public string scene;
    public GameSceneManager.SceneTransitionType transition = GameSceneManager.SceneTransitionType.NONE;

    [Range(0.0f, 60.0f)]
    public float delay = 0;

    public void InvokeSceneLoad()
    {
        var obj = FindObjectOfType<GameSceneManager>();
        if (obj == null)
        {
            Debug.LogWarning("Couldn't find GameSceneManager");
            return;
        }
        obj.GetComponent<GameSceneManager>().SwitchScenes(scene, transition, delay);
    }

    public void SetDelay(float delay)
    {
        this.delay = delay;
    }
}

#if UNITY_EDITOR

// https://docs.unity3d.com/ScriptReference/SceneAsset.html

[CustomEditor(typeof(SceneLoader), true)]
public class SceneLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var obj = target as SceneLoader;
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(obj.scene);

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        var newScene = EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

        if (EditorGUI.EndChangeCheck())
        {
            var newPath = AssetDatabase.GetAssetPath(newScene);
            var scenePathProperty = serializedObject.FindProperty("scene");
            scenePathProperty.stringValue = newPath;
        }
        bool found = false;
        foreach(var x in EditorBuildSettings.scenes)
        {
            if(x.enabled && x.path == serializedObject.FindProperty("scene").stringValue)
            {
                found = true;
                break;
            }
        }
        if (!found) {
            EditorGUILayout.HelpBox("Scene must be added to BuildSettings (File -> Build Settings) in order to be loadable.", MessageType.Warning);
        }

        serializedObject.ApplyModifiedProperties();

        DrawDefaultInspector();
    }
}
#endif
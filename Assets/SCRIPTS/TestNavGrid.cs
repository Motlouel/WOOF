using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

using UnityEngine.UIElements;


[Serializable]
public class NavGridVoxelData
{

}

[ExecuteAlways]
[DefaultExecutionOrder(-102)]
[AddComponentMenu("KH/TestNavGrid", 30)]
public class TestNavGrid : MonoBehaviour
{

    public Vector3Int gridDimension = Vector3Int.one;
    public Vector3 gridCellSize = Vector3.one;
    public Vector3 gridOffset = Vector3.zero;
    [SerializeField] private bool gridInvalid = true;
    [SerializeField] private List<NavGridVoxelData> gridData = new List<NavGridVoxelData>();

    void Start()
    {

    }

    void Update()
    {
        if(VoxelCount() != gridData.Count)
            gridInvalid = true;
        if (gridInvalid)
        {
            SetupGrid();
        }
    }

    void SetupGrid()
    {
        int voxCount = VoxelCount();
        gridData = new List<NavGridVoxelData>();
        for (int i = 0; i < voxCount; ++i)
        {
            gridData.Add(new NavGridVoxelData());
        }
    }

    int VoxelCount()
    {
        return gridDimension.x * gridDimension.y * gridDimension.z;
    }

    void GridInvalidated()
    {

    }

    public void Bake()
    {
        Debug.Log("BAKE CLICKED");
    }

    public void ClearBake()
    {
        Debug.Log("BAKE CLEAR CLICKED");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TestNavGrid))]
public class TestNavGridInspector : Editor
{

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement myInspector = new VisualElement();

        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/NavGridInspector.uxml");

        visualTree.CloneTree(myInspector);

        myInspector.Query<Button>("Bake").First().clicked += onClickBake;
        myInspector.Query<Button>("Clear").First().clicked += onClickBakeClear;

        return myInspector;
    }

    public void onClickBake()
    {
        //Debug.Log(this.target.Bake()
    }

    public void onClickBakeClear()
    {
        Debug.Log(this.target);
    }

}
#endif
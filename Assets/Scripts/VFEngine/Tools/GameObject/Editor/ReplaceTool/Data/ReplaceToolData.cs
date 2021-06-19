namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data { }
/*
using System;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using DataSO = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects.ReplaceToolDataSO;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;
using Controller = VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController;
using ModelView = VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolModelView;
*/
/*
//using static ReplaceToolState;
using static GC;

internal class ReplaceToolData : IDisposable
{
    /*internal UnityGameObject ReplacementPrefab => DataSO.ReplacementPrefab;

    internal UnityGameObject[] ObjectsToReplace
    {
        get => DataSO.ObjectsToReplace;
        set => DataSO.ObjectsToReplace = value;
    }*/

//internal SerializedObject SerializedData { get; set; }
//internal SerializedProperty ReplaceObjectField { get; set; }

// {
/*get => replaceObjectField;
set => replaceObjectField = value;*/
//}//=> replaceObjectField.Copy();
//private SerializedProperty replaceObjectField;// { get; set; }
//internal SerializedProperty ReplaceObjectFieldInstance { get; set; }
//internal DataSO DataSO { get; private set; }
/*
internal int[] GameObjectsInstances { get; set; }

//internal int ObjectsToReplaceIndex { get; set; }
//internal UnityGameObject GameObjectInstance { get; set; }
//internal UnityGameObject ReplacementPrefabInstance { get; set; }
//internal bool ReplacementPrefabInstanceNotNull { get; private set; }
internal Controller Window { get; set; }
internal Vector2? SelectObjectScrollPosition { get; set; }

//internal ReplaceToolState State { get; set; }
internal string SelectGameObjectsLabel { get; private set; }
internal bool HasReplaceObjectField { get; set; }

internal ReplaceToolData()
{
    Initialize();
}

private void Initialize()
{
    //DataSO = dataSO;
    //SerializedData = new SerializedObject(DataSO);
    //ReplaceObjectField = null; //SerializedData.FindProperty(Text.ReplacementPrefab);
    HasReplaceObjectField = false;
    SelectObjectScrollPosition = null;
    GameObjectsInstances = null;
    //ObjectsToReplaceIndex = 0;
    //GameObjectInstance = null;
    //ReplacementPrefabInstance = null;
    //ReplacementPrefabInstanceNotNull = ReplacementPrefabInstance != null;
    Window = null;
    //State = None;
    //ObjectsToReplace = new UnityGameObject[0];
    SelectGameObjectsLabel = Text.SelectGameObjectsLabel;
}

public void Dispose()
{
    SuppressFinalize(this);
}

~ReplaceToolData()
{
    Dispose();
}
}
}
*/
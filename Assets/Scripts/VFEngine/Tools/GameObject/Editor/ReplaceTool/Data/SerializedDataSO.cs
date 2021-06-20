using System;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data
{
    internal class SerializedDataSO : ScriptableObject
    {
        [SerializeField] private UnityGameObject replacementPrefab;
        [SerializeField] private UnityGameObject[] objectsToReplace;
    }
}
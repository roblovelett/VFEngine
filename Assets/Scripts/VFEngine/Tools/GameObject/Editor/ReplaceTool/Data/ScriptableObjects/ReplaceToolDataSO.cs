using System;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects
{
    internal class ReplaceToolDataSO : ScriptableObject
    {
        internal UnityGameObject ReplacementPrefab { get; set; }
        internal UnityGameObject[] ObjectsToReplace { get; set; }
        /*internal void OnDestroy()
        {
            Object.DestroyImmediate(ReplacementPrefab);
            foreach (var gameObject in ObjectsToReplace) DestroyImmediate(gameObject);
        }*/
    }
}
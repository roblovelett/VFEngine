using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects
{
    internal class ReplaceToolDataSO : ScriptableObject
    {
        internal UnityGameObject ReplacementPrefab { get; set; }
        internal UnityGameObject[] ObjectsToReplace { get; set; }
    }
}
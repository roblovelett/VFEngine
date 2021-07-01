using JetBrains.Annotations;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.ReplaceTool.Editor.ScriptableObjects
{
    internal class ReplaceToolDataSO : ScriptableObject
    {
        [SerializeField] [UsedImplicitly] internal UnityGameObject replacementPrefab;
        [SerializeField] [UsedImplicitly] private UnityGameObject[] objectsToReplace;
    }
}
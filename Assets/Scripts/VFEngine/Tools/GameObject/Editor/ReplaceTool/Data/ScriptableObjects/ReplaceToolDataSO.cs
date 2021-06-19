using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects
{
    public class ReplaceToolDataSO : ScriptableObject
    {
        public UnityGameObject ReplacementPrefab { get; set; }
        public UnityGameObject[] ObjectsToReplace { get; set; }
    }
}
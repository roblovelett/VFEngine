using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects
{
    public class ReplaceToolDataSO : ScriptableObject
    {
        public UnityGameObject ReplacementPrefab;
        public UnityGameObject[] ObjectsToReplace;
    }
}
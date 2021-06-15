using UnityEditor;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data
{
    using static ScriptableObject;
    using static ReplaceToolText;

    internal class ReplaceToolData
    {
        internal int NewGameObjectsIndex { get; set; }
        internal int[] NewGameObjectInstances { get; set; }
        internal Vector2? SelectObjectScrollPosition { get; set; }
        internal Transform[] Selected { get; set; }
        internal EditorWindow Window { get; set; }
        internal UnityGameObject NewGameObject { get; set; }
        internal UnityGameObject NewGameObjectInstance { get; set; }
        internal ReplaceToolDataSO DataSO { get; set; }
        internal SerializedObject SerializedData { get; set; }
        internal SerializedProperty ReplaceObjectField { get; set; }

        internal ReplaceToolData()
        {
            Initialize();
        }

        private void Initialize()
        {
            NewGameObjectsIndex = 0;
            NewGameObjectInstances = null;
            SelectObjectScrollPosition = new Vector2();
            Selected = null;
            Window = null;
            NewGameObject = null;
            NewGameObjectInstance = null;
            DataSO = CreateInstance<ReplaceToolDataSO>();
            SerializedData = new SerializedObject(DataSO);
            ReplaceObjectField = SerializedData.FindProperty(Prefab);
        }
    }
}
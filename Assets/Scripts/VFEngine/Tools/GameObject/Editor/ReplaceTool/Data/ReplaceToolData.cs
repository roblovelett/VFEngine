using System;
using UnityEditor;
using UnityEngine;
using static VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolState;
using UnityGameObject = UnityEngine.GameObject;
using DataSO = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects.ReplaceToolDataSO;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;
using Controller = VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController;
using ModelView = VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolModelView;
using Object = UnityEngine.Object;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data
{
    using static GC;
    using static Object;

    internal class ReplaceToolData : IDisposable
    {
        internal UnityGameObject ReplacementPrefab => DataSO.ReplacementPrefab;

        internal UnityGameObject[] ObjectsToReplace
        {
            get => DataSO.ObjectsToReplace;
            set => DataSO.ObjectsToReplace = value;
        }

        internal SerializedObject SerializedData { get; set; }
        internal SerializedProperty ReplaceObjectField { get; set; }
        internal SerializedProperty ReplaceObjectFieldInstance { get; set; }
        internal DataSO DataSO { get; private set; }
        internal int[] GameObjectsInstances { get; set; }
        internal int ObjectsToReplaceIndex { get; set; }
        internal UnityGameObject GameObjectInstance { get; set; }
        internal UnityGameObject ReplacementPrefabInstance { get; set; }
        internal bool ReplacementPrefabInstanceNotNull { get; private set; }
        internal Controller Window { get; set; }
        internal Vector2? SelectObjectScrollPosition { get; set; }
        internal ReplaceToolState State { get; set; }

        internal ReplaceToolData(DataSO dataSO)
        {
            Initialize(dataSO);
        }

        private void Initialize(DataSO dataSO)
        {
            DataSO = dataSO;
            SerializedData = new SerializedObject(DataSO);
            ReplaceObjectFieldInstance = SerializedData.FindProperty(Text.ReplacementPrefab);
            ReplaceObjectField = ReplaceObjectFieldInstance;
            SelectObjectScrollPosition = null;
            GameObjectsInstances = null;
            ObjectsToReplaceIndex = 0;
            GameObjectInstance = null;
            ReplacementPrefabInstance = null;
            ReplacementPrefabInstanceNotNull = ReplacementPrefabInstance != null;
            Window = null;
            State = None;
            ObjectsToReplace = new UnityGameObject[0];
        }

        public void Dispose()
        {
            Dispose(true);
            SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (!dispose) return;
            DestroyImmediate(DataSO);
            DestroyImmediate(Window);
        }

        ~ReplaceToolData()
        {
            Dispose(false);
        }
    }
}
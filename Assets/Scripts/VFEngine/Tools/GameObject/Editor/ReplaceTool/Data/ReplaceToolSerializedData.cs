using System;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects;
using Object = UnityEngine.Object;
using UnityGameObject = UnityEngine.GameObject;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data
{
    using static GC;
    using static ScriptableObject;
    using static Object;

    [Serializable]
    internal class ReplaceToolSerializedData : IDisposable
    {
        internal Vector2 SelectObjectScrollPosition { get; set; }
        internal ReplaceToolDataSO DataSO { get; set; }
        internal SerializedProperty ReplaceObjectField { get; set; }
        internal SerializedObject SerializedData { get; set; }
        internal ReplaceToolController Window { get; set; }

        internal UnityGameObject ReplacementPrefab
        {
            get => DataSO.ReplacementPrefab;
            set => DataSO.ReplacementPrefab = value;
        }

        internal UnityGameObject[] ObjectsToReplace
        {
            get => DataSO.ObjectsToReplace;
            set => DataSO.ObjectsToReplace = value;
        }

        //
        internal int ObjectInstancesIndex { get; set; }
        internal int ObjectToReplaceTransformSiblingIndex { get; set; }
        internal int[] ObjectInstances { get; set; }
        internal bool CanInitializeReplaceObjectField { get; set; }
        internal bool CanInitializeSelectObjectScrollPosition { get; set; }
        internal bool CanAssignReplacementObject { get; set; }
        internal bool CanGetObjectsToReplace { get; set; }
        internal bool CanInitializeObjectInstances { get; set; }
        internal bool CanInitializeObjectFilter { get; set; }
        internal bool CanInitializeSelection { get; set; }
        internal SelectionMode? ObjectFilter { get; set; }
        internal Transform[] Selection { get; set; }
        internal UnityGameObject ObjectToReplace { get; set; }

        internal UnityGameObject ReplacementPrefabInstance { get; set; }
        //

        internal ReplaceToolSerializedData()
        {
            Initialize();
        }

        private void Initialize()
        {
            DataSO = CreateInstance<ReplaceToolDataSO>();
            SerializedData = new SerializedObject(DataSO);
            ReplaceObjectField = SerializedData.FindProperty(Text.ReplacementPrefab);
            SelectObjectScrollPosition = new Vector2();
            //
            ObjectToReplaceTransformSiblingIndex = new int();
            ObjectInstancesIndex = 0;
            ObjectInstances = null;
            ObjectToReplace = null;
            ReplacementPrefabInstance = null;
            ObjectFilter = null;
            Selection = null;
            CanInitializeReplaceObjectField = false;
            CanInitializeSelectObjectScrollPosition = true;
            CanInitializeObjectInstances = true;
            CanInitializeObjectFilter = true;
            CanInitializeSelection = true;
            CanAssignReplacementObject = true;
            CanGetObjectsToReplace = true;
            //if (!ReplaceObjectField.objectReferenceValue) CanAssignReplacementObject = false;
            //if (ObjectsToReplace.Length == 0) CanGetObjectsToReplace = false;
            //
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
        }

        ~ReplaceToolSerializedData()
        {
            Dispose(false);
        }
    }
}
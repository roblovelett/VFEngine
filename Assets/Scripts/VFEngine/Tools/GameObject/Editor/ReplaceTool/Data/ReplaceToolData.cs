using System;
using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;
using DataSO = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects.ReplaceToolDataSO;
using Object = UnityEngine.Object;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data
{
    using static GC;
    using static ScriptableObject;
    using static Object;

    internal class ReplaceToolData : IDisposable
    {
        private DataSO dataSO;
        internal Vector2? ScrollPosition;
        internal UnityGameObject ReplacementPrefab;
        internal UnityGameObject[] ObjectsToReplace;
        internal SerializedProperty ReplaceObjectField;
        internal SerializedObject SerializedData;
        internal ReplaceToolController Window;
        internal int ObjectInstancesIndex { get; set; }
        internal int ObjectToReplaceTransformSiblingIndex { get; set; }
        internal int[] ObjectInstances { get; set; }
        internal SelectionMode? ObjectFilter { get; set; }
        internal Transform[] Selection { get; set; }
        internal UnityGameObject ObjectToReplace { get; set; }
        internal UnityGameObject ReplacementPrefabInstance { get; set; }

        internal ReplaceToolData()
        {
            Initialize();
        }

        private void Initialize()
        {
            ReplacementPrefab = new UnityGameObject();
            ObjectsToReplace = new UnityGameObject[0];
            ObjectToReplaceTransformSiblingIndex = new int();
            ObjectInstancesIndex = 0;
            ObjectInstances = null;
            ObjectToReplace = null;
            ReplacementPrefabInstance = null;
            ObjectFilter = null;
            Selection = null;
            dataSO = CreateInstance<DataSO>();
            SerializedData = new SerializedObject(dataSO);
            ReplaceObjectField = SerializedData.FindProperty(Text.ReplacementPrefab);
            ScrollPosition = new Vector2();
        }

        public void Dispose()
        {
            Dispose(true);
            SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (!dispose) return;
            DestroyImmediate(dataSO);
        }

        ~ReplaceToolData()
        {
            Dispose(false);
        }
    }
}
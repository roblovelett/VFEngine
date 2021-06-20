using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;
using ControllerData = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolData;
using DataSO = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects.ReplaceToolDataSO;

// ReSharper disable MemberCanBePrivate.Global
namespace VFEngine.Tools.GameObject.Editor.ReplaceTool
{
    using static EditorGUI;
    using static EditorGUILayout;
    using static Debug;
    using static GUILayout;
    using static Text;
    using static EditorStyles;
    using static PrefabUtility;
    using static Selection;
    using static SelectionMode;
    using static Undo;
    using static GUI;

    internal class ReplaceToolController : EditorWindow
    {
        #region fields

        private static ControllerData _data;

        private static int ObjectInstancesIndex
        {
            get => _data.ObjectInstancesIndex;
            set => _data.ObjectInstancesIndex = value;
        }

        private static int ObjectToReplaceTransformSiblingIndex
        {
            get => _data.ObjectToReplaceTransformSiblingIndex;
            set => _data.ObjectToReplaceTransformSiblingIndex = value;
        }

        private static int[] ObjectInstances
        {
            get => _data.ObjectInstances;
            set => _data.ObjectInstances = value;
        }

        private static SelectionMode? ObjectFilter
        {
            get => _data.ObjectFilter;
            set => _data.ObjectFilter = value;
        }

        private static Vector2? ScrollPosition
        {
            get => _data.ScrollPosition;
            set => _data.ScrollPosition = value;
        }

        private static UnityGameObject ReplacementPrefab
        {
            get => _data.ReplacementPrefab;
            set => _data.ReplacementPrefab = value;
        }

        private static UnityGameObject ReplacementPrefabInstance
        {
            get => _data.ReplacementPrefabInstance;
            set => _data.ReplacementPrefabInstance = value;
        }

        private static UnityGameObject ObjectToReplace
        {
            get => _data.ObjectToReplace;
            set => _data.ObjectToReplace = value;
        }

        private static UnityGameObject[] ObjectsToReplace
        {
            get => _data.ObjectsToReplace;
            set => _data.ObjectsToReplace = value;
        }

        private static Transform[] Selection
        {
            get => _data.Selection;
            set => _data.Selection = value;
        }

        private static SerializedProperty ReplaceObjectField => _data.ReplaceObjectField;
        private static SerializedObject SerializedData => _data.SerializedData;

        private static ReplaceToolController Window
        {
            get => _data.Window;
            set => _data.Window = value;
        }

        private static int ObjectsToReplaceAmount => HasObjectsToReplace ? ObjectsToReplace.Length : 0;
        private static bool HasData => _data != null;
        private static bool ReplaceObjectButtonPressed => Button(ReplaceButton);
        private static bool HasSerializedData => SerializedData != null;
        private static bool CanRepaintController => HasSerializedData && SerializedData.UpdateIfRequiredOrScript();
        private static bool HasObjectFilter => ObjectFilter != null;
        private static bool ProcessingObjectInstances => ObjectInstancesIndex < ObjectsToReplaceAmount;
        private static bool CanCreateObjectFields => HasData && HasObjectsToReplace;
        private static bool HasObjectsToReplace => ObjectsToReplace != null;

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Close();
        }

        private void OnDestroy()
        {
            Close();
        }

        private void OnGUI()
        {
            Initialize();
            GUI();
        }

        private void OnSelectionChange()
        {
            Initialize();
            ObjectFilter = Unfiltered ^ ~(Assets | DeepAssets | Deep);
            if (HasObjectFilter && ObjectFilter != null) Selection = GetTransforms((SelectionMode) ObjectFilter);
            ObjectsToReplace = Selection.Select(selected => selected.gameObject).ToArray();
            RepaintController();
        }

        private void OnInspectorUpdate()
        {
            RepaintController();
        }

        #endregion

        #region private methods

        private void RepaintController()
        {
            if (CanRepaintController) Repaint();
        }

        private new void Close()
        {
            _data.Dispose();
        }

        #region static private methods

        private static bool CanInitializeData => _data == null;

        private static void Initialize()
        {
            if (CanInitializeData) _data = new ControllerData();
        }

        [MenuItem(ReplaceToolMenuItem)]
        internal static void ShowWindow()
        {
            Window = GetWindow<ReplaceToolController>();
            Window.Show();
        }

        private static bool Error1;
        private static bool Error2;

        // ReSharper disable Unity.PerformanceAnalysis
        private static void GUI()
        {
            Separator();
            PropertyField(ReplaceObjectField);
            Separator();
            LabelField(HeaderText, boldLabel);
            Separator();
            IntField(ObjectCount, ObjectsToReplaceAmount);
            indentLevel++;
            Separator();
            LabelField(SelectGameObjectsLabel, wordWrappedLabel);
            if (ScrollPosition != null) ScrollPosition = EditorGUILayout.BeginScrollView((Vector2) ScrollPosition);
            EnableGUI(false);
            if (CanCreateObjectFields)
                foreach (var @object in ObjectsToReplace)
                    ObjectField(@object, typeof(UnityGameObject), true);
            EnableGUI(true);
            EditorGUILayout.EndScrollView();
            indentLevel--;
            Separator();
            if (!ReplaceObjectButtonPressed) return;
            if (!Error1) LogErrorFormat(ErrorFormat, NoPrefab);
            if (!Error2) LogErrorFormat(ErrorFormat, NoGameObject);
            ReplaceSelectedObjects(ObjectsToReplace, ReplacementPrefab);
        }

        private static void EnableGUI(bool enable)
        {
            enabled = enable;
        }

        #endregion

        #endregion

        #region public methods

        internal static void ReplaceSelectedObjects(UnityGameObject[] objects, UnityGameObject @object)
        {
            ObjectsToReplace = objects;
            ReplacementPrefab = @object;
            ObjectInstances = new int[ObjectsToReplaceAmount];
            for (ObjectInstancesIndex = 0; ProcessingObjectInstances; ObjectInstancesIndex++)
            {
                ObjectToReplace = ObjectsToReplace[ObjectInstancesIndex];
                RegisterCompleteObjectUndo(ObjectToReplace, SavingObjectState);
                ObjectToReplaceTransformSiblingIndex = ObjectToReplace.transform.GetSiblingIndex();
                ReplacementPrefabInstance = InstantiatePrefab(ReplacementPrefab) as UnityGameObject;
                if (ReplacementPrefabInstance is null) continue;
                ObjectInstances[ObjectInstancesIndex] = ReplacementPrefabInstance.GetInstanceID();
                ReplacementPrefabInstance.transform.position = ObjectToReplace.transform.position;
                ReplacementPrefabInstance.transform.rotation = ObjectToReplace.transform.rotation;
                ReplacementPrefabInstance.transform.parent = ObjectToReplace.transform.parent;
                ReplacementPrefabInstance.transform.localScale = ObjectToReplace.transform.localScale;
                ReplacementPrefabInstance.transform.SetSiblingIndex(ObjectToReplaceTransformSiblingIndex);
                RegisterCreatedObjectUndo(ReplacementPrefabInstance, CreatedReplacementObject);
                foreach (Transform transform in ObjectToReplace.transform)
                    SetTransformParent(transform, ReplacementPrefabInstance.transform, ParentChange);
                DestroyObjectImmediate(ReplacementPrefabInstance);
            }

            instanceIDs = ObjectInstances;
        }

        #endregion
    }
}
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;
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

        private DataSO dataSO;
        private Transform[] selection;
        private SelectionMode? objectFilter;
        private static int _objectInstancesIndex;
        private static int _objectToReplaceTransformSiblingIndex;
        private static int[] _objectInstances;
        private static Vector2? _scrollPosition;
        private static UnityGameObject _replacementPrefab;
        private static UnityGameObject _objectToReplace;
        private static UnityGameObject _replacementPrefabInstance;
        private static UnityGameObject[] _objectsToReplace;
        private static SerializedProperty _replaceObjectField;
        private static SerializedObject _serializedData;
        private static ReplaceToolController _window;
        private static int ObjectsToReplaceAmount => _objectsToReplace?.Length ?? 0;
        private static bool CanRepaintController => _serializedData.UpdateIfRequiredOrScript();
        private static bool ProcessingObjectInstances => _objectInstancesIndex < ObjectsToReplaceAmount;
        private static bool Error1 => Error2;
        private static bool Error2 => Error1;

        private void InitializeData()
        {
            _replacementPrefab = new UnityGameObject();
            _objectsToReplace = new UnityGameObject[0];
            _objectToReplaceTransformSiblingIndex = new int();
            _objectInstancesIndex = 0;
            _objectInstances = null;
            _objectToReplace = null;
            _replacementPrefabInstance = null;
            objectFilter = null;
            selection = null;
            dataSO = CreateInstance<DataSO>();
            _serializedData = new SerializedObject(dataSO);
            _replaceObjectField = _serializedData.FindProperty(ReplacementPrefab);
            _scrollPosition = new Vector2();
        }

        #endregion

        #region unity events

        private void Awake()
        {
            InitializeData();
        }

        private void OnEnable()
        {
            InitializeData();
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
            GUI();
        }

        private void OnSelectionChange()
        {
            objectFilter = Unfiltered ^ ~(Assets | DeepAssets | Deep);
            if (objectFilter != null) selection = GetTransforms((SelectionMode) objectFilter);
            _objectsToReplace = selection.Select(selected => selected.gameObject).ToArray();
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

        #region static private methods

        [MenuItem(ReplaceToolMenuItem)]
        internal static void ShowWindow()
        {
            _window = GetWindow<ReplaceToolController>();
            _window.Show();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private static void GUI()
        {
            Separator();
            PropertyField(_replaceObjectField);
            Separator();
            LabelField(HeaderText, boldLabel);
            Separator();
            IntField(ObjectCount, ObjectsToReplaceAmount);
            indentLevel++;
            Separator();
            LabelField(SelectGameObjectsLabel, wordWrappedLabel);
            if (_scrollPosition != null) _scrollPosition = EditorGUILayout.BeginScrollView((Vector2) _scrollPosition);
            enabled = false;
            foreach (var @object in _objectsToReplace) ObjectField(@object, typeof(UnityGameObject), true);
            enabled = !enabled;
            EditorGUILayout.EndScrollView();
            indentLevel--;
            Separator();
            if (!Button(ReplaceButton)) return;
            if (!Error1) LogErrorFormat(ErrorFormat, NoPrefab);
            if (!Error2) LogErrorFormat(ErrorFormat, NoGameObject);
            ReplaceSelectedObjects(_objectsToReplace, _replacementPrefab);
        }

        #endregion

        #endregion

        #region public methods

        internal static void ReplaceSelectedObjects(UnityGameObject[] objects, UnityGameObject @object)
        {
            _objectsToReplace = objects;
            _replacementPrefab = @object;
            _objectInstances = new int[ObjectsToReplaceAmount];
            for (_objectInstancesIndex = 0; ProcessingObjectInstances; _objectInstancesIndex++)
            {
                _objectToReplace = _objectsToReplace[_objectInstancesIndex];
                RegisterCompleteObjectUndo(_objectToReplace, SavingObjectState);
                _objectToReplaceTransformSiblingIndex = _objectToReplace.transform.GetSiblingIndex();
                _replacementPrefabInstance = InstantiatePrefab(_replacementPrefab) as UnityGameObject;
                if (_replacementPrefabInstance is null) continue;
                _objectInstances[_objectInstancesIndex] = _replacementPrefabInstance.GetInstanceID();
                _replacementPrefabInstance.transform.position = _objectToReplace.transform.position;
                _replacementPrefabInstance.transform.rotation = _objectToReplace.transform.rotation;
                _replacementPrefabInstance.transform.parent = _objectToReplace.transform.parent;
                _replacementPrefabInstance.transform.localScale = _objectToReplace.transform.localScale;
                _replacementPrefabInstance.transform.SetSiblingIndex(_objectToReplaceTransformSiblingIndex);
                RegisterCreatedObjectUndo(_replacementPrefabInstance, CreatedReplacementObject);
                foreach (Transform transform in _objectToReplace.transform)
                    SetTransformParent(transform, _replacementPrefabInstance.transform, ParentChange);
                DestroyObjectImmediate(_replacementPrefabInstance);
            }

            instanceIDs = _objectInstances;
        }

        #endregion
    }
}
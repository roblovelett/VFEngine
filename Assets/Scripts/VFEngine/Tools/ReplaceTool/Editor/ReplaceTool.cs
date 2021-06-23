using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using Text = VFEngine.Tools.ReplaceTool.Editor.Data.ReplaceToolText;
using DataSO = VFEngine.Tools.ReplaceTool.Editor.ScriptableObjects.ReplaceToolDataSO;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace VFEngine.Tools.ReplaceTool.Editor
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

    internal class ReplaceTool : EditorWindow
    {
        #region fields

        private Transform[] selection;
        private SelectionMode? objectFilter;

        #region static fields

        #region static fields

        private static int _objectInstancesIndex;
        private static int _objectToReplaceTransformSiblingIndex;
        private static int[] _objectInstances;
        private static DataSO _dataSO;
        private static Vector2 _scrollPosition;
        private static UnityGameObject _replacementPrefab;
        private static UnityGameObject _objectToReplace;
        private static UnityGameObject _replacementPrefabInstance;
        private static UnityGameObject[] _objectsToReplace;
        private static SerializedObject _serializedData;
        private static SerializedProperty _replaceObjectField;
        private static ReplaceTool _window;

        #endregion

        #endregion

        #endregion

        #region properties

        #region private static properties

        private static int ObjectsToReplaceAmount => _objectsToReplace?.Length ?? 0;
        private static bool DisplaySelectObjectsLabel => ObjectsToReplaceAmount == 0;

        #endregion

        #endregion

        #region private methods

        #region initialization

        private static void Initialize()
        {
            _objectInstancesIndex = 0;
            _dataSO = CreateInstance<DataSO>();
            _scrollPosition = new Vector2();
            _serializedData = new SerializedObject(_dataSO);
            _replaceObjectField = _serializedData.FindProperty(ReplacementPrefab);
        }

        #endregion

        #region unity events

        private void OnGUI()
        {
            Initialize();
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

        private void RepaintController()
        {
            if (_serializedData.UpdateIfRequiredOrScript()) Repaint();
        }

        #region static private methods

        private static bool NoObjectsToReplace => DisplaySelectObjectsLabel;

        private static void GUI()
        {
            Separator();
            PropertyField(_replaceObjectField);
            Separator();
            LabelField(HeaderText, boldLabel);
            Separator();
            IntField(ObjectCount, ObjectsToReplaceAmount);
            indentLevel++;
            if (DisplaySelectObjectsLabel)
            {
                Separator();
                LabelField(SelectGameObjectsLabel, wordWrappedLabel);
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            enabled = false;
            if (_dataSO && _objectsToReplace != null)
                foreach (var @object in _objectsToReplace)
                    ObjectField(@object, typeof(UnityGameObject), true);
            enabled = !enabled;
            EditorGUILayout.EndScrollView();
            indentLevel--;
            Separator();
            if (Button(ReplaceButton))
            {
                if (!_replaceObjectField.objectReferenceValue) LogErrorFormat(ErrorFormat, NoPrefab);
                if (NoObjectsToReplace) LogErrorFormat(ErrorFormat, NoGameObject);
                ReplaceSelectedObjects(_objectsToReplace, _replacementPrefab);
            }

            Separator();
            _serializedData.ApplyModifiedProperties();
        }

        #endregion

        #endregion

        #region internal methods

        #region static internal methods

        [MenuItem(ReplaceToolMenuItem)]
        internal static void ShowWindow()
        {
            _window = GetWindow<ReplaceTool>();
            _window.Show();
        }

        internal static void ReplaceSelectedObjects(UnityGameObject[] objects, UnityGameObject @object)
        {
            _objectsToReplace = objects;
            _replacementPrefab = @object;
            _objectInstances = new int[ObjectsToReplaceAmount];
            for (_objectInstancesIndex = 0; _objectInstancesIndex < ObjectsToReplaceAmount; _objectInstancesIndex++)
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

        #endregion
    }
}
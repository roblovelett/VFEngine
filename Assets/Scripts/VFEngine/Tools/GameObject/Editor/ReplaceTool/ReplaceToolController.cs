using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;
using ControllerData = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolSerializedData;
using DataSO = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects.ReplaceToolDataSO;

// ReSharper disable MemberCanBePrivate.Global
namespace VFEngine.Tools.GameObject.Editor.ReplaceTool
{
    using static EditorGUI;
    using static EditorGUILayout;
    using static Debug;
    using static UnityEngine.GUILayout;//GUILayout;
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

        //
        private static SerializedObject _serializedData;

        // Prefab variable from data object. Using SerializedProperty for integrated Undo
        private static SerializedProperty _replaceObjectField;

        // Scroll position for list of selected objects
        private static Vector2 _selectObjectScrollPosition;

        //
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

        private static bool CanAssignReplacementObject
        {
            get => _data.CanAssignReplacementObject;
            set => _data.CanAssignReplacementObject = value;
        }

        private static bool CanGetObjectsToReplace
        {
            get => _data.CanGetObjectsToReplace;
            set => _data.CanGetObjectsToReplace = value;
        }

        private static bool CanInitializeObjectInstances
        {
            get => _data.CanInitializeObjectInstances;
            set => _data.CanInitializeObjectInstances = value;
        }

        private static bool CanInitializeSelection
        {
            get => _data.CanInitializeSelection;
            set => _data.CanInitializeSelection = value;
        }

        private static bool CanInitializeObjectFilter
        {
            get => _data.CanInitializeObjectFilter;
            set => _data.CanInitializeObjectFilter = value;
        }

        private static bool CanInitializeSelectObjectScrollPosition
        {
            get => _data.CanInitializeSelectObjectScrollPosition;
            set => _data.CanInitializeSelectObjectScrollPosition = value;
        }

        private static bool CanInitializeReplaceObjectField
        {
            get => _data.CanInitializeReplaceObjectField;
            set => _data.CanInitializeReplaceObjectField = value;
        }

        private static SelectionMode? ObjectFilter
        {
            get => _data.ObjectFilter;
            set => _data.ObjectFilter = value;
        }

        /*private static Vector2 SelectObjectScrollPosition
        {
            get => _data.selectObjectScrollPosition;
            set => _data.selectObjectScrollPosition = value;
        }*/

        private static UnityGameObject ReplacementPrefab
        {
            get => _data.replacementPrefab;
            set => _data.replacementPrefab = value;
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
            get => _data.objectsToReplace;
            set => _data.objectsToReplace = value;
        }

        private static Transform[] Selection
        {
            get => _data.Selection;
            set => _data.Selection = value;
        }

        /*private static SerializedProperty ReplaceObjectField
        {
            get => _data.ReplaceObjectField;
            set => _data.ReplaceObjectField = value;
        }*/

        private static ReplaceToolController Window
        {
            get => _data.window;
            set => _data.window = value;
        }

        private static int ObjectsToReplaceAmount => HasObjectsToReplace ? ObjectsToReplace.Length : 0;
        private static bool HasData => _data != null;
        private static bool ReplaceObjectButtonPressed => Button(ReplaceButton);
        private static bool HasSerializedData => _serializedData != null;
        private static bool CanRepaintController => HasSerializedData && _serializedData.UpdateIfRequiredOrScript();
        private static bool HasObjectFilter => ObjectFilter != null;
        private static bool ProcessingObjectInstances => ObjectInstancesIndex < ObjectsToReplaceAmount;
        private static bool CanCreateObjectFields => HasData && HasObjectsToReplace;
        private static bool HasObjectsToReplace => ObjectsToReplace != null;
        private static bool CanPrintInformation => ObjectsToReplaceAmount == 0;

        private static bool CanInitializeData => _data == null || !InitializedData;
        //private static SerializedObject SerializedData => _data.SerializedData;

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
            InitializeObjectFilter();
            InitializeSelection();
            ApplySelectionToObjectsToReplace();
            RepaintController();
        }

        private void OnInspectorUpdate()
        {
            RepaintController();
        }

        #endregion

        #region private methods

        private static void InitializeObjectFilter()
        {
            if (!CanInitializeObjectFilter) return;
            ObjectFilter = Unfiltered ^ ~(Assets | DeepAssets | Deep);
            CanInitializeObjectFilter = !CanInitializeObjectFilter;
        }

        private static void InitializeSelection()
        {
            if (!CanInitializeSelection || !HasObjectFilter) return;
            // ReSharper disable once PossibleInvalidOperationException
            Selection = GetTransforms((SelectionMode) ObjectFilter);
            CanInitializeSelection = !CanInitializeSelection;
        }

        private static void ApplySelectionToObjectsToReplace()
        {
            ObjectsToReplace = Selection.Select(selected => selected.gameObject).ToArray();
        }

        private void RepaintController()
        {
            if (CanRepaintController) Repaint();
        }

        private new void Close()
        {
            _data.Dispose();
        }

        #region static private methods

        private static DataSO DataSO => _data.dataSO;
        //private static bool HasDataSO => DataSO != null;

        private static bool CanInitializeSerializedData
        {
            get => _data.CanInitializeSerializedData;
            set => _data.CanInitializeSerializedData = value;
        }

        private static bool InitializedData
        {
            get => _data.InitializedData;
            set => _data.InitializedData = value;
        }
        //private static bool CanInitializeSerializedData => _serializedData == null && HasDataSO;

        private static void Initialize()
        {
            if (CanInitializeData)
            {
                _data = new ControllerData();
                InitializedData = true;
            }

            if (CanInitializeSerializedData)
            {
                _serializedData = new SerializedObject(DataSO);
                CanInitializeSerializedData = !CanInitializeSerializedData;
            }

            if (CanInitializeReplaceObjectField)
            {
                _replaceObjectField = _serializedData.FindProperty(Text.ReplacementPrefab);
                CanInitializeReplaceObjectField = !CanInitializeReplaceObjectField;
            }

            if (CanInitializeSelectObjectScrollPosition)
            {
                _selectObjectScrollPosition = new Vector2();
                CanInitializeSelectObjectScrollPosition = !CanInitializeSelectObjectScrollPosition;
            }
        }

        [MenuItem(ReplaceToolMenuItem)]
        internal static void ShowWindow()
        {
            Window = GetWindow<ReplaceToolController>();
            Window.Show();
        }

        private static void GUI()
        {
            Header();
            SaveObjectsToReplace();
            IndentLevel(true);
            PrintSelectObjectsInformation();
            ScrollView(true);
            EnableGUI(false);
            CreateObjectFields();
            EnableGUI(true);
            ScrollView(false);
            IndentLevel(false);
            Separator();
            ReplaceObjectButton();
        }

        private static void Header()
        {
            Separator();
            OnSetPropertyField();
            Separator();
            LabelField(HeaderText, boldLabel);
            Separator();
        }

        private static void OnSetPropertyField()
        {
            if (CanInitializeReplaceObjectField)
            {
                _replaceObjectField = _serializedData.FindProperty(Text.ReplacementPrefab);
                CanInitializeReplaceObjectField = !CanInitializeReplaceObjectField;
            }

            PropertyField(_replaceObjectField);
        }

        private static void SaveObjectsToReplace()
        {
            IntField(ObjectCount, ObjectsToReplaceAmount);
        }

        private static void IndentLevel(bool hasIndent)
        {
            if (hasIndent) indentLevel++;
            else indentLevel--;
        }

        private static void PrintSelectObjectsInformation()
        {
            if (!CanPrintInformation) return;
            Separator();
            LabelField(SelectGameObjectsLabel, wordWrappedLabel);
        }

        private static void ScrollView(bool beginScrollView)
        {
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (beginScrollView) // && CanInitializeSelectObjectScrollPosition)
                _selectObjectScrollPosition = BeginScrollView(_selectObjectScrollPosition);
            //CanInitializeSelectObjectScrollPosition = !CanInitializeSelectObjectScrollPosition;
            if (!beginScrollView) EndScrollView();
        }

        private static Vector2 BeginScrollView(Vector2 scrollPosition)
        {
            return EditorGUILayout.BeginScrollView(scrollPosition);
        }

        private static void EndScrollView()
        {
            EditorGUILayout.EndScrollView();
        }

        private static void EnableGUI(bool enable)
        {
            enabled = enable;
        }

        private static void CreateObjectFields()
        {
            if (CanCreateObjectFields) ProcessObjectsToReplace();
        }

        private static void ProcessObjectsToReplace()
        {
            foreach (var @object in ObjectsToReplace) ObjectField(@object, typeof(UnityGameObject), true);
        }

        private static void ReplaceObjectButton()
        {
            if (!ReplaceObjectButtonPressed) return;
            IsReplaceObjectAssigned();
            CanReplaceObjects();
            ReplaceSelectedObjects(ObjectsToReplace, ReplacementPrefab);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private static void IsReplaceObjectAssigned()
        {
            if (CanAssignReplacementObject) return;
            CannotAssignReplacementObjectError();
            CanAssignReplacementObject = !CanAssignReplacementObject;
        }

        private static void CannotAssignReplacementObjectError()
        {
            LogErrorFormat(ErrorFormat, NoPrefab);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private static void CanReplaceObjects()
        {
            if (CanGetObjectsToReplace) return;
            CannotGetObjectsToReplaceError();
            CanGetObjectsToReplace = !CanGetObjectsToReplace;
        }

        private static void CannotGetObjectsToReplaceError()
        {
            LogErrorFormat(ErrorFormat, NoGameObject);
        }

        private static void InitializeObjectsToReplace(UnityGameObject[] objectsToReplace)
        {
            ObjectsToReplace = objectsToReplace;
        }

        private static void InitializeReplacementPrefab(UnityGameObject replacementPrefab)
        {
            ReplacementPrefab = replacementPrefab;
        }

        private static void InitializeObjectInstances()
        {
            if (!CanInitializeObjectInstances) return;
            ObjectInstances = new int[ObjectsToReplaceAmount];
            CanInitializeObjectInstances = !CanInitializeObjectInstances;
        }

        private static void ProcessObjectInstances()
        {
            for (ObjectInstancesIndex = 0; ProcessingObjectInstances; ObjectInstancesIndex++)
            {
                InitializeObjectToReplace();
                SaveObjectState();
                InitializeObjectToReplaceTransformSiblingIndex();
                InitializeReplacementPrefabInstance();
                InitializeObjectInstance();
                InitializeReplacementPrefabInstanceTransform();
                DeleteObjectState();
                ProcessObjectToReplace();
                DeleteObjectToReplaceToReplaceState();
            }
        }

        private static void InitializeObjectToReplace()
        {
            ObjectToReplace = ObjectsToReplace[ObjectInstancesIndex];
        }

        private static void SaveObjectState()
        {
            RegisterCompleteObjectUndo(ObjectToReplace, SavingObjectState);
        }

        private static void InitializeObjectToReplaceTransformSiblingIndex()
        {
            ObjectToReplaceTransformSiblingIndex = ObjectToReplace.transform.GetSiblingIndex();
        }

        private static void InitializeReplacementPrefabInstance()
        {
            ReplacementPrefabInstance = InstantiatePrefab(ReplacementPrefab) as UnityGameObject;
        }

        private static void InitializeObjectInstance()
        {
            ObjectInstances[ObjectInstancesIndex] = ReplacementPrefabInstance.GetInstanceID();
        }

        private static void InitializeReplacementPrefabInstanceTransform()
        {
            ReplacementPrefabInstance.transform.position = ObjectToReplace.transform.position;
            ReplacementPrefabInstance.transform.rotation = ObjectToReplace.transform.rotation;
            ReplacementPrefabInstance.transform.parent = ObjectToReplace.transform.parent;
            ReplacementPrefabInstance.transform.localScale = ObjectToReplace.transform.localScale;
            ReplacementPrefabInstance.transform.SetSiblingIndex(ObjectToReplaceTransformSiblingIndex);
        }

        private static void DeleteObjectState()
        {
            RegisterCreatedObjectUndo(ReplacementPrefabInstance, CreatedReplacementObject);
        }

        private static void ProcessObjectToReplace()
        {
            foreach (Transform transform in ObjectToReplace.transform)
                SaveObjectToReplaceState(transform, ReplacementPrefabInstance.transform);
        }

        private static void SaveObjectToReplaceState(Transform transform, Transform instanceTransform)
        {
            SetTransformParent(transform, instanceTransform, ParentChange);
        }

        private static void DeleteObjectToReplaceToReplaceState()
        {
            DestroyObjectImmediate(ReplacementPrefabInstance);
        }

        private static void SelectionInstanceIds()
        {
            instanceIDs = ObjectInstances;
        }

        #endregion

        #endregion

        #region public methods

        internal static void ReplaceSelectedObjects(UnityGameObject[] objectsToReplace,
            UnityGameObject replacementObject)
        {
            InitializeObjectsToReplace(objectsToReplace);
            InitializeReplacementPrefab(replacementObject);
            InitializeObjectInstances();
            ProcessObjectInstances();
            SelectionInstanceIds();
        }

        #endregion
    }
}
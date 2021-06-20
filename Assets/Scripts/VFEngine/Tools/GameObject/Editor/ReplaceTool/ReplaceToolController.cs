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

        private static bool InitializedData
        {
            get => _data.InitializedData;
            set => _data.InitializedData = value;
        }

        private static bool InitializedDataSO
        {
            get => _data.InitializedDataSO;
            set => _data.InitializedDataSO = value;
        }

        private static bool InitializedSerializedData
        {
            get => _data.InitializedSerializedData;
            set => _data.InitializedSerializedData = value;
        }

        private static bool InitializedObjectField
        {
            get => _data.InitializedObjectField;
            set => _data.InitializedObjectField = value;
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

        private static bool InitializedScrollPosition
        {
            get => _data.InitializedScrollPosition;
            set => _data.InitializedScrollPosition = value;
        }

        private static bool IsScrollView
        {
            get => _data.IsScrollView;
            set => _data.IsScrollView = value;
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

        private static SerializedProperty ReplaceObjectField
        {
            get => _data.ReplaceObjectField;
            set => _data.ReplaceObjectField = value;
        }

        private static SerializedObject SerializedData
        {
            get => _data.SerializedData;
            set => _data.SerializedData = value;
        }

        private static DataSO DataSO
        {
            get => _data.DataSO;
            set => _data.DataSO = value;
        }

        private static ReplaceToolController Window
        {
            get => _data.Window;
            set => _data.Window = value;
        }

        private static int ObjectsToReplaceAmount => HasObjectsToReplace ? ObjectsToReplace.Length : 0;
        private static bool CanInitializeData => _data == null || !InitializedData;
        private static bool CanInitializeDataSO => DataSO == null || !InitializedDataSO;
        private static bool CanInitializeSerializedData => SerializedData == null || !InitializedSerializedData;
        private static bool CanInitializeReplaceObjectField => ReplaceObjectField == null || !InitializedObjectField;
        private static bool HasData => _data != null;
        private static bool ReplaceObjectButtonPressed => Button(ReplaceButton);
        private static bool HasSerializedData => SerializedData != null;
        private static bool CanRepaintController => HasSerializedData && SerializedData.UpdateIfRequiredOrScript();
        private static bool HasObjectFilter => ObjectFilter != null;
        private static bool ProcessingObjectInstances => ObjectInstancesIndex < ObjectsToReplaceAmount;
        private static bool CanCreateObjectFields => HasData && HasObjectsToReplace;
        private static bool HasObjectsToReplace => ObjectsToReplace != null;
        private static bool CanPrintInformation => ObjectsToReplaceAmount == 0;
        private static bool CanInitializeScrollPosition => ScrollPosition == null || !InitializedScrollPosition;

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

        private static void Initialize()
        {
            InitializeData();
            InitializeDataSO();
            InitializeSerializedData();
            InitializeReplaceObjectField();
            InitializeScrollPosition();
        }

        private static void InitializeScrollPosition()
        {
            if (!CanInitializeScrollPosition) return;
            ScrollPosition = new Vector2();
            InitializedScrollPosition = !InitializedScrollPosition;
        }

        private static void InitializeData()
        {
            if (!CanInitializeData) return;
            _data = new ControllerData();
            InitializedData = true;
        }

        private static void InitializeDataSO()
        {
            if (!CanInitializeDataSO) return;
            DataSO = CreateInstance<DataSO>();
            InitializedDataSO = !InitializedDataSO;
        }

        private static void InitializeSerializedData()
        {
            if (!CanInitializeSerializedData) return;
            SerializedData = new SerializedObject(DataSO);
            InitializedSerializedData = !InitializedSerializedData;
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

        private static void InitializeReplaceObjectField()
        {
            if (!CanInitializeReplaceObjectField) return;
            ReplaceObjectField = SerializedData.FindProperty(Text.ReplacementPrefab);
            InitializedObjectField = !InitializedObjectField;
        }

        private static void OnSetPropertyField()
        {
            InitializeReplaceObjectField();
            PropertyField(ReplaceObjectField);
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
            if (beginScrollView)
            {
                InitializeScrollPosition();
                ScrollPosition = BeginScrollView(ScrollPosition);
            }
            else
            {
                EndScrollView();
            }
        }

        private static Vector2? BeginScrollView(Vector2? scrollPosition)
        {
            if (IsScrollView) return ScrollPosition;
            if (scrollPosition == null) return null;
            IsScrollView = !IsScrollView;
            return EditorGUILayout.BeginScrollView((Vector2) scrollPosition);
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

        internal static void ReplaceSelectedObjects(UnityGameObject[] objects, UnityGameObject @object)
        {
            InitializeObjectsToReplace(objects);
            InitializeReplacementPrefab(@object);
            InitializeObjectInstances();
            ProcessObjectInstances();
            SelectionInstanceIds();
        }

        #endregion
    }
}
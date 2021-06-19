using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;
using ControllerData = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolSerializedData;

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

        private static Vector2 SelectObjectScrollPosition
        {
            get => _data.SelectObjectScrollPosition;
            set => _data.SelectObjectScrollPosition = value;
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
        private static bool CanPrintInformation => ObjectsToReplaceAmount == 0;
        private static bool CanInitializeData => _data == null;
        private static SerializedObject SerializedData => _data.SerializedData;

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
            if (CanInitializeData) _data = new ControllerData();
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
                ReplaceObjectField = SerializedData.FindProperty(Text.ReplacementPrefab);
                CanInitializeReplaceObjectField = !CanInitializeReplaceObjectField;
            }

            PropertyField(ReplaceObjectField);
        }

        /*
        NullReferenceException: Object reference not set to an instance of an object
        UnityEditor.EditorGUILayout.IsChildrenIncluded (UnityEditor.SerializedProperty prop) (at <9540aba417024bb296674f70fa788b73>:0)
        UnityEditor.EditorGUILayout.PropertyField (UnityEditor.SerializedProperty property, UnityEngine.GUILayoutOption[] options) (at <9540aba417024bb296674f70fa788b73>:0)
        VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController.OnSetPropertyField () (at Assets/Scripts/VFEngine/Tools/GameObject/Editor/ReplaceTool/ReplaceToolController.cs:281)
        VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController.Header () (at Assets/Scripts/VFEngine/Tools/GameObject/Editor/ReplaceTool/ReplaceToolController.cs:267)
        VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController.GUI () (at Assets/Scripts/VFEngine/Tools/GameObject/Editor/ReplaceTool/ReplaceToolController.cs:250)
        VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController.OnGUI () (at Assets/Scripts/VFEngine/Tools/GameObject/Editor/ReplaceTool/ReplaceToolController.cs:183)
        UnityEditor.HostView.InvokeOnGUI (UnityEngine.Rect onGUIPosition, UnityEngine.Rect viewRect) (at <9540aba417024bb296674f70fa788b73>:0)
        UnityEditor.DockArea.DrawView (UnityEngine.Rect viewRect, UnityEngine.Rect dockAreaRect) (at <9540aba417024bb296674f70fa788b73>:0)
        UnityEditor.DockArea.OldOnGUI () (at <9540aba417024bb296674f70fa788b73>:0)
        UnityEngine.UIElements.IMGUIContainer.DoOnGUI (UnityEngine.Event evt, UnityEngine.Matrix4x4 parentTransform, UnityEngine.Rect clippingRect, System.Boolean isComputingLayout, UnityEngine.Rect layoutSize, System.Action onGUIHandler, System.Boolean canAffectFocus) (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.IMGUIContainer.HandleIMGUIEvent (UnityEngine.Event e, UnityEngine.Matrix4x4 worldTransform, UnityEngine.Rect clippingRect, System.Action onGUIHandler, System.Boolean canAffectFocus) (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.IMGUIContainer.DoIMGUIRepaint () (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.UIR.RenderChainCommand.ExecuteNonDrawMesh (UnityEngine.UIElements.UIR.DrawParams drawParams, System.Single pixelsPerPoint, System.Exception& immediateException) (at <1fd6bc3af931450b977286a218b046fb>:0)
        Rethrow as ImmediateModeException
        UnityEngine.UIElements.UIR.RenderChain.Render () (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.UIRRepaintUpdater.Update () (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.VisualTreeUpdater.UpdateVisualTreePhase (UnityEngine.UIElements.VisualTreeUpdatePhase phase) (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.Panel.UpdateForRepaint () (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.Panel.Repaint (UnityEngine.Event e) (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.UIElementsUtility.DoDispatch (UnityEngine.UIElements.BaseVisualElementPanel panel) (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.UIElementsUtility.UnityEngine.UIElements.IUIElementsUtility.ProcessEvent (System.Int32 instanceID, System.IntPtr nativeEventPtr, System.Boolean& eventHandled) (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.UIEventRegistration.ProcessEvent (System.Int32 instanceID, System.IntPtr nativeEventPtr) (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.UIElements.UIEventRegistration+<>c.<.cctor>b__1_2 (System.Int32 i, System.IntPtr ptr) (at <1fd6bc3af931450b977286a218b046fb>:0)
        UnityEngine.GUIUtility.ProcessEvent (System.Int32 instanceID, System.IntPtr nativeEventPtr, System.Boolean& result) (at <6ddf8eac3856492ab1b8cf42618915cc>:0)
         */

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
            if (beginScrollView && CanInitializeSelectObjectScrollPosition)
            {
                SelectObjectScrollPosition = BeginScrollView(SelectObjectScrollPosition);
                CanInitializeSelectObjectScrollPosition = !CanInitializeSelectObjectScrollPosition;
            }

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
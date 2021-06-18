using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.ReplaceTool.Data;
using UnityGameObject = UnityEngine.GameObject;
using ModelView = VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolModelView;
using DataSO = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects.ReplaceToolDataSO;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool
{
    using static EditorGUI;
    using static EditorGUILayout;
    using static Debug;
    using static GUI;
    using static GUILayout;
    using static ReplaceToolState;
    using static ReplaceToolText;
    using static EditorStyles;

    /*
    NullReferenceException: Object reference not set to an instance of an object
    UnityEditor.EditorGUILayout.IsChildrenIncluded (UnityEditor.SerializedProperty prop) (at <9540aba417024bb296674f70fa788b73>:0)
    UnityEditor.EditorGUILayout.PropertyField (UnityEditor.SerializedProperty property, UnityEngine.GUILayoutOption[] options) (at <9540aba417024bb296674f70fa788b73>:0)
    VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController.Header () (at Assets/Scripts/VFEngine/Tools/GameObject/Editor/ReplaceTool/ReplaceToolController.cs:165)
    VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController.OnGUI () (at Assets/Scripts/VFEngine/Tools/GameObject/Editor/ReplaceTool/ReplaceToolController.cs:48)
    UnityEditor.HostView.InvokeOnGUI (UnityEngine.Rect onGUIPosition, UnityEngine.Rect viewRect) (at <9540aba417024bb296674f70fa788b73>:0)
    UnityEditor.DockArea.DrawView (UnityEngine.Rect viewRect, UnityEngine.Rect dockAreaRect) (at <9540aba417024bb296674f70fa788b73>:0)
    UnityEditor.DockArea.OldOnGUI () (at <9540aba417024bb296674f70fa788b73>:0)
    UnityEngine.UIElements.IMGUIContainer.DoOnGUI (UnityEngine.Event evt, UnityEngine.Matrix4x4 parentTransform, UnityEngine.Rect clippingRect, System.Boolean isComputingLayout, UnityEngine.Rect layoutSize, System.Action onGUIHandler, System.Boolean canAffectFocus) (at <1fd6bc3af931450b977286a218b046fb>:0)
    UnityEngine.GUIUtility:ProcessEvent(Int32, IntPtr, Boolean&)
     */
    /*
     Destroying object multiple times. Don't use DestroyImmediate on the same object in OnDisable or OnDestroy.
    UnityEditor.EditorWindow:Close ()
    VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController:Close () (at Assets/Scripts/VFEngine/Tools/GameObject/Editor/ReplaceTool/ReplaceToolController.cs:232)
    VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController:OnDisable () (at Assets/Scripts/VFEngine/Tools/GameObject/Editor/ReplaceTool/ReplaceToolController.cs:222)
    UnityEngine.GUIUtility:ProcessEvent (int,intptr,bool&)
     */

    internal class ReplaceToolController : EditorWindow
    {
        private static ModelView _replaceTool;

        [MenuItem(ReplaceToolMenuItem)]
        internal static void ShowWindow()
        {
            var window = GetWindow<ReplaceToolController>();
            _replaceTool.ShowWindow(window);
        }

        private static bool HasScrollPosition => _replaceTool.HasScrollPosition;
        private static bool CanSetHeader => _replaceTool.State == SettingHeader;
        private static bool CanSaveObjectsToReplace => _replaceTool.State == SavingObjectsToReplace;
        private static bool CanPrintInformationOnSelection => _replaceTool.State == PrintingInformationOnSelection;
        private static bool CanBeginScrollView => _replaceTool.State == BeginningScrollView;
        private static bool CanViewSelectedObjects => _replaceTool.State == ViewingSelectedObjects;

        private static bool CanReplaceSelectedObjects => _replaceTool.State == ReplacingSelectedObjects &&
                                                         Button(ReplaceButton) && !LoggedErrors;

        private static bool CanApplyModifiedProperties => _replaceTool.State == ApplyingModifiedProperties;

        private void OnGUI()
        {
            _replaceTool.OnGUI();
            if (CanSetHeader) Header();
            if (CanSaveObjectsToReplace) SaveObjectsToReplace();
            if (CanPrintInformationOnSelection) PrintInformationOnSelection();
            if (CanBeginScrollView) BeginScrollView();
            if (CanViewSelectedObjects) ViewSelectedObjects();
            if (CanReplaceSelectedObjects) ReplaceSelectedObjects();
            if (CanApplyModifiedProperties) ApplyModifiedProperties();
        }

        private static void ApplyModifiedProperties()
        {
            Separator();
            _replaceTool.ApplyModifiedProperties();
        }

        private static bool LoggedErrors => LoggedNoPrefabToReplaceError() || LoggedObjectToReplaceError();
        private static bool ReplacementPrefabHasReferenceValue => ReplaceObjectField.objectReferenceValue;

        private static bool LoggedNoPrefabToReplaceError()
        {
            if (ReplacementPrefabHasReferenceValue) return false;
            LogErrorFormat(ErrorFormat, NoPrefab);
            return true;
        }

        private static bool HasObjectsToReplace => ObjectsToReplaceAmount != 0;

        private static bool LoggedObjectToReplaceError()
        {
            if (HasObjectsToReplace) return false;
            LogErrorFormat(ErrorFormat, NoGameObject);
            return true;
        }

        private static void ReplaceSelectedObjects()
        {
            _replaceTool.ReplaceSelectedObjects();
        }

        private static bool ViewedSelectedGameObjects =>
            HasScrollPosition ? ViewedSelectedObjects() : ScrollPositionError;

        private static bool ScrollPositionError => throw new InvalidOperationException(CannotSetScrollPosition);

        private static void ViewSelectedObjects()
        {
            if (!ViewedSelectedGameObjects) throw new InvalidOperationException(CannotViewSelectedObjects);
            _replaceTool.OnViewedSelectedObjects();
        }

        private static bool ViewedSelectedObjects()
        {
            EnableGUI();
            EndScrollView();
            indentLevel--;
            Separator();
            return true;
        }

        private static void EndScrollView()
        {
            EditorGUILayout.EndScrollView();
        }

        private static bool CanProcessSelectedGameObjects => _replaceTool.CanProcessSelectedGameObjects;

        private static bool EnabledOnObjectsProcessed =>
            CanProcessSelectedGameObjects && ProcessedSelectedGameObjects();

        private static void EnableGUI()
        {
            enabled = true;
            enabled = EnabledOnObjectsProcessed;
        }

        private static IEnumerable<UnityGameObject> ObjectsToReplace => _replaceTool.ObjectsToReplace;

        private static bool ProcessedSelectedGameObjects()
        {
            foreach (var @object in ObjectsToReplace) ObjectField(@object, typeof(UnityGameObject), true);
            return true;
        }

        private static void BeginScrollView()
        {
            var selectObjectScrollPosition = new Vector2();
            selectObjectScrollPosition = EditorGUILayout.BeginScrollView(selectObjectScrollPosition);
            _replaceTool.OnBeginScrollView(selectObjectScrollPosition);
        }

        private static bool CanPrintInformation => _replaceTool.ObjectsToReplaceAmount == 0;

        private static void PrintInformationOnSelection()
        {
            if (CanPrintInformation)
            {
                Separator();
                LabelField(SelectGameObjectsLabel, wordWrappedLabel);
            }

            _replaceTool.OnPrintedInformationOnSelection();
        }

        private static int ObjectsToReplaceAmount => _replaceTool.ObjectsToReplaceAmount;

        private static void SaveObjectsToReplace()
        {
            IntField(ObjectCount, ObjectsToReplaceAmount);
            indentLevel++;
            _replaceTool.OnSavedObjectsToReplace();
        }

        private static SerializedProperty ReplaceObjectField => _replaceTool.ReplaceObjectField;

        private static void Header()
        {
            Separator();
            PropertyField(ReplaceObjectField);
            Separator();
            LabelField(HeaderText, boldLabel);
            Separator();
            _replaceTool.OnSetHeader();
        }

        private static bool CanRepaint => _replaceTool.CanRepaint;

        private void OnInspectorUpdate()
        {
            OnRepaint();
        }

        private void OnSelectionChange()
        {
            _replaceTool.OnSelectionChange();
            OnRepaint();
        }

        private void OnRepaint()
        {
            if (!CanRepaint) return;
            Repaint();
        }

        internal void ReplaceSelectedObjects(UnityGameObject[] objectsToReplace, UnityGameObject replacementPrefab)
        {
            _replaceTool.ReplaceSelectedObjects(objectsToReplace, replacementPrefab);
        }

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            Initialize();
        }

        private static bool CanInitializeModelView => _replaceTool == null;

        private static void Initialize()
        {
            if (!CanInitializeModelView) return;
            InitializeModelView();
        }

        private static void InitializeModelView()
        {
            var dataSO = CreateInstance<DataSO>();
            _replaceTool = new ReplaceToolModelView(dataSO);
        }

        private void OnDisable()
        {
            Close();
        }

        private void OnDestroy()
        {
            Close();
        }

        private new void Close()
        {
            base.Close();
            _replaceTool.Dispose();
        }
    }
}
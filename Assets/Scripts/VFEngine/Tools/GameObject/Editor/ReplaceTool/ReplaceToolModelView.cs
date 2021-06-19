/*
using System;
using UnityEditor;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;
using SystemDebug = System.Diagnostics.Debug;
using Controller = VFEngine.Tools.GameObject.Editor.ReplaceTool.ReplaceToolController;
using ModelViewData = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolData;
using DataSO = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects.ReplaceToolDataSO;
using Text = VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ReplaceToolText;
*/
// ReSharper disable PossibleNullReferenceException

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool
{
}
/*

//using static ReplaceToolState;
    using static GC;
    using static Selection;

    internal class ReplaceToolModelView : IDisposable
    {
        private ModelViewData data;
        private bool CanInitializeData => data == null;

        internal ReplaceToolModelView()
        {
            if (!CanInitializeData) return;
            InitializeData();
        }

        private void InitializeData()
        {
            data = new ModelViewData();
        }

        private void Initialize()
        {
            if (!CanInitializeData) return;
            InitializeData();
        }

        private Controller Window
        {
            get => data.Window;
            set => data.Window = value;
        }

        private void InitializeWindow(Controller window)
        {
            Window = window;
        }

        //private bool CanInitializeSerializedData => !CanInitializeData && data.SerializedData == null;
        /*internal SerializedObject SerializedData
        {
            get => data.SerializedData;
            set => data.SerializedData = value;
        }*/

//internal SerializedProperty ReplaceObjectField => ReplaceObjectFieldInternal.Copy();
/*internal SerializedProperty ReplaceObjectField
{
    get => data.ReplaceObjectField;
    set => data.ReplaceObjectField = value;
}*/
/*private SerializedProperty ReplaceObjectFieldInstance
{
    get => data.ReplaceObjectFieldInstance;
    set => data.ReplaceObjectFieldInstance = value;
}*/

//private DataSO DataSO => data.DataSO;

//private void InitializeData()
//{
//if (!CanInitializeSerializedData) return;
//SerializedData = new SerializedObject(DataSO);
//ReplaceObjectField = SerializedData.FindProperty(Text.ReplacementPrefab);
//ReplaceObjectFieldInstance = ReplaceObjectField;
//}
/*
        internal Vector2? SelectObjectScrollPosition
        {
            get => data.SelectObjectScrollPosition;
            set => data.SelectObjectScrollPosition = value;
        }

        internal bool CanInitializeScrollPosition => SelectObjectScrollPosition == null;

        internal void OnSelectObjectScrollPosition()
        {
            SelectObjectScrollPosition = new Vector2();
        }

        internal void ShowWindow(Controller window)
        {
            InitializeWindow(window);
            Window.Show();
        }

        internal void OnGUI()
        {
            Initialize();
            Header();
        }

        //internal ReplaceToolState State
        {
            //get => data.State;
            //private set => data.State = value;
        }

        private void Header()
        {
            //State = SettingHeader;
        }

        internal void OnSetHeader()
        {
            //State = SavingObjectsToReplace;
        }

        internal string SelectGameObjectsLabel => data.SelectGameObjectsLabel;

        internal void OnSavedObjectsToReplace()
        {
            //State = PrintingInformationOnSelection;
        }

        private bool HasReplaceObjectField
        {
            get => data.HasReplaceObjectField;
            set => data.HasReplaceObjectField = value;
        }

        internal bool CanInitializeReplaceObjectField => HasReplaceObjectField == false;

        internal void OnPropertyField()
        {
            HasReplaceObjectField = !HasReplaceObjectField;
        }

        internal void ApplyModifiedProperties()
        {
            //SerializedData.ApplyModifiedProperties();
            //State = None;
        }

        internal void OnViewedSelectedObjects()
        {
            //State = ReplacingSelectedObjects;
            //ReplaceSelectedObjects();
        }

        /*internal void OnGUI(bool hasScrollPosition)
        {
            GUI(hasScrollPosition);
        }*/
/*private Vector2? SelectObjectScrollPosition
{
    set => data.SelectObjectScrollPosition = value;
}*/
/*
        internal void OnBeginScrollView() //Vector2 selectObjectScrollPosition)
        {
            //SelectObjectScrollPosition = selectObjectScrollPosition;
            //State = ViewingSelectedObjects;
        }

        /*internal UnityGameObject[] ObjectsToReplace
        {
            get => data.ObjectsToReplace;
            private set => data.ObjectsToReplace = value;
        }*/

//internal int ObjectsToReplaceAmount => ObjectsToReplace?.Length ?? 0;
/*
        internal void OnPrintedInformationOnSelection()
        {
            //State = BeginningScrollView;
        }

        internal bool HasScrollPosition => data.SelectObjectScrollPosition != null;
        //private bool ViewedSelectedGameObjects => HasScrollPosition ? ViewingSelectedObjects() : ScrollPositionError;

        /*private void ViewSelectedObjects()
        {
            if (!ViewedSelectedGameObjects) throw new InvalidOperationException(CannotViewSelectedObjects);
        }*/

//internal bool CanProcessSelectedGameObjects => ObjectsToReplace != null;
//private bool EnabledOnObjectsProcessed => CanProcessSelectedGameObjects && ProcessedSelectedGameObjects();
/*private bool ViewingSelectedObjects()
{
    EnableGUI();
    EndScrollView();
    indentLevel--;
    Separator();
    return true;
}*/
/*private static void EndScrollView()
{
    EditorGUILayout.EndScrollView();
}*/
/*private void EnableGUI()
{
    enabled = true;
    enabled = EnabledOnObjectsProcessed;
}*/

//private static bool ScrollPositionError => throw new InvalidOperationException(CannotSetScrollPosition);
/*private bool ProcessedSelectedGameObjects()
{
    foreach (var @object in ObjectsToReplace) ObjectField(@object, typeof(UnityGameObject), true);
    return true;
}*/

//private bool CanReplaceSelectedObjects => Button(ReplaceButton) && !LoggedErrors;
//private UnityGameObject ReplacementPrefab => data.ReplacementPrefab;
/*
        internal void ReplaceSelectedObjects()
        {
            //ReplaceSelectedObjects(ObjectsToReplace, ReplacementPrefab);
            //State = ApplyingModifiedProperties;
        }

        //private bool HasSerializedData => SerializedData != null;
        //private bool CanUpdateSerializedData => SerializedData.UpdateIfRequiredOrScript();
        //internal bool CanRepaint => HasSerializedData && CanUpdateSerializedData;
        //private static SelectionMode ObjectFilter => Unfiltered ^ ~(Assets | DeepAssets | Deep);
        //private static IEnumerable<Transform> SelectedTransforms => GetTransforms(ObjectFilter);

        internal void OnSelectionChange()
        {
            Initialize();
            //ObjectsToReplace = SelectedTransforms.Select(s => s.gameObject).ToArray();
        }

        private int[] GameObjectsInstances
        {
            get => data.GameObjectsInstances;
            set => data.GameObjectsInstances = value;
        }

        internal void ReplaceSelectedObjects(UnityGameObject[] objectsToReplace, UnityGameObject replacementPrefab)
        {
            GameObjectsInstances = new int[objectsToReplace.Length];
            //ObjectsToReplaceIndex = 0;
            ProcessObjectsToReplace();
            instanceIDs = GameObjectsInstances;
        }

        // private int ObjectsToReplaceIndex
        //  {
        //set => data.ObjectsToReplaceIndex = value;
        //  }

        //private bool ProcessingObjectsToReplace => ObjectsToReplaceIndex < ObjectsToReplaceAmount;

        private void ProcessObjectsToReplace()
        {
            /*for (ObjectsToReplaceIndex = 0; ProcessingObjectsToReplace; ObjectsToReplaceIndex++)
            {
                GameObjectInstance = objectsToReplace[ObjectsToReplaceIndex];
                RegisterCompleteObjectUndo(GameObjectInstance, SavingGameObjectState);
                ReplacementPrefabInstance = InstantiatePrefab(replacementPrefab) as UnityGameObject;
                if (CanSetGameObjectInstance)
                    GameObjectsInstances[ObjectsToReplaceIndex] = ReplacementPrefabInstance.GetInstanceID();
                ProcessReplacementPrefabInstance();
            }*/
/*
        }

        public void Dispose()
        {
            Dispose(true);
            SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (!dispose) return;
            data.Dispose();
        }

        ~ReplaceToolModelView()
        {
            Dispose(false);
        }
    }
}*/
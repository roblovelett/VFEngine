using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.ReplaceTool.Data;
using VFEngine.Tools.GameObject.Editor.ReplaceTool.Data.ScriptableObjects;
using Object = UnityEngine.Object;
using UnityGameObject = UnityEngine.GameObject;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable RedundantEmptyObjectCreationArgumentList
namespace VFEngine.Tools.GameObject.Editor.ReplaceTool
{
    using static ScriptableObject;
    using static PrefabUtility;
    using static Selection;
    using static SelectionMode;
    using static Undo;
    using static EditorGUI;
    using static EditorGUILayout;
    using static EditorStyles;
    using static Debug;
    using static GUI;
    using static GUILayout;
    using static ReplaceToolText;

    internal class ReplaceToolModel
    {
        private ReplaceToolData data;

        private int NewGameObjectsIndex
        {
            get => data.NewGameObjectsIndex;
            set => data.NewGameObjectsIndex = value;
        }

        private int[] NewGameObjectInstances
        {
            get => data.NewGameObjectInstances;
            set => data.NewGameObjectInstances = value;
        }

        private Transform[] Selected
        {
            get => data.Selected;
            set => data.Selected = value;
        }

        private Vector2? SelectObjectScrollPosition
        {
            get => data.SelectObjectScrollPosition;
            set => data.SelectObjectScrollPosition = value;
        }

        private EditorWindow Window
        {
            get => data.Window;
            set => data.Window = value;
        }

        private UnityGameObject NewGameObject
        {
            get => data.NewGameObject;
            set => data.NewGameObject = value;
        }

        private UnityGameObject NewGameObjectInstance
        {
            get => data.NewGameObjectInstance;
            set => data.NewGameObjectInstance = value;
        }

        private UnityGameObject[] GameObjects
        {
            get => data.DataSO.GameObjects;
            set => data.DataSO.GameObjects = value;
        }

        private UnityGameObject[] NewGameObjects
        {
            set => GameObjects = value;
        }

        private SerializedObject SerializedData
        {
            get => data.SerializedData;
            set => data.SerializedData = value;
        }

        private SerializedProperty ReplaceObjectField
        {
            get => data.ReplaceObjectField;
            set => data.ReplaceObjectField = value;
        }

        private Transform NewGameObjectTransform
        {
            get => NewGameObject.transform;
            set => InitializedNewGameObjectTransform(value);
        }

        private ReplaceToolDataSO DataSO
        {
            get => data.DataSO;
            set => data.DataSO = value;
        }

        private int GameObjectsLength => GameObjects.Length;
        private int ObjectsToReplaceCount => GameObjects != null ? GameObjectsLength : 0;
        private int NewGameObjectSiblingIndex => NewGameObject.transform.GetSiblingIndex();
        private bool CanInitializeReplaceToolData => data == null;
        private bool CanPrintInformation => ObjectsToReplaceCount == 0;
        private bool CanProcessSelectedGameObjects => GameObjects != null;
        private bool EnabledOnObjectsProcessed => CanProcessSelectedGameObjects && ProcessedSelectedGameObjects();
        private bool CanReplaceSelectedObjects => Button(ReplaceButton) && !LoggedErrors;
        private bool LoggedErrors => LoggedNoPrefabToReplaceError() || LoggedObjectToReplaceError();
        private bool UpdateSerializedData => SerializedData != null && CanRepaint;
        private bool CanRepaint => SerializedData.UpdateIfRequiredOrScript();
        private bool HasNewGameObjectSiblingIndex => NewGameObjectSiblingIndex >= 0;
        private bool ProcessingNewGameObjects => NewGameObjectsIndex < ObjectsToReplaceCount;
        internal bool OnInspectorUpdate => UpdateSerializedData;
        private bool CanInitializeSerializedData => SerializedData == null;
        private bool InitializeReplaceObjectField => ReplaceObjectField == null;
        private bool CanInitializeDataSO => DataSO == null;
        private bool CanSelectObjectScrollPosition => SelectObjectScrollPosition != null;
        private bool ViewedSelectedGameObjects => HasScrollPosition() ? ViewingSelectedObjects() : ScrollPositionError;
        private UnityGameObject Prefab => data.DataSO.Prefab;
        private UnityGameObject[] SelectedGameObjects => Selected.Select(s => s.gameObject).ToArray();
        private static SelectionMode ObjectFilter => Unfiltered ^ ~(Assets | DeepAssets | Deep);
        private static bool ScrollPositionError => throw new InvalidOperationException(CannotSetScrollPosition);

        private void InitializeData()
        {
            if (CanInitializeDataSO) InitializeDataSO();
            if (CanInitializeSerializedData) InitializeSerializedData();
            if (InitializeReplaceObjectField) ReplaceObjectField = SerializedData.FindProperty(ReplaceToolText.Prefab);
        }

        private void InitializeDataSO()
        {
            DataSO = CreateInstance<ReplaceToolDataSO>();
            SerializedData = null;
        }

        private void InitializeSerializedData()
        {
            SerializedData = new SerializedObject(DataSO);
            ReplaceObjectField = null;
        }

        private void Header()
        {
            Initialize();
            Separator();
            PropertyField(ReplaceObjectField);
            Separator();
            LabelField(HeaderText, boldLabel);
            Separator();
        }

        private void SaveObjectsToReplace()
        {
            IntField(ObjectCount, ObjectsToReplaceCount);
            indentLevel++;
        }

        private void PrintInformationOnSelection()
        {
            if (!CanPrintInformation) return;
            Separator();
            LabelField(SelectGameObjectsLabel, wordWrappedLabel);
        }

        private void ViewSelectedObjects()
        {
            if (!ViewedSelectedGameObjects) throw new InvalidOperationException(CannotViewSelectedObjects);
        }

        private bool ViewingSelectedObjects()
        {
            GUI();
            EditorGUILayout.EndScrollView();
            indentLevel--;
            Separator();
            return true;
        }

        private bool HasScrollPosition()
        {
            if (CanSelectObjectScrollPosition)
            {
                InitializeSelectObjectScrollPosition();
                return true;
            }

            SelectObjectScrollPosition = new Vector2();
            InitializeSelectObjectScrollPosition();
            return true;
        }

        private void InitializeSelectObjectScrollPosition()
        {
            System.Diagnostics.Debug.Assert(SelectObjectScrollPosition != null,
                nameof(SelectObjectScrollPosition) + NotNull);
            SelectObjectScrollPosition = EditorGUILayout.BeginScrollView((Vector2) SelectObjectScrollPosition);
        }

        private void ReplaceSelectedObjects()
        {
            if (!CanReplaceSelectedObjects) return;
            ReplaceSelectedObjects(GameObjects, Prefab);
        }

        private void GUI()
        {
            enabled = true;
            enabled = EnabledOnObjectsProcessed;
        }

        private void ProcessGameObjects(IReadOnlyCollection<UnityGameObject> objectToReplace, Object replaceObject)
        {
            NewGameObjectInstances = new int[objectToReplace.Count];
            ProcessNewGameObjects(replaceObject);
        }

        private void ProcessNewGameObjects(Object replaceObject)
        {
            for (NewGameObjectsIndex = 0; ProcessingNewGameObjects; NewGameObjectsIndex++)
            {
                NewGameObject = GameObjects[NewGameObjectsIndex];
                ProcessTemporaryNewGameObjectData(replaceObject);
            }
        }

        private bool ProcessedSelectedGameObjects()
        {
            ProcessSelectedGameObjects();
            return true;
        }

        private void ProcessSelectedGameObjects()
        {
            foreach (var gameObject in GameObjects) ObjectField(gameObject, typeof(UnityGameObject), true);
        }

        private bool LoggedNoPrefabToReplaceError()
        {
            if (ReplaceObjectField.objectReferenceValue) return false;
            LogErrorFormat(ErrorFormat, NoPrefab);
            return true;
        }

        private bool LoggedObjectToReplaceError()
        {
            if (GameObjectsLength != 0) return false;
            LogErrorFormat(ErrorFormat, NoGameObject);
            return true;
        }

        private void InitializedNewGameObjectTransform(Transform transform)
        {
            if (transform != NewGameObjectInstanceTransform)
                throw new InvalidOperationException(CannotSetGameObjectTransform);
            InitializeNewGameObjectInstanceTransform(transform);
        }

        private RectTransform NewGameObjectInstanceTransform => new RectTransform()
        {
            position = NewGameObjectTransform.position,
            rotation = NewGameObjectTransform.rotation,
            parent = NewGameObjectTransform.parent,
            localScale = NewGameObjectTransform.localScale
        };

        private void InitializeNewGameObjectInstanceTransform(Transform rectTransform)
        {
            NewGameObjectInstance.transform.position = rectTransform.position;
            NewGameObjectInstance.transform.rotation = rectTransform.rotation;
            NewGameObjectInstance.transform.parent = rectTransform.parent;
            NewGameObjectInstance.transform.localScale = rectTransform.localScale;
            if (HasNewGameObjectSiblingIndex)
                NewGameObjectInstance.transform.SetSiblingIndex(NewGameObjectSiblingIndex);
        }

        private void ProcessTemporaryNewGameObjectData(Object replaceObject)
        {
            RegisterCompleteObjectUndo(NewGameObject, ProcessingNewGameObjectState);
            ProcessNewGameObjectInstances(replaceObject);
            ProcessTransformsInNewGameObject();
            DestroyObjectImmediate(NewGameObject);
        }

        private bool HasNewGameObjectInstance => NewGameObjectInstance != null;

        private void ProcessNewGameObjectInstances(Object replaceObject)
        {
            NewGameObjectInstance = InstantiatePrefab(replaceObject) as UnityGameObject;
            if (HasNewGameObjectInstance)
            {
                NewGameObjectInstances[NewGameObjectsIndex] = NewGameObjectInstance.GetInstanceID();
                Selected[NewGameObjectInstances[NewGameObjectsIndex]] = NewGameObjectInstance.transform;
                NewGameObjectTransform = NewGameObjectInstanceTransform;
                RegisterFullObjectHierarchyUndo(NewGameObjectInstance, CompletedNewGameObjectReplacement);
            }
            else
            {
                throw new InvalidOperationException(CannotInstantiateNewGameObject);
            }
        }

        private void ProcessTransformsInNewGameObject()
        {
            foreach (Transform childTransform in NewGameObject.transform)
                SetTransformParent(childTransform, NewGameObjectInstance.transform, ParentChange);
        }

        internal ReplaceToolModel()
        {
            Initialize();
        }

        internal void Initialize()
        {
            if (CanInitializeReplaceToolData) data = new ReplaceToolData();
            else InitializeData();
        }

        internal void ShowWindow(ReplaceToolController window)
        {
            Window = window;
            Window.Show();
        }

        internal void OnGUI()
        {
            Header();
            SaveObjectsToReplace();
            PrintInformationOnSelection();
            ViewSelectedObjects();
            ReplaceSelectedObjects();
            Separator();
            SerializedData.ApplyModifiedProperties();
        }

        internal bool OnSelectionChange()
        {
            Initialize();
            Selected = GetTransforms(ObjectFilter);
            GameObjects = SelectedGameObjects;
            return CanRepaint;
        }

        internal void ReplaceSelectedObjects(UnityGameObject[] objectToReplace, UnityGameObject replaceObject)
        {
            NewGameObjects = new UnityGameObject[objectToReplace.Length];
            ProcessGameObjects(objectToReplace, replaceObject);
        }
    }
}
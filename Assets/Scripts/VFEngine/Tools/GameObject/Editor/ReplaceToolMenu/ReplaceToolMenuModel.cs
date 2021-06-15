using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VFEngine.Tools.GameObject.Editor.ReplaceToolMenu.Data;
using VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.ReplaceToolMenu
{
    using static EditorApplication;
    using static EditorWindow;
    using static Event;
    using static GUIUtility;

    internal class ReplaceToolMenuModel
    {
        private ReplaceToolMenuData data;
        private bool CanInitializeData => data == null;

        internal ReplaceToolMenuModel(Type sceneHierarchyWindow, EditorWindow focusedWindow)
        {
            Initialize(sceneHierarchyWindow, focusedWindow);
        }

        internal void Initialize(Type hierarchyType, EditorWindow focusedWindow)
        {
            if (CanInitializeData) data = new ReplaceToolMenuData(hierarchyType, focusedWindow);
            else InitializeDefault(hierarchyType, focusedWindow);
        }

        private Type HierarchyType
        {
            get => data.HierarchyType;
            set => data.HierarchyType = value;
        }

        private EditorWindow FocusedWindow
        {
            get => data.FocusedWindow;
            set => data.FocusedWindow = value;
        }

        private void InitializeDefault(Type hierarchyType, EditorWindow focusedWindow)
        {
            HierarchyType = hierarchyType;
            update += TrackFocusedHierarchy;
            Initialize(focusedWindow);
        }

        private void Initialize(EditorWindow focusedWindow)
        {
            FocusedWindow = focusedWindow;
        }

        private bool CanInitializeFocusedWindow => FocusedWindow != focusedWindow;
        private bool CanTrackFocusedHierarchy => FocusedWindow != null && FocusedWindow.GetType() == HierarchyType;
        private bool CanHandleGUI => HierarchyType != null;

        private IMGUIContainer HierarchyGUI
        {
            get => data.HierarchyGUI;
            set => data.HierarchyGUI = value;
        }

        private void TrackFocusedHierarchy()
        {
            if (!CanInitializeFocusedWindow) return;
            InitializeFocusedWindow();
        }

        private void InitializeFocusedWindow()
        {
            Initialize(focusedWindow);
            OnTrackFocusedHierarchy();
        }

        private void OnTrackFocusedHierarchy()
        {
            if (!CanTrackFocusedHierarchy) return;
            HandleGUI();
        }

        private void HandleGUI()
        {
            if (!CanHandleGUI) return;
            HierarchyGUI.onGUIHandler -= OnFocusedHierarchyGUI;
            HierarchyGUI = focusedWindow.rootVisualElement.parent.Query<IMGUIContainer>();
            HierarchyGUI.onGUIHandler += OnFocusedHierarchyGUI;
        }

        internal bool ReplaceSelectionValidate(UnityGameObject[] gameObjects)
        {
            return gameObjects.Length > 0;
        }

        internal bool HasExecuted
        {
            get => data.HasExecuted;
            private set => data.HasExecuted = value;
        }

        private Vector2 MousePosition
        {
            get => data.MousePosition;
            set => data.MousePosition = value;
        }

        private void OnFocusedHierarchyGUI()
        {
            MousePosition = GUIToScreenPoint(current.mousePosition);
        }

        private Rect SearchPopup => new Rect(MousePosition, new Vector2(240, 360));

        private ReplacePrefabSearchPopUpController ReplacePrefabSearchPopUpController =>
            data.ReplacePrefabSearchPopUpController;

        internal void ReplaceSelection()
        {
            ReplacePrefabSearchPopUpController.Show(SearchPopup);
            delayCall += () => HasExecuted = false;
        }
    }
}
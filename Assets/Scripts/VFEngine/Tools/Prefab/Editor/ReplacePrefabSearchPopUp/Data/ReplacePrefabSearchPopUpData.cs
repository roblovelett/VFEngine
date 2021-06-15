using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.GameObjectPreview;
using VFEngine.Tools.GameObject.Editor.ReplaceTool;
using VFEngine.Tools.Prefab.Editor.PrefabSelectionTreeView;
using VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.ScriptableObjects;
using UnityGameObject = UnityEngine.GameObject;
using ReplacePrefabSearchPopUpModel = VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Model;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Data
{
    using static ScriptableObject;

    internal class ReplacePrefabSearchPopUpData
    {
        private Rect layout;
        internal SearchField SearchField { get; private set; }
        internal ReplacePrefabSearchPopUpModel Model { get; private set; }
        internal ReplaceToolController ReplaceToolController { get; private set; }
        internal GameObjectPreviewController SelectionPreview { get; private set; }
        internal bool CloseWindow { get; set; }
        internal ReplacePrefabSearchPopUpStyles Styles { get; set; }
        internal EditorWindow FocusedWindow { get; set; }
        internal ReplacePrefabSearchPopUpSO ViewState { get; set; }
        internal UnityGameObject[] SelectedGameObjects { get; set; }
        internal ReplacePrefabSearchPopUpWindow WindowProperties { get; set; }
        internal PrefabSelectionTreeViewController Tree { get; set; }
        internal ReplacePrefabSearchPopUpController Window { get; set; }
        internal ReplacePrefabSearchPopUpController[] Windows { get; set; }
        internal ReplacePrefabSearchPopUpController Controller { get; set; }

        internal float LayoutX
        {
            get => layout.x;
            set => layout.x = value;
        }

        internal float LayoutY
        {
            get => layout.y;
            set => layout.y = value;
        }

        internal float LayoutWidth
        {
            get => layout.width;
            set => layout.width = value;
        }

        internal float LayoutHeight
        {
            get => layout.height;
            set => layout.height = value;
        }

        internal string SearchString
        {
            get => Tree.searchString;
            set => Tree.searchString = value;
        }

        internal Vector2 StartPosition
        {
            get => WindowProperties.StartPosition;
            set => WindowProperties.InitializeStartPosition(value);
        }

        internal Vector2 StartSize
        {
            get => WindowProperties.StartSize;
            set => WindowProperties.InitializeStartSize(value);
        }

        internal Vector2 NewSize
        {
            get => WindowProperties.NewSize;
            set => WindowProperties.InitializeNewSize(value);
        }

        internal Vector2 LastSize
        {
            get => WindowProperties.LastSize;
            set => WindowProperties.InitializeLastSize(value);
        }

        internal Rect Layout
        {
            get => layout;
            set => layout = value;
        }

        internal Rect Position
        {
            get => WindowProperties.Position;
            set => WindowProperties.InitializePosition(value);
        }

        private void Initialize()
        {
            Controller = null;
            Model = null;
            Styles = new ReplacePrefabSearchPopUpStyles();
            SearchField = new SearchField();
            ViewState = CreateInstance<ReplacePrefabSearchPopUpSO>();
            SelectionPreview = new GameObjectPreviewController();
            Tree = null;
            Windows = null;
            Window = null;
            WindowProperties = new ReplacePrefabSearchPopUpWindow();
            FocusedWindow = null;
            CloseWindow = new bool();
            layout = new Rect();
            ReplaceToolController = CreateInstance<ReplaceToolController>();
            SelectedGameObjects = null;
        }

        internal ReplacePrefabSearchPopUpData()
        {
            Initialize();
        }

        internal void Initialize(ReplacePrefabSearchPopUpController controller, ReplacePrefabSearchPopUpModel model)
        {
            Initialize();
            Controller = controller;
            Model = model;
        }
    }
}
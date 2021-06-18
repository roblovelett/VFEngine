using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.ReplaceTool;
using UnityGameObject = UnityEngine.GameObject;
//using Controller = VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.ReplacePrefabSearchPopUpController;
//using ModelView = VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.ReplacePrefabSearchPopUpModelView;
//using TreeViewController = VFEngine.Tools.Prefab.Editor.PrefabSelectionTreeView.PrefabSelectionTreeViewController;
//using SelectionPreviewController = VFEngine.Tools.GameObject.Editor.GameObjectPreview.GameObjectPreviewController;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Data
{
    using static EditorStyles;
    using static FontStyle;
    using static GC;

    /*internal sealed class ReplacePrefabSearchPopUpData : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (!dispose) return;
            ModelView.Dispose();
            ReplaceToolController.Close();
        }

        ~ReplacePrefabSearchPopUpData()
        {
            Dispose(false);
        }

        private Rect layout;
        internal Controller Controller { get; private set; }
        internal ModelView ModelView { get; private set; }
        internal SearchField SearchField { get; private set; }
        internal SelectionPreviewController SelectionPreview { get; private set; }
        internal ReplaceToolController ReplaceToolController { get; private set; }
        internal TreeViewState TreeViewState { get; private set; }
        internal TreeViewController TreeViewController { get; private set; }
        internal bool CanInitializeWindow { get; set; }
        internal Rect Position { get; set; }
        internal Vector2 StartPosition { get; set; }
        internal Vector2 StartSize { get; set; }
        internal Vector2 NewSize { get; set; }
        internal Vector2 LastSize { get; set; }

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

        internal Rect Layout
        {
            get => layout;
            set => layout = value;
        }

        internal GUIStyle HeaderLabel { get; private set; }

        internal ReplacePrefabSearchPopUpData(Controller controller, ModelView modelView,
            ReplaceToolController replaceToolController)
        {
            Initialize(controller, modelView, replaceToolController);
        }

        internal ReplacePrefabSearchPopUpData(ModelView modelView)
        {
            Initialize(modelView);
        }

        private void Initialize(Controller controller, ModelView modelView, ReplaceToolController replaceToolController)
        {
            Initialize(controller);
            Initialize(modelView);
            Initialize(replaceToolController);
            Initialize();
        }

        private void Initialize()
        {
            layout = new Rect();
            StartPosition = new Vector2();
            StartSize = new Vector2();
            NewSize = new Vector2();
            LastSize = new Vector2();
            Position = new Rect();
            SearchField = new SearchField();
            TreeViewState = new TreeViewState();
            TreeViewController = new TreeViewController(TreeViewState);
            HeaderLabel = new GUIStyle(centeredGreyMiniLabel) {fontSize = 11, fontStyle = Bold};
            SelectionPreview = new SelectionPreviewController();
        }

        private void Initialize(Controller controller)
        {
            Controller = controller;
        }

        private void Initialize(ModelView modelView)
        {
            ModelView = modelView;
        }

        private void Initialize(ReplaceToolController replaceToolController)
        {
            ReplaceToolController = replaceToolController;
        }
    }*/
}
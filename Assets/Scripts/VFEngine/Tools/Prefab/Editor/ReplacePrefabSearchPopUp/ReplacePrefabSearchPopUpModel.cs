using System;
using System.IO;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.GameObjectPreview;
using VFEngine.Tools.GameObject.Editor.ReplaceTool;
using VFEngine.Tools.Prefab.Editor.PrefabSelectionTreeView;
using VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Data;
using VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.ScriptableObjects;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp
{
    using static ScriptableObject;
    using static File;
    using static String;
    using static AssetPreview;
    using static Event;
    using static EventType;
    using static GUILayout;
    using static GUILayoutUtility;
    using static KeyCode;
    using static Resources;
    using static ReplacePrefabSearchPopUpText;
    using static EditorJsonUtility;

    internal class Model
    {
        private ReplacePrefabSearchPopUpData data;

        internal Model()
        {
            if (!Initialized()) throw new InvalidOperationException(CannotInitializeModel);
        }

        private bool Initialized()
        {
            data ??= new ReplacePrefabSearchPopUpData();
            return true;
        }

        private string ModelName => data.Model.ToString();
        private string ControllerName => data.Controller.name;
        private string ErrorController => $"{CannotInitializeModelText}{WithProperties}{ControllerName}";
        private string ErrorModel => $"{_}{ModelName}{InConstructor}";
        private string Error => $"{ErrorController}{ErrorModel}";

        internal void Initialize(ReplacePrefabSearchPopUpController controller, Model model)
        {
            if (!Initialized(controller, model)) throw new InvalidOperationException($"{Error}");
        }

        private PrefabSelectionTreeViewController Tree
        {
            get => data.Tree;
            set => data.Tree = value;
        }

        private int SelectedID => data.Tree.state.selectedIDs[0];
        private bool HasSelection => data.Tree.state.selectedIDs.Count > 0;

        private ReplacePrefabSearchPopUpController[] Windows
        {
            get => data.Windows;
            set => data.Windows = value;
        }

        private ReplacePrefabSearchPopUpController Window
        {
            get => data.Window;
            set => data.Window = value;
        }

        private UnityGameObject Instance => EditorUtility.InstanceIDToObject(SelectedID) as UnityGameObject;
        private static string AssetPath => ReplacePrefabSearchPopUpText.AssetPath;

        private ReplacePrefabSearchPopUpController Controller
        {
            get => data.Controller;
            set => data.Controller = value;
        }

        private static Event Event => current;
        private static float PreviewHeight => 128;

        private ReplacePrefabSearchPopUpStyles Styles
        {
            get => data.Styles;
            set => data.Styles = value;
        }

        private SearchField SearchField => data.SearchField;

        private ReplacePrefabSearchPopUpSO ViewState
        {
            get => data.ViewState;
            set => data.ViewState = value;
        }

        private bool Initialized(ReplacePrefabSearchPopUpController controller, Model model)
        {
            data.Initialize(controller, model);
            return true;
        }

        internal void Show(Rect rect, Rect windowPosition)
        {
            Windows = FindObjectsOfTypeAll<ReplacePrefabSearchPopUpController>();
            Window = Windows.Length != 0 ? Windows[0] : CreateInstance<ReplacePrefabSearchPopUpController>();
            WindowProperties = new ReplacePrefabSearchPopUpWindow(rect.position, rect.size, windowPosition);
            Window.Initialize();
        }

        private TreeViewState TreeViewState => ViewState.TreeViewState;
        private static bool OverwriteAssetPathWithViewState => Exists(AssetPath);
        private int RowsAmount => Tree.GetRows().Count;

        private Vector2 StartPosition
        {
            get => data.StartPosition;
            set => data.StartPosition = value;
        }

        private Vector2 StartSize
        {
            get => data.StartSize;
            set => data.StartSize = value;
        }

        private Rect Position
        {
            get => data.Position;
            set => data.Position = value;
        }

        private UnityGameObject[] SelectedGameObjects
        {
            get => data.SelectedGameObjects;
            set => data.SelectedGameObjects = value;
        }

        internal void Initialize(UnityGameObject[] gameObjects)
        {
            SelectedGameObjects = gameObjects;
            ViewState = CreateInstance<ReplacePrefabSearchPopUpSO>();
            if (OverwriteAssetPathWithViewState) FromJsonOverwrite(ReadAllText(AssetPath), ViewState);
            Tree = new PrefabSelectionTreeViewController(TreeViewState);
            Tree.OnSelectEntry += OnSelectEntry;
            SetPreviewTextureCacheSize(RowsAmount);
            SearchField.downOrUpArrowKeyPressed += Tree.SetFocusAndEnsureSelectedItem;
            SearchField.SetFocus();
            Window.StartPosition(StartPosition);
            Window.StartSize(StartSize);
            Window.Position(Position);
            Window.InitialSize();
            Window.ShowPopup();
        }

        private ReplaceToolController ReplaceToolController => data.ReplaceToolController;

        private void OnSelectEntry(UnityGameObject prefab)
        {
            ReplaceToolController.ReplaceSelectedObjects(SelectedGameObjects, prefab);
        }

        internal void OnDisable()
        {
            Tree.Cleanup();
        }

        internal void Close()
        {
            SaveState();
        }

        private void SaveState()
        {
            WriteAllText(AssetPath, ToJson(ViewState));
        }

        private static bool KeyboardCloseRequest => Event.type == KeyDown && Event.keyCode == Escape;
        private bool CanResetSearchField => KeyboardCloseRequest && Tree.hasSearch;

        private string SearchString
        {
            get => data.SearchString;
            set => data.SearchString = value;
        }

        internal bool CloseWindow
        {
            get => data.CloseWindow;
            private set => data.CloseWindow = value;
        }

        private EditorWindow FocusedWindow
        {
            get => data.FocusedWindow;
            set => data.FocusedWindow = value;
        }

        private bool WindowNotInFocus => FocusedWindow != Controller;
        private bool CanInitializeStyles => Styles == null;

        internal void OnGUI(EditorWindow focusedWindow, ReplacePrefabSearchPopUpController controller)
        {
            CloseWindow = false;
            Controller = controller;
            FocusedWindow = focusedWindow;
            if (CanResetSearchField) ResetSearchField();
            if (WindowNotInFocus) CloseWindow = true;
            if (CanInitializeStyles) Styles = new ReplacePrefabSearchPopUpStyles();
            Toolbar();
            TreeView();
            PreviewSelected();
        }

        private void ResetSearchField()
        {
            SearchString = Empty;
            CloseWindow = true;
        }

        private string SearchStringUpdated => SearchField.OnToolbarGUI(SearchString);
        private GUIStyle HeaderLabel => Styles.HeaderLabel;

        private void Toolbar()
        {
            SearchString = SearchStringUpdated;
            Label(ReplaceWith, HeaderLabel);
        }

        private Rect Layout
        {
            get => data.Layout;
            set => data.Layout = value;
        }

        private void TreeView()
        {
            InitializeLayout();
            Tree.OnGUI(Layout);
        }

        private float LayoutX
        {
            get => data.LayoutX;
            set => data.LayoutX = value;
        }

        private float LayoutY
        {
            get => data.LayoutY;
            set => data.LayoutY = value;
        }

        private float LayoutWidth
        {
            get => data.LayoutWidth;
            set => data.LayoutWidth = value;
        }

        private float LayoutHeight
        {
            get => data.LayoutHeight;
            set => data.LayoutHeight = value;
        }

        private void InitializeLayout()
        {
            Layout = GetRect(0, 10000, 0, 10000);
            LayoutX += 2;
            LayoutWidth -= 4;
            LayoutY += 2;
            LayoutHeight -= 4;
        }

        private bool CanPreviewSelection => HasSelection && Tree.CanRender(SelectedID);
        private Rect Preview => GetRect(Position.width, PreviewHeight);
        private GameObjectPreviewController SelectionPreview => data.SelectionPreview;

        private void PreviewSelected()
        {
            if (CanPreviewSelection)
            {
                InitializeNewSize(StartSize.x, StartSize.y + PreviewHeight);
                OnPreviewSelection();
            }
            else
            {
                InitializeNewSize(StartSize.x, StartSize.y);
            }
        }

        private void OnPreviewSelection()
        {
            SelectionPreview.CreatePreviewForTarget(Instance);
            SelectionPreview.RenderInteractivePreview(Preview);
            SelectionPreview.DrawPreviewTexture(Preview);
        }

        private bool InitializeNewSizeWithSelected => HasSelection && Tree.CanRender(SelectedID);

        internal void InitialSize()
        {
            if (InitializeNewSizeWithSelected) InitializeNewSize(StartSize.x, StartSize.y + PreviewHeight);
            else InitializeNewSize(StartSize.x, StartSize.y);
        }

        private Vector2 LastSize
        {
            get => data.LastSize;
            set => data.LastSize = value;
        }

        private bool WindowSizeChanged => NewSize != LastSize;

        private void InitializeNewSize(float width, float height)
        {
            NewSize = new Vector2(width, height);
            if (!WindowSizeChanged) return;
            AdjustSize();
            AdjustPosition(width, height);
        }

        private void AdjustSize()
        {
            LastSize = NewSize;
        }

        private void AdjustPosition(float width, float height)
        {
            Position = new Rect(Position.x, Position.y, width, height);
        }

        private Vector2 NewSize
        {
            get => data.NewSize;
            set => data.NewSize = value;
        }

        internal void InitializeStartPosition(Vector2 startPosition)
        {
            StartPosition = startPosition;
        }

        internal void InitializeStartSize(Vector2 startSize)
        {
            StartSize = startSize;
        }

        internal void InitializePosition(Rect position)
        {
            Position = position;
        }

        private ReplacePrefabSearchPopUpWindow WindowProperties
        {
            set => data.WindowProperties = value;
        }
    }
}
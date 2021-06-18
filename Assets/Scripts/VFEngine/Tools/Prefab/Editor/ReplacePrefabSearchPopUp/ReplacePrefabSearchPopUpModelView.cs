using System;
using System.IO;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.ReplaceTool;
//using ModelView = VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.ReplacePrefabSearchPopUpModelView;
using UnityGameObject = UnityEngine.GameObject;
//using SelectionPreviewController = VFEngine.Tools.GameObject.Editor.GameObjectPreview.GameObjectPreviewController;
//using ModelViewData = VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Data.ReplacePrefabSearchPopUpData;
//using Controller = VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.ReplacePrefabSearchPopUpController;
//using TreeViewController = VFEngine.Tools.Prefab.Editor.PrefabSelectionTreeView.PrefabSelectionTreeViewController;
//using Text = VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Data.ReplacePrefabSearchPopUpText;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp
{
    using static GC;
    using static Selection;
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
    //using static Text;
    using static EditorJsonUtility;

    /*internal sealed class ReplacePrefabSearchPopUpModelView : IDisposable
    {
        private ModelViewData data;
        private InvalidOperationException ModelViewError => new InvalidOperationException(Error);

        private bool Initialized(Controller controller,ReplaceToolController replaceToolController)
        {
            Initialize(controller, replaceToolController);
            return true;
        }

        internal ReplacePrefabSearchPopUpModelView()
        {
            if (!Initialized()) throw ModelViewError;
        }

        private bool Initialized()
        {
            data = new ModelViewData(this);
            return true;
        }
        
        internal ReplacePrefabSearchPopUpModelView(Controller controller, ReplaceToolController replaceToolController)//CreateInstance<ReplaceToolController>())
        {
            if (!Initialized(controller, replaceToolController)) throw ModelViewError;
        }

        private void Initialize(Controller controller, ReplaceToolController replaceToolController)
        {
            data = new ModelViewData(controller, this,replaceToolController);
        }

        private bool HasData => data != null;

        public void Dispose()
        {
            Dispose(true);
            SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (dispose) data.Dispose();
        }

        ~ReplacePrefabSearchPopUpModelView()
        {
            Dispose(false);
        }

        private string ModelName => data.ModelView.ToString();
        private string ControllerName => data.Controller.name;
        private string ErrorController => $"{CannotInitializeModelText}{WithProperties}{ControllerName}";
        private string ErrorModel => $"{_}{ModelName}{InConstructor}";
        private string Error => $"{ErrorController}{ErrorModel}";
        private int SelectedID => TreeViewController.state.selectedIDs[0];
        private bool HasSelection => TreeViewController.state.selectedIDs.Count > 0;
        private static Controller[] Windows => FindObjectsOfTypeAll<Controller>();
        private static Controller Window => Windows.Length != 0 ? Windows[0] : CreateInstance<Controller>();
        private UnityGameObject Instance => EditorUtility.InstanceIDToObject(SelectedID) as UnityGameObject;
        private static string AssetPath => Text.AssetPath;
        private static Event Event => current;
        private static float PreviewHeight => 128;
        private SearchField SearchField => data.SearchField;

        private bool CanInitializeWindow
        {
            get => data.CanInitializeWindow;
            set => data.CanInitializeWindow = value;
        }

        internal void Show(Rect rect)
        {
            CanInitializeWindow = !CanInitializeWindow;
            Initialize(rect);
        }

        private static bool OverwriteAssetPathWithViewState => Exists(AssetPath);
        private int RowsAmount => TreeViewController.GetRows().Count;

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

        private static UnityGameObject[] GameObjects => gameObjects;
        private TreeViewController TreeViewController => data.TreeViewController;
        private TreeViewState TreeViewState => data.TreeViewState;

        internal void Initialize()
        {
            if (!HasData) throw ModelViewError;
        }

        private void Initialize(Rect rect)
        {
            if (OverwriteAssetPathWithViewState) FromJsonOverwrite(ReadAllText(AssetPath), TreeViewState);
            TreeViewController.OnSelectEntry += OnSelectEntry;
            SetPreviewTextureCacheSize(RowsAmount);
            SearchField.downOrUpArrowKeyPressed += TreeViewController.SetFocusAndEnsureSelectedItem;
            SearchField.SetFocus();
            StartPosition = rect.position;
            StartSize = rect.size;
            Position = new Rect(StartPosition, StartSize);
            InitialSize();
            Window.ShowPopup();
        }

        private bool InitializeNewSizeWithSelected => HasSelection && TreeViewController.CanRender(SelectedID);

        private void InitialSize()
        {
            if (InitializeNewSizeWithSelected) InitializeNewSize(StartSize.x, StartSize.y + PreviewHeight);
            else InitializeNewSize(StartSize.x, StartSize.y);
        }

        private void InitializeNewSize(float width, float height)
        {
            NewSize = new Vector2(width, height);
            if (!WindowSizeChanged) return;
            AdjustSize();
            AdjustPosition(width, height);
        }

        private Vector2 NewSize
        {
            get => data.NewSize;
            set => data.NewSize = value;
        }

        private Vector2 LastSize
        {
            get => data.LastSize;
            set => data.LastSize = value;
        }

        private bool WindowSizeChanged => NewSize != LastSize;

        private void AdjustSize()
        {
            LastSize = NewSize;
        }

        private void AdjustPosition(float width, float height)
        {
            Position = new Rect(Position.x, Position.y, width, height);
        }

        private ReplaceToolController ReplaceToolController => data.ReplaceToolController;

        private void OnSelectEntry(UnityGameObject prefab)
        {
            //ReplaceToolController.ReplaceSelectedObjects(GameObjects, prefab);
        }

        internal void SaveState()
        {
            WriteAllText(AssetPath, ToJson(TreeViewState));
            TreeViewController.Cleanup();
            data.Dispose();
        }

        private GUIStyle HeaderLabel => data.HeaderLabel;
        private static bool KeyboardCloseRequest => Event.type == KeyDown && Event.keyCode == Escape;
        private bool CanResetSearchField => TreeViewController.hasSearch;

        internal bool CloseWindow(bool canCloseWindow)
        {
            return ClosedWindowOnEscapeKeyPressed || canCloseWindow;
        }

        private bool ClosedWindowOnEscapeKeyPressed => KeyboardCloseRequest && CannotFindSearchField();

        private bool CannotFindSearchField()
        {
            return !CanResetSearchField || ResetSearchField();
        }

        private bool ResetSearchField()
        {
            SearchString = Empty;
            return true;
        }

        private string SearchStringUpdated => SearchField.OnToolbarGUI(SearchString);

        private string SearchString
        {
            get => TreeViewController.searchString;
            set => TreeViewController.searchString = value;
        }

        internal void OnGUI()
        {
            Toolbar();
            TreeView();
            PreviewSelected();
        }

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
            TreeViewController.OnGUI(Layout);
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

        private bool CanPreviewSelection => HasSelection && TreeViewController.CanRender(SelectedID);
        private Rect Preview => GetRect(Position.width, PreviewHeight);
        private SelectionPreviewController SelectionPreview => data.SelectionPreview;

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
    }*/
}
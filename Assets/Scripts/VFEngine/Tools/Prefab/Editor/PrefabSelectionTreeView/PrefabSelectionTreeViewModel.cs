using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.GameObjectPreview;
using VFEngine.Tools.Prefab.Editor.PrefabSelectionTreeView.Data;
using UnityObject = UnityEngine.Object;
using UnityGameObject = UnityEngine.GameObject;

// ReSharper disable InconsistentNaming
namespace VFEngine.Tools.Prefab.Editor.PrefabSelectionTreeView
{
    //using static PrefabSelectionTreeViewText;
    using static Event;
    using static Graphics;
    using static GUI;
    using static RenderTexture;
    using static EditorGUIUtility;
    using static EditorStyles;
    using static EditorUtility;
    using static TreeView;
    using static KeyCode;
    using static UnityObject;
    using static Screen;
    using static Dictionary<int, RenderTexture>;
    using static AssetDatabase;
    using static PrefabAssetType;
    using static PrefabUtility;

    /*internal class PrefabSelectionTreeViewModel
    {
        private static PrefabSelectionTreeViewData data;
        private static bool CanInitializeData => data == null;
        internal List<TreeViewItem> Rows => data.Rows;
        private static Texture2D PrefabOnIconTexture => IconContent(PrefabOnIcon).image as Texture2D;
        private static Texture2D PrefabVariantOnIconTexture => IconContent(PrefabVariantOnIcon).image as Texture2D;
        private static Texture2D FolderIconTexture => IconContent(FolderIcon).image as Texture2D;
        private static Texture2D FolderOnIconTexture => IconContent(FolderOnIcon).image as Texture2D;
        private static Dictionary<int, RenderTexture> Cache => data.Cache;
        private static ValueCollection PreviewTextures => data.Cache.Values;

        internal PrefabSelectionTreeViewModel()
        {
            Initialize();
        }

        internal void Initialize()
        {
            if (CanInitializeData) data = new PrefabSelectionTreeViewData();
            else data.Initialize();
        }

        private static DoFoldoutCallback ExpandedState
        {
            get => data.ExpandedState;
            set => data.ExpandedState = value;
        }

        internal bool CanReload => data.CanReload;

        private static float PositionWidth
        {
            set => data.PositionWidth = value;
        }

        private static float PositionHeight
        {
            set => data.PositionHeight = value;
        }

        private static float PositionY
        {
            get => data.PositionY;
            set => data.PositionY = value;
        }

        internal static DoFoldoutCallback FoldoutOverride()
        {
            return DidFoldoutCallback() ? ExpandedState : CannotExpandStateError();
        }

        private static DoFoldoutCallback CannotExpandStateError()
        {
            throw new InvalidOperationException(ExpandStateError);
        }

        private static bool DidFoldoutCallback()
        {
            ExpandedState = Toggle();
            return ExpandedState != null;
        }

        private static DoFoldoutCallback Toggle()
        {
            PositionWidth = width;
            PositionHeight = 20;
            PositionY -= 2;
            return ExpandedState;
        }

        internal void Cleanup()
        {
            foreach (var previewTexture in PreviewTextures) DestroyImmediate(previewTexture);
        }

        private static HashSet<int> VisibleItems => data.VisibleItems;

        private int VisibleID
        {
            get => data.VisibleID;
            set => data.VisibleID = value;
        }

        private bool CanRenderID => VisibleItems.Contains(VisibleID);

        internal bool CanRender(int id)
        {
            VisibleID = id;
            return CanRenderID;
        }

        private UnityGameObject Prefab
        {
            get => data.Prefab;
            set => data.Prefab = value;
        }

        private static UnityObject Object
        {
            get => data.Object;
            set => data.Object = value;
        }

        private bool HasPrefab => Object is UnityGameObject @object && InitializedPrefab(@object);

        private bool InitializedPrefab(UnityGameObject gameObject)
        {
            Prefab = gameObject;
            return Prefab != null;
        }

        internal void DoubleClickedItem(UnityObject instanceIDToObject)
        {
            ObjectInstance = instanceIDToObject;
            OnDoubleClickedItem();
        }

        internal Action<UnityGameObject> OnSelectEntry
        {
            get => data.OnSelectEntry;
            set => data.OnSelectEntry = value;
        }

        private bool InitializedObjectToInstance()
        {
            Object = ObjectInstance;
            return Object != null;
        }

        private bool OnSelectedPrefab()
        {
            OnSelectEntry(Prefab);
            return true;
        }

        internal bool Expanded
        {
            get => data.Expanded;
            private set => data.Expanded = value;
        }

        private static KeyCode CurrentKeyPressed
        {
            get => data.CurrentKeyPressed;
            set => data.CurrentKeyPressed = value;
        }

        private static UnityObject ObjectInstance
        {
            get => data.ObjectInstance;
            set => data.ObjectInstance = value;
        }

        private static bool EnterKeyPressed => CurrentKeyPressed == KeypadEnter;
        private static bool ReturnKeyPressed => CurrentKeyPressed == Return;
        private static bool KeysAreDoubleClickedItem => EnterKeyPressed || ReturnKeyPressed;

        internal void KeyEvent(KeyCode key)
        {
            CurrentKeyPressed = key;
            if (KeysAreDoubleClickedItem) OnDoubleClickedItem();
        }

        private void OnDoubleClickedItem()
        {
            if (!SelectedPrefab) Expanded = true;
        }

        private bool SelectedPrefab => InitializedObjectToInstance() && HasPrefab && OnSelectedPrefab();

        internal int SelectedId
        {
            get => data.SelectedId;
            private set => data.SelectedId = value;
        }

        internal void SelectionChanged(bool isChanged, int selectedId)
        {
            if (!isChanged) return;
            SelectedId = selectedId;
        }

        internal TreeViewItem Root
        {
            get => data.Root;
            private set => data.Root = value;
        }

        private static HashSet<string> Paths => data.Paths;

        internal bool SetupParentsAndChildrenFromPathDepths
        {
            get => data.SetupParentsAndChildrenFromPathDepths;
            private set => data.SetupParentsAndChildrenFromPathDepths = value;
        }

        internal void BuildRoot()
        {
            Root = new TreeViewItem(0, -1);
            Rows.Clear();
            Paths.Clear();
            ProcessGameObjects();
            SetupParentsAndChildrenFromPathDepths = true;
        }

        private static string Path
        {
            get => data.Path;
            set => data.Path = value;
        }

        private static UnityGameObject Asset => LoadAssetAtPath<UnityGameObject>(Path);
        private static bool PathContainsAssets => PathSplits[0] == Assets;
        private static string[] PathSplits => Path.Split(PathSeparator);
        private static int PathDepth => PathSplits.Length - 2;

        private void ProcessGameObjects()
        {
            foreach (var currentGUID in FindAssets(FilterByPrefab))
            {
                Path = GUIDToAssetPath(currentGUID);
                if (!PathContainsAssets) break;
                AddFolders();
                AddPrefab();
            }
        }

        private static int CurrentFolder
        {
            get => data.CurrentFolder;
            set => data.CurrentFolder = value;
        }

        private static bool CanAddCurrentPathSplitToTreeViewItem => !Paths.Contains(CurrentPathSplit);
        private static string CurrentPathSplit => PathSplits[CurrentFolder];
        private static bool ProcessingFolders => CurrentFolder < PathSplits.Length - 1;

        private void AddFolders()
        {
            for (CurrentFolder = 1; ProcessingFolders; CurrentFolder++)
                if (CanAddCurrentPathSplitToTreeViewItem)
                    AddCurrentPathSplitToTreeViewItem();
        }

        private void AddCurrentPathSplitToTreeViewItem()
        {
            Rows.Add(new TreeViewItem(CurrentPathSplit.GetHashCode(), CurrentFolder - 1, _ + CurrentPathSplit)
            {
                icon = FolderOnIconTexture
            });
            Paths.Add(CurrentPathSplit);
        }

        private static int CurrentAssetInstanceID => Asset.GetInstanceID();
        private static GUIContent Content { get; } = new GUIContent(ObjectContent(Asset, Asset.GetType()));
        private static Texture2D Icon => Content.image as Texture2D;
        private static string Text => Content.text;

        private TreeViewItem NewTreeViewItem { get; } =
            new TreeViewItem(CurrentAssetInstanceID, PathDepth, Text) {icon = Icon};

        private static bool CanRenderAsset => ItemPreview.CanRenderAssets(Asset);

        private void AddPrefab()
        {
            if (CanRenderAsset) VisibleItems.Add(CurrentAssetInstanceID);
            Rows.Add(NewTreeViewItem);
        }

        private static bool HasSearch
        {
            get => data.HasSearch;
            set => data.HasSearch = value;
        }

        private bool NoRowHeight => HasPrefab && HasSearch;
        private float CustomRowHeight => NoRowHeight ? 0 : 20;

        internal float GetCustomRowHeight(UnityObject @object, bool hasSearch)
        {
            Object = @object;
            HasSearch = hasSearch;
            return CustomRowHeight;
        }

        private static GUIStyle WhiteLabel
        {
            get => data.WhiteLabel;
            set => data.WhiteLabel = value;
        }

        internal bool HasWhiteLabel => WhiteLabel != null;

        internal void OnGUI()
        {
            if (HasWhiteLabel) return;
            InitializeWhiteLabel();
        }

        private static void InitializeWhiteLabel()
        {
            WhiteLabel ??= new GUIStyle(label) {normal = {textColor = whiteLabel.normal.textColor}};
        }

        private static Rect RowRect
        {
            get => data.RowRect;
            set => data.RowRect = value;
        }

        private bool IsFolder => !HasPrefab;
        private static Event Event => current;
        private static Vector2 MousePosition => Event.mousePosition;
        private static bool MouseUp => Event.type == EventType.MouseUp;
        private static bool RectContainsMouseUpEvent => RowRect.Contains(MousePosition) && MouseUp;

        internal bool CanInitializeSelectionAndFocus
        {
            get => data.CanInitializeSelectionAndFocus;
            private set => data.CanInitializeSelectionAndFocus = value;
        }

        private bool CannotInitializeSelectionAndFocus => IsFolder && HasSearch;
        private bool InitializeSelectionAndFocus => IsFolder && RectContainsMouseUpEvent;

        internal void InitializeRowGUI(TreeViewItem item, Rect rowRect, bool hasSearch)
        {
            Initialize(hasSearch, item, rowRect);
            if (CannotInitializeSelectionAndFocus) return;
            if (InitializeSelectionAndFocus) CanInitializeSelectionAndFocus = true;
        }

        private static int CurrentID
        {
            get => data.CurrentID;
            set => data.CurrentID = value;
        }

        private static void Initialize(bool hasSearch, TreeViewItem item, Rect rowRect)
        {
            Object = InstanceIDToObject(item.id);
            CurrentID = item.id;
            HasSearch = hasSearch;
            RowRect = rowRect;
        }

        internal List<int> InitializedSelection { get; } = new List<int> {CurrentID};

        private static bool HasFocus
        {
            get => data.HasFocus;
            set => data.HasFocus = value;
        }

        private static GUIStyle LabelStyle => HasFocus ? WhiteLabel : label;

        private static float ContentIndent
        {
            get => data.ContentIndent;
            set => data.ContentIndent = value;
        }

        private static GUIContent ItemContent => data.ItemContent;

        private static float RowRectX
        {
            get => data.RowRectX;
            set => data.RowRectX = value;
        }

        private static float RowRectWidth
        {
            get => data.RowRectWidth;
            set => data.RowRectWidth = value;
        }

        private Rect IconRect { get; } = new Rect(RowRect) {width = 20};
        private PrefabAssetType PrefabAssetType => GetPrefabAssetType(Prefab);
        private Texture2D OnIcon => PrefabAssetType == Regular ? PrefabOnIconTexture : PrefabVariantOnIconTexture;

        private static Rect LabelRect
        {
            get => data.LabelRect;
            set => data.LabelRect = value;
        }

        private bool CanRenderCurrentID => CanRender(CurrentID);
        private static GameObjectPreviewController ItemPreview { get; } = new GameObjectPreviewController();
        internal bool CanCreatePreviewForTarget => CreatingPreviewForTarget && CacheHasPreviewTexture;
        private bool CreatingPreviewForTarget => HasPrefab && CanRenderCurrentID;

        private static bool CacheHasPreviewTexture => Cache.TryGetValue(CurrentID, out var previewTexture) &&
                                                      InitializedPreviewTexture(previewTexture);

        private static bool InitializedPreviewTexture(RenderTexture previewTexture)
        {
            PreviewTexture = previewTexture;
            return true;
        }

        internal void InitializeRowGUI(bool hasFocus, float contentIndent, string displayName)
        {
            HasFocus = hasFocus;
            ContentIndent = contentIndent;
            ItemContent.text = displayName;
            RowRectX += ContentIndent;
            RowRectWidth -= ContentIndent;
            LabelRect = new Rect(RowRect);
        }

        private bool HasOutputTexture => ItemPreview.OutputTexture != null;
        private Rect PreviewRect { get; } = new Rect(RowRect) {width = 32, height = 32};

        internal void CreatePreviewForTarget()
        {
            ItemPreview.CreatePreviewForTarget(Prefab);
            ItemPreview.RenderInteractivePreview(PreviewRect);
            if (HasOutputTexture) CachePreview();
        }

        internal void InitializeRowGUI()
        {
            if (!CreatedPreviewForTarget) throw new InvalidOperationException(CreatePreviewTextureError);
        }

        private bool CreatedPreviewForTarget => CreatingPreviewForTarget && CachedPreviewTexture;
        private bool CachedPreviewTexture => CanCachePreviewTexture ? CachedPreview() : CanPreviewTexture;
        private bool CanPreviewTexture => PreviewTexture == null;
        internal bool CanRepaint => CanPreviewTexture;

        private static bool CachedPreview()
        {
            CachePreview();
            return true;
        }

        private static bool CanCachePreviewTexture => OutputTexture;
        private static RenderTexture OutputTexture => ItemPreview.OutputTexture;
        private static RenderTexture CopiedTexture => new RenderTexture(OutputTexture);

        private static RenderTexture PreviousTexture
        {
            get => data.PreviousTexture;
            set => data.PreviousTexture = value;
        }

        private static void CachePreview()
        {
            PreviousTexture = active;
            Blit(OutputTexture, CopiedTexture);
            active = PreviousTexture;
            Cache.Add(CurrentID, CopiedTexture);
        }

        private static RenderTexture PreviewTexture
        {
            get => data.PreviewTexture;
            set => data.PreviewTexture = value;
        }

        private static bool CannotDrawTexture => PreviewTexture != null;

        private static float LabelRectX
        {
            get => data.LabelRectX;
            set => data.LabelRectX = value;
        }

        private static float LabelRectWidth
        {
            get => data.LabelRectWidth;
            set => data.LabelRectWidth = value;
        }

        internal void InitializeRowGUI(ScaleMode scaleMode)
        {
            if (CannotDrawTexture) return;
            DrawTexture(IconRect, PreviewTexture, scaleMode);
        }

        private Rect PrefabIconRect => new Rect(IconRect) {x = RowRect.xMax - 24};

        internal void InitializeRowGUI(string label, bool isSelected, Texture2D itemIcon)
        {
            InitializeLabel(label, isSelected, itemIcon);
        }

        private void InitializeLabel(string label, bool isSelected, Texture2D itemIcon)
        {
            InitializeLabelProperties(label);
            InitializePrefabIconRect(isSelected, itemIcon);
            InitializeItemIconAndLabel(isSelected, itemIcon);
            InitializeItemContentAndLabel();
        }

        private void InitializeItemContentAndLabel()
        {
            if (HasPrefab) return;
            ItemContent.image = HasFocus ? FolderOnIconTexture : FolderIconTexture;
            Label(RowRect, ItemContent, LabelStyle);
        }

        private void InitializeItemIconAndLabel(bool isSelected, Texture2D itemIcon)
        {
            ItemContent.image = isSelected ? OnIcon : itemIcon;
            Label(RowRect, ItemContent, LabelStyle);
        }

        private void InitializePrefabIconRect(bool isSelected, Texture2D itemIcon)
        {
            if (isSelected) Label(PrefabIconRect, HasFocus ? OnIcon : itemIcon);
        }

        private void InitializeLabelProperties(string label)
        {
            LabelRectX += IconRect.width;
            LabelRectWidth -= IconRect.width + 24;
            Label(LabelRect, label, LabelStyle);
        }
    }*/
}
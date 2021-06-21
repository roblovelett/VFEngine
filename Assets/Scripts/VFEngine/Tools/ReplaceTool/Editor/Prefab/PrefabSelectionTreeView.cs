using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.GameObjectPreview;
using Object = UnityEngine.Object;
using UnityGameObject = UnityEngine.GameObject;
using Text = VFEngine.Tools.ReplaceTool.Editor.Data.ReplaceToolText;

namespace VFEngine.Tools.ReplaceTool.Editor.Prefab
{
    using static KeyCode;
    using static EventType;
    using static ScaleMode;
    using static PrefabAssetType;
    using static EditorGUIUtility;
    using static GameObjectPreview;
    using static AssetDatabase;
    using static EditorStyles;
    using static EditorUtility;
    using static PrefabUtility;
    using static Event;
    using static Graphics;
    using static GUI;
    using static GUIContent;
    using static Object;
    using static RenderTexture;
    using static Screen;

    internal class PrefabSelectionTreeView : TreeView
    {
        private static readonly Texture2D OnIcon = IconContent(Text.OnIcon).image as Texture2D;
        private static readonly Texture2D VariantOnIcon = IconContent(Text.VariantOnIcon).image as Texture2D;
        private static readonly Texture2D FolderIcon = IconContent(Text.FolderIcon).image as Texture2D;
        private static readonly Texture2D FolderOnIcon = IconContent(Text.FolderOnIcon).image as Texture2D;
        private static int _prefabId;
        private static GUIStyle _whiteLabel;
        private readonly GUIContent itemContent = new GUIContent();
        private readonly GameObjectPreview itemPreview = new GameObjectPreview();
        private readonly HashSet<int> visibleItems = new HashSet<int>();
        private readonly HashSet<string> paths = new HashSet<string>();
        private readonly List<TreeViewItem> rows = new List<TreeViewItem>();
        private readonly Dictionary<int, RenderTexture> previewCache = new Dictionary<int, RenderTexture>();
        private int pathSplitItem;
        private string prefabGuid;
        private IReadOnlyList<string> pathSplits;
        private IList<int> selectedIds;
        private Rect rowRect;
        private Rect labelRect;
        private RowGUIArgs args;
        private RenderTexture previewTexture;
        private UnityGameObject prefab;
        private UnityGameObject addedPrefab;
        private UnityGameObject clickedPrefab;
        private static bool NotSelectItemKey => Key != KeypadEnter && Key != Return;
        private static KeyCode Key => Event.keyCode;
        private static Event Event => current;
        private static Object Object => InstanceIDToObject(_prefabId);
        private static TreeViewItem Root => new TreeViewItem(0, -1);
        private static RenderTexture PreviousTexture => active;
        private int PrefabId => addedPrefab.GetInstanceID();
        private int Depth => RootPathSplits.Length - 2;
        private int SelectedId => HasSelectedIds ? selectedIds[0] : new int();
        private bool HasSelectedIds => selectedIds.Count > 0;
        private bool ItemIsVisible => IsVisible(Item.id);
        private bool ItemSelected => IsSelected(Item.id);
        private bool IsFocused => HasFocus() && ItemSelected;
        private bool IsPrefab => IsPrefabAsset(Item.id, out prefab);
        private bool IsFolder => !IsPrefab;
        private bool ProcessingPathSplits => pathSplitItem < pathSplits.Count - 1;
        private float ContentIndent => GetContentIndent(Item);
        private string Path => GUIDToAssetPath(prefabGuid);
        private string PathSplit => pathSplits[pathSplitItem];
        private string[] RootPathSplits => Path.Split(Text.PathSeparator);
        private PrefabAssetType Type => GetPrefabAssetType(prefab);
        private Rect IconRect => new Rect(rowRect) {width = 20};
        private Rect PreviewRect => new Rect(rowRect) {width = 32, height = 32};
        private Rect PrefabIconRect => new Rect(IconRect) {x = rowRect.xMax - 24};
        private UnityGameObject Asset => LoadAssetAtPath<UnityGameObject>(Path);
        private Texture2D PrefabOnIcon => Type == Regular ? OnIcon : VariantOnIcon;
        private RenderTexture CopiedTexture => new RenderTexture(itemPreview.OutputTexture);
        private GUIStyle LabelStyle => IsFocused ? _whiteLabel : label;
        private GUIContent PrefabContent => new GUIContent(ObjectContent(addedPrefab, addedPrefab.GetType()));
        private TreeViewItem Item => args.item;
        internal readonly Action<UnityGameObject> SelectEntry;
        internal int RowsCount => rows.Count;

        #region constructor method

        internal PrefabSelectionTreeView(TreeViewState state, Action<UnityGameObject> selectEntry) : base(state)
        {
            SelectEntry = selectEntry;
            foldoutOverride = (position, expandedState, style) =>
            {
                position.width = width;
                position.height = 20;
                position.y -= 2;
                expandedState = Toggle(position, expandedState, none, style);
                return expandedState;
            };
            Reload();
        }

        #endregion

        internal void Destroy()
        {
            foreach (var texture in previewCache.Values) DestroyImmediate(texture);
        }

        internal bool IsVisible(int visibleId)
        {
            return visibleItems.Contains(visibleId);
        }

        protected override bool CanMultiSelect(TreeViewItem _)
        {
            return false;
        }

        private static bool IsPrefabAsset(int id, out UnityGameObject prefab)
        {
            _prefabId = id;
            if (Object is UnityGameObject gameObject)
            {
                prefab = gameObject;
                return true;
            }

            prefab = null;
            return false;
        }

        protected override void DoubleClickedItem(int itemId)
        {
            if (IsPrefabAsset(itemId, out clickedPrefab)) SelectEntry(clickedPrefab);
            else SetExpanded(itemId, !IsExpanded(itemId));
        }

        protected override void KeyEvent()
        {
            if (NotSelectItemKey) return;
            DoubleClickedItem(SelectedId);
        }

        protected override void SelectionChanged(IList<int> changedSelectedIds)
        {
            selectedIds = changedSelectedIds;
        }

        protected override TreeViewItem BuildRoot()
        {
            rows.Clear();
            paths.Clear();
            foreach (var guid in FindAssets(Text.FilterByPrefab))
            {
                prefabGuid = guid;
                if (RootPathSplits[0] != Text.PrefabAssets) break;
                pathSplits = RootPathSplits;
                for (pathSplitItem = 1; ProcessingPathSplits; pathSplitItem++)
                {
                    if (paths.Contains(PathSplit)) continue;
                    rows.Add(new TreeViewItem(PathSplit.GetHashCode(), pathSplitItem - 1, Text._ + PathSplit)
                    {
                        icon = FolderIcon
                    });
                    paths.Add(PathSplit);
                }

                addedPrefab = Asset;
                if (CanRender(Asset)) visibleItems.Add(PrefabId);
                rows.Add(new TreeViewItem(PrefabId, Depth, PrefabContent.text)
                {
                    icon = PrefabContent.image as Texture2D
                });
            }

            SetupParentsAndChildrenFromDepths(Root, rows);
            return Root;
        }

        protected override float GetCustomRowHeight(int row, TreeViewItem viewItem)
        {
            if (!IsPrefabAsset(viewItem.id, out _) && hasSearch) return 0;
            return 20;
        }

        public override void OnGUI(Rect guiRect)
        {
            _whiteLabel ??= new GUIStyle(label) {normal = {textColor = whiteLabel.normal.textColor}};
            base.OnGUI(guiRect);
        }

        protected override void RowGUI(RowGUIArgs rowGUIArgs)
        {
            args = rowGUIArgs;
            rowRect = args.rowRect;
            if (IsFolder)
            {
                if (hasSearch) return;
                if (rowRect.Contains(Event.mousePosition) && Event.type == MouseUp)
                {
                    SetSelection(new List<int> {Item.id});
                    SetFocus();
                }
            }

            customFoldoutYOffset = 2;
            itemContent.text = Item.displayName;
            rowRect.x += ContentIndent;
            rowRect.width -= ContentIndent;
            if (IsPrefab)
            {
                labelRect = new Rect(rowRect);
                if (ItemIsVisible)
                {
                    if (!previewCache.TryGetValue(Item.id, out previewTexture))
                    {
                        itemPreview.CreatePreview(prefab);
                        itemPreview.RenderInteractivePreview(PreviewRect);
                        if (itemPreview.OutputTexture)
                        {
                            Blit(itemPreview.OutputTexture, CopiedTexture);
                            active = PreviousTexture;
                            previewCache.Add(Item.id, CopiedTexture);
                        }
                    }

                    if (!previewTexture) Repaint();
                    else DrawTexture(IconRect, previewTexture, ScaleAndCrop);
                    labelRect.x += IconRect.width;
                    labelRect.width -= IconRect.width + 24;
                    Label(labelRect, args.label, LabelStyle);
                    if (!ItemSelected) return;
                    Label(PrefabIconRect, IsFocused ? PrefabOnIcon : Item.icon);
                }
                else
                {
                    itemContent.image = ItemSelected ? PrefabOnIcon : Item.icon;
                    Label(rowRect, itemContent, LabelStyle);
                }
            }
            else
            {
                itemContent.image = IsFocused ? FolderOnIcon : FolderIcon;
                Label(rowRect, itemContent, LabelStyle);
            }
        }
    }
}
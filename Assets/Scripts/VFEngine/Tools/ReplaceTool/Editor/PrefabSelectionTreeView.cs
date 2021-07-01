using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.GameObjectPreview;
using UnityGameObject = UnityEngine.GameObject;
using Text = VFEngine.Tools.ReplaceTool.Editor.Data.ReplaceToolText;

namespace VFEngine.Tools.ReplaceTool.Editor
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
    using static RenderTexture;
    using static Screen;

    internal class PrefabSelectionTreeView : TreeView
    {
        private int selectedId;
        private static GUIStyle _whiteLabel;
        private readonly GUIContent itemContent = new GUIContent();
        private readonly GameObjectPreview itemPreview = new GameObjectPreview();
        private readonly HashSet<int> visibleItems = new HashSet<int>();
        private readonly HashSet<string> paths = new HashSet<string>();
        private readonly List<TreeViewItem> rows = new List<TreeViewItem>();
        private static readonly Texture2D FolderIcon = IconContent(Text.FolderIcon).image as Texture2D;
        internal int RowsCount => rows.Count;
        internal Action<UnityGameObject> SelectEntry;
        internal readonly Dictionary<int, RenderTexture> PreviewCache = new Dictionary<int, RenderTexture>();

        #region constructor method

        internal PrefabSelectionTreeView(TreeViewState state) : base(state)
        {
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
            if (InstanceIDToObject(id) is UnityGameObject gameObject)
            {
                prefab = gameObject;
                return true;
            }

            prefab = null;
            return false;
        }

        protected override void DoubleClickedItem(int id)
        {
            if (IsPrefabAsset(id, out var clickedPrefab)) SelectEntry(clickedPrefab);
            else SetExpanded(id, !IsExpanded(id));
        }

        protected override void KeyEvent()
        {
            var key = current.keyCode;
            if (key != KeypadEnter && key != Return) return;
            DoubleClickedItem(selectedId);
        }

        protected override void SelectionChanged(IList<int> changedSelectedIds)
        {
            if (changedSelectedIds.Count > 0) selectedId = changedSelectedIds[0];
        }

        protected override TreeViewItem BuildRoot()
        {
            rows.Clear();
            paths.Clear();
            foreach (var guid in FindAssets(Text.FilterByPrefab))
            {
                var path = GUIDToAssetPath(guid);
                var rootPathSplits = path.Split(Text.PathSeparator);
                if (rootPathSplits[0] != Text.PrefabAssets) break;
                for (var pathSplitItem = 1; pathSplitItem < ((ICollection) rootPathSplits).Count - 1; pathSplitItem++)
                {
                    var pathSplit = rootPathSplits[pathSplitItem];
                    if (paths.Contains(pathSplit)) continue;
                    rows.Add(new TreeViewItem(pathSplit.GetHashCode(), pathSplitItem - 1, Text._ + pathSplit)
                    {
                        icon = FolderIcon
                    });
                    paths.Add(pathSplit);
                }

                var asset = LoadAssetAtPath<UnityGameObject>(path);
                var prefabId = asset.GetInstanceID();
                if (CanRender(asset)) visibleItems.Add(prefabId);
                var prefabContent = new GUIContent(ObjectContent(asset, asset.GetType()));
                rows.Add(new TreeViewItem(prefabId, rootPathSplits.Length - 2, prefabContent.text)
                {
                    icon = prefabContent.image as Texture2D
                });
            }

            var root = new TreeViewItem(0, -1);
            SetupParentsAndChildrenFromDepths(root, rows);
            return root;
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
            var item = rowGUIArgs.item;
            var rowRect = rowGUIArgs.rowRect;
            var isPrefab = IsPrefabAsset(item.id, out var prefab);
            if (!isPrefab)
            {
                if (hasSearch) return;
                var @event = current;
                if (rowRect.Contains(@event.mousePosition) && @event.type == MouseUp)
                {
                    SetSelection(new List<int> {item.id});
                    SetFocus();
                }
            }

            var itemSelected = IsSelected(item.id);
            var isFocused = HasFocus() && itemSelected;
            var labelStyle = isFocused ? _whiteLabel : label;
            var contentIndent = GetContentIndent(item);
            customFoldoutYOffset = 2;
            itemContent.text = item.displayName;
            rowRect.x += contentIndent;
            rowRect.width -= contentIndent;
            if (isPrefab)
            {
                var labelRect = new Rect(rowRect);
                var variantOnIcon = IconContent(Text.VariantOnIcon).image as Texture2D;
                var onIcon = IconContent(Text.OnIcon).image as Texture2D;
                var type = GetPrefabAssetType(prefab);
                var prefabOnIcon = type == Regular ? onIcon : variantOnIcon;
                if (IsVisible(item.id))
                {
                    if (!PreviewCache.TryGetValue(item.id, out var previewTexture))
                    {
                        CreatePreview(prefab);
                        var previewRect = new Rect(rowRect) {width = 32, height = 32};
                        itemPreview.RenderInteractivePreview(previewRect);
                        if (itemPreview.OutputTexture)
                        {
                            var previousTexture = active;
                            var copiedTexture = new RenderTexture(itemPreview.OutputTexture);
                            Blit(itemPreview.OutputTexture, copiedTexture);
                            active = previousTexture;
                            PreviewCache.Add(item.id, copiedTexture);
                        }
                    }

                    var iconRect = new Rect(rowRect) {width = 20};
                    if (!previewTexture) Repaint();
                    else DrawTexture(iconRect, previewTexture, ScaleAndCrop);
                    labelRect.x += iconRect.width;
                    labelRect.width -= iconRect.width + 24;
                    Label(labelRect, rowGUIArgs.label, labelStyle);
                    if (!itemSelected) return;
                    var prefabIconRect = new Rect(iconRect) {x = rowRect.xMax - 24};
                    Label(prefabIconRect, isFocused ? prefabOnIcon : item.icon);
                }
                else
                {
                    itemContent.image = itemSelected ? prefabOnIcon : item.icon;
                    Label(rowRect, itemContent, labelStyle);
                }
            }
            else
            {
                var folderOnIcon = IconContent(Text.FolderOnIcon).image as Texture2D;
                itemContent.image = isFocused ? folderOnIcon : FolderIcon;
                Label(rowRect, itemContent, labelStyle);
            }
        }
    }
}
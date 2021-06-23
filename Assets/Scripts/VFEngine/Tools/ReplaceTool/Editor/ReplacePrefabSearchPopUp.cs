using System;
using System.IO;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VFEngine.Tools.GameObject.Editor.GameObjectPreview;
using VFEngine.Tools.ReplaceTool.Editor.Data;
using VFEngine.Tools.ReplaceTool.Editor.ScriptableObjects;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.ReplaceTool.Editor
{
    using static EditorJsonUtility;
    using static Application;
    using static Event;
    using static Resources;
    using static GameObjectPreview;
    using static ReplaceToolText;
    using static File;
    using static Path;
    using static String;
    using static AssetPreview;
    using static EditorStyles;
    using static Selection;
    using static EventType;
    using static FontStyle;
    using static GUILayout;
    using static GUILayoutUtility;
    using static KeyCode;
    using static ReplaceTool;

    public class ReplacePrefabSearchPopUp : EditorWindow
    {
        private static ReplacePrefabSearchPopUp _window;
        private static ReplacePrefabSearchPopUp[] _windows;
        private const float PreviewHeight = 128;
        private static readonly GameObjectPreview SelectionPreview = new GameObjectPreview();
        private readonly GUIStyle headerLabel = new GUIStyle(centeredGreyMiniLabel) {fontSize = 11, fontStyle = Bold};
        private SearchField searchField;
        private static PrefabSelectionTreeView _tree;
        private TreeViewStateSO viewState;
        private UnityGameObject instance;
        private Vector2 startSize;
        private Vector2 lastSize;
        private Vector2 newSize;
        private Rect treeViewRect;

        private static string AssetPath =>
            Combine(dataPath.Remove(dataPath.Length - 7, 7), Library, ReplacePrefabTreeState);

        private static Event Event => current;
        private static int SelectedId => _tree.state.selectedIDs[0];
        private static bool HasSelection => _tree.state.selectedIDs.Count > 0;

        private void Initialize()
        {
            viewState = CreateInstance<TreeViewStateSO>();
            if (Exists(AssetPath)) FromJsonOverwrite(ReadAllText(AssetPath), viewState);
            _tree = new PrefabSelectionTreeView(viewState.treeViewState);
            _tree.SelectEntry += prefab => { ReplaceSelectedObjects(gameObjects, prefab); };
            SetPreviewTextureCacheSize(_tree.RowsCount);
            searchField = new SearchField();
            searchField.downOrUpArrowKeyPressed += _tree.SetFocusAndEnsureSelectedItem;
            searchField.SetFocus();
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            foreach (var renderTexture in _tree.PreviewCache.Values) 
                DestroyImmediate(renderTexture);
            RenderUtility.Cleanup();
        }

        private void OnGUI()
        {
            if (Event.type == KeyDown && Event.keyCode == Escape)
            {
                if (_tree.hasSearch) _tree.searchString = Empty;
                else Close();
            }

            if (focusedWindow != this) Close();
            _tree.searchString = searchField.OnToolbarGUI(_tree.searchString);
            Label(ReplaceWith, headerLabel);
            treeViewRect = GetRect(0, 10000, 0, 10000);
            treeViewRect.x += 2;
            treeViewRect.width -= 4;
            treeViewRect.y += 2;
            treeViewRect.height -= 4;
            _tree.OnGUI(treeViewRect);
            if (HasSelection && _tree.IsVisible(SelectedId))
            {
                Size(startSize.x, startSize.y + PreviewHeight);
                var previewRect = GetRect(position.width, PreviewHeight);
                instance = EditorUtility.InstanceIDToObject(SelectedId) as UnityGameObject;
                CreatePreview(instance);
                SelectionPreview.RenderInteractivePreview(previewRect);
                SelectionPreview.DrawPreviewTexture(previewRect);
            }
            else
            {
                Size(startSize.x, startSize.y);
            }
        }

        private void Size(float width, float height)
        {
            newSize = new Vector2(width, height);
            if (newSize == lastSize) return;
            lastSize = newSize;
            position = new Rect(position.x, position.y, width, height);
        }

        internal static void Show(Rect rect)
        {
            _windows = FindObjectsOfTypeAll<ReplacePrefabSearchPopUp>();
            _window = _windows.Length != 0 ? _windows[0] : CreateInstance<ReplacePrefabSearchPopUp>();
            _window.Initialize();
            _window.startSize = rect.size;
            _window.position = new Rect(rect.position, rect.size);
            if (HasSelection && _tree.IsVisible(SelectedId))
                _window.Size(_window.startSize.x, _window.startSize.y + PreviewHeight);
            else _window.Size(_window.startSize.x, _window.startSize.y);
            _window.ShowPopup();
        }

        internal new void Close()
        {
            WriteAllText(AssetPath, ToJson(viewState));
            base.Close();
        }
    }
}
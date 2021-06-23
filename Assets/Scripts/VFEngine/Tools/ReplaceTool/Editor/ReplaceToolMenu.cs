using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VFEngine.Tools.ReplaceTool.Editor.Data;
using EditorUnity = UnityEditor.Editor;

namespace VFEngine.Tools.ReplaceTool.Editor
{
    using static Selection;
    using static Event;
    using static GUIUtility;
    using static EditorApplication;
    using static EditorWindow;
    using static ReplaceToolText;
    using static ReplacePrefabSearchPopUp;

    internal static class ReplaceToolMenu
    {
        private static bool _replacedPrefab;
        private static Type _hierarchyType;
        private static EditorWindow _focusedWindow;
        private static IMGUIContainer _hierarchyGUI;
        private static Vector2 _mousePosition;
        private static Rect _replacePrefabRect;

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            _hierarchyType = typeof(EditorUnity).Assembly.GetType(SceneHierarchyWindow);
            update += () =>
            {
                if (_focusedWindow == focusedWindow) return;
                _focusedWindow = focusedWindow;
                if (_focusedWindow.GetType() != _hierarchyType) return;
                if (_hierarchyGUI != null) _hierarchyGUI.onGUIHandler -= OnFocusedHierarchyGUI;
                _hierarchyGUI = _focusedWindow.rootVisualElement.parent.Query<IMGUIContainer>();
                _hierarchyGUI.onGUIHandler += OnFocusedHierarchyGUI;
            };
        }

        private static void OnFocusedHierarchyGUI()
        {
            _mousePosition = GUIToScreenPoint(current.mousePosition);
        }

        [MenuItem(GameObjectReplace, true, priority = 0)]
        private static bool ReplaceSelectionValidate()
        {
            return gameObjects.Length > 0;
        }

        [MenuItem(GameObjectReplace, priority = 0)]
        private static void ReplaceSelection()
        {
            if (_replacedPrefab) return;
            _replacePrefabRect = new Rect(_mousePosition, new Vector2(240, 360));
            Show(_replacePrefabRect);
            delayCall += () => _replacedPrefab = false;
        }
    }
}
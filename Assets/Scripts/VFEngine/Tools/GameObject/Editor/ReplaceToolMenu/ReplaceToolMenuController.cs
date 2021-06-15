using System;
using UnityEditor;
using VFEngine.Tools.GameObject.Editor.ReplaceToolMenu.Data;
using EditorUnity = UnityEditor.Editor;

namespace VFEngine.Tools.GameObject.Editor.ReplaceToolMenu
{
    using static EditorWindow;
    using static Selection;
    using static ReplaceToolMenuText;

    internal class ReplaceToolMenuController
    {
        private static ReplaceToolMenuModel _replaceToolMenu;
        private static bool CanInitializeReplaceToolMenu => _replaceToolMenu == null;

        internal ReplaceToolMenuController()
        {
            Initialize();
        }

        private static Type HierarchyType => typeof(EditorUnity).Assembly.GetType(SceneHierarchyWindow);

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            if (CanInitializeReplaceToolMenu) _replaceToolMenu = new ReplaceToolMenuModel(HierarchyType, focusedWindow);
            else _replaceToolMenu.Initialize(HierarchyType, focusedWindow);
        }

        [MenuItem(GameObjectReplace, true, priority = 0)]
        private static bool ReplaceSelectionValidate()
        {
            return _replaceToolMenu.ReplaceSelectionValidate(gameObjects);
        }

        private static bool HasExecuted => _replaceToolMenu.HasExecuted;

        [MenuItem(GameObjectReplace, priority = 0)]
        private static void ReplaceSelection()
        {
            if (HasExecuted) return;
            _replaceToolMenu.ReplaceSelection();
        }
    }
}
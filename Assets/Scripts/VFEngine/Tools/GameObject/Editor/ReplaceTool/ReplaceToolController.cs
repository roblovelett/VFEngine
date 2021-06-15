using UnityEditor;
using VFEngine.Tools.GameObject.Editor.ReplaceTool.Data;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.GameObject.Editor.ReplaceTool
{
    using static ReplaceToolText;

    internal class ReplaceToolController : EditorWindow
    {
        private static ReplaceToolModel _replaceTool;
        private static bool CanInitializeReplaceToolModel => _replaceTool == null;
        private static bool CanRepaint => _replaceTool.OnInspectorUpdate;
        private static bool CanRepaintOnSelectionChange => _replaceTool.OnSelectionChange();

        private void OnGUI()
        {
            _replaceTool.OnGUI();
        }

        private void OnInspectorUpdate()
        {
            if (CanRepaint) Repaint();
        }

        private void OnSelectionChange()
        {
            if (CanRepaintOnSelectionChange) Repaint();
        }

        private static void Initialize()
        {
            if (CanInitializeReplaceToolModel) _replaceTool = new ReplaceToolModel();
            else _replaceTool.Initialize();
        }

        [MenuItem(ReplaceToolMenuItem)]
        internal static void ShowWindow()
        {
            _replaceTool.ShowWindow(GetWindow<ReplaceToolController>());
        }
        
        internal ReplaceToolController()
        {
            Initialize();
        }

        internal void ReplaceSelectedObjects(UnityGameObject[] objectToReplace, UnityGameObject replaceObject)
        {
            _replaceTool.ReplaceSelectedObjects(objectToReplace, replaceObject);
        }
    }
}
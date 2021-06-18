using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp;

namespace VFEngine.Tools.GameObject.Editor.ReplaceToolMenu.Data
{
    using static ScriptableObject;

    /*internal class ReplaceToolMenuData
    {
        internal Type HierarchyType { get; set; }
        internal EditorWindow FocusedWindow { get; set; }
        internal IMGUIContainer HierarchyGUI { get; set; }
        internal Vector2 MousePosition { get; set; }
        internal bool HasExecuted { get; set; }
        internal ReplacePrefabSearchPopUpController ReplacePrefabSearchPopUpController { get; private set; }

        internal ReplaceToolMenuData(Type hierarchyType, EditorWindow focusedWindow)
        {
            Initialize(hierarchyType, focusedWindow);
        }

        private void Initialize(Type hierarchyType, EditorWindow focusedWindow)
        {
            HierarchyType = hierarchyType;
            FocusedWindow = focusedWindow;
            HierarchyGUI = new IMGUIContainer();
            MousePosition = new Vector2();
            HasExecuted = new bool();
            ReplacePrefabSearchPopUpController = CreateInstance<ReplacePrefabSearchPopUpController>();
        }
    }*/
}
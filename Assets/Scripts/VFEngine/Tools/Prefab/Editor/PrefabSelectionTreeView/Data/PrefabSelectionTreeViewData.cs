using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityObject = UnityEngine.Object;
using UnityGameObject = UnityEngine.GameObject;

// ReSharper disable InconsistentNaming
namespace VFEngine.Tools.Prefab.Editor.PrefabSelectionTreeView.Data
{
    using static TreeView;

    /*internal class PrefabSelectionTreeViewData
    {
        private Rect labelRect;
        private Rect rowRect;
        private Rect position;
        internal bool CanReload { get; private set; }
        internal Dictionary<int, RenderTexture> Cache { get; private set; }
        internal List<TreeViewItem> Rows { get; private set; }
        internal GUIContent ItemContent { get; private set; }
        internal HashSet<int> VisibleItems { get; private set; }
        internal HashSet<string> Paths { get; private set; }
        internal int CurrentFolder { get; set; }
        internal int CurrentID { get; set; }
        internal bool HasSearch { get; set; }
        internal bool Expanded { get; set; }
        internal bool HasFocus { get; set; }
        internal bool CanInitializeSelectionAndFocus { get; set; }
        internal bool SetupParentsAndChildrenFromPathDepths { get; set; }
        internal float ContentIndent { get; set; }
        internal string Path { get; set; }
        internal KeyCode CurrentKeyPressed { get; set; }
        private Rect Position { get; set; }
        internal Action<UnityGameObject> OnSelectEntry { get; set; }
        internal GUIStyle WhiteLabel { get; set; }
        internal UnityObject Object { get; set; }
        internal UnityObject ObjectInstance { get; set; }
        internal UnityGameObject Prefab { get; set; }
        internal TreeViewItem Root { get; set; }
        internal RenderTexture PreviousTexture { get; set; }
        internal RenderTexture PreviewTexture { get; set; }
        internal DoFoldoutCallback ExpandedState { get; set; }
        internal int SelectedId { get; set; }
        internal int VisibleID { get; set; }

        internal float PositionWidth
        {
            set => position.width = value;
        }

        internal float PositionHeight
        {
            set => position.height = value;
        }

        internal float PositionY
        {
            get => position.y;
            set => position.y = value;
        }

        internal float RowRectX
        {
            get => rowRect.x;
            set => rowRect.x = value;
        }

        internal float RowRectWidth
        {
            get => rowRect.width;
            set => rowRect.width = value;
        }

        internal Rect RowRect
        {
            get => rowRect;
            set => rowRect = value;
        }

        internal Rect LabelRect
        {
            get => labelRect;
            set => labelRect = value;
        }

        internal float LabelRectX
        {
            get => labelRect.x;
            set => labelRect.x = value;
        }

        internal float LabelRectWidth
        {
            get => labelRect.width;
            set => labelRect.width = value;
        }

        internal PrefabSelectionTreeViewData()
        {
            Initialize();
        }

        internal void Initialize()
        {
            CanReload = new bool();
            Expanded = new bool();
            Position = new Rect();
            LabelRect = new Rect();
            position = Position;
            ExpandedState = null;
            WhiteLabel = null;
            Object = null;
            Prefab = null;
            Rows = null;
            Paths = null;
            Cache = null;
            VisibleItems = null;
            ItemContent = null;
            VisibleID = new int();
            SelectedId = new int();
        }
    }*/
}
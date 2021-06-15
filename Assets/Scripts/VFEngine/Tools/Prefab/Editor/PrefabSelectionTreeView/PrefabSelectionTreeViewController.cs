using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VContainer;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.Prefab.Editor.PrefabSelectionTreeView
{
    using static Model;
    using static ScaleMode;
    using static EditorUtility;
    using static Event;

    internal class Controller : TreeView
    {
        private static Model _treeView;
        private bool CanInitializePrefabSelectionTreeViewModel => _treeView == null;
        private bool CanReload => _treeView.CanReload;

        [Inject]
        internal Controller(TreeViewState state) : base(state)
        {
            if (CanInitializePrefabSelectionTreeViewModel) _treeView = new Model(this);
            else _treeView.Initialize(this);
            foldoutOverride = FoldoutOverride();
            if (CanReload) Reload();
        }

        internal void Cleanup()
        {
            _treeView.Cleanup();
        }

        public bool CanRender(int id)
        {
            return _treeView.CanRender(id);
        }

        protected override bool CanMultiSelect(TreeViewItem item)
        {
            return false;
        }

        private bool Expanded => _treeView.Expanded;
        private bool CanCreatePreviewForTarget => _treeView.CanCreatePreviewForTarget;

        internal Action<UnityGameObject> OnSelectEntry
        {
            get => _treeView.OnSelectEntry;
            set => _treeView.OnSelectEntry = value;
        }

        protected override void DoubleClickedItem(int id)
        {
            _treeView.DoubleClickedItem(InstanceIDToObject(id));
            if (Expanded) SetExpanded(id, !IsExpanded(id));
        }

        protected override void KeyEvent()
        {
            _treeView.KeyEvent(current.keyCode);
        }

        protected override void SelectionChanged(IList<int> selectedIds)
        {
            _treeView.SelectionChanged(selectedIds.Count > 0, selectedIds[0]);
        }

        private TreeViewItem Root => _treeView.Root;
        private List<TreeViewItem> Rows => _treeView.Rows;
        private bool SetupParentsAndChildrenFromPathDepths => _treeView.SetupParentsAndChildrenFromPathDepths;

        protected override TreeViewItem BuildRoot()
        {
            _treeView.BuildRoot();
            if (SetupParentsAndChildrenFromPathDepths) SetupParentsAndChildrenFromDepths(Root, Rows);
            return Root;
        }

        protected override float GetCustomRowHeight(int row, TreeViewItem item)
        {
            return _treeView.GetCustomRowHeight(InstanceIDToObject(item.id), hasSearch);
        }

        private bool HasWhiteLabel => _treeView.HasWhiteLabel;

        public override void OnGUI(Rect rect)
        {
            if (!HasWhiteLabel) _treeView.OnGUI();
            base.OnGUI(rect);
        }

        private bool CanRepaint => _treeView.CanRepaint;

        protected override void RowGUI(RowGUIArgs arguments)
        {
            _treeView.InitializeRowGUI(arguments.item, arguments.rowRect, hasSearch);
            InitializeRowGUI();
            _treeView.InitializeRowGUI(IsSelected(arguments.item.id) && HasFocus(), GetContentIndent(arguments.item),
                arguments.item.displayName);
            if (CanCreatePreviewForTarget) _treeView.CreatePreviewForTarget();
            _treeView.InitializeRowGUI();
            RepaintOrInitializeRowGUI();
            _treeView.InitializeRowGUI(arguments.label, IsSelected(arguments.item.id), arguments.item.icon);
        }

        private bool CanInitializeSelectionAndFocus => _treeView.CanInitializeSelectionAndFocus;
        private List<int> InitializedSelection => _treeView.InitializedSelection;

        private void InitializeRowGUI()
        {
            if (!CanInitializeSelectionAndFocus) return;
            SetSelection(InitializedSelection);
            SetFocus();
            customFoldoutYOffset = 2;
        }

        private void RepaintOrInitializeRowGUI()
        {
            if (CanRepaint) Repaint();
            else _treeView.InitializeRowGUI(ScaleAndCrop);
        }
    }
}
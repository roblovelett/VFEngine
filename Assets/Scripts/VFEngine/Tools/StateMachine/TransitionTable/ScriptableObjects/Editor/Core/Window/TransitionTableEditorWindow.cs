/*using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using EditorUnity = UnityEditor.Editor;
using Object = UnityEngine.Object;

namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.Window
{
    using static AssetDatabase;
    using static TransitionTableEditorWindow.Text;
    using static Array;
    using static EditorApplication;
    using static EditorGUIUtility;
    using static PlayModeStateChange;
    using static SelectionType;
    using static EditorUnity;

    internal class TransitionTableEditorWindow : EditorWindow
    {
        private bool doRefresh;
        private EditorUnity transitionTableEditor;
        private static TransitionTableEditorWindow _window;
        private bool DoRefresh => doRefresh == false;
        private bool SelectIndexToTargetIndex => transitionTableEditor && Target;
        private bool CreateTransitionTableEditor => transitionTableEditor == null;
        private bool SelectedIsNotTarget => Selected != Target;
        private bool NoTarget => !Target;
        private bool NoTargetIndex => TargetIndex < 0;
        private int TargetIndex => ListView.itemsSource.IndexOf(Target);

        private int SelectedIndex
        {
            set => ListView.selectedIndex = value;
        }

        private Object Target => transitionTableEditor.target;
        private Object Selected => ListView.selectedItem as Object;
        private ListView ListView => rootVisualElement.Q<ListView>(className: TableList);
        private IMGUIContainer Editor => rootVisualElement.Q<IMGUIContainer>(className: TableEditor);
        private static bool GetEditorWindow => _window == null;
        private static string EditorLabel => TableEditorLabel(isProSkin);
        private static Object[] Assets => GetAssets();
        private static VisualTreeAsset VisualTree => LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
        private static StyleSheet StyleSheet => LoadAssetAtPath<StyleSheet>(UssPath);

        [MenuItem(WindowText, menuItem = "State Machine/Transition Table Editor")]
        internal static void Display()
        {
            if (GetEditorWindow) _window = GetWindow<TransitionTableEditorWindow>(WindowText);
            _window.Show();
        }

        private void OnEnable()
        {
            rootVisualElement.Add(VisualTree.CloneTree());
            rootVisualElement.Query<Label>().Build().ForEach(label => label.AddToClassList(EditorLabel));
            rootVisualElement.styleSheets.Add(StyleSheet);
            minSize = new Vector2(480, 360);
            playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            var inPlayMode = state == EnteredPlayMode;
            if (inPlayMode) doRefresh = true;
        }

        private void OnFocus()
        {
            if (DoRefresh) doRefresh = true;
        }

        private void OnLostFocus()
        {
            ListView.onSelectionChange -= OnListSelectionChange;
        }

        private void Update()
        {
            if (DoRefresh) return;
            CreateListView();
            doRefresh = false;
        }

        private void CreateListView()
        {
            ListView.makeItem = null;
            ListView.bindItem = null;
            ListView.itemsSource = Assets;
            ListView.itemHeight = 16;
            ListView.makeItem = () =>
            {
                var label = new Label();
                label.AddToClassList(WindowText);
                return label;
            };
            ListView.bindItem = (element, i) => ((Label) element).text = Assets[i].name;
            ListView.selectionType = Single;
            ListView.onSelectionChange -= OnListSelectionChange;
            ListView.onSelectionChange += OnListSelectionChange;
            if (SelectIndexToTargetIndex) ListView.selectedIndex = IndexOf(Assets, Target);
        }

        private void OnListSelectionChange(IEnumerable<object> items)
        {
            var list = items.ToList();
            var table = list[0] as TransitionTableSO;
            var cacheTransitionTableEditor = Target != table;
            var noTable = table == null;
            var noListItems = list.Count == 0;
            var noItems = noTable || noListItems;
            if (noItems) return;
            if (CreateTransitionTableEditor) transitionTableEditor = CreateEditor(table, typeof(TransitionTableEditor));
            else if (cacheTransitionTableEditor)
                CreateCachedEditor(table, typeof(TransitionTableEditor), ref transitionTableEditor);
            Editor.onGUIHandler = null;
            Editor.onGUIHandler = () =>
            {
                if (NoTarget)
                {
                    Editor.onGUIHandler = null;
                    return;
                }

                if (SelectedIsNotTarget)
                {
                    SelectedIndex = TargetIndex;
                    if (NoTargetIndex)
                    {
                        Editor.onGUIHandler = null;
                        return;
                    }
                }

                transitionTableEditor.OnInspectorGUI();
            };
        }

        private static Object[] GetAssets()
        {
            var typeOfTransitionTable = TypeIs(nameof(TransitionTableSO));
            var guids = FindAssets(typeOfTransitionTable);
            var guidsAmount = guids.Length;
            var loadedAssets = new Object[guidsAmount];
            var loadedAssetsAmount = loadedAssets.Length;
            for (var currentAsset = 0; currentAsset < loadedAssetsAmount; currentAsset++)
                loadedAssets[currentAsset] = LoadAssetAtPath<TransitionTableSO>(GUIDToAssetPath(guids[currentAsset]));
            return loadedAssets;
        }

        public static class Text
        {
            private const string State = "State";
            private const string Machine = "Machine/";
            private const string ToolsPath = "Assets/Scripts/VFEngine/Tools/";
            private static readonly string StateMachineEditorPath = $"{ToolsPath}{State}{Machine}Editor/";
            private const string Pro = "pro";
            private const string Personal = "personal";
            public const string WindowText = "Transition Table Editor";
            public static readonly string UxmlPath = $"{StateMachineEditorPath}TransitionTableEditorWindow.uxml";
            public static readonly string UssPath = $"{StateMachineEditorPath}TransitionTableEditorWindow.uss";
            public const string TableList = "table-list";
            public const string TableEditor = "table-editor";

            public static string TableEditorLabel(bool isProSkin)
            {
                var editorVersion = EditorVersion(isProSkin);
                return $"label-{editorVersion}";
            }

            public static string TypeIs(string nameOf)
            {
                return $"t:{nameOf}";
            }

            private static string EditorVersion(bool isProSkin)
            {
                var message = isProSkin ? Pro : Personal;
                return message;
            }
        }
    }
}*/
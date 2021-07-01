using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VFEngine.Tools.StateMachineSO.Editor.Data;
using VFEngine.Tools.StateMachineSO.ScriptableObjects;
using EditorUnity = UnityEditor.Editor;
using UnityObject = UnityEngine.Object;

namespace VFEngine.Tools.StateMachineSO.Editor
{
    using static PlayModeStateChange;
    using static Array;
    using static AssetDatabase;
    using static EditorUnity;
    using static EditorApplication;
    using static EditorGUIUtility;
    using static EditorText;

    internal class TransitionTableWindow : EditorWindow
    {
        private bool doRefresh;
        private string labelClass;
        private EditorUnity transitionTableEditor;
        private static TransitionTableWindow _window;

        [MenuItem(TransitionTableEditorItem, menuItem = TransitionTableEditorMenu)]
        internal static void Display()
        {
            if (_window == null) _window = GetWindow<TransitionTableWindow>(TransitionTableEditorItem);
            _window.Show();
        }

        private void OnEnable()
        {
            var visualTree = LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            var styleSheet = LoadAssetAtPath<StyleSheet>(UssPath);
            labelClass = LabelClass(isProSkin);
            rootVisualElement.Add(visualTree.CloneTree());
            rootVisualElement.Query<Label>().Build().ForEach(label => label.AddToClassList(labelClass));
            rootVisualElement.styleSheets.Add(styleSheet);
            minSize = new Vector2(480, 360);
            playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == EnteredPlayMode) doRefresh = true;
        }

        private void OnFocus()
        {
            if (doRefresh == false) doRefresh = true;
        }

        private void OnLostFocus()
        {
            var listView = TableListView();
            listView.onSelectionChange -= OnListSelectionChange;
        }

        private ListView TableListView()
        {
            return rootVisualElement.Q<ListView>(className: TableList);
        }

        private void Update()
        {
            if (!doRefresh) return;
            var guids = FindAssets(GuidFilter);
            var transitionTables = new TransitionTableSO[guids.Length];
            for (var index = 0; index < guids.Length; index++)
                transitionTables[index] = LoadAssetAtPath<TransitionTableSO>(GUIDToAssetPath(guids[index]));
            var assets = transitionTables.ToArray<UnityObject>();
            var listView = TableListView();
            listView.makeItem = null;
            listView.bindItem = null;
            listView.itemsSource = assets;
            listView.itemHeight = 16;
            listView.makeItem = () =>
            {
                var label = new Label();
                label.AddToClassList(labelClass);
                return label;
            };
            listView.bindItem = (element, assetsIndex) => ((Label) element).text = assets[assetsIndex].name;
            listView.selectionType = SelectionType.Single;
            listView.onSelectionChange -= OnListSelectionChange;
            listView.onSelectionChange += OnListSelectionChange;
            if (!transitionTableEditor || !transitionTableEditor.target) return;
            listView.selectedIndex = IndexOf(assets, transitionTableEditor.target);
            doRefresh = false;
        }

        private void OnListSelectionChange(IEnumerable<object> list)
        {
            var enumeratedList = list.ToList();
            var table = enumeratedList[0] as TransitionTableSO;
            var editor = rootVisualElement.Q<IMGUIContainer>(className: TableEditor);
            editor.onGUIHandler = null;
            if (enumeratedList.Count == 0 || table == null) return;
            if (transitionTableEditor == null)
                transitionTableEditor = CreateEditor(table, typeof(TransitionTableEditor));
            else if (transitionTableEditor.target != table)
                CreateCachedEditor(table, typeof(TransitionTableEditor), ref transitionTableEditor);
            editor.onGUIHandler = () =>
            {
                if (!transitionTableEditor.target)
                {
                    editor.onGUIHandler = null;
                    return;
                }

                var listView = TableListView();
                if (listView.selectedItem as UnityObject != transitionTableEditor.target)
                {
                    var itemIndex = listView.itemsSource.IndexOf(transitionTableEditor.target);
                    listView.selectedIndex = itemIndex;
                    if (itemIndex < 0)
                    {
                        editor.onGUIHandler = null;
                        return;
                    }
                }

                transitionTableEditor.OnInspectorGUI();
            };
        }
    }
}
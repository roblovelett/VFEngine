using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

using UnityObject = UnityEngine.Object;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects.Menu
{
    using static HelpBoxMessageType;
    using static PlayModeStateChange;
    using static Array;
    using static AssetDatabase;
    using static Editor;
    using static EditorApplication;
    using static EditorGUIUtility;

    internal class TransitionTableEditorWindow : EditorWindow
    {
        private static TransitionTableEditorWindow _window;

        private const string UxmlPath =
            "Assets/Scripts/VFEngine/Tools/StateMachineSO/ScriptableObjects/Menu/TransitionTableEditorWindow.uxml";

        private const string USSPath =
            "Assets/Scripts/VFEngine/Tools/StateMachineSO/ScriptableObjects/Menu/TransitionTableEditorWindow.uss";

        private bool doRefresh;
        private Editor transitionTableEditor;

        [MenuItem("Transition Table Editor", menuItem = "Tools/State Machine SO/Transition Table Editor")]
        internal static void Display()
        {
            if (_window == null) _window = GetWindow<TransitionTableEditorWindow>("Transition Table Editor");
            _window.Show();
        }

        private void OnEnable()
        {
            var visualTree = LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            var styleSheet = LoadAssetAtPath<StyleSheet>(USSPath);
            rootVisualElement.Add(visualTree.CloneTree());
            var labelClass = $"label-{(isProSkin ? "pro" : "personal")}";
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
            var listView = rootVisualElement.Q<ListView>(className: "table-list");
            listView.onSelectionChange -= OnListSelectionChanged;
        }

        private void Update()
        {
            if (!doRefresh) return;
            CreateListView();
            doRefresh = false;
        }

        private void CreateListView()
        {
            var assets = FindAssets(out var objects);
            var listView = rootVisualElement.Q<ListView>(className: "table-list");
            if (assets.Length <= 0)
            {
                Debug.Log("no transition tables created.");
                /*
                listView.bindItem = (element, _) =>
                {
                    ((HelpBox) element).messageType = Info;
                    ((HelpBox) element).text = "No Transition Table Assets exist.";
                };
                */
                return;
            }

            listView.itemsSource = assets;
            listView.itemHeight = 16;
            var labelClass = $"label-{(isProSkin ? "pro" : "personal")}";
            listView.makeItem = () =>
            {
                var label = new Label();
                label.AddToClassList(labelClass);
                return label;
            };
            listView.bindItem = (element, i) => ((Label) element).text = assets[i].name;
            listView.selectionType = SelectionType.Single;
            listView.onSelectionChange -= OnListSelectionChanged;
            listView.onSelectionChange += OnListSelectionChanged;
            if (transitionTableEditor && transitionTableEditor.target)
                listView.selectedIndex = IndexOf(objects, transitionTableEditor.target);
        }

        private void OnListSelectionChanged(IEnumerable<object> list)
        {
            var editor = rootVisualElement.Q<IMGUIContainer>(className: "table-editor");
            editor.onGUIHandler = null;
            var listArray = list.ToArray();
            if (listArray.Length == 0) return;
            var transitionTable = listArray[0] as TransitionTableSO;
            if (transitionTable == null) return;
            if (transitionTableEditor == null)
                transitionTableEditor = CreateEditor(transitionTable, typeof(TransitionTableEditor));
            else if (transitionTableEditor.target != transitionTable)
                CreateCachedEditor(transitionTable, typeof(TransitionTableEditor), ref transitionTableEditor);
            editor.onGUIHandler = () =>
            {
                if (!transitionTableEditor.target)
                {
                    editor.onGUIHandler = null;
                    return;
                }

                var listView = rootVisualElement.Q<ListView>(className: "table-list");
                if (listView.selectedItem as UnityObject != transitionTableEditor.target)
                {
                    var i = listView.itemsSource.IndexOf(transitionTableEditor.target);
                    listView.selectedIndex = i;
                    if (i < 0)
                    {
                        editor.onGUIHandler = null;
                        return;
                    }
                }

                transitionTableEditor.OnInspectorGUI();
            };
        }

        private static TransitionTableSO[] FindAssets(out UnityObject[] objects)
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(TransitionTableSO)}");
            var assets = new TransitionTableSO[guids.Length];
            objects = new UnityObject[guids.Length];
            for (var i = 0; i < guids.Length; i++)
            {
                assets[i] = LoadAssetAtPath<TransitionTableSO>(GUIDToAssetPath(guids[i]));
                objects[i] = assets[i];
            }
            return assets;
        }
    }
}
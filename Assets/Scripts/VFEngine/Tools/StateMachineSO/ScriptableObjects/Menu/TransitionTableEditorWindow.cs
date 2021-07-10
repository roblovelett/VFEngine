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
            var labelClass = $"label-{(isProSkin ? "pro" : "personal")}";
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

        private void OnPlayModeStateChanged(PlayModeStateChange @object)
        {
            if (@object == EnteredPlayMode) doRefresh = true;
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

            #region List View

            #region Assets

            var guids = FindAssets($"t:{nameof(TransitionTableSO)}");
            var guidsAmount = guids.Length;
            var assets = new TransitionTableSO[guidsAmount];
            var objects = new UnityObject[guidsAmount];
            for (var idx = 0; idx < guidsAmount; idx++)
            {
                assets[idx] = LoadAssetAtPath<TransitionTableSO>(GUIDToAssetPath(guids[idx]));
                objects[idx] = assets[idx];
            }

            #endregion

            var listView = rootVisualElement.Q<ListView>(className: "table-list");
            var helpBox = rootVisualElement.Q<HelpBox>(className: "table-list-help-box");
            if (assets.Length <= 0)
            {
                helpBox.messageType = Info;
                helpBox.text = "No State Transition Table assets exist.";
                return;
            }

            helpBox.RemoveFromHierarchy();
            listView.itemsSource = assets;
            listView.itemHeight = 16;
            var labelClass = $"label-{(isProSkin ? "pro" : "personal")}";
            listView.makeItem = () =>
            {
                var label = new Label();
                label.AddToClassList(labelClass);
                return label;
            };
            listView.bindItem = (element, idx) =>
            {
                var @object = assets[idx] as UnityObject;
                ((Label) element).text = @object.name;
            };
            listView.selectionType = SelectionType.Single;
            listView.onSelectionChange -= OnListSelectionChanged;
            listView.onSelectionChange += OnListSelectionChanged;
            if (transitionTableEditor && transitionTableEditor.target)
                listView.selectedIndex = IndexOf(objects, transitionTableEditor.target);

            #endregion

            doRefresh = false;
        }

        private void OnListSelectionChanged(IEnumerable<object> list)
        {
            var editor = rootVisualElement.Q<IMGUIContainer>(className: "table-editor");
            editor.onGUIHandler = null;
            var listArray = list.ToArray();
            if (listArray.Length == 0 || listArray[0] == null) return;
            var transitionTable = listArray[0] as TransitionTableSO;
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
                    var idx = listView.itemsSource.IndexOf(transitionTableEditor.target);
                    listView.selectedIndex = idx;
                    if (idx < 0)
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
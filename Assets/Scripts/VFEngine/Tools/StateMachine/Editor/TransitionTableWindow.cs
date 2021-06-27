using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VFEngine.Tools.StateMachine.Editor.Data;
using VFEngine.Tools.StateMachine.ScriptableObjects;
using EditorUnity = UnityEditor.Editor;
using UnityObject = UnityEngine.Object;

namespace VFEngine.Tools.StateMachine.Editor
{
    using static PlayModeStateChange;
    using static SelectionType;
    using static EditorText;
    using static Array;
    using static AssetDatabase;
    using static EditorUnity;
    using static EditorApplication;
    using static EditorGUIUtility;

    internal class TransitionTableWindow : EditorWindow
    {
        private int assetIndex;
        private int targetIndex;
        private bool doRefresh;
        private string labelClass;
        private string[] guids;
        private object[] enumerable;
        private Label addedLabel;
        private ListView listView;
        private StyleSheet styleSheet;
        private EditorUnity transitionTableEditor;
        private VisualTreeAsset visualTree;
        private IMGUIContainer editor;
        private TransitionTableSO table;
        private UnityObject[] objectAssets;
        private TransitionTableSO[] assets;
        private static TransitionTableWindow _window;

        [MenuItem("Transition Table Editor", menuItem = "Tools/State Machine/Transition Table Editor")]
        internal static void Display()
        {
            if (_window == null) _window = GetWindow<TransitionTableWindow>(TransitionTableWindowLabel);
            _window.Show();
        }

        private void OnEnable()
        {
            visualTree = LoadAssetAtPath<VisualTreeAsset>(UxmlPath);
            styleSheet = LoadAssetAtPath<StyleSheet>(USSPath);
            rootVisualElement.Add(visualTree.CloneTree());
            labelClass = LabelClass(isProSkin);
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
            listView = rootVisualElement.Q<ListView>(className: TableList);
            listView.onSelectionChange -= OnListSelectionChange;
        }

        private void Update()
        {
            if (!doRefresh) return;
            guids = FindAssets(GuidFilter);
            assets = new TransitionTableSO[guids.Length];
            for (assetIndex = 0; assetIndex < guids.Length; assetIndex++)
                assets[assetIndex] = LoadAssetAtPath<TransitionTableSO>(GUIDToAssetPath(guids[assetIndex]));
            listView = rootVisualElement.Q<ListView>(className: TableList);
            listView.makeItem = null;
            listView.bindItem = null;
            listView.itemsSource = assets;
            listView.itemHeight = 16;
            labelClass = LabelClass(isProSkin);
            listView.makeItem = () =>
            {
                addedLabel = new Label();
                addedLabel.AddToClassList(labelClass);
                return addedLabel;
            };
            listView.bindItem = (element, i) => ((Label) element).text = assets[i].name;
            listView.selectionType = Single;
            listView.onSelectionChange -= OnListSelectionChange;
            listView.onSelectionChange += OnListSelectionChange;
            if (!transitionTableEditor || !transitionTableEditor.target) return;
            objectAssets = assets.ToArray<UnityObject>();
            listView.selectedIndex = IndexOf(objectAssets, transitionTableEditor.target);
            doRefresh = false;
        }

        private void OnListSelectionChange(IEnumerable<object> list)
        {
            editor = rootVisualElement.Q<IMGUIContainer>(className: TableEditor);
            editor.onGUIHandler = null;
            enumerable = list as object[] ?? list.ToArray();
            if (!enumerable.Any()) return;
            table = enumerable[0] as TransitionTableSO;
            if (table == null) return;
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

                listView = rootVisualElement.Q<ListView>(className: TableList);
                if (listView.selectedItem as UnityObject != transitionTableEditor.target)
                {
                    targetIndex = listView.itemsSource.IndexOf(transitionTableEditor.target);
                    listView.selectedIndex = targetIndex;
                    if (targetIndex < 0)
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
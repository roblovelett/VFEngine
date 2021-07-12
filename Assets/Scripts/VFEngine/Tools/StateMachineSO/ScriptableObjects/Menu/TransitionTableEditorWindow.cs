using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityObject = UnityEngine.Object;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects.Menu
{
	internal class TransitionTableEditorWindow : EditorWindow
	{
		private static TransitionTableEditorWindow _window;
		private static readonly string _uxmlPath = "Assets/Scripts/VFEngine/Tools/StateMachineSO/ScriptableObjects/Menu/TransitionTableEditorWindow.uxml";
		private static readonly string _ussPath = "Assets/Scripts/VFEngine/Tools/StateMachineSO/ScriptableObjects/Menu/TransitionTableEditorWindow.uss";
		private bool _doRefresh;
		private bool hasHelpBox;
		private HelpBox helpBox;
		
		private UnityEditor.Editor _transitionTableEditor;

		[MenuItem("Transition Table Editor", menuItem = "Tools/State Machine SO/Transition Table Editor")]
		internal static void Display()
		{
			if (_window == null)
				_window = GetWindow<TransitionTableEditorWindow>("Transition Table Editor");

			_window.Show();
		}

		private void OnEnable()
		{
			hasHelpBox = false;
			var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(_uxmlPath);
			var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(_ussPath);

			rootVisualElement.Add(visualTree.CloneTree());

			string labelClass = $"label-{(EditorGUIUtility.isProSkin ? "pro" : "personal")}";
			rootVisualElement.Query<Label>().Build().ForEach(label => label.AddToClassList(labelClass));

			rootVisualElement.styleSheets.Add(styleSheet);

			minSize = new Vector2(480, 360);

			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private void OnDisable()
		{
			EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
		}

		private void OnPlayModeStateChanged(PlayModeStateChange obj)
		{
			if (obj == PlayModeStateChange.EnteredPlayMode)
				_doRefresh = true;
		}

		/// <summary>
		/// Update list every time we gain focus
		/// </summary>
		private void OnFocus()
		{
			// Calling CreateListView() from here when the window is docked
			// throws a NullReferenceException in UnityEditor.DockArea:OnEnable().
			if (_doRefresh == false)
				_doRefresh = true;
		}

		private void OnLostFocus()
		{
			ListView listView = rootVisualElement.Q<ListView>(className: "table-list");
			listView.onSelectionChange -= OnListSelectionChange;
		}

		private void Update()
		{
			if (!_doRefresh)
				return;

			CreateListView();
			_doRefresh = false;
		}

		private void CreateListView()
		{
			#region List View

			#region Assets

			var guids = AssetDatabase.FindAssets($"t:{nameof(TransitionTableSO)}");
			var guidsAmount = guids.Length;
			
			if (guidsAmount <= 0 && !hasHelpBox)
			{
				helpBox = rootVisualElement.Q<HelpBox>(className: "table-list-help-box");
				helpBox.messageType = HelpBoxMessageType.Info;
				helpBox.text = "No State Transition Table assets exist.";
				hasHelpBox = true;
				return;
			}
			
			if (hasHelpBox)
			{
				helpBox.RemoveFromHierarchy();
				hasHelpBox = false;
			}
			
			var assets = new TransitionTableSO[guidsAmount];
			var objects = new UnityObject[guidsAmount];
			for (var idx = 0; idx < guidsAmount; idx++)
			{
				assets[idx] =  AssetDatabase.LoadAssetAtPath<TransitionTableSO>( AssetDatabase.GUIDToAssetPath(guids[idx]));
				objects[idx] = assets[idx];
			}

			#endregion

			var listView = rootVisualElement.Q<ListView>(className: "table-list");
			
			
			
			

			
			
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
			listView.onSelectionChange -= OnListSelectionChange;
			listView.onSelectionChange += OnListSelectionChange;
			if (transitionTableEditor && transitionTableEditor.target)
				listView.selectedIndex = IndexOf(objects, transitionTableEditor.target);

			#endregion

			doRefresh = false;
			/*
			var assets = FindAssets();
			ListView listView = rootVisualElement.Q<ListView>(className: "table-list");

			listView.makeItem = null;
			listView.bindItem = null;

			listView.itemsSource = assets;
			listView.itemHeight = 16;
			string labelClass = $"label-{(EditorGUIUtility.isProSkin ? "pro" : "personal")}";
			listView.makeItem = () =>
			{
				var label = new Label();
				label.AddToClassList(labelClass);
				return label;
			};
			listView.bindItem = (element, i) => ((Label)element).text = assets[i].name;
			listView.selectionType = SelectionType.Single;

			listView.onSelectionChanged -= OnListSelectionChanged;
			listView.onSelectionChanged += OnListSelectionChanged;

			if (_transitionTableEditor && _transitionTableEditor.target)
				listView.selectedIndex = System.Array.IndexOf(assets, _transitionTableEditor.target);
			*/
		}

		private void OnListSelectionChange(IEnumerable<object> enumerable)
		{
			IMGUIContainer editor = rootVisualElement.Q<IMGUIContainer>(className: "table-editor");
			editor.onGUIHandler = null;

			if (list.Count == 0)
				return;

			var table = (TransitionTableSO)list[0];
			if (table == null)
				return;

			if (_transitionTableEditor == null)
				_transitionTableEditor = UnityEditor.Editor.CreateEditor(table, typeof(TransitionTableEditor));
			else if (_transitionTableEditor.target != table)
				UnityEditor.Editor.CreateCachedEditor(table, typeof(TransitionTableEditor), ref _transitionTableEditor);

			editor.onGUIHandler = () =>
			{
				if (!_transitionTableEditor.target)
				{
					editor.onGUIHandler = null;
					return;
				}

				ListView listView = rootVisualElement.Q<ListView>(className: "table-list");
				if ((Object)listView.selectedItem != _transitionTableEditor.target)
				{
					var i = listView.itemsSource.IndexOf(_transitionTableEditor.target);
					listView.selectedIndex = i;
					if (i < 0)
					{
						editor.onGUIHandler = null;
						return;
					}
				}

				_transitionTableEditor.OnInspectorGUI();
			};
		}


		/*private TransitionTableSO[] FindAssets(string s)
		{
			var guids = AssetDatabase.FindAssets($"t:{nameof(TransitionTableSO)}");
			var assets = new TransitionTableSO[guids.Length];
			for (int i = 0; i < guids.Length; i++)
				assets[i] = AssetDatabase.LoadAssetAtPath<TransitionTableSO>(AssetDatabase.GUIDToAssetPath(guids[i]));
			return assets;
		}*/
	}
}
/*
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
        private bool hasHelpBox;
        private Editor transitionTableEditor;
        private HelpBox helpBox;

        [MenuItem("Transition Table Editor", menuItem = "Tools/State Machine SO/Transition Table Editor")]
        internal static void Display()
        {
            if (_window == null) _window = GetWindow<TransitionTableEditorWindow>("Transition Table Editor");
            _window.Show();
        }

        private void OnEnable()
        {
            hasHelpBox = false;
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

            #region Initialize

            var guids = FindAssets($"t:{nameof(TransitionTableSO)}");
            var guidsAmount = guids.Length;

            #endregion

            if (guidsAmount <= 0 && !hasHelpBox)
            {
                #region Help Box

                helpBox = rootVisualElement.Q<HelpBox>(className: "table-list-help-box");
                helpBox.messageType = Info;
                helpBox.text = "No State Transition Table assets exist.";
                hasHelpBox = true;

                #endregion
            }
            else
            {
                #region Help Box Toggle

                if (hasHelpBox)
                {
                    helpBox.RemoveFromClassList("table-list-help-box");
                    hasHelpBox = false;
                }

                #endregion

                #region Initialize

                var assets = new TransitionTableSO[guidsAmount];
                var objects = new UnityObject[guidsAmount];
                for (var idx = 0; idx < guidsAmount; idx++)
                {
                    assets[idx] = LoadAssetAtPath<TransitionTableSO>(GUIDToAssetPath(guids[idx]));
                    objects[idx] = assets[idx];
                }

                #endregion

                #region List Transitions

                var listView = rootVisualElement.Q<ListView>(className: "table-list");
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

                #region Do Refresh Toggle

                doRefresh = false;

                #endregion
            }
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
*/
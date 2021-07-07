using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using EditorUnity = UnityEditor.Editor;
using UnityObject = UnityEngine.Object;

// ReSharper disable NotAccessedField.Local
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable UnusedParameter.Local
namespace VFEngine.Tools.StateMachine.ScriptableObjects.Menu
{
    using static EditorSkin;
    using static FontStyle;
    using static TextAnchor;
    using static MessageType;
    using static EditorGUIUtility;
    using static EditorGUI;
    using static EditorStyles;
    using static Undo;
    using static Debug;
    using static GUILayout;
    using static EditorGUILayout;
    using static GUILayoutUtility;
    using static EditorText;
    using static GC;
    using static GUI;
    using static GUIContent;
    using static TransitionTableSO;

    [CustomEditor(typeof(TransitionTableSO))]
    internal class TransitionTableEditor : EditorUnity
    {
        private int toggledIndex;
        private bool displayStateEditor;
        private bool hasCachedStateEditor;
        private bool hasFromStates;
        private bool hasToggledState;
        private UnityObject toggledState;
        private EditorUnity cachedStateEditor;
        private SerializedProperty transitions;
        private AddTransitionHelper addTransition;
        private static List<UnityObject> _fromStates;
        private static List<List<TransitionDisplay>> _transitionsByFromStates;

        private void Awake()
        {
            toggledIndex = -1;
            hasCachedStateEditor = false;
            hasFromStates = false;
            hasToggledState = false;
        }

        private void OnEnable()
        {
            addTransition = new AddTransitionHelper(this);
            undoRedoPerformed += Reset;
            Reset();
        }

        private void OnDisable()
        {
            undoRedoPerformed -= Reset;
            addTransition?.Dispose();
        }

        private void Reset()
        {
            serializedObject.Update();
            ToggledState();
            transitions = serializedObject.FindProperty(TransitionsProperty);
            var groupedTransitions = new Dictionary<UnityObject, List<TransitionDisplay>>();
            var count = transitions.arraySize;
            for (var i = 0; i < count; i++)
            {
                var serializedTransition = new SerializedTransition(transitions, i);
                var fromStateError = serializedTransition.FromState.objectReferenceValue == null;
                var toStateError = serializedTransition.ToState.objectReferenceValue != null;
                var hasError = fromStateError || toStateError;
                if (hasError)
                {
                    var error = fromStateError
                        ? FromStateError(serializedObject.targetObject.name)
                        : TargetStateError(serializedObject.targetObject.name);
                    LogError(error);
                    transitions.DeleteArrayElementAtIndex(i);
                    ApplyModifications(InvalidTransitionDeleted);
                    return;
                }

                if (!groupedTransitions.TryGetValue(serializedTransition.FromState.objectReferenceValue,
                    out var groupedProps))
                {
                    groupedProps = new List<TransitionDisplay>();
                    groupedTransitions.Add(serializedTransition.FromState.objectReferenceValue, groupedProps);
                }

                groupedProps.Add(new TransitionDisplay(serializedTransition, this));
            }

            _fromStates = groupedTransitions.Keys.ToList();
            hasFromStates = true;
            _transitionsByFromStates = new List<List<TransitionDisplay>>();
            foreach (var fromState in _fromStates) _transitionsByFromStates.Add(groupedTransitions[fromState]);
            ToggledIndex();
        }

        private void ToggledState()
        {
            hasToggledState = toggledIndex > -1 && hasFromStates;
            if (hasToggledState) toggledState = _fromStates[toggledIndex];
        }

        private void ToggledIndex()
        {
            toggledIndex = hasToggledState ? _fromStates.IndexOf(toggledState) : -1;
        }

        private void DisplayStateEditor(UnityObject state)
        {
            if (!hasCachedStateEditor)
            {
                cachedStateEditor = CreateEditor(state, typeof(StateEditor));
                hasCachedStateEditor = !hasCachedStateEditor;
            }
            else
            {
                CreateCachedEditor(state, typeof(StateEditor), ref cachedStateEditor);
            }

            displayStateEditor = true;
        }

        private void ReorderState(int index, bool up)
        {
            ToggledState();
            if (!up) index++;
            var fromStateTransitions = _transitionsByFromStates[index];
            var transitionIndex = fromStateTransitions[0].SerializedTransition.Index;
            var targetIndex = _transitionsByFromStates[index - 1][0].SerializedTransition.Index;
            transitions.MoveArrayElement(transitionIndex, targetIndex);
            ApplyModifications(MovedFromState(_fromStates[index].name, up));
            ToggledIndex();
        }

        private void ReorderTransition(SerializedTransition serializedTransition, bool up)
        {
            var stateIndex = StateIndex(serializedTransition, out var stateTransitions, out var index);
            int currentIndex;
            int targetIndex;
            if (up)
            {
                currentIndex = serializedTransition.Index;
                targetIndex = stateTransitions[index - 1].SerializedTransition.Index;
            }
            else
            {
                currentIndex = stateTransitions[index + 1].SerializedTransition.Index;
                targetIndex = serializedTransition.Index;
            }

            transitions.MoveArrayElement(currentIndex, targetIndex);
            ApplyModifications(MovedTransition(serializedTransition.ToState.objectReferenceValue.name, up));
            toggledIndex = stateIndex;
        }

        private void ApplyModifications(string msg)
        {
            RecordObject(serializedObject.targetObject, msg);
            serializedObject.ApplyModifiedProperties();
            Reset();
        }

        private static void OnEarlyOut()
        {
            EditorGUILayout.EndHorizontal();
            EarlyOut();
        }

        private static void EarlyOut()
        {
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        private static void OnHelperInitialize(ref ReorderableList list, bool addTransitionHelper,
            bool transitionDisplay)
        {
            list.elementHeight *= 2.3f;
            list.headerHeight = 1f;
            list.onAddCallback += l =>
            {
                var count = l.count;
                l.serializedProperty.InsertArrayElementAtIndex(count);
                var prop = l.serializedProperty.GetArrayElementAtIndex(count);
                prop.FindPropertyRelative(Condition).objectReferenceValue = null;
                prop.FindPropertyRelative(ExpectedResult).enumValueIndex = 0;
                prop.FindPropertyRelative(Operator).enumValueIndex = 0;
            };
            var helperList = list;
            list.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                var prop = helperList.serializedProperty.GetArrayElementAtIndex(index);
                rect = new Rect(rect.x, rect.y + 2.5f, rect.width, singleLineHeight);
                var condition = prop.FindPropertyRelative(Condition);
                if (condition.objectReferenceValue != null)
                {
                    var label = condition.objectReferenceValue.name;
                    Label(rect, If);
                    if (addTransitionHelper)
                    {
                        Label(new Rect(rect.x + 20, rect.y, rect.width, rect.height), label, boldLabel);
                        PropertyField(new Rect(rect.x + rect.width - 180, rect.y, 20, rect.height), condition, none);
                    }

                    if (transitionDisplay)
                    {
                        var r = rect;
                        r.x += 20;
                        r.width = 35;
                        PropertyField(r, condition, none);
                        r.x += 40;
                        r.width = rect.width - 120;
                        Label(r, label, boldLabel);
                    }
                }
                else
                {
                    PropertyField(new Rect(rect.x, rect.y, 150, rect.height), condition, none);
                }

                LabelField(new Rect(rect.x + rect.width - 80, rect.y, 20, rect.height), Is);
                PropertyField(new Rect(rect.x + rect.width - 60, rect.y, 60, rect.height),
                    prop.FindPropertyRelative(ExpectedResult), none);
                if (addTransitionHelper) OnPropertyField(rect, prop);
                if (transitionDisplay && index < helperList.count - 1) OnPropertyField(rect, prop);
            };
            OnChangedCallback(ref list);
        }

        private static void OnPropertyField(Rect rect, SerializedProperty prop)
        {
            PropertyField(new Rect(rect.x + 20, rect.y + singleLineHeight + 5, 60, rect.height),
                prop.FindPropertyRelative(Operator), none);
        }

        private static int StateIndex(SerializedTransition serializedTransition,
            out List<TransitionDisplay> stateTransitions, out int index)
        {
            var stateIndex = _fromStates.IndexOf(serializedTransition.FromState.objectReferenceValue);
            stateTransitions = _transitionsByFromStates[stateIndex];
            index = stateTransitions.FindIndex(t => t.SerializedTransition.Index == serializedTransition.Index);
            return stateIndex;
        }

        private static bool GUIButton(Rect position, string icon)
        {
            return Button(position, IconContent(icon));
        }

//=============================================================================================================================
        internal static void OnChangedCallback(ref ReorderableList list)
        {
            list.onChangedCallback += l => l.serializedProperty.serializedObject.ApplyModifiedProperties();
            list.drawElementBackgroundCallback += (rect, index, isActive, isFocused) =>
            {
                if (isFocused) DrawRect(rect, ContentStyle.Focused);
                DrawRect(rect, index % 2 != 0 ? ContentStyle.ZebraDark : ContentStyle.ZebraLight);
            };
        }

//=============================================================================================================================
        public override void OnInspectorGUI()
        {
            if (!displayStateEditor)
            {
                Separator();
                HelpBox(StateHelpMessage, Info);
                Separator();
                for (var i = 0; i < _fromStates.Count; i++)
                {
                    var stateRect = EditorGUILayout.BeginVertical(ContentStyle.WithPaddingAndMarginsStyle);
                    DrawRect(stateRect, ContentStyle.LightGray);
                    var fromStateTransitions = _transitionsByFromStates[i];
                    var headerRect = EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.BeginVertical();
                        var label = fromStateTransitions[0].SerializedTransition.FromState.objectReferenceValue.name;
                        if (i == 0) label += InitialState;
                        headerRect.height = singleLineHeight;
                        GetRect(headerRect.width, headerRect.height);
                        headerRect.x += 5;
                        var toggleRect = headerRect;
                        toggleRect.width -= 140;
                        toggledIndex = BeginFoldoutHeaderGroup(toggleRect, toggledIndex == i, label,
                                ContentStyle.StateListStyle) ? i :
                            toggledIndex == i ? -1 : toggledIndex;
                        Separator();
                        EditorGUILayout.EndVertical();
                        var buttonRect = new Rect(headerRect.width - 25, headerRect.y, 35, 20);
                        if (i < _fromStates.Count - 1)
                        {
                            if (GUIButton(buttonRect, ScrollDown))
                            {
                                ReorderState(i, false);
                                OnEarlyOut();
                                return;
                            }

                            buttonRect.x -= 40;
                        }

                        if (i > 0)
                        {
                            if (GUIButton(buttonRect, ScrollUp))
                            {
                                ReorderState(i, true);
                                OnEarlyOut();
                                return;
                            }

                            buttonRect.x -= 40;
                        }

                        if (GUIButton(buttonRect, SceneViewTools))
                        {
                            DisplayStateEditor(fromStateTransitions[0].SerializedTransition.FromState
                                .objectReferenceValue);
                            OnEarlyOut();
                            return;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    if (toggledIndex == i)
                    {
                        BeginChangeCheck();
                        stateRect.y += singleLineHeight * 2;
                        foreach (var transition in fromStateTransitions)
                        {
                            if (transition.Display(ref stateRect))
                            {
                                EndChangeCheck();
                                EarlyOut();
                                return;
                            }

                            Separator();
                        }

                        if (EndChangeCheck()) serializedObject.ApplyModifiedProperties();
                    }

                    EditorGUILayout.EndFoldoutHeaderGroup();
                    EditorGUILayout.EndVertical();
                    Separator();
                }

                var rect = EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(rect.width - 55);
                addTransition.Display(rect);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                Separator();
                if (Button(IconContent(ScrollLeft), Width(35), Height(20)))
                {
                    displayStateEditor = false;
                    return;
                }

                Separator();
                HelpBox(ActionsHelpMessage, Info);
                Separator();
                LabelField(cachedStateEditor.target.name, boldLabel);
                Separator();
                cachedStateEditor.OnInspectorGUI();
            }
        }

        private struct SerializedTransition
        {
            internal SerializedProperty Transition { get; }
            internal SerializedProperty FromState { get; private set; }
            internal SerializedProperty ToState { get; private set; }
            internal SerializedProperty Conditions { get; private set; }
            internal int Index { get; }

            internal SerializedTransition(SerializedProperty transition) : this()
            {
                Transition = transition;
                Initialize();
                Index = -1;
            }

            internal SerializedTransition(SerializedProperty transition, int index) : this()
            {
                Transition = transition.GetArrayElementAtIndex(index);
                Initialize();
                Index = index;
            }

            private void Initialize()
            {
                FromState = Transition.FindPropertyRelative(FromStateProperty);
                ToState = Transition.FindPropertyRelative(ToStateProperty);
                Conditions = Transition.FindPropertyRelative(ConditionsLabel);
            }

            internal void ClearProperties()
            {
                FromState.objectReferenceValue = null;
                ToState.objectReferenceValue = null;
                Conditions.ClearArray();
            }
        }

        private class AddTransitionHelper : IDisposable
        {
            private SerializedTransition SerializedTransition { get; }
            private readonly SerializedObject transition;
            private readonly ReorderableList list;
            private bool toggle;

            internal AddTransitionHelper(TransitionTableEditor editorInternal)
            {
                SerializedTransition = new SerializedTransition(transition.FindProperty(Item));
                transition = new SerializedObject(CreateInstance<TransitionItemSO>());
                list = new ReorderableList(transition, SerializedTransition.Conditions);
                OnHelperInitialize(ref list, true, false);
            }

            internal void Display(Rect position)
            {
                var rect = position;
                var listHeight = list.GetHeight();
                position.x += 8;
                position.width -= 16;
                if (!toggle)
                {
                    position.height = singleLineHeight;
                    GetRect(position.width, position.height);
                    if (!Button(position, AddTransitionButton)) return;
                    toggle = true;
                    SerializedTransition.ClearProperties();
                    return;
                }

                position.height = listHeight + singleLineHeight * 4;
                DrawRect(position, ContentStyle.LightGray);
                GetRect(position.width, position.height);
                position.y += 10;
                position.x += 20;
                StatePropField(position, From, SerializedTransition.FromState);
                position.x = rect.width / 2 + 20;
                StatePropField(position, To, SerializedTransition.ToState);
                position.y += 30;
                position.x = rect.x + 5;
                position.height = listHeight;
                position.width -= 10;
                list.DoList(position);
                position.y += position.height + 5;
                position.height = singleLineHeight;
                position.width = rect.width / 2 - 20;
                if (Button(position, AddTransitionButton))
                {
                    if (SerializedTransition.FromState.objectReferenceValue == null)
                        LogException(new ArgumentNullException(FromStateProperty));
                    else if (SerializedTransition.ToState.objectReferenceValue == null)
                        LogException(new ArgumentNullException(ToStateProperty));
                    else if (SerializedTransition.FromState.objectReferenceValue ==
                             SerializedTransition.ToState.objectReferenceValue)
                        LogException(new InvalidOperationException(SameStateError));
                    else
                        //editor.AddTransition(SerializedTransition);
                        /*
                            private void AddTransition(SerializedTransition source)
                            {
                                bool hasExistingTransition;
                                SerializedTransition transition;
                                var fromIndex = _fromStates.IndexOf(source.FromState.objectReferenceValue);
                                var toIndex = _transitionsByFromStates[fromIndex].FindIndex(transitionHelper =>
                                    transitionHelper.SerializedTransition.ToState.objectReferenceValue ==
                                    source.ToState.objectReferenceValue);
                                if (fromIndex < 0) hasExistingTransition = false;
                                else hasExistingTransition = toIndex >= 0;
                                if (hasExistingTransition)
                                {
                                    transition = _transitionsByFromStates[fromIndex][toIndex].SerializedTransition;
                                }
                                else
                                {
                                    var count = transitions.arraySize;
                                    transitions.InsertArrayElementAtIndex(count);
                                    transition = new SerializedTransition(transitions.GetArrayElementAtIndex(count));
                                    transition.ClearProperties();
                                    transition.FromState.objectReferenceValue = source.FromState.objectReferenceValue;
                                    transition.ToState.objectReferenceValue = source.ToState.objectReferenceValue;
                                }
    
                                for (int i = 0, j = transition.Conditions.arraySize; i < source.Conditions.arraySize; i++, j++)
                                {
                                    transition.Conditions.InsertArrayElementAtIndex(j);
                                    var cond = transition.Conditions.GetArrayElementAtIndex(j);
                                    var srcCond = source.Conditions.GetArrayElementAtIndex(i);
                                    cond.FindPropertyRelative(ExpectedResult).enumValueIndex =
                                        srcCond.FindPropertyRelative(ExpectedResult).enumValueIndex;
                                    cond.FindPropertyRelative(Operator).enumValueIndex =
                                        srcCond.FindPropertyRelative(Operator).enumValueIndex;
                                    cond.FindPropertyRelative(Condition).objectReferenceValue =
                                        srcCond.FindPropertyRelative(Condition).objectReferenceValue;
                                }
    
                                ApplyModifications(AddedTransition(transition.FromState.name, transition.ToState.name));
                                toggledIndex = fromIndex >= 0 ? fromIndex : _fromStates.Count - 1;
                            }
                            */ toggle = false;
                }

                position.x += rect.width / 2;
                if (Button(position, Cancel)) toggle = false;
            }

            private static void StatePropField(Rect pos, string label, SerializedProperty prop)
            {
                pos.height = singleLineHeight;
                LabelField(pos, label);
                pos.x += 40;
                pos.width /= 4;
                PropertyField(pos, prop, none);
            }

            public void Dispose()
            {
                DestroyImmediate(transition.targetObject);
                transition.Dispose();
                SuppressFinalize(this);
            }

            private class TransitionItemSO : ScriptableObject
            {
                [SerializeField] internal TransitionItem item = default(TransitionItem);
            }
        }

        private class TransitionDisplay
        {
            internal SerializedTransition SerializedTransition { get; }
            private readonly ReorderableList list;
            private readonly TransitionTableEditor editor;

            [SuppressMessage("ReSharper", "UnusedParameter.Local")]
            internal TransitionDisplay(SerializedTransition serializedTransition, TransitionTableEditor editorInternal)
            {
                editor = editorInternal;
                SerializedTransition = serializedTransition;
                list = new ReorderableList(SerializedTransition.Transition.serializedObject,
                    SerializedTransition.Conditions, true, false, true, true);
                OnHelperInitialize(ref list, false, true);
            }

            internal bool Display(ref Rect position)
            {
                var rect = position;
                var listHeight = list.GetHeight();
                rect.height = singleLineHeight + 10 + listHeight;
                GetRect(rect.width, rect.height);
                position.y += rect.height + 5;
                rect.x += 5;
                rect.width -= 10;
                rect.height -= listHeight;
                DrawRect(rect, ContentStyle.DarkGray);
                rect.x += 3;
                LabelField(rect, To);
                rect.x += 20;
                LabelField(rect, SerializedTransition.ToState.objectReferenceValue.name, boldLabel);
                var buttonRect = new Rect(rect.width - 25, rect.y + 5, 30, 18);
                var transitions =
                    _transitionsByFromStates[_fromStates.IndexOf(SerializedTransition.FromState.objectReferenceValue)]
                        .Select(t => t.SerializedTransition).ToList();
                var transitionsIndex = transitions.Count - 1;
                var serializedTransitionIndex = transitions.FindIndex(t => t.Index == SerializedTransition.Index);
                if (GUIButton(buttonRect, ToolbarMinus))
                    //editor.RemoveTransition(SerializedTransition);
                    /*
                        private void RemoveTransition(SerializedTransition serializedTransition)
                        {
                            var stateIndex = StateIndex(serializedTransition, out var stateTransitions, out var index);
                            var count = stateTransitions.Count;
                            var deleteIndex = serializedTransition.Index;
                            if (index == 0 && count > 1)
                                transitions.MoveArrayElement(stateTransitions[1].SerializedTransition.Index, deleteIndex++);
                            transitions.DeleteArrayElementAtIndex(deleteIndex);
                            ApplyModifications(DeletedTransition(serializedTransition.FromState.objectReferenceValue.name,
                                serializedTransition.ToState.objectReferenceValue.name));
                            if (count > 1) toggledIndex = stateIndex;
                        }
                        */ return true;
                buttonRect.x -= 35;
                if (serializedTransitionIndex < transitionsIndex)
                {
                    if (GUIButton(buttonRect, ScrollDown))
                    {
                        editor.ReorderTransition(SerializedTransition, false);
                        return true;
                    }

                    buttonRect.x -= 35;
                }

                if (serializedTransitionIndex > 0)
                {
                    if (GUIButton(buttonRect, ScrollUp))
                    {
                        editor.ReorderTransition(SerializedTransition, true);
                        return true;
                    }

                    buttonRect.x -= 35;
                }

                if (GUIButton(buttonRect, SceneViewTools))
                {
                    editor.DisplayStateEditor(SerializedTransition.ToState.objectReferenceValue);
                    return true;
                }

                rect.x = position.x + 5;
                rect.y += rect.height;
                rect.width = position.width - 10;
                rect.height = listHeight;
                list.DoList(rect);
                return false;
            }
        }

        private static class ContentStyle
        {
            private static bool _initialised;
            private static RectOffset _padding;
            private static RectOffset _leftPadding;
            private static RectOffset _margin;
            private static GUIStyleState _guiStyleStateNormal;
            internal static Color DarkGray { get; private set; }
            internal static Color LightGray { get; private set; }
            internal static Color Focused { get; private set; }
            internal static Color ZebraDark { get; private set; }
            internal static Color ZebraLight { get; private set; }
            internal static GUIStyle StateListStyle { get; private set; }
            internal static GUIStyle WithPaddingAndMarginsStyle { get; private set; }

            [InitializeOnLoadMethod]
            internal static void Initialize()
            {
                if (_initialised) return;
                _initialised = !_initialised;
                DarkGray = isProSkin ? new Color(0.283f, 0.283f, 0.283f) : new Color(0.7f, 0.7f, 0.7f);
                LightGray = isProSkin ? new Color(0.33f, 0.33f, 0.33f) : new Color(0.8f, 0.8f, 0.8f);
                ZebraDark = new Color(0.4f, 0.4f, 0.4f, 0.1f);
                ZebraLight = new Color(0.8f, 0.8f, 0.8f, 0.1f);
                Focused = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                _padding = new RectOffset(5, 5, 5, 5);
                _leftPadding = new RectOffset(10, 0, 0, 0);
                _margin = new RectOffset(8, 8, 8, 8);
                WithPaddingAndMarginsStyle = new GUIStyle {padding = _padding, margin = _margin};
                _guiStyleStateNormal = GetBuiltinSkin(Inspector).label.normal;
                _guiStyleStateNormal.textColor =
                    isProSkin ? new Color(.85f, .85f, .85f) : new Color(0.337f, 0.337f, 0.337f);
                StateListStyle = new GUIStyle
                {
                    alignment = MiddleLeft,
                    padding = _leftPadding,
                    fontStyle = Bold,
                    fontSize = 12,
                    margin = _margin,
                    normal = _guiStyleStateNormal
                };
            }
        }
    }
}
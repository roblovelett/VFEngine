using System;
using System.Collections.Generic;
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
    using static MessageType;
    using static List;
    using static EditorGUIUtility;
    using static EditorGUI;
    using static EditorStyles;
    using static Undo;
    using static Debug;
    using static GUILayout;
    using static EditorGUILayout;
    using static GUILayoutUtility;
    using static EditorText;
    using static GUI;
    using static GUIContent;
    using static TransitionTableSO;

    [CustomEditor(typeof(TransitionTableSO))]
    internal class TransitionTableEditor : EditorUnity
    {
        private bool displayStateEditor;
        private bool hasCachedStateEditor;
        private bool hasFromStates;
        private bool hasToggledState;
        private bool addedToggle;
        private UnityObject toggledState;
        private EditorUnity cachedStateEditor;
        private SerializedTransition addedSerializedTransition;
        private SerializedObject addedTransition;
        private ReorderableList addedList;
        private static int _toggledIndex;
        private static SerializedProperty _transitions;
        private static List<UnityObject> _fromStates;
        private static List<List<TransitionDisplay>> _transitionsByFromStates;

        private void Awake()
        {
            _toggledIndex = -1;
            hasCachedStateEditor = false;
            hasFromStates = false;
            hasToggledState = false;
            addedTransition = new SerializedObject(CreateInstance<TransitionItemSO>());
            addedSerializedTransition = new SerializedTransition(addedTransition.FindProperty(Item));
            addedList = new ReorderableList(addedTransition, addedSerializedTransition.Conditions);
            addedToggle = false;
            OnHelperInitialize(ref addedList, true, false);
        }

        private void OnEnable()
        {
            undoRedoPerformed += Reset;
            Reset();
        }

        private void OnDisable()
        {
            undoRedoPerformed -= Reset;
        }

        private void Reset()
        {
            serializedObject.Update();
            ToggledState();
            _transitions = serializedObject.FindProperty(TransitionsProperty);
            var groupedTransitions = new Dictionary<UnityObject, List<TransitionDisplay>>();
            var count = _transitions.arraySize;
            for (var i = 0; i < count; i++)
            {
                var serializedTransition = new SerializedTransition(_transitions, i);
                var fromStateError = serializedTransition.FromState.objectReferenceValue == null;
                var toStateError = serializedTransition.ToState.objectReferenceValue != null;
                var hasError = fromStateError || toStateError;
                if (hasError)
                {
                    var error = fromStateError
                        ? FromStateError(serializedObject.targetObject.name)
                        : TargetStateError(serializedObject.targetObject.name);
                    LogError(error);
                    _transitions.DeleteArrayElementAtIndex(i);
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
            hasToggledState = _toggledIndex > -1 && hasFromStates;
            if (hasToggledState) toggledState = _fromStates[_toggledIndex];
        }

        private void ToggledIndex()
        {
            _toggledIndex = hasToggledState ? _fromStates.IndexOf(toggledState) : -1;
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
            _transitions.MoveArrayElement(transitionIndex, targetIndex);
            ApplyModifications(MovedFromState(_fromStates[index].name, up));
            ToggledIndex();
        }

        private void ReorderTransition(SerializedTransition serializedTransition, bool up)
        {
            OnModifyTransition(serializedTransition, out var stateIndex, out var stateTransitions, out var index);
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

            _transitions.MoveArrayElement(currentIndex, targetIndex);
            ApplyModifications(MovedTransition(serializedTransition.ToState.objectReferenceValue.name, up));
            _toggledIndex = stateIndex;
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

        private static void StatePropField(Rect pos, string label, SerializedProperty prop)
        {
            pos.height = singleLineHeight;
            LabelField(pos, label);
            pos.x += 40;
            pos.width /= 4;
            PropertyField(pos, prop, none);
        }

        private static void OnPropertyField(Rect rect, SerializedProperty prop)
        {
            PropertyField(new Rect(rect.x + 20, rect.y + singleLineHeight + 5, 60, rect.height),
                prop.FindPropertyRelative(Operator), none);
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

        private static void OnModifyTransition(SerializedTransition serializedTransition, out int stateIndex,
            out List<TransitionDisplay> stateTransitions, out int index)
        {
            stateIndex = _fromStates.IndexOf(serializedTransition.FromState.objectReferenceValue);
            stateTransitions = _transitionsByFromStates[stateIndex];
            index = stateTransitions.FindIndex(t => t.SerializedTransition.Index == serializedTransition.Index);
        }

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
                        _toggledIndex = BeginFoldoutHeaderGroup(toggleRect, _toggledIndex == i, label,
                                ContentStyle.StateListStyle) ? i :
                            _toggledIndex == i ? -1 : _toggledIndex;
                        Separator();
                        EditorGUILayout.EndVertical();
                        var buttonRect = new Rect(headerRect.width - 25, headerRect.y, 35, 20);
                        if (i < _fromStates.Count - 1)
                        {
                            if (Button(buttonRect, IconContent(ScrollDown)))
                            {
                                ReorderState(i, false);
                                OnEarlyOut();
                                return;
                            }

                            buttonRect.x -= 40;
                        }

                        if (i > 0)
                        {
                            if (Button(buttonRect, IconContent(ScrollUp)))
                            {
                                ReorderState(i, true);
                                OnEarlyOut();
                                return;
                            }

                            buttonRect.x -= 40;
                        }

                        if (Button(buttonRect, IconContent(SceneViewTools)))
                        {
                            DisplayStateEditor(fromStateTransitions[0].SerializedTransition.FromState
                                .objectReferenceValue);
                            OnEarlyOut();
                            return;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    if (_toggledIndex == i)
                    {
                        BeginChangeCheck();
                        stateRect.y += singleLineHeight * 2;
                        var stateChanged = false;
                        foreach (var transition in fromStateTransitions)
                        {
                            if (stateChanged)
                            {
                                EndChangeCheck();
                                EarlyOut();
                                return;
                            }

                            // Display Helper
                            var displayedStateRect = stateRect;
                            var listHeight = transition.List.GetHeight();
                            displayedStateRect.height = singleLineHeight + 10 + listHeight;
                            GetRect(displayedStateRect.width, displayedStateRect.height);
                            stateRect.y += displayedStateRect.height + 5;
                            displayedStateRect.x += 5;
                            displayedStateRect.width -= 10;
                            displayedStateRect.height -= listHeight;
                            DrawRect(displayedStateRect, ContentStyle.DarkGray);
                            displayedStateRect.x += 3;
                            LabelField(displayedStateRect, To);
                            displayedStateRect.x += 20;
                            LabelField(displayedStateRect,
                                transition.SerializedTransition.ToState.objectReferenceValue.name, boldLabel);
                            var buttonRect = new Rect(displayedStateRect.width - 25, displayedStateRect.y + 5, 30, 18);
                            var fromState = transition.SerializedTransition.FromState.objectReferenceValue;
                            var transitions = _transitionsByFromStates[_fromStates.IndexOf(fromState)]
                                .Select(t => t.SerializedTransition).ToList();
                            var transitionsIndex = transitions.Count - 1;
                            var serializedTransitionIndex =
                                transitions.FindIndex(t => t.Index == transition.SerializedTransition.Index);
                            if (Button(buttonRect, IconContent(ToolbarMinus)))
                            {
                                // Remove Transition
                                var serializedTransition = transition.SerializedTransition;
                                OnModifyTransition(serializedTransition, out var stateIndex, out var stateTransitions,
                                    out var index);
                                var count = stateTransitions.Count;
                                var deleteIndex = transition.SerializedTransition.Index;
                                if (index == 0 && count > 1)
                                    _transitions.MoveArrayElement(stateTransitions[1].SerializedTransition.Index,
                                        deleteIndex++);
                                _transitions.DeleteArrayElementAtIndex(deleteIndex);
                                ApplyModifications(DeletedTransition(
                                    serializedTransition.FromState.objectReferenceValue.name,
                                    serializedTransition.ToState.objectReferenceValue.name));
                                if (count > 1) _toggledIndex = stateIndex;
                                stateChanged = true;
                                continue;
                            }

                            buttonRect.x -= 35;
                            if (serializedTransitionIndex < transitionsIndex)
                            {
                                if (Button(buttonRect, IconContent(ScrollDown)))
                                {
                                    transition.Editor.ReorderTransition(transition.SerializedTransition, false);
                                    stateChanged = true;
                                    continue;
                                }

                                buttonRect.x -= 35;
                            }

                            if (serializedTransitionIndex > 0)
                            {
                                if (Button(buttonRect, IconContent(ScrollUp)))
                                {
                                    transition.Editor.ReorderTransition(transition.SerializedTransition, true);
                                    stateChanged = true;
                                    continue;
                                }

                                buttonRect.x -= 35;
                            }

                            if (Button(buttonRect, IconContent(SceneViewTools)))
                            {
                                transition.Editor.DisplayStateEditor(transition.SerializedTransition.ToState
                                    .objectReferenceValue);
                                stateChanged = true;
                                continue;
                            }

                            displayedStateRect.x = stateRect.x + 5;
                            displayedStateRect.y += displayedStateRect.height;
                            displayedStateRect.width = stateRect.width - 10;
                            displayedStateRect.height = listHeight;
                            transition.List.DoList(displayedStateRect);
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

                #region Add Transition Button

                var position = rect;
                position.x += 8;
                position.width -= 16;
                var addedRect = position;
                var addedListHeight = addedList.GetHeight();
                switch (addedToggle)
                {
                    case false:
                    {
                        position.height = singleLineHeight;
                        GetRect(position.width, position.height);
                        if (Button(position, AddTransitionButton))
                        {
                            addedToggle = true;
                            addedSerializedTransition.ClearProperties();
                        }

                        break;
                    }
                    case true:
                    {
                        position.height = addedListHeight + singleLineHeight * 4;
                        DrawRect(position, ContentStyle.LightGray);
                        GetRect(position.width, position.height);
                        position.y += 10;
                        position.x += 20;
                        StatePropField(position, From, addedSerializedTransition.FromState);
                        position.x = addedRect.width / 2 + 20;
                        StatePropField(position, To, addedSerializedTransition.ToState);
                        position.y += 30;
                        position.x = addedRect.x + 5;
                        position.height = addedListHeight;
                        position.width -= 10;
                        addedList.DoList(position);
                        position.y += position.height + 5;
                        position.height = singleLineHeight;
                        position.width = addedRect.width / 2 - 20;
                        if (Button(position, AddTransitionButton))
                        {
                            if (addedSerializedTransition.FromState.objectReferenceValue == null)
                            {
                                LogException(new ArgumentNullException(FromStateProperty));
                            }
                            else if (addedSerializedTransition.ToState.objectReferenceValue == null)
                            {
                                LogException(new ArgumentNullException(ToStateProperty));
                            }
                            else if (addedSerializedTransition.FromState.objectReferenceValue ==
                                     addedSerializedTransition.ToState.objectReferenceValue)
                            {
                                LogException(new InvalidOperationException(SameStateError));
                            }
                            else
                            {
                                bool hasExistingTransition;
                                SerializedTransition serializedTransition;
                                var fromIndex =
                                    _fromStates.IndexOf(addedSerializedTransition.FromState.objectReferenceValue);
                                var toIndex = _transitionsByFromStates[fromIndex].FindIndex(transitionHelper =>
                                    transitionHelper.SerializedTransition.ToState.objectReferenceValue ==
                                    addedSerializedTransition.ToState.objectReferenceValue);
                                if (fromIndex < 0) hasExistingTransition = false;
                                else hasExistingTransition = toIndex >= 0;
                                if (hasExistingTransition)
                                {
                                    serializedTransition = _transitionsByFromStates[fromIndex][toIndex]
                                        .SerializedTransition;
                                }
                                else
                                {
                                    var count = _transitions.arraySize;
                                    _transitions.InsertArrayElementAtIndex(count);
                                    serializedTransition =
                                        new SerializedTransition(_transitions.GetArrayElementAtIndex(count));
                                    serializedTransition.ClearProperties();
                                    serializedTransition.FromState.objectReferenceValue =
                                        addedSerializedTransition.FromState.objectReferenceValue;
                                    serializedTransition.ToState.objectReferenceValue =
                                        addedSerializedTransition.ToState.objectReferenceValue;
                                }

                                for (int i = 0, j = serializedTransition.Conditions.arraySize;
                                    i < addedSerializedTransition.Conditions.arraySize;
                                    i++, j++)
                                {
                                    serializedTransition.Conditions.InsertArrayElementAtIndex(j);
                                    var cond = serializedTransition.Conditions.GetArrayElementAtIndex(j);
                                    var srcCond = addedSerializedTransition.Conditions.GetArrayElementAtIndex(i);
                                    cond.FindPropertyRelative(ExpectedResult).enumValueIndex =
                                        srcCond.FindPropertyRelative(ExpectedResult).enumValueIndex;
                                    cond.FindPropertyRelative(Operator).enumValueIndex =
                                        srcCond.FindPropertyRelative(Operator).enumValueIndex;
                                    cond.FindPropertyRelative(Condition).objectReferenceValue =
                                        srcCond.FindPropertyRelative(Condition).objectReferenceValue;
                                }

                                ApplyModifications(AddedTransition(serializedTransition.FromState.name,
                                    serializedTransition.ToState.name));
                                _toggledIndex = fromIndex >= 0 ? fromIndex : _fromStates.Count - 1;
                                addedToggle = false;
                            }
                        }

                        position.x += addedRect.width / 2;
                        if (Button(position, Cancel)) addedToggle = false;
                        break;
                    }
                }

                EditorGUILayout.EndHorizontal();

                #endregion
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

        private class TransitionItemSO : ScriptableObject
        {
            [SerializeField] internal TransitionItem item = default(TransitionItem);
        }

        private class TransitionDisplay
        {
            internal SerializedTransition SerializedTransition { get; }
            internal readonly ReorderableList List;
            internal readonly TransitionTableEditor Editor;

            internal TransitionDisplay(SerializedTransition serializedTransition, TransitionTableEditor editorInternal)
            {
                Editor = editorInternal;
                SerializedTransition = serializedTransition;
                List = new ReorderableList(SerializedTransition.Transition.serializedObject,
                    SerializedTransition.Conditions, true, false, true, true);
                OnHelperInitialize(ref List, false, true);
            }
        }
    }
}
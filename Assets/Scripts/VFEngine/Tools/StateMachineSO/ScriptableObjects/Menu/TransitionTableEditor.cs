
/*
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityObject = UnityEngine.Object;

// ReSharper disable UnusedParameter.Local
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace VFEngine.Tools.StateMachineSO.ScriptableObjects.Menu
{
    using static MessageType;
    using static EditorSkin;
    using static FontStyle;
    using static TextAnchor;
    using static EditorGUI;
    using static GUI;
    using static GUILayoutUtility;
    using static EditorGUILayout;
    using static EditorStyles;
    using static Undo;
    using static Debug;
    using static EditorGUIUtility;
    using static GUIContent;
    using static GUILayout;
    using static TransitionTableSO;

    [CustomEditor(typeof(TransitionTableSO))]
    internal class TransitionTableEditor : Editor
    {
        private int toggledIndex = -1;
        private bool displayStateEditor;
        private bool hasCachedStateEditor;
        private bool hasAddedTransitions;
        private bool addToggle;
        private Editor cachedStateEditor;
        private ReorderableList createdTransitions;
        private SerializedObject newTransition;
        private SerializedProperty transitions;
        private SerializedTransition serializedNewTransition;
        private static List<List<TransitionDisplay>> _transitionsByFromStates;
        private static List<UnityObject> _fromStates;

        private void OnEnable()
        {
            hasCachedStateEditor = false;
            hasAddedTransitions = false;
            undoRedoPerformed += Reset;
            Reset();
        }

        private void OnDisable()
        {
            undoRedoPerformed -= Reset;
        }

        internal void Reset()
        {
            Debug.Log("Reset.");
            serializedObject.Update();
            var toggledState = toggledIndex > -1 ? _fromStates[toggledIndex] : null;
            transitions = serializedObject.FindProperty("transitions");

            #region Group By FromState

            var groupedTransitions = new Dictionary<UnityObject, List<TransitionDisplay>>();
            var count = transitions.arraySize;
            for (var i = 0; i < count; i++)
            {
                var serializedTransition = new SerializedTransition(transitions, i);
                if (serializedTransition.FromState.objectReferenceValue == null)
                {
                    LogError("Transition with invalid \"From State\" found in table " +
                             serializedObject.targetObject.name + ", deleting...");
                    transitions.DeleteArrayElementAtIndex(i);
                    ApplyModifications("Invalid transition deleted");
                    return;
                }

                if (serializedTransition.ToState.objectReferenceValue == null)
                {
                    LogError("Transition with invalid \"Target State\" found in table " +
                             serializedObject.targetObject.name + ", deleting...");
                    transitions.DeleteArrayElementAtIndex(i);
                    ApplyModifications("Invalid transition deleted");
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
            _transitionsByFromStates = new List<List<TransitionDisplay>>();
            foreach (var fromState in _fromStates) _transitionsByFromStates.Add(groupedTransitions[fromState]);

            #endregion

            toggledIndex = toggledState ? _fromStates.IndexOf(toggledState) : -1;
        }

        public override void OnInspectorGUI()
        {
            if (!displayStateEditor)
            {
                #region Transition Table GUI

                Separator();
                HelpBox(
                    "Click on any State's name to see the Transitions it contains, or click the Pencil/Wrench icon to see its Actions.",
                    Info);
                Separator();
                for (var i = 0; i < _fromStates.Count; i++)
                {
                    var stateTransition = EditorGUILayout.BeginVertical(ContentStyle.WithPaddingAndMargins);
                    var fromStatesTransitions = _transitionsByFromStates[i];

                    #region State Header

                    var headerRect = EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.BeginVertical();
                        var label = fromStatesTransitions[0].SerializedDisplayedTransition.FromState
                            .objectReferenceValue.name;
                        if (i == 0) label += " (Initial State)";
                        headerRect.height = singleLineHeight;
                        GetRect(headerRect.width, headerRect.height);
                        headerRect.x += 5;

                        #region Toggle

                        var toggleRect = headerRect;
                        toggleRect.width -= 140;
                        toggledIndex = BeginFoldoutHeaderGroup(toggleRect, toggledIndex == i, label,
                                ContentStyle.StateListStyle) ? i :
                            toggledIndex == i ? -1 : toggledIndex;

                        #endregion

                        Separator();
                        EditorGUILayout.EndVertical();

                        #region State Header Buttons

                        var buttonRect = new Rect(headerRect.width - 25, headerRect.y, 35, 20);

                        #region Move State Down

                        if (i < _fromStates.Count - 1)
                        {
                            if (Button(buttonRect, IconContent("scrolldown")))
                            {
                                ReorderState(i, false);
                                EarlyOut();
                                return;
                            }

                            buttonRect.x -= 40;
                        }

                        #endregion

                        #region Move State Up

                        if (i > 0)
                        {
                            if (Button(buttonRect, IconContent("scrollup")))
                            {
                                ReorderState(i, true);
                                EarlyOut();
                                return;
                            }

                            buttonRect.x -= 40;
                        }

                        #endregion

                        #region Switch To State Editor

                        if (Button(buttonRect, IconContent("SceneViewTools")))
                        {
                            DisplayStateEditor(fromStatesTransitions[0].SerializedDisplayedTransition.FromState
                                .objectReferenceValue);
                            EarlyOut();
                            return;
                        }

                        #endregion

                        #endregion
                    }
                    EditorGUILayout.EndHorizontal();

                    #endregion

                    #region State Change Toggle

                    if (toggledIndex == i)
                    {
                        BeginChangeCheck();
                        stateTransition.y += singleLineHeight * 2;

                        #region Display State Transitions

                        foreach (var transition in fromStatesTransitions)
                        {
                            #region Return Changed States

                            #region Can Display State Transition

                            bool DisplayTransition()
                            {
                                var rect = stateTransition;
                                var reorderableList = transition.DisplayedTransitions;
                                var serializedTransition = transition.SerializedDisplayedTransition;
                                var listHeight = reorderableList.GetHeight();

                                #region Reserve Space

                                rect.height = singleLineHeight + 10 + listHeight;
                                GetRect(rect.width, rect.height);
                                stateTransition.y += rect.height + 5;

                                #endregion

                                #region Background

                                rect.x += 5;
                                rect.width -= 10;
                                rect.height -= listHeight;
                                DrawRect(rect, ContentStyle.DarkGray);

                                #endregion

                                #region Transition Header

                                rect.x += 3;
                                LabelField(rect, "To");
                                rect.x += 20;
                                LabelField(rect, serializedTransition.ToState.objectReferenceValue.name, boldLabel);

                                #endregion

                                #region Buttons

                                var buttonRect = new Rect(rect.width - 25, rect.y + 5, 30, 18);
                                var displayedTransitions =
                                    _transitionsByFromStates[
                                            _fromStates.IndexOf(serializedTransition.FromState.objectReferenceValue)]
                                        .Select(t => t.SerializedDisplayedTransition).ToList();
                                var displayedTransitionsAmount = displayedTransitions.Count - 1;
                                var displayedTransitionsIdx =
                                    displayedTransitions.FindIndex(t => t.Index == serializedTransition.Index);

                                #region Remove Transition Button

                                if (Button(buttonRect, IconContent("Toolbar Minus")))
                                {
                                    #region Remove Transition

                                    var removedSerializedTransition = serializedTransition;
                                    var stateIndex =
                                        _fromStates.IndexOf(removedSerializedTransition.FromState.objectReferenceValue);
                                    var stateTransitions = _transitionsByFromStates[stateIndex];
                                    var count = stateTransitions.Count;
                                    var index = stateTransitions.FindIndex(t =>
                                        t.SerializedDisplayedTransition.Index == removedSerializedTransition.Index);
                                    var deleteIndex = removedSerializedTransition.Index;
                                    var fromStateName = removedSerializedTransition.FromState.objectReferenceValue.name;
                                    if (index == 0 && count > 1)
                                        transitions.MoveArrayElement(
                                            stateTransitions[1].SerializedDisplayedTransition.Index, deleteIndex++);
                                    transitions.DeleteArrayElementAtIndex(deleteIndex);
                                    ApplyModifications($"Deleted transition from {fromStateName} " +
                                                       "to {serializedTransition.ToState.objectReferenceValue.name}");
                                    if (count > 1) toggledIndex = stateIndex;

                                    #endregion

                                    return true;
                                }

                                buttonRect.x -= 35;

                                #endregion

                                #region Move Transition Down

                                if (displayedTransitionsIdx < displayedTransitionsAmount)
                                {
                                    if (Button(buttonRect, IconContent("scrolldown")))
                                    {
                                        ReorderTransition(serializedTransition, false);
                                        return true;
                                    }

                                    buttonRect.x -= 35;
                                }

                                #endregion

                                #region Move Transition Up

                                if (displayedTransitionsIdx > 0)
                                {
                                    if (Button(buttonRect, IconContent("scrollup")))
                                    {
                                        ReorderTransition(serializedTransition, true);
                                        return true;
                                    }

                                    buttonRect.x -= 35;
                                }

                                #endregion

                                #region State Editor

                                if (Button(buttonRect, IconContent("SceneViewTools")))
                                {
                                    DisplayStateEditor(serializedTransition.ToState.objectReferenceValue);
                                    return true;
                                }

                                #endregion

                                #endregion

                                rect.x = stateTransition.x + 5;
                                rect.y += rect.height;
                                rect.width = stateTransition.width - 10;
                                rect.height = listHeight;

                                #region Display Conditions

                                reorderableList.DoList(rect);

                                #endregion

                                return false;
                            }

                            #endregion

                            if (DisplayTransition())
                            {
                                EndChangeCheck();
                                EditorGUILayout.EndFoldoutHeaderGroup();
                                EditorGUILayout.EndVertical();
                                EditorGUILayout.EndHorizontal();
                                return;
                            }

                            #endregion

                            Separator();
                        }

                        #endregion

                        if (EndChangeCheck()) serializedObject.ApplyModifiedProperties();
                    }

                    #endregion

                    EditorGUILayout.EndFoldoutHeaderGroup();
                    EditorGUILayout.EndVertical();
                    Separator();
                }

                #region Added State Transitions

                var addedTransitions = EditorGUILayout.BeginHorizontal();
                {
                    #region Initialize

                    if (!hasAddedTransitions)
                    {
                        addToggle = false;
                        newTransition = new SerializedObject(CreateInstance<TransitionItemSO>());
                        serializedNewTransition = new SerializedTransition(newTransition.FindProperty("item"));
                        createdTransitions = new ReorderableList(newTransition, serializedNewTransition.Conditions);
                        hasAddedTransitions = true;
                    }

                    //var addedListHeight = createdTransitions.GetHeight();

                    #endregion

                    EditorGUILayout.Space(addedTransitions.width - 55);
                    var addedListItem = addedTransitions;
                    addedListItem.x += 8;
                    addedListItem.width -= 16;
                    var addedList = addedListItem;

                    #region Added State Transition Toggle

                    if (!addToggle)
                    {
                        #region Reserve Space

                        addedListItem.height = singleLineHeight;
                        GetRect(addedListItem.width, addedListItem.height);

                        #endregion

                        if (!Button(addedListItem, "Add Transition")) return;
                        addToggle = true;
                        serializedNewTransition.ClearProperties();
                        return;
                    }

                    #endregion

                    addedListItem.height = createdTransitions.GetHeight() + singleLineHeight * 4;
                    DrawRect(addedListItem, ContentStyle.LightGray);
                    GetRect(addedListItem.width, addedListItem.height);
                    addedListItem.y += 10;
                    addedListItem.x += 20;

                    #region Added From State Transition Field

                    var fromStateField = addedListItem;
                    fromStateField.height = singleLineHeight;
                    LabelField(fromStateField, "From");
                    fromStateField.x += 40;
                    fromStateField.width /= 4;
                    PropertyField(fromStateField, serializedNewTransition.FromState);

                    #endregion

                    #region Reserved Space

                    addedListItem.x = addedList.width / 2 + 20;

                    #endregion

                    #region Added To State Transition Field

                    var toStateField = addedListItem;
                    toStateField.height = singleLineHeight;
                    LabelField(toStateField, "To");
                    toStateField.x += 40;
                    toStateField.width /= 4;
                    PropertyField(toStateField, serializedNewTransition.ToState);

                    #endregion

                    #region Added State Transition Conditions List

                    addedListItem.y += 30;
                    addedListItem.x = addedTransitions.x + 5;
                    addedListItem.height = createdTransitions.GetHeight();
                    addedListItem.width -= 10;
                    createdTransitions.DoList(addedListItem);

                    #endregion

                    #region Add State Transtion Button

                    var addButton = addedListItem;
                    addButton.y += addButton.height + 5;
                    addButton.height = singleLineHeight;
                    addButton.width = addedTransitions.width / 2 - 20;
                    if (Button(addButton, "Add Transition"))
                    {
                        if (serializedNewTransition.FromState.objectReferenceValue == null)
                        {
                            LogException(
                                new ArgumentNullException($"{serializedNewTransition.FromState.name} is null"));
                        }
                        else if (serializedNewTransition.ToState.objectReferenceValue == null)
                        {
                            LogException(new ArgumentNullException($"{serializedNewTransition.ToState.name} is null"));
                        }
                        else if (serializedNewTransition.FromState.objectReferenceValue ==
                                 serializedNewTransition.ToState.objectReferenceValue)
                        {
                            LogException(new InvalidOperationException("FromState and ToState are the same."));
                        }
                        else
                        {
                            #region Add Transtion

                            {
                                SerializedTransition addedTransition;
                                var source = serializedNewTransition;

                                #region Try Get Existing Transition

                                var fromIndex = _fromStates.IndexOf(source.FromState.objectReferenceValue);
                                var toIndex = -1;
                                bool hasTransition;
                                if (fromIndex < 0)
                                {
                                    hasTransition = false;
                                }
                                else
                                {
                                    toIndex = _transitionsByFromStates[fromIndex].FindIndex(transitionHelper =>
                                        transitionHelper.SerializedDisplayedTransition.ToState.objectReferenceValue ==
                                        source.ToState.objectReferenceValue);
                                    hasTransition = toIndex >= 0;
                                }

                                #endregion

                                if (hasTransition)
                                {
                                    addedTransition = _transitionsByFromStates[fromIndex][toIndex]
                                        .SerializedDisplayedTransition;
                                }
                                else
                                {
                                    var count = transitions.arraySize;
                                    transitions.InsertArrayElementAtIndex(count);
                                    addedTransition =
                                        new SerializedTransition(transitions.GetArrayElementAtIndex(count));
                                    addedTransition.ClearProperties();
                                    addedTransition.FromState.objectReferenceValue =
                                        source.FromState.objectReferenceValue;
                                    addedTransition.ToState.objectReferenceValue = source.ToState.objectReferenceValue;
                                }

                                #region Copy Conditions

                                var copyTo = addedTransition.Conditions;
                                var copyFrom = source.Conditions;
                                for (int i = 0, j = copyTo.arraySize; i < copyFrom.arraySize; i++, j++)
                                {
                                    copyTo.InsertArrayElementAtIndex(j);
                                    var cond = copyTo.GetArrayElementAtIndex(j);
                                    var srcCond = copyFrom.GetArrayElementAtIndex(i);
                                    cond.FindPropertyRelative("ExpectedResult").enumValueIndex =
                                        srcCond.FindPropertyRelative("ExpectedResult").enumValueIndex;
                                    cond.FindPropertyRelative("Operator").enumValueIndex =
                                        srcCond.FindPropertyRelative("Operator").enumValueIndex;
                                    cond.FindPropertyRelative("Condition").objectReferenceValue =
                                        srcCond.FindPropertyRelative("Condition").objectReferenceValue;
                                }

                                #endregion

                                ApplyModifications(
                                    $"Added transition from {addedTransition.FromState} to {addedTransition.ToState}");
                                toggledIndex = fromIndex >= 0 ? fromIndex : _fromStates.Count - 1;
                            }

                            #endregion

                            addToggle = false;
                        }
                    }

                    #endregion

                    #region Cancel State Transition Button

                    var cancelButton = addButton;
                    cancelButton.x += addedList.width / 2;
                    if (Button(cancelButton, "Cancel")) addToggle = false;

                    #endregion
                }
                EditorGUILayout.EndHorizontal();

                #endregion

                #endregion
            }
            else
            {
                #region State Editor GUI

                Separator();

                #region Back Button

                if (Button(IconContent("scrollleft"), Width(35), Height(20)))
                {
                    displayStateEditor = false;
                    return;
                }

                #endregion

                Separator();
                HelpBox("Edit the Actions that a State performs per frame. The order represent the order of execution.",
                    Info);
                Separator();

                #region State Name

                LabelField(cachedStateEditor.target.name, boldLabel);
                Separator();

                #endregion

                cachedStateEditor.OnInspectorGUI();

                #endregion
            }
        }

        internal class TransitionItemSO : ScriptableObject
        {
            [UsedImplicitly] public TransitionItem item = default(TransitionItem);
        }

        private static void EarlyOut()
        {
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        private void ReorderState(int index, bool up)
        {
            var toggledState = toggledIndex > -1 ? _fromStates[toggledIndex] : null;
            if (!up) index++;
            var fromStatesTransitions = _transitionsByFromStates[index];
            var transitionIndex = fromStatesTransitions[0].SerializedDisplayedTransition.Index;
            var targetIndex = _transitionsByFromStates[index - 1][0].SerializedDisplayedTransition.Index;
            transitions.MoveArrayElement(transitionIndex, targetIndex);
            ApplyModifications($"Moved {_fromStates[index].name} State {(up ? "up" : "down")}");
            if (toggledState) toggledIndex = _fromStates.IndexOf(toggledState);
        }

        private void ApplyModifications(string msg)
        {
            RecordObject(serializedObject.targetObject, msg);
            serializedObject.ApplyModifiedProperties();
            Reset();
        }

        private void DisplayStateEditor(UnityObject state)
        {
            if (!hasCachedStateEditor)
            {
                cachedStateEditor = CreateEditor(state, typeof(StateEditor));
                hasCachedStateEditor = true;
            }
            else
            {
                CreateCachedEditor(state, typeof(StateEditor), ref cachedStateEditor);
            }

            displayStateEditor = true;
        }

        private void ReorderTransition(SerializedTransition serializedTransition, bool up)
        {
            var stateIndex = _fromStates.IndexOf(serializedTransition.FromState.objectReferenceValue);
            var stateTransitions = _transitionsByFromStates[stateIndex];
            var index = stateTransitions.FindIndex(t =>
                t.SerializedDisplayedTransition.Index == serializedTransition.Index);
            (int currentIndex, int targetIndex) = up
                ? (serializedTransition.Index, stateTransitions[index - 1].SerializedDisplayedTransition.Index)
                : (stateTransitions[index + 1].SerializedDisplayedTransition.Index, serializedTransition.Index);
            transitions.MoveArrayElement(currentIndex, targetIndex);
            ApplyModifications(
                $"Moved transition to {serializedTransition.ToState.objectReferenceValue.name} {(up ? "up" : "down")}");
            toggledIndex = stateIndex;
        }

        [CustomEditor(typeof(StateSO))]
        private class StateEditor : Editor
        {
            private ReorderableList reorderableList;
            private SerializedProperty actions;

            private void OnEnable()
            {
                undoRedoPerformed += DoUndo;
                actions = serializedObject.FindProperty("actions");
                reorderableList = new ReorderableList(serializedObject, actions, true, true, true, true);

                #region Setup Actions List

                reorderableList.elementHeight *= 1.5f;
                reorderableList.drawHeaderCallback += rect => Label(rect, "Actions");
                reorderableList.onAddCallback += l =>
                {
                    var count = l.count;
                    l.serializedProperty.InsertArrayElementAtIndex(count);
                    var prop = l.serializedProperty.GetArrayElementAtIndex(count);
                    prop.objectReferenceValue = null;
                };
                reorderableList.drawElementCallback += (rect, index, isActive, isFocused) =>
                {
                    var r = rect;
                    r.height = singleLineHeight;
                    r.y += 5;
                    r.x += 5;
                    var prop = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                    if (prop.objectReferenceValue != null)
                    {
                        var label = prop.objectReferenceValue.name;
                        r.width = 35;
                        PropertyField(r, prop, none);
                        r.width = rect.width - 50;
                        r.x += 42;
                        Label(r, label, boldLabel);
                    }
                    else
                    {
                        PropertyField(r, prop, none);
                    }
                };
                reorderableList.onChangedCallback +=
                    l => l.serializedProperty.serializedObject.ApplyModifiedProperties();
                reorderableList.drawElementBackgroundCallback += (rect, index, isActive, isFocused) =>
                {
                    if (isFocused) DrawRect(rect, ContentStyle.Focused);
                    DrawRect(rect, index % 2 != 0 ? ContentStyle.ZebraDark : ContentStyle.ZebraLight);
                };

                #endregion
            }

            private void OnDisable()
            {
                undoRedoPerformed -= DoUndo;
            }

            public override void OnInspectorGUI()
            {
                reorderableList.DoLayoutList();
                serializedObject.ApplyModifiedProperties();
            }

            private void DoUndo()
            {
                serializedObject.UpdateIfRequiredOrScript();
            }
        }

        private class TransitionDisplay
        {
            internal SerializedTransition SerializedDisplayedTransition { get; }
            internal readonly ReorderableList DisplayedTransitions;

            internal TransitionDisplay(SerializedTransition serializedTransition, TransitionTableEditor editor)
            {
                SerializedDisplayedTransition = serializedTransition;
                DisplayedTransitions = new ReorderableList(SerializedDisplayedTransition.Transition.serializedObject,
                    SerializedDisplayedTransition.Conditions, true, false, true, true);

                #region Setup Conditions List

                DisplayedTransitions.elementHeight *= 2.3f;
                DisplayedTransitions.headerHeight = 1f;
                DisplayedTransitions.onAddCallback += l =>
                {
                    var count = l.count;
                    l.serializedProperty.InsertArrayElementAtIndex(count);
                    var prop = l.serializedProperty.GetArrayElementAtIndex(count);
                    prop.FindPropertyRelative("Condition").objectReferenceValue = null;
                    prop.FindPropertyRelative("ExpectedResult").enumValueIndex = 0;
                    prop.FindPropertyRelative("Operator").enumValueIndex = 0;
                };
                DisplayedTransitions.drawElementCallback += (rect, index, isActive, isFocused) =>
                {
                    var prop = DisplayedTransitions.serializedProperty.GetArrayElementAtIndex(index);
                    rect = new Rect(rect.x, rect.y + 2.5f, rect.width, singleLineHeight);
                    var condition = prop.FindPropertyRelative("Condition");

                    #region Draw ConditionSO Picker

                    if (condition.objectReferenceValue != null)
                    {
                        var label = condition.objectReferenceValue.name;
                        Label(rect, "If");
                        var r = rect;
                        r.x += 20;
                        r.width = 35;
                        PropertyField(r, condition, none);
                        r.x += 40;
                        r.width = rect.width - 120;
                        Label(r, label, boldLabel);
                    }
                    else
                    {
                        PropertyField(new Rect(rect.x, rect.y, 150, rect.height), condition, none);
                    }

                    #endregion

                    #region Draw Condition Expected Result

                    LabelField(new Rect(rect.x + rect.width - 80, rect.y, 20, rect.height), "Is");
                    PropertyField(new Rect(rect.x + rect.width - 60, rect.y, 60, rect.height),
                        prop.FindPropertyRelative("ExpectedResult"), none);

                    #endregion

                    #region Display Logic Condition

                    if (index < DisplayedTransitions.count - 1)
                        PropertyField(new Rect(rect.x + 20, rect.y + singleLineHeight + 5, 60, rect.height),
                            prop.FindPropertyRelative("Operator"), none);

                    #endregion
                };
                DisplayedTransitions.onChangedCallback +=
                    l => l.serializedProperty.serializedObject.ApplyModifiedProperties();
                DisplayedTransitions.drawElementBackgroundCallback += (rect, index, isActive, isFocused) =>
                {
                    if (isFocused) DrawRect(rect, ContentStyle.Focused);
                    DrawRect(rect, index % 2 != 0 ? ContentStyle.ZebraDark : ContentStyle.ZebraLight);
                };

                #endregion
            }
        }

        private readonly struct SerializedTransition
        {
            internal readonly SerializedProperty Transition;
            internal readonly SerializedProperty FromState;
            internal readonly SerializedProperty ToState;
            internal readonly SerializedProperty Conditions;
            internal readonly int Index;

            internal SerializedTransition(SerializedProperty transition)
            {
                Transition = transition;
                FromState = Transition.FindPropertyRelative("FromState");
                ToState = Transition.FindPropertyRelative("ToState");
                Conditions = Transition.FindPropertyRelative("Conditions");
                Index = -1;
            }

            internal SerializedTransition(SerializedProperty transition, int index)
            {
                Transition = transition.GetArrayElementAtIndex(index);
                FromState = Transition.FindPropertyRelative("FromState");
                ToState = Transition.FindPropertyRelative("ToState");
                Conditions = Transition.FindPropertyRelative("Conditions");
                Index = index;
            }

            internal void ClearProperties()
            {
                FromState.objectReferenceValue = null;
                ToState.objectReferenceValue = null;
                Conditions.ClearArray();
            }
        }

        private static class ContentStyle
        {
            private static bool _initialised;
            private static RectOffset _padding;
            private static RectOffset _leftPadding;
            private static RectOffset _margin;
            internal static Color DarkGray { get; private set; }
            internal static Color LightGray { get; private set; }
            internal static Color Focused { get; private set; }
            internal static Color ZebraDark { get; private set; }
            internal static Color ZebraLight { get; private set; }
            internal static GUIStyle StateListStyle { get; private set; }
            internal static GUIStyle WithPaddingAndMargins { get; private set; }

            [InitializeOnLoadMethod]
            internal static void Initialize()
            {
                if (_initialised) return;
                var guiStyleStateNormal = GetBuiltinSkin(Inspector).label.normal;
                _initialised = true;
                _padding = new RectOffset(5, 5, 5, 5);
                _leftPadding = new RectOffset(10, 0, 0, 0);
                _margin = new RectOffset(8, 8, 8, 8);
                guiStyleStateNormal.textColor =
                    isProSkin ? new Color(.85f, .85f, .85f) : new Color(0.337f, 0.337f, 0.337f);
                DarkGray = isProSkin ? new Color(0.283f, 0.283f, 0.283f) : new Color(0.7f, 0.7f, 0.7f);
                LightGray = isProSkin ? new Color(0.33f, 0.33f, 0.33f) : new Color(0.8f, 0.8f, 0.8f);
                ZebraDark = new Color(0.4f, 0.4f, 0.4f, 0.1f);
                ZebraLight = new Color(0.8f, 0.8f, 0.8f, 0.1f);
                Focused = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                WithPaddingAndMargins = new GUIStyle {padding = _padding, margin = _margin};
                StateListStyle = new GUIStyle
                {
                    alignment = MiddleLeft,
                    padding = _leftPadding,
                    fontStyle = Bold,
                    fontSize = 12,
                    margin = _margin,
                    normal = guiStyleStateNormal
                };
            }
        }
    }
}
*/
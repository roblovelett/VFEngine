using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.Editor.Data;
using VFEngine.Tools.StateMachine.ScriptableObjects;
using EditorUnity = UnityEditor.Editor;
using UnityObject = UnityEngine.Object;

namespace VFEngine.Tools.StateMachine.Editor
{
    using static MessageType;
    using static EditorGUI;
    using static EditorStyles;
    using static Undo;
    using static Debug;
    using static GUILayout;
    using static EditorGUILayout;
    using static EditorGUIUtility;
    using static GUILayoutUtility;
    using static ContentStyle;
    using static EditorText;

    [CustomEditor(typeof(TransitionTableSO))]
    internal class TransitionTableEditor : EditorUnity
    {
        private int transitionsIndex;
        private int fromStatesIndex;
        private int fromIndex;
        private int toIndex;
        private int transitionsAmount;
        private int transitionIndex;
        private int targetIndex;
        private int stateIndex;
        private int index;
        private int currentIndex;
        private int destinedIndex;
        private int fromStatesTransitionIndex;
        private int stateTransitionsAmount;
        private int stateTransitionsIndex;
        private int deleteIndex;
        private int toggledIndex;
        private bool displayStateEditor;
        private bool hasToIndex;
        private bool hasCachedStateEditor;
        private string fromStateLabel;
        private Rect stateRect;
        private Rect headerRect;
        private Rect toggleRect;
        private Rect buttonRect;
        private Rect addTransitionRect;
        private UnityObject toggledState;
        private SerializedProperty condition;
        private SerializedProperty sourceCondition;
        private SerializedProperty transitions;
        private SerializedTransition serializedTransition;
        private SerializedTransition addedTransition;
        private AddTransition addTransition;
        private EditorUnity cachedStateEditor;
        private List<TransitionDisplay> groupedProperties;
        private List<TransitionDisplay> stateTransitions;
        private List<TransitionDisplay> fromStatesTransitions;
        private List<TransitionDisplay> reorderedTransitions;
        private List<TransitionDisplay> transitionByFromState;
        private Dictionary<UnityObject, List<TransitionDisplay>> groupedTransitions;
        private readonly UnityObject state;
        internal List<List<TransitionDisplay>> TransitionsByFromStates { get; private set; }
        internal List<UnityObject> FromStates { get; private set; }

        internal TransitionTableEditor(object stateInternal, EditorUnity cachedStateEditorInternal)
        {
            state = stateInternal as UnityObject;
            cachedStateEditor = cachedStateEditorInternal;
        }

        private void OnEnable()
        {
            hasCachedStateEditor = false;
            addTransition = new AddTransition(this);
            toggledIndex = -1;
            undoRedoPerformed += Reset;
            Reset();
        }

        private void OnDisable()
        {
            undoRedoPerformed -= Reset;
            (addTransition as IDisposable)?.Dispose();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        internal void Reset()
        {
            serializedObject.Update();
            toggledState = toggledIndex > -1 ? FromStates[toggledIndex] : null;
            transitions = serializedObject.FindProperty(TransitionsProperty);
            transitionsAmount = transitions.arraySize;
            groupedTransitions = new Dictionary<UnityObject, List<TransitionDisplay>>();
            for (transitionsIndex = 0; transitionsIndex < transitions.arraySize; transitionsIndex++)
            {
                serializedTransition = new SerializedTransition(transitions, transitionsIndex);
                if (TransitionError(serializedTransition.FromState.objectReferenceValue == null, true, false)) return;
                if (TransitionError(serializedTransition.ToState.objectReferenceValue == null, false, true)) return;
                if (!groupedTransitions.TryGetValue(serializedTransition.FromState.objectReferenceValue,
                    out groupedProperties))
                {
                    groupedProperties = new List<TransitionDisplay>();
                    groupedTransitions.Add(serializedTransition.FromState.objectReferenceValue, groupedProperties);
                }

                groupedProperties.Add(new TransitionDisplay(serializedTransition, this));
            }

            FromStates = groupedTransitions.Keys.ToList();
            TransitionsByFromStates = new List<List<TransitionDisplay>>();
            foreach (var fromState in FromStates) TransitionsByFromStates.Add(groupedTransitions[fromState]);
            toggledIndex = toggledState ? FromStates.IndexOf(toggledState) : -1;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private bool TransitionError(bool transitionIsNull, bool isFromStateError, bool isTargetStateError)
        {
            if (!transitionIsNull) return false;
            if (isFromStateError) LogError(FromStateError(serializedObject.targetObject.name));
            if (isTargetStateError) LogError(TargetStateError(serializedObject.targetObject.name));
            transitions.DeleteArrayElementAtIndex(transitionsIndex);
            ApplyModifications(InvalidTransitionDeleted);
            return true;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public override void OnInspectorGUI()
        {
            if (!displayStateEditor)
            {
                Separator();
                HelpBox(StateHelpMessage);
                Separator();
                for (fromStatesIndex = 0; fromStatesIndex < FromStates.Count; fromStatesIndex++)
                {
                    stateRect = EditorGUILayout.BeginVertical(WithPaddingAndMargins);
                    DrawRect(stateRect, LightGray);
                    fromStatesTransitions = TransitionsByFromStates[fromStatesIndex];
                    headerRect = EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    fromStateLabel = fromStatesTransitions[0].SerializedTransition.FromState.objectReferenceValue.name;
                    if (fromStatesIndex == 0) fromStateLabel += InitialState;
                    headerRect.height = singleLineHeight;
                    GetRect(headerRect.width, headerRect.height);
                    headerRect.x += 5;
                    toggleRect = headerRect;
                    toggleRect.width -= 140;
                    toggledIndex =
                        BeginFoldoutHeaderGroup(toggleRect, toggledIndex == fromStatesIndex, fromStateLabel,
                            StateListStyle) ? fromStatesIndex :
                        toggledIndex == fromStatesIndex ? -1 : toggledIndex;
                    Separator();
                    EditorGUILayout.EndVertical();
                    buttonRect = new Rect(headerRect.width - 25, headerRect.y, 35, 20);
                    if (fromStatesIndex < FromStates.Count - 1 && ButtonPressed(ScrollDown, false, false)) return;
                    if (fromStatesIndex > 0 && ButtonPressed(ScrollUp, true, false)) return;
                    if (ButtonPressed(SceneViewTools, false, true)) return;
                    EditorGUILayout.EndHorizontal();
                    if (toggledIndex == fromStatesIndex)
                    {
                        BeginChangeCheck();
                        stateRect.y += singleLineHeight * 2;
                        foreach (var fromStatesTransition in fromStatesTransitions)
                        {
                            if (fromStatesTransition.Display(ref stateRect))
                            {
                                EndChangeCheck();
                                EditorGUILayout.EndFoldoutHeaderGroup();
                                EditorGUILayout.EndVertical();
                                EditorGUILayout.EndHorizontal();
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

                addTransitionRect = EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(addTransitionRect.width - 55);
                addTransition.Display(addTransitionRect);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                Separator();
                if (Button(IconContent(ScrollLeft), Width(35), Height(20))) displayStateEditor = false;
                if (displayStateEditor) return;
                HelpBox(ActionsHelpMessage);
                LabelField(cachedStateEditor.target.name, boldLabel);
                Separator();
                cachedStateEditor.OnInspectorGUI();
            }
        }

        private static void HelpBox(string helpMessage)
        {
            Separator();
            EditorGUILayout.HelpBox(helpMessage, Info);
            Separator();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private bool ButtonPressed(string iconContent, bool reorderState, bool isSceneViewTools)
        {
            if (GUI.Button(buttonRect, IconContent(iconContent)))
            {
                if (isSceneViewTools)
                {
                    DisplayStateEditor(state);
                }
                else
                {
                    toggledState = toggledIndex > -1 ? FromStates[toggledIndex] : null;
                    if (!reorderState) fromStatesIndex++;
                    reorderedTransitions = TransitionsByFromStates[fromStatesIndex];
                    transitionIndex = reorderedTransitions[0].SerializedTransition.Index;
                    targetIndex = TransitionsByFromStates[fromStatesIndex - 1][0].SerializedTransition.Index;
                    transitions.MoveArrayElement(transitionIndex, targetIndex);
                    ApplyModifications(MovedFromState(FromStates[fromStatesIndex].name, reorderState));
                    if (toggledState) toggledIndex = FromStates.IndexOf(toggledState);
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndFoldoutHeaderGroup();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                return true;
            }

            if (!isSceneViewTools) buttonRect.x -= 40;
            return false;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        internal void AddTransition(SerializedTransition source)
        {
            fromIndex = FromStates.IndexOf(source.FromState.objectReferenceValue);
            toIndex = -1;
            if (fromIndex < 0) hasToIndex = false;
            toIndex = TransitionsByFromStates[fromIndex].FindIndex(transitionHelper =>
                transitionHelper.SerializedTransition.ToState.objectReferenceValue ==
                source.ToState.objectReferenceValue);
            hasToIndex = toIndex >= 0;
            if (hasToIndex)
            {
                addedTransition = TransitionsByFromStates[fromIndex][toIndex].SerializedTransition;
            }
            else
            {
                transitionsAmount = transitions.arraySize;
                transitions.InsertArrayElementAtIndex(transitionsAmount);
                addedTransition = new SerializedTransition(transitions.GetArrayElementAtIndex(transitionsAmount));
                addedTransition.ClearProperties();
                addedTransition.FromState.objectReferenceValue = source.FromState.objectReferenceValue;
                addedTransition.ToState.objectReferenceValue = source.ToState.objectReferenceValue;
            }

            for (int i = 0, j = addedTransition.Conditions.arraySize; i < source.Conditions.arraySize; i++, j++)
            {
                addedTransition.Conditions.InsertArrayElementAtIndex(j);
                condition = addedTransition.Conditions.GetArrayElementAtIndex(j);
                sourceCondition = source.Conditions.GetArrayElementAtIndex(i);
                condition.FindPropertyRelative(ExpectedResult).enumValueIndex =
                    sourceCondition.FindPropertyRelative(ExpectedResult).enumValueIndex;
                condition.FindPropertyRelative(Operator).enumValueIndex =
                    sourceCondition.FindPropertyRelative(Operator).enumValueIndex;
                condition.FindPropertyRelative(Condition).objectReferenceValue =
                    sourceCondition.FindPropertyRelative(Condition).objectReferenceValue;
            }

            ApplyModifications(AddedTransition(addedTransition.FromState.name, addedTransition.ToState.name));
            toggledIndex = fromIndex >= 0 ? fromIndex : FromStates.Count - 1;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        internal void ReorderTransition(SerializedTransition transitionInternal, bool up)
        {
            stateIndex = FromStates.IndexOf(transitionInternal.FromState.objectReferenceValue);
            transitionByFromState = TransitionsByFromStates[stateIndex];
            index = transitionByFromState.FindIndex(t => t.SerializedTransition.Index == transitionInternal.Index);
            if (up)
            {
                currentIndex = transitionInternal.Index;
                destinedIndex = transitionByFromState[index - 1].SerializedTransition.Index;
            }
            else
            {
                currentIndex = transitionByFromState[index + 1].SerializedTransition.Index;
                destinedIndex = transitionInternal.Index;
            }

            transitions.MoveArrayElement(currentIndex, destinedIndex);
            ApplyModifications(MovedTransition(transitionInternal.ToState.objectReferenceValue.name, up));
            toggledIndex = stateIndex;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        internal void RemoveTransition(SerializedTransition transitionInternal)
        {
            fromStatesTransitionIndex = FromStates.IndexOf(transitionInternal.FromState.objectReferenceValue);
            stateTransitions = TransitionsByFromStates[fromStatesTransitionIndex];
            stateTransitionsAmount = stateTransitions.Count;
            stateTransitionsIndex =
                stateTransitions.FindIndex(t => t.SerializedTransition.Index == transitionInternal.Index);
            deleteIndex = transitionInternal.Index;
            if (stateTransitionsIndex == 0 && stateTransitionsAmount > 1)
                transitions.MoveArrayElement(stateTransitions[1].SerializedTransition.Index, deleteIndex++);
            transitions.DeleteArrayElementAtIndex(deleteIndex);
            ApplyModifications(DeletedTransition(transitionInternal.FromState.objectReferenceValue.name,
                serializedTransition.ToState.objectReferenceValue.name));
            if (stateTransitionsAmount > 1) toggledIndex = fromStatesTransitionIndex;
        }

        private void ApplyModifications(string msg)
        {
            RecordObject(serializedObject.targetObject, msg);
            serializedObject.ApplyModifiedProperties();
            Reset();
        }

        internal void DisplayStateEditor(UnityObject stateInternal)
        {
            if (!hasCachedStateEditor)
            {
                cachedStateEditor = CreateEditor(stateInternal, typeof(StateEditor));
                hasCachedStateEditor = !hasCachedStateEditor;
            }
            else
            {
                CreateCachedEditor(stateInternal, typeof(StateEditor), ref cachedStateEditor);
                hasCachedStateEditor = !hasCachedStateEditor;
            }

            displayStateEditor = true;
        }
    }
}
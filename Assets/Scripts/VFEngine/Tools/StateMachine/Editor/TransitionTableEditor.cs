using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.Editor.Data;
using EditorUnity = UnityEditor.Editor;
using TransitionTableSO = VFEngine.Tools.StateMachine.ScriptableObjects.StateMachineTransitionTableSO;

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
        private Object toggledState;
        private SerializedProperty condition;
        private SerializedProperty sourceCondition;
        private SerializedProperty transitions;
        private SerializedTransition serializedTransition;
        private SerializedTransition addedTransition;
        private List<Object> fromStates;
        private List<TransitionDisplay> groupedProperties;
        private List<TransitionDisplay> stateTransitions;
        private List<List<TransitionDisplay>> transitionsByFromStates;
        private List<TransitionDisplay> fromStatesTransitions;
        private List<TransitionDisplay> reorderedTransitions;
        private List<TransitionDisplay> transitionByFromState;
        private Dictionary<Object, List<TransitionDisplay>> groupedTransitions;
        private readonly Object state;
        private readonly EditorUnity cachedStateEditor;

        //private AddTransitionHelper addTransitionHelper;
        internal TransitionTableEditor(object stateInternal, EditorUnity cachedStateEditorInternal)
        {
            state = stateInternal as Object;
            cachedStateEditor = cachedStateEditorInternal;
        }

        private void OnEnable()
        {
            hasCachedStateEditor = false;
            //addTransitionHelper = new AddTransitionHelper(this);
            toggledIndex = -1;
            undoRedoPerformed += Reset;
            Reset();
        }

        private void OnDisable()
        {
            undoRedoPerformed -= Reset;
            //addTransitionHelper?.Dispose();
        }

        private void Error(bool isFromStateError, bool isTargetStateError)
        {
            if (isFromStateError) LogError(FromStateError(serializedObject.targetObject.name));
            if (isTargetStateError) LogError(TargetStateError(serializedObject.targetObject.name));
            transitions.DeleteArrayElementAtIndex(transitionsIndex);
            ApplyModifications(InvalidTransitionDeleted);
        }

        internal void Reset()
        {
            serializedObject.Update();
            toggledState = toggledIndex > -1 ? fromStates[toggledIndex] : null;
            transitions = serializedObject.FindProperty(TransitionsProperty);
            groupedTransitions = new Dictionary<Object, List<TransitionDisplay>>();
            for (transitionsIndex = 0; transitionsIndex < transitions.arraySize; transitionsIndex++)
            {
                serializedTransition = new SerializedTransition(transitions, transitionsIndex);
                if (serializedTransition.FromState.objectReferenceValue == null)
                {
                    Error(true, false);
                    return;
                }

                if (serializedTransition.ToState.objectReferenceValue == null)
                {
                    Error(false, true);
                    return;
                }

                if (!groupedTransitions.TryGetValue(serializedTransition.FromState.objectReferenceValue,
                    out groupedProperties))
                {
                    groupedProperties = new List<TransitionDisplay>();
                    groupedTransitions.Add(serializedTransition.FromState.objectReferenceValue, groupedProperties);
                }

                groupedProperties.Add(new TransitionDisplay(serializedTransition, this));
            }

            fromStates = groupedTransitions.Keys.ToList();
            transitionsByFromStates = new List<List<TransitionDisplay>>();
            foreach (var fromState in fromStates) transitionsByFromStates.Add(groupedTransitions[fromState]);
            toggledIndex = toggledState ? fromStates.IndexOf(toggledState) : -1;
        }

        public override void OnInspectorGUI()
        {
            if (!displayStateEditor)
            {
                Separator();
                HelpBox(StateHelpMessage, Info);
                Separator();
                for (fromStatesIndex = 0; fromStatesIndex < fromStates.Count; fromStatesIndex++)
                {
                    stateRect = EditorGUILayout.BeginVertical(WithPaddingAndMargins);
                    DrawRect(stateRect, LightGray);
                    fromStatesTransitions = transitionsByFromStates[fromStatesIndex];
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
                    if (fromStatesIndex < fromStates.Count - 1)
                    {
                        if (GUI.Button(buttonRect, IconContent(ScrollDown)))
                        {
                            ReorderState(false);
                            EarlyOut();
                            return;
                        }

                        buttonRect.x -= 40;
                    }

                    if (fromStatesIndex > 0)
                    {
                        if (GUI.Button(buttonRect, IconContent(ScrollUp)))
                        {
                            ReorderState(true);
                            EarlyOut();
                            return;
                        }

                        buttonRect.x -= 40;
                    }

                    if (GUI.Button(buttonRect, IconContent(SceneViewTools)))
                    {
                        DisplayStateEditor(state);
                        EarlyOut();
                        return;
                    }

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
                //addTransitionHelper.Display(addTransitionRect);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                Separator();
                if (Button(IconContent(ScrollLeft), Width(35), Height(20))) displayStateEditor = false;
                if (displayStateEditor) return;
                Separator();
                HelpBox(ActionsHelpMessage, Info);
                Separator();
                LabelField(cachedStateEditor.target.name, boldLabel);
                Separator();
                cachedStateEditor.OnInspectorGUI();
            }
        }

        private static void EarlyOut()
        {
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        private void ReorderState(bool up)
        {
            toggledState = toggledIndex > -1 ? fromStates[toggledIndex] : null;
            if (!up) fromStatesIndex++;
            reorderedTransitions = transitionsByFromStates[fromStatesIndex];
            transitionIndex = reorderedTransitions[0].SerializedTransition.Index;
            targetIndex = transitionsByFromStates[fromStatesIndex - 1][0].SerializedTransition.Index;
            transitions.MoveArrayElement(transitionIndex, targetIndex);
            ApplyModifications(MovedFromState(fromStates[fromStatesIndex].name, up));
            if (toggledState) toggledIndex = fromStates.IndexOf(toggledState);
        }

        internal void AddTransition(SerializedTransition source)
        {
            fromIndex = fromStates.IndexOf(source.FromState.objectReferenceValue);
            toIndex = -1;
            if (fromIndex < 0) hasToIndex = false;
            toIndex = transitionsByFromStates[fromIndex].FindIndex(transitionHelper =>
                transitionHelper.SerializedTransition.ToState.objectReferenceValue ==
                source.ToState.objectReferenceValue);
            hasToIndex = toIndex >= 0;
            if (hasToIndex)
            {
                addedTransition = transitionsByFromStates[fromIndex][toIndex].SerializedTransition;
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
            toggledIndex = fromIndex >= 0 ? fromIndex : fromStates.Count - 1;
        }

        internal void ReorderTransition(SerializedTransition transitionInternal, bool up)
        {
            stateIndex = fromStates.IndexOf(transitionInternal.FromState.objectReferenceValue);
            transitionByFromState = transitionsByFromStates[stateIndex];
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

        internal void RemoveTransition(SerializedTransition transitionInternal)
        {
            fromStatesTransitionIndex = fromStates.IndexOf(transitionInternal.FromState.objectReferenceValue);
            stateTransitions = transitionsByFromStates[fromStatesTransitionIndex];
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

        internal List<SerializedTransition> GetStateTransitions(Object @object)
        {
            return transitionsByFromStates[fromStates.IndexOf(@object)]
                .Select(transitionDisplay => transitionDisplay.SerializedTransition).ToList();
        }

        private void ApplyModifications(string msg)
        {
            RecordObject(serializedObject.targetObject, msg);
            serializedObject.ApplyModifiedProperties();
            Reset();
        }

        internal void DisplayStateEditor(Object stateInternal)
        {
            if (!hasCachedStateEditor)
                //cachedStateEditor = CreateEditor(stateInternal, typeof(StateEditor));
                hasCachedStateEditor = !hasCachedStateEditor;
            else
                //CreateCachedEditor(stateInternal, typeof(StateEditor), ref cachedStateEditor);
                hasCachedStateEditor = !hasCachedStateEditor;
            displayStateEditor = true;
        }
    }
}
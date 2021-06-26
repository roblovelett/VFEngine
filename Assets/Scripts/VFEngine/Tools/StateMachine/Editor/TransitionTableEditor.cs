using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.Editor.Data;
using EditorUnity = UnityEditor.Editor;
using TransitionTableSO = VFEngine.Tools.StateMachine.ScriptableObjects.StateMachineTransitionTableSO;

namespace VFEngine.Tools.StateMachine.Editor
{
    using static EditorGUI;
    using static EditorStyles;
    using static MessageType;
    using static Undo;
    using static Debug;
    using static GUILayout;
    using static EditorGUILayout;
    using static EditorGUIUtility;
    using static GUILayoutUtility;
    using static ContentStyle;

    [CustomEditor(typeof(TransitionTableSO))]
    internal class TransitionTableEditor : EditorUnity
    {
        //private readonly object state;
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
        private readonly EditorUnity cachedStateEditor;
        private SerializedTransition serializedTransition;

        //private AddTransitionHelper addTransitionHelper;
        private List<Object> fromStates;
        //private List<TransitionDisplayHelper> groupedProperties;
        //private List<List<TransitionDisplayHelper>> transitionsByFromStates;
        //private Dictionary<Object, List<TransitionDisplayHelper>> groupedTransitions;

        internal TransitionTableEditor(object stateInternal, EditorUnity cachedStateEditorInternal)
        {
            //state = stateInternal;
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

        internal void Reset()
        {
            serializedObject.Update();
            toggledState = toggledIndex > -1 ? fromStates[toggledIndex] : null;
            transitions = serializedObject.FindProperty("transitions");
            //groupedTransitions = new Dictionary<Object, List<TransitionDisplayHelper>>();
            for (transitionsIndex = 0; transitionsIndex < transitions.arraySize; transitionsIndex++)
            {
                serializedTransition = new SerializedTransition(transitions, transitionsIndex);
                if (serializedTransition.FromState.objectReferenceValue == null)
                {
                    LogError("Transition with invalid \"From State\" found in table " +
                             serializedObject.targetObject.name + ", deleting...");
                    transitions.DeleteArrayElementAtIndex(transitionsIndex);
                    ApplyModifications("Invalid transition deleted");
                    return;
                }

                if (serializedTransition.ToState.objectReferenceValue == null)
                {
                    LogError("Transition with invalid \"Target State\" found in table " +
                             serializedObject.targetObject.name + ", deleting...");
                    transitions.DeleteArrayElementAtIndex(transitionsIndex);
                    ApplyModifications("Invalid transition deleted");
                    return;
                }

                //if (!groupedTransitions.TryGetValue(serializedTransition.FromState.objectReferenceValue,
                //    out groupedProperties))
                //  {
                //groupedProperties = new List<TransitionDisplayHelper>();
                //    groupedTransitions.Add(serializedTransition.FromState.objectReferenceValue, groupedProperties);
                // }

                //groupedProperties.Add(new TransitionDisplayHelper(serializedTransition, this));
            }

            // fromStates = groupedTransitions.Keys.ToList();
            // transitionsByFromStates = new List<List<TransitionDisplayHelper>>();
            // foreach (var fromState in fromStates) transitionsByFromStates.Add(groupedTransitions[fromState]);
            toggledIndex = toggledState ? fromStates.IndexOf(toggledState) : -1;
        }

        public override void OnInspectorGUI()
        {
            if (!displayStateEditor)
            {
                Separator();
                HelpBox(
                    "Click on any State's name to see the Transitions it contains, or click the Pencil/Wrench icon to see its Actions.",
                    Info);
                Separator();
                for (fromStatesIndex = 0; fromStatesIndex < fromStates.Count; fromStatesIndex++)
                {
                    stateRect = EditorGUILayout.BeginVertical(WithPaddingAndMargins);
                    DrawRect(stateRect, LightGray);
                    // var fromStatesTransitions = transitionsByFromStates[fromStatesIndex];
                    headerRect = EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.BeginVertical();
                        //fromStateLabel = fromStatesTransitions[0].SerializedTransition.FromState.objectReferenceValue.name;
                        if (fromStatesIndex == 0) fromStateLabel += " (Initial State)";
                        headerRect.height = singleLineHeight;
                        GetRect(headerRect.width, headerRect.height);
                        headerRect.x += 5;
                        {
                            toggleRect = headerRect;
                            toggleRect.width -= 140;
                            toggledIndex = BeginFoldoutHeaderGroup(toggleRect, toggledIndex == fromStatesIndex,
                                    fromStateLabel, StateListStyle) ? fromStatesIndex :
                                toggledIndex == fromStatesIndex ? -1 : toggledIndex;
                        }
                        Separator();
                        EditorGUILayout.EndVertical();
                        {
                            buttonRect = new Rect(headerRect.width - 25, headerRect.y, 35, 20);
                            if (fromStatesIndex < fromStates.Count - 1)
                            {
                                if (Button(buttonRect, "scrolldown"))
                                {
                                    ReorderState(false);
                                    EarlyOut();
                                    return;
                                }

                                buttonRect.x -= 40;
                            }

                            if (fromStatesIndex > 0)
                            {
                                if (Button(buttonRect, "scrollup"))
                                {
                                    ReorderState(true);
                                    EarlyOut();
                                    return;
                                }

                                buttonRect.x -= 40;
                            }

                            if (Button(buttonRect, "SceneViewTools"))
                            {
                                if (!hasCachedStateEditor)
                                {
                                    // cachedStateEditor = CreateEditor(state, typeof(StateEditor));
                                    hasCachedStateEditor = !hasCachedStateEditor;
                                }

                                displayStateEditor = true;
                                EarlyOut();
                                return;
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    if (toggledIndex == fromStatesIndex)
                    {
                        BeginChangeCheck();
                        stateRect.y += singleLineHeight * 2;
                        /*foreach (var transition in fromStatesTransitions)
                        {
                            if (transition.Display(ref stateRect))
                            {
                                EndChangeCheck();
                                EditorGUILayout.EndFoldoutHeaderGroup();
                                EditorGUILayout.EndVertical();
                                EditorGUILayout.EndHorizontal();
                                return;
                            }

                            Separator();
                        }*/
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
                if (GUILayout.Button(IconContent("scrollleft"), Width(35), Height(20))) displayStateEditor = false;
                if (displayStateEditor) return;
                Separator();
                HelpBox("Edit the Actions that a State performs per frame. The order represent the order of execution.",
                    Info);
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

        private static bool Button(Rect position, string icon)
        {
            return GUI.Button(position, IconContent(icon));
        }

        private void ReorderState(bool up)
        {
            toggledState = toggledIndex > -1 ? fromStates[toggledIndex] : null;
            if (!up) fromStatesIndex++;
            //var reorderedTransitions = transitionsByFromStates[fromStatesIndex];
            //transitionIndex = reorderedTransitions[0].SerializedTransition.Index;
            //targetIndex = transitionsByFromStates[fromStatesIndex - 1][0].SerializedTransition.Index;
            transitions.MoveArrayElement(transitionIndex, targetIndex);
            ApplyModifications($"Moved {fromStates[fromStatesIndex].name} State {(up ? "up" : "down")}");
            if (toggledState) toggledIndex = fromStates.IndexOf(toggledState);
        }

        internal void AddTransition(SerializedTransition source)
        {
            SerializedTransition transition;
            fromIndex = fromStates.IndexOf(source.FromState.objectReferenceValue);
            toIndex = -1;
            if (fromIndex < 0) hasToIndex = false;
            /*toIndex = transitionsByFromStates[fromIndex].FindIndex(transitionHelper =>
                transitionHelper.SerializedTransition.ToState.objectReferenceValue ==
                source.ToState.objectReferenceValue);*/
            hasToIndex = toIndex >= 0;
            if (hasToIndex)
            {
                //transition = transitionsByFromStates[fromIndex][toIndex].SerializedTransition;
            }
            else
            {
                transitionsAmount = transitions.arraySize;
                transitions.InsertArrayElementAtIndex(transitionsAmount);
                transition = new SerializedTransition(transitions.GetArrayElementAtIndex(transitionsAmount));
                transition.ClearProperties();
                transition.FromState.objectReferenceValue = source.FromState.objectReferenceValue;
                transition.ToState.objectReferenceValue = source.ToState.objectReferenceValue;
            }

            /*for (int i = 0, j = transition.Conditions.arraySize; i < source.Conditions.arraySize; i++, j++)
            {
                transition.Conditions.InsertArrayElementAtIndex(j);
                condition = transition.Conditions.GetArrayElementAtIndex(j);
                sourceCondition = source.Conditions.GetArrayElementAtIndex(i);
                condition.FindPropertyRelative("ExpectedResult").enumValueIndex =
                    sourceCondition.FindPropertyRelative("ExpectedResult").enumValueIndex;
                condition.FindPropertyRelative("Operator").enumValueIndex =
                    sourceCondition.FindPropertyRelative("Operator").enumValueIndex;
                condition.FindPropertyRelative("Condition").objectReferenceValue =
                    sourceCondition.FindPropertyRelative("Condition").objectReferenceValue;
            }*/

            //ApplyModifications($"Added transition from {transition.FromState} to {transition.ToState}");
            toggledIndex = fromIndex >= 0 ? fromIndex : fromStates.Count - 1;
        }

        internal void ReorderTransition(SerializedTransition transition, bool up)
        {
            stateIndex = fromStates.IndexOf(transition.FromState.objectReferenceValue);
            //var stateTransitions = transitionsByFromStates[stateIndex];
            /*index = stateTransitions.FindIndex(t => t.SerializedTransition.Index == transition.Index);
            if (up)
            {
                currentIndex = transition.Index;
                destinedIndex = stateTransitions[index - 1].SerializedTransition.Index;
            }
            else
            {
                currentIndex = stateTransitions[index + 1].SerializedTransition.Index;
                destinedIndex = transition.Index;
            }*/
            transitions.MoveArrayElement(currentIndex, destinedIndex);
            ApplyModifications(
                $"Moved transition to {transition.ToState.objectReferenceValue.name} {(up ? "up" : "down")}");
            toggledIndex = stateIndex;
        }

        internal void RemoveTransition(SerializedTransition transition)
        {
            fromStatesTransitionIndex = fromStates.IndexOf(transition.FromState.objectReferenceValue);
            //var stateTransitions = transitionsByFromStates[fromStatesTransitionIndex];
            /*
            stateTransitionsAmount = stateTransitions.Count;
            stateTransitionsIndex = stateTransitions.FindIndex(t => t.SerializedTransition.Index == transition.Index);
            deleteIndex = transition.Index;
            if (stateTransitionsIndex == 0 && stateTransitionsAmount > 1)
                transitions.MoveArrayElement(stateTransitions[1].SerializedTransition.Index, deleteIndex++);
            */
            transitions.DeleteArrayElementAtIndex(deleteIndex);
            ApplyModifications($"Deleted transition from {transition.FromState.objectReferenceValue.name} " +
                               "to {serializedTransition.ToState.objectReferenceValue.name}");
            if (stateTransitionsAmount > 1) toggledIndex = fromStatesTransitionIndex;
        }

        /*internal List<SerializedTransition> GetStateTransitions(Object s)
        {
            return transitionsByFromStates[fromStates.IndexOf(s)].Select(t => t.SerializedTransition).ToList();
        }*/

        private void ApplyModifications(string msg)
        {
            RecordObject(serializedObject.targetObject, msg);
            serializedObject.ApplyModifiedProperties();
            Reset();
        }
    }
}
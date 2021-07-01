using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachineSO.Editor.Data;
using VFEngine.Tools.StateMachineSO.ScriptableObjects;
using EditorUnity = UnityEditor.Editor;
using UnityObject = UnityEngine.Object;

namespace VFEngine.Tools.StateMachineSO.Editor
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
        private int toggledIndex;
        private bool displayStateEditor;
        private bool hasCachedStateEditor;
        private bool hasFromStates;
        private bool hasToggledState;
        private SerializedProperty transitions;
        private List<UnityObject> fromStates;
        private List<List<TransitionDisplay>> transitionsByFromStates;
        private AddTransition addTransition;
        private EditorUnity cachedStateEditor;
        private UnityObject toggledState;

        private void Awake()
        {
            toggledIndex = -1;
            hasCachedStateEditor = false;
            hasFromStates = false;
            hasToggledState = false;
        }

        private void OnEnable()
        {
            addTransition = new AddTransition(this);
            undoRedoPerformed += Reset;
            Reset();
        }

        private void OnDisable()
        {
            undoRedoPerformed -= Reset;
            addTransition?.Dispose();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        internal void Reset()
        {
            serializedObject.Update();
            ToggledState();
            transitions = serializedObject.FindProperty(TransitionsProperty);
            var groupedTransitions = new Dictionary<UnityObject, List<TransitionDisplay>>();
            var count = transitions.arraySize;
            for (var i = 0; i < count; i++)
            {
                var serializedTransition = new SerializedTransition(transitions, i);
                if (serializedTransition.FromState.objectReferenceValue == null)
                {
                    LogError(FromStateError(serializedObject.targetObject.name));
                    transitions.DeleteArrayElementAtIndex(i);
                    ApplyModifications(InvalidTransitionDeleted);
                    return;
                }

                if (serializedTransition.ToState.objectReferenceValue == null)
                {
                    LogError(TargetStateError(serializedObject.targetObject.name));
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

            fromStates = groupedTransitions.Keys.ToList();
            hasFromStates = true;
            transitionsByFromStates = new List<List<TransitionDisplay>>();
            foreach (var fromState in fromStates) transitionsByFromStates.Add(groupedTransitions[fromState]);
            ToggledIndex();
        }

        private void ToggledState()
        {
            hasToggledState = toggledIndex > -1 && hasFromStates;
            if (hasToggledState) toggledState = fromStates[toggledIndex];
        }

        private void ToggledIndex()
        {
            toggledIndex = hasToggledState ? fromStates.IndexOf(toggledState) : -1;
        }

        public override void OnInspectorGUI()
        {
            if (!displayStateEditor)
            {
                Separator();
                HelpBox(StateHelpMessage, Info);
                Separator();
                for (var i = 0; i < fromStates.Count; i++)
                {
                    var stateRect = EditorGUILayout.BeginVertical(WithPaddingAndMarginsStyle);
                    DrawRect(stateRect, LightGray);
                    var fromStateTransitions = transitionsByFromStates[i];
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
                        toggledIndex = BeginFoldoutHeaderGroup(toggleRect, toggledIndex == i, label, StateListStyle) ?
                            i :
                            toggledIndex == i ? -1 : toggledIndex;
                        Separator();
                        EditorGUILayout.EndVertical();
                        var buttonRect = new Rect(headerRect.width - 25, headerRect.y, 35, 20);
                        if (i < fromStates.Count - 1)
                        {
                            if (GUIButton(buttonRect, ScrollDown))
                            {
                                ReorderState(i, false);
                                EarlyOut();
                                return;
                            }

                            buttonRect.x -= 40;
                        }

                        if (i > 0)
                        {
                            if (GUIButton(buttonRect, ScrollUp))
                            {
                                ReorderState(i, true);
                                EarlyOut();
                                return;
                            }

                            buttonRect.x -= 40;
                        }

                        if (GUIButton(buttonRect, SceneViewTools))
                        {
                            DisplayStateEditor(fromStateTransitions[0].SerializedTransition.FromState
                                .objectReferenceValue);
                            EarlyOut();
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

        private static bool GUIButton(Rect position, string icon)
        {
            return GUI.Button(position, IconContent(icon));
        }

        private static void EarlyOut()
        {
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        internal void DisplayStateEditor(UnityObject state)
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
            var fromStateTransitions = transitionsByFromStates[index];
            var transitionIndex = fromStateTransitions[0].SerializedTransition.Index;
            var targetIndex = transitionsByFromStates[index - 1][0].SerializedTransition.Index;
            transitions.MoveArrayElement(transitionIndex, targetIndex);
            ApplyModifications(MovedFromState(fromStates[index].name, up));
            ToggledIndex();
        }

        internal void AddTransition(SerializedTransition source)
        {
            bool hasExistingTransition;
            SerializedTransition transition;
            var fromIndex = fromStates.IndexOf(source.FromState.objectReferenceValue);
            var toIndex = transitionsByFromStates[fromIndex].FindIndex(transitionHelper =>
                transitionHelper.SerializedTransition.ToState.objectReferenceValue ==
                source.ToState.objectReferenceValue);
            if (fromIndex < 0) hasExistingTransition = false;
            else hasExistingTransition = toIndex >= 0;
            if (hasExistingTransition)
            {
                transition = transitionsByFromStates[fromIndex][toIndex].SerializedTransition;
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
            toggledIndex = fromIndex >= 0 ? fromIndex : fromStates.Count - 1;
        }

        internal void ReorderTransition(SerializedTransition serializedTransition, bool up)
        {
            var stateIndex = fromStates.IndexOf(serializedTransition.FromState.objectReferenceValue);
            var stateTransitions = transitionsByFromStates[stateIndex];
            var index = stateTransitions.FindIndex(t => t.SerializedTransition.Index == serializedTransition.Index);
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

        internal void RemoveTransition(SerializedTransition serializedTransition)
        {
            var stateIndex = fromStates.IndexOf(serializedTransition.FromState.objectReferenceValue);
            var stateTransitions = transitionsByFromStates[stateIndex];
            var count = stateTransitions.Count;
            var index = stateTransitions.FindIndex(t => t.SerializedTransition.Index == serializedTransition.Index);
            var deleteIndex = serializedTransition.Index;
            if (index == 0 && count > 1)
                transitions.MoveArrayElement(stateTransitions[1].SerializedTransition.Index, deleteIndex++);
            transitions.DeleteArrayElementAtIndex(deleteIndex);
            ApplyModifications(DeletedTransition(serializedTransition.FromState.objectReferenceValue.name,
                serializedTransition.ToState.objectReferenceValue.name));
            if (count > 1) toggledIndex = stateIndex;
        }

        internal List<SerializedTransition> GetStateTransitions(UnityObject state)
        {
            return transitionsByFromStates[fromStates.IndexOf(state)].Select(t => t.SerializedTransition).ToList();
        }

        private void ApplyModifications(string msg)
        {
            RecordObject(serializedObject.targetObject, msg);
            serializedObject.ApplyModifiedProperties();
            Reset();
        }
    }
}
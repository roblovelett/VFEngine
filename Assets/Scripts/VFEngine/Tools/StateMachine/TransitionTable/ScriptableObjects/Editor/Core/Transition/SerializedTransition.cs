using JetBrains.Annotations;
using UnityEditor;

//using UnityEditor.Graphs;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor
{
    public readonly struct SerializedTransition
    {
        [CanBeNull] internal readonly SerializedProperty Transition;
        [CanBeNull] internal readonly SerializedProperty FromState;
        [CanBeNull] internal readonly SerializedProperty ToState;
        [CanBeNull] internal readonly SerializedProperty Conditions;
        internal readonly int Index;

        internal SerializedTransition(int index)
        {
            Transition = null;
            FromState = null;
            ToState = null;
            Conditions = null;
            Index = index;
        }
        
        internal SerializedTransition(SerializedProperty transition)
        {
            Transition = transition;
            FromState = Transition.FindPropertyRelative("FromState");
            ToState = Transition.FindPropertyRelative("ToState");
            Conditions = Transition.FindPropertyRelative("Conditions");
            Index = -1;
        }

        internal SerializedTransition(SerializedObject transitionTable, int index)
        {
            Transition = transitionTable.FindProperty("_transitions").GetArrayElementAtIndex(index);
            FromState = Transition.FindPropertyRelative("FromState");
            ToState = Transition.FindPropertyRelative("ToState");
            Conditions = Transition.FindPropertyRelative("Conditions");
            Index = index;
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
}
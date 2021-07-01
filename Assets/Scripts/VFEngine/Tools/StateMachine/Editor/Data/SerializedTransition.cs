using UnityEditor;

namespace VFEngine.Tools.StateMachine.Editor.Data
{
    using static EditorText;

    internal readonly struct SerializedTransition
    {
        internal readonly SerializedProperty Transition;
        internal readonly SerializedProperty FromState;
        internal readonly SerializedProperty ToState;
        internal readonly SerializedProperty Conditions;
        internal readonly int Index;

        internal SerializedTransition(SerializedProperty transition)
        {
            Transition = transition;
            FromState = Transition.FindPropertyRelative(FromStateProperty);
            ToState = Transition.FindPropertyRelative(ToStateProperty);
            Conditions = Transition.FindPropertyRelative(ConditionsProperty);
            Index = -1;
        }

        /*internal SerializedTransition(SerializedObject transitionTable, int index)
        {
            Transition = transitionTable.FindProperty(TransitionsProperty).GetArrayElementAtIndex(index);
            FromState = Transition.FindPropertyRelative(FromStateProperty);
            ToState = Transition.FindPropertyRelative(ToStateProperty);
            Conditions = Transition.FindPropertyRelative(ConditionsProperty);
            Index = index;
        }*/

        internal SerializedTransition(SerializedProperty transition, int index)
        {
            Transition = transition.GetArrayElementAtIndex(index);
            FromState = Transition.FindPropertyRelative(FromStateProperty);
            ToState = Transition.FindPropertyRelative(ToStateProperty);
            Conditions = Transition.FindPropertyRelative(ConditionsProperty);
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
using UnityEditor;

namespace VFEngine.Tools.StateMachine.Editor.Data
{
    internal readonly struct SerializedTransition
    {
        internal readonly SerializedProperty FromState;
        internal readonly SerializedProperty ToState;
        internal readonly SerializedProperty Conditions;
        internal readonly int Index;

        internal SerializedTransition(SerializedProperty transition)
        {
            FromState = transition.FindPropertyRelative("FromState");
            ToState = transition.FindPropertyRelative("ToState");
            Conditions = transition.FindPropertyRelative("Conditions");
            Index = -1;
        }

        internal SerializedTransition(SerializedObject transitionTable, int index)
        {
            var transition = transitionTable.FindProperty("transitions").GetArrayElementAtIndex(index);
            FromState = transition.FindPropertyRelative("FromState");
            ToState = transition.FindPropertyRelative("ToState");
            Conditions = transition.FindPropertyRelative("Conditions");
            Index = index;
        }

        internal SerializedTransition(SerializedProperty transitionInternal, int index)
        {
            var transition = transitionInternal.GetArrayElementAtIndex(index);
            FromState = transition.FindPropertyRelative("FromState");
            ToState = transition.FindPropertyRelative("ToState");
            Conditions = transition.FindPropertyRelative("Conditions");
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
using UnityEditor;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Editor.Data
{
    using static EditorText;

    internal struct SerializedTransition
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
            Conditions = Transition.FindPropertyRelative(ConditionsProperty);
        }

        internal void ClearProperties()
        {
            FromState.objectReferenceValue = null;
            ToState.objectReferenceValue = null;
            Conditions.ClearArray();
        }
    }
}
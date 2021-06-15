using System;
using VFEngine.Tools.StateMachine.State.ScriptableObjects;

namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects
{
    [Serializable]
    internal readonly struct Transition
    {
        //internal ModelSO FromState { get; }
        //internal ModelSO ToState {get;}
        internal Condition.ScriptableObjects.Data.Condition[] Conditions { get; }//Conditions;

        /*internal Transition(ModelSO fromState, ModelSO toState, Condition.ScriptableObjects.Data.Condition[] conditions)
        {
            FromState = fromState;
            ToState = toState;
            Conditions = conditions;
        }*/
    }
}
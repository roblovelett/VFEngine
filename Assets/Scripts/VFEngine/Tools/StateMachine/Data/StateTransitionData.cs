using System;
using VFEngine.Tools.StateMachine.ScriptableObjects;

namespace VFEngine.Tools.StateMachine.Data
{
    [Serializable]
    internal struct StateTransitionData
    {
        internal StateSO FromState;
        internal StateSO ToState;
        internal StateTransitionConditionData[] Conditions;

        internal StateTransitionData(StateSO fromState, StateSO toState, StateTransitionConditionData[] conditions)
        {
            FromState = fromState;
            ToState = toState;
            Conditions = conditions;
        }
    }
}
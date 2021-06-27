using System;
using VFEngine.Tools.StateMachine.ScriptableObjects;

namespace VFEngine.Tools.StateMachine.Data
{
    [Serializable]
    internal struct StateTransitionConditionData
    {
        internal bool ExpectedResult;
        internal StateConditionSO Condition;
        internal StateConditionOperator Operator;

        internal StateTransitionConditionData(bool expectedResult, StateConditionSO condition,
            StateConditionOperator @operator)
        {
            ExpectedResult = expectedResult;
            Condition = condition;
            Operator = @operator;
        }
    }
}
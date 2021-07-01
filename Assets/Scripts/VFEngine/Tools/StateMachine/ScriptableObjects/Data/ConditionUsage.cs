using System;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Data
{
    [Serializable]
    public struct ConditionUsage
    {
        public Result ExpectedResult;
        public StateConditionSO Condition;
        public Operator Operator;
    }
}
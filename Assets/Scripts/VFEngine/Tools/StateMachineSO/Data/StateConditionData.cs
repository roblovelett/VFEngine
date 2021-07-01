﻿namespace VFEngine.Tools.StateMachineSO.Data
{
    internal struct StateConditionData
    {
        private bool statement;
        private bool isMet;
        private readonly StateMachine stateMachine;
        private readonly bool expectedResult;
        internal readonly StateCondition Condition;

        internal StateConditionData(StateMachine stateMachineInternal, StateCondition condition, bool expectedResultInternal)
        {
            stateMachine = stateMachineInternal;
            Condition = condition;
            expectedResult = expectedResultInternal;
            statement = false;
            isMet = false;
        }

        internal bool IsMet()
        {
            statement = Condition.GetStatement();
            isMet = statement == expectedResult;
#if UNITY_EDITOR
            stateMachine.debug.TransitionConditionResult(Condition.OriginSO.name, statement, isMet);
#endif
            return isMet;
        }
    }
}
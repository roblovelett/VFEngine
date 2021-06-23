using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    internal class StateConditionSO : ScriptableObject
    {
        internal readonly StateMachine StateMachine;
        internal readonly StateCondition Condition;
        internal readonly bool ExpectedResult;
        private bool statement;
        private bool isMet;

        internal StateConditionSO(StateMachine stateMachine, StateCondition condition, bool expectedResult)
        {
            StateMachine = stateMachine;
            Condition = condition;
            ExpectedResult = expectedResult;
        }

        internal void Enter()
        {
            ((IState) Condition).Enter();
        }

        internal void Exit()
        {
            ((IState) Condition).Exit();
        }

        internal void ClearCache()
        {
            Condition.ClearCache();
        }

        internal bool IsMet()
        {
            statement = Condition.GetStatement();
            isMet = statement == ExpectedResult;

#if UNITY_EDITOR
            StateMachine.debug.TransitionConditionResult(Condition.OriginSO.name, statement, isMet);
#endif
            return isMet;
        }
    }
}
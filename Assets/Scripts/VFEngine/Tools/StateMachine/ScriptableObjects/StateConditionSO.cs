using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    public abstract class StateConditionSO : ScriptableObject
    {
        private StateCondition condition;
        private object @object;

        internal StateConditionData Get(StateMachine stateMachine, bool expectedResult,
            Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out @object)) return Condition(stateMachine, expectedResult);
            condition = Condition();
            condition.OriginSO = this;
            createdInstances.Add(this, condition);
            condition.Awake(stateMachine);
            @object = condition;
            return Condition(stateMachine, expectedResult);
        }

        private StateConditionData Condition(StateMachine stateMachine, bool expectedResult)
        {
            return new StateConditionData(stateMachine, @object as StateCondition, expectedResult);
        }

        protected abstract StateCondition Condition();
    }

    public abstract class StateConditionSO<T> : StateConditionSO where T : StateCondition, new()
    {
        protected override StateCondition Condition()
        {
            return new T();
        }
    }
    
    public struct StateConditionData
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
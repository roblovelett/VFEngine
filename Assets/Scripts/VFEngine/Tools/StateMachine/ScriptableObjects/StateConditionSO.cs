using System.Collections.Generic;
using UnityEngine;
using VFEngine.Tools.StateMachine.Data;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    public abstract class StateConditionSO : ScriptableObject
    {
        private StateCondition condition;
        private object @object;

        internal StateConditionData GetCondition(StateMachine stateMachine, bool expectedResult,
            Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out @object)) return StateCondition(stateMachine, expectedResult);
            condition = CreateCondition();
            condition.OriginSO = this;
            createdInstances.Add(this, condition);
            condition.Awake(stateMachine);
            @object = condition;
            return StateCondition(stateMachine, expectedResult);
        }

        private StateConditionData StateCondition(StateMachine stateMachine, bool expectedResult)
        {
            return new StateConditionData(stateMachine, @object as StateCondition, expectedResult);
        }

        private protected abstract StateCondition CreateCondition();
    }

    public abstract class StateConditionSO<T> : StateConditionSO where T : StateCondition, new()
    {
        private protected override StateCondition CreateCondition()
        {
            return new T();
        }
    }
}
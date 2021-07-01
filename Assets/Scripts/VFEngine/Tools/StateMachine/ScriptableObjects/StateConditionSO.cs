using System.Collections.Generic;
using UnityEngine;
using VFEngine.Tools.StateMachine.Data;
using VFEngine.Tools.StateMachine.ScriptableObjects.Data;

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
}
using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects
{
    public abstract class StateConditionSO : ScriptableObject
    {
        internal StateCondition Get(StateMachine stateMachine, bool trueResult,
            Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var @object))
                return new StateCondition(stateMachine, @object as Condition, trueResult);
            var condition = CreateCondition();
            condition.OriginSO = this;
            createdInstances.Add(this, condition);
            ((IState) condition).Awake(stateMachine);
            @object = condition;
            return new StateCondition(stateMachine, @object as Condition, trueResult);
        }

        protected abstract Condition CreateCondition();
    }

    internal abstract class StateConditionSO<T> : StateConditionSO where T : Condition, new()
    {
        protected override Condition CreateCondition()
        {
            return new T();
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects
{
    public abstract class StateActionSO : ScriptableObject
    {
        internal StateAction Get(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var @object)) return @object as StateAction;
            var action = CreateAction();
            createdInstances.Add(this, action);
            action.OriginSO = this;
            ((IState) action).Awake(stateMachine);
            return action;
        }

        protected abstract StateAction CreateAction();
    }

    internal abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
    {
        protected override StateAction CreateAction()
        {
            return new T();
        }
    }
}
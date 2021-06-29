﻿using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    public abstract class StateActionSO : ScriptableObject
    {
        private StateAction action;
        private object @object;

        internal StateAction Get(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out @object)) return (StateAction) @object;
            action = Action();
            createdInstances.Add(this, action);
            action.OriginSO = this;
            action.Awake(stateMachine);
            return action;
        }

        protected abstract StateAction Action();
    }

    public abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
    {
        protected override StateAction Action()
        {
            return new T();
        }
    }
}
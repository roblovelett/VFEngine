using System.Collections.Generic;
using UnityEngine;
using VFEngine.Tools.StateMachine;
using static VFEngine.Tools.StateMachine.Data.StateMachineText;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    [CreateAssetMenu(fileName = NewState, menuName = StateMenu)]
    public class StateSO : ScriptableObject
    {
        private State state;

        internal State Get(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var @object)) return @object as State;
            state = new State();
            createdInstances.Add(this, state);
            state.OriginSO = this;
            state.StateMachine = stateMachine;
            state.Transitions = new StateTransition[0];
            return state;
        }
    }
}
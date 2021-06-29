using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New State", menuName = "Tools/State Machine/State")]
    public class StateSO : ScriptableObject
    {
        private State state;
        private object @object;

        internal State Get(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out @object)) return @object as State;
            state = new State();
            createdInstances.Add(this, state);
            state.OriginSO = this;
            state.StateMachine = stateMachine;
            state.Transitions = new StateTransition[0];
            return state;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New State", menuName = "State Machines/State")]
    public class StateSO : ScriptableObject
    {
        private State state;
        
        internal State Get(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var obj)) return (State) obj;
            state = new State();
            createdInstances.Add(this, state);
            state.OriginSO = this;
            state.StateMachine = stateMachine;
            state.Transitions = new StateTransition[0];
            return state;
        }
    }
}
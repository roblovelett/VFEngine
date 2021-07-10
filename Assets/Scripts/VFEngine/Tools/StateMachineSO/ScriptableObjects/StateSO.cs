using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New State", menuName = "State Machine SO/State", order = 0)]
    internal class StateSO : ScriptableObject
    {
        [SerializeField] private StateActionSO[] actions;

        internal State GetState(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var @object)) return @object as State;
            var state = new State();
            createdInstances.Add(this, state);
            state.OriginSO = this;
            state.StateMachine = stateMachine;
            state.Transitions = new StateTransition[0];

            #region State Actions

            var actionsAmount = actions.Length;
            var stateActions = new StateAction[actionsAmount];
            for (var idx = 0; idx < actionsAmount; idx++)
                stateActions[idx] = actions[idx].Get(stateMachine, createdInstances);
            state.Actions = stateActions;

            #endregion

            return state;
        }
    }
}
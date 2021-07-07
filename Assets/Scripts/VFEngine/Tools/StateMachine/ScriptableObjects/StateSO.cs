using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.Menu;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    using static EditorText;

    [CreateAssetMenu(fileName = NewState, menuName = StateMenu)]
    public class StateSO : ScriptableObject
    {
        [SerializeField] private StateActionSO[] actions;

        internal State Get(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
        {
            if (createdInstances.TryGetValue(this, out var @object)) return @object as State;
            var count = (actions as ICollection).Count;
            var stateActions = new StateAction[count];
            for (var i = 0; i < count; i++) stateActions[i] = actions[i].Get(stateMachine, createdInstances);
            var state = new State(this, stateMachine, stateActions);
            createdInstances.Add(this, state);
            return state;
        }
    }
}
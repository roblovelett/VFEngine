﻿using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New State Machine Transition Table", menuName = "State Machine/Transition Table")]
    internal class StateMachineTransitionTableSO : ScriptableObject
    {
        /*
        [SerializeField] private TransitionItem[] _transitions = default(TransitionItem[]);

        /// <summary>
        ///     Will get the initial state and instantiate all subsequent states, transitions, actions and conditions.
        /// </summary>
        */
        internal State GetInitialState(StateMachine stateMachine)
        {
            /*
            var states = new List<State>();
            var transitions = new List<StateTransition>();
            var createdInstances = new Dictionary<ScriptableObject, object>();
            var fromStates = _transitions.GroupBy(transition => transition.FromState);
            foreach (var fromState in fromStates)
            {
                if (fromState.Key == null)
                    throw new ArgumentNullException(nameof(fromState.Key), $"TransitionTable: {name}");
                var state = fromState.Key.GetState(stateMachine, createdInstances);
                states.Add(state);
                transitions.Clear();
                foreach (var transitionItem in fromState)
                {
                    if (transitionItem.ToState == null)
                        throw new ArgumentNullException(nameof(transitionItem.ToState),
                            $"TransitionTable: {name}, From State: {fromState.Key.name}");
                    var toState = transitionItem.ToState.GetState(stateMachine, createdInstances);
                    ProcessConditionUsages(stateMachine, transitionItem.Conditions, createdInstances,
                        out var conditions, out var resultGroups);
                    transitions.Add(new StateTransition(toState, conditions, resultGroups));
                }

                state._transitions = transitions.ToArray();
            }

            return states.Count > 0
                ? states[0]
                : throw new InvalidOperationException($"TransitionTable {name} is empty.");
            */
            return null;
        }

        /*
        private static void ProcessConditionUsages(StateMachine stateMachine, ConditionUsage[] conditionUsages,
            Dictionary<ScriptableObject, object> createdInstances, out StateCondition[] conditions,
            out int[] resultGroups)
        {
            var count = conditionUsages.Length;
            conditions = new StateCondition[count];
            for (var i = 0; i < count; i++)
                conditions[i] = conditionUsages[i].Condition.GetCondition(stateMachine,
                    conditionUsages[i].ExpectedResult == Result.True, createdInstances);
            var resultGroupsList = new List<int>();
            for (var i = 0; i < count; i++)
            {
                var idx = resultGroupsList.Count;
                resultGroupsList.Add(1);
                while (i < count - 1 && conditionUsages[i].Operator == Operator.And)
                {
                    i++;
                    resultGroupsList[idx]++;
                }
            }

            resultGroups = resultGroupsList.ToArray();
        }*/
    }
}
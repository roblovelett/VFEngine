using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VFEngine.Tools.StateMachine.Data;
using VFEngine.Tools.StateMachine.ScriptableObjects.Data;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    using static Operator;
    using static Result;
    using static StateMachineText;

    [CreateAssetMenu(fileName = NewTransitionTable, menuName = TransitionTableMenuName)]
    public class TransitionTableSO : ScriptableObject
    {
        [SerializeField] private TransitionItem[] transitions;

        internal State GetInitialState(StateMachine stateMachine)
        {
            var resultGroupsList = new List<int>();
            var states = new List<State>();
            var stateTransitions = new List<StateTransition>();
            var createdInstances = new Dictionary<ScriptableObject, object>();
            var fromStates = transitions.GroupBy(t => t.FromState);
            foreach (var fromState in fromStates)
            {
                if (fromState.Key == null)
                    throw new ArgumentNullException(nameof(fromState.Key), TransitionTableName(name));
                var state = fromState.Key.Get(stateMachine, createdInstances);
                states.Add(state);
                stateTransitions.Clear();
                foreach (var transition in fromState)
                {
                    if (transition.ToState == null)
                        throw new ArgumentNullException(nameof(transition.ToState),
                            TransitionError(name, fromState.Key.name));
                    var toState = transition.ToState.Get(stateMachine, createdInstances);
                    var transitionConditionsAmount = transition.Conditions.Length;
                    var conditions = new StateConditionData[transitionConditionsAmount];
                    int conditionsIndex;
                    for (conditionsIndex = 0; conditionsIndex < transitionConditionsAmount; conditionsIndex++)
                        conditions[conditionsIndex] = transition.Conditions[conditionsIndex].Condition.Get(stateMachine,
                            transition.Conditions[conditionsIndex].ExpectedResult == True, createdInstances);
                    for (conditionsIndex = 0; conditionsIndex < transitionConditionsAmount; conditionsIndex++)
                    {
                        var resultGroupsIndex = resultGroupsList.Count;
                        resultGroupsList.Add(1);
                        while (conditionsIndex < transitionConditionsAmount - 1 &&
                               transition.Conditions[conditionsIndex].Operator == And)
                        {
                            conditionsIndex++;
                            resultGroupsList[resultGroupsIndex]++;
                        }
                    }

                    var resultGroups = resultGroupsList.ToArray();
                    stateTransitions.Add(new StateTransition(toState, conditions, resultGroups));
                }

                state.Transitions = stateTransitions.ToArray();
            }

            return states.Count > 0 ? states[0] : throw new InvalidOperationException(StateError(name));
        }
    }
}
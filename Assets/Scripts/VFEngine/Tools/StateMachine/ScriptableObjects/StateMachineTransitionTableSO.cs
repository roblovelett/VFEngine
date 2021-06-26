using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VFEngine.Tools.StateMachine.Data;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    using static StateMachineText;
    using static StateConditionOperator;

    [CreateAssetMenu(fileName = "New State Machine Transition Table", menuName = "State Machine/Transition Table")]
    public class StateMachineTransitionTableSO : ScriptableObject
    {
        [SerializeField] private StateTransitionData[] transitions;
        private int transitionConditionsAmount;
        private int resultGroupsIndex;
        private int conditionsIndex;
        private int[] resultGroups;
        private State state;
        private State toState;
        private StateMachine stateMachine;
        private List<int> resultGroupsList;
        private List<State> states;
        private List<StateTransition> stateTransitions;
        private Dictionary<ScriptableObject, object> createdInstances;
        private StateConditionData[] conditions;
        private IEnumerable<IGrouping<StateSO, StateTransitionData>> fromStates;

        internal void OnEnable()
        {
            transitions = default(StateTransitionData[]);
            states = new List<State>();
            stateTransitions = new List<StateTransition>();
            createdInstances = new Dictionary<ScriptableObject, object>();
            fromStates = transitions!.GroupBy(transition => transition.FromState);
            resultGroupsList = new List<int>();
        }

        internal State GetInitialState(StateMachine stateMachineInternal)
        {
            stateMachine = stateMachineInternal;
            foreach (var fromState in fromStates)
            {
                if (fromState.Key == null)
                    throw new ArgumentNullException(nameof(fromState.Key), TransitionTableName(name));
                state = fromState.Key.Get(stateMachine, createdInstances);
                states.Add(state);
                stateTransitions.Clear();
                foreach (var transition in fromState)
                {
                    if (transition.ToState == null)
                        throw new ArgumentNullException(nameof(transition.ToState),
                            TransitionError(name, fromState.Key.name));
                    toState = transition.ToState.Get(stateMachine, createdInstances);
                    transitionConditionsAmount = transition.Conditions.Length;
                    conditions = new StateConditionData[transitionConditionsAmount];
                    for (conditionsIndex = 0; conditionsIndex < transitionConditionsAmount; conditionsIndex++)
                        conditions[conditionsIndex] = transition.Conditions[conditionsIndex].Condition
                            .GetCondition(stateMachine, transition.Conditions[conditionsIndex].ExpectedResult,
                                createdInstances);
                    for (conditionsIndex = 0; conditionsIndex < transitionConditionsAmount; conditionsIndex++)
                    {
                        resultGroupsIndex = resultGroupsList.Count;
                        resultGroupsList.Add(1);
                        while (conditionsIndex < transitionConditionsAmount - 1 &&
                               transition.Conditions[conditionsIndex].Operator == And)
                        {
                            conditionsIndex++;
                            resultGroupsList[resultGroupsIndex]++;
                        }
                    }

                    resultGroups = resultGroupsList.ToArray();
                    stateTransitions.Add(new StateTransition(toState, conditions, resultGroups));
                }

                state.Transitions = stateTransitions.ToArray();
            }

            return states.Count > 0 ? states[0] : throw new InvalidOperationException(StateError(name));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.Menu;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    using static TransitionTableSO.TransitionItem.Operator;
    using static TransitionTableSO.TransitionItem.Result;
    using static EditorText;

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
            var fromStates = transitions.GroupBy(t => t.fromState);
            foreach (var fromState in fromStates)
            {
                if (fromState.Key == null)
                    throw new ArgumentNullException(nameof(fromState.Key), TransitionTableName(name));
                var state = fromState.Key.Get(stateMachine, createdInstances);
                states.Add(state);
                stateTransitions.Clear();
                foreach (var transition in fromState)
                {
                    if (transition.toState == null)
                        throw new ArgumentNullException(nameof(transition.toState),
                            TransitionError(name, fromState.Key.name));
                    var toState = transition.toState.Get(stateMachine, createdInstances);
                    var transitionConditionsAmount = transition.conditions.Length;
                    var conditions = new StateConditionData[transitionConditionsAmount];
                    int conditionsIndex;
                    for (conditionsIndex = 0; conditionsIndex < transitionConditionsAmount; conditionsIndex++)
                        conditions[conditionsIndex] = transition.conditions[conditionsIndex].condition.Get(stateMachine,
                            transition.conditions[conditionsIndex].expectedResult == True, createdInstances);
                    for (conditionsIndex = 0; conditionsIndex < transitionConditionsAmount; conditionsIndex++)
                    {
                        var resultGroupsIndex = resultGroupsList.Count;
                        resultGroupsList.Add(1);
                        while (conditionsIndex < transitionConditionsAmount - 1 &&
                               transition.conditions[conditionsIndex].@operator == And)
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

        [Serializable]
        public class TransitionItem
        {
            [SerializeField] internal StateSO fromState;
            [SerializeField] internal StateSO toState;
            [SerializeField] internal ConditionUsage[] conditions;

            [Serializable]
            public struct ConditionUsage
            {
                [SerializeField] internal Result expectedResult;
                [SerializeField] internal StateConditionSO condition;
                [SerializeField] internal Operator @operator;
            }

            public enum Result
            {
                True,
                False
            }

            public enum Operator
            {
                And,
                Or
            }
        }
    }
}
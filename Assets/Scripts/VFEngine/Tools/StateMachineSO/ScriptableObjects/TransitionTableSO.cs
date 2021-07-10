using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects
{
    using static TransitionTableSO.Operator;
    using static TransitionTableSO.Result;

    [CreateAssetMenu(fileName = "New Transition Table", menuName = "State Machine SO/State Transition Table",
        order = 4)]
    internal class TransitionTableSO : ScriptableObject
    {
        [SerializeField] private TransitionItem[] transitions = default(TransitionItem[]);
        

        internal State GetInitialState(StateMachine stateMachine)
        {
            var states = new List<State>();
            var stateTransitions = new List<StateTransition>();
            var createdInstances = new Dictionary<ScriptableObject, object>();
            var fromStates = transitions.GroupBy(transition => transition.FromState);
            foreach (var fromState in fromStates)
            {
                if (fromState.Key == null)
                    throw new ArgumentNullException(nameof(fromState.Key), $"TransitionTable: {name}");
                var state = fromState.Key.GetState(stateMachine, createdInstances);
                states.Add(state);
                stateTransitions.Clear();
                foreach (var transition in fromState)
                {
                    if (transition.ToState == null)
                        throw new ArgumentNullException(nameof(transition.ToState),
                            $"TransitionTable: {name}, From State: {fromState.Key.name}");
                    var toState = transition.ToState.GetState(stateMachine, createdInstances);

                    #region Process Condition Usage

                    var conditions = new StateCondition[transition.Conditions.Length];
                    for (var idx = 0; idx < transition.Conditions.Length; idx++)
                        conditions[idx] = transition.Conditions[idx].Condition.Get(stateMachine,
                            transition.Conditions[idx].ExpectedResult == True, createdInstances);
                    var resultGroupsList = new List<int>();
                    for (var idx = 0; idx < transition.Conditions.Length; idx++)
                    {
                        var resultGroupsAmount = resultGroupsList.Count;
                        resultGroupsList.Add(1);
                        while (idx < transition.Conditions.Length - 1 && transition.Conditions[idx].Operator == And)
                        {
                            idx++;
                            resultGroupsList[resultGroupsAmount]++;
                        }
                    }

                    var resultGroups = resultGroupsList.ToArray();

                    #endregion

                    stateTransitions.Add(new StateTransition(toState, conditions, resultGroups));
                }

                state.Transitions = stateTransitions.ToArray();
            }

            return states.Count > 0
                ? states[0]
                : throw new InvalidOperationException($"TransitionTable {name} is empty.");
        }

        [Serializable]
        internal struct TransitionItem
        {
            internal StateSO FromState;
            internal StateSO ToState;
            internal ConditionUsage[] Conditions;

            private TransitionItem(StateSO fromState, StateSO toState, ConditionUsage[] conditions)
            {
                FromState = fromState;
                ToState = toState;
                Conditions = conditions;
            }
        }

        [Serializable]
        internal struct ConditionUsage
        {
            internal Result ExpectedResult;
            internal Operator Operator;
            internal StateConditionSO Condition;

            private ConditionUsage(Result expectedResult, Operator @operator, StateConditionSO condition)
            {
                ExpectedResult = expectedResult;
                Operator = @operator;
                Condition = condition;
            }
        }

        internal enum Result
        {
            True,
            False
        }

        internal enum Operator
        {
            And,
            Or
        }
    }
}
using System.Collections.Generic;

// ReSharper disable SwitchStatementMissingSomeEnumCasesNoDefault
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace VFEngine.Tools.StateMachineSO
{
    internal class StateTransition : IState
    {
        private readonly int resultGroupsAmount;
        private readonly bool[] results;
        private readonly State targetState;
        private readonly StateCondition[] conditions;

        internal enum Input
        {
            Enter,
            Exit,
            Get,
            ClearConditionsCache
        }

        internal StateTransition(State targetState, StateCondition[] conditions,
            IReadOnlyCollection<int> resultGroups = null)
        {
            this.targetState = targetState;
            this.conditions = conditions;
            resultGroupsAmount = resultGroups?.Count ?? 1;
            results = new bool[resultGroupsAmount];
        }

        void IState.Awake(StateMachine stateMachine)
        {
        }

        void IState.Enter()
        {
            Transition(conditions, Input.Enter);
        }

        void IState.Update()
        {
        }

        void IState.Exit()
        {
            Transition(conditions, Input.Exit);
        }

        private static void Transition(IEnumerable<StateCondition> stateConditions, Input input)
        {
            foreach (var transition in stateConditions)
                switch (input)
                {
                    case Input.ClearConditionsCache:
                        transition.Condition.ClearCache();
                        break;
                    case Input.Enter:
                        ((IState) transition.Condition).Enter();
                        break;
                    case Input.Exit:
                        ((IState) transition.Condition).Exit();
                        break;
                }
        }

        internal void ClearConditionsCache()
        {
            Transition(conditions, Input.ClearConditionsCache);
        }

        internal bool Get(out State state)
        {
            #region Can Transition

#if UNITY_EDITOR
            targetState.StateMachine.debugger.TransitionEvaluationBegin(targetState.OriginSO.name);
#endif
            var idx = 0;
            var onFirstResult = true;
            foreach (var _ in conditions)
            {
                if (onFirstResult)
                {
                    results[idx] = conditions[idx].IsMet();
                    onFirstResult = false;
                }

                results[idx] = results[idx] && conditions[idx].IsMet();
                idx++;
                if (idx < resultGroupsAmount || idx < conditions.Length) continue;
                idx = 0;
                break;
            }

            var canTransition = false;
            foreach (var _ in results)
            {
                canTransition = results[idx];
                idx++;
                if (idx >= resultGroupsAmount && !canTransition) break;
            }

#if UNITY_EDITOR
            targetState.StateMachine.debugger.TransitionEvaluationEnd(canTransition, targetState.Actions);
#endif

            #endregion

            state = canTransition ? targetState : null;
            return state != null;
        }
    }
}
using VFEngine.Tools.StateMachineSO.Data;

namespace VFEngine.Tools.StateMachineSO
{
    internal class StateTransition : IState
    {
        private readonly State targetState;
        private readonly StateConditionData[] conditions;
        private readonly int[] resultGroups;
        private readonly bool[] results;

        internal StateTransition(State targetStateInternal, StateConditionData[] conditionsInternal,
            int[] resultGroupsInternal = null)
        {
            targetState = targetStateInternal;
            conditions = conditionsInternal;
            if (resultGroupsInternal != null)
                resultGroups = resultGroupsInternal.Length > 0 ? resultGroupsInternal : new int[1];
            results = new bool[resultGroups.Length];
        }

        internal bool TryGetTransition(out State state)
        {
#if UNITY_EDITOR
            targetState.StateMachine.debug.TransitionEvaluationBegin(targetState.OriginSO.name);
#endif
            for (int resultIndex = 0, conditionsIndex = 0;
                resultIndex < resultGroups.Length && conditionsIndex < conditions.Length;
                conditionsIndex++)
            for (var resultsIndex = 0; resultsIndex < resultGroups[resultIndex]; resultsIndex++, conditionsIndex++)
                results[resultIndex] = resultsIndex == 0
                    ? conditions[conditionsIndex].IsMet()
                    : results[resultIndex] && conditions[conditionsIndex].IsMet();
            var hasTargetState = false;
            for (var resultsIndex = 0; resultsIndex < resultGroups.Length && !hasTargetState; resultsIndex++)
                hasTargetState = results[resultsIndex];
#if UNITY_EDITOR
            targetState.StateMachine.debug.TransitionEvaluationEnd(hasTargetState);
#endif
            state = hasTargetState ? targetState : null;
            return state != null;
        }

        internal void ClearCache()
        {
            foreach (var condition in conditions) condition.Condition.ClearCache();
        }

        void IState.Enter()
        {
            foreach (var condition in conditions) (condition.Condition as IState).Enter();
        }

        void IState.Exit()
        {
            foreach (var condition in conditions) (condition.Condition as IState).Exit();
        }
    }
}
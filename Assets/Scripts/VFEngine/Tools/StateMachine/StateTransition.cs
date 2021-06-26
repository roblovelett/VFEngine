using VFEngine.Tools.StateMachine.Data;

namespace VFEngine.Tools.StateMachine
{
    internal class StateTransition : IState
    {
        private int resultGroupsIndex;
        private int resultGroupIndex;
        private int conditionsIndex;
        private bool hasTargetState;
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
            for (resultGroupsIndex = 0, conditionsIndex = 0;
                resultGroupsIndex < resultGroups.Length && conditionsIndex < conditions.Length;
                conditionsIndex++)
            for (resultGroupIndex = 0;
                resultGroupIndex < resultGroups[resultGroupsIndex];
                resultGroupIndex++, conditionsIndex++)
                results[resultGroupsIndex] = resultGroupIndex == 0
                    ? conditions[conditionsIndex].IsMet()
                    : results[resultGroupsIndex] && conditions[conditionsIndex].IsMet();
            hasTargetState = false;
            for (resultGroupsIndex = 0; resultGroupsIndex < resultGroups.Length && !hasTargetState; resultGroupsIndex++)
                hasTargetState = hasTargetState || results[resultGroupsIndex];
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
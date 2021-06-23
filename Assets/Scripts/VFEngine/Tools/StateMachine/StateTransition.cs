using VFEngine.Tools.StateMachine.Data.ScriptableObjects;

namespace VFEngine.Tools.StateMachine
{
    internal class StateTransition : IState
    {
        private readonly State targetState;
        private readonly StateConditionSO[] conditions;
        private readonly int[] resultGroups;
        private readonly bool[] results;
        private int resultGroupsAmount;
        private int result;
        private int resultIndex;
        private int resultGroup;
        private bool hasTargetState;

        internal StateTransition(State targetStateInternal, StateConditionSO[] conditionsInternal,
            int[] resultGroupsInternal = null)
        {
            targetState = targetStateInternal;
            conditions = conditionsInternal;
            if (resultGroupsInternal != null)
                resultGroups = resultGroupsInternal.Length > 0 ? resultGroupsInternal : new int[1];
            results = new bool[resultGroups.Length];
        }

        public bool TryGetTransition(out State state)
        {
#if UNITY_EDITOR
            targetState.StateMachine.debug.TransitionEvaluationBegin(targetState.OriginSO.name);
#endif
            resultGroupsAmount = resultGroups.Length;
            for (result = 0, resultIndex = 0; result < resultGroupsAmount && resultIndex < conditions.Length; result++)
            for (resultGroup = 0; resultGroup < resultGroups[result]; resultGroup++, resultIndex++)
                results[result] = resultGroup == 0
                    ? conditions[resultIndex].IsMet()
                    : results[result] && conditions[resultIndex].IsMet();
            hasTargetState = false;
            for (resultGroup = 0; resultGroup < resultGroupsAmount && !hasTargetState; resultGroup++)
                hasTargetState = results[resultGroup];
            state = hasTargetState ? targetState : null;
#if UNITY_EDITOR
            targetState.StateMachine.debug.TransitionEvaluationEnd(hasTargetState);
#endif
            return state != null;
        }

        void IState.Enter()
        {
            foreach (var condition in conditions) condition.Enter();
        }

        void IState.Exit()
        {
            foreach (var condition in conditions) condition.Exit();
        }

        internal void ClearConditionsCache()
        {
            foreach (var condition in conditions) condition.ClearCache();
        }
    }
}
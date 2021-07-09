// ReSharper disable MergeIntoPattern
namespace VFEngine.Tools.StateMachineSO
{
    public class StateTransition : IState
    {
        private readonly State targetState;
        private readonly StateCondition[] conditions;
        private readonly int[] resultGroups;
        private readonly bool[] results;

        internal StateTransition()
        {
        }

        public StateTransition(State targetState, StateCondition[] conditions, int[] resultGroups = null)
        {
            this.targetState = targetState;
            this.conditions = conditions;
            this.resultGroups = resultGroups != null && resultGroups.Length > 0 ? resultGroups : new int[1];
            results = new bool[this.resultGroups.Length];
        }

        internal bool TryGetTransition(out State state)
        {
            state = ShouldTransition() ? targetState : null;
            return state != null;
        }

        /*public void Enter()
        {
            for (int i = 0; i < _conditions.Length; i++)
                _conditions[i]._condition.Enter();
        }

        public void Exit()
        {
            for (int i = 0; i < _conditions.Length; i++)
                _conditions[i]._condition.Exit();
        }*/

        // ReSharper disable Unity.PerformanceAnalysis
        private bool ShouldTransition()
        {
#if UNITY_EDITOR
            targetState.StateMachine.debugger.TransitionEvaluationBegin(targetState.OriginSO.name);
#endif
            var count = resultGroups.Length;
            for (int i = 0, idx = 0; i < count && idx < conditions.Length; i++)
            for (var j = 0; j < resultGroups[i]; j++, idx++)
                results[i] = j == 0 ? conditions[idx].IsMet() : results[i] && conditions[idx].IsMet();
            var ret = false;
            for (var i = 0; i < count && !ret; i++) ret = results[i];
#if UNITY_EDITOR
            targetState.StateMachine.debugger.TransitionEvaluationEnd(ret, targetState.Actions);
#endif
            return ret;
        }

        internal void ClearConditionsCache()
        {
            for (var i = 0; i < conditions.Length; i++) conditions[i].Condition.ClearStatementCache();
        }

        void IState.Enter()
        {
            //throw new System.NotImplementedException();
        }

        void IState.Update()
        {
            //throw new System.NotImplementedException();
        }

        void IState.Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
}
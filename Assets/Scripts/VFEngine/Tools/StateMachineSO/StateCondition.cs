using VFEngine.Tools.StateMachineSO.ScriptableObjects;

namespace VFEngine.Tools.StateMachineSO
{
    public abstract class Condition : IState
    {
        private bool cached;
        private bool statement;
        protected internal StateConditionSO OriginSO { get; internal set; }
        protected abstract bool Statement();

        internal bool Get()
        {
            if (cached) return statement;
            cached = true;
            statement = Statement();
            return statement;
        }

        internal void ClearCache()
        {
            cached = false;
        }

        void IState.Awake(StateMachine stateMachine)
        {
        }

        void IState.Enter()
        {
        }

        void IState.Update()
        {
        }

        void IState.Exit()
        {
        }
    }

    internal readonly struct StateCondition
    {
        private readonly StateMachine stateMachine;
        private readonly bool trueResult;
        internal readonly Condition Condition;

        internal StateCondition(StateMachine stateMachine, Condition condition, bool trueResult)
        {
            this.stateMachine = stateMachine;
            this.trueResult = trueResult;
            Condition = condition;
        }

        internal bool IsMet()
        {
            var statement = Condition.Get();
            var isMet = statement == trueResult;
#if UNITY_EDITOR
            stateMachine.debugger.TransitionConditionResult(Condition.OriginSO.name, statement, isMet);
#endif
            return isMet;
        }
    }
}
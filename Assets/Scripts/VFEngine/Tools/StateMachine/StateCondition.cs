using VFEngine.Tools.StateMachine.ScriptableObjects;

namespace VFEngine.Tools.StateMachine
{
    public abstract class StateCondition : IState
    {
        private bool isCached;
        private bool cachedStatement;
        protected internal StateConditionSO OriginSO { get; internal set; }
        protected abstract bool Statement();

        internal bool GetStatement()
        {
            if (isCached) return cachedStatement;
            isCached = true;
            cachedStatement = Statement();
            return cachedStatement;
        }

        internal void ClearCache()
        {
            isCached = false;
        }

        public virtual void Awake(StateMachine stateMachine)
        {
        }

        void IState.Enter()
        {
        }

        void IState.Exit()
        {
        }
    }
}
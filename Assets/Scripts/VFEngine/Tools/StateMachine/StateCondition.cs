using VFEngine.Tools.StateMachine.ScriptableObjects;

// ReSharper disable UnusedParameter.Global
namespace VFEngine.Tools.StateMachine
{
    public abstract class StateCondition : IState
    {
        private bool isCached;
        protected internal StateConditionSO OriginSO { get; internal set; }
        protected abstract bool Statement();

        internal bool GetStatement()
        {
            var cachedStatement = Statement();
            if (isCached) return cachedStatement;
            isCached = true;
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
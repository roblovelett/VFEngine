using VFEngine.Tools.StateMachine.ScriptableObjects;
using static UnityEngine.ScriptableObject;


namespace VFEngine.Tools.StateMachine
{
    internal abstract class StateCondition : IState
    {
        private bool isCached;
        private bool cachedStatement = default(bool);

        protected internal StateConditionSO OriginSO { get; private set; }

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

        internal virtual void Awake(StateMachine stateMachine)
        {
            isCached = false;
            cachedStatement = default(bool);
            OriginSO = CreateInstance<StateConditionSO>();
        }

        void IState.Enter() { }
        void IState.Exit(){}
    }
}
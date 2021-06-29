using VFEngine.Tools.StateMachine.ScriptableObjects;

// ReSharper disable EmptyConstructor
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Tools.StateMachine
{
    public abstract class StateAction : IState
    {
        protected internal StateActionSO OriginSO { get; internal set; }

        protected StateAction(){}

        public virtual void Awake(StateMachine stateMachine)
        {
        }

        void IState.Enter()
        {
        }

        public abstract void Update();

        void IState.Exit()
        {
        }
    }
}
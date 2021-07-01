using VFEngine.Tools.StateMachineSO.ScriptableObjects;

// ReSharper disable UnusedParameter.Global
// ReSharper disable EmptyConstructor
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Tools.StateMachineSO
{
    public abstract class StateAction : IState
    {
        protected internal StateActionSO OriginSO { get; internal set; }

        protected StateAction()
        {
        }

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
using VFEngine.Tools.StateMachineSO.ScriptableObjects;

namespace VFEngine.Tools.StateMachineSO
{
    public abstract class StateAction : IState
    {
        protected internal StateActionSO OriginSO { get; internal set; }

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
}
using VFEngine.Tools.StateMachine.State.ScriptableObjects;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using StateTransitionController = VFEngine.Tools.StateMachine.Transition.Controller;

namespace VFEngine.Tools.StateMachine.State
{
    internal class Controller : IStateController
    {
        private Model state;
        private bool HasStateController => state.HasStateController;
        private Controller StateController => state.StateController;
        internal string Name => state.Name;

        internal Controller(StateSO originStateSO, StateMachineController stateMachineController, StateTransitionController[] stateTransitionControllers)
        {
            state = new Model(this, originStateSO, stateMachineController, stateTransitionControllers);
        }

        internal Controller(StateSO originStateSO, StateTransitionController[] transitions)
        {
            state = new Model(this, originStateSO, transitions);
        }

        private bool CanInitializeStateModel => state == null;

        internal bool GetNextStateController(out Controller stateController)
        {
            Initialize();
            state.TryGetTransition();
            stateController = StateController;
            return HasStateController;
        }

        private void Initialize()
        {
            if (CanInitializeStateModel) state = new Model();
        }

        public void OnEnter()
        {
            state.OnEnter();
        }

        public void OnExit()
        {
            state.OnExit();
        }
    }
}
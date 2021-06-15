using VFEngine.Tools.StateMachine.State.ScriptableObjects;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using TransitionController = VFEngine.Tools.StateMachine.Transition.Controller;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateTransitionController = VFEngine.Tools.StateMachine.Transition.Controller;
namespace VFEngine.Tools.StateMachine.State.Data
{
    using static State;

    internal class Data
    {
        internal bool HasStateController { get; set; }
        internal StateController StateController { get; set; }
        internal int CurrentStateTransitionControllerIndex { get; set; }
        internal StateSO OriginStateSO { get; private set; }
        internal State State { get; set; }
        private StateMachineController StateMachineController { get; set; }
        internal StateTransitionController[] StateTransitionControllers { get; private set; }

        internal Data(StateController stateController, StateSO originStateSO, StateMachineController stateMachineController, StateTransitionController[] stateTransitionControllers)
        {
            Initialize();
            Initialize(stateController, originStateSO, stateTransitionControllers);
            StateMachineController = stateMachineController;
        }

        internal Data(StateController stateController, StateSO originStateSO, StateTransitionController[] stateTransitionControllers)
        {
            Initialize();
            Initialize(stateController, originStateSO, stateTransitionControllers);
        }

        internal Data()
        {
            Initialize();
        }

        private void Initialize()
        {
            StateController = null;
            CurrentStateTransitionControllerIndex = 0;
            OriginStateSO = null;
            State = Initialized;
            StateMachineController = null;
            StateTransitionControllers = null;
        }

        private void Initialize(StateController stateController, StateSO originStateSO,
            StateTransitionController[] stateTransitionControllers)
        {
            OriginStateSO = originStateSO;
            StateTransitionControllers = stateTransitionControllers;
        }
    }
}
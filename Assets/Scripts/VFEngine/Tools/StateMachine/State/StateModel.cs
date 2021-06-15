using VFEngine.Tools.StateMachine.State.ScriptableObjects;
using StateData = VFEngine.Tools.StateMachine.State.Data.Data;
using DataState = VFEngine.Tools.StateMachine.State.Data.State;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateTransitionController = VFEngine.Tools.StateMachine.Transition.Controller;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;

namespace VFEngine.Tools.StateMachine.State
{
    using static DataState;

    internal class Model
    {
        private readonly StateData data;

        private int CurrentStateTransitionControllerIndex
        {
            get => data.CurrentStateTransitionControllerIndex;
            set => data.CurrentStateTransitionControllerIndex = value;
        }

        private DataState State
        {
            get => data.State;
            set => data.State = value;
        }

        internal StateController StateController
        {
            get => data.StateController;
            private set => data.StateController = value;
        }

        private int TransitionControllersAmount => StateTransitionControllers.Length;

        private bool ProcessingTransitionControllers =>
            CurrentStateTransitionControllerIndex < TransitionControllersAmount;

        private StateTransitionController CurrentStateTransitionController =>
            StateTransitionControllers[CurrentStateTransitionControllerIndex];

        private StateTransitionController[] StateTransitionControllers => data.StateTransitionControllers;

        internal bool HasStateController => StateController != null;

        internal string Name => data.OriginStateSO.name;

        private void ProcessTransitionControllers(DataState state)
        {
            State = state;
            for (CurrentStateTransitionControllerIndex = 0;
                ProcessingTransitionControllers;
                CurrentStateTransitionControllerIndex++)
            {
                if (EnteredCurrentStateTransitionController) continue;
                if (ExitedCurrentStateTransitionController) continue;
                if (ClearedCurrentStateTransitionControllerConditionsCache) continue;
                if (ReceivedCurrentStateTransitionController) break;
            }
        }

        private bool EnteringState => State == Enter;
        private bool EnteredCurrentStateTransitionController => EnteringState && EnteredState();

        private bool EnteredState()
        {
            (CurrentStateTransitionController as IStateController).OnEnter();
            return true;
        }

        private bool ExitingState => State == Exit;
        private bool ExitedCurrentStateTransitionController => ExitingState && ExitedState();

        private bool ExitedState()
        {
            (CurrentStateTransitionController as IStateController).OnExit();
            return true;
        }

        private bool ClearingConditionsCache => State == ClearConditionsCache;

        private bool ClearedCurrentStateTransitionControllerConditionsCache =>
            ClearingConditionsCache && ClearedConditionsCache();

        private bool ClearedConditionsCache()
        {
            CurrentStateTransitionController.ClearConditionsCache();
            return true;
        }

        private bool TriedAndReceivedCurrentStateTransitionController =>
            CurrentStateTransitionController.TryGetTransition(out var stateController)
                ? StateData(stateController, true)
                : StateData(null, false);

        private bool GettingStateTransitionController => State == GetStateTransitionController;

        private bool ReceivedCurrentStateTransitionController =>
            GettingStateTransitionController && TriedAndReceivedCurrentStateTransitionController;

        private bool StateData(StateController stateController, bool hasStateController)
        {
            StateController = stateController;
            return hasStateController;
        }

        internal Model()
        {
            data = new StateData();
        }

        internal Model(StateController stateController, StateSO originStateSO,
            StateTransitionController[] stateTransitionControllers)
        {
            data = new StateData(stateController, originStateSO, stateTransitionControllers);
        }

        internal Model(StateController stateController, StateSO originStateSO,
            StateMachineController stateMachineController, StateTransitionController[] stateTransitionControllers)
        {
            data = new StateData(stateController, originStateSO, stateMachineController, stateTransitionControllers);
        }

        internal void OnEnter()
        {
            ProcessTransitionControllers(Enter);
        }

        internal void OnExit()
        {
            ProcessTransitionControllers(Exit);
        }

        internal void TryGetTransition()
        {
            ProcessTransitionControllers(GetStateTransitionController);
        }
    }
}
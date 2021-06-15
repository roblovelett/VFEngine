using StateTransitionData = VFEngine.Tools.StateMachine.Transition.Data.Data;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateConditionController = VFEngine.Tools.StateMachine.Condition.Controller;

namespace VFEngine.Tools.StateMachine.Transition
{
    internal class Model
    {
        private StateTransitionData data;
        private bool CanInitializeStateTransitionData => data == null;

        internal Model()
        {
            if (CanInitializeStateTransitionData) data = new StateTransitionData();
        }

        internal Model(StateController targetStateController, StateConditionController[] stateConditionControllers,
            int[] resultGroups = null)
        {
            if (CanInitializeStateTransitionData)
                data = new StateTransitionData(targetStateController, stateConditionControllers, resultGroups);

            Initialize(targetStateController, stateConditionControllers, resultGroups);
        }

        private StateController TargetStateController
        {
            get => data.TargetStateController;
            set => data.TargetStateController = value;
        }

        private StateConditionController[] StateConditionControllers
        {
            get => data.StateConditionControllers;
            set => data.StateConditionControllers = value;
        }

        private int[] ResultGroups
        {
            get => data.ResultGroups;
            set => data.ResultGroups = value;
        }

        internal void Initialize()
        {
            if (CanInitializeStateTransitionData)
                InitializeDefault();
            else
                data.Initialize();
        }

        private void InitializeDefault()
        {
            data = new StateTransitionData();
        }

        internal void Initialize(StateController targetStateController,
            StateConditionController[] stateConditionControllers,
            int[] resultGroups = null)
        {
            TargetStateController = targetStateController;
            StateConditionControllers = stateConditionControllers;
            ResultGroups = resultGroups;
        }

        internal void OnEnter()
        {
        }

        internal void OnExit()
        {
        }
    }
}
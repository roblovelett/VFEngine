using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateConditionController = VFEngine.Tools.StateMachine.Condition.Controller;

namespace VFEngine.Tools.StateMachine.Transition.Data
{
    internal class Data
    {
        internal StateController TargetStateController { get; set; }
        internal StateConditionController[] StateConditionControllers { get; set; }
        internal int[] ResultGroups { get; set; }

        internal Data()
        {
            Initialize();
        }

        internal Data(StateController targetStateController, StateConditionController[] stateConditionControllers,
            int[] resultGroups = null)
        {
            Initialize();
            Initialize(targetStateController, stateConditionControllers, resultGroups);
        }

        internal void Initialize()
        {
            TargetStateController = null;
            StateConditionControllers = null;
            ResultGroups = null;
        }

        private void Initialize(StateController targetStateController,
            StateConditionController[] stateConditionControllers, int[] resultGroups = null)
        {
            TargetStateController = targetStateController;
            StateConditionControllers = stateConditionControllers;
            ResultGroups = resultGroups;
        }
    }
}
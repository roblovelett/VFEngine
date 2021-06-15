using StateConditionController = VFEngine.Tools.StateMachine.Condition.Controller;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using StateConditionSO = VFEngine.Tools.StateMachine.Condition.ScriptableObjects.ConditionSO;

namespace VFEngine.Tools.StateMachine.Condition.Data
{
    internal class Data
    {
        internal bool IsCached { get; set; }
        internal bool CachedStatement { get; set; }
        internal StateConditionSO StateConditionSO { get; set; }
        private bool ExpectedResult { get; set; }
        internal StateMachineController StateMachineController { get; set; }

        internal Data(StateMachineController stateMachineController)
        {
            InitializeDefault();
            InitializeStateMachineController(stateMachineController);
        }

        internal Data(StateConditionSO stateConditionSO)
        {
            InitializeDefault();
            InitializeStateConditionSO(stateConditionSO);
        }

        internal Data(StateMachineController stateMachineController, StateConditionSO stateConditionSO,
            bool expectedResult)
        {
            InitializeDefault();
            Initialize(stateMachineController, stateConditionSO, expectedResult);
        }

        private void InitializeDefault()
        {
            IsCached = false;
            ExpectedResult = false;
            CachedStatement = default;
            StateConditionSO = null;
            StateMachineController = null;
        }

        private void Initialize(StateMachineController stateMachineController, StateConditionSO stateConditionSO,
            bool expectedResult)
        {
            InitializeStateMachineController(stateMachineController);
            InitializeStateConditionSO(stateConditionSO);
            InitializeExpectedResult(expectedResult);
        }

        private void InitializeStateMachineController(StateMachineController stateMachineController)
        {
            StateMachineController = stateMachineController;
        }

        private void InitializeStateConditionSO(StateConditionSO stateConditionSO)
        {
            StateConditionSO = stateConditionSO;
        }

        private void InitializeExpectedResult(bool expectedResult)
        {
            ExpectedResult = expectedResult;
        }
    }
}
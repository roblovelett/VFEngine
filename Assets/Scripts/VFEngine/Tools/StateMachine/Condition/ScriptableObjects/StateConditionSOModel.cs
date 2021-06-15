using System.Collections.Generic;
using UnityEngine;
using StateConditionSO = VFEngine.Tools.StateMachine.Condition.ScriptableObjects.ConditionSO;
using StateConditionData = VFEngine.Tools.StateMachine.Condition.ScriptableObjects.Data.Data;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using StateConditionController = VFEngine.Tools.StateMachine.Condition.Controller;

namespace VFEngine.Tools.StateMachine.Condition.ScriptableObjects
{
    internal class ConditionSOModel
    {
        private readonly StateConditionData data;

        private bool ExpectedResult
        {
            get => data.ExpectedResult;
            set => data.ExpectedResult = value;
        }

        private StateConditionSO StateConditionSO
        {
            get => data.StateConditionSO;
            set => data.StateConditionSO = value;
        }

        private StateMachineController StateMachineController
        {
            get => data.StateMachineController;
            set => data.StateMachineController = value;
        }

        private StateConditionController CurrentStateConditionController
        {
            get => data.CurrentStateConditionController;
            set => data.CurrentStateConditionController = value;
        }

        private Dictionary<ScriptableObject, StateConditionController> StateConditionControllers
        {
            get => data.StateConditionControllers;
            set => data.StateConditionControllers = value;
        }

        internal StateConditionController CreatedStateConditionController
        {
            get => data.CreatedStateConditionController;
            private set => data.CreatedStateConditionController = value;
        }

        internal bool CanInitializeStateConditionController => CurrentStateConditionController == null;
        

        private void InitializeStateConditionController(StateConditionSO stateConditionSO,
            StateMachineController stateMachineController, bool expectedResult,
            Dictionary<ScriptableObject, StateConditionController> stateConditionControllers)
        {
            StateConditionSO = stateConditionSO;
            StateMachineController = stateMachineController;
            ExpectedResult = expectedResult;
            StateConditionControllers = stateConditionControllers;
        }

        private void GetStateConditionSO()
        {
            CurrentStateConditionController = null;
            if (StateConditionControllers.TryGetValue(StateConditionSO, out var stateConditionController))
                InitializeDefaultStateConditionController(stateConditionController);
        }

        private void InitializeDefaultStateConditionController(StateConditionController stateConditionController)
        {
            CurrentStateConditionController = stateConditionController;
        }

        internal void StateConditionController()
        {
            CreatedStateConditionController = new Controller(StateMachineController, StateConditionSO, ExpectedResult);
        }

        internal void StateConditionController(StateConditionController stateConditionController)
        {
            InitializeDefaultStateConditionController(stateConditionController);
            CurrentStateConditionController.Initialize(StateConditionSO);
            StateConditionControllers.Add(StateConditionSO, CurrentStateConditionController);
            CurrentStateConditionController.Awake(StateMachineController);
        }

        internal void StateConditionController(StateConditionSO stateConditionSO,
            StateMachineController stateMachineController, bool expectedResult,
            Dictionary<ScriptableObject, StateConditionController> stateConditionControllers)
        {
            InitializeStateConditionController(stateConditionSO, stateMachineController, expectedResult,
                stateConditionControllers);
            GetStateConditionSO();
        }

        internal ConditionSOModel(StateConditionSO stateConditionSO, StateMachineController stateMachineController,
            bool expectedResult, Dictionary<ScriptableObject, StateConditionController> stateConditionControllers)
        {
            data = new StateConditionData(stateConditionSO, stateMachineController, expectedResult,
                stateConditionControllers);
        }
    }
}
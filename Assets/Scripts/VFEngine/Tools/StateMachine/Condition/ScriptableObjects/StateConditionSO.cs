using System.Collections.Generic;
using UnityEngine;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using StateConditionController = VFEngine.Tools.StateMachine.Condition.Controller;

namespace VFEngine.Tools.StateMachine.Condition.ScriptableObjects
{
    internal abstract class ConditionSO : ScriptableObject
    {
        private ConditionSOModel condition;
        private bool InitializeStateConditionSOModel => condition == null;
        private bool CanInitializeStateConditionController => condition.CanInitializeStateConditionController;
        private StateConditionController CreatedStateConditionController => condition.CreatedStateConditionController;

        private void CreateStateConditionController(StateMachineController stateMachineController, bool expectedResult,
            Dictionary<ScriptableObject, Controller> stateConditionControllers)
        {
            InitializeCondition(stateMachineController, expectedResult, stateConditionControllers);
            condition.StateConditionController(this, stateMachineController, expectedResult, stateConditionControllers);
            InitializeStateConditionController();
            condition.StateConditionController();
        }

        private void InitializeCondition(StateMachineController stateMachineController, bool expectedResult,
            Dictionary<ScriptableObject, StateConditionController> stateConditionControllers)
        {
            if (!InitializeStateConditionSOModel) return;
            condition = new ConditionSOModel(this, stateMachineController, expectedResult, stateConditionControllers);
        }

        private void InitializeStateConditionController()
        {
            if (!CanInitializeStateConditionController) return;
            condition.StateConditionController(StateConditionController());
        }

        internal StateConditionController GetStateConditionController(StateMachineController stateMachineController,
            bool expectedResult, Dictionary<ScriptableObject, StateConditionController> stateConditionControllers)
        {
            CreateStateConditionController(stateMachineController, expectedResult, stateConditionControllers);
            return CreatedStateConditionController;
        }

        protected abstract StateConditionController StateConditionController();
    }

    internal abstract class ConditionSO<T> : ConditionSO where T : StateConditionController, new()
    {
        protected override StateConditionController StateConditionController()
        {
            return new T();
        }
    }
}
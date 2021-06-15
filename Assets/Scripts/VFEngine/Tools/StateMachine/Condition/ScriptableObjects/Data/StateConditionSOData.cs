using System.Collections.Generic;
using UnityEngine;
using StateConditionSO = VFEngine.Tools.StateMachine.Condition.ScriptableObjects.ConditionSO;
using StateConditionData = VFEngine.Tools.StateMachine.Condition.ScriptableObjects.Data.Data;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using StateConditionController = VFEngine.Tools.StateMachine.Condition.Controller;

namespace VFEngine.Tools.StateMachine.Condition.ScriptableObjects.Data
{
    internal class Data
    {
        internal bool ExpectedResult { get; set; }
        internal StateConditionSO StateConditionSO { get; set; }
        internal StateConditionController CurrentStateConditionController { get; set; }
        internal StateConditionController CreatedStateConditionController { get; set; }
        internal StateMachineController StateMachineController { get; set; }
        internal Dictionary<ScriptableObject, StateConditionController> StateConditionControllers { get; set; }

        internal Data(StateConditionSO stateConditionSO, StateMachineController stateMachineController,
            bool expectedResult, Dictionary<ScriptableObject, StateConditionController> stateConditionControllers)
        {
            ExpectedResult = expectedResult;
            StateConditionSO = stateConditionSO;
            StateMachineController = stateMachineController;
            StateConditionControllers = stateConditionControllers;
            CurrentStateConditionController = null;
        }
    }
}
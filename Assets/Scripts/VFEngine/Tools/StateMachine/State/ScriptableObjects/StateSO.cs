using System.Collections.Generic;
using UnityEngine;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
namespace VFEngine.Tools.StateMachine.State.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New State", menuName = "State Machine/State")]
    public class StateSO : ScriptableObject
    {
        private Model stateSO;

        private bool CanInitializeStateSOModel => stateSO == null;

        //private bool HasStateController => stateSO.HasStateController;
        //private StateController StateController => stateSO.StateController;

        private StateController StateController => stateSO.StateController;
        
        internal StateController GetStateController(StateMachineController stateMachineController, Dictionary<ScriptableObject, object> stateSOControllers)
        {
            InitializeStateSOModel();
            stateSO.GetStateController(this,stateMachineController,stateSOControllers);//Initialize(this, stateMachineController, stateSOControllers);
            return StateController;
        }

        private void InitializeStateSOModel()
        {
            if (CanInitializeStateSOModel) stateSO = new Model();
        }
        /*
        private StateSOData data;
        private MachineState State => data.State;
        private bool HasState => data.HasState;

        internal MachineState Get(Controller stateMachineController,
            Dictionary<ModelSO, object> transitionsByFromStates)
        {
            return ExistingState(this, transitionsByFromStates, out var existingState)
                ? existingState
                : NewAddedState(this, stateMachineController, transitionsByFromStates);
        }

        private bool ExistingState(ModelSO stateSo, IReadOnlyDictionary<ModelSO, object> transitionsByFromStates,
            out MachineState existingState)
        {
            Initialize(stateSo, transitionsByFromStates);
            existingState = State;
            return HasState;
        }

        private MachineState NewAddedState(ModelSO stateSo, Controller stateMachineController,
            IDictionary<ModelSO, object> transitionsByFromStates)
        {
            data.Initialize();
            transitionsByFromStates.Add(stateSo, State);
            data.Initialize(stateMachineController);
            return State;
        }

        private void Initialize(ModelSO stateSO, IReadOnlyDictionary<ModelSO, object> transitionsByFromStates)
        {
            data = new StateSOData(stateSO, transitionsByFromStates);
        }*/
    }
}
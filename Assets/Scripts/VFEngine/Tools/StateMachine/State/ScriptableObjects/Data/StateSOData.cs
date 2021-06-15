using System.Collections.Generic;
using UnityEngine;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;

namespace VFEngine.Tools.StateMachine.State.ScriptableObjects.Data
{
    public class Data
    {
        internal bool HasStateController { get; set; }
        internal StateSO StateSO { get; set; }
        internal StateController StateController { get; set; }
        internal StateMachineController StateMachineController { get; set; }
        internal Dictionary<ScriptableObject, object> StateSOControllers { get; set; }

        public Data()
        {
            InitializeDefault();
        }

        private void InitializeDefault()
        {
            HasStateController = false;
            StateSO = null;
            StateController = null;
            StateMachineController = null;
            StateSOControllers = null;
        }

        private void Initialize(StateSO stateSO, StateMachineController stateMachineController,
            Dictionary<ScriptableObject, object> stateSOControllers)
        {
            StateSO = stateSO;
            StateMachineController = stateMachineController;
            StateSOControllers = stateSOControllers;
        }
        /*
        public MachineState State { get; private set; }
        public bool HasState { get; private set; }
        private ModelSO stateSO;
        [CanBeNull] private object stateObject;

        public Data(ModelSO state, IReadOnlyDictionary<ModelSO, object> transitionsByFromStates) : this()
        {
            InitializeDefault();
            Initialize(state, transitionsByFromStates);
            if (HasState) State = stateObject as MachineState;
        }

        private void InitializeDefault()
        {
            stateObject = null;
            State = null;
            HasState = false;
        }

        private void Initialize(ModelSO state, IReadOnlyDictionary<ModelSO, object> transitionsByFromStates)
        {
            stateSO = state;
            HasState = transitionsByFromStates.TryGetValue(stateSO, out stateObject);
        }

        public void Initialize(Controller stateMachineController)
        {
            State = new MachineState(stateSO, stateMachineController, new StateTransition[0]);
        }

        public void Initialize()
        {
            State = new MachineState();
        }
        */
    }
}
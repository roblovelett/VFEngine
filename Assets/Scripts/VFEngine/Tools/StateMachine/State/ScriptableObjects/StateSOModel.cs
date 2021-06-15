using System.Collections.Generic;
using UnityEngine;
using StateSOData = VFEngine.Tools.StateMachine.State.ScriptableObjects.Data.Data;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using StateTransitionController = VFEngine.Tools.StateMachine.Transition.Controller;

namespace VFEngine.Tools.StateMachine.State.ScriptableObjects
{
    public class Model
    {
        private StateSOData data;

        internal StateController StateController
        {
            get => data.StateController;
            private set => data.StateController = value;
        }

        private bool CanInitializeStateSOData => data == null;

        private StateSO StateSO
        {
            get => data.StateSO;
            set => data.StateSO = value;
        }

        private StateMachineController StateMachineController
        {
            get => data.StateMachineController;
            set => data.StateMachineController = value;
        }

        private Dictionary<ScriptableObject, object> StateSOControllers
        {
            get => data.StateSOControllers;
            set => data.StateSOControllers = value;
        }
        
        

        private void Initialize()
        {
            data = new StateSOData();
        }

        internal void GetStateController(StateSO stateSO, StateMachineController stateMachineController, Dictionary<ScriptableObject, object> stateSOControllers)
        {
            if (CanInitializeStateSOData)
            {
                Initialize();
            }
            
            StateSOData(stateSO, stateMachineController,stateSOControllers);

            if (HasStateController()) return;

            StateSOData();//new StateController());

            //StateController
            //if ()//HasStateController())
            //{
            //    return;
            //}

            /*
            return (StateSOControllers.TryGetValue(StateSO, out var stateController)
                ? Foo(stateController, stateController == null)
                : false;

            */


            //if (!StateSOControllers.TryGetValue(StateSO, out var stateController)) return;
            //StateController = stateController as StateController;
            //HasStateController = true;
        }

        private bool HasStateController()
        {
            return StateSOControllers.TryGetValue(StateSO, out var stateController)
                ? StateSOData(stateController as StateController, true)
                : StateSOData(null, false);
        }

        private bool StateSOData(StateController stateController, bool hasStateController)
        {
            StateController = stateController;
            return hasStateController;
        }

        private void StateSOData()//StateController stateController)
        {
            StateController = new StateController(StateSO, StateMachineController, new StateTransitionController[0]);
            StateSOControllers.Add(StateSO, StateController);
            
            //StateControllers.Add()
        }
        
        private void StateSOData(StateSO stateSO, StateMachineController stateMachineController, Dictionary<ScriptableObject, object> stateSOControllers)
        {
            StateSO = stateSO;
            StateMachineController = stateMachineController;
            StateSOControllers = stateSOControllers;
        }

        internal Model()
        {
            Initialize();
        }

        /*private bool Foo(object stateController, bool hasStateController)
        {
            StateController = stateController as StateController;
            HasStateController = hasStateController;
            return HasStateController;
        }*/
    }
}
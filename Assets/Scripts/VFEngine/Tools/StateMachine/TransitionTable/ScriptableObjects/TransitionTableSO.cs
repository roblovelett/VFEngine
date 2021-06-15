using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VFEngine.Tools.StateMachine.Condition;
using VFEngine.Tools.StateMachine.State.ScriptableObjects;
using VFEngine.Tools.StateMachine.Transition;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Transition Table", menuName = "State Machine/Transition Table")]
    public class TransitionTableSO : ScriptableObject
    {
        [SerializeField] private Transition[] transitions;
        private TransitionTableSOData data;
        private StateMachine.Transition.Controller[] stateTransitions;
        //private MachineState FirstState => States[0];
        //private bool HasStates => States.Count > 0;

        internal StateController GetInitialState(StateMachineController stateMachineController)
        {
            return null;
            //return new StateController();
        }
        /*internal State.Controller GetInitialState(Controller stateMachineManager)
        {
            Initialize(stateMachineManager);
            ProcessFromStateTransitions();
            if (HasStates) return FirstState;
            throw new InvalidOperationException($"TransitionTable {name} is empty.");
        }*/

        /*private List<MachineState> States
        {
            get => data.States;
            set => data.States = value;
        }*/

        /*private List<StateMachine.Transition.Controller> FromTransitions
        {
            get => data.FromTransitions;
            set => data.FromTransitions = value;
        }*/

        /*private Dictionary<ModelSO, object> TransitionsByFromStates
        {
            get => data.TransitionsByFromStates;
            set => data.TransitionsByFromStates = value;
        }*/

        /*private IEnumerable<IGrouping<ModelSO, TransitionTableSOData>> FromStateTransitions
        {
            get => data.FromStateTransitions;
            set => data.FromStateTransitions = value;
        }*/

        private Controller StateMachineController
        {
            get => data.StateMachineController;
            set => data.StateMachineController = value;
        }

        /*private void Initialize(Controller stateMachineController)
        {
            data = new TransitionTableSOData();
            StateMachineController = stateMachineController;
            States = new List<MachineState>();
            FromTransitions = new List<StateMachine.Transition.Controller>();
            TransitionsByFromStates = new Dictionary<ModelSO, object>();
            FromStateTransitions =
                (IEnumerable<IGrouping<ModelSO, TransitionTableSOData>>) transitions.GroupBy(t => t.FromState);
        }*/
/*
        private MachineState State
        {
            get => data.State;
            set => data.State = value;
        }
*/
        /*private IGrouping<ModelSO, TransitionTableSOData> FromStateTransition
        {
            get => data.FromStateTransition;
            set => data.FromStateTransition = value;
        }*/

        /*private void ProcessFromStateTransitions()
        {
            foreach (var fromStateTransition in FromStateTransitions)
            {
                FromStateTransition = fromStateTransition;
                if (FromStateTransition.Key == null) NoFromStateTransitionError(nameof(FromStateTransition.Key));
                State = FromStateTransition.Key.Get(StateMachineController, TransitionsByFromStates);
                States.Add(State);
                FromTransitions.Clear();
                ProcessFromStateTransition();
            }

            //FromTransitions.ToArray();
        }*/

        private void NoFromStateTransitionError(string fromState)
        {
            throw new ArgumentNullException(fromState, $"TransitionTable: {name}");
        }
/*
        private MachineState ToState
        {
            get => data.ToState;
            set => data.ToState = value;
        }
*/
        private int[] AddedConditions
        {
            get => data.AddedConditions;
            set => data.AddedConditions = value;
        }

        /*private Core.ConditionData[] TransitionCondition
        {
            //get => data.TransitionCondition;
            set => data.TransitionCondition = null;//value;
        }*/

        private List<int> AddedConditionsList
        {
            get => data.AddedConditionsList;
            set => data.AddedConditionsList = value;
        }

        /*private void ProcessFromStateTransition()
        {
            foreach (var transition in FromStateTransition)
            {
                if (transition.ToState == null)
                    NoToStateTransitionError(nameof(transition.ToState), FromStateTransition.Key.name);
                //ToState = transition.ToState.Get(StateMachineController, TransitionsByFromStates);
                //TransitionConditions = transition.Conditions;
                ProcessTransitionCondition();
                AddedConditionsList = new List<int>();
                AddConditions();
                AddedConditions = AddedConditionsList.ToArray();
                //FromTransitions.Add(new StateTransition(ToState, TransitionCondition, AddedConditions));
            }
        }*/

        private void NoToStateTransitionError(string toStateName, string fromStateName)
        {
            throw new ArgumentNullException(toStateName, $"TransitionTable: {name}, FromState: {fromStateName}");
        }

        /*private ConditionData[] TransitionConditions
        {
            get => data.TransitionConditions;
            set => data.TransitionConditions = value;
        }*/

        private int Condition
        {
            get => data.Condition;
            set => data.Condition = value;
        }

        /*private int TransitionConditionsAmount => TransitionConditions.Length;
        private bool SettingCondition => Condition < TransitionConditionsAmount;
        private bool IsTrue => TransitionConditions[Condition].Result == true;
        */
        private void ProcessTransitionCondition()
        {
            //TransitionCondition = new Core.ConditionData[TransitionConditionsAmount];
            //for (Condition = 0; SettingCondition; Condition++)
            {
                /*
                TransitionCondition[Condition] = TransitionConditions[Condition].Condition
                    .Get(StateMachine, IsTrue, TransitionsByFromStates);
                */
            }

        }

        //private bool HasAnd => TransitionConditions[Condition].Operator == And;
        //private int LastTransitionConditionIndex => TransitionConditionsAmount - 1;

        //private bool NotOnLastTransitionConditionIndex => Condition < LastTransitionConditionIndex;

        //private bool AddingConditions => NotOnLastTransitionConditionIndex && HasAnd;
        private int ConditionsAmount => AddedConditionsList.Count;

        private void AddConditions()
        {
            //for (Condition = 0; SettingCondition; Condition++)
            {
                AddedConditionsList.Add(1);
                /*while (AddingConditions)
                {
                    Condition++;
                    AddedConditionsList[ConditionsAmount]++;
                }
            }
        }*/
            }
        }
    }
}

//[Serializable]
        /*public struct TransitionData
        {
            public StateSO FromState { get; internal set; }
            public StateSO ToState { get; internal set; }
            public ConditionData[] Conditions { get; internal set; }
        }*/

        //[Serializable]
        /*public struct ConditionData
        {
            public Result Result { get; internal set; }
            public StateConditionSO Condition { get; internal set; }
            public Operator Operator { get; internal set; }
        }*/

        /*public enum Operator
        {
            And,
            Or
        }*/

        /*public enum Result
        {
            True,
            False
        }*/

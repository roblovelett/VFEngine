using System.Collections.Generic;
using System.Linq;


namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects
{
    public class TransitionTableSOData
    {
        public int Condition { get; internal set; }
        public int[] AddedConditions { get; internal set; }
        public List<int> AddedConditionsList { get; internal set; }
        //public List<MachineState> States { get; internal set; }
        //public List<StateMachine.Transition.Controller> FromTransitions { get; internal set; }
        //public MachineState State { get; internal set; }
        //public MachineState ToState { get; internal set; }
        public Controller StateMachineController { get; internal set; }
        //public ConditionData[] TransitionConditions { get; internal set; }
        //public IGrouping<ModelSO, TransitionTableSOData> FromStateTransition { get; internal set; }
        //public IEnumerable<IGrouping<ModelSO, TransitionTableSOData>> FromStateTransitions { get; internal set; }
        //public Dictionary<ModelSO, object> TransitionsByFromStates { get; internal set; }
    }
}
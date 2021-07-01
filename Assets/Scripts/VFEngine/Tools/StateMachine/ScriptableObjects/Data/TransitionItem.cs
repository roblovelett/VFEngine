using System;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Data
{
    [Serializable]
    public class TransitionItem
    {
        public StateSO FromState;
        public StateSO ToState;
        public ConditionUsage[] Conditions;
    }
}
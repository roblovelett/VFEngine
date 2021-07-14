using System;

namespace VFEngine.Tools.StateMachine
{
    [Serializable]
    internal class StateMachineDebug
    {
        private StateMachine stateMachine;
        
        internal void Awake(StateMachine stateMachineInternal)
        {
            stateMachine = stateMachineInternal;
        }
    }
}
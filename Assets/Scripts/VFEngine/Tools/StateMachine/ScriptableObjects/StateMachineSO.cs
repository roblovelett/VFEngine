using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    public class StateMachineSO : ScriptableObject
    {
        private StateMachine stateMachine;
        internal void GetInitialState(StateMachine stateMachineInternal)
        {
            stateMachine = stateMachineInternal;
        }
    }
}
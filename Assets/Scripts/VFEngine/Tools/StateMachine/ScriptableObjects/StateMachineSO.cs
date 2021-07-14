using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    public class StateMachineSO : ScriptableObject
    {
        internal State GetInitialState(StateMachine stateMachineInternal)
        {
            return new State();
        }
    }
}
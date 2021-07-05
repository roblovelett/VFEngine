using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    public class StateMachineSO : ScriptableObject
    {
        
        private StateMachine currentStateMachine;
        public void Initialize(StateMachine stateMachine)
        {
            currentStateMachine = stateMachine;
        }
    }
}
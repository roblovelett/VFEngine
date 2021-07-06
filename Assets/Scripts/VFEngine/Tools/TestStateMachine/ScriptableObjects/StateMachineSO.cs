using UnityEngine;

namespace VFEngine.Tools.TestStateMachine.ScriptableObjects
{
    public class StateMachineSO : ScriptableObject
    {
        [SerializeField] private Component[] componentDependencies;
        private Tools.StateMachine.StateMachine currentStateMachine;
        public void Initialize(Tools.StateMachine.StateMachine stateMachine)
        {
            currentStateMachine = stateMachine;
        }
    }
}
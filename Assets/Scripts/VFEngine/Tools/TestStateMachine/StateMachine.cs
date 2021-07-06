using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
//using Cysharp.Threading.Tasks;
using StateMachineObject = VFEngine.Tools.TestStateMachine.ScriptableObjects.StateMachineSO;
namespace VFEngine.Tools.TestStateMachine
{
    using static ScriptableObject;
    
    public class StateMachine : MonoBehaviour
    {
        [Tooltip("State Machine SO")] [SerializeField]
        private StateMachineObject stateMachine;
        //private Dictionary<Type, Component> cachedComponents;
        //private CancellationToken ct;
        //private CancellationTokenSource cts;
        //private State currentState;

        private void Awake()
        {
            if (stateMachine == null)
            {
                stateMachine = CreateInstance<StateMachineObject>();
            }
            
            // initialize
            
            // get needed components for state machine cache

            
            
            
            //currentState = stateMachine.GetInitialState(this);
            //cachedComponents = new Dictionary<Type, Component>();
        }
        
        private new bool TryGetComponent<T>(out T component) where T : Component
        {
            var type = typeof(T);
            /*if (!cachedComponents.TryGetValue(type, out var cachedComponent))
            {
                if (base.TryGetComponent(out component)) cachedComponents.Add(type, component);
                return component != null;
            }*/

            //component = (T) cachedComponent;
            component = null;
            return true;
        }
    }
}
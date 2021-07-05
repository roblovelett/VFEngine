using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
//using Cysharp.Threading.Tasks;
using StateMachineObject = VFEngine.Tools.StateMachine.ScriptableObjects.StateMachineSO;
namespace VFEngine.Tools.StateMachine
{
    using static AssemblyReloadEvents;
    using static UniTask;
    using static ScriptableObject;
    
    public class StateMachine : MonoBehaviour
    {
        [Tooltip("State Machine SO")] [SerializeField]
        private StateMachineObject stateMachine;
        private Dictionary<Type, Component> cachedComponents;
        private CancellationToken ct;
        private CancellationTokenSource cts;
        private State currentState;

        private void Awake()
        {
            if (stateMachine == null) stateMachine = CreateInstance<StateMachineObject>();
            stateMachine.Initialize(this);
            //currentState = stateMachine.GetInitialState(this);
            //cachedComponents = new Dictionary<Type, Component>();
        }
    }
}
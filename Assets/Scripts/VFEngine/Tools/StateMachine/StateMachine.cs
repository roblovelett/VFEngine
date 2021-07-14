using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects;
using static UnityEngine.ScriptableObject;

namespace VFEngine.Tools.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private StateMachineSettingsSO stateMachineSettings = default(StateMachineSettingsSO);
        [SerializeField] private StateMachineSO stateMachine = default(StateMachineSO);
#if UNITY_EDITOR
        [SerializeField] private StateMachineDebug debug = default(StateMachineDebug);
#endif
        private readonly Dictionary<Type, Component> cachedComponents;
        private State currentState;

        private void OnEnable()
        {
#if UNITY_EDITOR
            Initialize(true, true);
#endif
        }

        private void Awake()
        {
            Initialize(false, false);
        }

        private void Start()
        {
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            Initialize(true, false);
#endif
        }
        
        
        private void Initialize(bool unityEditor, bool onEnabled)
        {
            if (unityEditor)
            {
                if (onEnabled)
                {
                    AssemblyReloadEvents.afterAssemblyReload += InitializeAfterAssemblyReload;
                }
                else
                {
                    AssemblyReloadEvents.afterAssemblyReload -= InitializeAfterAssemblyReload;
                }

                return;
            }
            
            if (stateMachineSettings == null) stateMachineSettings = CreateInstance<StateMachineSettingsSO>();
            if (stateMachine == null) stateMachine = CreateInstance<StateMachineSO>();
            CurrentState();
        }
        
        private void InitializeAfterAssemblyReload()
        {
            CurrentState();
            debug.Awake(this);
        }

        private void CurrentState()
        {
            currentState = stateMachine.GetInitialState(this);
        }
    }
}
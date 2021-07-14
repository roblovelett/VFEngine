using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects;

namespace VFEngine.Tools.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private StateMachineSettingsSO stateMachineSettings = default(StateMachineSettingsSO);
        [SerializeField] private StateMachineSO stateMachine = default(StateMachineSO);
        
        #if UNITY_EDITOR
        [SerializeField] private StateMachineDebugSettingsSO debugSettings = default(StateMachineDebugSettingsSO);
        [SerializeField] private StateMachineDebugSO debug = default(StateMachineDebugSO);
        #endif

        private readonly Dictionary<Type, Component> cachedComponents;
        private State currentState;
        private void OnEnable()
        {
            if (stateMachineSettings == null)
            {
                stateMachineSettings = ScriptableObject.CreateInstance<StateMachineSettingsSO>();
            }

            if (stateMachine == null)
            {
                stateMachine = ScriptableObject.CreateInstance<StateMachineSO>();
            }
            
            if (debugSettings == null)
            {
                debugSettings = ScriptableObject.CreateInstance<StateMachineDebugSettingsSO>();
            }

            if (debug == null)
            {
                debug = ScriptableObject.CreateInstance<StateMachineDebugSO>();
            }

            AssemblyReloadEvents.afterAssemblyReload += AfterAssemblyReload;
        }

        private void AfterAssemblyReload()
        {
            currentState = stateMachine.GetInitialState(this);
            debug.Awale(this);
        }

        private void OnDisable()
        {
            AssemblyReloadEvents.afterAssemblyReload -= ;
        }
    }
}
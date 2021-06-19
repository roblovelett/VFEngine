using System;
using System.Text;
using UnityEngine;
using VFEngine.Tools.StateMachine.Debug.ScriptableObjects;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;

namespace VFEngine.Tools.StateMachine.Debug.Data
{
    using static String;

    [Serializable]
    internal class Data
    {
        [SerializeField] private string stateMachineControllerName;
        internal bool IsUnityEditor { get; set; }
        internal bool DebugStateMachineControl { get; set; }
        internal bool DebugStateTransitionsControl { get; set; }
        internal bool AppendStateTransitionsConditionsInformation { get; set; }
        internal string TargetState { get; set; }
        internal StringBuilder Log { get; set; }
        internal StateMachineController StateMachineController { get; set; }

        internal string StateMachineControllerName
        {
            get => stateMachineControllerName;
            set => stateMachineControllerName = value;
        }

        internal Data(bool isUnityEditor, StateMachineController stateMachineController,
            StateMachineDebugSettingsSO stateMachineDebugSettingsSO)
        {
            Initialize();
            IsUnityEditor = isUnityEditor;
            DebugStateMachineControl = stateMachineDebugSettingsSO.debugStateMachineControl;
            DebugStateTransitionsControl = stateMachineDebugSettingsSO.debugStateTransitionsControl;
            AppendStateTransitionsConditionsInformation =
                stateMachineDebugSettingsSO.appendStateTransitionsConditionsInformation;
            StateMachineController = stateMachineController;
            StateMachineControllerName = stateMachineController.name;
        }

        private void Initialize()
        {
            IsUnityEditor = false;
            DebugStateMachineControl = false;
            DebugStateTransitionsControl = false;
            AppendStateTransitionsConditionsInformation = false;
            TargetState = Empty;
            Log = null;
            StateMachineController = null;
            StateMachineControllerName = Empty;
        }
    }
}
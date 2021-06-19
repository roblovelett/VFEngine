using System;
using System.Text;
using UnityEngine;
using VFEngine.Tools.StateMachine.Debug.ScriptableObjects;

using DebugData = VFEngine.Tools.StateMachine.Debug.Data.Data;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;

namespace VFEngine.Tools.StateMachine.Debug
{
    using static UnityEngine.Debug;
    using static VFEngine.Tools.StateMachine.Debug.ScriptableObjects.Data.Text;
    
    [Serializable]
    public class Model
    {
        [SerializeField] private DebugData data;
        internal bool HasDebugStateMachineControl => data.DebugStateMachineControl;
        private string TargetState
        {
            get => data.TargetState;
            set => data.TargetState = value;
        }

        

        internal Model(bool isUnityEditor, StateMachineController stateMachineController, StateMachineDebugSettingsSO stateMachineDebugSettingsSO)
        {
            data = new DebugData(isUnityEditor, stateMachineController, stateMachineDebugSettingsSO);
        }

        internal void Awake()
        {
            Log = new StringBuilder();
        }

        private bool HasTransitionConditionResult => CanEvaluateTransition || AppendStateTransitionsConditionsInformation;
        private bool CanEvaluateTransition => DebugStateMachineControl || CanBuildLog;
        private bool AppendStateTransitionsConditionsInformation => data.AppendStateTransitionsConditionsInformation;

        private bool DebugStateMachineControl
        {
            get => data.DebugStateMachineControl;
            set => data.DebugStateMachineControl = value;
        }

        private bool CanBuildLog => Log.Length != 0;
        private string IsEqual => EqualsOperator;

        private StringBuilder Log
        {
            get => data.Log;
            set => data.Log = value;
        }
        
        private void PrintDebugLog()
        {
            Log.AppendLine();
            Log.Append(Divider);
            Log(Log.ToString());
        }

        private bool DebugStateTransitionsControl
        {
            get => data.DebugStateTransitionsControl;
            set => data.DebugStateTransitionsControl = value;
        }

        private string StateMachineControllerName => data.StateMachineControllerName;        
        private bool DebugTransitions()
        {
            if (!DebugStateTransitionsControl) return false;
            Log.Clear();
            Log.AppendLine(StateChangedMessage());
            Log.AppendLine(TransitionMessage(TargetState));
            return true;
        }

        private string StateChangedMessage()//string stateMachineControllerName)
        {
            return $"{StateMachineControllerName}{StateChanged}";
        }

        private string TransitionMessage(string targetStateName)
        {
            return $"{StateMachineControllerName}{SharpArrow}{targetStateName}";
        }

        private void AppendConditionInformation()
        {
            if (!AppendStateTransitionsConditionsInformation) return;
            Log.AppendLine();
            Log.AppendLine(TransitionConditions);
        }

        private void AppendConditionInformation(string conditionName, bool result, bool isMet)
        {
            Log.Append(ConditionIsResultMessage(conditionName, result));
            Log.AppendLine(ConditionInformation(isMet));
        }

        private string ConditionIsResultMessage(string conditionName, bool result)
        {
            return $"{ThickArrow}{conditionName}{IsEqual}{result}";
        }

        private string ConditionInformation(bool isMet)
        {
            return isMet ? AppendCheckMark : AppendNoCheckMark;
        }

        internal void TransitionEvaluation(string state)
        {
            TargetState = state;
            if (!DebugTransitions()) return;
            AppendConditionInformation();
        }

        internal void TransitionEvaluation(bool passed)
        {
            if (passed)
            {
                //StateControllerName = TargetState;
            }
            if (!CanEvaluateTransition) return;
            if (passed) PrintDebugLog();
            Log.Clear();
        }

        internal void TransitionConditionResult(string conditionName, bool result, bool isMet)
        {
            if (!HasTransitionConditionResult) return;
            AppendConditionInformation(conditionName, result, isMet);
        }
    }
}
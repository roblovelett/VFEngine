#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace VFEngine.Tools.StateMachineSO
{
    using static Debug;

    [Serializable]
    internal class StateMachineDebugger
    {
        private StateMachine stateMachine;
        private StringBuilder logBuilder;
        private string targetState = string.Empty;
        private const string CheckMark = "\u2714";
        private const string UncheckMark = "\u2718";
        private const string ThickArrow = "\u279C";
        private const string SharpArrow = "\u27A4";

        [SerializeField] [Tooltip("Issues a debug log when a state transition is triggered")]
        internal bool debugTransitions;

        [SerializeField]
        [Tooltip("List all conditions evaluated, the result is read: ConditionName == BooleanResult [PassedTest]")]
        internal bool appendConditionsInfo = true;

        [SerializeField] [Tooltip("List all actions activated by the new State")]
        internal bool appendActionsInfo = true;

        [SerializeField] [Tooltip("The current State name [Readonly]")]
        internal string currentState;

        internal void Awake(StateMachine machine)
        {
            stateMachine = machine;
            logBuilder = new StringBuilder();
            currentState = stateMachine.CurrentState.OriginSO.name;
        }

        internal void TransitionEvaluationBegin(string state)
        {
            targetState = state;
            if (!debugTransitions) return;
            logBuilder.Clear();
            logBuilder.AppendLine($"{stateMachine.name} state changed");
            logBuilder.AppendLine($"{currentState}  {SharpArrow}  {targetState}");
            if (!appendConditionsInfo) return;
            logBuilder.AppendLine();
            logBuilder.AppendLine("Transition Conditions:");
        }

        internal void TransitionConditionResult(string conditionName, bool result, bool isMet)
        {
            if (!debugTransitions || logBuilder.Length == 0 || !appendConditionsInfo) return;
            logBuilder.Append($"    {ThickArrow} {conditionName} == {result}");
            logBuilder.AppendLine(isMet ? $" [{CheckMark}]" : $" [{UncheckMark}]");
        }

        internal void TransitionEvaluationEnd(bool passed, IEnumerable<StateAction> actions)
        {
            if (passed) currentState = targetState;
            if (!debugTransitions || logBuilder.Length == 0) return;
            if (passed)
            {
                #region Log Actions

                if (!appendActionsInfo) return;
                logBuilder.AppendLine();
                logBuilder.AppendLine("State Actions:");
                foreach (var action in actions) logBuilder.AppendLine($"    {ThickArrow} {action.OriginSO.name}");

                #endregion

                #region Print Debug Log

                logBuilder.AppendLine();
                logBuilder.Append("--------------------------------");
                Log(logBuilder.ToString());

                #endregion
            }

            logBuilder.Clear();
        }
    }
}

#endif
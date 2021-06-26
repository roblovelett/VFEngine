using System;
using System.Text;
using UnityEngine;
using VFEngine.Tools.StateMachine.Data;

namespace VFEngine.Tools.StateMachine
{
    using static Debug;
    using static StateMachineText;
    using static String;

    [Serializable]
    internal class StateMachineDebug
    {
        [SerializeField] [Tooltip(DebugTransitions)]
        internal bool debugTransitions;

        [SerializeField] [Tooltip(AppendConditionsInfo)]
        internal bool appendConditionsInfo = true;

        [SerializeField] [Tooltip(CurrentState)]
        internal string currentState;

        private StateMachine stateMachine;
        private StringBuilder logBuilder;
        private string targetState;

        internal void Awake(StateMachine stateMachineInternal)
        {
            stateMachine = stateMachineInternal;
            logBuilder = new StringBuilder();
            targetState = Empty;
            currentState = stateMachine.CurrentState.OriginSO.name;
        }

        internal void TransitionEvaluationBegin(string targetStateInternal)
        {
            targetState = targetStateInternal;
            if (!debugTransitions) return;
            logBuilder.Clear();
            logBuilder.AppendLine(StateMachineChanged(stateMachine.name));
            logBuilder.AppendLine(CurrentToTargetState(currentState, targetState));
            if (!appendConditionsInfo) return;
            logBuilder.AppendLine(Conditions());
        }

        internal void TransitionConditionResult(string conditionName, bool result, bool isMet)
        {
            if (!debugTransitions || logBuilder.Length == 0 || !appendConditionsInfo) return;
            logBuilder.Append(ConditionIs(conditionName, result));
            logBuilder.AppendLine(IsMetCheckMark(isMet));
        }

        internal void TransitionEvaluationEnd(bool passed)
        {
            if (passed) currentState = targetState;
            if (!debugTransitions || logBuilder.Length == 0) return;
            if (passed)
            {
                logBuilder.AppendLine();
                logBuilder.Append(Divider);
                Log(logBuilder.ToString());
            }

            logBuilder.Clear();
        }
    }
}
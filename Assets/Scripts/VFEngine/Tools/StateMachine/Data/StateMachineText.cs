using System;

namespace VFEngine.Tools.StateMachine.Data
{
    using static Environment;

    internal static class StateMachineText
    {
        private const string StateChanged = " state changed";
        private const string TransitionTable = "TransitionTable";
        private const string NotFoundIn = " not found in ";
        private const string FromState = ", From State: ";
        private const string IsEmpty = " is empty.";
        private const string Nbsp = " ";
        private const string CheckMark = " [\u2714]";
        private const string UncheckMark = " [\u2718]";
        private const string ThickArrow = "    \u279C ";
        private const string SharpArrow = "  \u27A4  ";
        private const string TransitionConditions = "Transition Conditions:";
        private const string Is = " == ";
        private static readonly string TransitionTableProperty = $"{TransitionTable}: ";
        internal const string InitialState = "Set the initial state of this StateMachine";
        internal const string TransitionStateError = "Transition state is null.";
        internal const string DebugTransitions = "Issues a debug log when a state transition is triggered";

        internal const string AppendConditionsInfo =
            "List all conditions evaluated, the result is read: ConditionName == BooleanResult [PassedTest]";

        internal const string CurrentState = "The current State name [Readonly]";
        internal const string Divider = "--------------------------------";

        internal static string GetComponentError(string typeName, string name)
        {
            return $"{typeName}{NotFoundIn}{name}.";
        }

        internal static string TransitionTableName(string name)
        {
            return $"{TransitionTableProperty}{name}";
        }

        internal static string TransitionError(string name, string fromStateName)
        {
            return $"{TransitionTableProperty}{name}{FromState}{fromStateName}";
        }

        internal static string StateError(string name)
        {
            return $"{TransitionTable}{Nbsp}{name}{IsEmpty}";
        }

        internal static string StateMachineChanged(string stateMachineName)
        {
            return $"{stateMachineName}{StateChanged}";
        }

        internal static string CurrentToTargetState(string currentState, string targetState)
        {
            return $"{currentState}{SharpArrow}{targetState}";
        }

        internal static string ConditionIs(string conditionName, bool result)
        {
            return $"{ThickArrow}{conditionName}{Is}{result.ToString()}";
        }

        internal static string IsMetCheckMark(bool isMet)
        {
            return isMet ? $"{CheckMark}" : $"{UncheckMark}";
        }

        internal static string Conditions()
        {
            return $"{NewLine}{TransitionConditions}";
        }
    }
}
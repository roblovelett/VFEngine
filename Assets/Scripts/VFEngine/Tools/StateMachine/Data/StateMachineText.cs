namespace VFEngine.Tools.StateMachine.Data
{
    internal static class StateMachineText
    {
        internal const string InitialState = "Set the initial state of this StateMachine";
        internal const string TransitionStateError = "Transition state is null.";
        internal const string DebugTransitions = "Issues a debug log when a state transition is triggered";
         internal const string AppendConditionsInfo = "List all conditions evaluated, the result is read: ConditionName == BooleanResult [PassedTest]";
         internal const string CurrentState = "The current State name [Readonly]";
        internal const string CheckMark = " [\u2714]";
        internal const string UncheckMark = " [\u2718]";
        internal const string ThickArrow = "    \u279C ";
        internal const string SharpArrow = "  \u27A4  ";
        internal const string StateChanged = " state changed";
        internal const string TransitionConditions = "Transition Conditions:";
        internal const string Is = " == ";
        internal const string Divider = "--------------------------------";
        internal static string GetComponentError(string typeName, string name)
        {
            return $"{typeName} not found in {name}.";
        }
    }
}
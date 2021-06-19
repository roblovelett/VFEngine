namespace VFEngine.Tools.StateMachine.Debug.ScriptableObjects.Data
{
    internal static class Text
    {
        internal const string AppendNoCheckMark = " [✘]";
        internal const string AppendCheckMark = " [✔]";
        internal const string TransitionConditions = "Transition Conditions:";
        internal const string StateChanged = " state changed";
        internal const string Divider = "--------------------------------";
        internal const string EqualsOperator = " == ";
        internal const string SharpArrow = "  \u27A4  ";
        internal const string ThickArrow = "    \u279C ";
        internal const string DebugToolTip = "Enable debug: ";
        internal const string DebugLogToolTip = "Issues a debug log when a state transition is triggered";
        internal const string ConditionsToolTip =
            "List all conditions evaluated, the result is read: ConditionName == BooleanResult [PassedTest]";
    }
}
namespace VFEngine.Tools.StateMachine.Editor.Data
{
    internal static class EditorText
    {
        internal const string TransitionsProperty = "transitions";
        private const string Transition = "Transition";
        private static readonly string TransitionWithInvalid = $"{Transition} with invalid ";
        private const string From = "\"From ";
        private const string Target = "\"Target ";
        private const string StateFoundInTable = "State\" found in table ";
        private const string Deleting = ", deleting...";

        internal const string StateHelpMessage =
            "Click on any State's name to see the Transitions it contains, or click the Pencil/Wrench icon to see its Actions.";

        internal const string ActionsHelpMessage =
            "Edit the Actions that a State performs per frame. The order represent the order of execution.";

        internal const string InitialState = " (Initial State)";
        internal const string ScrollDown = "scrolldown";
        internal const string ScrollUp = "scrollup";
        internal const string SceneViewTools = "SceneViewTools";
        internal const string ScrollLeft = "scrollleft";
        internal const string ExpectedResult = "ExpectedResult";
        internal const string Operator = "Operator";
        internal const string Condition = "Condition";
        internal const string FromStateProperty = "FromState";
        internal const string ToStateProperty = "ToState";
        internal const string If = "If";
        internal const string Is = "Is";
        internal const string To = "To";
        internal const string ToolbarMinus = "Toolbar Minus";
        internal static readonly string ConditionsProperty = $"{Condition}s";

        internal static string FromStateError(string name)
        {
            return $"{TransitionWithInvalid}{From}{StateFoundInTable}{name}{Deleting}";
        }

        internal static string TargetStateError(string name)
        {
            return $"{TransitionWithInvalid}{Target}{StateFoundInTable}{name}{Deleting}";
        }

        internal static string MovedFromState(string fromStateName, bool up)
        {
            var direction = up ? "up" : "down";
            return $"Moved {fromStateName} State {direction}";
        }

        internal static string AddedTransition(string fromStateName, string toStateName)
        {
            return $"Added transition from {fromStateName} to {toStateName}";
        }

        internal static string MovedTransition(string toStateName, bool up)
        {
            var direction = up ? "up" : "down";
            return $"Moved transition to {toStateName} {direction}";
        }

        internal static string DeletedTransition(string fromStateName, string toStateName)
        {
            return $"Deleted transition from {fromStateName} to {toStateName}";
        }

        internal const string InvalidTransitionDeleted = "Invalid transition deleted";
        /*"Click on any State's name to see the Transitions it contains, or click the Pencil/Wrench icon to see its Actions."
        " (Initial State)"
        "scrolldown"
        "scrollup"
        "SceneViewTools"
        "scrollleft"
        "Edit the Actions that a State performs per frame. The order represent the order of execution."
        $"Moved {fromStates[fromStatesIndex].name} State {(up ? "up" : "down")}"
        "ExpectedResult"
        "Operator"
        "Condition"
        $"Added transition from {transition.FromState} to {transition.ToState}"
        $"Moved transition to {transition.ToState.objectReferenceValue.name} {(up ? "up" : "down")}"
        $"Deleted transition from {transition.FromState.objectReferenceValue.name} " + "to {serializedTransition.ToState.objectReferenceValue.name}"*/
    }
}
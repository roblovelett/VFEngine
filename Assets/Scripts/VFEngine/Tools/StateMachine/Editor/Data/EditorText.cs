using VFEngine.Tools.StateMachine.ScriptableObjects;

namespace VFEngine.Tools.StateMachine.Editor.Data
{
    internal static class EditorText
    {
        private const string Transition = "Transition";
        private const string Target = "\"Target ";
        private const string StateFoundInTable = "State\" found in table ";
        private const string Deleting = ", deleting...";
        private const string TableField = "table-";
        private const string DefaultPath = "Assets/Scripts/VFEngine/Tools/StateMachine/Editor/Data/";
        private static readonly string TransitionWithInvalid = $"{Transition} with invalid ";
        private static readonly string FromLabel = $"\"{From} ";
        internal const string From = "From";
        internal const string TransitionsProperty = "transitions";
        internal const string Item = "Item";
        internal const string Actions = "Actions";
        internal const string ActionsProperty = "actions";
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
        internal const string Cancel = "Cancel";
        internal const string InvalidTransitionDeleted = "Invalid transition deleted";
        internal const string StateHelpMessage = "Click on any State's name to see the Transitions it contains.";
        internal static readonly string UxmlPath = $"{DefaultPath}TransitionTableWindow.uxml";
        internal static readonly string USSPath = $"{DefaultPath}TransitionTableWindow.uss";
        internal static readonly string AddTransitionButton = $"Add {Transition}";
        internal static readonly string ConditionsProperty = $"{Condition}s";
        internal static readonly string SameStateError = $"{FromStateProperty} and {ToStateProperty} are the same.";
        internal static readonly string TransitionTableWindowLabel = $"{Transition} Table Editor";
        internal static readonly string TableList = $"{TableField}list";
        internal static readonly string TableEditor = $"{TableField}-editor";
        internal static readonly string GuidFilter = $"t:{nameof(TransitionTableSO)}";

        internal static string FromStateError(string name)
        {
            return $"{TransitionWithInvalid}{FromLabel}{StateFoundInTable}{name}{Deleting}";
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

        internal static string LabelClass(bool isProSkin)
        {
            return $"label-{(isProSkin ? "pro" : "personal")}";
        }

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
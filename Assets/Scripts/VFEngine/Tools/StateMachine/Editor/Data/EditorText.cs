using VFEngine.Tools.StateMachine.ScriptableObjects;

namespace VFEngine.Tools.StateMachine.Editor.Data
{
    internal static class EditorText
    {
        // ReSharper disable once InconsistentNaming
        private const string VFEnginePath = "Assets/Scripts/VFEngine/";
        private const string Transition = "Transition";
        private const string Target = "\"Target ";
        private const string State = "State";
        private const string Deleting = ", deleting...";
        private const string TableField = "table-";
        private const string Tools = "Tools";
        private const string TransitionLc = "transition";
        private const string Action = "Action";
        private const string Scroll = "scroll";
        private const string Table = "Table";
        private const string New = "New";
        private const string Cs = "cs";
        private const string Name = "NAME";
        private const string Txt = ".txt";
        private const string Moved = "Moved ";
        private const string FromLc = " from ";
        private const string ToLc = " to ";
        private const string TheLc = " the ";
        private static readonly string SOScript = $"{SOProperty}.{Cs}";
        private static readonly string Transitions = $"{Transition}s";
        private static readonly string StateFoundInTable = $"{State}\" found in table ";
        private static readonly string StateMachine = $"{State}Machine";
        private static readonly string DefaultPath = $"{VFEnginePath}{Tools}/{StateMachine}/Editor/Data/";
        private static readonly string ScriptTemplatesPath = $"{DefaultPath}Templates";
        private static readonly string TransitionWithInvalid = $"{Transition} with invalid ";
        private static readonly string FromLabel = $"\"{From} ";
        private static readonly string TransitionTableWindow = $"{Transition}{Table}Window";
        private static readonly string StatePath = $"/{State}";
        internal const string From = "From";
        internal const string Item = "Item";
        internal const string ActionsProperty = "actions";
        internal const string ExpectedResult = "ExpectedResult";
        internal const string Operator = "Operator";
        internal const string Condition = "Condition";
        internal const string If = "If";
        internal const string Is = "Is";
        internal const string To = "To";
        internal const string ToolbarMinus = "Toolbar Minus";
        internal const string Cancel = "Cancel";
        internal const string SOProperty = "SO";
        internal const string Nbsp = " ";
        internal static readonly string TransitionsProperty = $"{TransitionLc}s";
        internal static readonly string InitialState = $" (Initial {State})";
        internal static readonly string ScrollDown = $"{Scroll}down";
        internal static readonly string ScrollUp = $"{Scroll}up";
        internal static readonly string SceneViewTools = $"SceneView{Tools}";
        internal static readonly string ScrollLeft = $"{Scroll}left";
        internal static readonly string FromStateProperty = $"From{State}";
        internal static readonly string ToStateProperty = $"To{State}";
        internal static readonly string InvalidTransitionDeleted = $"Invalid {TransitionLc} deleted";
        internal static readonly string Actions = $"{Action}s";
        internal static readonly string UxmlPath = $"{DefaultPath}{TransitionTableWindow}.uxml";
        internal static readonly string USSPath = $"{DefaultPath}{TransitionTableWindow}.uss";
        internal static readonly string AddTransitionButton = $"Add {Transition}";
        internal static readonly string ConditionsProperty = $"{Condition}s";
        internal static readonly string SameStateError = $"{FromStateProperty} and {ToStateProperty} are{TheLc}same.";
        internal static readonly string TransitionTableWindowLabel = $"{Transition} Table Editor";
        internal static readonly string TableList = $"{TableField}list";
        internal static readonly string TableEditor = $"{TableField}-editor";
        internal static readonly string GuidFilter = $"t:{nameof(TransitionTableSO)}";
        internal static readonly string ActionPath = $"{New}{Action}{SOScript}";
        internal static readonly string ConditionPath = $"{New}{Condition}{SOScript}";
        internal static readonly string ScriptName = $"#SCRIPT{Name}#";
        internal static readonly string RuntimeName = $"#RUNTIME{Name}#";
        internal static readonly string RuntimeNameWithSpaces = $"#{RuntimeName}_WITH_SPACES#";
        internal static readonly string ScriptIconContent = $"{Cs} Script Icon";
        internal static readonly string ActionTemplatePath = $"{ScriptTemplatesPath}{StatePath}{Action}{Txt}";
        internal static readonly string ConditionTemplatePath = $"{ScriptTemplatesPath}{StatePath}{Condition}{Txt}";

        internal static readonly string StateHelpMessage =
            $"Click on any {State}'s name{ToLc}see{TheLc}{Transitions} it contains.";

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
            return $"{Moved}{fromStateName} {State} {direction}";
        }

        internal static string AddedTransition(string fromStateName, string toStateName)
        {
            return $"Added {TransitionLc}{FromLc}{fromStateName}{ToLc}{toStateName}";
        }

        internal static string MovedTransition(string toStateName, bool up)
        {
            var direction = up ? "up" : "down";
            return $"{Moved}{TransitionLc}{ToLc}{toStateName} {direction}";
        }

        internal static string DeletedTransition(string fromStateName, string toStateName)
        {
            return $"Deleted {TransitionLc}{FromLc}{fromStateName}{ToLc}{toStateName}";
        }

        internal static string LabelClass(bool isProSkin)
        {
            return $"label-{(isProSkin ? "pro" : "personal")}";
        }
    }
}
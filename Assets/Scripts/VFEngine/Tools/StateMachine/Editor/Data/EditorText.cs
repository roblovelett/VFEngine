namespace VFEngine.Tools.StateMachine.Editor.Data
{
    internal static class EditorText
    {
        internal const string TransitionsProperty = "transitions";
        private const string Transition = "Transition";
        internal static string Transitions = $"{Transition}s";
        private static readonly string TransitionWithInvalid = $"{Transition} with invalid ";
        private const string From = "\"From ";
        private const string Target = "\"Target ";
        private const string StateFoundInTable = "State\" found in table ";
        private const string Deleting = ", deleting...";

        internal static string FromStateError(string name)
        {
            return $"{TransitionWithInvalid}{From}{StateFoundInTable}{name}{Deleting}";
        }

        internal static string TargetStateError(string name)
        {
            return $"{TransitionWithInvalid}{Target}{StateFoundInTable}{name}{Deleting}";
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
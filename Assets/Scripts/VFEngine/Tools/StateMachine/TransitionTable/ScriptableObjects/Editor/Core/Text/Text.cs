namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.Text
{
    public static class Text
    {
        public static string SeeTransitionText => "Click on any State's name to see the Transition it contains, or click the Pencil/Wrench to see its Actions.";
        
        public static class Reset
        {
            public static string TransitionsProperty => "Transitions";
            private static string TargetState => "Target State";
            private static string FromState => "From State";

            public static class Debug
            {
                public const string DeletedText = "Invalid transition deleted";
                public static string DeleteStateMessage(bool deleteFromState, bool deleteToState, string targetName)
                {
                    const string noState = "";
                    var deletingFromState = deleteFromState && deleteToState == false;
                    var deletingToState = deleteToState && deleteFromState == false;
                    var fromState = deletingFromState ? FromState : noState;
                    var toState = deletingToState ? TargetState : noState;
                    var deleteState = (deletingFromState ? fromState : toState);
                    return DeletedInvalidTransitionText.Replace("@State", deleteState).Replace("Table", targetName);
                }
                
                private const string DeletedInvalidTransitionText = "Transition with invalid @State found in table @Table, deleting...";

            }
        }
        
        
    }
}
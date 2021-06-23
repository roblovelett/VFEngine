/*using VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor;
using static UnityEngine.ScriptableObject;
//using static VFEngine.Tools.StateMachine.Editor.TransitionTableEditor.Text;
// ReSharper disable RedundantAssignment

namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.State
{
    internal class ReorderState
    {
        #region fields

        private static TransitionTableEditorDataSO _reorderData;
        private int initialTransitionsIndex;

        #endregion
        
        #region initialization

        internal ReorderState()
        {
            _reorderData = CreateInstance<TransitionTableEditorDataSO>();
        }
        
        #endregion
        
        #region Reorder

        // needed
        /*private static int CurrentFromState => _reorderData.currentFromState;
        private static bool MoveTransitionUp => _reorderData.MoveTransitionUp;
        private static Dictionary<Object, List<DisplayTransition>> GroupedTransitions => TransitionTableEditorDataSO.GroupedTransitions;
        private static List<Object> FromStates => GroupedTransitions.Keys.ToList();
        private static string CurrentFromStateName => FromStates[CurrentFromState].name;
        private static SerializedObject SerializedObject => _reorderData.SerializedObject;
        private static SerializedProperty Transitions => SerializedObject.FindProperty(TransitionsProperty);
        private static List<List<DisplayTransition>> TransitionsByFromStates => TransitionTableEditorDataSO.TransitionsByFromStates;*/
        
        // internal
        /*private static bool AddToInitialTransitionsIndex => !MoveTransitionUp;
        private List<DisplayTransition> ReorderedTransitions => TransitionsByFromStates[initialTransitionsIndex];
        private int TransitionsSourceIndex => ReorderedTransitions[0].SerializedTransition.Index;
        private int TargetTransitionsIndex => initialTransitionsIndex - 1;
        private DisplayTransition ReorderedTransition => TransitionsByFromStates[TargetTransitionsIndex][0];
        private SerializedTransition ReorderedSerializedTransition => ReorderedTransition.SerializedTransition;
        private int ReorderedTransitionsIndex => ReorderedSerializedTransition.Index;
        private string MovedState => MovedStateMessage(CurrentFromStateName, MoveTransitionUp);*/
/* 
 internal void Reorder(ref TransitionTableEditorDataSO data)
 {
     SetReorderData(data);
     OnReorderStateStart();
     SetInitialReorderedTransitionsIndex();
     OnReorderStateEnd(ref data);

     void OnReorderStateStart()
     {
         //initialTransitionsIndex = CurrentFromState;
     }

     void SetInitialReorderedTransitionsIndex()
     {
         //if (AddToInitialTransitionsIndex) initialTransitionsIndex++;
     }

     void OnReorderStateEnd(ref TransitionTableEditorDataSO dataInternal)
     {
         //Transitions.MoveArrayElement(TransitionsSourceIndex, ReorderedTransitionsIndex);
         SetData(ref dataInternal);
         //ApplyModifications(MovedState);
         //OnResetToggledIndex();
     }
 }

 private static void SetReorderData(TransitionTableEditorDataSO data)
 {
     _reorderData = data;
 }

 private static void SetData(ref TransitionTableEditorDataSO data)
 {
     data = _reorderData;
 }
 
 #endregion
}
}*/
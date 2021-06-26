using UnityEngine;
using VFEngine.Tools.StateMachine.Editor.Data;

namespace VFEngine.Tools.StateMachine.Editor.ScriptableObjects
{
    internal class TransitionTableItemSO : ScriptableObject
    {
        private SerializedTransition item;

        public TransitionTableItemSO(SerializedTransition itemInternal)
        {
            item = itemInternal;
        }
    }
}
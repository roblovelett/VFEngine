using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor;

namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.Reset
{
    using static ScriptableObject;

    internal class TransitionTableReset
    {
        private ResetTransitionTableData data;

        public TransitionTableReset()
        {
            Initialize();
        }

        private void Initialize()
        {
            data = CreateInstance<ResetTransitionTableData>();
            data.Initialize();
        }

        public void Do(ref TransitionTableEditorDataSO @in)
        {
            data.OnReset(ref @in);
        }
    }
}
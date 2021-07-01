using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.Data;

namespace VFEngine.Tools.StateMachine.Editor.Data.ScriptableObjects
{
    internal class TransitionItemSO : ScriptableObject
    {
        [SerializeField] public TransitionItem item = default(TransitionItem);
    }
}
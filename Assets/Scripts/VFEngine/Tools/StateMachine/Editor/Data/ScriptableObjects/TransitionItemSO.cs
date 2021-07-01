using JetBrains.Annotations;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.Data;

namespace VFEngine.Tools.StateMachine.Editor.Data.ScriptableObjects
{
    internal class TransitionItemSO : ScriptableObject
    {
        [SerializeField] [UsedImplicitly] internal TransitionItem item = default(TransitionItem);
    }
}
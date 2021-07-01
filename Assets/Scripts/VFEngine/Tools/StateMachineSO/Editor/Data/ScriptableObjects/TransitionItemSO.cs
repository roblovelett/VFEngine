using JetBrains.Annotations;
using UnityEngine;
using VFEngine.Tools.StateMachineSO.ScriptableObjects.Data;

namespace VFEngine.Tools.StateMachineSO.Editor.Data.ScriptableObjects
{
    internal class TransitionItemSO : ScriptableObject
    {
        [SerializeField] [UsedImplicitly] internal TransitionItem item = default(TransitionItem);
    }
}
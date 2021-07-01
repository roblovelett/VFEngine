using System;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects.Data
{
    [Serializable]
    public class TransitionItem
    {
        [SerializeField] internal StateSO fromState;
        [SerializeField] internal StateSO toState;
        [SerializeField] internal ConditionUsage[] conditions;
    }
}
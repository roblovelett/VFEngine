using System;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Data
{
    [Serializable]
    public class TransitionItem
    {
        [SerializeField] internal StateSO fromState;
        [SerializeField] internal StateSO toState;
        [SerializeField] internal ConditionUsage[] conditions;
    }
}
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

        [Serializable]
        internal struct ConditionUsage
        {
            [SerializeField] internal Result expectedResult;
            [SerializeField] internal StateConditionSO condition;
            [SerializeField] internal Operator @operator;
        }

        internal enum Result
        {
            True,
            False
        }

        internal enum Operator
        {
            And,
            Or
        }
    }
}
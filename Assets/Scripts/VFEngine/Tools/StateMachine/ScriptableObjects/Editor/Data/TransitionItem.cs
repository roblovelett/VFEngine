using System;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Editor.Data
{
    [Serializable]
    public class TransitionItem
    {
        [SerializeField] internal StateSO fromState;
        [SerializeField] internal StateSO toState;
        [SerializeField] internal ConditionUsage[] conditions;

        [Serializable]
        public struct ConditionUsage
        {
            [SerializeField] internal Result expectedResult;
            [SerializeField] internal StateConditionSO condition;
            [SerializeField] internal Operator @operator;
        }

        public enum Result
        {
            True,
            False
        }

        public enum Operator
        {
            And,
            Or
        }
    }
}
using System;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Data
{
    [Serializable]
    public struct ConditionUsage
    {
        [SerializeField] internal Result expectedResult;
        [SerializeField] internal StateConditionSO condition;
        [SerializeField] internal Operator @operator;
    }
}
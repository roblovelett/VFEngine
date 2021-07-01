using System;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects.Data
{
    [Serializable]
    public struct ConditionUsage
    {
        [SerializeField] internal Result expectedResult;
        [SerializeField] internal StateConditionSO condition;
        [SerializeField] internal Operator @operator;
    }
}
using System;
using UnityEngine;

namespace VFEngine.Tools.StateMachine
{
    using static AttributeTargets;
    
    [AttributeUsage(Field)]
    public class InitOnlyAttribute : PropertyAttribute { }
}
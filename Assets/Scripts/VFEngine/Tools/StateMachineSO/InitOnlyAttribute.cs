using System;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO
{
    using static AttributeTargets;
    
    [AttributeUsage(Field)]
    public class InitOnlyAttribute : PropertyAttribute { }
}
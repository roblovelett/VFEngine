using System;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Menu
{
    using static AttributeTargets;
    
    [AttributeUsage(Field)]
    public class InitOnlyAttribute : PropertyAttribute { }
}
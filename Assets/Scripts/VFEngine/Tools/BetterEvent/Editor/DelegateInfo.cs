using System.Reflection;
using UnityEngine;

namespace VFEngine.Tools.BetterEvent.Editor
{
    public struct DelegateInfo
    {
        public Object target;
        public MethodInfo method;
    }
}
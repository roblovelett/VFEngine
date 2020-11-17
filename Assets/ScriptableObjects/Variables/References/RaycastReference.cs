using System;
using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.Variables;

// ReSharper disable RedundantBaseConstructorCall
namespace ScriptableObjects.Variables.References
{
    [Serializable]
    public sealed class RaycastReference : BaseReference<Raycast, RaycastVariable>
    {
        public RaycastReference() : base()
        {
        }

        public RaycastReference(Raycast value) : base(value)
        {
        }
    }
}
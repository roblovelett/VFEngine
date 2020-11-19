using System;
using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.Variables;

namespace ScriptableObjects.Variables.References
{
    [Serializable]
    public sealed class CollisionReference : BaseReference<Collision, CollisionVariable>
    {
        public CollisionReference()
        {
        }

        public CollisionReference(Collision value) : base(value)
        {
        }
    }
}
using System;
using UnityEngine;

namespace ScriptableObjects.Atoms.Mask
{
    using static Debug;

    [Serializable]
    public struct Mask : IEquatable<Mask>
    {
        public readonly LayerMask layer;

        public Mask(LayerMask layer)
        {
            this.layer = layer;
        }

        public bool Equals(Mask other)
        {
            return layer == other.layer;
        }

        public override bool Equals(object obj)
        {
            Assert(obj != null, nameof(obj) + " != null");
            return Equals((Mask) obj);
        }

        public override int GetHashCode()
        {
            return layer.GetHashCode();
        }
    }
}
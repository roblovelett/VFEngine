using System;
using UnityEngine;

namespace ScriptableObjects.Atoms.Raycast
{
    [Serializable]
    public struct Raycast : IEquatable<Raycast>
    {
        public readonly RaycastHit2D hit2D;
        public readonly RaycastHit hit;

        public Raycast(RaycastHit2D hit2D)
        {
            this.hit2D = hit2D;
            hit = new RaycastHit();
        }

        public Raycast(RaycastHit hit)
        {
            hit2D = new RaycastHit2D();
            this.hit = hit;
        }

        public Raycast(RaycastHit2D hit2D, RaycastHit hit)
        {
            this.hit2D = hit2D;
            this.hit = hit;
        }

        public override bool Equals(object obj)
        {
            return Equals((Raycast) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (hit2D.GetHashCode() * 397) ^ hit.GetHashCode();
            }
        }

        public bool Equals(Raycast other)
        {
            return hit2D.Equals(other.hit2D) && hit.Equals(other.hit);
        }
    }
}
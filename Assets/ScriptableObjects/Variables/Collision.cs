using System;
using UnityEngine;

namespace ScriptableObjects.Variables
{
    [Serializable]
    public struct Collision : IEquatable<Collision>
    {
        public readonly Collider2D collider2D;
        public readonly Collider collider;

        public Collision(Collider2D collider2D)
        {
            this.collider2D = collider2D;
            collider = new Collider();
        }

        public Collision(Collider collider)
        {
            this.collider = collider;
            collider2D = new Collider2D();
        }

        public Collision(Collider2D collider2D, Collider collider)
        {
            this.collider2D = collider2D;
            this.collider = collider;
        }

        public bool Equals(Collision other)
        {
            return Equals(collider2D, other.collider2D) && Equals(collider, other.collider);
        }

        public override bool Equals(object obj)
        {
            return obj is Collision other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((collider2D != null ? collider2D.GetHashCode() : 0) * 397) ^
                       (collider != null ? collider.GetHashCode() : 0);
            }
        }
    }
}
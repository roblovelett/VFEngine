using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace VFEngine.Platformer.Physics.PhysicsMaterial
{
    public class PhysicsMaterialData : MonoBehaviour
    {
        [SerializeField] private FloatReference friction;

        public float Friction { get; set; }

        public float FrictionRef
        {
            set => value = friction.Value;
        }
    }
}
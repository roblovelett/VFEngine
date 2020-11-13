using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace VFEngine.Platformer.Physics.PhysicsMaterial
{
    public class PhysicsMaterialData : MonoBehaviour
    {
        [SerializeField] private FloatReference friction = new FloatReference();
        public float Friction => friction.Value;
    }
}
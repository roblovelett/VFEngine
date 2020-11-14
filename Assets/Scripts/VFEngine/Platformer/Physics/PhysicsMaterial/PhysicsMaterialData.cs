using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace VFEngine.Platformer.Physics.PhysicsMaterial
{[InlineEditor]
    public class PhysicsMaterialData : SerializedMonoBehaviour
    {
        [SerializeField] private FloatReference friction = new FloatReference();
        public float Friction => friction.Value;
    }
}
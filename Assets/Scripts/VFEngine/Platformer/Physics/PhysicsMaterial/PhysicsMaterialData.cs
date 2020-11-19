using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.PhysicsMaterial
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PhysicsMaterialData", menuName = PlatformerPhysicsMaterialDataPath, order = 0)]
    [InlineEditor]
    public class PhysicsMaterialData : ScriptableObject
    {
        [SerializeField] private FloatReference friction = new FloatReference();
        public float Friction => friction.Value;
    }
}
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.PhysicsMaterial
{
    using static ScriptableObjectExtensions;
    
    [CreateAssetMenu(fileName = "PhysicsMaterialSettings", menuName = PlatformerPhysicsMaterialSettingsPath, order = 0)]
    [InlineEditor]
    public class PhysicsMaterialSettings : ScriptableObject
    {
        [SerializeField] public float friction = 1f;
    }
}
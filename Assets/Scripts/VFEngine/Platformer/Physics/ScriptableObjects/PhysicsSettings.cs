using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.ScriptableObjects
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PhysicsSettings", menuName = PlatformerPhysicsSettingsPath, order = 0)]
    [InlineEditor]
    public class PhysicsSettings : ScriptableObject
    {
        #region properties

        [SerializeField] [Range(0, 90)] public float maximumSlopeAngle = 60f;
        [SerializeField] [Range(0, 90)] public float minimumWallAngle = 80f;
        [SerializeField] public float minimumMovementThreshold = 0.0001f;
        [SerializeField] public float gravity = -30f;
        [SerializeField] public float airFriction = 15f;
        [SerializeField] public float groundFriction = 30f;

        #endregion
    }
}
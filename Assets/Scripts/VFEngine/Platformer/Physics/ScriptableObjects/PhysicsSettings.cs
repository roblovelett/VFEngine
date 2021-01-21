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

        [SerializeField] public bool displayWarnings = true;
        [SerializeField] public float gravity = -30f;
        [SerializeField] public float fallMultiplier = 1f;
        [SerializeField] public float ascentMultiplier = 1f;
        [SerializeField] public Vector2 maximumVelocity = new Vector2(100f, 100f);
        [SerializeField] public float speedAccelerationOnGround = 20f;
        [SerializeField] public float speedAccelerationInAir = 5f;
        [SerializeField] public float speedFactor = 1f;
        [SerializeField] [Range(0, 90)] public float maximumSlopeAngle = 30f;
        [SerializeField] public AnimationCurve slopeAngleSpeedFactor = new AnimationCurve(new Keyframe(-90f, 1f), new Keyframe(0f, 1f), new Keyframe(90f, 1f));
        [SerializeField] public bool physics2DInteraction = true;
        [SerializeField] public float physics2DPushForce = 2f;
        [SerializeField] public bool safeSetTransform = false;
        [SerializeField] public bool automaticGravityControl;
        [SerializeField] public bool stickToSlopeBehavior = false;
        [SerializeField] public float movementDirectionThreshold = 0.0001f;

        #endregion
    }
}
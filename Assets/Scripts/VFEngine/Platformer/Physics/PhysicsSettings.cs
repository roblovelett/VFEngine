using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PhysicsSettings", menuName = PlatformerPhysicsSettingsPath, order = 0)]
    [InlineEditor]
    public class PhysicsSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public float gravity = -30f;
        [SerializeField] public float fallMultiplier = 1f;
        [SerializeField] public float ascentMultiplier = 1f;
        [SerializeField] public Vector2 maximumVelocity = new Vector2(100f, 100f);
        [SerializeField] public float speedAccelerationOnGround = 20f;
        [SerializeField] public float speedAccelerationInAir = 5f;
        [SerializeField] public float speedFactor = 1f;
        [SerializeField] [Range(0, 90)] public float maximumSlopeAngle = 30f;
        [SerializeField] public AnimationCurve slopeAngleSpeedFactor = new AnimationCurve(new Keyframe(-90f, 1f), new Keyframe(0f, 1f), new Keyframe(90f, 1f));
        [SerializeField] public float physics2DPushForce = 2f;
        [SerializeField] public bool physics2DInteractionControl = true;
        [SerializeField] public bool safeSetTransformControl;
        [SerializeField] public bool safetyBoxcastControl = true;
        [SerializeField] public bool stickToSlopeControl = true;
        [SerializeField] public bool automaticGravityControl = true;

        [LabelText("Display Warnings")] [SerializeField] public bool displayWarningsControl = true;

        #endregion
    }
}
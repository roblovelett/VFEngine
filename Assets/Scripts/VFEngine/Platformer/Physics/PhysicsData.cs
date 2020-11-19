using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PhysicsData", menuName = PlatformerPhysicsDataPath, order = 0)]
    [InlineEditor]
    public class PhysicsData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsSettings settings = null;

        #endregion

        [SerializeField] private BoolReference physics2DInteractionControl = new BoolReference();
        [SerializeField] private BoolReference safetyBoxcastControl = new BoolReference();
        [SerializeField] private BoolReference stickToSlopesControl = new BoolReference();
        [SerializeField] private BoolReference safeSetTransformControl = new BoolReference();
        [SerializeField] private BoolReference isJumping = new BoolReference();
        [SerializeField] private BoolReference isFalling = new BoolReference();
        [SerializeField] private BoolReference gravityActive = new BoolReference();
        [SerializeField] private IntReference horizontalMovementDirection = new IntReference();
        [SerializeField] private FloatReference physics2DPushForce = new FloatReference();
        [SerializeField] private FloatReference maximumSlopeAngle = new FloatReference();
        [SerializeField] private FloatReference fallSlowFactor = new FloatReference();
        [SerializeField] private FloatReference smallValue = new FloatReference();
        [SerializeField] private FloatReference gravity = new FloatReference();
        [SerializeField] private FloatReference movementDirectionThreshold = new FloatReference();
        [SerializeField] private Vector2Reference maximumVelocity = new Vector2Reference();
        [SerializeField] private Vector2Reference speed = new Vector2Reference();
        [SerializeField] private Vector2Reference newPosition = new Vector2Reference();
        [SerializeField] private Vector2Reference externalForce = new Vector2Reference();
        private const string PhPath = "Physics/";
        private static readonly string ModelAssetPath = $"{PhPath}PhysicsModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public bool HasTransform => Transform;
        public bool HasSettings => settings;
        public float Physics2DPushForceSetting => settings.physics2DPushForce;
        public bool Physics2DInteractionControlSetting => settings.physics2DInteractionControl;
        public Vector2 MaximumVelocitySetting => settings.maximumVelocity;
        public AnimationCurve SlopeAngleSpeedFactorSetting => settings.slopeAngleSpeedFactor;
        public bool SafetyBoxcastControlSetting => settings.safetyBoxcastControl;
        public float MaximumSlopeAngleSetting => settings.maximumSlopeAngle;
        public bool StickToSlopesControlSetting => settings.stickToSlopeControl;
        public bool SafeSetTransformControlSetting => settings.safeSetTransformControl;
        public bool DisplayWarningsControlSetting => settings.displayWarningsControl;
        public bool AutomaticGravityControlSetting => settings.automaticGravityControl;
        public float AscentMultiplierSetting => settings.ascentMultiplier;
        public float FallMultiplierSetting => settings.fallMultiplier;
        public float GravitySetting => settings.gravity;
        public float MovingPlatformCurrentGravity { get; set; }
        public Vector2 MovingPlatformCurrentSpeed { get; set; }
        public float BoundsWidth { get; set; }
        public float RayOffset { get; set; }
        public float CurrentDownHitSmallestDistance { get; set; }
        public float BoundsHeight { get; set; }
        public RaycastHit2D SafetyBoxcastHit { get; set; }
        public float LeftStickyRaycastOriginY { get; set; }
        public float RightStickyRaycastOriginY { get; set; }
        public RaycastHit2D LeftStickyRaycastHit { get; set; }
        public RaycastHit2D RightStickyRaycastHit { get; set; }
        public float UpRaycastSmallestDistance { get; set; }
        public float DistanceBetweenRightHitAndRaycastOrigin { get; set; }
        public float DistanceBetweenLeftHitAndRaycastOrigin { get; set; }
        public float BelowSlopeAngle { get; set; }
        public RaycastHitColliderContactList ContactList { get; set; }

        #endregion

        public AnimationCurve SlopeAngleSpeedFactor { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public float CurrentVerticalSpeedFactor { get; set; }

        public float Physics2DPushForce
        {
            get => physics2DPushForce.Value;
            set => value = physics2DPushForce.Value;
        }

        public bool Physics2DInteractionControl
        {
            get => physics2DInteractionControl.Value;
            set => value = physics2DInteractionControl.Value;
        }

        public Vector2 MaximumVelocity
        {
            get => maximumVelocity.Value;
            set => value = maximumVelocity.Value;
        }

        public bool SafetyBoxcastControl
        {
            get => safetyBoxcastControl.Value;
            set => value = safetyBoxcastControl.Value;
        }

        public float MaximumSlopeAngle
        {
            get => maximumSlopeAngle.Value;
            set => value = maximumSlopeAngle.Value;
        }

        public bool StickToSlopesControl
        {
            get => stickToSlopesControl.Value;
            set => value = stickToSlopesControl.Value;
        }

        public bool SafeSetTransformControl
        {
            get => safeSetTransformControl.Value;
            set => value = safeSetTransformControl.Value;
        }

        public Vector2 Speed
        {
            get => speed.Value;
            set => value = speed.Value;
        }

        public bool GravityActive
        {
            get => gravityActive.Value;
            set => value = gravityActive.Value;
        }

        public float FallSlowFactor
        {
            get => fallSlowFactor.Value;
            set => value = fallSlowFactor.Value;
        }

        public int HorizontalMovementDirection
        {
            get => horizontalMovementDirection.Value;
            set => value = horizontalMovementDirection.Value;
        }

        public Vector2 NewPosition
        {
            get => newPosition.Value;
            set => value = newPosition.Value;
        }

        public float NewPositionX
        {
            get => newPosition.Value.x;
            set => value = NewPosition.x;
        }

        public float NewPositionY
        {
            get => newPosition.Value.y;
            set => value = NewPosition.y;
        }

        public float SmallValue
        {
            get => smallValue.Value;
            set => value = smallValue.Value;
        }

        public float Gravity
        {
            get => gravity.Value;
            set => value = gravity.Value;
        }

        public bool IsJumping
        {
            get => isJumping.Value;
            set => value = isJumping.Value;
        }

        public bool IsFalling
        {
            get => isFalling.Value;
            set => value = isFalling.Value;
        }

        public Vector2 ExternalForce
        {
            get => externalForce.Value;
            set => value = externalForce.Value;
        }

        public float ExternalForceY
        {
            get => ExternalForce.y;
            set => value = ExternalForce.y;
        }

        public bool DisplayWarningsControl { get; set; }
        public bool AutomaticGravityControl { get; set; }
        public float AscentMultiplier { get; set; }
        public float FallMultiplier { get; set; }
        public Vector2 WorldSpeed { get; set; } = Vector2.zero;
        public Vector2 ForcesApplied { get; set; } = Vector2.zero;
        public Rigidbody2D CurrentHitRigidBody { get; set; }
        public bool CurrentHitRigidBodyCanBePushed { get; set; }
        public Vector2 CurrentPushDirection { get; set; } = Vector2.zero;
        public float CurrentGravity { get; set; }
        public int StoredHorizontalMovementDirection { get; set; }

        public float SpeedX
        {
            get => Speed.x;
            set => value = Speed.x;
        }

        public float SpeedY
        {
            get => Speed.y;
            set => value = Speed.y;
        }

        public float MovementDirectionThreshold
        {
            get => movementDirectionThreshold.Value;
            set => value = movementDirectionThreshold.Value;
        }

        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
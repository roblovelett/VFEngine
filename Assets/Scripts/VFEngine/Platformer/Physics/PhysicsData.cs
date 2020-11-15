using System.Collections.Generic;
using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics
{
    using static ScriptableObjectExtensions;
    [InlineEditor]
    public class PhysicsData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private PhysicsSettings settings = null;
        [SerializeField] private new Transform transform = null;
        [SerializeField] private FloatReference movingPlatformCurrentGravity = new FloatReference();
        [SerializeField] private FloatReference currentVerticalSpeedFactor = new FloatReference();
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed = new Vector2Reference();
        [SerializeField] private FloatReference boundsWidth = new FloatReference();
        [SerializeField] private FloatReference rayOffset = new FloatReference();
        [SerializeField] private FloatReference currentDownHitSmallestDistance = new FloatReference();
        [SerializeField] private FloatReference boundsHeight = new FloatReference();
        [SerializeField] private RaycastReference safetyBoxcast = new RaycastReference();
        [SerializeField] private FloatReference leftStickyRaycastOriginY = new FloatReference();
        [SerializeField] private FloatReference rightStickyRaycastOriginY = new FloatReference();
        [SerializeField] private RaycastReference leftStickyRaycast = new RaycastReference();
        [SerializeField] private RaycastReference rightStickyRaycast = new RaycastReference();
        [SerializeField] private FloatReference upRaycastSmallestDistance = new FloatReference();
        [SerializeField] private FloatReference distanceBetweenRightHitAndRaycastOrigin = new FloatReference();
        [SerializeField] private FloatReference distanceBetweenLeftHitAndRaycastOrigin = new FloatReference();
        [SerializeField] private FloatReference belowSlopeAngle = new FloatReference();
        [SerializeField] private RaycastHitColliderContactList rhcContact = null;

        #endregion

        [SerializeField] private FloatReference physics2DPushForce = new FloatReference();
        [SerializeField] private BoolReference physics2DInteractionControl = new BoolReference();
        [SerializeField] private Vector2Reference maximumVelocity = new Vector2Reference();
        [SerializeField] private BoolReference safetyBoxcastControl = new BoolReference();
        [SerializeField] private FloatReference maximumSlopeAngle = new FloatReference();
        [SerializeField] private BoolReference stickToSlopesControl = new BoolReference();
        [SerializeField] private BoolReference safeSetTransformControl = new BoolReference();
        [SerializeField] private Vector2Reference speed = new Vector2Reference();
        [SerializeField] private BoolReference gravityActive = new BoolReference();
        [SerializeField] private FloatReference fallSlowFactor = new FloatReference();
        [SerializeField] private IntReference horizontalMovementDirection = new IntReference();
        [SerializeField] private Vector2Reference newPosition = new Vector2Reference();
        [SerializeField] private FloatReference smallValue = new FloatReference();
        [SerializeField] private FloatReference gravity = new FloatReference();
        [SerializeField] private BoolReference isJumping = new BoolReference();
        [SerializeField] private BoolReference isFalling = new BoolReference();
        [SerializeField] private Vector2Reference externalForce = new Vector2Reference();
        [SerializeField] private FloatReference movementDirectionThreshold = new FloatReference();
        private const string PhPath = "Physics/";
        private static readonly string ModelAssetPath = $"{PhPath}PhysicsModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool HasTransform => transform;
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
        public float CurrentVerticalSpeedFactor => currentVerticalSpeedFactor.Value;

        //public bool HasGravityController => gravityController;
        public float MovingPlatformCurrentGravity => movingPlatformCurrentGravity.Value;
        public Vector2 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public float BoundsWidth => boundsWidth.Value;
        public float RayOffset => rayOffset.Value;
        public float CurrentDownHitSmallestDistance => currentDownHitSmallestDistance.Value;
        public float BoundsHeight => boundsHeight.Value;

        public RaycastHit2D SafetyBoxcast
        {
            get
            {
                var r = safetyBoxcast.Value;
                return r.hit2D;
            }
        }

        public float LeftStickyRaycastOriginY => leftStickyRaycastOriginY.Value;
        public float RightStickyRaycastOriginY => rightStickyRaycastOriginY.Value;

        public RaycastHit2D LeftStickyRaycast
        {
            get
            {
                var r = leftStickyRaycast.Value;
                return r.hit2D;
            }
        }

        public RaycastHit2D RightStickyRaycast
        {
            get
            {
                var r = rightStickyRaycast.Value;
                return r.hit2D;
            }
        }

        public float UpRaycastSmallestDistance => upRaycastSmallestDistance.Value;
        public float DistanceBetweenRightHitAndRaycastOrigin => distanceBetweenRightHitAndRaycastOrigin.Value;
        public float DistanceBetweenLeftHitAndRaycastOrigin => distanceBetweenLeftHitAndRaycastOrigin.Value;
        public float BelowSlopeAngle => belowSlopeAngle.Value;
        public IEnumerable<RaycastHit2D> ContactList => rhcContact.List;

        #endregion

        public AnimationCurve SlopeAngleSpeedFactor { get; set; }

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
            set => value = safetyBoxcastControl.Value;
        }

        public float MaximumSlopeAngle
        {
            set => value = maximumSlopeAngle.Value;
        }

        public bool StickToSlopesControl
        {
            set => value = stickToSlopesControl.Value;
        }

        public bool SafeSetTransformControl
        {
            set => value = safeSetTransformControl.Value;
        }

        public Transform Transform
        {
            get => transform;
            set => value = transform;
        }

        public Vector2 Speed
        {
            get => speed.Value;
            set => value = speed.Value;
        }

        public bool GravityActive
        {
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
            set => value = NewPosition.x;
        }

        public float NewPositionY
        {
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
            set => value = isJumping.Value;
        }

        public bool IsFalling
        {
            set => value = isFalling.Value;
        }

        public Vector2 ExternalForce
        {
            get => externalForce.Value;
            set => value = externalForce.Value;
        }

        public float ExternalForceY
        {
            set => value = ExternalForce.y;
        }

        public bool DisplayWarningsControl { get; set; }
        public bool AutomaticGravityControl { get; set; }
        public float AscentMultiplier { get; set; }
        public float FallMultiplier { get; set; }
        [HideInInspector] public Vector2 worldSpeed = Vector2.zero;
        [HideInInspector] public Vector2 forcesApplied = Vector2.zero;
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
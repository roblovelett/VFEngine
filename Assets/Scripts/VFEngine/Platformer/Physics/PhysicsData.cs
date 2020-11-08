using System.Collections.Generic;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Gravity;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics
{
    using static ScriptableObjectExtensions;

    public class PhysicsData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private PhysicsSettings settings;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private GravityController gravityController;
        [SerializeField] private FloatReference movingPlatformCurrentGravity;
        [SerializeField] private FloatReference currentVerticalSpeedFactor;
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private FloatReference distanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private RaycastHit2DReference safetyBoxcast;
        [SerializeField] private FloatReference leftStickyRaycastOriginY;
        [SerializeField] private FloatReference rightStickyRaycastOriginY;
        [SerializeField] private RaycastHit2DReference leftStickyRaycast;
        [SerializeField] private RaycastHit2DReference rightStickyRaycast;
        [SerializeField] private FloatReference upRaycastSmallestDistance;
        [SerializeField] private FloatReference distanceBetweenRightHitAndRaycastOrigin;
        [SerializeField] private FloatReference distanceBetweenLeftHitAndRaycastOrigin;
        [SerializeField] private FloatReference belowSlopeAngle;
        [SerializeField] private RaycastHitColliderContactList rhcContact;

        #endregion

        [SerializeField] private FloatReference physics2DPushForce;
        [SerializeField] private BoolReference physics2DInteractionControl;
        [SerializeField] private Vector2Reference maximumVelocity;
        [SerializeField] private BoolReference safetyBoxcastControl;
        [SerializeField] private FloatReference maximumSlopeAngle;
        [SerializeField] private BoolReference stickToSlopesControl;
        [SerializeField] private BoolReference safeSetTransformControl;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private BoolReference gravityActive;
        [SerializeField] private FloatReference fallSlowFactor;
        [SerializeField] private IntReference horizontalMovementDirection;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private FloatReference smallValue;
        [SerializeField] private FloatReference gravity;
        [SerializeField] private BoolReference isJumping;
        [SerializeField] private BoolReference isFalling;
        [SerializeField] private Vector2Reference externalForce;
        private const string PhPath = "Physics/";
        private static readonly string ModelAssetPath = $"{PhPath}DefaultPhysicsModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool HasTransform => characterTransform;
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
        public Transform CharacterTransform => characterTransform;
        public float CurrentVerticalSpeedFactor => currentVerticalSpeedFactor.Value;
        public bool HasGravityController => gravityController;
        public float MovingPlatformCurrentGravity => movingPlatformCurrentGravity.Value;
        public Vector2 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public float BoundsWidth => boundsWidth.Value;
        public float RayOffset => rayOffset.Value;

        public float DistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint =>
            distanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint.Value;

        public float BoundsHeight => boundsHeight.Value;
        public RaycastHit2D SafetyBoxcast => safetyBoxcast.Value;
        public float LeftStickyRaycastOriginY => leftStickyRaycastOriginY.Value;
        public float RightStickyRaycastOriginY => rightStickyRaycastOriginY.Value;
        public RaycastHit2D LeftStickyRaycast => leftStickyRaycast.Value;
        public RaycastHit2D RightStickyRaycast => rightStickyRaycast.Value;
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
            get => transform.Value;
            set => value = transform.Value;
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
        public Vector2 worldSpeed = Vector2.zero;
        public Vector2 forcesApplied = Vector2.zero;
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

        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
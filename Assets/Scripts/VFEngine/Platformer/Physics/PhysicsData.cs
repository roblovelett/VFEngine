using System.Collections.Generic;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Gravity;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics
{
    using static ScriptableObjectExtensions;

    public class PhysicsData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private PhysicsSettings settings;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private GravityController gravityController;
        [SerializeField] private FloatReference movingPlatformCurrentGravity;
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed;
        [SerializeField] private FloatReference maximumSlopeAngle;
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

        /* fields */
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private BoolReference gravityActive;
        [SerializeField] private FloatReference fallSlowFactor;
        [SerializeField] private IntReference horizontalMovementDirection;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private FloatReference smallValue;
        [SerializeField] private FloatReference gravity;
        [SerializeField] private BoolReference stickToSlopesControl;
        [SerializeField] private BoolReference isJumping;
        [SerializeField] private BoolReference safetyBoxcastControl;
        [SerializeField] private Vector2Reference externalForce;
        [SerializeField] private FloatReference externalForceX;
        [SerializeField] private FloatReference externalForceY;
        private const string PhPath = "Physics/";
        private static readonly string ModelAssetPath = $"{PhPath}DefaultPhysicsModel.asset";

        /* properties dependencies */
        public float Physics2DPushForce => settings.physics2DPushForce;
        public bool Physics2DInteractionControl => settings.physics2DInteractionControl;
        public Vector2 MaximumVelocity => settings.maximumVelocity;
        public float BelowSlopeAngle => belowSlopeAngle.Value;
        public AnimationCurve SlopeAngleSpeedFactor => settings.slopeAngleSpeedFactor;
        public float DistanceBetweenRightHitAndRaycastOrigin => distanceBetweenRightHitAndRaycastOrigin.Value;
        public float DistanceBetweenLeftHitAndRaycastOrigin => distanceBetweenLeftHitAndRaycastOrigin.Value;
        public float UpRaycastSmallestDistance => upRaycastSmallestDistance.Value;
        public RaycastHit2D LeftStickyRaycast => leftStickyRaycast.Value;
        public RaycastHit2D RightStickyRaycast => rightStickyRaycast.Value;
        public float LeftStickyRaycastOriginY => leftStickyRaycastOriginY.Value;
        public float RightStickyRaycastOriginY => rightStickyRaycastOriginY.Value;
        public RaycastHit2D SafetyBoxcast => safetyBoxcast.Value;
        public bool SafetyBoxcastControl => settings.safetyBoxcastControl;

        public bool SafetyBoxcastControlRef
        {
            set => value = safetyBoxcastControl.Value;
        }

        public bool IsJumpingRef
        {
            set => value = isJumping.Value;
        }

        public bool StickToSlopesControl => settings.stickToSlopeControl;

        public bool StickToSlopesControlRef
        {
            set => value = stickToSlopesControl.Value;
        }

        public Transform Transform
        {
            get => characterTransform;
            set => value = characterTransform;
        }

        public Transform TransformRef
        {
            set => value = transform.Value;
        }

        public bool HasSettings => settings;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool AutomaticGravityControl => settings.automaticGravityControl;
        public bool HasGravityController => gravityController;
        public bool HasTransform => characterTransform;
        public float Gravity => settings.gravity;

        public float GravityRef
        {
            set => value = gravity.Value;
        }

        public float AscentMultiplier => settings.ascentMultiplier;
        public float FallMultiplier => settings.fallMultiplier;
        public float MovingPlatformCurrentGravity => movingPlatformCurrentGravity.Value;
        public Vector2 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public float MaximumSlopeAngle => settings.maximumSlopeAngle;

        public float MaximumSlopeAngleRef
        {
            set => value = maximumSlopeAngle;
        }

        public float BoundsWidth => boundsWidth.Value;
        public float RayOffset => rayOffset.Value;

        public float DistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint =>
            distanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint.Value;

        public float BoundsHeight => boundsHeight.Value;
        public IEnumerable<RaycastHit2D> ContactList => rhcContact.List;

        /* properties */
        public Vector2 WorldSpeed { get; set; } = Vector2.zero;
        public Rigidbody2D CurrentHitRigidBody { get; set; }

        public bool CurrentHitRigidBodyCanBePushed { get; set; }
        public Vector2 CurrentPushDirection { get; set; } = Vector2.zero;
        
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public readonly PhysicsState state = new PhysicsState();
        public Vector2 ExternalForce { get; set; } = Vector2.zero;

        public Vector2 ExternalForceRef
        {
            set => value = externalForce.Value;
        }

        public float ExternalForceX
        {
            get => ExternalForce.x;
            set => value = ExternalForce.x;
        }

        public float ExternalForceXRef
        {
            set => value = externalForceX.Value;
        }

        public float ExternalForceY
        {
            get => ExternalForce.y;
            set => value = ExternalForce.y;
        }

        public float ExternalForceYRef
        {
            set => value = externalForceY.Value;
        }

        public float CurrentGravity { get; set; }
        public Vector2 Speed { get; set; } = Vector2.zero;
        public Vector2 ForcesApplied { get; set; }
        public int HorizontalMovementDirection { get; set; }

        public int HorizontalMovementDirectionRef
        {
            set => value = horizontalMovementDirection.Value;
        }

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

        public Vector2 SpeedRef
        {
            set => value = speed.Value;
        }

        public bool GravityActiveRef
        {
            set => value = gravityActive.Value;
        }

        public float FallSlowFactor { get; set; }

        public float FallSlowFactorRef
        {
            set => value = fallSlowFactor.Value;
        }

        public Vector2 NewPosition { get; set; }

        public Vector2 NewPositionRef
        {
            set => value = newPosition.Value;
        }

        public float NewPositionX
        {
            get => NewPosition.x;
            set => value = NewPosition.x;
        }

        public float NewPositionY
        {
            get => NewPosition.y;
            set => value = NewPosition.y;
        }

        public float SmallValue { get; set; } = 0.0001f;

        public float SmallValueRef
        {
            set => value = smallValue.Value;
        }
    }
}
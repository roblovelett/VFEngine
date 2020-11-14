using ScriptableObjects.Atoms.Raycast.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;

    public class PlatformerData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerSettings settings = null;
        [SerializeField] private PhysicsController physics = null;
        [SerializeField] private RaycastController raycast = null;
        [SerializeField] private RaycastHitColliderController raycastHitCollider = null;
        [SerializeField] private LayerMaskController layerMask = null;
        [SerializeField] private BoxcastController boxcast = null;
        [SerializeField] private Vector2Reference speed = new Vector2Reference();
        [SerializeField] private BoolReference gravityActive = new BoolReference();
        [SerializeField] private FloatReference fallSlowFactor = new FloatReference();
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed = new Vector2Reference();
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame = new BoolReference();
        [SerializeField] private FloatReference movementDirectionThreshold = new FloatReference();
        [SerializeField] private Vector2Reference externalForce = new Vector2Reference();
        [SerializeField] private BoolReference castRaysOnBothSides = new BoolReference();
        [SerializeField] private IntReference horizontalMovementDirection = new IntReference();
        [SerializeField] private BoolReference wasGroundedLastFrame = new BoolReference();
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide = new IntReference();
        [SerializeField] private FloatReference currentRightHitDistance = new FloatReference();
        [SerializeField] private FloatReference currentLeftHitDistance = new FloatReference();
        [SerializeField] private FloatReference currentDownHitSmallestDistance = new FloatReference();
        [SerializeField] private Collider2DReference currentRightHitCollider = new Collider2DReference();
        [SerializeField] private Collider2DReference currentLeftHitCollider = new Collider2DReference();
        [SerializeField] private Collider2DReference ignoredCollider = new Collider2DReference();
        [SerializeField] private FloatReference currentRightHitAngle = new FloatReference();
        [SerializeField] private FloatReference currentLeftHitAngle = new FloatReference();
        [SerializeField] private FloatReference maximumSlopeAngle = new FloatReference();
        [SerializeField] private BoolReference groundedEvent = new BoolReference();
        [SerializeField] private Vector2Reference newPosition = new Vector2Reference();
        [SerializeField] private FloatReference smallValue = new FloatReference();
        [SerializeField] private FloatReference gravity = new FloatReference();
        [SerializeField] private BoolReference isFalling = new BoolReference();
        [SerializeField] private BoolReference onMovingPlatform = new BoolReference();
        [SerializeField] private IntReference downHitsStorageLength = new IntReference();
        [SerializeField] private GameObjectReference standingOnLastFrame = new GameObjectReference();
        [SerializeField] private BoolReference hasStandingOnLastFrame = new BoolReference();
        [SerializeField] private LayerMask midHeightOneWayPlatformMask = new LayerMask();
        [SerializeField] private LayerMask stairsMask = new LayerMask();
        [SerializeField] private Collider2DReference standingOnCollider = new Collider2DReference();
        [SerializeField] private Vector2Reference boundsBottomCenterPosition = new Vector2Reference();
        [SerializeField] private FloatReference smallestDistanceToDownHit = new FloatReference();
        [SerializeField] private RaycastReference raycastDownHitAt = new RaycastReference();
        [SerializeField] private Vector3Reference crossBelowSlopeAngle = new Vector3Reference();
        [SerializeField] private LayerMask standingOnWithSmallestDistanceLayer = new LayerMask();
        [SerializeField] private FloatReference boundsHeight = new FloatReference();
        [SerializeField] private LayerMask oneWayPlatformMask = new LayerMask();
        [SerializeField] private LayerMask movingOneWayPlatformMask = new LayerMask();
        [SerializeField] private BoolReference hasPhysicsMaterialClosestToDownHit = new BoolReference();
        [SerializeField] private BoolReference hasPathMovementClosestToDownHit = new BoolReference();
        [SerializeField] private BoolReference hasMovingPlatform = new BoolReference();
        [SerializeField] private BoolReference stickToSlopesControl = new BoolReference();
        [SerializeField] private FloatReference stickToSlopesOffsetY = new FloatReference();
        [SerializeField] private BoolReference isJumping = new BoolReference();
        [SerializeField] private FloatReference stickyRaycastLength = new FloatReference();
        [SerializeField] private FloatReference rightStickyRaycastLength = new FloatReference();
        [SerializeField] private FloatReference leftStickyRaycastLength = new FloatReference();
        [SerializeField] private Vector3Reference crossBelowSlopeAngleLeft = new Vector3Reference();
        [SerializeField] private Vector3Reference crossBelowSlopeAngleRight = new Vector3Reference();
        [SerializeField] private FloatReference belowSlopeAngleLeft = new FloatReference();
        [SerializeField] private FloatReference belowSlopeAngleRight = new FloatReference();
        [SerializeField] private BoolReference isCastingLeft = new BoolReference();
        [SerializeField] private BoolReference performSafetyBoxcast = new BoolReference();
        [SerializeField] private RaycastReference safetyBoxcast = new RaycastReference();
        [SerializeField] private RaycastReference leftStickyRaycast = new RaycastReference();
        [SerializeField] private RaycastReference rightStickyRaycast = new RaycastReference();
        [SerializeField] private IntReference upHitsStorageLength = new IntReference();
        [SerializeField] private RaycastReference raycastUpHitAt = new RaycastReference();
        [SerializeField] private FloatReference upRaycastSmallestDistance = new FloatReference();
        [SerializeField] private BoolReference upHitConnected = new BoolReference();
        [SerializeField] private BoolReference rightHitConnected = new BoolReference();
        [SerializeField] private BoolReference downHitConnected = new BoolReference();
        [SerializeField] private BoolReference leftHitConnected = new BoolReference();
        [SerializeField] private IntReference rightHitsStorageLength = new IntReference();
        [SerializeField] private IntReference leftHitsStorageLength = new IntReference();
        [SerializeField] private BoolReference isCollidingBelow = new BoolReference();
        [SerializeField] private BoolReference isCollidingLeft = new BoolReference();
        [SerializeField] private BoolReference isCollidingRight = new BoolReference();
        [SerializeField] private BoolReference isCollidingAbove = new BoolReference();
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength = new FloatReference();
        [SerializeField] private BoolReference distanceToGroundRaycastHit = new BoolReference();
        [SerializeField] private RaycastReference distanceToGroundRaycast = new RaycastReference();

        #endregion

        private const string ModelAssetPath = "PlatformerModel.asset";

        #endregion

        #region properties

        #region dependencies

        public float DistanceToGroundRayMaximumLength => distanceToGroundRayMaximumLength.Value;
        public bool IsCollidingAbove => isCollidingAbove.Value;
        public bool IsCollidingBelow => isCollidingBelow.Value;
        public bool IsCollidingLeft => isCollidingLeft.Value;
        public bool IsCollidingRight => isCollidingRight.Value;
        public int LeftHitsStorageLength => leftHitsStorageLength.Value;
        public int RightHitsStorageLength => rightHitsStorageLength.Value;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool HasSettings => settings;
        public PhysicsController Physics => physics;
        public RaycastController Raycast => raycast;
        public RaycastHitColliderController RaycastHitCollider => raycastHitCollider;
        public LayerMaskController LayerMask => layerMask;
        public BoxcastController Boxcast => boxcast;
        public Vector2 Speed => speed.Value;
        public bool GravityActive => gravityActive.Value;
        public float FallSlowFactor => fallSlowFactor.Value;
        public Vector2 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public bool WasTouchingCeilingLastFrame => wasTouchingCeilingLastFrame.Value;
        public float MovementDirectionThreshold => movementDirectionThreshold.Value;
        public bool CastRaysOnBothSides => castRaysOnBothSides.Value;
        public Vector2 ExternalForce => externalForce.Value;
        public int HorizontalMovementDirection => horizontalMovementDirection.Value;
        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public bool WasGroundedLastFrame => wasGroundedLastFrame.Value;
        public float CurrentRightHitDistance => currentRightHitDistance.Value;
        public float CurrentLeftHitDistance => currentLeftHitDistance.Value;
        public Collider2D CurrentRightHitCollider => currentRightHitCollider.Value;
        public Collider2D CurrentLeftHitCollider => currentLeftHitCollider.Value;
        public Collider2D IgnoredCollider => ignoredCollider.Value;
        public float CurrentRightHitAngle => currentRightHitAngle.Value;
        public float CurrentLeftHitAngle => currentLeftHitAngle.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;
        public bool GroundedEvent => groundedEvent.Value;
        public Vector2 NewPosition => newPosition.Value;
        public float SmallValue => smallValue.Value;
        public float Gravity => gravity.Value;
        public bool IsFalling => isFalling.Value;
        public bool OnMovingPlatform => onMovingPlatform.Value;
        public int DownHitsStorageLength => downHitsStorageLength.Value;
        public int NumberOfVerticalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public GameObject StandingOnLastFrame => standingOnLastFrame.Value;
        public bool HasStandingOnLastFrame => hasStandingOnLastFrame.Value;
        public LayerMask MidHeightOneWayPlatformMask => midHeightOneWayPlatformMask;
        public LayerMask StairsMask => stairsMask;
        public Collider2D StandingOnCollider => standingOnCollider.Value;
        public Vector2 BoundsBottomCenterPosition => boundsBottomCenterPosition.Value;
        public float SmallestDistanceToDownHit => smallestDistanceToDownHit.Value;
        public bool DownHitConnected => downHitConnected.Value;
        public float CurrentDownHitSmallestDistance => currentDownHitSmallestDistance.Value;

        public RaycastHit2D RaycastDownHitAt
        {
            get
            {
                var r = raycastDownHitAt.Value;
                return r.hit2D;
            }
        }

        public Vector3 CrossBelowSlopeAngle => crossBelowSlopeAngle.Value;
        public LayerMask StandingOnWithSmallestDistanceLayer => standingOnWithSmallestDistanceLayer;
        public float BoundsHeight => boundsHeight.Value;
        public LayerMask OneWayPlatformMask => oneWayPlatformMask;
        public LayerMask MovingOneWayPlatformMask => movingOneWayPlatformMask;
        public bool HasPhysicsMaterialDataClosestToDownHit => hasPhysicsMaterialClosestToDownHit.Value;
        public bool HasPathMovementControllerClosestToDownHit => hasPathMovementClosestToDownHit.Value;
        public bool HasMovingPlatform => hasMovingPlatform.Value;
        public bool StickToSlopesControl => stickToSlopesControl.Value;
        public float StickToSlopesOffsetY => stickToSlopesOffsetY.Value;
        public bool IsJumping => isJumping.Value;
        public float StickyRaycastLength => stickyRaycastLength.Value;
        public float LeftStickyRaycastLength => leftStickyRaycastLength.Value;
        public float RightStickyRaycastLength => rightStickyRaycastLength.Value;
        public Vector3 CrossBelowSlopeAngleLeft => crossBelowSlopeAngleLeft.Value;
        public Vector3 CrossBelowSlopeAngleRight => crossBelowSlopeAngleRight.Value;
        public float BelowSlopeAngleLeft => belowSlopeAngleLeft.Value;
        public float BelowSlopeAngleRight => belowSlopeAngleRight.Value;
        public bool IsCastingLeft => isCastingLeft.Value;
        public bool PerformSafetyBoxcast => performSafetyBoxcast.Value;

        public RaycastHit2D SafetyBoxcast
        {
            get
            {
                var r = safetyBoxcast.Value;
                return r.hit2D;
            }
        }

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

        public int UpHitsStorageLength => upHitsStorageLength.Value;

        public RaycastHit2D RaycastUpHitAt
        {
            get
            {
                var r = raycastUpHitAt.Value;
                return r.hit2D;
            }
        }

        public float UpRaycastSmallestDistance => upRaycastSmallestDistance.Value;
        public bool UpHitConnected => upHitConnected.Value;
        public bool RightHitConnected => rightHitConnected.Value;
        public bool LeftHitConnected => leftHitConnected.Value;
        public bool DistanceToGroundRaycastHit => distanceToGroundRaycastHit.Value;

        public RaycastHit2D DistanceToGroundRaycast
        {
            get
            {
                var r = distanceToGroundRaycast.Value;
                return r.hit2D;
            }
        }

        #endregion

        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using ScriptableObjects.Atoms.LayerMask.References;
//using ScriptableObjects.Atoms.RaycastHit2D.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;

    public class PlatformerData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerSettings settings;
        [SerializeField] private PhysicsController physics;
        [SerializeField] private RaycastController raycast;
        [SerializeField] private RaycastHitColliderController raycastHitCollider;
        [SerializeField] private LayerMaskController layerMask;
        [SerializeField] private BoxcastController boxcast;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private BoolReference gravityActive;
        [SerializeField] private FloatReference fallSlowFactor;
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed;
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame;
        [SerializeField] private FloatReference movementDirectionThreshold;
        [SerializeField] private Vector2Reference externalForce;
        [SerializeField] private BoolReference castRaysOnBothSides;
        [SerializeField] private IntReference horizontalMovementDirection;
        [SerializeField] private BoolReference wasGroundedLastFrame;
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        [SerializeField] private FloatReference currentRightHitDistance;
        [SerializeField] private FloatReference currentLeftHitDistance;
        [SerializeField] private FloatReference currentDownHitSmallestDistance;
        [SerializeField] private Collider2DReference currentRightHitCollider;
        [SerializeField] private Collider2DReference currentLeftHitCollider;
        [SerializeField] private Collider2DReference ignoredCollider;
        [SerializeField] private FloatReference currentRightHitAngle;
        [SerializeField] private FloatReference currentLeftHitAngle;
        [SerializeField] private FloatReference maximumSlopeAngle;
        [SerializeField] private BoolReference isGrounded;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private FloatReference smallValue;
        [SerializeField] private FloatReference gravity;
        [SerializeField] private BoolReference isFalling;
        [SerializeField] private BoolReference onMovingPlatform;
        [SerializeField] private IntReference downHitsStorageLength;
        [SerializeField] private GameObjectReference standingOnLastFrame;
        [SerializeField] private BoolReference hasStandingOnLastFrame;
        [SerializeField] private LayerMaskReference midHeightOneWayPlatformMask;
        [SerializeField] private LayerMaskReference stairsMask;
        [SerializeField] private Collider2DReference standingOnCollider;
        [SerializeField] private Vector2Reference boundsBottomCenterPosition;
        [SerializeField] private FloatReference smallestDistance;
        //[SerializeField] private RaycastHit2DReference raycastDownHitAt;
        [SerializeField] private Vector3Reference crossBelowSlopeAngle;
        [SerializeField] private LayerMaskReference standingOnWithSmallestDistanceLayer;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private LayerMaskReference oneWayPlatformMask;
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask;
        [SerializeField] private BoolReference hasPhysicsMaterialClosestToDownHit;
        [SerializeField] private BoolReference hasPathMovementClosestToDownHit;
        [SerializeField] private BoolReference hasMovingPlatform;
        [SerializeField] private BoolReference stickToSlopesControl;
        [SerializeField] private FloatReference stickToSlopesOffsetY;
        [SerializeField] private BoolReference isJumping;
        [SerializeField] private FloatReference stickyRaycastLength;
        [SerializeField] private FloatReference rightStickyRaycastLength;
        [SerializeField] private FloatReference leftStickyRaycastLength;
        [SerializeField] private Vector3Reference crossBelowSlopeAngleLeft;
        [SerializeField] private Vector3Reference crossBelowSlopeAngleRight;
        [SerializeField] private FloatReference belowSlopeAngleLeft;
        [SerializeField] private FloatReference belowSlopeAngleRight;
        [SerializeField] private BoolReference castFromLeft;
        [SerializeField] private BoolReference performSafetyBoxcast;
        //[SerializeField] private RaycastHit2DReference safetyBoxcast;
        //[SerializeField] private RaycastHit2DReference leftStickyRaycast;
        //[SerializeField] private RaycastHit2DReference rightStickyRaycast;
        [SerializeField] private IntReference upHitsStorageLength;
        //[SerializeField] private RaycastHit2DReference raycastUpHitAt;
        [SerializeField] private FloatReference upRaycastSmallestDistance;
        [SerializeField] private BoolReference upHitConnected;
        [SerializeField] private BoolReference rightHitConnected;
        [SerializeField] private BoolReference downHitConnected;
        [SerializeField] private BoolReference leftHitConnected;
        [SerializeField] private IntReference rightHitsStorageLength;
        [SerializeField] private IntReference leftHitsStorageLength;
        [SerializeField] private BoolReference isCollidingBelow;
        [SerializeField] private BoolReference isCollidingLeft;
        [SerializeField] private BoolReference isCollidingRight;
        [SerializeField] private BoolReference isCollidingAbove;
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength;
        [SerializeField] private BoolReference distanceToGroundRaycastHit;
        //[SerializeField] private RaycastHit2DReference distanceToGroundRaycast;

        #endregion

        private const string ModelAssetPath = "DefaultPlatformerModel.asset";

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
        public bool IsGrounded => isGrounded.Value;
        public Vector2 NewPosition => newPosition.Value;
        public float SmallValue => smallValue.Value;
        public float Gravity => gravity.Value;
        public bool IsFalling => isFalling.Value;
        public bool OnMovingPlatform => onMovingPlatform.Value;
        public int DownHitsStorageLength => downHitsStorageLength.Value;
        public int NumberOfVerticalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public GameObject StandingOnLastFrame => standingOnLastFrame.Value;
        public bool HasStandingOnLastFrame => hasStandingOnLastFrame.Value;
        public LayerMask MidHeightOneWayPlatformMask => midHeightOneWayPlatformMask.Value;
        public LayerMask StairsMask => stairsMask.Value;
        public Collider2D StandingOnCollider => standingOnCollider.Value;
        public Vector2 BoundsBottomCenterPosition => boundsBottomCenterPosition.Value;
        public float SmallestDistance => smallestDistance.Value;
        public bool DownHitConnected => downHitConnected.Value;
        public float CurrentDownHitSmallestDistance => currentDownHitSmallestDistance.Value;
        //public RaycastHit2D RaycastDownHitAt => raycastDownHitAt.Value;
        public Vector3 CrossBelowSlopeAngle => crossBelowSlopeAngle.Value;
        public LayerMask StandingOnWithSmallestDistanceLayer => standingOnWithSmallestDistanceLayer.Value;
        public float BoundsHeight => boundsHeight.Value;
        public LayerMask OneWayPlatformMask => oneWayPlatformMask.Value;
        public LayerMask MovingOneWayPlatformMask => movingOneWayPlatformMask.Value;
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
        public bool CastFromLeft => castFromLeft.Value;
        public bool PerformSafetyBoxcast => performSafetyBoxcast.Value;
        //public RaycastHit2D SafetyBoxcast => safetyBoxcast.Value;
        //public RaycastHit2D LeftStickyRaycast => leftStickyRaycast.Value;
        //public RaycastHit2D RightStickyRaycast => rightStickyRaycast.Value;
        public int UpHitsStorageLength => upHitsStorageLength.Value;
        //public RaycastHit2D RaycastUpHitAt => raycastUpHitAt.Value;
        public float UpRaycastSmallestDistance => upRaycastSmallestDistance.Value;
        public bool UpHitConnected => upHitConnected.Value;
        public bool RightHitConnected => rightHitConnected.Value;
        public bool LeftHitConnected => leftHitConnected.Value;
        public bool DistanceToGroundRaycastHit => distanceToGroundRaycastHit.Value;
        //public RaycastHit2D DistanceToGroundRaycast => distanceToGroundRaycast.Value;

        #endregion

        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
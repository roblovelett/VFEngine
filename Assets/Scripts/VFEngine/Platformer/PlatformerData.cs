using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
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
        /* fields: dependencies */
        [SerializeField] private PlatformerSettings settings;
        [SerializeField] private PhysicsController physics;
        [SerializeField] private RaycastController raycast;
        [SerializeField] private RaycastHitColliderController raycastHitCollider;
        [SerializeField] private LayerMaskController layerMask;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private BoolReference gravityActive;
        [SerializeField] private FloatReference fallSlowFactor;
        [SerializeField] private BoolReference isCollidingWithMovingPlatform;
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed;
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame;
        [SerializeField] private FloatReference movementDirectionThreshold;
        [SerializeField] private Vector2Reference externalForce;
        [SerializeField] private BoolReference castRaysOnBothSides;
        [SerializeField] private IntReference horizontalMovementDirection;
        [SerializeField] private Vector2Reference horizontalRaycastFromBottom;
        [SerializeField] private Vector2Reference horizontalRaycastToTop;
        [SerializeField] private IntReference numberOfHorizontalRays;
        [SerializeField] private Vector2Reference currentRightRaycastOrigin;
        [SerializeField] private Vector2Reference currentLeftRaycastOrigin;
        [SerializeField] private BoolReference wasGroundedLastFrame;
        [SerializeField] private IntReference rightHitsStorageIndex;
        [SerializeField] private IntReference leftHitsStorageIndex;
        [SerializeField] private FloatReference horizontalRayLength;
        [SerializeField] private IntReference horizontalHitsStorageLength;
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
        [SerializeField] private FloatReference friction;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private FloatReference smallValue;
        [SerializeField] private FloatReference gravity;
        [SerializeField] private BoolReference isFalling;
        [SerializeField] private FloatReference downRayLength;
        [SerializeField] private BoolReference onMovingPlatform;
        [SerializeField] private IntReference numberOfVerticalRaysPerSide;
        [SerializeField] private IntReference downHitsStorageLength;
        [SerializeField] private GameObjectReference standingOnLastFrame;
        [SerializeField] private BoolReference isStandingOnLastFrameNotNull;
        [SerializeField] private LayerMaskReference midHeightOneWayPlatformMask;
        [SerializeField] private LayerMaskReference stairsMask;
        [SerializeField] private Collider2DReference standingOnCollider;
        [SerializeField] private Vector2Reference colliderBottomCenterPosition;
        [SerializeField] private FloatReference smallestDistance;
        [SerializeField] private IntReference downHitsStorageSmallestDistanceIndex;
        [SerializeField] private BoolReference downHitConnected;
        [SerializeField] private RaycastHit2DReference raycastDownHitAt;
        
        /* fields */
        private const string ModelAssetPath = "DefaultPlatformerModel.asset";

        /* properties: dependencies */
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool HasSettings => settings;
        public PhysicsController Physics => physics;
        public RaycastController Raycast => raycast;
        public RaycastHitColliderController RaycastHitCollider => raycastHitCollider;
        public LayerMaskController LayerMask => layerMask;
        public Vector2 Speed => speed.Value;
        public bool GravityActive => gravityActive.Value;
        public float FallSlowFactor => fallSlowFactor.Value;
        public bool IsCollidingWithMovingPlatform => isCollidingWithMovingPlatform.Value;
        public Vector2 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public bool WasTouchingCeilingLastFrame => wasTouchingCeilingLastFrame.Value;
        public float MovementDirectionThreshold => movementDirectionThreshold.Value;
        public bool CastRaysOnBothSides => castRaysOnBothSides.Value;
        public Vector2 ExternalForce => externalForce.Value;
        public int HorizontalMovementDirection => horizontalMovementDirection.Value;
        public Vector2 HorizontalRaycastFromBottom => horizontalRaycastFromBottom.Value;
        public Vector2 HorizontalRaycastToTop => horizontalRaycastToTop.Value;
        public int NumberOfHorizontalRays => numberOfHorizontalRays.Value;
        public int RightHitsStorageIndex => rightHitsStorageIndex.Value;
        public int LeftHitsStorageIndex => leftHitsStorageIndex;
        public float HorizontalRayLength => horizontalRayLength.Value;
        public int HorizontalHitsStorageLength => horizontalHitsStorageLength;
        public Vector2 CurrentRightRaycastOrigin => currentRightRaycastOrigin.Value;
        public Vector2 CurrentLeftRaycastOrigin => currentLeftRaycastOrigin.Value;
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
        public float Friction => friction.Value;
        public Vector2 NewPosition => newPosition.Value;
        public float SmallValue => smallValue.Value;
        public float Gravity => gravity.Value;
        public bool IsFalling => isFalling.Value;
        public bool OnMovingPlatform => onMovingPlatform.Value;
        public float DownRayLength => downRayLength.Value;
        public int DownHitsStorageLength => downHitsStorageLength.Value;
        public int NumberOfVerticalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public GameObject StandingOnLastFrame => standingOnLastFrame.Value;
        public bool IsStandingOnLastFrameNotNull => isStandingOnLastFrameNotNull.Value;
        public LayerMask MidHeightOneWayPlatformMask => midHeightOneWayPlatformMask.Value;
        public LayerMask StairsMask => stairsMask.Value;
        public Collider2D StandingOnCollider => standingOnCollider.Value;
        public Vector2 ColliderBottomCenterPosition => colliderBottomCenterPosition.Value;
        public float SmallestDistance => smallestDistance.Value;
        public int DownHitsStorageSmallestDistanceIndex => downHitsStorageSmallestDistanceIndex.Value;
        public bool DownHitConnected => downHitConnected.Value;

        public float CurrentDownHitSmallestDistance => currentDownHitSmallestDistance.Value;
        public RaycastHit2D RaycastDownHitAt => raycastDownHitAt.Value;
        
        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
    }
}
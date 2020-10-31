using JetBrains.Annotations;
using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Serialization;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Platformer.Physics.PhysicsMaterial;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    public class RaycastHitColliderData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private RaycastHitColliderSettings settings;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private RaycastHit2DReference currentRightRaycast;
        [SerializeField] private RaycastHit2DReference currentLeftRaycast;
        [SerializeField] private RaycastHit2DReference currentDownRaycast;
        [SerializeField] private RaycastHit2DReference currentUpRaycast;
        [SerializeField] private IntReference numberOfVerticalRaysPerSide;
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        [SerializeField] private Vector2Reference verticalRaycastFromLeft;
        [SerializeField] private Vector2Reference verticalRaycastToRight;
        [SerializeField] private Vector2Reference rightRaycastFromBottomOrigin;
        [SerializeField] private Vector2Reference rightRaycastToTopOrigin;
        [SerializeField] private Vector2Reference leftRaycastFromBottomOrigin;
        [SerializeField] private Vector2Reference leftRaycastToTopOrigin;
        [SerializeField] private IntReference savedBelowLayer;

        /* fields */
        [SerializeField] private RaycastHitColliderContactList contactList;
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private Vector2Reference boxColliderBoundsCenter;
        [SerializeField] private IntReference currentRightHitsStorageIndex;
        [SerializeField] private IntReference currentLeftHitsStorageIndex;
        [SerializeField] private IntReference currentDownHitsStorageIndex;
        [SerializeField] private IntReference currentUpHitsStorageIndex;
        [SerializeField] private FloatReference currentRightHitDistance;
        [SerializeField] private FloatReference currentLeftHitDistance;
        [SerializeField] private FloatReference currentDownHitSmallestDistance;
        [SerializeField] private Collider2DReference currentRightHitCollider;
        [SerializeField] private Collider2DReference currentLeftHitCollider;
        [SerializeField] private Collider2DReference ignoredCollider;
        [SerializeField] private FloatReference currentRightHitAngle;
        [SerializeField] private FloatReference currentLeftHitAngle;
        [SerializeField] private BoolReference isGrounded;
        [SerializeField] private FloatReference friction;
        [SerializeField] private BoolReference onMovingPlatform;
        [SerializeField] private GameObjectReference standingOnLastFrame;
        [SerializeField] private BoolReference isStandingOnLastFrame;
        [SerializeField] private Collider2DReference standingOnCollider;
        [SerializeField] private Vector2Reference colliderBottomCenterPosition;
        [SerializeField] private IntReference downHitsStorageSmallestDistanceIndex;
        [SerializeField] private BoolReference downHitConnected;
        [SerializeField] private RaycastHit2DReference raycastDownHitAt;
        [SerializeField] private Vector3Reference crossBelowSlopeAngle;
        [SerializeField] private RaycastHit2DReference downHitWithSmallestDistance;
        [SerializeField] private GameObjectReference standingOnWithSmallestDistance;
        [SerializeField] private Collider2DReference standingOnWithSmallestDistanceCollider;
        [SerializeField] private LayerMaskReference standingOnWithSmallestDistanceLayer;
        [SerializeField] private Vector2Reference standingOnWithSmallestDistancePoint;
        [SerializeField] private BoolReference hasPhysicsMaterialData;
        [SerializeField] private BoolReference hasPathMovementData;
        [SerializeField] private BoolReference hasMovingPlatform;
        [SerializeField] private BoolReference upHitConnected;
        [SerializeField] private IntReference upHitsStorageCollidingIndex;
        [SerializeField] private RaycastHit2DReference raycastUpHitAt;
        [SerializeField] private FloatReference distanceBetweenRightHitAndRaycastOrigin;
        [SerializeField] private FloatReference distanceBetweenLeftHitAndRaycastOrigin;
        [SerializeField] private FloatReference belowSlopeAngle;
        [SerializeField] private BoolReference isCollidingBelow;
        [SerializeField] private BoolReference isCollidingLeft;
        [SerializeField] private BoolReference isCollidingRight;
        [SerializeField] private BoolReference isCollidingAbove;
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength;
        [SerializeField] private RaycastHit2DReference distanceToGroundRaycast;
        [SerializeField] private FloatReference boundsHeight;
        private const string RhcPath = "Physics/Collider/RaycastHitCollider/";
        private static readonly string ModelAssetPath = $"{RhcPath}DefaultRaycastHitColliderModel.asset";

        /* properties: dependencies */
        public int SavedBelowLayer => savedBelowLayer.Value;
        public float BoundsHeight => boundsHeight.Value;
        public RaycastHit2D DistanceToGroundRaycast => distanceToGroundRaycast.Value;
        public float DistanceToGroundRayMaximumLength => distanceToGroundRayMaximumLength.Value;

        public float BelowSlopeAngleRef
        {
            set => value = belowSlopeAngle.Value;
        }

        public float BelowSlopeAngle => state.BelowSlopeAngle;
        public bool HasSettings => settings;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool HasBoxCollider => boxCollider;
        public Vector2 OriginalColliderSize => boxCollider.size;
        public Vector2 OriginalColliderOffset => boxCollider.offset;
        public Vector2 OriginalColliderBoundsCenter => boxCollider.bounds.center;
        public Transform Transform => transform.Value;
        public RaycastHit2D CurrentRightRaycast => currentRightRaycast.Value;
        public RaycastHit2D CurrentLeftRaycast => currentLeftRaycast.Value;
        public RaycastHit2D CurrentDownRaycast => currentDownRaycast.Value;
        public RaycastHit2D CurrentUpRaycast => currentUpRaycast.Value;
        public RaycastHit2D RaycastDownHitAt { get; set; }

        public RaycastHit2D RaycastDownHitAtRef
        {
            set => value = raycastDownHitAt.Value;
        }

        public RaycastHit2D RaycastUpHitAt { get; set; }

        public RaycastHit2D RaycastUpHitAtRef
        {
            set => value = raycastUpHitAt.Value;
        }

        public Vector2 VerticalRaycastFromLeft => verticalRaycastFromLeft.Value;
        public Vector2 VerticalRaycastToRight => verticalRaycastToRight.Value;
        public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;
        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public Vector2 RightRaycastFromBottomOrigin => rightRaycastFromBottomOrigin.Value;
        public Vector2 RightRaycastToTopOrigin => rightRaycastToTopOrigin.Value;
        public Vector2 LeftRaycastFromBottomOrigin => leftRaycastFromBottomOrigin.Value;
        public Vector2 LeftRaycastToTopOrigin => leftRaycastToTopOrigin.Value;

        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public RaycastHitColliderContactList ContactList => contactList;
        public readonly RaycastHitColliderState state = new RaycastHitColliderState();

        public bool IsCollidingAboveRef
        {
            set => value = isCollidingAbove.Value;
        }

        public bool IsCollidingBelowRef
        {
            set => value = isCollidingBelow.Value;
        }

        public bool IsCollidingLeftRef
        {
            set => value = isCollidingLeft.Value;
        }

        public bool IsCollidingRightRef
        {
            set => value = isCollidingRight.Value;
        }

        public int UpHitsStorageCollidingIndex { get; set; }

        public int UpHitsStorageCollidingIndexRef
        {
            set => value = upHitsStorageCollidingIndex.Value;
        }

        public bool UpHitConnected { get; set; }

        public bool UpHitConnectedRef
        {
            set => value = upHitConnected.Value;
        }

        public float MovingPlatformCurrentGravity { get; set; }
        public float MovingPlatformGravity { get; } = -500f;

        public Vector2 BoxColliderSizeRef
        {
            set => value = boxColliderSize.Value;
        }

        public Vector2 BoxColliderOffsetRef
        {
            set => value = boxColliderOffset.Value;
        }

        public Vector2 BoxColliderBoundsCenterRef
        {
            set => value = boxColliderBoundsCenter.Value;
        }

        public RaycastHit2D[] UpHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] RightHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] DownHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] LeftHitsStorage { get; set; } = new RaycastHit2D[0];
        public int CurrentRightHitsStorageIndex { get; set; } = 0;

        public int CurrentRightHitsStorageIndexRef
        {
            set => value = currentRightHitsStorageIndex.Value;
        }

        public int CurrentLeftHitsStorageIndex { get; set; } = 0;

        public int CurrentLeftHitsStorageIndexRef
        {
            set => value = currentLeftHitsStorageIndex.Value;
        }

        public int CurrentDownHitsStorageIndex { get; set; } = 0;

        public int CurrentDownHitsStorageIndexRef
        {
            set => value = currentDownHitsStorageIndex.Value;
        }

        public int CurrentUpHitsStorageIndex { get; set; } = 0;

        public int CurrentUpHitsStorageIndexRef
        {
            set => value = currentUpHitsStorageIndex.Value;
        }

        public float CurrentRightHitDistance { get; set; }

        public float CurrentRightHitDistanceRef
        {
            set => value = currentRightHitDistance.Value;
        }

        public float CurrentLeftHitDistance { get; set; }

        public float CurrentLeftHitDistanceRef
        {
            set => value = currentLeftHitDistance.Value;
        }

        public float CurrentDownHitSmallestDistance { get; set; }

        public float CurrentDownHitSmallestDistanceRef
        {
            set => value = currentDownHitSmallestDistance.Value;
        }

        public Collider2D CurrentRightHitCollider { get; set; }
        public Collider2D CurrentLeftHitCollider { get; set; }

        public Collider2D CurrentRightHitColliderRef
        {
            set => value = currentRightHitCollider.Value;
        }

        public Collider2D CurrentLeftHitColliderRef
        {
            set => value = currentLeftHitCollider.Value;
        }

        public Collider2D IgnoredCollider { get; set; }

        public Collider2D IgnoredColliderRef
        {
            set => value = ignoredCollider.Value;
        }

        public float CurrentRightHitAngle { get; set; }
        public float CurrentLeftHitAngle { get; set; }

        public float CurrentRightHitAngleRef
        {
            set => value = currentRightHitAngle.Value;
        }

        public float CurrentLeftHitAngleRef
        {
            set => value = currentLeftHitAngle.Value;
        }

        public bool IsGroundedRef
        {
            set => value = isGrounded.Value;
        }

        public bool OnMovingPlatformRef
        {
            set => value = onMovingPlatform.Value;
        }

        public GameObject StandingOnLastFrameRef
        {
            set => value = standingOnLastFrame.Value;
        }

        public bool IsStandingOnLastFrame { get; set; }

        public bool IsStandingOnLastFrameRef
        {
            set => value = isStandingOnLastFrame.Value;
        }

        public Collider2D StandingOnColliderRef
        {
            set => value = standingOnCollider.Value;
        }

        public Vector2 ColliderBottomCenterPosition
        {
            get
            {
                var position = new Vector2();
                var bounds = boxCollider.bounds;
                position.x = bounds.center.x;
                position.y = bounds.min.y;
                return position;
            }
        }

        public Vector2 ColliderBottomCenterPositionRef
        {
            set => value = colliderBottomCenterPosition.Value;
        }

        public int DownHitsStorageSmallestDistanceIndex { get; set; }

        public int DownHitsStorageSmallestDistanceIndexRef
        {
            set => value = downHitsStorageSmallestDistanceIndex.Value;
        }

        public bool DownHitConnected { get; set; }

        public bool DownHitConnectedRef
        {
            set => value = downHitConnected.Value;
        }

        public Vector3 CrossBelowSlopeAngleRef
        {
            set => value = crossBelowSlopeAngle.Value;
        }

        public RaycastHit2D DownHitWithSmallestDistance { get; set; }

        public RaycastHit2D DownHitWithSmallestDistanceRef
        {
            set => value = downHitWithSmallestDistance.Value;
        }

        public GameObject StandingOnWithSmallestDistance => DownHitWithSmallestDistance.collider.gameObject;

        public GameObject StandingOnWithSmallestDistanceRef
        {
            set => value = standingOnWithSmallestDistance.Value;
        }

        public Collider2D StandingOnWithSmallestDistanceCollider => DownHitWithSmallestDistance.collider;

        public Collider2D StandingOnWithSmallestDistanceColliderRef
        {
            set => value = standingOnWithSmallestDistanceCollider.Value;
        }

        public LayerMask StandingOnWithSmallestDistanceLayer => DownHitWithSmallestDistance.collider.gameObject.layer;

        public LayerMask StandingOnWithSmallestDistanceLayerRef
        {
            set => value = standingOnWithSmallestDistanceLayer.Value;
        }

        public Vector2 StandingOnWithSmallestDistancePoint => DownHitWithSmallestDistance.point;

        public Vector2 StandingOnWithSmallestDistancePointRef
        {
            set => value = standingOnWithSmallestDistancePoint.Value;
        }

        [CanBeNull] public PhysicsMaterialData PhysicsMaterialClosestToDownHit { get; set; }
        [CanBeNull] public PathMovementData PathMovementClosestToDownHit { get; set; }
        public bool HasPathMovementClosestToDownHit => PathMovementClosestToDownHit != null;

        public bool HasPathMovementClosestToDownHitRef
        {
            set => value = hasPathMovementData.Value;
        }

        public bool HasPhysicsMaterialClosestToDownHit => PhysicsMaterialClosestToDownHit != null;

        public bool HasPhysicsMaterialClosestToDownHitRef
        {
            set => value = hasPhysicsMaterialData.Value;
        }

        public float Friction { get; set; }

        public float FrictionRef
        {
            set => value = friction.Value;
        }

        public PathMovementData MovingPlatform { get; set; }
        public bool HasMovingPlatform { get; set; }

        public bool HasMovingPlatformRef
        {
            set => value = hasMovingPlatform.Value;
        }

        public float DistanceBetweenRightHitAndRaycastOrigin { get; set; }

        public float DistanceBetweenRightHitAndRaycastOriginRef
        {
            set => value = distanceBetweenRightHitAndRaycastOrigin.Value;
        }

        public float DistanceBetweenLeftHitAndRaycastOrigin { get; set; }

        public float DistanceBetweenLeftHitAndRaycastOriginRef
        {
            set => value = distanceBetweenLeftHitAndRaycastOrigin.Value;
        }
    }
}
using JetBrains.Annotations;
using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
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
        //[SerializeField] private RaycastHit2DReference currentRightRaycast;
        [SerializeField] private RaycastHit2DReference currentLeftRaycast;
        //[SerializeField] private RaycastHit2DReference currentDownRaycast;

        //[SerializeField] private RaycastHit2DReference currentUpRaycast;
        //[SerializeField] private IntReference numberOfVerticalRaysPerSide;
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        //[SerializeField] private Vector2Reference verticalRaycastFromLeft;
        //[SerializeField] private Vector2Reference verticalRaycastToRight;
        //[SerializeField] private Vector2Reference rightRaycastFromBottomOrigin;
        //[SerializeField] private Vector2Reference rightRaycastToTopOrigin;
        [SerializeField] private Vector2Reference leftRaycastFromBottomOrigin;
        [SerializeField] private Vector2Reference leftRaycastToTopOrigin;
        //[SerializeField] private IntReference savedBelowLayer;

        /* fields */
        [SerializeField] private RaycastHitColliderContactList contactList;
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private Vector2Reference boxColliderBoundsCenter;
        //[SerializeField] private IntReference currentRightHitsStorageIndex;
        [SerializeField] private IntReference currentLeftHitsStorageIndex;
        //[SerializeField] private IntReference currentDownHitsStorageIndex;
        //[SerializeField] private IntReference currentUpHitsStorageIndex;
        //[SerializeField] private FloatReference currentRightHitDistance;
        [SerializeField] private Collider2DReference currentLeftHitCollider;
        [SerializeField] private FloatReference currentLeftHitDistance;
        [SerializeField] private FloatReference currentLeftHitAngle;
        //[SerializeField] private FloatReference currentDownHitSmallestDistance;
        //[SerializeField] private Collider2DReference currentRightHitCollider;
        [SerializeField] private Collider2DReference ignoredCollider;
        //[SerializeField] private FloatReference currentRightHitAngle;
        //[SerializeField] private BoolReference isGrounded;
        //[SerializeField] private FloatReference friction;
        //[SerializeField] private BoolReference onMovingPlatform;
        //[SerializeField] private GameObjectReference standingOnLastFrame;
        //[SerializeField] private BoolReference isStandingOnLastFrame;
        //[SerializeField] private Collider2DReference standingOnCollider;
        [SerializeField] private Vector2Reference colliderBottomCenterPosition;
        //[SerializeField] private IntReference downHitsStorageSmallestDistanceIndex;
        //[SerializeField] private BoolReference downHitConnected;
        //[SerializeField] private RaycastHit2DReference raycastDownHitAt;
        //[SerializeField] private Vector3Reference crossBelowSlopeAngle;
        //[SerializeField] private RaycastHit2DReference downHitWithSmallestDistance;
        //[SerializeField] private GameObjectReference standingOnWithSmallestDistance;
        //[SerializeField] private Collider2DReference standingOnWithSmallestDistanceCollider;
        //[SerializeField] private LayerMaskReference standingOnWithSmallestDistanceLayer;
        //[SerializeField] private Vector2Reference standingOnWithSmallestDistancePoint;
        //[SerializeField] private BoolReference hasPhysicsMaterialData;
        //[SerializeField] private BoolReference hasPathMovementData;
        //[SerializeField] private BoolReference hasMovingPlatform;
        //[SerializeField] private BoolReference upHitConnected;
        //[SerializeField] private IntReference upHitsStorageCollidingIndex;
        //[SerializeField] private RaycastHit2DReference raycastUpHitAt;
        //[SerializeField] private FloatReference distanceBetweenRightHitAndRaycastOrigin;
        [SerializeField] private FloatReference distanceBetweenLeftHitAndRaycastOrigin;
        //[SerializeField] private FloatReference belowSlopeAngle;
        //[SerializeField] private BoolReference isCollidingBelow;
        [SerializeField] private BoolReference isCollidingLeft;
        //[SerializeField] private BoolReference isCollidingRight;
        //[SerializeField] private BoolReference isCollidingAbove;
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength;
        [SerializeField] private RaycastHit2DReference distanceToGroundRaycast;
        [SerializeField] private FloatReference boundsHeight;
        private const string RhcPath = "Physics/Collider/RaycastHitCollider/";
        private static readonly string ModelAssetPath = $"{RhcPath}DefaultRaycastHitColliderModel.asset";

        /* properties: dependencies */
        //public int SavedBelowLayer => savedBelowLayer.Value;
        public float BoundsHeight => boundsHeight.Value;
        public RaycastHit2D DistanceToGroundRaycast => distanceToGroundRaycast.Value;
        public float DistanceToGroundRayMaximumLength => distanceToGroundRayMaximumLength.Value;
        //public float BelowSlopeAngle => state.BelowSlopeAngle;
        public bool HasSettings => settings;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool HasBoxCollider => boxCollider;
        public Vector2 OriginalColliderSize => boxCollider.size;
        public Vector2 OriginalColliderOffset => boxCollider.offset;
        public Vector2 OriginalColliderBoundsCenter => boxCollider.bounds.center;
        public Transform Transform => transform.Value;
        //public RaycastHit2D CurrentRightRaycast => currentRightRaycast.Value;
        public RaycastHit2D CurrentLeftRaycast => currentLeftRaycast.Value;
        //public RaycastHit2D CurrentDownRaycast => currentDownRaycast.Value;
        //public RaycastHit2D CurrentUpRaycast => currentUpRaycast.Value;
        //public RaycastHit2D RaycastDownHitAt { get; set; }
        //public RaycastHit2D RaycastUpHitAt { get; set; }
        //public Vector2 VerticalRaycastFromLeft => verticalRaycastFromLeft.Value;
        //public Vector2 VerticalRaycastToRight => verticalRaycastToRight.Value;
        //public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;
        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        //public Vector2 RightRaycastFromBottomOrigin => rightRaycastFromBottomOrigin.Value;
        //public Vector2 RightRaycastToTopOrigin => rightRaycastToTopOrigin.Value;
        public Vector2 LeftRaycastFromBottomOrigin => leftRaycastFromBottomOrigin.Value;
        public Vector2 LeftRaycastToTopOrigin => leftRaycastToTopOrigin.Value;

        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public RaycastHitColliderContactList ContactList => contactList;
        public readonly RaycastHitColliderState state = new RaycastHitColliderState();
        //public int UpHitsStorageCollidingIndex { get; set; }
        //public bool UpHitConnected { get; set; }
        //public float MovingPlatformCurrentGravity { get; set; }
        //public float MovingPlatformGravity { get; } = -500f;
        //public RaycastHit2D[] UpHitsStorage { get; set; } = new RaycastHit2D[0];
        //public RaycastHit2D[] RightHitsStorage { get; set; } = new RaycastHit2D[0];
        //public RaycastHit2D[] DownHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] LeftHitsStorage { get; set; } = new RaycastHit2D[0];
        //public int CurrentRightHitsStorageIndex { get; set; } = 0;
        public int CurrentLeftHitsStorageIndex { get; set; } = 0;
        //public int CurrentDownHitsStorageIndex { get; set; } = 0;
        //public int CurrentUpHitsStorageIndex { get; set; } = 0;
        //public float CurrentRightHitDistance { get; set; }
        public float CurrentLeftHitDistance { get; set; }
        //public float CurrentDownHitSmallestDistance { get; set; }
        //public Collider2D CurrentRightHitCollider { get; set; }
        public Collider2D CurrentLeftHitCollider { get; set; }
        public Collider2D IgnoredCollider { get; set; }
        //public float CurrentRightHitAngle { get; set; }
        public float CurrentLeftHitAngle { get; set; }
        
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

        //public int DownHitsStorageSmallestDistanceIndex { get; set; }
        //public bool DownHitConnected { get; set; }
        //public RaycastHit2D DownHitWithSmallestDistance { get; set; }
        //public GameObject StandingOnWithSmallestDistance => DownHitWithSmallestDistance.collider.gameObject;
        //public Collider2D StandingOnWithSmallestDistanceCollider => DownHitWithSmallestDistance.collider;
        //public LayerMask StandingOnWithSmallestDistanceLayer => DownHitWithSmallestDistance.collider.gameObject.layer;
        //public Vector2 StandingOnWithSmallestDistancePoint => DownHitWithSmallestDistance.point;
        //[CanBeNull] public PhysicsMaterialData PhysicsMaterialClosestToDownHit { get; set; }
        //[CanBeNull] public PathMovementData PathMovementClosestToDownHit { get; set; }
        //public bool HasPathMovementClosestToDownHit => PathMovementClosestToDownHit != null;
        //public bool HasPhysicsMaterialClosestToDownHit => PhysicsMaterialClosestToDownHit != null;
        //public float Friction { get; set; }
        //public PathMovementData MovingPlatform { get; set; }
        //public bool HasMovingPlatform { get; set; }
        //public float DistanceBetweenRightHitAndRaycastOrigin { get; set; }
        public float DistanceBetweenLeftHitAndRaycastOrigin { get; set; }
        public const string RaycastHitColliderPath = "Physics/Collider/RaycastHitCollider/";

        public static readonly string RaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
    }
}
using JetBrains.Annotations;
using ScriptableObjects.Atoms.Mask.References;
using ScriptableObjects.Atoms.Raycast;
using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Platformer.Physics.PhysicsMaterial;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;
    [InlineEditor]
    public class DownRaycastHitColliderData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private IntReference numberOfVerticalRaysPerSide = new IntReference();
        [SerializeField] private RaycastReference currentDownRaycast = new RaycastReference();
        [SerializeField] private new Transform transform = null;
        [SerializeField] private Vector2Reference downRaycastFromLeft = new Vector2Reference();
        [SerializeField] private Vector2Reference downRaycastToRight = new Vector2Reference();
        [SerializeField] private IntReference savedBelowLayer = new IntReference();

        #endregion

        [SerializeField] private FloatReference smallestDistanceToDownHit = new FloatReference();
        [SerializeField] private FloatReference movingPlatformCurrentGravity = new FloatReference();
        [SerializeField] private BoolReference hasPhysicsMaterialClosestToDownHit = new BoolReference();
        [SerializeField] private BoolReference hasPathMovementClosestToDownHit = new BoolReference();
        [SerializeField] private IntReference downHitsStorageLength = new IntReference();
        [SerializeField] private IntReference currentDownHitsStorageIndex = new IntReference();
        [SerializeField] private BoolReference downHitConnected = new BoolReference();
        [SerializeField] private RaycastReference raycastDownHitAt = new RaycastReference();
        [SerializeField] private Vector3Reference crossBelowSlopeAngle = new Vector3Reference();
        [SerializeField] private BoolReference isCollidingBelow = new BoolReference();
        [SerializeField] private BoolReference onMovingPlatform = new BoolReference();
        [SerializeField] private BoolReference hasMovingPlatform = new BoolReference();
        [SerializeField] private FloatReference currentDownHitSmallestDistance = new FloatReference();
        [SerializeField] private Collider2DReference standingOnCollider = new Collider2DReference();
        [SerializeField] private GameObjectReference standingOnLastFrame = new GameObjectReference();
        [SerializeField] private BoolReference hasStandingOnLastFrame = new BoolReference();
        [SerializeField] private BoolReference wasGroundedLastFrame = new BoolReference();
        [SerializeField] private BoolReference groundedEvent = new BoolReference();
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed = new Vector2Reference();
        [SerializeField] private MaskReference standingOnWithSmallestDistanceLayer = new MaskReference();
        private static readonly string DownRaycastHitColliderPath = $"{RaycastHitColliderPath}DownRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{DownRaycastHitColliderPath}DownRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;
        public RaycastHit2D CurrentDownRaycast => currentDownRaycast.Value.hit2D;
        public Vector2 DownRaycastFromLeft => downRaycastFromLeft.Value;
        public Vector2 DownRaycastToRight => downRaycastToRight.Value;
        public int SavedBelowLayer => savedBelowLayer.Value;
        public Transform Transform => transform;

        #endregion

        public float SmallestDistanceToDownHit
        {
            set => value = smallestDistanceToDownHit.Value;
        }

        public bool HasPhysicsMaterialClosestToDownHit
        {
            set => value = hasPhysicsMaterialClosestToDownHit.Value;
        }

        public bool HasPathMovementClosestToDownHit
        {
            set => value = hasPathMovementClosestToDownHit.Value;
        }

        [HideInInspector] [CanBeNull] public PhysicsMaterialData physicsMaterialClosestToDownHit;
        [HideInInspector] [CanBeNull] public PathMovementData pathMovementClosestToDownHit;

        public Vector2 MovingPlatformCurrentSpeed
        {
            set => value = movingPlatformCurrentSpeed.Value;
        }

        public int DownHitsStorageLength
        {
            set => value = downHitsStorageLength.Value;
        }

        public RaycastHit2D[] DownHitsStorage { get; set; } = new RaycastHit2D[0];

        public int CurrentDownHitsStorageIndex
        {
            get => currentDownHitsStorageIndex.Value;
            set => value = currentDownHitsStorageIndex.Value;
        }

        [HideInInspector] public float friction;
        public int DownHitsStorageSmallestDistanceIndex { get; set; }
        public RaycastHit2D DownHitWithSmallestDistance { get; set; }

        public bool DownHitConnected
        {
            set => value = downHitConnected.Value;
        }

        public Raycast RaycastDownHitAt
        {
            get => raycastDownHitAt.Value;
            set => value = raycastDownHitAt.Value;
        }

        [HideInInspector] public float belowSlopeAngle;

        public Vector3 CrossBelowSlopeAngle
        {
            get => crossBelowSlopeAngle.Value;
            set => value = crossBelowSlopeAngle.Value;
        }

        public bool IsCollidingBelow
        {
            get => isCollidingBelow.Value;
            set => value = isCollidingBelow.Value;
        }

        public bool OnMovingPlatform
        {
            set => value = onMovingPlatform.Value;
        }

        public PathMovementData MovingPlatform { get; set; }

        public bool HasMovingPlatform
        {
            set => value = hasMovingPlatform.Value;
        }

        public float MovingPlatformCurrentGravity
        {
            set => value = movingPlatformCurrentGravity.Value;
        }

        [HideInInspector] public float movingPlatformGravity = -500f;

        public float CurrentDownHitSmallestDistance
        {
            set => value = currentDownHitSmallestDistance.Value;
        }

        public bool GroundedEvent
        {
            set => value = groundedEvent.Value;
        }

        public GameObject StandingOnLastFrame
        {
            get => standingOnLastFrame.Value;
            set => value = standingOnLastFrame.Value;
        }

        public bool HasStandingOnLastFrame
        {
            set => value = hasStandingOnLastFrame.Value;
        }

        [HideInInspector] public GameObject standingOn;

        public Collider2D StandingOnCollider
        {
            set => value = standingOnCollider.Value;
        }

        public bool WasGroundedLastFrame
        {
            set => value = wasGroundedLastFrame.Value;
        }

        public LayerMask StandingOnWithSmallestDistanceLayer
        {
            set => value = standingOnWithSmallestDistanceLayer.Value.layer;
        }

        [HideInInspector] public GameObject standingOnWithSmallestDistance;

        public static readonly string DownRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
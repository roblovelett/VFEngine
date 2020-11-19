using JetBrains.Annotations;
using ScriptableObjectArchitecture;
using ScriptableObjects.Variables;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Platformer.Physics.PhysicsMaterial;
using VFEngine.Tools;
using Collision = ScriptableObjects.Variables.Collision;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "DownRaycastHitColliderData", menuName = PlatformerDownRaycastHitColliderDataPath,
        order = 0)]
    [InlineEditor]
    public class DownRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObjectReference character = null;

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
        [SerializeField] private CollisionReference standingOnCollider = new CollisionReference();
        [SerializeField] private GameObjectReference standingOnLastFrame = new GameObjectReference();
        [SerializeField] private BoolReference hasStandingOnLastFrame = new BoolReference();
        [SerializeField] private BoolReference hasGroundedLastFrame = new BoolReference();
        [SerializeField] private BoolReference groundedEvent = new BoolReference();
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed = new Vector2Reference();
        [SerializeField] private LayerMaskReference standingOnWithSmallestDistanceLayer = new LayerMaskReference();
        private static readonly string DownRaycastHitColliderPath = $"{RaycastHitColliderPath}DownRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{DownRaycastHitColliderPath}DownRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character.Value;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public Transform Transform { get; set; }
        public int NumberOfVerticalRaysPerSide { get; set; }
        public int SavedBelowLayer { get; set; }
        public Vector2 DownRaycastFromLeft { get; set; }
        public Vector2 DownRaycastToRight { get; set; }
        public RaycastHit2D CurrentDownRaycastHit { get; set; }

        #endregion

        public float SmallestDistanceToDownHit
        {
            get => smallestDistanceToDownHit.Value;
            set => value = smallestDistanceToDownHit.Value;
        }

        public bool HasPhysicsMaterialClosestToDownHit
        {
            get => hasPhysicsMaterialClosestToDownHit.Value;
            set => value = hasPhysicsMaterialClosestToDownHit.Value;
        }

        public bool HasPathMovementClosestToDownHit
        {
            get => hasPathMovementClosestToDownHit.Value;
            set => value = hasPathMovementClosestToDownHit.Value;
        }

        [CanBeNull] public PhysicsMaterialData PhysicsMaterialClosestToDownHit { get; set; }
        [CanBeNull] public PathMovementData PathMovementClosestToDownHit { get; set; }

        public Vector2 MovingPlatformCurrentSpeed
        {
            get => movingPlatformCurrentSpeed.Value;
            set => value = movingPlatformCurrentSpeed.Value;
        }

        public int DownHitsStorageLength
        {
            get => downHitsStorageLength.Value;
            set => value = downHitsStorageLength.Value;
        }

        public RaycastHit2D[] DownHitsStorage { get; set; } = new RaycastHit2D[0];

        public int CurrentDownHitsStorageIndex
        {
            get => currentDownHitsStorageIndex.Value;
            set => value = currentDownHitsStorageIndex.Value;
        }

        public float Friction { get; set; }
        public int DownHitsStorageSmallestDistanceIndex { get; set; }
        public RaycastHit2D DownHitWithSmallestDistance { get; set; }

        public bool DownHitConnected
        {
            get => downHitConnected.Value;
            set => value = downHitConnected.Value;
        }

        public RaycastHit2D RaycastDownHitAt
        {
            get => raycastDownHitAt.Value.hit2D;
            set => raycastDownHitAt.Value = new Raycast(value);
        }

        public float BelowSlopeAngle { get; set; }

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
            get => onMovingPlatform.Value;
            set => value = onMovingPlatform.Value;
        }

        public PathMovementData MovingPlatform { get; set; }

        public bool HasMovingPlatform
        {
            get => hasMovingPlatform.Value;
            set => value = hasMovingPlatform.Value;
        }

        public float MovingPlatformCurrentGravity
        {
            get => movingPlatformCurrentGravity.Value;
            set => value = movingPlatformCurrentGravity.Value;
        }

        public float MovingPlatformGravity { get; } = -500f;

        public float CurrentDownHitSmallestDistance
        {
            get => currentDownHitSmallestDistance.Value;
            set => value = currentDownHitSmallestDistance.Value;
        }

        public bool GroundedEvent
        {
            get => groundedEvent.Value;
            set => value = groundedEvent.Value;
        }

        public GameObject StandingOnLastFrame
        {
            get => standingOnLastFrame.Value;
            set => value = standingOnLastFrame.Value;
        }

        public bool HasStandingOnLastFrame
        {
            get => hasStandingOnLastFrame.Value;
            set => value = hasStandingOnLastFrame.Value;
        }

        public GameObject StandingOn { get; set; }

        public Collider2D StandingOnCollider
        {
            get => standingOnCollider.Value.collider2D;
            set => standingOnCollider.Value = new Collision(value);
        }

        public bool HasGroundedLastFrame
        {
            get => hasGroundedLastFrame.Value;
            set => value = hasGroundedLastFrame.Value;
        }

        public LayerMask StandingOnWithSmallestDistanceLayer
        {
            get => standingOnWithSmallestDistanceLayer.Value;
            set => value = standingOnWithSmallestDistanceLayer.Value;
        }

        public GameObject StandingOnWithSmallestDistance { get; set; }

        public static readonly string DownRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
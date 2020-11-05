using JetBrains.Annotations;
using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Platformer.Physics.PhysicsMaterial;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class DownRaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private IntReference numberOfVerticalRaysPerSide;
        [SerializeField] private RaycastHit2DReference currentDownRaycast;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference downRaycastFromLeft;
        [SerializeField] private Vector2Reference downRaycastToRight;
        [SerializeField] private LayerMaskReference savedBelowLayer;

        #endregion

        [SerializeField] private BoolReference hasPhysicsMaterialClosestToDownHit;
        [SerializeField] private BoolReference hasPathMovementClosestToDownHit;
        [SerializeField] private IntReference downHitsStorageLength;
        [SerializeField] private BoolReference downHitConnected;
        [SerializeField] private RaycastHit2DReference raycastDownHitAt;
        [SerializeField] private Vector3Reference crossBelowSlopeAngle;
        [SerializeField] private BoolReference isCollidingBelow;
        [SerializeField] private BoolReference onMovingPlatform;
        [SerializeField] private BoolReference hasMovingPlatform;
        [SerializeField] private FloatReference currentDownHitSmallestDistance;
        [SerializeField] private Collider2DReference standingOnCollider;
        [SerializeField] private GameObjectReference standingOnLastFrame;
        [SerializeField] private BoolReference hasStandingOnLastFrame;
        [SerializeField] private BoolReference wasGroundedLastFrame;
        private static readonly string DownRaycastHitColliderPath = $"{RaycastHitColliderPath}DownRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{DownRaycastHitColliderPath}DefaultDownRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;
        public RaycastHit2D CurrentDownRaycast => currentDownRaycast.Value;
        public Vector2 DownRaycastFromLeft => downRaycastFromLeft.Value;
        public Vector2 DownRaycastToRight => downRaycastToRight.Value;
        public LayerMask SavedBelowLayer => savedBelowLayer.Value;
        public Transform Transform => transform.Value;

        #endregion

        public bool HasPhysicsMaterialClosestToDownHit
        {
            set => value = hasPhysicsMaterialClosestToDownHit.Value;
        }

        public bool HasPathMovementClosestToDownHit
        {
            set => value = hasPathMovementClosestToDownHit.Value;
        }

        [CanBeNull] public PhysicsMaterialData physicsMaterialClosestToDownHit;
        [CanBeNull] public PathMovementData pathMovementClosestToDownHit;

        public int DownHitsStorageLength
        {
            set => value = downHitsStorageLength.Value;
        }

        public RaycastHit2D[] DownHitsStorage { get; set; } = new RaycastHit2D[0];
        public int CurrentDownHitsStorageIndex { get; set; }
        public float friction;
        public int DownHitsStorageSmallestDistanceIndex { get; set; }
        public RaycastHit2D DownHitWithSmallestDistance { get; set; }

        public bool DownHitConnected
        {
            set => value = downHitConnected.Value;
        }

        public RaycastHit2D RaycastDownHitAt
        {
            set => value = raycastDownHitAt.Value;
        }

        public float belowSlopeAngle;

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

        public float movingPlatformCurrentGravity;
        public float movingPlatformGravity = -500f;

        public float CurrentDownHitSmallestDistance
        {
            set => value = currentDownHitSmallestDistance.Value;
        }

        public bool groundedEvent;

        public GameObject StandingOnLastFrame
        {
            get => standingOnLastFrame.Value;
            set => value = standingOnLastFrame.Value;
        }

        public bool HasStandingOnLastFrame
        {
            set => value = hasStandingOnLastFrame.Value;
        }

        public GameObject standingOn;

        public Collider2D StandingOnCollider
        {
            set => value = standingOnCollider.Value;
        }

        public bool WasGroundedLastFrame
        {
            set => value = wasGroundedLastFrame.Value;
        }

        public GameObject standingOnWithSmallestDistance;

        public static readonly string DownRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
//using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class LeftRaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        //[SerializeField] private RaycastHit2DReference currentLeftRaycast;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference leftRaycastFromBottomOrigin;
        [SerializeField] private Vector2Reference leftRaycastToTopOrigin;

        #endregion

        [SerializeField] private IntReference leftHitsStorageLength;
        [SerializeField] private FloatReference currentLeftHitAngle;
        [SerializeField] private FloatReference currentLeftHitDistance;
        [SerializeField] private FloatReference distanceBetweenLeftHitAndRaycastOrigin;
        [SerializeField] private BoolReference leftHitConnected;
        [SerializeField] private BoolReference isCollidingLeft;
        [SerializeField] private Collider2DReference currentLeftHitCollider;
        private static readonly string LeftRaycastHitColliderPath = $"{RaycastHitColliderPath}LeftRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{LeftRaycastHitColliderPath}DefaultLeftRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        //public RaycastHit2D CurrentLeftRaycast => currentLeftRaycast.Value;
        public Transform Transform => transform.Value;
        public Vector2 LeftRaycastFromBottomOrigin => leftRaycastFromBottomOrigin.Value;
        public Vector2 LeftRaycastToTopOrigin => leftRaycastToTopOrigin.Value;

        #endregion

        public RaycastHit2D[] LeftHitsStorage { get; set; } = new RaycastHit2D[0];

        public int LeftHitsStorageLength
        {
            set => value = leftHitsStorageLength.Value;
        }

        public int CurrentLeftHitsStorageIndex { get; set; }

        public float CurrentLeftHitAngle
        {
            get => currentLeftHitAngle.Value;
            set => value = currentLeftHitAngle.Value;
        }

        public float CurrentLeftHitDistance
        {
            set => value = currentLeftHitDistance.Value;
        }

        public float DistanceBetweenLeftHitAndRaycastOrigin
        {
            set => value = distanceBetweenLeftHitAndRaycastOrigin.Value;
        }

        public bool LeftHitConnected
        {
            set => value = leftHitConnected.Value;
        }

        public bool IsCollidingLeft
        {
            set => value = isCollidingLeft.Value;
        }

        public bool passedLeftSlopeAngle;

        public Collider2D CurrentLeftHitCollider
        {
            get => currentLeftHitCollider.Value;
            set => value = currentLeftHitCollider.Value;
        }

        public GameObject currentLeftWallCollider;
        public float distanceToLeftCollider;
        public float leftLateralSlopeAngle;

        public static readonly string LeftRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [InlineEditor]
    public class LeftRaycastHitColliderData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private IntReference numberOfHorizontalRaysPerSide = new IntReference();
        [SerializeField] private RaycastReference currentLeftRaycast = new RaycastReference();
        [SerializeField] private new Transform transform = null;
        [SerializeField] private Vector2Reference leftRaycastFromBottomOrigin = new Vector2Reference();
        [SerializeField] private Vector2Reference leftRaycastToTopOrigin = new Vector2Reference();

        #endregion

        [SerializeField] private IntReference leftHitsStorageLength = new IntReference();
        [SerializeField] private IntReference currentLeftHitsStorageIndex = new IntReference();
        [SerializeField] private FloatReference currentLeftHitAngle = new FloatReference();
        [SerializeField] private FloatReference currentLeftHitDistance = new FloatReference();
        [SerializeField] private FloatReference distanceBetweenLeftHitAndRaycastOrigin = new FloatReference();
        [SerializeField] private BoolReference leftHitConnected = new BoolReference();
        [SerializeField] private BoolReference isCollidingLeft = new BoolReference();
        [SerializeField] private Collider2DReference currentLeftHitCollider = new Collider2DReference();
        private static readonly string LeftRaycastHitColliderPath = $"{RaycastHitColliderPath}LeftRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{LeftRaycastHitColliderPath}LeftRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public RaycastHit2D CurrentLeftRaycast => currentLeftRaycast.Value.hit2D;
        public Transform Transform => transform;
        public Vector2 LeftRaycastFromBottomOrigin => leftRaycastFromBottomOrigin.Value;
        public Vector2 LeftRaycastToTopOrigin => leftRaycastToTopOrigin.Value;

        #endregion

        public RaycastHit2D[] LeftHitsStorage { get; set; } = new RaycastHit2D[0];

        public int LeftHitsStorageLength
        {
            set => value = leftHitsStorageLength.Value;
        }

        public int CurrentLeftHitsStorageIndex
        {
            get => currentLeftHitsStorageIndex.Value;
            set => value = currentLeftHitsStorageIndex.Value;
        }

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

        [HideInInspector] public bool passedLeftSlopeAngle;

        public Collider2D CurrentLeftHitCollider
        {
            get => currentLeftHitCollider.Value;
            set => value = currentLeftHitCollider.Value;
        }

        [HideInInspector] public GameObject currentLeftWallCollider;
        [HideInInspector] public float distanceToLeftCollider;
        [HideInInspector] public float leftLateralSlopeAngle;

        public static readonly string LeftRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
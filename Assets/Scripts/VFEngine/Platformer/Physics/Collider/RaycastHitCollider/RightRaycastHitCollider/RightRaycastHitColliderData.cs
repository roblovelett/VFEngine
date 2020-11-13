using ScriptableObjects.Atoms.Raycast.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;
// ReSharper disable RedundantAssignment

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class RightRaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private IntReference numberOfHorizontalRaysPerSide = new IntReference();
        [SerializeField] private RaycastReference currentRightRaycast = new RaycastReference();
        [SerializeField] private new TransformReference transform = new TransformReference();
        [SerializeField] private Vector2Reference rightRaycastFromBottomOrigin = new Vector2Reference();
        [SerializeField] private Vector2Reference rightRaycastToTopOrigin = new Vector2Reference();

        #endregion

        [SerializeField] private IntReference rightHitsStorageLength = new IntReference();
        [SerializeField] private IntReference currentRightHitsStorageIndex = new IntReference();
        [SerializeField] private FloatReference currentRightHitAngle = new FloatReference();
        [SerializeField] private FloatReference currentRightHitDistance = new FloatReference();
        [SerializeField] private FloatReference distanceBetweenRightHitAndRaycastOrigin = new FloatReference();
        [SerializeField] private BoolReference rightHitConnected = new BoolReference();
        [SerializeField] private BoolReference isCollidingRight = new BoolReference();
        [SerializeField] private Collider2DReference currentRightHitCollider = new Collider2DReference();

        private static readonly string RightRaycastHitColliderPath =
            $"{RaycastHitColliderPath}RightRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{RightRaycastHitColliderPath}DefaultRightRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;

        public RaycastHit2D CurrentRightRaycast
        {
            get
            {
                var r = currentRightRaycast.Value;
                return r.hit2D;
            }
        }
        public Transform Transform => transform.Value;
        public Vector2 RightRaycastFromBottomOrigin => rightRaycastFromBottomOrigin.Value;
        public Vector2 RightRaycastToTopOrigin => rightRaycastToTopOrigin.Value;

        #endregion

        public RaycastHit2D[] RightHitsStorage { get; set; } = new RaycastHit2D[0];

        public int RightHitsStorageLength
        {
            set => value = rightHitsStorageLength.Value;
        }

        public int CurrentRightHitsStorageIndex
        {
            get => currentRightHitsStorageIndex.Value;
            set => value = currentRightHitsStorageIndex.Value;
        }

        public float CurrentRightHitAngle
        {
            get => currentRightHitAngle.Value;
            set => value = currentRightHitAngle.Value;
        }

        public float CurrentRightHitDistance
        {
            set => value = currentRightHitDistance.Value;
        }

        public float DistanceBetweenRightHitAndRaycastOrigin
        {
            set => value = distanceBetweenRightHitAndRaycastOrigin.Value;
        }

        public bool RightHitConnected
        {
            set => value = rightHitConnected.Value;
        }

        public bool IsCollidingRight
        {
            set => value = isCollidingRight.Value;
        }

        [HideInInspector] public bool passedRightSlopeAngle;

        public Collider2D CurrentRightHitCollider
        {
            get => currentRightHitCollider.Value;
            set => value = currentRightHitCollider.Value;
        }

        [HideInInspector] public GameObject currentRightWallCollider;
        [HideInInspector] public float distanceToRightCollider;
        [HideInInspector] public float rightLateralSlopeAngle;

        public static readonly string RightRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
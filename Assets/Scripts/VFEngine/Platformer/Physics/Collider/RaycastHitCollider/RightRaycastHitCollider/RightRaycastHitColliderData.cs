using ScriptableObjects.Atoms.RaycastHit2D.References;
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

        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        [SerializeField] private RaycastHit2DReference currentRightRaycast;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference rightRaycastFromBottomOrigin;
        [SerializeField] private Vector2Reference rightRaycastToTopOrigin;

        #endregion

        [SerializeField] private IntReference rightHitsStorageLength;
        [SerializeField] private FloatReference currentRightHitAngle;
        [SerializeField] private FloatReference currentRightHitDistance;
        [SerializeField] private FloatReference distanceBetweenRightHitAndRaycastOrigin;
        [SerializeField] private BoolReference rightHitConnected;
        [SerializeField] private BoolReference isCollidingRight;
        [SerializeField] private Collider2DReference currentRightHitCollider;

        private static readonly string RightRaycastHitColliderPath =
            $"{RaycastHitColliderPath}RightRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{RightRaycastHitColliderPath}DefaultUpRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public RaycastHit2D CurrentRightRaycast => currentRightRaycast.Value;
        public Transform Transform => transform.Value;
        public Vector2 RightRaycastFromBottomOrigin => rightRaycastFromBottomOrigin.Value;
        public Vector2 RightRaycastToTopOrigin => rightRaycastToTopOrigin.Value;

        #endregion

        public RaycastHit2D[] RightHitsStorage { get; set; } = new RaycastHit2D[0];

        public int RightHitsStorageLength
        {
            set => value = rightHitsStorageLength.Value;
        }

        public int CurrentRightHitsStorageIndex { get; set; }

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

        public bool passedRightSlopeAngle;

        public Collider2D CurrentRightHitCollider
        {
            get => currentRightHitCollider.Value;
            set => value = currentRightHitCollider.Value;
        }

        public GameObject currentRightWallCollider;
        public float distanceToRightCollider;
        public float rightLateralSlopeAngle;

        public static readonly string RightRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
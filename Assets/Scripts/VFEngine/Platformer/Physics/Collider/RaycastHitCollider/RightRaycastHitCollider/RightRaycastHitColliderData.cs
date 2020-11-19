using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using Collision = ScriptableObjects.Variables.Collision;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment
// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RightRaycastHitColliderData", menuName = PlatformerRightRaycastHitColliderDataPath,
        order = 0)]
    [InlineEditor]
    public class RightRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;

        #endregion

        [SerializeField] private IntReference rightHitsStorageLength = new IntReference();
        [SerializeField] private IntReference currentRightHitsStorageIndex = new IntReference();
        [SerializeField] private FloatReference currentRightHitAngle = new FloatReference();
        [SerializeField] private FloatReference currentRightHitDistance = new FloatReference();
        [SerializeField] private FloatReference distanceBetweenRightHitAndRaycastOrigin = new FloatReference();
        [SerializeField] private BoolReference rightHitConnected = new BoolReference();
        [SerializeField] private BoolReference isCollidingRight = new BoolReference();
        [SerializeField] private CollisionReference currentRightHitCollider = new CollisionReference();

        private static readonly string RightRaycastHitColliderPath =
            $"{RaycastHitColliderPath}RightRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{RightRaycastHitColliderPath}RightRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public RaycastHit2D CurrentRightRaycast { get; set; }
        public Vector2 RightRaycastFromBottomOrigin { get; set; }
        public Vector2 RightRaycastToTopOrigin { get; set; }

        #endregion

        public RaycastHit2D[] RightHitsStorage { get; set; } = new RaycastHit2D[0];

        public int RightHitsStorageLength
        {
            get => rightHitsStorageLength.Value;
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
            get => currentRightHitDistance.Value;
            set => value = currentRightHitDistance.Value;
        }

        public float DistanceBetweenRightHitAndRaycastOrigin
        {
            get => distanceBetweenRightHitAndRaycastOrigin.Value;
            set => value = distanceBetweenRightHitAndRaycastOrigin.Value;
        }

        public bool RightHitConnected
        {
            get => rightHitConnected.Value;
            set => value = rightHitConnected.Value;
        }

        public bool IsCollidingRight
        {
            get => isCollidingRight.Value;
            set => value = isCollidingRight.Value;
        }

        public bool PassedRightSlopeAngle { get; set; }

        public Collider2D CurrentRightHitCollider
        {
            get => currentRightHitCollider.Value.collider2D;
            set => currentRightHitCollider.Value = new Collision(value);
        }

        public GameObject CurrentRightWallCollider { get; set; }
        public float DistanceToRightCollider { get; set; }
        public float RightLateralSlopeAngle { get; set; }

        public static readonly string RightRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using Collision = ScriptableObjects.Variables.Collision;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LeftRaycastHitColliderData", menuName = PlatformerLeftRaycastHitColliderDataPath,
        order = 0)]
    [InlineEditor]
    public class LeftRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;

        #endregion

        [SerializeField] private IntReference leftHitsStorageLength = new IntReference();
        [SerializeField] private IntReference currentLeftHitsStorageIndex = new IntReference();
        [SerializeField] private FloatReference currentLeftHitAngle = new FloatReference();
        [SerializeField] private FloatReference currentLeftHitDistance = new FloatReference();
        [SerializeField] private FloatReference distanceBetweenLeftHitAndRaycastOrigin = new FloatReference();
        [SerializeField] private BoolReference leftHitConnected = new BoolReference();
        [SerializeField] private BoolReference isCollidingLeft = new BoolReference();
        [SerializeField] private CollisionReference currentLeftHitCollider = new CollisionReference();
        private static readonly string LeftRaycastHitColliderPath = $"{RaycastHitColliderPath}LeftRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{LeftRaycastHitColliderPath}LeftRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public RaycastHit2D CurrentLeftRaycastHit { get; set; }
        public Vector2 LeftRaycastFromBottomOrigin { get; set; }
        public Vector2 LeftRaycastToTopOrigin { get; set; }

        #endregion

        public RaycastHit2D[] LeftHitsStorage { get; set; } = new RaycastHit2D[0];

        public int LeftHitsStorageLength
        {
            get => leftHitsStorageLength.Value;
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
            get => currentLeftHitDistance.Value;
            set => value = currentLeftHitDistance.Value;
        }

        public float DistanceBetweenLeftHitAndRaycastOrigin
        {
            get => distanceBetweenLeftHitAndRaycastOrigin.Value;
            set => value = distanceBetweenLeftHitAndRaycastOrigin.Value;
        }

        public bool LeftHitConnected
        {
            get => leftHitConnected.Value;
            set => value = leftHitConnected.Value;
        }

        public bool IsCollidingLeft
        {
            get => isCollidingLeft.Value;
            set => value = isCollidingLeft.Value;
        }

        public bool PassedLeftSlopeAngle { get; set; }

        public Collider2D CurrentLeftHitCollider
        {
            get => currentLeftHitCollider.Value.collider2D;
            set => currentLeftHitCollider.Value = new Collision(value);
        }

        public GameObject CurrentLeftWallCollider { get; set; }
        public float DistanceToLeftCollider { get; set; }
        public float LeftLateralSlopeAngle { get; set; }

        public static readonly string LeftRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
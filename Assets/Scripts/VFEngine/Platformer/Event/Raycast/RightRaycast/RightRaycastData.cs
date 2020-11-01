using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class RightRaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference drawRaycastGizmos;
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        [SerializeField] private IntReference currentRightHitsStorageIndex;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private FloatReference obstacleHeightTolerance;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private Vector2Reference boundsBottomLeftCorner;
        [SerializeField] private Vector2Reference boundsBottomRightCorner;
        [SerializeField] private Vector2Reference boundsTopLeftCorner;
        [SerializeField] private Vector2Reference boundsTopRightCorner;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private LayerMaskReference platformMask;
        [SerializeField] private LayerMaskReference oneWayPlatformMask;
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask;

        #endregion

        [SerializeField] private FloatReference rightRayLength;
        [SerializeField] private Vector2Reference rightRaycastFromBottomOrigin;
        [SerializeField] private Vector2Reference rightRaycastToTopOrigin;
        [SerializeField] private RaycastHit2DReference currentRightRaycast;
        private static readonly string RightRaycastPath = $"{RaycastPath}RightRaycast/";
        private static readonly string ModelAssetPath = $"{RightRaycastPath}DefaultRightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool DrawRaycastGizmos => drawRaycastGizmos.Value;
        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public int CurrentRightHitsStorageIndex => currentRightHitsStorageIndex.Value;
        public float RayOffset => rayOffset.Value;
        public float ObstacleHeightTolerance => obstacleHeightTolerance.Value;
        public float BoundsWidth => boundsWidth.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Vector2 BoundsTopLeftCorner => boundsTopLeftCorner.Value;
        public Vector2 BoundsTopRightCorner => boundsTopRightCorner.Value;
        public Vector2 Speed => speed.Value;
        public Transform Transform => transform.Value;
        public LayerMask PlatformMask => platformMask.Value;
        public LayerMask OneWayPlatformMask => oneWayPlatformMask.Value;
        public LayerMask MovingOneWayPlatformMask => movingOneWayPlatformMask.Value;

        #endregion

        public float RightRayLength
        {
            get => rightRayLength.Value;
            set => value = rightRayLength.Value;
        }

        public Vector2 RightRaycastFromBottomOrigin
        {
            get => rightRaycastFromBottomOrigin.Value;
            set => value = rightRaycastFromBottomOrigin.Value;
        }

        public Vector2 RightRaycastToTopOrigin
        {
            get => rightRaycastToTopOrigin.Value;
            set => value = rightRaycastToTopOrigin.Value;
        }

        public Vector2 CurrentRightRaycastOrigin { get; set; } = Vector2.zero;

        public RaycastHit2D CurrentRightRaycast
        {
            set => value = currentRightRaycast.Value;
        }

        public static readonly string RightRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
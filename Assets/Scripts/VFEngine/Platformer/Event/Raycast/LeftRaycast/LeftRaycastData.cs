using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class LeftRaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference displayWarningsControl;
        [SerializeField] private BoolReference drawRaycastGizmosControl;
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        [SerializeField] private IntReference currentLeftHitsStorageIndex;
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

        [SerializeField] private FloatReference leftRayLength;
        [SerializeField] private Vector2Reference leftRaycastFromBottomOrigin;
        [SerializeField] private Vector2Reference leftRaycastToTopOrigin;
        [SerializeField] private RaycastHit2DReference currentLeftRaycast;
        private static readonly string LeftRaycastPath = $"{RaycastPath}RightRaycast/";
        private static readonly string ModelAssetPath = $"{LeftRaycastPath}DefaultLeftRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool DisplayWarningsControl => displayWarningsControl.Value;
        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public int CurrentLeftHitsStorageIndex => currentLeftHitsStorageIndex.Value;
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

        public float LeftRayLength
        {
            get => leftRayLength.Value;
            set => value = leftRayLength.Value;
        }

        public Vector2 LeftRaycastFromBottomOrigin
        {
            get => leftRaycastFromBottomOrigin.Value;
            set => value = leftRaycastFromBottomOrigin.Value;
        }

        public Vector2 LeftRaycastToTopOrigin
        {
            get => leftRaycastToTopOrigin.Value;
            set => value = leftRaycastToTopOrigin.Value;
        }

        public Vector2 CurrentLeftRaycastOrigin { get; set; } = Vector2.zero;

        public RaycastHit2D CurrentLeftRaycast
        {
            set => value = currentLeftRaycast.Value;
        }

        public static readonly string LeftRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
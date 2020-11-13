using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.Raycast.References;
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

        [SerializeField] private BoolReference drawRaycastGizmosControl = new BoolReference();
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide = new IntReference();
        [SerializeField] private IntReference currentRightHitsStorageIndex = new IntReference();
        [SerializeField] private FloatReference rayOffset = new FloatReference();
        [SerializeField] private FloatReference obstacleHeightTolerance = new FloatReference();
        [SerializeField] private FloatReference boundsWidth = new FloatReference();
        [SerializeField] private Vector2Reference boundsBottomLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomRightCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsTopLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsTopRightCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference speed = new Vector2Reference();
        [SerializeField] private new TransformReference transform = new TransformReference();
        [SerializeField] private LayerMaskReference platformMask = new LayerMaskReference();
        [SerializeField] private LayerMaskReference oneWayPlatformMask = new LayerMaskReference();
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask = new LayerMaskReference();

        #endregion

        [SerializeField] private FloatReference rightRayLength = new FloatReference();
        [SerializeField] private Vector2Reference rightRaycastFromBottomOrigin = new Vector2Reference();
        [SerializeField] private Vector2Reference rightRaycastToTopOrigin = new Vector2Reference();
        [SerializeField] private RaycastReference currentRightRaycast = new RaycastReference();
        private static readonly string RightRaycastPath = $"{RaycastPath}RightRaycast/";
        private static readonly string ModelAssetPath = $"{RightRaycastPath}DefaultRightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
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

        [HideInInspector] public Vector2 currentRightRaycastOrigin;

        public ScriptableObjects.Atoms.Raycast.Raycast CurrentRightRaycast
        {
            set => value = currentRightRaycast.Value;
        }

        public static readonly string RightRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;

    [InlineEditor]
    public class RaycastData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private RaycastSettings settings = null;
        [SerializeField] private BoxCollider2D boxCollider = null;

        #endregion

        [SerializeField] private BoolReference displayWarningsControl = new BoolReference();
        [SerializeField] private BoolReference drawRaycastGizmosControl = new BoolReference();
        [SerializeField] private BoolReference castRaysOnBothSides = new BoolReference();
        [SerializeField] private BoolReference performSafetyBoxcast = new BoolReference();
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength = new FloatReference();
        [SerializeField] private FloatReference boundsWidth = new FloatReference();
        [SerializeField] private FloatReference boundsHeight = new FloatReference();
        [SerializeField] private Vector2Reference bounds = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsCenter = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomRightCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomCenterPosition = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsTopLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsTopRightCorner = new Vector2Reference();
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide = new IntReference();
        [SerializeField] private IntReference numberOfVerticalRaysPerSide = new IntReference();
        [SerializeField] private FloatReference rayOffset = new FloatReference();
        [SerializeField] private FloatReference obstacleHeightTolerance = new FloatReference();
        private static readonly string ModelAssetPath = $"{RaycastPath}RaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public BoxCollider2D BoxCollider => boxCollider;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public bool HasSettings => settings;
        public bool HasBoxCollider => boxCollider;
        public bool DisplayWarningsControlSetting => settings.displayWarningsControl;
        public bool DrawRaycastGizmosControlSetting => settings.drawRaycastGizmosControl;
        public bool CastRaysOnBothSidesSetting => settings.castRaysOnBothSides;
        public bool PerformSafetyBoxcastSetting => settings.performSafetyBoxcast;
        public int NumberOfHorizontalRaysSetting => settings.numberOfHorizontalRays;
        public int NumberOfVerticalRaysSetting => settings.numberOfVerticalRays;
        public float RayOffsetSetting => settings.rayOffset;
        public float DistanceToGroundRayMaximumLengthSetting => settings.distanceToGroundRayMaximumLength;

        #endregion

        public bool DisplayWarningsControl
        {
            get => displayWarningsControl.Value;
            set => value = displayWarningsControl.Value;
        }

        public bool DrawRaycastGizmosControl
        {
            get => drawRaycastGizmosControl.Value;
            set => value = drawRaycastGizmosControl.Value;
        }

        public bool CastRaysOnBothSides
        {
            get => castRaysOnBothSides.Value;
            set => value = castRaysOnBothSides.Value;
        }

        public bool PerformSafetyBoxcast
        {
            get => performSafetyBoxcast.Value;
            set => value = performSafetyBoxcast.Value;
        }

        public float DistanceToGroundRayMaximumLength
        {
            get => distanceToGroundRayMaximumLength.Value;
            set => value = distanceToGroundRayMaximumLength.Value;
        }

        public int NumberOfHorizontalRays { get; set; }
        public int NumberOfVerticalRays { get; set; }

        public float ObstacleHeightTolerance
        {
            get => obstacleHeightTolerance.Value;
            set => value = obstacleHeightTolerance.Value;
        }

        public float RayOffset
        {
            get => rayOffset.Value;
            set => value = rayOffset.Value;
        }

        public float BoundsWidth
        {
            get => boundsWidth.Value;
            set => value = boundsWidth.Value;
        }

        public float BoundsHeight
        {
            get => boundsHeight.Value;
            set => value = boundsHeight.Value;
        }

        public Vector2 Bounds
        {
            get => bounds.Value;
            set => value = bounds.Value;
        }

        public Vector2 BoundsCenter
        {
            get => boundsCenter.Value;
            set => value = boundsCenter.Value;
        }

        public Vector2 BoundsBottomLeftCorner
        {
            get => boundsBottomLeftCorner.Value;
            set => value = boundsBottomLeftCorner.Value;
        }

        public Vector2 BoundsBottomRightCorner
        {
            get => boundsBottomRightCorner.Value;
            set => value = boundsBottomRightCorner.Value;
        }

        public Vector2 BoundsTopLeftCorner
        {
            get => boundsTopLeftCorner.Value;
            set => value = boundsTopLeftCorner.Value;
        }

        public Vector2 BoundsTopRightCorner
        {
            get => boundsTopRightCorner.Value;
            set => value = boundsTopRightCorner.Value;
        }

        public Vector2 BoundsBottomCenterPosition
        {
            get => boundsBottomCenterPosition.Value;
            set => value = boundsBottomCenterPosition.Value;
        }

        public int NumberOfHorizontalRaysPerSide
        {
            get => numberOfHorizontalRaysPerSide.Value;
            set => value = numberOfHorizontalRaysPerSide.Value;
        }

        public int NumberOfVerticalRaysPerSide
        {
            get => numberOfVerticalRaysPerSide.Value;
            set => value = numberOfVerticalRaysPerSide.Value;
        }

        public const string RaycastPath = "Event/Raycast/";
        public static readonly string RaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
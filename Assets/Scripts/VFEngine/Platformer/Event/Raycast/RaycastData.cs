using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastData", menuName = PlatformerRaycastDataPath, order = 0)]
    [InlineEditor]
    public class RaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character = null;
        [SerializeField] private RaycastSettings settings = null;
        [SerializeField] private BoxCollider2D boxCollider = null;

        #endregion

        private static readonly string ModelAssetPath = $"{RaycastPath}RaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public BoxCollider2D BoxCollider => boxCollider;
        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
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

        public RaycastRuntimeData RuntimeData { get; set; }
        public bool DisplayWarningsControl { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public bool CastRaysOnBothSides { get; set; }
        public bool PerformSafetyBoxcast { get; set; }
        public float DistanceToGroundRayMaximumLength { get; set; }
        public float BoundsWidth { get; set; }
        public float BoundsHeight { get; set; }
        public Vector2 Bounds { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsBottomCenterPosition { get; set; }
        public Vector2 BoundsTopLeftCorner { get; set; }
        public Vector2 BoundsTopRightCorner { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public int NumberOfVerticalRaysPerSide { get; set; }
        public float RayOffset { get; set; }
        public float ObstacleHeightTolerance { get; set; }
        public int NumberOfHorizontalRays { get; set; }
        public int NumberOfVerticalRays { get; set; }
        public const string RaycastPath = "Event/Raycast/";
        public static readonly string RaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
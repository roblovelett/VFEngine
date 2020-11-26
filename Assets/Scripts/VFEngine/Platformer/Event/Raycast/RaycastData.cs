using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;

    public class RaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastSettings settings;

        #endregion

        private static readonly string ModelAssetPath = $"{RaycastPath}RaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public RaycastSettings Settings
        {
            get => settings;
            set => settings = value;
        }

        public bool DisplayWarningsControl => settings.displayWarningsControl;
        public bool DrawRaycastGizmosControl => settings.drawRaycastGizmosControl;
        public bool CastRaysOnBothSides => settings.castRaysOnBothSides;
        public bool PerformSafetyBoxcast => settings.performSafetyBoxcast;
        public int NumberOfHorizontalRays => settings.numberOfHorizontalRays;
        public int NumberOfVerticalRays => settings.numberOfVerticalRays;
        public float RayOffset => settings.rayOffset;
        public float DistanceToGroundRayMaximumLength => settings.distanceToGroundRayMaximumLength;

        #endregion

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
        public int NumberOfVerticalRaysPerSide => NumberOfVerticalRays / 2;
        public float ObstacleHeightTolerance => RayOffset;
        public const string RaycastPath = "Event/Raycast/";
        public static readonly string RaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
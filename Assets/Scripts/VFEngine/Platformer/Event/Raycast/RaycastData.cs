using UnityEngine;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;

    public class RaycastData
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string ModelAssetPath = $"{RaycastPath}RaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public BoxCollider2D BoxCollider { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public RaycastSettings Settings { get; set; }
        public bool HasSettings => Settings;
        public bool HasBoxCollider => BoxCollider;
        public bool DisplayWarningsControlSetting => Settings.displayWarningsControl;
        public bool DrawRaycastGizmosControlSetting => Settings.drawRaycastGizmosControl;
        public bool CastRaysOnBothSidesSetting => Settings.castRaysOnBothSides;
        public bool PerformSafetyBoxcastSetting => Settings.performSafetyBoxcast;
        public int NumberOfHorizontalRaysSetting => Settings.numberOfHorizontalRays;
        public int NumberOfVerticalRaysSetting => Settings.numberOfVerticalRays;
        public float RayOffsetSetting => Settings.rayOffset;
        public float DistanceToGroundRayMaximumLengthSetting => Settings.distanceToGroundRayMaximumLength;

        #endregion

        public RaycastController Controller { get; set; }
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
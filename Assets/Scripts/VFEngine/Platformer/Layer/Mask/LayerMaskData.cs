using UnityEngine;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;

    public class LayerMaskData
    {
        #region fields

        #region dependencies

        #endregion

        private const string LmPath = "Layer/Mask/";
        private static readonly string ModelAssetPath = $"{LmPath}LayerMaskModel.asset";

        #endregion

        #region properties

        #region dependencies

        public LayerMaskController Controller { get; set; }
        public LayerMaskSettings Settings { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public DownRaycastHitColliderRuntimeData DownRaycastHitColliderRuntimeData { get; set; }
        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public int StandingOnLastFrameLayer { get; set; }
        public bool HasSettings => Settings;
        public bool DisplayWarningsControlSetting => Settings.displayWarningsControl;
        public LayerMask PlatformMaskSetting => Settings.platformMask;
        public LayerMask MovingPlatformMaskSetting => Settings.movingPlatformMask;
        public LayerMask OneWayPlatformMaskSetting => Settings.oneWayPlatformMask;
        public LayerMask MovingOneWayPlatformMaskSetting => Settings.movingOneWayPlatformMask;
        public LayerMask MidHeightOneWayPlatformMaskSetting => Settings.midHeightOneWayPlatformMask;
        public LayerMask StairsMaskSetting => Settings.stairsMask;

        #endregion

        public LayerMaskRuntimeData RuntimeData { get; set; }
        public bool DisplayWarningsControl { get; set; }
        public LayerMask PlatformMask { get; set; }
        public LayerMask MovingPlatformMask { get; set; }
        public LayerMask OneWayPlatformMask { get; set; }
        public LayerMask MovingOneWayPlatformMask { get; set; }
        public LayerMask MidHeightOneWayPlatformMask { get; set; }
        public LayerMask StairsMask { get; set; }
        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay { get; set; }
        public LayerMask RaysBelowLayerMaskPlatformsWithoutMidHeight { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }
        public int SavedBelowLayer { get; set; }
        public LayerMask SavedPlatformMask { get; set; }
        public static readonly string LayerMaskModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
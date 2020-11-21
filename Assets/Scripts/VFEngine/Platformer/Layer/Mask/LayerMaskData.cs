using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LayerMaskData", menuName = PlatformerLayerMaskDataPath, order = 0)]
    [InlineEditor]
    public class LayerMaskData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character = null;
        [SerializeField] private LayerMaskSettings settings = null;

        #endregion

        private const string LmPath = "Layer/Mask/";
        private static readonly string ModelAssetPath = $"{LmPath}LayerMaskModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public DownRaycastHitColliderRuntimeData DownRaycastHitColliderRuntimeData { get; set; }
        public GameObject Character => character;
        public Transform Transform { get; set; }
        public int StandingOnLastFrameLayer { get; set; }
        public bool HasSettings => settings;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public LayerMask PlatformMaskSetting => settings.platformMask;
        public LayerMask MovingPlatformMaskSetting => settings.movingPlatformMask;
        public LayerMask OneWayPlatformMaskSetting => settings.oneWayPlatformMask;
        public LayerMask MovingOneWayPlatformMaskSetting => settings.movingOneWayPlatformMask;
        public LayerMask MidHeightOneWayPlatformMaskSetting => settings.midHeightOneWayPlatformMask;
        public LayerMask StairsMaskSetting => settings.stairsMask;

        #endregion

        public LayerMaskRuntimeData RuntimeData { get; set; }
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
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
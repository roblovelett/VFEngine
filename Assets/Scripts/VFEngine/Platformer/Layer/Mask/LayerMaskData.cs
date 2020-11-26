using UnityEngine;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;

    public class LayerMaskData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private LayerMaskSettings settings;

        #endregion

        private const string LmPath = "Layer/Mask/";
        private static readonly string ModelAssetPath = $"{LmPath}LayerMaskModel.asset";

        #endregion

        #region properties

        #region dependencies

        public LayerMaskSettings Settings => settings;
        public bool DisplayWarningsControl => settings.displayWarningsControl;

        public LayerMask PlatformMask
        {
            get => settings.platformMask;
            set => settings.platformMask = value;
        }

        public LayerMask MovingPlatformMask => settings.movingPlatformMask;
        public LayerMask OneWayPlatformMask => settings.oneWayPlatformMask;
        public LayerMask MovingOneWayPlatformMask => settings.movingOneWayPlatformMask;
        public LayerMask MidHeightOneWayPlatformMask => settings.midHeightOneWayPlatformMask;
        public LayerMask StairsMask => settings.stairsMask;

        #endregion

        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay { get; set; }
        public LayerMask RaysBelowLayerMaskPlatformsWithoutMidHeight { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }
        public int SavedBelowLayer { get; set; }
        public LayerMask SavedPlatformMask { get; set; }
        public static readonly string LayerMaskModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
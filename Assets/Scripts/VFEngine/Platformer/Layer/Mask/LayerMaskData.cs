using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Layer.Mask
{
    public class LayerMaskData
    {
        #region properties

        #region dependencies

        public bool DisplayWarningsControl { get; set; }
        public LayerMask PlatformMask { get; set; }
        public LayerMask MovingPlatformMask { get; set; }
        public LayerMask OneWayPlatformMask { get; set; }
        public LayerMask MovingOneWayPlatformMask { get; set; }
        public LayerMask MidHeightOneWayPlatformMask { get; set; }
        public LayerMask StairsMask { get; set; }

        #endregion

        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay { get; set; }
        public LayerMask RaysBelowLayerMaskPlatformsWithoutMidHeight { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }
        public int SavedBelowLayer { get; set; }
        public LayerMask SavedPlatformMask { get; set; }
        public bool MidHeightOneWayPlatformMaskHasStandingOnLastFrameLayer { get; set; }

        #region public methods

        public void ApplySettings(LayerMaskSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            PlatformMask = settings.platformMask;
            MovingPlatformMask = settings.movingPlatformMask;
            OneWayPlatformMask = settings.oneWayPlatformMask;
            MovingOneWayPlatformMask = settings.movingOneWayPlatformMask;
            MidHeightOneWayPlatformMask = settings.midHeightOneWayPlatformMask;
            StairsMask = settings.stairsMask;
        }

        #endregion

        #endregion
    }
}
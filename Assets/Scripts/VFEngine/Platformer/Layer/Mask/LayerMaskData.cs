using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Layer.Mask
{
    public class LayerMaskData
    {
        #region properties

        #region dependencies

        public bool DisplayWarningsControl { get; set; }
        public LayerMask Platform { get; set; }
        public LayerMask MovingPlatform { get; set; }
        public LayerMask OneWayPlatform { get; set; }
        public LayerMask MovingOneWayPlatform { get; set; }
        public LayerMask MidHeightOneWayPlatform { get; set; }
        public LayerMask Stairs { get; set; }

        #endregion

        public LayerMask RaysBelowPlatformsWithoutOneWay { get; set; }
        public LayerMask RaysBelowPlatformsWithoutMidHeight { get; set; }
        public LayerMask RaysBelowPlatforms { get; set; }
        public int SavedBelowLayer { get; set; }
        public LayerMask SavedPlatform { get; set; }
        public bool MidHeightOneWayPlatformHasStandingOnLastFrame { get; set; }

        #region public methods

        public void ApplySettings(LayerMaskSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            Platform = settings.platform;
            MovingPlatform = settings.movingPlatform;
            OneWayPlatform = settings.oneWayPlatform;
            MovingOneWayPlatform = settings.movingOneWayPlatform;
            MidHeightOneWayPlatform = settings.midHeightOneWayPlatform;
            Stairs = settings.stairs;
        }

        #endregion

        #endregion
    }
}
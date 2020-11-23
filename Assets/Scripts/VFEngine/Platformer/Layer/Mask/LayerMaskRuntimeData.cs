using UnityEngine;

namespace VFEngine.Platformer.Layer.Mask
{
    public class LayerMaskRuntimeData
    {
        #region properties

        public LayerMaskController LayerMaskController { get; private set; }
        public int SavedBelowLayer { get; private set; }
        public LayerMask PlatformMask { get; private set; }
        public LayerMask MovingPlatformMask { get; private set; }
        public LayerMask OneWayPlatformMask { get; private set; }
        public LayerMask MovingOneWayPlatformMask { get; private set; }
        public LayerMask MidHeightOneWayPlatformMask { get; private set; }
        public LayerMask StairsMask { get; private set; }
        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay { get; private set; }
        public LayerMask RaysBelowLayerMaskPlatformsWithoutMidHeight { get; private set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; private set; }

        #region public methods

        public static LayerMaskRuntimeData CreateInstance(LayerMaskController controller, int savedBelowLayer,
            LayerMask platformMask, LayerMask movingPlatformMask, LayerMask oneWayPlatformMask,
            LayerMask movingOneWayPlatformMask, LayerMask midHeightOneWayPlatformMask, LayerMask stairsMask,
            LayerMask raysBelowLayerMaskPlatformsWithoutOneWay, LayerMask raysBelowLayerMaskPlatformsWithoutMidHeight,
            LayerMask raysBelowLayerMaskPlatforms)
        {
            return new LayerMaskRuntimeData
            {
                LayerMaskController = controller,
                SavedBelowLayer = savedBelowLayer,
                PlatformMask = platformMask,
                MovingPlatformMask = movingPlatformMask,
                OneWayPlatformMask = oneWayPlatformMask,
                MovingOneWayPlatformMask = movingOneWayPlatformMask,
                MidHeightOneWayPlatformMask = midHeightOneWayPlatformMask,
                StairsMask = stairsMask,
                RaysBelowLayerMaskPlatformsWithoutOneWay = raysBelowLayerMaskPlatformsWithoutOneWay,
                RaysBelowLayerMaskPlatformsWithoutMidHeight = raysBelowLayerMaskPlatformsWithoutMidHeight,
                RaysBelowLayerMaskPlatforms = raysBelowLayerMaskPlatforms
            };
        }

        #endregion

        #endregion
    }
}
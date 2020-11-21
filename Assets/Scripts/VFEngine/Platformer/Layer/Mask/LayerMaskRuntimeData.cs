using UnityEngine;

namespace VFEngine.Platformer.Layer.Mask
{
    public class LayerMaskRuntimeData : ScriptableObject
    {
        public LayerMasks layerMask;
        
        public struct LayerMasks
        {
            public LayerMaskController LayerMaskController { get; set; }
            public int SavedBelowLayer { get; set; }
            public LayerMask PlatformMask { get; set; }
            public LayerMask MovingPlatformMask { get; set; }
            public LayerMask OneWayPlatformMask { get; set; }
            public LayerMask MovingOneWayPlatformMask { get; set; }
            public LayerMask MidHeightOneWayPlatformMask { get; set; }
            public LayerMask StairsMask { get; set; }
            public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay { get; set; }
            public LayerMask RaysBelowLayerMaskPlatformsWithoutMidHeight { get; set; }
            public LayerMask RaysBelowLayerMaskPlatforms { get; set; }
        }
        
        public void SetLayerMaskController(LayerMaskController controller)
        {
            layerMask.LayerMaskController = controller;
        }
        
        public void SetLayerMasks(int savedBelowLayer, LayerMask platformMask, LayerMask movingPlatformMask,
            LayerMask oneWayPlatformMask, LayerMask movingOneWayPlatformMask, LayerMask midHeightOneWayPlatformMask,
            LayerMask stairsMask, LayerMask raysBelowLayerMaskPlatformsWithoutOneWay,
            LayerMask raysBelowLayerMaskPlatformsWithoutMidHeight, LayerMask raysBelowLayerMaskPlatforms)
        {
            layerMask.SavedBelowLayer = savedBelowLayer;
            layerMask.PlatformMask = platformMask;
            layerMask.MovingPlatformMask = movingPlatformMask;
            layerMask.OneWayPlatformMask = oneWayPlatformMask;
            layerMask.MovingOneWayPlatformMask = movingOneWayPlatformMask;
            layerMask.MidHeightOneWayPlatformMask = midHeightOneWayPlatformMask;
            layerMask.StairsMask = stairsMask;
            layerMask.RaysBelowLayerMaskPlatformsWithoutOneWay = raysBelowLayerMaskPlatformsWithoutOneWay;
            layerMask.RaysBelowLayerMaskPlatformsWithoutMidHeight = raysBelowLayerMaskPlatformsWithoutMidHeight;
            layerMask.RaysBelowLayerMaskPlatforms = raysBelowLayerMaskPlatforms;
        }
    }
}
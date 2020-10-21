using ScriptableObjects.Atoms.LayerMask.References;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;

    public class LayerMaskData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private LayerMaskSettings settings;

        /* fields */
        [SerializeField] private LayerMaskReference platformMask;
        [SerializeField] private LayerMaskReference oneWayPlatformMask;
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask;
        [SerializeField] private LayerMaskReference midHeightOneWayPlatformMask;
        [SerializeField] private LayerMaskReference stairsMask;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatformsWithoutOneWay;
        private const string LmPath = "Layer/Mask/";
        private static readonly string ModelAssetPath = $"{LmPath}DefaultLayerMaskModel.asset";

        /* properties: dependencies */
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool HasSettings => settings;

        public LayerMask PlatformMask
        {
            get => settings.platformMask;
            set => value = PlatformMask;
        }

        public LayerMask MovingPlatformMask => settings.movingPlatformMask;
        public LayerMask OneWayPlatformMask => settings.oneWayPlatformMask;
        public LayerMask MovingOneWayPlatformMask => settings.movingOneWayPlatformMask;
        public LayerMask MidHeightOneWayPlatformMask => settings.midHeightOneWayPlatformMask;
        public LayerMask StairsMask => settings.stairsMask;

        public LayerMask PlatformMaskRef
        {
            set => value = platformMask.Value;
        }

        public LayerMask OneWayPlatformMaskRef
        {
            set => value = oneWayPlatformMask.Value;
        }

        public LayerMask MovingOneWayPlatformMaskRef
        {
            set => value = movingOneWayPlatformMask.Value;
        }
        
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }
        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay { get; set; }

        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWayRef
        {
            set => value = raysBelowLayerMaskPlatformsWithoutOneWay.Value;
        }
        public LayerMask RaysBelowLayerMaskPlatformsWithoutMidHeight { get; set; }

        public LayerMask MidHeightOneWayPlatformMaskRef
        {
            set => value = midHeightOneWayPlatformMask.Value;
        }

        public LayerMask StairsMaskRef
        {
            set => value = stairsMask.Value;
        }

        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public LayerMask SavedPlatformMask { get; set; }
    }
}
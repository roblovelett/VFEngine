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

        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public LayerMask SavedPlatformMask { get; set; }
    }
}
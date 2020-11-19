using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable NotAccessedField.Global
// ReSharper disable RedundantDefaultMemberInitializer
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

        [SerializeField] private GameObject character;
        [SerializeField] private LayerMaskSettings settings = CreateInstance<LayerMaskSettings>();

        #endregion

        [SerializeField] private LayerMaskReference platformMask = new LayerMaskReference();
        [SerializeField] private LayerMaskReference movingPlatformMask = new LayerMaskReference();
        [SerializeField] private LayerMaskReference oneWayPlatformMask = new LayerMaskReference();
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask = new LayerMaskReference();
        [SerializeField] private LayerMaskReference midHeightOneWayPlatformMask = new LayerMaskReference();
        [SerializeField] private LayerMaskReference stairsMask = new LayerMaskReference();
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatformsWithoutOneWay = new LayerMaskReference();

        [SerializeField]
        private LayerMaskReference raysBelowLayerMaskPlatformsWithoutMidHeight = new LayerMaskReference();

        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms = new LayerMaskReference();
        [SerializeField] private IntReference savedBelowLayer = new IntReference();
        private const string LmPath = "Layer/Mask/";
        private static readonly string ModelAssetPath = $"{LmPath}LayerMaskModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
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

        public LayerMask PlatformMask
        {
            get => platformMask.Value;
            set => value = platformMask.Value;
        }

        public LayerMask OneWayPlatformMask
        {
            get => oneWayPlatformMask.Value;
            set => value = oneWayPlatformMask.Value;
        }

        public LayerMask MovingPlatformMask
        {
            get => movingPlatformMask.Value;
            set => value = movingPlatformMask.Value;
        }

        public LayerMask MovingOneWayPlatformMask
        {
            get => movingOneWayPlatformMask.Value;
            set => value = movingOneWayPlatformMask.Value;
        }

        public LayerMask MidHeightOneWayPlatformMask
        {
            get => midHeightOneWayPlatformMask.Value;
            set => value = midHeightOneWayPlatformMask.Value;
        }

        public LayerMask StairsMask
        {
            get => stairsMask.Value;
            set => value = stairsMask.Value;
        }

        public LayerMask RaysBelowLayerMaskPlatforms
        {
            get => raysBelowLayerMaskPlatforms.Value;
            set => value = raysBelowLayerMaskPlatforms.Value;
        }

        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay
        {
            get => raysBelowLayerMaskPlatformsWithoutOneWay.Value;
            set => value = raysBelowLayerMaskPlatformsWithoutOneWay.Value;
        }

        public LayerMask RaysBelowLayerMaskPlatformsWithoutMidHeight
        {
            get => raysBelowLayerMaskPlatformsWithoutMidHeight.Value;
            set => value = raysBelowLayerMaskPlatformsWithoutMidHeight.Value;
        }

        public int SavedBelowLayer
        {
            get => savedBelowLayer.Value;
            set => value = savedBelowLayer.Value;
        }

        public LayerMask SavedPlatformMask { get; set; }
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
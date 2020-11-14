using ScriptableObjects.Atoms.Mask.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable NotAccessedField.Global

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;

    public class LayerMaskData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private LayerMaskSettings settings = null;
        [SerializeField] private GameObjectReference standingOnLastFrame = new GameObjectReference();
        private GameObject StandingOnLastFrame => standingOnLastFrame;

        #endregion

        [SerializeField] private MaskReference platformMask = new MaskReference();
        [SerializeField] private MaskReference movingPlatformMask = new MaskReference();
        [SerializeField] private MaskReference oneWayPlatformMask = new MaskReference();
        [SerializeField] private MaskReference movingOneWayPlatformMask = new MaskReference();
        [SerializeField] private MaskReference midHeightOneWayPlatformMask = new MaskReference();
        [SerializeField] private MaskReference stairsMask = new MaskReference();
        [SerializeField] private MaskReference raysBelowLayerMaskPlatformsWithoutOneWay = new MaskReference();
        [SerializeField] private MaskReference raysBelowLayerMaskPlatformsWithoutMidHeight = new MaskReference();
        [SerializeField] private MaskReference raysBelowLayerMaskPlatforms = new MaskReference();
        [SerializeField] private IntReference savedBelowLayer = new IntReference();
        private const string LmPath = "Layer/Mask/";
        private static readonly string ModelAssetPath = $"{LmPath}LayerMaskModel.asset";

        #endregion

        #region properties

        #region dependencies

        public int StandingOnLastFrameLayer => StandingOnLastFrame.layer;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool HasSettings => settings;
        public LayerMask PlatformMaskSetting => settings.platformMask;
        public LayerMask MovingPlatformMaskSetting => settings.movingPlatformMask;
        public LayerMask OneWayPlatformMaskSetting => settings.oneWayPlatformMask;
        public LayerMask MovingOneWayPlatformMaskSetting => settings.movingOneWayPlatformMask;
        public LayerMask MidHeightOneWayPlatformMaskSetting => settings.midHeightOneWayPlatformMask;
        public LayerMask StairsMaskSetting => settings.stairsMask;

        #endregion

        public LayerMask PlatformMask
        {
            get => platformMask.Value.layer;
            set => value = platformMask.Value.layer;
        }

        public LayerMask OneWayPlatformMask
        {
            get => oneWayPlatformMask.Value.layer;
            set => value = oneWayPlatformMask.Value.layer;
        }

        public LayerMask MovingPlatformMask
        {
            get => movingPlatformMask.Value.layer;
            set => value = movingPlatformMask.Value.layer;
        }

        public LayerMask MovingOneWayPlatformMask
        {
            get => movingOneWayPlatformMask.Value.layer;
            set => value = movingOneWayPlatformMask.Value.layer;
        }

        public LayerMask MidHeightOneWayPlatformMask
        {
            get => midHeightOneWayPlatformMask.Value.layer;
            set => value = midHeightOneWayPlatformMask.Value.layer;
        }

        public LayerMask StairsMask
        {
            get => stairsMask.Value.layer;
            set => value = stairsMask.Value.layer;
        }

        public LayerMask RaysBelowLayerMaskPlatforms
        {
            get => raysBelowLayerMaskPlatforms.Value.layer;
            set => value = raysBelowLayerMaskPlatforms.Value.layer;
        }

        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay
        {
            get => raysBelowLayerMaskPlatformsWithoutOneWay.Value.layer;
            set => value = raysBelowLayerMaskPlatformsWithoutOneWay.Value.layer;
        }

        public LayerMask RaysBelowLayerMaskPlatformsWithoutMidHeight
        {
            get => raysBelowLayerMaskPlatformsWithoutMidHeight.Value.layer;
            set => value = raysBelowLayerMaskPlatformsWithoutMidHeight.Value.layer;
        }

        public int SavedBelowLayer
        {
            set => value = savedBelowLayer;
        }

        [HideInInspector] public LayerMask savedPlatformMask;
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
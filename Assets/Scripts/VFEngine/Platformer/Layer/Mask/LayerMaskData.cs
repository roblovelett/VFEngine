using ScriptableObjects.Atoms.LayerMask.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;

    public class LayerMaskData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private LayerMaskSettings settings;
        [SerializeField] private GameObjectReference standingOnLastFrame;
        private GameObject StandingOnLastFrame => standingOnLastFrame.Value;

        #endregion

        [SerializeField] private LayerMaskReference platformMask;
        [SerializeField] private LayerMaskReference movingPlatformMask;
        [SerializeField] private LayerMaskReference oneWayPlatformMask;
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask;
        [SerializeField] private LayerMaskReference midHeightOneWayPlatformMask;
        [SerializeField] private LayerMaskReference stairsMask;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatformsWithoutOneWay;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatformsWithoutMidHeight;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
        [SerializeField] private IntReference savedBelowLayer;
        private const string LmPath = "Layer/Mask/";
        private static readonly string ModelAssetPath = $"{LmPath}DefaultLayerMaskModel.asset";

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

        public LayerMask savedPlatformMask;
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable UnusedVariable
namespace VFEngine.Platformer.Layer.Mask
{
    using static LayerMask;
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LayerMaskModel", menuName = PlatformerLayerMaskModelPath, order = 0)]
    [InlineEditor]
    public class LayerMaskModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Layer Mask Data")] [SerializeField]
        private LayerMaskData l = null;

        #endregion

        #region private methods

        private async UniTaskVoid Initialize()
        {
            var lTask1 = Async(InitializeData());
            var lTask2 = Async(GetWarningMessages());
            var lTask3 = Async(InitializeModel());
            var lTask = await (lTask1, lTask2, lTask3);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeData()
        {
            l.PlatformMask = l.PlatformMaskSetting;
            l.MovingPlatformMask = l.MovingPlatformMaskSetting;
            l.OneWayPlatformMask = l.OneWayPlatformMaskSetting;
            l.MovingOneWayPlatformMask = l.MovingOneWayPlatformMaskSetting;
            l.MidHeightOneWayPlatformMask = l.MidHeightOneWayPlatformMaskSetting;
            l.StairsMask = l.StairsMaskSetting;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid GetWarningMessages()
        {
            if (l.DisplayWarnings)
            {
                const string lM = "Layer Mask";
                const string player = "Player";
                const string platform = "Platform";
                const string movingPlatform = "MovingPlatform";
                const string oneWayPlatform = "OneWayPlatform";
                const string movingOneWayPlatform = "MovingOneWayPlatform";
                const string midHeightOneWayPlatform = "MidHeightOneWayPlatform";
                const string stairs = "Stairs";
                var platformLayers = GetMask($"{player}", $"{platform}");
                var movingPlatformLayer = GetMask($"{movingPlatform}");
                var oneWayPlatformLayer = GetMask($"{oneWayPlatform}");
                var movingOneWayPlatformLayer = GetMask($"{movingOneWayPlatform}");
                var midHeightOneWayPlatformLayer = GetMask($"{midHeightOneWayPlatform}");
                var stairsLayer = GetMask($"{stairs}");
                LayerMask[] layers =
                {
                    platformLayers, movingPlatformLayer, oneWayPlatformLayer, movingOneWayPlatformLayer,
                    midHeightOneWayPlatformLayer, stairsLayer
                };
                LayerMask[] layerMasks =
                {
                    l.PlatformMask, l.MovingPlatformMask, l.OneWayPlatformMask, l.MovingOneWayPlatformMask,
                    l.MidHeightOneWayPlatformMask, l.StairsMask
                };
                var settings = $"{lM} Settings";
                var warningMessage = "";
                var warningMessageCount = 0;
                if (!l.HasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
                for (var i = 0; i < layers.Length; i++)
                {
                    if (layers[i].value == layerMasks[i].value) continue;
                    var mask = LayerToName(layerMasks[i]);
                    var layer = LayerToName(layers[i]);
                    warningMessage += FieldPropertyString($"{mask}", $"{layer}", $"{settings}");
                }

                string FieldString(string field, string scriptableObject)
                {
                    AddWarningMessageCount();
                    return FieldMessage(field, scriptableObject);
                }

                string FieldPropertyString(string field, string property, string scriptableObject)
                {
                    AddWarningMessageCount();
                    return FieldPropertyMessage(field, property, scriptableObject);
                }

                void AddWarningMessageCount()
                {
                    warningMessageCount++;
                }

                DebugLogWarning(warningMessageCount, warningMessage);
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeModel()
        {
            l.savedPlatformMask = l.PlatformMask;
            l.PlatformMask |= l.OneWayPlatformMask;
            l.PlatformMask |= l.MovingPlatformMask;
            l.PlatformMask |= l.MovingOneWayPlatformMask;
            l.PlatformMask |= l.MidHeightOneWayPlatformMask;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetRaysBelowLayerMaskPlatforms()
        {
            l.RaysBelowLayerMaskPlatforms = l.PlatformMask;
        }

        private void SetRaysBelowLayerMaskPlatformsWithoutOneWay()
        {
            l.RaysBelowLayerMaskPlatformsWithoutOneWay = l.PlatformMask & ~l.MidHeightOneWayPlatformMask &
                                                         ~l.OneWayPlatformMask & ~l.MovingOneWayPlatformMask;
        }

        private void SetRaysBelowLayerMaskPlatformsWithoutMidHeight()
        {
            l.RaysBelowLayerMaskPlatformsWithoutMidHeight =
                l.RaysBelowLayerMaskPlatforms & ~l.MidHeightOneWayPlatformMask;
        }

        private void SetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight()
        {
            l.RaysBelowLayerMaskPlatforms = l.RaysBelowLayerMaskPlatformsWithoutMidHeight;
        }

        private void SetRaysBelowLayerMaskPlatformsToOneWayOrStairs()
        {
            l.RaysBelowLayerMaskPlatforms = (l.RaysBelowLayerMaskPlatforms & ~l.OneWayPlatformMask) | l.StairsMask;
        }

        private void SetRaysBelowLayerMaskPlatformsToOneWay()
        {
            l.RaysBelowLayerMaskPlatforms &= ~l.OneWayPlatformMask;
        }

        private void SetSavedBelowLayerToStandingOnLastFrameLayer()
        {
            l.SavedBelowLayer = l.StandingOnLastFrameLayer;
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnInitialize()
        {
            Async(Initialize());
        }

        public void OnSetRaysBelowLayerMaskPlatforms()
        {
            SetRaysBelowLayerMaskPlatforms();
        }

        public void OnSetRaysBelowLayerMaskPlatformsWithoutOneWay()
        {
            SetRaysBelowLayerMaskPlatformsWithoutOneWay();
        }

        public void OnSetRaysBelowLayerMaskPlatformsWithoutMidHeight()
        {
            SetRaysBelowLayerMaskPlatformsWithoutMidHeight();
        }

        public void OnSetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight()
        {
            SetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight();
        }

        public void OnSetRaysBelowLayerMaskPlatformsToOneWayOrStairs()
        {
            SetRaysBelowLayerMaskPlatformsToOneWayOrStairs();
        }

        public void OnSetRaysBelowLayerMaskPlatformsToOneWay()
        {
            SetRaysBelowLayerMaskPlatformsToOneWay();
        }

        public void OnSetSavedBelowLayerToStandingOnLastFrameLayer()
        {
            SetSavedBelowLayerToStandingOnLastFrameLayer();
        }

        #endregion

        #endregion
    }
}
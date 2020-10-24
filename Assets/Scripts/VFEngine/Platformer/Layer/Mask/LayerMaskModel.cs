using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedVariable
namespace VFEngine.Platformer.Layer.Mask
{
    using static LayerMask;
    using static UniTaskExtensions;
    using static DebugExtensions;

    [CreateAssetMenu(fileName = "LayerMaskModel", menuName = "VFEngine/Platformer/Layer/Mask/Layer Mask Model",
        order = 0)]
    public class LayerMaskModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [LabelText("Layer Mask Data")] [SerializeField]
        private LayerMaskData lm;

        /* fields */
        private const string Lm = "Layer Mask";

        /* fields: methods */
        private async UniTaskVoid InitializeInternal()
        {
            var lTask1 = Async(InitializeData());
            var lTask2 = Async(GetWarningMessages());
            var lTask3 = Async(InitializeModel());
            var lTask = await (lTask1, lTask2, lTask3);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeData()
        {
            lm.PlatformMaskRef = lm.PlatformMask;
            lm.OneWayPlatformMaskRef = lm.OneWayPlatformMask;
            lm.MovingOneWayPlatformMaskRef = lm.MovingOneWayPlatformMask;
            lm.MidHeightOneWayPlatformMaskRef = lm.MidHeightOneWayPlatformMask;
            lm.StairsMaskRef = lm.StairsMask;
            lm.RaysBelowLayerMaskPlatformsWithoutOneWayRef = lm.RaysBelowLayerMaskPlatformsWithoutOneWay;
            lm.RaysBelowLayerMaskPlatformsRef = lm.RaysBelowLayerMaskPlatforms;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid GetWarningMessages()
        {
            if (lm.DisplayWarnings)
            {
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
                    lm.PlatformMask, lm.MovingPlatformMask, lm.OneWayPlatformMask, lm.MovingOneWayPlatformMask,
                    lm.MidHeightOneWayPlatformMask, lm.StairsMask
                };
                var settings = $"{Lm} Settings";
                var warningMessage = "";
                var warningMessageCount = 0;
                if (!lm.HasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
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
            lm.SavedPlatformMask = lm.PlatformMask;
            lm.PlatformMask |= lm.OneWayPlatformMask;
            lm.PlatformMask |= lm.MovingPlatformMask;
            lm.PlatformMask |= lm.MovingOneWayPlatformMask;
            lm.PlatformMask |= lm.MidHeightOneWayPlatformMask;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetRaysBelowLayerMaskPlatforms()
        {
            lm.RaysBelowLayerMaskPlatforms = lm.PlatformMask;
        }

        private void SetRaysBelowLayerMaskPlatformsWithoutOneWay()
        {
            lm.RaysBelowLayerMaskPlatformsWithoutOneWay = lm.PlatformMask & ~lm.MidHeightOneWayPlatformMask &
                                                          ~lm.OneWayPlatformMask & ~lm.MovingOneWayPlatformMask;
        }

        private void SetRaysBelowLayerMaskPlatformsWithoutMidHeight()
        {
            lm.RaysBelowLayerMaskPlatformsWithoutMidHeight =
                lm.RaysBelowLayerMaskPlatforms & ~lm.MidHeightOneWayPlatformMask;
        }

        private void SetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight()
        {
            lm.RaysBelowLayerMaskPlatforms = lm.RaysBelowLayerMaskPlatformsWithoutMidHeight;
        }

        private void SetRaysBelowLayerMaskPlatformsToOneWayOrStairs()
        {
            lm.RaysBelowLayerMaskPlatforms = (lm.RaysBelowLayerMaskPlatforms & ~lm.OneWayPlatformMask) | lm.StairsMask;
        }

        private void SetRaysBelowLayerMaskPlatformsToOneWay()
        {
            lm.RaysBelowLayerMaskPlatforms &= ~lm.OneWayPlatformMask;
        }

        /* properties: methods */
        public void Initialize()
        {
            Async(InitializeInternal());
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
    }
}
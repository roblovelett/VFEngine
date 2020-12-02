using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObject;
    using static LayerMask;
    using static DebugExtensions;
    using static UniTaskExtensions;

    public class LayerMaskController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private LayerMaskSettings settings;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private LayerMaskData l;
        private DownRaycastHitColliderData downRaycastHitCollider;

        #endregion

        #region private methods

        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
            if (l.DisplayWarningsControl) GetWarningMessages();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<LayerMaskSettings>();
            l = new LayerMaskData();
            l.ApplySettings(settings);
            l.SavedPlatformMask = l.PlatformMask;
            l.PlatformMask |= l.OneWayPlatformMask;
            l.PlatformMask |= l.MovingPlatformMask;
            l.PlatformMask |= l.MovingOneWayPlatformMask;
            l.PlatformMask |= l.MidHeightOneWayPlatformMask;
        }

        private void SetControllers()
        {
            downRaycastHitColliderController = character.GetComponentNoAllocation<DownRaycastHitColliderController>();
        }

        private void GetWarningMessages()
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
            var layerMaskSettings = $"{lM} Settings";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!settings) warningMessage += FieldString($"{layerMaskSettings}", $"{layerMaskSettings}");
            for (var i = 0; i < layers.Length; i++)
            {
                if (layers[i].value == layerMasks[i].value) continue;
                var mask = LayerToName(layerMasks[i]);
                var layer = LayerToName(layers[i]);
                warningMessage += FieldPropertyString($"{mask}", $"{layer}", $"{layerMaskSettings}");
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

        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            downRaycastHitCollider = downRaycastHitColliderController.Data;
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
            l.SavedBelowLayer = downRaycastHitCollider.StandingOnLastFrame.layer;
        }

        #endregion

        #endregion

        #region properties

        public LayerMaskData Data => l;

        #region public methods

        public async UniTaskVoid OnSetRaysBelowLayerMaskPlatforms()
        {
            SetRaysBelowLayerMaskPlatforms();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetRaysBelowLayerMaskPlatformsWithoutOneWay()
        {
            SetRaysBelowLayerMaskPlatformsWithoutOneWay();
            await SetYieldOrSwitchToThreadPoolAsync();
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
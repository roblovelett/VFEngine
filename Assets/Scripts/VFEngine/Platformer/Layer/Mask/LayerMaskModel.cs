using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Layer.Mask
{
    using static LayerMask;
    using static DebugExtensions;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "LayerMaskModel", menuName = PlatformerLayerMaskModelPath, order = 0)]
    [InlineEditor]
    public class LayerMaskModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Layer Mask Data")][SerializeField] private LayerMaskData l;
        [SerializeField] private GameObject character;
        [SerializeField] private LayerMaskController layerMaskController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        private DownRaycastHitColliderData downRaycastHitCollider;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!l) l = CreateInstance<LayerMaskData>();
            if (!l.Settings) l.Settings = CreateInstance<LayerMaskSettings>();
            if (!layerMaskController && character) layerMaskController = character.GetComponent<LayerMaskController>();
            else if (layerMaskController && !character) character = layerMaskController.Character;
            if (!raycastHitColliderController) raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
            l.SavedPlatformMask = l.PlatformMask;
            l.PlatformMask |= l.OneWayPlatformMask;
            l.PlatformMask |= l.MovingPlatformMask;
            l.PlatformMask |= l.MovingOneWayPlatformMask;
            l.PlatformMask |= l.MidHeightOneWayPlatformMask;
            if (l.DisplayWarningsControl) GetWarningMessages();
        }

        private void InitializeModel()
        {
            downRaycastHitCollider = raycastHitColliderController.DownRaycastHitColliderModel.Data;
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
            var settings = $"{lM} Settings";
            var warningMessage = "";
            var warningMessageCount = 0;
            
            if (!l.Settings) warningMessage += FieldString($"{settings}", $"{settings}");
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

        public void OnInitializeData()
        {
            InitializeData();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
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
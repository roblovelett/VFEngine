using Cysharp.Threading.Tasks;
using UnityEngine;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Layer.Mask
{
    using static UniTaskExtensions;

    public class LayerMaskController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private LayerMaskModel layerMaskModel;
        /*----------------------------------------------------------------------*/
        [SerializeField] private LayerMaskSettings settings;
        [SerializeField] private GameObject character;
        [SerializeField] private LayerMaskController layerMaskController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        private LayerMaskData l;
        private DownRaycastHitColliderData downRaycastHitCollider;
        #endregion

        #region private methods
        
        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
            //if (p.DisplayWarningsControl) GetWarningMessages();
        }
        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }
        private void PlatformerInitializeData()
        {
            LoadCharacter();
            LoadLayerMaskModel();
            InitializeLayerMaskData();
        }

        

        private void LoadLayerMaskModel()
        {
            layerMaskModel = new LayerMaskModel();
        }

        private void InitializeLayerMaskData()
        {
            layerMaskModel.OnInitializeData();
        }
        
        /*----------------------------------------------------------------------------*/
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
            if (!layerMaskController && character) layerMaskController = character.GetComponent<LayerMaskController>();
            else if (layerMaskController && !character) character = layerMaskController.Character;
            if (!raycastHitColliderController)
                raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
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

        public GameObject Character => character;
        public LayerMaskModel LayerMaskModel => layerMaskModel;
        //--------------------------------------------------------------------------------------//
        public LayerMaskData Data => l;

        #region public methods

        public void OnPlatformerInitializeData()
        {
            PlatformerInitializeData();
        }

        public async UniTaskVoid SetRaysBelowLayerMaskPlatforms()
        {
            layerMaskModel.OnSetRaysBelowLayerMaskPlatforms();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRaysBelowLayerMaskPlatformsWithoutOneWay()
        {
            layerMaskModel.OnSetRaysBelowLayerMaskPlatformsWithoutOneWay();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetRaysBelowLayerMaskPlatformsWithoutMidHeight()
        {
            layerMaskModel.OnSetRaysBelowLayerMaskPlatformsWithoutMidHeight();
        }

        public void SetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight()
        {
            layerMaskModel.OnSetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight();
        }

        public void SetRaysBelowLayerMaskPlatformsToOneWayOrStairs()
        {
            layerMaskModel.OnSetRaysBelowLayerMaskPlatformsToOneWayOrStairs();
        }

        public void SetRaysBelowLayerMaskPlatformsToOneWay()
        {
            layerMaskModel.OnSetRaysBelowLayerMaskPlatformsToOneWay();
        }

        public void SetSavedBelowLayerToStandingOnLastFrameLayer()
        {
            layerMaskModel.OnSetSavedBelowLayerToStandingOnLastFrameLayer();
        }
        
        // --------------------------------------------------------------------------------------------
        
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
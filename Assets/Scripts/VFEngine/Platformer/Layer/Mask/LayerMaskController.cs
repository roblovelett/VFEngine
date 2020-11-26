using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;
    using static LayerMaskData;
    using static UniTaskExtensions;

    public class LayerMaskController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private LayerMaskModel layerMaskModel;

        #endregion

        #region private methods

        private async void Awake()
        {
            await Async(InitializeData());
        }

        private async UniTaskVoid InitializeData()
        {
            await Async(LoadModels());
            await Async(InitializeModelsData());
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async void Start()
        {
            await Async(InitializeModels());
        }

        private async UniTaskVoid LoadModels()
        {
            await Async(LoadLayerMaskModel());
            await SetYieldOrSwitchToThreadPoolAsync();

            async UniTaskVoid LoadLayerMaskModel()
            {
                if (!layerMaskModel) layerMaskModel = LoadModel<LayerMaskModel>(LayerMaskModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }
        }

        private async UniTaskVoid InitializeModelsData()
        {
            await Async(layerMaskModel.OnInitializeData());
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeModels()
        {
            await Async(layerMaskModel.OnInitializeModel());
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion

        #region properties

        public LayerMaskModel LayerMaskModel => layerMaskModel;
        

        #region public methods

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

        #endregion

        #endregion
    }
}
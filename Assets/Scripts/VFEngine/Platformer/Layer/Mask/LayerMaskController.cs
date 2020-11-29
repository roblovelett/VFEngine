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

        #endregion

        #region private methods

        private void PlatformerInitializeData()
        {
            LoadCharacter();
            LoadLayerMaskModel();
            InitializeLayerMaskData();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void LoadLayerMaskModel()
        {
            layerMaskModel = new LayerMaskModel();
        }

        private void InitializeLayerMaskData()
        {
            layerMaskModel.OnInitializeData();
        }

        #endregion

        #endregion

        #region properties

        public GameObject Character => character;
        public LayerMaskModel LayerMaskModel => layerMaskModel;

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

        #endregion

        #endregion
    }
}
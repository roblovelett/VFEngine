using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Layer.Mask
{
    using static ScriptableObjectExtensions;
    using static LayerMaskData;
    using static Debug;
    using static UniTaskExtensions;
    using static ScriptableObject;

    public class LayerMaskController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private LayerMaskModel model;
        private readonly LayerMaskController controller;

        #endregion

        #region private methods

        private void Awake()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            RuntimeData = CreateInstance<LayerMaskRuntimeData>();
            RuntimeData.SetLayerMaskController(controller);
        }

        private void InitializeModel()
        {
            if (!model) model = LoadData(ModelPath) as LayerMaskModel;
            Assert(model != null, nameof(model) + " != null");
            model.OnInitialize();
        }

        #endregion

        #endregion

        #region properties

        public LayerMaskController()
        {
            controller = this;
        }

        public LayerMaskRuntimeData RuntimeData { get; private set; }

        #region public methods

        public async UniTaskVoid SetRaysBelowLayerMaskPlatforms()
        {
            model.OnSetRaysBelowLayerMaskPlatforms();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRaysBelowLayerMaskPlatformsWithoutOneWay()
        {
            model.OnSetRaysBelowLayerMaskPlatformsWithoutOneWay();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetRaysBelowLayerMaskPlatformsWithoutMidHeight()
        {
            model.OnSetRaysBelowLayerMaskPlatformsWithoutMidHeight();
        }

        public void SetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight()
        {
            model.OnSetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight();
        }

        public void SetRaysBelowLayerMaskPlatformsToOneWayOrStairs()
        {
            model.OnSetRaysBelowLayerMaskPlatformsToOneWayOrStairs();
        }

        public void SetRaysBelowLayerMaskPlatformsToOneWay()
        {
            model.OnSetRaysBelowLayerMaskPlatformsToOneWay();
        }

        public void SetSavedBelowLayerToStandingOnLastFrameLayer()
        {
            model.OnSetSavedBelowLayerToStandingOnLastFrameLayer();
        }

        #endregion

        #endregion
    }
}
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

    public class LayerMaskController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private LayerMaskModel model;

        #endregion

        #region private methods

        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as LayerMaskModel;
            Assert(model != null, nameof(model) + " != null");
            model.OnInitialize();
        }

        #endregion

        #endregion

        #region properties

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
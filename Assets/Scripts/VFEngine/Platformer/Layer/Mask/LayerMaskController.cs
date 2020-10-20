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
        /* fields: dependencies */
        [SerializeField] private LayerMaskModel model;
        /* fields */

        /* fields: methods */
        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as LayerMaskModel;
            Assert(model != null, nameof(model) + " != null");
            model.Initialize();
        }

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
    }
}
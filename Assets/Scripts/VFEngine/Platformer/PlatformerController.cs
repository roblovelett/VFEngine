using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    public class PlatformerController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerModel platformerModel;
        private readonly PlatformerController controller;

        #endregion

        #region private methods

        private async void Awake()
        {
            await Async(LoadModels());
            await Async(InitializeModelsData());
        }

        private async void Start()
        {
            await Async(InitializeModels());
        }

        private async UniTaskVoid LoadModels()
        {
            await Async(LoadPlatformerModel());
            await SetYieldOrSwitchToThreadPoolAsync();

            async UniTaskVoid LoadPlatformerModel()
            {
                if (!platformerModel) platformerModel = LoadModel<PlatformerModel>(PlatformerModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }
        }

        private async UniTaskVoid InitializeModelsData()
        {
            await Async(platformerModel.OnInitializeData());
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeModels()
        {
            await Async(platformerModel.OnInitializeModel());
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void FixedUpdate()
        {
            platformerModel.OnRunPlatformer();
        }

        #endregion

        #endregion

        #region properties

        public PlatformerRuntimeData PlatformerRuntimeData { get; private set; }

        #endregion
    }
}
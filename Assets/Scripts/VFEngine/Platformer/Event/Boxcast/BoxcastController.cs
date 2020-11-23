using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedVariable
// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace VFEngine.Platformer.Event.Boxcast
{
    using static SafetyBoxcastData;
    using static BoxcastData;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    public class BoxcastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private BoxcastModel boxcastModel;
        [SerializeField] private SafetyBoxcastModel safetyBoxcastModel;

        #endregion

        #region private methods

        private void Awake()
        {
            InitializeData();
        }

        private void Start()
        {
            Async(InitializeModels());
        }

        private void InitializeData()
        {
            Async(LoadModels());
            Async(InitializeModelsData());
        }

        private async UniTaskVoid LoadModels()
        {
            var t1 = Async(LoadBoxcastModel());
            var t2 = Async(LoadSafetyBoxcastModel());
            var task1 = await (t1, t2);
            await SetYieldOrSwitchToThreadPoolAsync();

            async UniTaskVoid LoadBoxcastModel()
            {
                if (!boxcastModel) boxcastModel = LoadModel<BoxcastModel>(BoxcastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid LoadSafetyBoxcastModel()
            {
                if (!safetyBoxcastModel) safetyBoxcastModel = LoadModel<SafetyBoxcastModel>(SafetyBoxcastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }
        }

        private async UniTaskVoid InitializeModelsData()
        {
            var t1 = Async(boxcastModel.OnInitializeData());
            var t2 = Async(safetyBoxcastModel.OnInitializeData());
            var task1 = await (t1, t2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeModels()
        {
            var t1 = Async(boxcastModel.OnInitializeModel());
            var t2 = Async(safetyBoxcastModel.OnInitializeModel());
            var task1 = await (t1, t2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion

        #region properties

        public BoxcastRuntimeData BoxcastRuntimeData { get; private set; }
        public SafetyBoxcastRuntimeData SafetyBoxcastRuntimeData { get; private set; }

        #region public methods

        #region safety boxcast model

        public void SetSafetyBoxcastForImpassableAngle()
        {
            safetyBoxcastModel.OnSetSafetyBoxcastForImpassableAngle();
        }

        public void SetSafetyBoxcast()
        {
            safetyBoxcastModel.OnSetSafetyBoxcast();
        }

        #endregion

        #endregion

        #endregion
    }
}
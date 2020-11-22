using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedVariable
// ReSharper disable NotAccessedField.Local
namespace VFEngine.Platformer.Event.Boxcast
{
    using static SafetyBoxcastData;
    using static BoxcastData;
    using static Debug;
    using static ScriptableObject;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    public class BoxcastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private BoxcastModel boxcastModel;
        [SerializeField] private SafetyBoxcastModel safetyBoxcastModel;
        private readonly BoxcastController controller;

        #endregion

        #region private methods

        private void Awake()
        {
            Async(InitializeData());
        }

        private void Start()
        {
            Async(InitializeModels());
        }

        private async UniTaskVoid InitializeData()
        {
            var t1 = Async(SetBoxcastRuntimeData());
            var t2 = Async(SetSafetyBoxcastRuntimeData());
            var t3 = Async(SetBoxcastModel());
            var t4 = Async(SetSafetyBoxcastModel());
            var task1 = await (t1, t2, t3, t4);
            var t5 = Async(boxcastModel.OnInitializeData());
            var t6 = Async(safetyBoxcastModel.OnInitializeData());
            var task2 = await (t5, t6);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetBoxcastRuntimeData()
        {
            RuntimeData = CreateInstance<BoxcastRuntimeData>();
            RuntimeData.SetBoxcastController(controller);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetSafetyBoxcastRuntimeData()
        {
            SafetyBoxcastRuntimeData = CreateInstance<SafetyBoxcastRuntimeData>();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetBoxcastModel()
        {
            if (!boxcastModel) boxcastModel = LoadData(BoxcastModelPath) as BoxcastModel;
            Assert(boxcastModel != null, nameof(boxcastModel) + " != null");
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetSafetyBoxcastModel()
        {
            if (!safetyBoxcastModel) safetyBoxcastModel = LoadData(SafetyBoxcastModelPath) as SafetyBoxcastModel;
            Assert(safetyBoxcastModel != null, nameof(safetyBoxcastModel) + " != null");
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

        public BoxcastController()
        {
            controller = this;
        }

        public BoxcastRuntimeData RuntimeData { get; private set; }
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
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static Debug;
    using static SafetyBoxcastData;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    public class SafetyBoxcastController : MonoBehaviour, IController
    {
        [SerializeField] private SafetyBoxcastModel model;

        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as SafetyBoxcastModel;
            Assert(model != null, nameof(model) + " != null");
            model.OnInitialize();
        }

        public async UniTaskVoid SetSafetyBoxcastForImpassableAngle()
        {
            model.OnSetSafetyBoxcastForImpassableAngle();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetHasSafetyBoxcast()
        {
            model.OnSetHasSafetyBoxcast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetSafetyBoxcast()
        {
            model.OnSetSafetyBoxcast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void ResetState()
        {
            model.OnResetState();
        }
    }
}
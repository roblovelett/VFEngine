using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Boxcast
{
    using static SafetyBoxcastData;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    public class BoxcastController : MonoBehaviour, IController
    {
        [SerializeField] private SafetyBoxcastModel safetyBoxcastModel;

        private void Awake()
        {
            if (!safetyBoxcastModel) safetyBoxcastModel = LoadModel<SafetyBoxcastModel>(SafetyBoxcastModelPath);
            safetyBoxcastModel.OnInitialize();
        }

        #region safety boxcast
        
        public async UniTaskVoid SetSafetyBoxcastForImpassableAngle()
        {
            safetyBoxcastModel.OnSetSafetyBoxcastForImpassableAngle();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetHasSafetyBoxcast()
        {
            safetyBoxcastModel.OnSetHasSafetyBoxcast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetSafetyBoxcast()
        {
            safetyBoxcastModel.OnSetSafetyBoxcast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void ResetSafetyBoxcastState()
        {
            safetyBoxcastModel.OnResetState();
        }
        
        #endregion
    }
}
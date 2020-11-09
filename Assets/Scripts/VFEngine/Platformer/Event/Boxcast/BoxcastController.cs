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
        #region fields

        #region dependencies

        [SerializeField] private SafetyBoxcastModel safetyBoxcastModel;

        #endregion

        #region private methods

        private void Awake()
        {
            GetModels();
            InitializeModels();
        }

        private void GetModels()
        {
            if (!safetyBoxcastModel) safetyBoxcastModel = LoadModel<SafetyBoxcastModel>(SafetyBoxcastModelPath);
        }

        private void InitializeModels()
        {
            safetyBoxcastModel.OnInitialize();
        }

        #endregion

        #endregion

        #region properties

        #region public methods
        
        #region platformer model

        /*
        public void OnInitializeFrameSafetyBoxcast()
        {
            ResetSafetyBoxcastState();
        }
        */
        
        #endregion

        #region safety boxcast model

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

        #endregion

        #endregion
    }
}
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast
{
    using static SafetyBoxcastData;
    using static ScriptableObjectExtensions;

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
        }

        private void GetModels()
        {
            if (!safetyBoxcastModel) safetyBoxcastModel = LoadModel<SafetyBoxcastModel>(SafetyBoxcastModelPath);
        }

        #endregion

        #endregion

        #region properties

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
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Tools;

// ReSharper disable UnusedVariable
namespace VFEngine.Platformer.Event.Boxcast
{
    using static SafetyBoxcastData;
    using static BoxcastData;
    using static ScriptableObjectExtensions;

    public class BoxcastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private BoxcastModel boxcastModel;
        [SerializeField] private SafetyBoxcastModel safetyBoxcastModel;

        #endregion

        #region private methods

        private void LoadCharacter()
        {
            if (!character) character = transform.parent.gameObject;
        }

        private void LoadBoxcastModel()
        {
            if (!boxcastModel) boxcastModel = LoadModel<BoxcastModel>(BoxcastModelPath);
        }

        private void LoadSafetyBoxcastModel()
        {
            if (!safetyBoxcastModel) safetyBoxcastModel = LoadModel<SafetyBoxcastModel>(SafetyBoxcastModelPath);
        }

        private void InitializeBoxcastData()
        {
            boxcastModel.OnInitializeData();
        }

        private void InitializeSafetyBoxcastData()
        {
            safetyBoxcastModel.OnInitializeData();
        }

        private void PlatformerInitializeData()
        {
            LoadCharacter();
            LoadBoxcastModel();
            LoadSafetyBoxcastModel();
            InitializeBoxcastData();
            InitializeSafetyBoxcastData();
        }

        #endregion

        #endregion

        #region properties

        public GameObject Character => character;
        public BoxcastModel BoxcastModel => boxcastModel;
        public SafetyBoxcastModel SafetyBoxcastModel => safetyBoxcastModel;

        #region public methods

        #region platformer

        public void OnPlatformerInitializeData()
        {
            PlatformerInitializeData();
        }

        #endregion

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
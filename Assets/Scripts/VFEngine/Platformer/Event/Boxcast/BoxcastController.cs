using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Tools;

// ReSharper disable NotAccessedField.Local
namespace VFEngine.Platformer.Event.Boxcast
{
    using static SafetyBoxcastData;
    using static BoxcastData;
    using static Debug;
    using static ScriptableObject;
    using static ScriptableObjectExtensions;

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
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            RuntimeData = CreateInstance<BoxcastRuntimeData>();
            RuntimeData.SetBoxcastController(controller);
            SafetyBoxcastRuntimeData = CreateInstance<SafetyBoxcastRuntimeData>();
        }

        private void InitializeModel()
        {
            if (!boxcastModel) boxcastModel = LoadData(BoxcastModelPath) as BoxcastModel;
            Assert(boxcastModel != null, nameof(boxcastModel) + " != null");
            boxcastModel.OnInitialize();
            if (!safetyBoxcastModel) safetyBoxcastModel = LoadData(SafetyBoxcastModelPath) as SafetyBoxcastModel;
            Assert(safetyBoxcastModel != null, nameof(safetyBoxcastModel) + " != null");
            safetyBoxcastModel.OnInitialize();
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
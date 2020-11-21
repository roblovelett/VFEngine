using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer
{
    using static PlatformerData;
    using static ScriptableObjectExtensions;
    using static Debug;
    using static ScriptableObject;

    public class PlatformerController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerModel model = null;
        private readonly PlatformerController controller;

        #endregion

        #region private methods

        private void Awake()
        {
            InitializeData();
            InitializeModel();
        }
        
        private void InitializeData()
        {
            RuntimeData = CreateInstance<PlatformerRuntimeData>();
            RuntimeData.SetPlatformerController(controller);
        }

        private void InitializeModel()
        {
            if (!model) model = LoadData(ModelPath) as PlatformerModel;
            Assert(model != null, nameof(model) + " != null");
            model.OnInitialize();
        }

        private void FixedUpdate()
        {
            model.OnRunPlatformer();
        }

        #endregion

        #endregion
        
        #region properties

        public PlatformerRuntimeData RuntimeData { get; private set; }
        public PlatformerController()
        {
            controller = this;
        }
        #endregion
    }
}
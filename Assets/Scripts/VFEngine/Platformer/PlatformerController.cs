using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

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

        private void Awake()
        {
            LoadModels();
            platformerModel.OnInitializeData();
        }

        private void Start()
        {
            platformerModel.OnInitializeModel();
        }

        private void LoadModels()
        {
            if (!platformerModel) platformerModel = LoadModel<PlatformerModel>(PlatformerModelPath);
        }

        private void FixedUpdate()
        {
            //platformerModel.OnRunPlatformer();
        }

        #endregion

        #endregion

        #region properties

        public PlatformerModel PlatformerModel => platformerModel;

        #endregion
    }
}
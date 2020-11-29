using UnityEngine;

// ReSharper disable Unity.RedundantEventFunction
namespace VFEngine.Platformer
{
    public class PlatformerController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private PlatformerModel platformerModel;

        #endregion

        #region private methods

        private void Awake()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            LoadCharacter();
            LoadPlatformerModel();
            InitializePlatformerData();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void LoadPlatformerModel()
        {
            platformerModel = new PlatformerModel();
        }

        private void InitializePlatformerData()
        {
            platformerModel.OnInitializeData();
        }

        private void Start()
        {
            //platformerModel.OnInitializeModel();
        }

        private void FixedUpdate()
        {
            //platformerModel.OnRunPlatformer();
        }

        #endregion

        #endregion

        #region properties

        public GameObject Character => character;
        public PlatformerModel PlatformerModel => platformerModel;

        #endregion
    }
}
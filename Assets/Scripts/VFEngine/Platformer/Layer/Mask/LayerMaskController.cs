using UnityEngine;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Layer.Mask
{
    using static GameObject;
    using static ScriptableObject;

    public class LayerMaskController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private LayerMaskSettings settings;
        private GameObject character;
        private LayerMaskData l;

        #endregion

        #region internal

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
        }

        private void InitializeData()
        {
            l = new LayerMaskData();
            l.InitializeData();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            character = Find("Character");
            if (!settings) settings = CreateInstance<LayerMaskSettings>();
        }

        private void Initialize()
        {
            l.Initialize(settings, character);
        }

        #endregion

        #endregion

        #endregion

        #region properties

        public LayerMaskData Data => l;

        #region public methods

        #endregion

        #endregion
    }
}
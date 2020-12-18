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
            character = Find("Character");
            if (!settings) settings = CreateInstance<LayerMaskSettings>();
            l = new LayerMaskData();
            l.ApplySettings(settings);
            l.Initialize(character);
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
        }

        private void Initialize()
        {
        }

        #endregion

        #endregion

        private void PlatformerInitializeFrame()
        {
            l.SetLayerToCharacter();
            l.SetCharacterLayerToIgnoreRaycast();
        }
        
        #endregion

        #region properties

        public LayerMaskData Data => l;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }
        
        #endregion

        #endregion
    }
}
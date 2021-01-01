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

        [SerializeField] private GameObject character;
        [SerializeField] private LayerMaskSettings settings;
        private LayerMaskModel _model;

        #endregion

        #region internal

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<LayerMaskSettings>();
            _model = new LayerMaskModel(character, settings);
        }

        #endregion

        #endregion

        #endregion

        #region properties

        public LayerMaskData Data => _model.Data;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            _model.OnInitializeFrame();
        }

        #endregion

        #endregion
    }
}
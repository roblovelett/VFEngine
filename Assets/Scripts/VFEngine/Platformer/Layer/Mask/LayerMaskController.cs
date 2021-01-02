using UnityEngine;

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
        private LayerMaskModel LayerMask { get; set; }

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
            LayerMask = new LayerMaskModel(character, settings);
        }

        #endregion

        #endregion

        #endregion

        #region properties

        public LayerMaskData Data => LayerMask.Data;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            LayerMask.OnInitializeFrame();
        }

        public void OnPlatformerSetLayerToSaved()
        {
            LayerMask.OnSetLayerToSaved();
        }

        #endregion

        #endregion
    }
}
using UnityEngine;

namespace VFEngine.Platformer.Layer.Mask
{
    using static Physics2D;

    public class LayerMaskModel
    {
        #region fields

        #region internal

        private LayerMaskData LayerMask { get; }
        private GameObject Character => LayerMask.CharacterGameObject;
        private int SavedLayer => LayerMask.SavedLayer;

        #endregion

        #endregion

        #region properties

        public LayerMaskData Data => LayerMask;

        #region public methods

        #region constructors

        public LayerMaskModel(GameObject character, LayerMaskSettings settings)
        {
            LayerMask = new LayerMaskData(character, settings);
        }

        #endregion

        public void OnInitializeFrame()
        {
            LayerMask.SetSavedLayer(Character.layer);
            LayerMask.SetCharacterLayer(IgnoreRaycastLayer);
        }

        public void OnSetLayerToSaved()
        {
            LayerMask.SetCharacterLayer(SavedLayer);
        }

        #endregion

        #endregion
    }
}
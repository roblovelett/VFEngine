using UnityEngine;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer.Layer.Mask
{
    using static Physics2D;
    public class LayerMaskModel
    {
        #region fields
        
        #region internal

        private LayerMaskData LayerMask { get; }
        private GameObject Character => LayerMask.CharacterGameObject;

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

        public void SetSavedLayer()
        {
            LayerMask.SetSavedLayer(Character.layer);
        }

        public void SetCharacterToIgnoreRaycast()
        {
            LayerMask.SetCharacterLayer(IgnoreRaycastLayer);
        }

        #endregion

        #endregion
    }
}
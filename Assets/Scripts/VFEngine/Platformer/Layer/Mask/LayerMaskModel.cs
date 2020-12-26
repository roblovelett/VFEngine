using UnityEngine;

namespace VFEngine.Platformer.Layer.Mask
{
    public class LayerMaskModel
    {
        #region fields

        #endregion

        #region properties

        public LayerMaskData Data { get; }

        #region public methods

        #region constructors

        public LayerMaskModel(LayerMaskSettings settings, GameObject character)
        {
            Data = new LayerMaskData(settings, character);
        }

        public LayerMaskModel(LayerMaskSettings settings)
        {
            Data = new LayerMaskData(settings);
        }

        public LayerMaskModel(GameObject character)
        {
            Data = new LayerMaskData(character);
        }

        public LayerMaskModel()
        {
            Data = new LayerMaskData();
        }

        #endregion

        #endregion

        #endregion
    }
}
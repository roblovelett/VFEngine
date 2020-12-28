using UnityEngine;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer.Layer.Mask
{
    public class LayerMaskModel
    {
        #region fields

        private LayerMaskData layerMask;
        private PhysicsController physics;
        private PlatformerController platformer;

        #endregion

        #region properties

        public LayerMaskData Data => layerMask;

        #region public methods

        #region constructors

        public LayerMaskModel(GameObject character, LayerMaskSettings settings, PhysicsController physicsController,
            PlatformerController platformerController)
        {
            layerMask = new LayerMaskData(character, settings);
            physics = physicsController;
            platformer = platformerController;
        }

        #endregion

        #endregion

        #endregion
    }
}
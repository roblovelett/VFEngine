using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObject;

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private new Collider2D collider;
        [SerializeField] private RaycastSettings settings;
        private RaycastModel raycast;
        private LayerMaskController layerMaskController;
        private PhysicsController physicsController;
        private PlatformerController platformerController;

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
            layerMaskController = GetComponent<LayerMaskController>();
            physicsController = GetComponent<PhysicsController>();
            platformerController = GetComponent<PlatformerController>();
            if (!collider) collider = GetComponent<BoxCollider2D>();
            if (!settings) settings = CreateInstance<RaycastSettings>();
            raycast = new RaycastModel(collider, settings, layerMaskController, physicsController, platformerController);
        }

        #endregion

        #endregion

        #endregion

        #region properties

        public RaycastData Data => raycast.Data;
        
        #region public methods

        public void PlatformerInitializeFrame()
        {
            raycast.ResetCollision();
            raycast.SetBounds();
        }

        #endregion

        #endregion
    }
}
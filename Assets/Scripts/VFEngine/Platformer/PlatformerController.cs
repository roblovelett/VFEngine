using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

// ReSharper disable NotAccessedField.Local
namespace VFEngine.Platformer
{
    using static ScriptableObject;

    public class PlatformerController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerSettings settings;
        private PlatformerModel platformer;
        private RaycastController raycastController;
        private LayerMaskController layerMaskController;
        private PhysicsController physicsController;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            raycastController = GetComponent<RaycastController>();
            layerMaskController = GetComponent<LayerMaskController>();
            physicsController = GetComponent<PhysicsController>();
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            platformer = new PlatformerModel(settings, raycastController, layerMaskController, physicsController);
        }

        #endregion

        #endregion

        private void FixedUpdate()
        {
            platformer.Run();
        }

        #endregion

        #region properties

        public PlatformerData Data => platformer.Data;

        #region public methods

        #endregion

        #endregion
    }
}
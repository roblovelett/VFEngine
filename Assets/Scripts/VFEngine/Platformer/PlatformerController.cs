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
        private PlatformerModel _model;
        private RaycastController _raycastController;
        private LayerMaskController _layerMaskController;
        private PhysicsController _physicsController;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _raycastController = GetComponent<RaycastController>();
            _layerMaskController = GetComponent<LayerMaskController>();
            _physicsController = GetComponent<PhysicsController>();
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            _model = new PlatformerModel(settings, _raycastController, _layerMaskController, _physicsController);
        }

        #endregion

        #endregion

        private void FixedUpdate()
        {
            _model.Run();
        }

        #endregion

        #region properties

        public PlatformerData Data => _model.Data;

        #region public methods

        #endregion

        #endregion
    }
}
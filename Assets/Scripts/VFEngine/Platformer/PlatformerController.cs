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
        private PlatformerModel Platformer { get; set; }
        private RaycastController _raycast;
        private LayerMaskController _layerMask;
        private PhysicsController _physics;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _raycast = GetComponent<RaycastController>();
            _layerMask = GetComponent<LayerMaskController>();
            _physics = GetComponent<PhysicsController>();
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            Platformer = new PlatformerModel(settings, ref _raycast, ref _layerMask, ref _physics);
        }

        #endregion

        #endregion

        private void FixedUpdate()
        {
            Platformer.Run();
        }

        #endregion

        #region properties

        public PlatformerData Data => Platformer.Data;

        #region public methods

        #endregion

        #endregion
    }
}
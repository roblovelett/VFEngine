using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

namespace VFEngine.Platformer
{
    using static ScriptableObject;

    public class PlatformerController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerSettings settings;
        private PlatformerModel Platformer { get; set; }
        private RaycastController Raycast { get; set; }
        private LayerMaskController LayerMask { get; set; }
        private PhysicsController Physics { get; set; }

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            Platformer = new PlatformerModel(settings);
        }

        private void Start()
        {
            SetControllers();
            SetDependencies();
        }

        private void SetControllers()
        {
            Raycast = GetComponent<RaycastController>();
            LayerMask = GetComponent<LayerMaskController>();
            Physics = GetComponent<PhysicsController>();
        }

        private void SetDependencies()
        {
            Platformer.SetDependencies(Raycast, LayerMask, Physics);
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
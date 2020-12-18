using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer
{
    using static ScriptableObject;

    public class PlatformerController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerSettings settings;
        private RaycastController raycastController;
        private UpRaycastHitColliderController upRaycastHitColliderController;
        private RightRaycastHitColliderController rightRaycastHitColliderController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private LeftRaycastHitColliderController leftRaycastHitColliderController;
        private PhysicsController physicsController;
        private LayerMaskController layerMaskController;
        private PlatformerData p;
        private RaycastData raycast;
        private UpRaycastHitColliderData upRaycastHitCollider;
        private RightRaycastHitColliderData rightRaycastHitCollider;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private LeftRaycastHitColliderData leftRaycastHitCollider;
        private PhysicsData physics;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        #region initialization
        
        private void Awake()
        {
            InitializeData();
            SetControllers();
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            p = new PlatformerData();
            p.ApplySettings(settings);
            p.Initialize();
        }

        private void SetControllers()
        {
            physicsController = GetComponent<PhysicsController>();
            raycastController = GetComponent<RaycastController>();
            upRaycastHitColliderController = GetComponent<UpRaycastHitColliderController>();
            rightRaycastHitColliderController = GetComponent<RightRaycastHitColliderController>();
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
            leftRaycastHitColliderController = GetComponent<LeftRaycastHitColliderController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            raycast = raycastController.Data;
            upRaycastHitCollider = upRaycastHitColliderController.Data;
            rightRaycastHitCollider = rightRaycastHitColliderController.Data;
            downRaycastHitCollider = downRaycastHitColliderController.Data;
            leftRaycastHitCollider = leftRaycastHitColliderController.Data;
            layerMask = layerMaskController.Data;
        }

        private void Initialize()
        {
            
        }
        
        #endregion

        #endregion

        private void FixedUpdate()
        {
            Platformer();
        }

        private void Platformer()
        {
            InitializeFrame();
            SetSlopes();
            SetExternalForce();
            SetGravity();
        }

        #region initialize frame
        
        private void InitializeFrame()
        {
            InitializeRaycastHitColliders();
            InitializeLayerMask();
            InitializeRaycast();
            InitializePhysics();
        }

        private void InitializeRaycastHitColliders()
        {
            upRaycastHitColliderController.OnPlatformerInitializeFrame();
            rightRaycastHitColliderController.OnPlatformerInitializeFrame();
            downRaycastHitColliderController.OnPlatformerInitializeFrame();
            leftRaycastHitColliderController.OnPlatformerInitializeFrame();
        }

        private void InitializeLayerMask()
        {
            layerMaskController.OnPlatformerInitializeFrame();
        }

        private void InitializeRaycast()
        {
            raycastController.OnPlatformerInitializeFrame();
        }

        private void InitializePhysics()
        {
            physicsController.OnPlatformerInitializeFrame();
        }
        
        #endregion

        private void SetSlopes()
        {
            
        }
        
        #endregion

        #region properties

        public PlatformerData Data => p;

        #region public methods

        #endregion

        #endregion
    }
}
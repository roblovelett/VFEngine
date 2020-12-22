using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
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
        private DownRaycastController downRaycastController;
        private UpRaycastHitColliderController upRaycastHitColliderController;
        private RightRaycastHitColliderController rightRaycastHitColliderController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private LeftRaycastHitColliderController leftRaycastHitColliderController;
        private PhysicsController physicsController;
        private LayerMaskController layerMaskController;
        private PlatformerData p;
        private RaycastData raycast;
        private DownRaycastData downRaycast;
        private UpRaycastHitColliderData upRaycastHitCollider;
        private RightRaycastHitColliderData rightRaycastHitCollider;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private LeftRaycastHitColliderData leftRaycastHitCollider;
        private PhysicsData physics;
        private LayerMaskData layerMask;

        #endregion
        
        #region internal

        private bool HorizontalMovement => physics.DeltaMove.x != 0;
        private bool NegativeVerticalMovement => physics.DeltaMove.y <= 0;
        private bool OnSlope => downRaycastHitCollider.Collision.OnSlope;
        private bool OnSlopes => NegativeVerticalMovement && OnSlope;
        private bool DescendingSlope => GroundDirection == HorizontalMovementDirection;
        private int GroundDirection => downRaycastHitCollider.Collision.groundDirection;
        private int HorizontalMovementDirection => physics.HorizontalMovementDirection;
        
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
            p = new PlatformerData();
            p.InitializeData();
        }

        private void SetControllers()
        {
            physicsController = GetComponent<PhysicsController>();
            raycastController = GetComponent<RaycastController>();
            downRaycastController = GetComponent<DownRaycastController>();
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
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            physics = physicsController.Data;
            raycast = raycastController.Data;
            downRaycast = downRaycastController.Data;
            upRaycastHitCollider = upRaycastHitColliderController.Data;
            rightRaycastHitCollider = rightRaycastHitColliderController.Data;
            downRaycastHitCollider = downRaycastHitColliderController.Data;
            leftRaycastHitCollider = leftRaycastHitColliderController.Data;
            layerMask = layerMaskController.Data;
        }

        private void Initialize()
        {
            p.Initialize(settings);
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
            CastRaysDown();
            SetExternalForce();
            SetGravity();
            SetHorizontalExternalForce();
            CheckSlopes();
        }

        #region initialize frame
        
        private void InitializeFrame()
        {
            InitializeRaycastHitColliders();
            InitializeRaycast();
        }

        private void InitializeRaycastHitColliders()
        {
            upRaycastHitColliderController.OnPlatformerInitializeFrame();
            rightRaycastHitColliderController.OnPlatformerInitializeFrame();
            downRaycastHitColliderController.OnPlatformerInitializeFrame();
            leftRaycastHitColliderController.OnPlatformerInitializeFrame();
        }

        private void InitializeRaycast()
        {
            raycastController.OnPlatformerInitializeFrame();
        }

        #endregion

        private void CastRaysDown()
        {
            raycastController.OnPlatformerCastRaysDown();
        }
        
        private void SetExternalForce()
        {
            physicsController.OnPlatformerSetExternalForce();
        }

        private void SetGravity()
        {
            physicsController.OnPlatformerSetGravity();
        }

        private void SetHorizontalExternalForce()
        {
            physicsController.OnPlatformerSetHorizontalExternalForce();
        }

        private void CheckSlopes()
        {
            if (!HorizontalMovement) return;
            if (OnSlopes)
            {
                if (DescendingSlope) DescendSlope();
                else ClimbSlope();
            }
            SetHorizontalCollisions();
        }

        private void DescendSlope()
        {
            
        }

        private void ClimbSlope()
        {
            
        }

        private void SetHorizontalCollisions()
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
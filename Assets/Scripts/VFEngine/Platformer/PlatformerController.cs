using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer
{
    using static ScriptableObject;

    public class PlatformerController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerSettings settings;
        private Platformer platformer;
        //private RaycastController raycastController;
        //private PhysicsController physicsController;
        //private LayerMaskController layerMaskController;
        //private RaycastData raycast;
        //private PhysicsData physics;
        //private LayerMaskData layerMask;

        #endregion
        
        #region internal

        //private bool HorizontalMovement => physics.DeltaMovement.x != 0;
        //private bool NegativeVerticalMovement => physics.DeltaMovement.y <= 0;
        //private bool OnSlope => downRaycastHitCollider.Collision.OnSlope;
        //private bool OnSlopes => NegativeVerticalMovement && OnSlope;
        //private bool DescendingSlope => GroundDirection == HorizontalMovementDirection;
        //private int GroundDirection => downRaycastHitCollider.Collision.groundDirection;
        //private int HorizontalMovementDirection => physics.HorizontalMovementDirection;
        
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
            platformer = new Platformer(settings);
        }

        private void Start()
        {
            SetControllers();
            SetDependencies();
        }

        private void SetControllers()
        {
            //physicsController = GetComponent<PhysicsController>();
            //raycastController = GetComponent<RaycastController>();
            //layerMaskController = GetComponent<LayerMaskController>();
        }
        
        private void SetDependencies()
        {
            //physics = physicsController.Data;
            //raycast = raycastController.Data;
            //layerMask = layerMaskController.Data;
        }

        #endregion

        #endregion

        private void FixedUpdate()
        {
            Platformer();
        }

        private void Platformer()
        {
            /*InitializeFrame();
            CastRaysDown();
            SetExternalForce();
            SetGravity();
            SetHorizontalExternalForce();
            CheckSlopes();
            CastRaysToSides();*/
        }

        #region initialize frame
        
        /*private void InitializeFrame()
        {
            InitializeRaycastHitColliders();
            InitializeRaycast();
        }

        private void InitializeRaycastHitColliders()
        {
            
        }

        private void InitializeRaycast()
        {
            raycastController.OnPlatformerInitializeFrame();
        }*/

        #endregion

        /*private void CastRaysDown()
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
            //if (!HorizontalMovement || !OnSlopes) return;
            //if (DescendingSlope) DescendSlope();
            //else ClimbSlope();
        }

        private void DescendSlope()
        {
            physicsController.OnPlatformerDescendSlope();
            //downRaycastHitColliderController.OnPlatformerDescendSlope();
        }

        private void ClimbSlope()
        {
            physicsController.OnPlatformerClimbSlope();
            //downRaycastHitColliderController.OnPlatformerClimbSlope();
        }

        private void CastRaysToSides()
        {
            raycastController.OnPlatformerCastRaysToSides();
        }*/
        
        #endregion

        #region properties

        public PlatformerData Data => platformer.data;

        #region public methods

        #endregion

        #endregion
    }
}
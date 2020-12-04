using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static Raycast;
    using static DebugExtensions;
    using static Color;    
    using static Vector2;
    public class LeftRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private LeftRaycastHitColliderController leftRaycastHitColliderController;
        private LayerMaskController layerMaskController;
        private LeftRaycastData l;
        private PhysicsData physics;
        private RaycastData raycast;
        private LeftRaycastHitColliderData leftRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void Awake()
        { 
            SetControllers();
            InitializeData();
        }
        
        private void SetControllers()
        {
            raycastController = GetComponent<RaycastController>();
            physicsController = GetComponent<PhysicsController>();
            leftRaycastHitColliderController = GetComponent<LeftRaycastHitColliderController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }
        
        private void InitializeData()
        {
            l = new LeftRaycastData
            {
                CurrentLeftRaycastOrigin = zero,
                LeftRaycastFromBottomOrigin = zero,
                LeftRaycastToTopOrigin = zero
            };
        }
        
        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            raycast = raycastController.Data;
            leftRaycastHitCollider = leftRaycastHitColliderController.Data;
            layerMask = layerMaskController.Data;
        }

        private void SetLeftRaycastFromBottomOrigin()
        {
            l.LeftRaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(raycast.BoundsBottomRightCorner,
                raycast.BoundsBottomLeftCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetLeftRaycastToTopOrigin()
        {
            l.LeftRaycastToTopOrigin = OnSetRaycastToTopOrigin(raycast.BoundsTopLeftCorner,
                raycast.BoundsTopRightCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetCurrentLeftRaycastOrigin()
        {
            l.CurrentLeftRaycastOrigin = OnSetCurrentRaycastOrigin(l.LeftRaycastFromBottomOrigin,
                l.LeftRaycastToTopOrigin, leftRaycastHitCollider.CurrentLeftHitsStorageIndex,
                raycast.NumberOfHorizontalRaysPerSide);
        }

        private void InitializeLeftRaycastLength()
        {
            l.LeftRayLength = OnSetHorizontalRayLength(physics.Speed.x, raycast.BoundsWidth, raycast.RayOffset);
        }

        private void SetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -physics.Transform.right, l.LeftRayLength,
                layerMask.PlatformMask, red, raycast.DrawRaycastGizmosControl);
        }

        private void SetCurrentLeftRaycast()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -physics.Transform.right, l.LeftRayLength,
                layerMask.PlatformMask & ~layerMask.OneWayPlatformMask & ~layerMask.MovingOneWayPlatformMask, red,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public LeftRaycastData Data => l;

        #region public methods

        public void OnSetLeftRaycastFromBottomOrigin()
        {
            SetLeftRaycastFromBottomOrigin();
        }

        public void OnSetLeftRaycastToTopOrigin()
        {
            SetLeftRaycastToTopOrigin();
        }

        public void OnInitializeLeftRaycastLength()
        {
            InitializeLeftRaycastLength();
        }

        public void OnSetCurrentLeftRaycastOrigin()
        {
            SetCurrentLeftRaycastOrigin();
        }

        public void OnSetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentLeftRaycast()
        {
            SetCurrentLeftRaycast();
        }

        #endregion

        #endregion
    }
}
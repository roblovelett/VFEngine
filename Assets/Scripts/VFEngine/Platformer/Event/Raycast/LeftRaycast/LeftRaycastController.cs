using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static Raycast;
    using static Color;
    using static Vector2;
    using static Time;

    public class LeftRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private LeftRaycastHitColliderController leftRaycastHitColliderController;
        private LayerMaskController layerMaskController;
        private LeftRaycastData l;
        private PhysicsData physics;
        private RaycastData raycast;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private LeftRaycastHitColliderData leftRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion
        
        private bool ExcludeOneWayPlatformsFromRaycast => downRaycastHitCollider.HasGroundedLastFrame &&
                                                          leftRaycastHitCollider.CurrentHitsStorageIndex == 0;

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
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
            leftRaycastHitColliderController = GetComponent<LeftRaycastHitColliderController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }

        private void InitializeData()
        {
            l = new LeftRaycastData
            {
                CurrentRaycastOrigin = zero, RaycastFromBottomOrigin = zero, RaycastToTopOrigin = zero
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
            downRaycastHitCollider = downRaycastHitColliderController.Data;
            leftRaycastHitCollider = leftRaycastHitColliderController.Data;
            layerMask = layerMaskController.Data;
        }

        private void PlatformerSetRaycast()
        {
            SetRaycastFromBottomOrigin();
            SetRaycastToTopOrigin();
            SetRaycastLength();
        }

        private void SetRaycastFromBottomOrigin()
        {
            l.RaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(raycast.BoundsBottomRightCorner,
                raycast.BoundsBottomLeftCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetRaycastToTopOrigin()
        {
            l.RaycastToTopOrigin = OnSetRaycastToTopOrigin(raycast.BoundsTopLeftCorner, raycast.BoundsTopRightCorner,
                physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetRaycastLength()
        {
            l.RayLength = OnSetHorizontalRayLength(physics.Speed.x, deltaTime, raycast.BoundsWidth, raycast.RayOffset);
        }

        private void PlatformerSetCurrentRaycast()
        {
            SetCurrentRaycastOrigin();
            SetCurrentRaycast();
        }

        private void SetCurrentRaycastOrigin()
        {
            l.CurrentRaycastOrigin = OnSetCurrentRaycastOrigin(l.RaycastFromBottomOrigin, l.RaycastToTopOrigin,
                leftRaycastHitCollider.CurrentHitsStorageIndex, raycast.NumberOfHorizontalRaysPerSide);
        }

        private void SetCurrentRaycast()
        {
            if (ExcludeOneWayPlatformsFromRaycast) SetCurrentRaycastToIgnoreOneWayPlatform();
            else SetCurrentRaycastWithLayerMasks();
        }

        private void SetCurrentRaycastToIgnoreOneWayPlatform()
        {
            l.CurrentRaycastHit = OnSetRaycast(l.CurrentRaycastOrigin, -physics.Transform.right, l.RayLength,
                layerMask.PlatformMask, red, raycast.DrawRaycastGizmosControl);
        }

        private void SetCurrentRaycastWithLayerMasks()
        {
            l.CurrentRaycastHit = OnSetRaycast(l.CurrentRaycastOrigin, -physics.Transform.right, l.RayLength,
                layerMask.PlatformMask & ~layerMask.OneWayPlatformMask & ~layerMask.MovingOneWayPlatformMask, red,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public LeftRaycastData Data => l;

        #region public methods

        #region platformer

        public void OnPlatformerSetRaycast()
        {
            PlatformerSetRaycast();
        }

        public void OnPlatformerSetCurrentRaycast()
        {
            PlatformerSetCurrentRaycast();
        }

        #endregion

        #endregion

        #endregion
    }
}
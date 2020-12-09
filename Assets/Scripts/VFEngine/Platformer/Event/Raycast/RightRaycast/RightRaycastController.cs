using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static Raycast;
    using static Color;
    using static Vector2;
    using static Time;

    public class RightRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private RightRaycastHitColliderController rightRaycastHitColliderController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private LayerMaskController layerMaskController;
        private RightRaycastData r;
        private PhysicsData physics;
        private RaycastData raycast;
        private RightRaycastHitColliderData rightRaycastHitCollider;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        private bool ExcludeOneWayPlatformsFromRaycast => downRaycastHitCollider.HasGroundedLastFrame &&
                                                          rightRaycastHitCollider.CurrentHitsStorageIndex == 0;

        #region private methods

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            physicsController = GetComponent<PhysicsController>();
            raycastController = GetComponent<RaycastController>();
            rightRaycastHitColliderController = GetComponent<RightRaycastHitColliderController>();
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }

        private void InitializeData()
        {
            r = new RightRaycastData
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
            rightRaycastHitCollider = rightRaycastHitColliderController.Data;
            downRaycastHitCollider = downRaycastHitColliderController.Data;
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
            r.RaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(raycast.BoundsBottomRightCorner,
                raycast.BoundsBottomLeftCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetRaycastToTopOrigin()
        {
            r.RaycastToTopOrigin = OnSetRaycastToTopOrigin(raycast.BoundsTopLeftCorner, raycast.BoundsTopRightCorner,
                physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetRaycastLength()
        {
            r.RayLength = OnSetHorizontalRayLength(physics.Speed.x, deltaTime, raycast.BoundsWidth, raycast.RayOffset);
        }

        private void PlatformerSetCurrentRaycast()
        {
            SetCurrentRightRaycastOrigin();
            SetCurrentRaycast();
        }

        private void SetCurrentRightRaycastOrigin()
        {
            r.CurrentRaycastOrigin = OnSetCurrentRaycastOrigin(r.RaycastFromBottomOrigin, r.RaycastToTopOrigin,
                rightRaycastHitCollider.CurrentHitsStorageIndex, raycast.NumberOfHorizontalRaysPerSide);
        }

        private void SetCurrentRaycast()
        {
            if (ExcludeOneWayPlatformsFromRaycast) SetCurrentRaycastToIgnoreOneWayPlatform();
            else SetCurrentRaycastWithLayerMasks();
        }

        private void SetCurrentRaycastToIgnoreOneWayPlatform()
        {
            r.CurrentRaycastHit = OnSetRaycast(r.CurrentRaycastOrigin, physics.Transform.right, r.RayLength,
                layerMask.PlatformMask, blue, raycast.DrawRaycastGizmosControl);
        }

        private void SetCurrentRaycastWithLayerMasks()
        {
            r.CurrentRaycastHit = OnSetRaycast(r.CurrentRaycastOrigin, physics.Transform.right, r.RayLength,
                layerMask.PlatformMask & ~layerMask.OneWayPlatformMask & ~layerMask.MovingOneWayPlatformMask, blue,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public RightRaycastData Data => r;

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
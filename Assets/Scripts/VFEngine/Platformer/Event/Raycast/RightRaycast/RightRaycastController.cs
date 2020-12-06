﻿using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static Raycast;
    using static DebugExtensions;
    using static Color;
    using static Vector2;

    public class RightRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private RightRaycastHitColliderController rightRaycastHitColliderController;
        private LayerMaskController layerMaskController;
        private RightRaycastData r;
        private PhysicsData physics;
        private RaycastData raycast;
        private RightRaycastHitColliderData rightRaycastHitCollider;
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
            physicsController = GetComponent<PhysicsController>();
            raycastController = GetComponent<RaycastController>();
            rightRaycastHitColliderController = GetComponent<RightRaycastHitColliderController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }
        
        private void InitializeData()
        {
            r = new RightRaycastData
            {
                CurrentRightRaycastOrigin = zero,
                RightRaycastFromBottomOrigin = zero,
                RightRaycastToTopOrigin = zero
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
            layerMask = layerMaskController.Data;
        }

        private void SetRightRaycastFromBottomOrigin()
        {
            r.RightRaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(raycast.BoundsBottomRightCorner,
                raycast.BoundsBottomLeftCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetRightRaycastToTopOrigin()
        {
            r.RightRaycastToTopOrigin = OnSetRaycastToTopOrigin(raycast.BoundsTopLeftCorner,
                raycast.BoundsTopRightCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void InitializeRightRaycastLength()
        {
            r.RightRayLength = OnSetHorizontalRayLength(physics.Speed.x, raycast.BoundsWidth, raycast.RayOffset);
        }

        private void SetCurrentRightRaycastOrigin()
        {
            r.CurrentRightRaycastOrigin = OnSetCurrentRaycastOrigin(r.RightRaycastFromBottomOrigin,
                r.RightRaycastToTopOrigin, rightRaycastHitCollider.CurrentRightHitsStorageIndex,
                raycast.NumberOfHorizontalRaysPerSide);
        }

        private void SetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            r.CurrentRightRaycastHit = Raycast(r.CurrentRightRaycastOrigin, physics.Transform.right, r.RightRayLength,
                layerMask.PlatformMask, red, raycast.DrawRaycastGizmosControl);
        }

        private void SetCurrentRightRaycast()
        {
            r.CurrentRightRaycastHit = Raycast(r.CurrentRightRaycastOrigin, physics.Transform.right, r.RightRayLength,
                layerMask.PlatformMask & ~layerMask.OneWayPlatformMask & ~layerMask.MovingOneWayPlatformMask, red,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public RightRaycastData Data => r;

        #region public methods

        public void OnSetRightRaycastFromBottomOrigin()
        {
            SetRightRaycastFromBottomOrigin();
        }

        public void OnSetRightRaycastToTopOrigin()
        {
            SetRightRaycastToTopOrigin();
        }

        public void OnInitializeRightRaycastLength()
        {
            InitializeRightRaycastLength();
        }

        public void OnSetCurrentRightRaycastOrigin()
        {
            SetCurrentRightRaycastOrigin();
        }

        public void OnSetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentRightRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentRightRaycast()
        {
            SetCurrentRightRaycast();
        }

        #endregion

        #endregion
    }
}
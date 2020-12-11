using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static Mathf;
    using static Raycast;
    using static Vector2;
    using static Color;

    public class DownRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private LayerMaskController layerMaskController;
        private DownRaycastData d;
        private PhysicsData physics;
        private RaycastData raycast;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region internal

        private bool IsNotCollidingBelow => physics.Gravity > 0 && !physics.IsFalling;
        private bool OnMovingPlatform => downRaycastHitCollider.OnMovingPlatform;
        private bool NegativeVerticalNewPosition => physics.NewPosition.y < 0;

        private bool ExcludeOneWayPlatformsFromRaycast =>
            physics.NewPosition.y > 0 && !downRaycastHitCollider.WasGroundedLastFrame;

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
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }

        private void InitializeData()
        {
            d = new DownRaycastData
            {
                CurrentRaycastOrigin = zero, RaycastFromLeftOrigin = zero, RaycastToRightOrigin = zero
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
            layerMask = layerMaskController.Data;
        }

        #region platformer

        private void PlatformerCastRaysDown()
        {
            if (IsNotCollidingBelow) return;
            SetRaycastLength();
            if (OnMovingPlatform) DoubleRayLength();
            if (NegativeVerticalNewPosition) SetRayLengthToVerticalNewPosition();
            SetRaycastFromLeftOrigin();
            SetRaycastToRightOrigin();
        }

        private void PlatformerCastCurrentRay()
        {
            SetCurrentRaycastOrigin();
            SetCurrentRaycast();
        }

        #endregion

        private void SetRaycastLength()
        {
            d.RayLength = OnSetRayLength(raycast.BoundsHeight, raycast.RayOffset);
        }

        private void DoubleRayLength()
        {
            d.RayLength *= 2;
        }

        private void SetRayLengthToVerticalNewPosition()
        {
            d.RayLength += Abs(physics.NewPosition.y);
        }

        private void SetRaycastFromLeftOrigin()
        {
            d.RaycastFromLeftOrigin = OnSetDownRaycastFromLeftOrigin(raycast.BoundsBottomLeftCorner,
                raycast.BoundsTopLeftCorner, physics.Transform, raycast.RayOffset, physics.NewPosition);
        }

        private void SetRaycastToRightOrigin()
        {
            d.RaycastToRightOrigin = OnSetDownRaycastToRightOrigin(raycast.BoundsBottomRightCorner,
                raycast.BoundsTopRightCorner, physics.Transform, raycast.RayOffset, physics.NewPosition);
        }

        private void SetCurrentRaycastOrigin()
        {
            d.CurrentRaycastOrigin = OnSetCurrentRaycastOrigin(d.RaycastFromLeftOrigin, d.RaycastToRightOrigin,
                downRaycastHitCollider.CurrentHitsStorageIndex, raycast.NumberOfVerticalRaysPerSide);
        }

        private void SetCurrentRaycast()
        {
            if (ExcludeOneWayPlatformsFromRaycast) SetCurrentRaycastToIgnoreOneWayPlatform();
            else SetCurrentRaycastToIncludePlatforms();
        }

        private void SetCurrentRaycastToIgnoreOneWayPlatform()
        {
            d.CurrentRaycast = OnSetRaycast(d.CurrentRaycastOrigin, -physics.Transform.up, d.RayLength,
                layerMask.RaysBelowPlatformsWithoutOneWay, blue, raycast.DrawRaycastGizmosControl);
        }

        private void SetCurrentRaycastToIncludePlatforms()
        {
            d.CurrentRaycast = OnSetRaycast(d.CurrentRaycastOrigin, -physics.Transform.up, d.RayLength,
                layerMask.RaysBelowPlatforms, blue, raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public DownRaycastData Data => d;

        #region public methods

        #region platformer

        public void OnPlatformerCastRaysDown()
        {
            PlatformerCastRaysDown();
        }

        public void OnPlatformerCastCurrentRay()
        {
            PlatformerCastCurrentRay();
        }

        #endregion

        #endregion

        #endregion
    }
}
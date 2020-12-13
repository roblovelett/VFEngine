using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static Mathf;
    using static Raycast;
    using static Vector2;
    using static Color;
    using static RaycastDirection;
    using static DebugExtensions;

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

        private bool CastingDown => raycast.CurrentRaycastDirection == Down;
        private bool IsNotCollidingBelow => physics.Gravity > 0 && !physics.IsFalling;
        private bool OnMovingPlatform => downRaycastHitCollider.OnMovingPlatform;
        private bool NegativeVerticalNewPosition => physics.NewPosition.y < 0;
        private void PlatformerCastRaysDown()
        {
            if (!CastingDown || IsNotCollidingBelow) return;
            SetRaycastLength();
            if (OnMovingPlatform) DoubleRayLength();
            if (NegativeVerticalNewPosition) SetRayLengthToVerticalNewPosition();
            SetRaycastsOrigins();
        }

        private void PlatformerCastCurrentRay()
        {
            SetCurrentRaycastOrigin();
            SetCurrentRaycast();
        }

        #endregion

        private void SetRaycastLength()
        {
            d.RayLength = raycast.BoundsHeight / 2 + raycast.RayOffset;
        }

        private void DoubleRayLength()
        {
            d.RayLength *= 2;
        }

        private void SetRayLengthToVerticalNewPosition()
        {
            d.RayLength += Abs(physics.NewPosition.y);
        }

        private void SetRaycastsOrigins()
        {
            var up = physics.Transform.up;
            var right = transform.right;
            d.RaycastFromLeftOrigin = (raycast.BoundsBottomLeftCorner + raycast.BoundsTopLeftCorner) / 2;
            d.RaycastToRightOrigin = (raycast.BoundsBottomRightCorner + raycast.BoundsTopRightCorner) / 2;
            d.RaycastFromLeftOrigin += (Vector2) up * raycast.RayOffset;
            d.RaycastToRightOrigin += (Vector2) up * raycast.RayOffset;
            d.RaycastFromLeftOrigin += (Vector2) right * physics.NewPosition.x;
            d.RaycastToRightOrigin += (Vector2) right * physics.NewPosition.y;
        }

        /*private void SetRaycastFromLeftOrigin()
        {
            d.RaycastFromLeftOrigin = OnSetDownRaycastFromLeftOrigin(raycast.BoundsBottomLeftCorner,
                raycast.BoundsTopLeftCorner, physics.Transform, raycast.RayOffset, physics.NewPosition);
        }*/

        /*private void SetRaycastToRightOrigin()
        {
            d.RaycastToRightOrigin = OnSetDownRaycastToRightOrigin(raycast.BoundsBottomRightCorner,
                raycast.BoundsTopRightCorner, physics.Transform, raycast.RayOffset, physics.NewPosition);
        }*/

        private void SetCurrentRaycastOrigin()
        {
            d.CurrentRaycastOrigin = Lerp(d.RaycastFromLeftOrigin, d.RaycastToRightOrigin, downRaycastHitCollider.HitsStorageIndex / (float) (raycast.NumberOfVerticalRaysPerSide - 1));
            /*d.CurrentRaycastOrigin = OnSetCurrentRaycastOrigin(d.RaycastFromLeftOrigin, d.RaycastToRightOrigin,
                downRaycastHitCollider.HitsStorageIndex, raycast.NumberOfVerticalRaysPerSide);*/
        }
        private bool ExcludeOneWayPlatformsFromRaycast => physics.NewPosition.y > 0 && !downRaycastHitCollider.WasGroundedLastFrame;
        private void SetCurrentRaycast()
        {
            if (ExcludeOneWayPlatformsFromRaycast) SetCurrentRaycastToIgnoreOneWayPlatform();
            else SetCurrentRaycastToIncludePlatforms();
        }

        private void SetCurrentRaycastToIgnoreOneWayPlatform()
        {
            d.CurrentRaycast = RayCast(d.CurrentRaycastOrigin, -physics.Transform.up, d.RayLength,
                layerMask.RaysBelowPlatformsWithoutOneWay, green, raycast.DrawRaycastGizmosControl);
        }

        private void SetCurrentRaycastToIncludePlatforms()
        {
            d.CurrentRaycast = RayCast(d.CurrentRaycastOrigin, -physics.Transform.up, d.RayLength,
                layerMask.RaysBelowPlatforms, cyan, raycast.DrawRaycastGizmosControl);
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
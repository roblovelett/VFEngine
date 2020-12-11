using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static Mathf;
    using static Raycast;
    using static UniTaskExtensions;
    using static Vector2;

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

        private bool IsNotCollidingBelow => physics.Gravity > 0 && !physics.IsFalling;
        private bool OnMovingPlatform => downRaycastHitCollider.OnMovingPlatform;
        private bool NegativeVerticalNewPosition => physics.NewPosition.y < 0;

        private void PlatformerCastRaysDown()
        {
            if (IsNotCollidingBelow) return;
            SetRaycastLength();
            if (OnMovingPlatform) DoubleRayLength();
            if (NegativeVerticalNewPosition) SetRayLengthToVerticalNewPosition();
            SetRaycastFromLeftOrigin();
            SetRaycastToRightOrigin();
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

        //private void SetCurrentDownRaycastToIgnoreOneWayPlatform()
        //{
        /*d.CurrentDownRaycastHit = Raycast(d.CurrentDownRaycastOrigin, -physics.Transform.up, d.DownRayLength,
            layerMask.RaysBelowLayerMaskPlatformsWithoutOneWay, blue, raycast.DrawRaycastGizmosControl);*/
        //}

        //private void SetCurrentDownRaycast()
        //{
        /*d.CurrentDownRaycastHit = Raycast(d.CurrentDownRaycastOrigin, -physics.Transform.up, d.DownRayLength,
            layerMask.RaysBelowLayerMaskPlatforms, blue, raycast.DrawRaycastGizmosControl);*/
        //}
        /*private void SetDownRaycastFromLeft()
        {
            d.DownRaycastFromLeft = OnSetVerticalRaycast(raycast.BoundsBottomLeftCorner, raycast.BoundsTopLeftCorner,
                physics.Transform, raycast.RayOffset, physics.NewPosition.x);
        }

        private void SetDownRaycastToRight()
        {
            d.DownRaycastToRight = OnSetVerticalRaycast(raycast.BoundsBottomRightCorner, raycast.BoundsTopRightCorner,
                physics.Transform, raycast.RayOffset, physics.NewPosition.x);
        }*/
        /*private void SetCurrentDownRaycastOriginPoint()
        {
            d.CurrentRaycastOrigin = OnSetCurrentRaycastOrigin(d.DownRaycastFromLeft, d.RaycastToRightOrigin,
                downRaycastHitCollider.CurrentDownHitsStorageIndex, raycast.NumberOfVerticalRaysPerSide);
        }*/

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
            //PlatformerCastCurrentRay();
        }

        #endregion

        public void OnSetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            //SetCurrentDownRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentDownRaycast()
        {
            //SetCurrentDownRaycast();
        }

        public async UniTaskVoid OnInitializeDownRayLength()
        {
            //InitializeDownRayLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnDoubleDownRayLength()
        {
            //DoubleDownRayLength();
        }

        public void OnSetDownRayLengthToVerticalNewPosition()
        {
            //SetDownRayLengthToVerticalNewPosition();
        }

        /*public async UniTaskVoid OnSetDownRaycastFromLeft()
        {
            SetDownRaycastFromLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetDownRaycastToRight()
        {
            SetDownRaycastToRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }*/

        public void OnSetCurrentDownRaycastOriginPoint()
        {
            //SetCurrentDownRaycastOriginPoint();
        }

        #endregion

        #endregion
    }
}
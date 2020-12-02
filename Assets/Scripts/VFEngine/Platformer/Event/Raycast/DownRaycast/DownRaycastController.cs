using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static DebugExtensions;
    using static Color;
    using static Mathf;
    using static Raycast;
    using static UniTaskExtensions;

    public class DownRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
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
            LoadCharacter();
            InitializeData();
            SetControllers();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            d = new DownRaycastData();
        }

        private void SetControllers()
        {
            physicsController = character.GetComponentNoAllocation<PhysicsController>();
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            downRaycastHitColliderController = character.GetComponentNoAllocation<DownRaycastHitColliderController>();
            layerMaskController = character.GetComponentNoAllocation<LayerMaskController>();
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

        private void SetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            d.CurrentDownRaycastHit = Raycast(d.CurrentDownRaycastOrigin, -physics.Transform.up, d.DownRayLength,
                layerMask.RaysBelowLayerMaskPlatformsWithoutOneWay, blue, raycast.DrawRaycastGizmosControl);
        }

        private void SetCurrentDownRaycast()
        {
            d.CurrentDownRaycastHit = Raycast(d.CurrentDownRaycastOrigin, -physics.Transform.up, d.DownRayLength,
                layerMask.RaysBelowLayerMaskPlatforms, blue, raycast.DrawRaycastGizmosControl);
        }

        private void InitializeDownRayLength()
        {
            d.DownRayLength = raycast.BoundsHeight / 2 + raycast.RayOffset;
        }

        private void DoubleDownRayLength()
        {
            d.DownRayLength *= 2;
        }

        private void SetDownRayLengthToVerticalNewPosition()
        {
            d.DownRayLength += Abs(physics.NewPosition.y);
        }

        private void SetDownRaycastFromLeft()
        {
            d.DownRaycastFromLeft = OnSetVerticalRaycast(raycast.BoundsBottomLeftCorner, raycast.BoundsTopLeftCorner,
                physics.Transform, raycast.RayOffset, physics.NewPosition.x);
        }

        private void SetDownRaycastToRight()
        {
            d.DownRaycastToRight = OnSetVerticalRaycast(raycast.BoundsBottomRightCorner, raycast.BoundsTopRightCorner,
                physics.Transform, raycast.RayOffset, physics.NewPosition.x);
        }

        private void SetCurrentDownRaycastOriginPoint()
        {
            d.CurrentDownRaycastOrigin = OnSetCurrentRaycastOrigin(d.DownRaycastFromLeft, d.DownRaycastToRight,
                downRaycastHitCollider.CurrentDownHitsStorageIndex, raycast.NumberOfVerticalRaysPerSide);
        }

        #endregion

        #endregion

        #region properties

        public DownRaycastData Data => d;

        #region public methods

        public void OnSetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentDownRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentDownRaycast()
        {
            SetCurrentDownRaycast();
        }

        public async UniTaskVoid OnInitializeDownRayLength()
        {
            InitializeDownRayLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnDoubleDownRayLength()
        {
            DoubleDownRayLength();
        }

        public void OnSetDownRayLengthToVerticalNewPosition()
        {
            SetDownRayLengthToVerticalNewPosition();
        }

        public async UniTaskVoid OnSetDownRaycastFromLeft()
        {
            SetDownRaycastFromLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetDownRaycastToRight()
        {
            SetDownRaycastToRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetCurrentDownRaycastOriginPoint()
        {
            SetCurrentDownRaycastOriginPoint();
        }

        #endregion

        #endregion
    }
}
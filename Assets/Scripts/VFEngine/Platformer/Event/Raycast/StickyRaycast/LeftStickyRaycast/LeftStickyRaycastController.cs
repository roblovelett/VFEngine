using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static StickyRaycast;
    using static DebugExtensions;
    using static Color;
    using static UniTaskExtensions;
    using static Vector2;

    public class LeftStickyRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private StickyRaycastController stickyRaycastController;
        private LayerMaskController layerMaskController;
        private LeftStickyRaycastData l;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastData stickyRaycast;
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
            stickyRaycastController = GetComponent<StickyRaycastController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }
        
        private void InitializeData()
        {
            l = new LeftStickyRaycastData
            {
                LeftStickyRaycastOrigin = zero
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
            stickyRaycast = stickyRaycastController.Data;
            layerMask = layerMaskController.Data;
        }

        private void SetLeftStickyRaycastLength()
        {
            l.LeftStickyRaycastLength = OnSetStickyRaycastLength(raycast.BoundsWidth, physics.MaximumSlopeAngle,
                raycast.BoundsHeight, raycast.RayOffset);
        }

        private void SetLeftStickyRaycastLengthToStickyRaycastLength()
        {
            l.LeftStickyRaycastLength = stickyRaycast.StickyRaycastLength;
        }

        private void SetLeftStickyRaycastOriginX()
        {
            l.LeftStickyRaycastOriginX = raycast.BoundsBottomLeftCorner.x * 2 + physics.NewPosition.x;
        }

        private void SetLeftStickyRaycastOriginY()
        {
            l.LeftStickyRaycastOriginY = raycast.BoundsCenter.y;
        }

        private void SetLeftStickyRaycast()
        {
            l.LeftStickyRaycastHit = Raycast(l.LeftStickyRaycastOrigin, -physics.Transform.up,
                l.LeftStickyRaycastLength, layerMask.RaysBelowLayerMaskPlatforms, cyan,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public LeftStickyRaycastData Data => l;

        #region public methods

        public void OnSetLeftStickyRaycastLength()
        {
            SetLeftStickyRaycastLength();
        }

        public void OnSetLeftStickyRaycastLengthToStickyRaycastLength()
        {
            SetLeftStickyRaycastLengthToStickyRaycastLength();
        }

        public async UniTaskVoid OnSetLeftStickyRaycastOriginX()
        {
            SetLeftStickyRaycastOriginX();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetLeftStickyRaycastOriginY()
        {
            SetLeftStickyRaycastOriginY();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetLeftStickyRaycast()
        {
            SetLeftStickyRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
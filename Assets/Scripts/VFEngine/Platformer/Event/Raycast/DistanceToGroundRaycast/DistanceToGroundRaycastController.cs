using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static DebugExtensions;
    using static Color;
    using static UniTaskExtensions;
    using static Vector2;

    public class DistanceToGroundRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private StickyRaycastHitColliderController stickyRaycastHitColliderController;
        private LayerMaskController layerMaskController;
        private DistanceToGroundRaycastData d;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastHitColliderData stickyRaycastHitCollider;
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
            stickyRaycastHitColliderController = GetComponent<StickyRaycastHitColliderController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }
        
        private void InitializeData()
        {
            d = new DistanceToGroundRaycastData
            {
                DistanceToGroundRaycastOrigin = zero
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
            stickyRaycastHitCollider = stickyRaycastHitColliderController.Data;
            layerMask = layerMaskController.Data;
        }

        private void SetDistanceToGroundRaycastOrigin()
        {
            d.DistanceToGroundRaycastOrigin = new Vector2
            {
                x = stickyRaycastHitCollider.BelowSlopeAngle < 0
                    ? raycast.BoundsBottomLeftCorner.x
                    : raycast.BoundsBottomRightCorner.x,
                y = raycast.BoundsCenter.y
            };
        }

        private void SetDistanceToGroundRaycast()
        {
            /*d.DistanceToGroundRaycastHit = Raycast(d.DistanceToGroundRaycastOrigin, -physics.Transform.up,
                raycast.DistanceToGroundRayMaximumLength, layerMask.RaysBelowLayerMaskPlatforms, blue,
                raycast.DrawRaycastGizmosControl);*/
        }

        #endregion

        #endregion

        #region properties

        public DistanceToGroundRaycastData Data => d;

        #region public methods

        public void OnSetDistanceToGroundRaycastOrigin()
        {
            SetDistanceToGroundRaycastOrigin();
        }

        public async UniTaskVoid OnSetDistanceToGroundRaycast()
        {
            SetDistanceToGroundRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
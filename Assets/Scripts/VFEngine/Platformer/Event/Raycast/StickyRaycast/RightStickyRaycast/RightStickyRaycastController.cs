using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    using static StickyRaycast;
    using static DebugExtensions;
    using static Color;
    using static UniTaskExtensions;

    public class RightStickyRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private PhysicsController physicsController;
        private RaycastController raycastController;
        private StickyRaycastController stickyRaycastController;
        private LayerMaskController layerMaskController;
        private RightStickyRaycastData r;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastData stickyRaycast;
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
            r = new RightStickyRaycastData();
        }

        private void SetControllers()
        {
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            stickyRaycastController = character.GetComponentNoAllocation<StickyRaycastController>();
            physicsController = character.GetComponent<PhysicsController>();
            layerMaskController = character.GetComponent<LayerMaskController>();
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

        private void SetRightStickyRaycastLengthToStickyRaycastLength()
        {
            r.RightStickyRaycastLength = stickyRaycast.StickyRaycastLength;
        }

        private void SetRightStickyRaycastLength()
        {
            r.RightStickyRaycastLength = OnSetStickyRaycastLength(raycast.BoundsWidth, physics.MaximumSlopeAngle,
                raycast.BoundsHeight, raycast.RayOffset);
        }

        private void SetRightStickyRaycastOriginX()
        {
            r.RightStickyRaycastOriginX = raycast.BoundsBottomRightCorner.x * 2 + physics.NewPosition.x;
        }

        private void SetRightStickyRaycastOriginY()
        {
            r.RightStickyRaycastOriginY = raycast.BoundsCenter.y;
        }

        private void SetRightStickyRaycast()
        {
            r.RightStickyRaycastHit = Raycast(r.RightStickyRaycastOrigin, -physics.Transform.up,
                r.RightStickyRaycastLength, layerMask.RaysBelowLayerMaskPlatforms, cyan,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public RightStickyRaycastData Data => r;

        #region public methods

        public void OnSetRightStickyRaycastLengthToStickyRaycastLength()
        {
            SetRightStickyRaycastLengthToStickyRaycastLength();
        }

        public void OnSetRightStickyRaycastLength()
        {
            SetRightStickyRaycastLength();
        }

        public async UniTaskVoid OnSetRightStickyRaycastOriginY()
        {
            SetRightStickyRaycastOriginY();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetRightStickyRaycastOriginX()
        {
            SetRightStickyRaycastOriginX();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetRightStickyRaycast()
        {
            SetRightStickyRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
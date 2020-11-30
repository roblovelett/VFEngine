using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static StickyRaycast;
    using static DebugExtensions;
    using static Color;

    public class LeftStickyRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
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
            l = new LeftStickyRaycastData();
        }

        private void SetControllers()
        {
            physicsController = character.GetComponentNoAllocation<PhysicsController>();
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            stickyRaycastController = character.GetComponentNoAllocation<StickyRaycastController>();
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
            stickyRaycast = stickyRaycastController.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
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

        public void OnSetLeftStickyRaycastOriginX()
        {
            SetLeftStickyRaycastOriginX();
        }

        public void OnSetLeftStickyRaycastOriginY()
        {
            SetLeftStickyRaycastOriginY();
        }

        public void OnSetLeftStickyRaycast()
        {
            SetLeftStickyRaycast();
        }

        #endregion

        #endregion
    }
}
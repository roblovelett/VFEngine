using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static StickyRaycastModel;
    using static DebugExtensions;
    using static Color;
    using static UniTaskExtensions;

    [Serializable]
    public class LeftStickyRaycastModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private LayerMaskController layerMaskController;
        private LeftStickyRaycastData l;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastData stickyRaycast;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void InitializeData()
        {
            l = new LeftStickyRaycastData();
            if (!raycastController && character)
            {
                raycastController = character.GetComponent<RaycastController>();
            }
            else if (raycastController && !character)
            {
                character = raycastController.Character;
                raycastController = character.GetComponent<RaycastController>();
            }

            if (!physicsController) physicsController = character.GetComponent<PhysicsController>();
            if (!layerMaskController) layerMaskController = character.GetComponent<LayerMaskController>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            stickyRaycast = raycastController.StickyRaycastModel.Data;
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

        public void OnInitializeData()
        {
            InitializeData();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

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
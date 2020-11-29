using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static DebugExtensions;
    using static Color;
    using static UniTaskExtensions;

    [Serializable]
    public class DistanceToGroundRaycastModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private LayerMaskController layerMaskController;
        private DistanceToGroundRaycastData d;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastHitColliderData stickyRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void InitializeData()
        {
            d = new DistanceToGroundRaycastData();
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
            if (!raycastHitColliderController)
                raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
            if (!layerMaskController) layerMaskController = character.GetComponent<LayerMaskController>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            stickyRaycastHitCollider = raycastHitColliderController.StickyRaycastHitColliderModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
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
            d.DistanceToGroundRaycastHit = Raycast(d.DistanceToGroundRaycastOrigin, -physics.Transform.up,
                raycast.DistanceToGroundRayMaximumLength, layerMask.RaysBelowLayerMaskPlatforms, blue,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public DistanceToGroundRaycastData Data => d;

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

        public void OnSetDistanceToGroundRaycastOrigin()
        {
            SetDistanceToGroundRaycastOrigin();
        }

        public void OnSetDistanceToGroundRaycast()
        {
            SetDistanceToGroundRaycast();
        }

        #endregion

        #endregion
    }
}
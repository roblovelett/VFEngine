using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static DebugExtensions;
    using static Vector2;
    using static Color;
    using static UniTaskExtensions;

    [Serializable]
    public class SafetyBoxcastModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private BoxcastController boxcastController;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private LayerMaskController layerMaskController;
        private SafetyBoxcastData s;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastData stickyRaycast;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void InitializeData()
        {
            s = new SafetyBoxcastData();
            if (!boxcastController && character) boxcastController = character.GetComponent<BoxcastController>();
            else if (boxcastController && !character) character = boxcastController.Character;
            if (!physicsController) physicsController = character.GetComponent<PhysicsController>();
            if (!raycastController) raycastController = character.GetComponent<RaycastController>();
            if (!layerMaskController) layerMaskController = character.GetComponent<LayerMaskController>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            stickyRaycast = raycastController.StickyRaycastModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
        }

        private void SetSafetyBoxcastForImpassableAngle()
        {
            var transformUp = physics.Transform.up;
            s.SafetyBoxcastHit = Boxcast(raycast.BoundsCenter, raycast.Bounds, Angle(transformUp, up), -transformUp,
                stickyRaycast.StickyRaycastLength, layerMask.RaysBelowLayerMaskPlatforms, red,
                raycast.DrawRaycastGizmosControl);
        }

        private void SetSafetyBoxcast()
        {
            var transformUp = physics.Transform.up;
            s.SafetyBoxcastHit = Boxcast(raycast.BoundsCenter, raycast.Bounds, Angle(transformUp, up),
                physics.NewPosition.normalized, physics.NewPosition.magnitude, layerMask.PlatformMask, red,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public SafetyBoxcastData Data => s;

        #region public methods

        public void OnSetSafetyBoxcastForImpassableAngle()
        {
            SetSafetyBoxcastForImpassableAngle();
        }

        public void OnSetSafetyBoxcast()
        {
            SetSafetyBoxcast();
        }

        public void OnInitializeData()
        {
            InitializeData();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "RightRaycastModel", menuName = PlatformerRightRaycastModelPath, order = 0)]
    [InlineEditor]
    public class RightRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private RightRaycastData r;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private LayerMaskController layerMaskController;
        private PhysicsData physics;
        private RaycastData raycast;
        private RightRaycastHitColliderData rightRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!r) r = CreateInstance<RightRaycastData>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            rightRaycastHitCollider = raycastHitColliderController.RaycastHitColliderModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
        }

        private void SetRightRaycastFromBottomOrigin()
        {
            r.RightRaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(raycast.BoundsBottomRightCorner,
                raycast.BoundsBottomLeftCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void SetRightRaycastToTopOrigin()
        {
            r.RightRaycastToTopOrigin = OnSetRaycastToTopOrigin(raycast.BoundsTopLeftCorner,
                raycast.BoundsTopRightCorner, physics.Transform, raycast.ObstacleHeightTolerance);
        }

        private void InitializeRightRaycastLength()
        {
            r.RightRayLength = OnSetHorizontalRayLength(physics.Speed.x, raycast.BoundsWidth, raycast.RayOffset);
        }

        private void SetCurrentRightRaycastOrigin()
        {
            r.CurrentRightRaycastOrigin = OnSetCurrentRaycastOrigin(r.RightRaycastFromBottomOrigin,
                r.RightRaycastToTopOrigin, rightRaycastHitCollider.CurrentRightHitsStorageIndex,
                raycast.NumberOfHorizontalRaysPerSide);
        }

        private void SetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            r.CurrentRightRaycastHit = Raycast(r.CurrentRightRaycastOrigin, physics.Transform.right, r.RightRayLength,
                layerMask.PlatformMask, red, raycast.DrawRaycastGizmosControl);
        }

        private void SetCurrentRightRaycast()
        {
            r.CurrentRightRaycastHit = Raycast(r.CurrentRightRaycastOrigin, physics.Transform.right, r.RightRayLength,
                layerMask.PlatformMask & ~layerMask.OneWayPlatformMask & ~layerMask.MovingOneWayPlatformMask, red,
                raycast.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public RightRaycastData Data => r;

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetRightRaycastFromBottomOrigin()
        {
            SetRightRaycastFromBottomOrigin();
        }

        public void OnSetRightRaycastToTopOrigin()
        {
            SetRightRaycastToTopOrigin();
        }

        public void OnInitializeRightRaycastLength()
        {
            InitializeRightRaycastLength();
        }

        public void OnSetCurrentRightRaycastOrigin()
        {
            SetCurrentRightRaycastOrigin();
        }

        public void OnSetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentRightRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentRightRaycast()
        {
            SetCurrentRightRaycast();
        }

        #endregion

        #endregion
    }
}
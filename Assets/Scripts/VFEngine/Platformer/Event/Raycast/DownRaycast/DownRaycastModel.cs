using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static Mathf;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "DownRaycastModel", menuName = PlatformerDownRaycastModelPath, order = 0)]
    [InlineEditor]
    public class DownRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private DownRaycastData d;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private LayerMaskController layerMaskController;
        private PhysicsData physics;
        private RaycastData raycast;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            if (!d) d = CreateInstance<DownRaycastData>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            downRaycastHitCollider = raycastHitColliderController.DownRaycastHitColliderModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
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

        public void OnSetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentDownRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentDownRaycast()
        {
            SetCurrentDownRaycast();
        }

        public void OnInitializeDownRayLength()
        {
            InitializeDownRayLength();
        }

        public void OnDoubleDownRayLength()
        {
            DoubleDownRayLength();
        }

        public void OnSetDownRayLengthToVerticalNewPosition()
        {
            SetDownRayLengthToVerticalNewPosition();
        }

        public void OnSetDownRaycastFromLeft()
        {
            SetDownRaycastFromLeft();
        }

        public void OnSetDownRaycastToRight()
        {
            SetDownRaycastToRight();
        }

        public void OnSetCurrentDownRaycastOriginPoint()
        {
            SetCurrentDownRaycastOriginPoint();
        }

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    using static StickyRaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "RightStickyRaycastModel", menuName = PlatformerRightStickyRaycastModelPath, order = 0)]
    [InlineEditor]
    public class RightStickyRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private RightStickyRaycastData r;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private LayerMaskController layerMaskController;
        private PhysicsData physics;
        private RaycastData raycast;
        private StickyRaycastData stickyRaycast;
        private LayerMaskData layerMask;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!r) r = CreateInstance<RightStickyRaycastData>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            stickyRaycast = raycastController.StickyRaycastModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
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

        public void OnSetRightStickyRaycastLengthToStickyRaycastLength()
        {
            SetRightStickyRaycastLengthToStickyRaycastLength();
        }

        public void OnSetRightStickyRaycastLength()
        {
            SetRightStickyRaycastLength();
        }

        public void OnSetRightStickyRaycastOriginY()
        {
            SetRightStickyRaycastOriginY();
        }

        public void OnSetRightStickyRaycastOriginX()
        {
            SetRightStickyRaycastOriginX();
        }

        public void OnSetRightStickyRaycast()
        {
            SetRightStickyRaycast();
        }

        #endregion

        #endregion
    }
}
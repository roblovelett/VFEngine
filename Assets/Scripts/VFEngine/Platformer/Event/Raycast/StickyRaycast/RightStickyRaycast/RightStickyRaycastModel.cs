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

        [SerializeField] private GameObject character;
        private RightStickyRaycastData r;

        #endregion

        #region private methods

        private void InitializeData()
        {
            r = new RightStickyRaycastData {Character = character};
            r.RuntimeData = RightStickyRaycastRuntimeData.CreateInstance(r.RightStickyRaycastLength,
                r.RightStickyRaycastOrigin.y, r.RightStickyRaycastHit);
        }

        private void InitializeModel()
        {
            r.PhysicsRuntimeData = r.Character.GetComponentNoAllocation<PhysicsController>().PhysicsRuntimeData;
            r.RaycastRuntimeData = r.Character.GetComponentNoAllocation<RaycastController>().RaycastRuntimeData;
            r.StickyRaycastRuntimeData =
                r.Character.GetComponentNoAllocation<RaycastController>().StickyRaycastRuntimeData;
            r.LayerMaskRuntimeData = r.Character.GetComponentNoAllocation<LayerMaskController>().LayerMaskRuntimeData;
            r.Transform = r.PhysicsRuntimeData.Transform;
            r.MaximumSlopeAngle = r.PhysicsRuntimeData.MaximumSlopeAngle;
            r.NewPosition = r.PhysicsRuntimeData.NewPosition;
            r.DrawRaycastGizmosControl = r.RaycastRuntimeData.DrawRaycastGizmosControl;
            r.BoundsWidth = r.RaycastRuntimeData.BoundsWidth;
            r.BoundsHeight = r.RaycastRuntimeData.BoundsHeight;
            r.RayOffset = r.RaycastRuntimeData.RayOffset;
            r.BoundsBottomRightCorner = r.RaycastRuntimeData.BoundsBottomRightCorner;
            r.BoundsCenter = r.RaycastRuntimeData.BoundsCenter;
            r.StickyRaycastLength = r.StickyRaycastRuntimeData.StickyRaycastLength;
            r.RaysBelowLayerMaskPlatforms = r.LayerMaskRuntimeData.RaysBelowLayerMaskPlatforms;
        }

        private void SetRightStickyRaycastLengthToStickyRaycastLength()
        {
            r.RightStickyRaycastLength = r.StickyRaycastLength;
        }

        private void SetRightStickyRaycastLength()
        {
            r.RightStickyRaycastLength =
                OnSetStickyRaycastLength(r.BoundsWidth, r.MaximumSlopeAngle, r.BoundsHeight, r.RayOffset);
        }

        private void SetRightStickyRaycastOriginX()
        {
            r.RightStickyRaycastOriginX = r.BoundsBottomRightCorner.x * 2 + r.NewPosition.x;
        }

        private void SetRightStickyRaycastOriginY()
        {
            r.RightStickyRaycastOriginY = r.BoundsCenter.y;
        }

        private void SetRightStickyRaycast()
        {
            r.RightStickyRaycastHit = Raycast(r.RightStickyRaycastOrigin, -r.Transform.up, r.RightStickyRaycastLength,
                r.RaysBelowLayerMaskPlatforms, cyan, r.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public RightStickyRaycastRuntimeData RuntimeData => r.RuntimeData;

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
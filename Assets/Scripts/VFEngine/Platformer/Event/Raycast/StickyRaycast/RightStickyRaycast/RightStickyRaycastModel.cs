using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
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

        [LabelText("Right Sticky Raycast Data")] [SerializeField]
        private RightStickyRaycastData r = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            r.RuntimeData = r.Character.GetComponentNoAllocation<RaycastController>().RightStickyRaycastRuntimeData;
            r.RuntimeData.SetRightStickyRaycast(r.RightStickyRaycastLength, r.RightStickyRaycastOrigin.y,
                r.RightStickyRaycastHit);
        }

        private void InitializeModel()
        {
            r.PlatformerRuntimeData = r.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            r.RaycastRuntimeData = r.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            r.StickyRaycastRuntimeData =
                r.Character.GetComponentNoAllocation<RaycastController>().StickyRaycastRuntimeData;
            r.PhysicsRuntimeData = r.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            r.LayerMaskRuntimeData = r.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            r.Transform = r.PlatformerRuntimeData.platformer.Transform;
            r.DrawRaycastGizmosControl = r.RaycastRuntimeData.raycast.DrawRaycastGizmosControl;
            r.BoundsWidth = r.RaycastRuntimeData.raycast.BoundsWidth;
            r.BoundsHeight = r.RaycastRuntimeData.raycast.BoundsHeight;
            r.RayOffset = r.RaycastRuntimeData.raycast.RayOffset;
            r.BoundsBottomRightCorner = r.RaycastRuntimeData.raycast.BoundsBottomRightCorner;
            r.BoundsCenter = r.RaycastRuntimeData.raycast.BoundsCenter;
            r.StickyRaycastLength = r.StickyRaycastRuntimeData.stickyRaycast.StickyRaycastLength;
            r.MaximumSlopeAngle = r.PhysicsRuntimeData.MaximumSlopeAngle;
            r.NewPosition = r.PhysicsRuntimeData.NewPosition;
            r.RaysBelowLayerMaskPlatforms = r.LayerMaskRuntimeData.layerMask.RaysBelowLayerMaskPlatforms;
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

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
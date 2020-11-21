using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static StickyRaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "LeftStickyRaycastModel", menuName = PlatformerLeftStickyRaycastModelPath, order = 0)]
    [InlineEditor]
    public class LeftStickyRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Left Sticky Raycast Data")] [SerializeField]
        private LeftStickyRaycastData l = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            l.RuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().LeftStickyRaycastRuntimeData;
            l.RuntimeData.SetLeftStickyRaycast(l.LeftStickyRaycastLength, l.LeftStickyRaycastOrigin.y,
                l.LeftStickyRaycastHit);
        }

        private void InitializeModel()
        {
            l.PlatformerRuntimeData = l.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            l.RaycastRuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            l.StickyRaycastRuntimeData =
                l.Character.GetComponentNoAllocation<RaycastController>().StickyRaycastRuntimeData;
            l.PhysicsRuntimeData = l.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            l.LayerMaskRuntimeData = l.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            l.Transform = l.PlatformerRuntimeData.platformer.Transform;
            l.DrawRaycastGizmosControl = l.RaycastRuntimeData.raycast.DrawRaycastGizmosControl;
            l.BoundsWidth = l.RaycastRuntimeData.raycast.BoundsWidth;
            l.BoundsHeight = l.RaycastRuntimeData.raycast.BoundsHeight;
            l.RayOffset = l.RaycastRuntimeData.raycast.RayOffset;
            l.BoundsBottomLeftCorner = l.RaycastRuntimeData.raycast.BoundsBottomLeftCorner;
            l.BoundsCenter = l.RaycastRuntimeData.raycast.BoundsCenter;
            l.StickyRaycastLength = l.StickyRaycastRuntimeData.stickyRaycast.StickyRaycastLength;
            l.MaximumSlopeAngle = l.PhysicsRuntimeData.physics.MaximumSlopeAngle;
            l.NewPosition = l.PhysicsRuntimeData.physics.NewPosition;
            l.RaysBelowLayerMaskPlatforms = l.LayerMaskRuntimeData.layerMask.RaysBelowLayerMaskPlatforms;
        }

        private void SetLeftStickyRaycastLength()
        {
            l.LeftStickyRaycastLength =
                OnSetStickyRaycastLength(l.BoundsWidth, l.MaximumSlopeAngle, l.BoundsHeight, l.RayOffset);
        }

        private void SetLeftStickyRaycastLengthToStickyRaycastLength()
        {
            l.LeftStickyRaycastLength = l.StickyRaycastLength;
        }

        private void SetLeftStickyRaycastOriginX()
        {
            l.LeftStickyRaycastOriginX = l.BoundsBottomLeftCorner.x * 2 + l.NewPosition.x;
        }

        private void SetLeftStickyRaycastOriginY()
        {
            l.LeftStickyRaycastOriginY = l.BoundsCenter.y;
        }

        private void SetLeftStickyRaycast()
        {
            l.LeftStickyRaycastHit = Raycast(l.LeftStickyRaycastOrigin, -l.Transform.up, l.LeftStickyRaycastLength,
                l.RaysBelowLayerMaskPlatforms, cyan, l.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

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

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
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

        [SerializeField] private GameObject character;
        private LeftStickyRaycastData l = null;

        #endregion

        #region private methods

        private void InitializeData()
        {
            l = new LeftStickyRaycastData {Character = character};
            l.RuntimeData = LeftStickyRaycastRuntimeData.CreateInstance(l.LeftStickyRaycastLength, l.LeftStickyRaycastOrigin.y, l.LeftStickyRaycastHit);
        }

        private void InitializeModel()
        {
            l.RaycastRuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            l.StickyRaycastRuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().StickyRaycastRuntimeData;
            l.PhysicsRuntimeData = l.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            l.LayerMaskRuntimeData = l.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            l.Transform = l.PhysicsRuntimeData.Transform;
            l.MaximumSlopeAngle = l.PhysicsRuntimeData.MaximumSlopeAngle;
            l.NewPosition = l.PhysicsRuntimeData.NewPosition;
            l.DrawRaycastGizmosControl = l.RaycastRuntimeData.DrawRaycastGizmosControl;
            l.BoundsWidth = l.RaycastRuntimeData.BoundsWidth;
            l.BoundsHeight = l.RaycastRuntimeData.BoundsHeight;
            l.RayOffset = l.RaycastRuntimeData.RayOffset;
            l.BoundsBottomLeftCorner = l.RaycastRuntimeData.BoundsBottomLeftCorner;
            l.BoundsCenter = l.RaycastRuntimeData.BoundsCenter;
            l.StickyRaycastLength = l.StickyRaycastRuntimeData.StickyRaycastLength;
            l.RaysBelowLayerMaskPlatforms = l.LayerMaskRuntimeData.RaysBelowLayerMaskPlatforms;
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

        public LeftStickyRaycastRuntimeData RuntimeData => l.RuntimeData;

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
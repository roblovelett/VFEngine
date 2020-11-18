using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
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
            r.RuntimeData = r.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            r.RuntimeData.SetRightStickyRaycast(r.RightStickyRaycastLength, r.RightStickyRaycastOrigin.y,
                r.RightStickyRaycastHit);
        }

        private void InitializeModel()
        {
            r.Transform = r.RuntimeData.platformer.Transform;
            r.DrawRaycastGizmosControl = r.RuntimeData.raycast.DrawRaycastGizmosControl;
            r.StickyRaycastLength = r.RuntimeData.stickyRaycast.StickyRaycastLength;
            r.BoundsWidth = r.RuntimeData.raycast.BoundsWidth;
            r.MaximumSlopeAngle = r.RuntimeData.physics.MaximumSlopeAngle;
            r.BoundsHeight = r.RuntimeData.raycast.BoundsHeight;
            r.RayOffset = r.RuntimeData.raycast.RayOffset;
            r.BoundsBottomRightCorner = r.RuntimeData.raycast.BoundsBottomRightCorner;
            r.NewPosition = r.RuntimeData.physics.NewPosition;
            r.BoundsCenter = r.RuntimeData.raycast.BoundsCenter;
            r.RaysBelowLayerMaskPlatforms = r.RuntimeData.layerMask.RaysBelowLayerMaskPlatforms;
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

        #region public methods

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
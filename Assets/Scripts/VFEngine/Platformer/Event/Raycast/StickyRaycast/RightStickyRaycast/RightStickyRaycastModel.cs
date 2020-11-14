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
    using static RaycastModel;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "RightStickyRaycastModel", menuName = PlatformerRightStickyRaycastModelPath, order = 0)][InlineEditor]
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
        }

        private void InitializeData()
        {
            r.RightStickyRaycastOriginY = r.rightStickyRaycastOrigin.y;
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
            r.rightStickyRaycastOrigin.x = r.BoundsBottomRightCorner.x * 2 + r.NewPosition.x;
        }

        private void SetRightStickyRaycastOriginY()
        {
            r.rightStickyRaycastOrigin.y = r.BoundsCenter.y;
        }

        private void SetRightStickyRaycast()
        {
            var hit = Raycast(r.rightStickyRaycastOrigin, -r.Transform.up, r.RightStickyRaycastLength,
                r.RaysBelowLayerMaskPlatforms, cyan, r.DrawRaycastGizmosControl);
            r.RightStickyRaycast = OnSetRaycast(hit);
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
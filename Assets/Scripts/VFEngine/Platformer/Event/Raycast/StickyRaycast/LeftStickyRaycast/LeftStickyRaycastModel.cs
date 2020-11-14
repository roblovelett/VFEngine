using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static StickyRaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static RaycastModel;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "LeftStickyRaycastModel", menuName = PlatformerLeftStickyRaycastModelPath, order = 0)][InlineEditor]
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
        }

        private void InitializeData()
        {
            l.LeftStickyRaycastOriginY = l.leftStickyRaycastOrigin.y;
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
            l.leftStickyRaycastOrigin.x = l.BoundsBottomLeftCorner.x * 2 + l.NewPosition.x;
        }

        private void SetLeftStickyRaycastOriginY()
        {
            l.leftStickyRaycastOrigin.y = l.BoundsCenter.y;
        }

        private void SetLeftStickyRaycast()
        {
            var hit = Raycast(l.leftStickyRaycastOrigin, -l.Transform.up, l.LeftStickyRaycastLength,
                l.RaysBelowLayerMaskPlatforms, cyan, l.DrawRaycastGizmosControl);
            l.LeftStickyRaycast = OnSetRaycast(hit);
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
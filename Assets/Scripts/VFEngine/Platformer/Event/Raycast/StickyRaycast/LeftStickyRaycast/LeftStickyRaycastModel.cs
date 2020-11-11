using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static StickyRaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static RaycastModel;

    [CreateAssetMenu(fileName = "LeftStickyRaycastModel", menuName = PlatformerLeftStickyRaycastModelPath, order = 0)]
    public class LeftStickyRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Left Sticky Raycast Data")] [SerializeField]
        private LeftStickyRaycastData l;

        #endregion

        #region private methods

        private void SetLeftStickyRaycastLength()
        {
            l.leftStickyRaycastLength =
                OnSetStickyRaycastLength(l.BoundsWidth, l.MaximumSlopeAngle, l.BoundsHeight, l.RayOffset);
        }

        private void SetLeftStickyRaycastLengthToStickyRaycastLength()
        {
            l.leftStickyRaycastLength = l.StickyRaycastLength;
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
            var hit = Raycast(l.leftStickyRaycastOrigin, -l.Transform.up, l.leftStickyRaycastLength,
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

        #endregion

        #endregion
    }
}
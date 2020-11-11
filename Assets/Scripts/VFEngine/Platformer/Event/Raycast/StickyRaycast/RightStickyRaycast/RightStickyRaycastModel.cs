using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    using static StickyRaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static RaycastModel;

    [CreateAssetMenu(fileName = "RightStickyRaycastModel", menuName = PlatformerRightStickyRaycastModelPath, order = 0)]
    public class RightStickyRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Right Sticky Raycast Data")] [SerializeField]
        private RightStickyRaycastData r;

        #endregion

        #region private methods

        private void SetRightStickyRaycastLengthToStickyRaycastLength()
        {
            r.rightStickyRaycastLength = r.StickyRaycastLength;
        }

        private void SetRightStickyRaycastLength()
        {
            r.rightStickyRaycastLength =
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
            var hit = Raycast(r.rightStickyRaycastOrigin, -r.Transform.up, r.rightStickyRaycastLength,
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

        #endregion

        #endregion
    }
}
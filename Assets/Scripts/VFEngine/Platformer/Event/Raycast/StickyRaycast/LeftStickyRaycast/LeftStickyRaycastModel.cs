using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static StickyRaycastModel;
    using static DebugExtensions;
    using static Color;
    using static Vector3;

    [CreateAssetMenu(fileName = "LeftStickyRaycastModel",
        menuName = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Left Sticky Raycast/Left Sticky Raycast Model",
        order = 0)]
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
            l.LeftStickyRaycast = Raycast(l.leftStickyRaycastOrigin, -l.Transform.up, l.LeftStickyRaycastLength,
                l.RaysBelowLayerMaskPlatforms, cyan, l.DrawRaycastGizmosControl);
        }

        private void SetBelowSlopeAngleLeft()
        {
            l.BelowSlopeAngleLeft = Vector2.Angle(l.LeftStickyRaycast.normal, l.Transform.up);
        }

        private void SetCrossBelowSlopeAngleLeft()
        {
            l.CrossBelowSlopeAngleLeft = Cross(l.Transform.up, l.LeftStickyRaycast.normal);
        }

        private void SetBelowSlopeAngleLeftToNegative()
        {
            l.BelowSlopeAngleLeft = -l.BelowSlopeAngleLeft;
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

        public void OnSetBelowSlopeAngleLeft()
        {
            SetBelowSlopeAngleLeft();
        }

        public void OnSetCrossBelowSlopeAngleLeft()
        {
            SetCrossBelowSlopeAngleLeft();
        }

        public void OnSetBelowSlopeAngleLeftToNegative()
        {
            SetBelowSlopeAngleLeftToNegative();
        }

        #endregion

        #endregion
    }
}
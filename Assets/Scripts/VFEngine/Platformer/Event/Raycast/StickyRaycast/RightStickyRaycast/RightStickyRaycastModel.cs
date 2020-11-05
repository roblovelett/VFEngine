﻿using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    using static StickyRaycastModel;
    using static DebugExtensions;
    using static Color;
    using static Vector3;

    [CreateAssetMenu(fileName = "RightStickyRaycastModel",
        menuName = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Right Sticky Raycast/Right Sticky Raycast Model",
        order = 0)]
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
            r.RightStickyRaycast = Raycast(r.rightStickyRaycastOrigin, -r.Transform.up, r.RightStickyRaycastLength,
                r.RaysBelowLayerMaskPlatforms, cyan, r.DrawRaycastGizmosControl);
        }

        private void SetBelowSlopeAngleRight()
        {
            r.BelowSlopeAngleRight = Vector2.Angle(r.RightStickyRaycast.normal, r.Transform.up);
        }

        private void SetCrossBelowSlopeAngleRight()
        {
            r.CrossBelowSlopeAngleRight = Cross(r.Transform.up, r.RightStickyRaycast.normal);
        }

        private void SetBelowSlopeAngleRightToNegative()
        {
            r.BelowSlopeAngleRight = -r.BelowSlopeAngleRight;
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

        public void OnSetBelowSlopeAngleRight()
        {
            SetBelowSlopeAngleRight();
        }

        public void OnSetCrossBelowSlopeAngleRight()
        {
            SetCrossBelowSlopeAngleRight();
        }

        public void OnSetBelowSlopeAngleRightToNegative()
        {
            SetBelowSlopeAngleRightToNegative();
        }

        #endregion

        #endregion
    }
}
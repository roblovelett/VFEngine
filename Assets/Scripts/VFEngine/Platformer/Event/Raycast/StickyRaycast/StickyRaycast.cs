using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static Mathf;

    public static class StickyRaycast
    {
        #region fields

        #region private methods

        private static float SetStickyRaycastLength(float boundsWidth, float slopeAngle, float boundsHeight,
            float offset)
        {
            return boundsWidth * Abs(Tan(slopeAngle)) * 2 + boundsHeight / 2 * offset;
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public static float OnSetStickyRaycastLength(float boundsWidth, float slopeAngle, float boundsHeight,
            float offset)
        {
            return SetStickyRaycastLength(boundsWidth, slopeAngle, boundsHeight, offset);
        }

        #endregion

        #endregion
    }
}
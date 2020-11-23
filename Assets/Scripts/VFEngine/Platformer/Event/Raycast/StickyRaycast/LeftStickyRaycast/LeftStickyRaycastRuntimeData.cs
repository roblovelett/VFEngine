using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    public class LeftStickyRaycastRuntimeData
    {
        #region properties

        public float LeftStickyRaycastLength { get; private set; }
        public float LeftStickyRaycastOriginY { get; private set; }
        public RaycastHit2D LeftStickyRaycastHit { get; private set; }

        #region public methods

        public static LeftStickyRaycastRuntimeData CreateInstance(float leftStickyRaycastLength,
            float leftStickyRaycastOriginY, RaycastHit2D leftStickyRaycastHit)
        {
            return new LeftStickyRaycastRuntimeData
            {
                LeftStickyRaycastLength = leftStickyRaycastLength,
                LeftStickyRaycastOriginY = leftStickyRaycastOriginY,
                LeftStickyRaycastHit = leftStickyRaycastHit
            };
        }

        #endregion

        #endregion
    }
}
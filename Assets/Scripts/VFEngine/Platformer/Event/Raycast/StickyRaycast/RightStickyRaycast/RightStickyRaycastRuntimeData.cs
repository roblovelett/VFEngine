using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    public class RightStickyRaycastRuntimeData
    {
        #region properties

        public float RightStickyRaycastLength { get; private set; }
        public float RightStickyRaycastOriginY { get; private set; }
        public RaycastHit2D RightStickyRaycastHit { get; private set; }

        #region public methods

        public static RightStickyRaycastRuntimeData CreateInstance(float rightStickyRaycastLength,
            float rightStickyRaycastOriginY, RaycastHit2D rightStickyRaycastHit)
        {
            return new RightStickyRaycastRuntimeData
            {
                RightStickyRaycastLength = rightStickyRaycastLength,
                RightStickyRaycastOriginY = rightStickyRaycastOriginY,
                RightStickyRaycastHit = rightStickyRaycastHit
            };
        }

        #endregion

        #endregion
    }
}
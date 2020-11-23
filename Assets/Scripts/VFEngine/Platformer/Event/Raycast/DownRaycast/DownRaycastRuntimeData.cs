using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    public class DownRaycastRuntimeData
    {
        #region properties

        public float DownRayLength { get; private set; }
        public Vector2 CurrentDownRaycastOrigin { get; private set; }
        public Vector2 DownRaycastFromLeft { get; private set; }
        public Vector2 DownRaycastToRight { get; private set; }
        public RaycastHit2D CurrentDownRaycastHit { get; private set; }

        #region public methods

        public static DownRaycastRuntimeData CreateInstance(float downRayLength, Vector2 currentDownRaycastOrigin,
            Vector2 downRaycastFromLeft, Vector2 downRaycastToRight, RaycastHit2D currentDownRaycastHit)
        {
            return new DownRaycastRuntimeData
            {
                DownRayLength = downRayLength,
                CurrentDownRaycastOrigin = currentDownRaycastOrigin,
                DownRaycastFromLeft = downRaycastFromLeft,
                DownRaycastToRight = downRaycastToRight,
                CurrentDownRaycastHit = currentDownRaycastHit
            };
        }

        #endregion

        #endregion
    }
}
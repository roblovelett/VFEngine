using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    public class RightRaycastRuntimeData
    {
        #region properties

        public float RightRayLength { get; private set; }
        public Vector2 RightRaycastFromBottomOrigin { get; private set; }
        public Vector2 RightRaycastToTopOrigin { get; private set; }
        public RaycastHit2D CurrentRightRaycastHit { get; private set; }

        #region public methods

        public static RightRaycastRuntimeData CreateInstance(float rightRayLength, Vector2 rightRaycastFromBottomOrigin,
            Vector2 rightRaycastToTopOrigin, RaycastHit2D currentRightRaycastHit)
        {
            return new RightRaycastRuntimeData
            {
                RightRayLength = rightRayLength,
                RightRaycastFromBottomOrigin = rightRaycastFromBottomOrigin,
                RightRaycastToTopOrigin = rightRaycastToTopOrigin,
                CurrentRightRaycastHit = currentRightRaycastHit
            };
        }

        #endregion

        #endregion
    }
}
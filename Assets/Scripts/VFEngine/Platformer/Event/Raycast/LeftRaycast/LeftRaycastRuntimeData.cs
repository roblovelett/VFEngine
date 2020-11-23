using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    public class LeftRaycastRuntimeData
    {
        #region properties

        public float LeftRayLength { get; private set; }
        public Vector2 LeftRaycastFromBottomOrigin { get; private set; }
        public Vector2 LeftRaycastToTopOrigin { get; private set; }
        public RaycastHit2D CurrentLeftRaycastHit { get; private set; }

        #region public methods

        public static LeftRaycastRuntimeData CreateInstance(float leftRayLength, Vector2 leftRaycastFromBottomOrigin,
            Vector2 leftRaycastToTopOrigin, RaycastHit2D currentLeftRaycastHit)
        {
            return new LeftRaycastRuntimeData
            {
                LeftRayLength = leftRayLength,
                LeftRaycastFromBottomOrigin = leftRaycastFromBottomOrigin,
                LeftRaycastToTopOrigin = leftRaycastToTopOrigin,
                CurrentLeftRaycastHit = currentLeftRaycastHit
            };
        }

        #endregion

        #endregion
    }
}
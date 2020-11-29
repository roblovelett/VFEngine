using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    public class RightRaycastData
    {
        #region properties

        public float RightRayLength { get; set; }
        public Vector2 RightRaycastFromBottomOrigin { get; set; }
        public Vector2 RightRaycastToTopOrigin { get; set; }
        public RaycastHit2D CurrentRightRaycastHit { get; set; }
        public Vector2 CurrentRightRaycastOrigin { get; set; }

        #endregion
    }
}
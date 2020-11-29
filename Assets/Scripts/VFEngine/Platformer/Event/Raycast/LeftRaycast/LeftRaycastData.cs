using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    public class LeftRaycastData
    {
        #region properties

        public float LeftRayLength { get; set; }
        public Vector2 LeftRaycastFromBottomOrigin { get; set; }
        public Vector2 LeftRaycastToTopOrigin { get; set; }
        public RaycastHit2D CurrentLeftRaycastHit { get; set; }
        public Vector2 CurrentLeftRaycastOrigin { get; set; }

        #endregion
    }
}
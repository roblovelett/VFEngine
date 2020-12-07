using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    public class RightRaycastData
    {
        #region properties

        public float RayLength { get; set; }
        public Vector2 CurrentRaycastOrigin { get; set; }
        public Vector2 RaycastFromBottomOrigin { get; set; }
        public Vector2 RaycastToTopOrigin { get; set; }
        public RaycastHit2D CurrentRaycastHit { get; set; }

        #endregion
    }
}
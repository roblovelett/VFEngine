using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    public class DownRaycastData
    {
        #region properties

        public float DownRayLength { get; set; }
        public Vector2 CurrentDownRaycastOrigin { get; set; }
        public Vector2 DownRaycastFromLeft { get; set; }
        public Vector2 DownRaycastToRight { get; set; }
        public RaycastHit2D CurrentDownRaycastHit { get; set; }

        #endregion
    }
}
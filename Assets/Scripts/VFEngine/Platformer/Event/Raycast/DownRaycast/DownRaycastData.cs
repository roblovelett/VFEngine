using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    public class DownRaycastData
    {
        #region properties

        public float RayLength { get; set; }
        public Vector2 CurrentRaycastOrigin { get; set; }
        public Vector2 RaycastFromLeftOrigin{ get; set; }
        public Vector2 RaycastToRightOrigin { get; set; }
        public RaycastHit2D CurrentRaycast { get; set; }

        #endregion
    }
}
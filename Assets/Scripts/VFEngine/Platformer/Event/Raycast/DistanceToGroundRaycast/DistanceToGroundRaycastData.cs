using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    public class DistanceToGroundRaycastData
    {
        #region properties

        public RaycastHit2D DistanceToGroundRaycastHit { get; set; }
        public Vector2 DistanceToGroundRaycastOrigin { get; set; } = Vector2.zero;

        #endregion
    }
}
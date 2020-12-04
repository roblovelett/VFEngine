using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    public class DistanceToGroundRaycastData
    {
        #region properties

        public Vector2 DistanceToGroundRaycastOrigin { get; set; }
        public RaycastHit2D DistanceToGroundRaycastHit { get; set; }

        #endregion
    }
}
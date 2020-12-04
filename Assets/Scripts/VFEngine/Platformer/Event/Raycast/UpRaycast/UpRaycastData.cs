using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    public class UpRaycastData
    {
        #region properties

        public float UpRayLength { get; set; }
        public float UpRaycastSmallestDistance { get; set; }
        public Vector2 CurrentUpRaycastOrigin { get; set; }
        public Vector2 UpRaycastStart { get; set; }
        public Vector2 UpRaycastEnd { get; set; }
        public RaycastHit2D CurrentUpRaycastHit { get; set; }

        #endregion
    }
}
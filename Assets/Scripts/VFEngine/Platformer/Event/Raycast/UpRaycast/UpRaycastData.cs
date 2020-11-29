using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    public class UpRaycastData
    {
        #region properties

        public float UpRayLength { get; set; }
        public Vector2 UpRaycastStart { get; set; } = Vector2.zero;
        public Vector2 UpRaycastEnd { get; set; } = Vector2.zero;
        public float UpRaycastSmallestDistance { get; set; }
        public Vector2 CurrentUpRaycastOrigin { get; set; }
        public RaycastHit2D CurrentUpRaycastHit { get; set; }

        #endregion
    }
}
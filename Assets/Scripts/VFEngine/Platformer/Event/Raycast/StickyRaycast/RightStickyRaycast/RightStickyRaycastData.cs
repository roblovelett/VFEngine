using UnityEngine;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    public class RightStickyRaycastData
    {
        #region properties

        public RaycastHit2D RightStickyRaycastHit { get; set; }
        public float RightStickyRaycastLength { get; set; }
        public Vector2 RightStickyRaycastOrigin { get; } = Vector2.zero;

        public float RightStickyRaycastOriginX
        {
            set => value = RightStickyRaycastOrigin.x;
        }

        public float RightStickyRaycastOriginY
        {
            set => value = RightStickyRaycastOrigin.y;
        }

        #endregion
    }
}
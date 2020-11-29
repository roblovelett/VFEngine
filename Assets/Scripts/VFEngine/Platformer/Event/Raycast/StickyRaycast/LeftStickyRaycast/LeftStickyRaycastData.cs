using UnityEngine;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    public class LeftStickyRaycastData
    {
        #region properties

        public RaycastHit2D LeftStickyRaycastHit { get; set; }
        public float LeftStickyRaycastLength { get; set; }
        public Vector2 LeftStickyRaycastOrigin { get; } = Vector2.zero;

        public float LeftStickyRaycastOriginX
        {
            set => value = LeftStickyRaycastOrigin.x;
        }

        public float LeftStickyRaycastOriginY
        {
            set => value = LeftStickyRaycastOrigin.y;
        }

        #endregion
    }
}
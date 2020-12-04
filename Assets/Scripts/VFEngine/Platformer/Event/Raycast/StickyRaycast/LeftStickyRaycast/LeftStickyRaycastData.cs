using UnityEngine;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    public class LeftStickyRaycastData
    {
        #region properties

        public float LeftStickyRaycastLength { get; set; }
        public Vector2 LeftStickyRaycastOrigin { get; set; }
        public RaycastHit2D LeftStickyRaycastHit { get; set; }

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
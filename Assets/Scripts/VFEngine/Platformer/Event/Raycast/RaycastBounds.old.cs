using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public struct RaycastBounds
    {
        #region properties

        public Bounds Bounds { get; private set; }
        public Vector2 TopLeft { get; private set; }
        public Vector2 TopRight { get; private set; }
        public Vector2 BottomLeft { get; private set; }
        public Vector2 BottomRight { get; private set; }
        public Vector2 Size { get; private set; }

        #region public methods

        #region constructor

        public RaycastBounds(Collider2D collider, float skinWidth) : this()
        {
            Set(collider, skinWidth);
        }

        #endregion

        public void Set(Collider2D collider, float skinWidth)
        {
            Bounds = collider.bounds;
            Bounds.Expand(skinWidth * -2);
            Size = Bounds.size;
            BottomLeft = new Vector2(Bounds.min.x, Bounds.min.y);
            BottomRight = new Vector2(Bounds.max.x, Bounds.min.y);
            TopLeft = new Vector2(Bounds.min.x, Bounds.max.y);
            TopRight = new Vector2(Bounds.max.x, Bounds.max.y);
        }

        #endregion

        #endregion
    }
}
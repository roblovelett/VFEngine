using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public class RaycastBounds
    {
        #region fields

        #region private methods

        #endregion

        #endregion

        #region properties

        public Vector2 TopLeft { get; private set; }
        public Vector2 TopRight { get; private set; }
        public Vector2 BottomLeft { get; private set; }
        public Vector2 BottomRight { get; private set; }
        public Vector2 Size { get; private set; }
        public Bounds Bounds { get; private set; }

        #region public methods

        public void Initialize(Collider2D boxCollider, float skinWidth)
        {
            SetBounds(boxCollider, skinWidth);
        }

        public void SetBounds(Collider2D boxCollider, float skinWidth)
        {
            Bounds = boxCollider.bounds;
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
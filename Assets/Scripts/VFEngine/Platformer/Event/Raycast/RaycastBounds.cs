using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Event.Raycast
{
    public struct RaycastBounds
    {
        #region fields

        #region private methods

        private void Initialize(Collider2D collider, float skinWidth)
        {
            SetBounds(collider, skinWidth);
        }

        private void SetBounds(Collider2D collider, float skinWidth)
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

        #region properties

        public Bounds Bounds { get; private set; }
        public Vector2 TopLeft { get; private set; }
        public Vector2 TopRight { get; private set; }
        public Vector2 BottomLeft { get; private set; }
        public Vector2 BottomRight { get; private set; }
        public Vector2 Size { get; private set; }

        #region public methods

        #region constructors

        public RaycastBounds(Collider2D collider, float skinWidth) : this()
        {
            Initialize(collider, skinWidth);
        }

        #endregion

        #endregion

        #endregion
    }
}
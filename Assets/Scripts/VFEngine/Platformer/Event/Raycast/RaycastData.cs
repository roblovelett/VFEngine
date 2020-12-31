using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Event.Raycast
{
    using static Vector2;
    using static Mathf;

    public struct RaycastData
    {
        #region fields

        private RaycastCollision _collision;

        #region private methods

        #region initialization

        private void InitializeDependencies(RaycastSettings settings, Collider2D collider)
        {
            InitializeSettings(settings);
            InitializeCollider(collider);
        }

        private void InitializeSettings(RaycastSettings settings)
        {
            DisplayWarnings = settings.displayWarnings;
            DrawGizmos = settings.drawGizmos;
            TotalHorizontalRays = settings.totalHorizontalRays;
            TotalVerticalRays = settings.totalVerticalRays;
            Spacing = settings.spacing;
            SkinWidth = settings.skinWidth;
        }

        private void InitializeCollider(Collider2D collider)
        {
            Collider = collider;
        }

        private void InitializeInternal()
        {
            Length = 0;
            HorizontalSpacing = 0;
            VerticalSpacing = 0;
            Origin = zero;
            Hit = new RaycastHit2D();
            Bounds = new RaycastBounds(Collider, SkinWidth);
            SetCount();
            SetSpacing();
            Collision = new RaycastCollision();
            Collision.Initialize();
        }

        #endregion

        private void SetCount()
        {
            HorizontalRays = (int) Round(Bounds.Size.y / Spacing);
            VerticalRays = (int) Round(Bounds.Size.x / Spacing);
        }

        private void SetSpacing()
        {
            HorizontalSpacing = Bounds.Size.y / (HorizontalRays - 1);
            VerticalSpacing = Bounds.Size.x / (VerticalRays - 1);
        }

        #endregion

        #endregion

        #region properties

        #region dependencies

        public bool DisplayWarnings { get; private set; }
        public bool DrawGizmos { get; private set; }
        public int TotalHorizontalRays { get; private set; }
        public int TotalVerticalRays { get; private set; }
        public float Spacing { get; private set; }
        public float SkinWidth { get; private set; }
        public Collider2D Collider { get; set; }

        #endregion

        public int HorizontalRays { get; set; }
        public int VerticalRays { get; set; }
        public float Length { get; private set; }
        public float HorizontalSpacing { get; set; }
        public float VerticalSpacing { get; set; }
        public Vector2 Origin { get; set; }
        public RaycastHit2D Hit { get; set; }
        public RaycastBounds Bounds { get; private set; }
        public RaycastCollision Collision { get; private set; }

        #region public methods

        #region constructors

        public RaycastData(RaycastSettings settings, Collider2D collider) : this()
        {
            InitializeDependencies(settings, collider);
            InitializeInternal();
        }

        #endregion

        public void ResetCollision()
        {
            Collision.Reset();
        }

        public void SetBounds()
        {
            Bounds.Set(Collider, SkinWidth);
        }

        public void SetOrigin(Vector2 origin)
        {
            Origin = origin;
        }

        public void SetHit(RaycastHit2D hit)
        {
            Hit = hit;
        }

        public void SetLength(float length)
        {
            Length = length;
        }
        
        #endregion

        #endregion
    }
}
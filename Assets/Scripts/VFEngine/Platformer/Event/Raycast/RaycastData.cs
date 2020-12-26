using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Event.Raycast
{
    using static Mathf;

    public struct RaycastData
    {
        #region fields

        #region private methods

        private void Initialize()
        {
            InitializeBounds();
            InitializeInternal();
        }

        private void InitializeDependencies(RaycastSettings settings, Collider2D collider)
        {
            InitializeSettings(settings);
            InitializeCollider(collider);
        }

        private void InitializeDependencies(RaycastSettings settings)
        {
            InitializeSettings(settings);
            InitializeCollider();
        }

        private void InitializeDependencies(Collider2D collider)
        {
            InitializeSettings();
            InitializeCollider(collider);
        }

        private void InitializeSettings(RaycastSettings settings)
        {
            DisplayWarnings = settings.displayWarnings;
            DrawGizmos = settings.drawGizmos;
            HorizontalRaysAmount = settings.horizontalRaysAmount;
            VerticalRaysAmount = settings.verticalRaysAmount;
            Spacing = settings.spacing;
            SkinWidth = settings.skinWidth;
        }

        private void InitializeSettings()
        {
            DisplayWarnings = false;
            DrawGizmos = false;
            HorizontalRaysAmount = 0;
            VerticalRaysAmount = 0;
            Spacing = 0;
            SkinWidth = 0;
        }

        private void InitializeCollider(Collider2D collider)
        {
            Collider = collider;
        }

        private void InitializeCollider()
        {
            Collider = new Collider2D();
        }

        private void InitializeBounds()
        {
            Bounds = new RaycastBounds(Collider, SkinWidth);
        }

        private void InitializeInternal()
        {
            Collision = new RaycastCollision();
            Collision.OnInitialize();
            Index = 0;
            SetCount();
            SetSpacing();
        }

        private void SetCount()
        {
            HorizontalCount = (int) Round(Bounds.Size.y / Spacing);
            VerticalCount = (int) Round(Bounds.Size.x / Spacing);
        }

        private void SetSpacing()
        {
            HorizontalSpacing = Bounds.Size.y / (HorizontalCount - 1);
            VerticalSpacing = Bounds.Size.x / (VerticalCount - 1);
        }

        #endregion

        #endregion

        #region properties

        #region dependencies

        public bool DisplayWarnings { get; private set; }
        public bool DrawGizmos { get; private set; }
        public int HorizontalRaysAmount { get; private set; }
        public int VerticalRaysAmount { get; private set; }
        public float Spacing { get; private set; }
        public float SkinWidth { get; private set; }
        public Collider2D Collider { get; set; }

        #endregion

        public RaycastBounds Bounds { get; set; }
        public RaycastCollision Collision { get; set; }
        public int Index { get; set; }
        public int HorizontalCount { get; set; }
        public int VerticalCount { get; set; }
        public float HorizontalSpacing { get; set; }
        public float VerticalSpacing { get; set; }

        #region public methods

        #region constructors

        public RaycastData(RaycastSettings settings, Collider2D collider) : this()
        {
            InitializeDependencies(settings, collider);
            Initialize();
        }

        public RaycastData(RaycastSettings settings) : this()
        {
            InitializeDependencies(settings);
            Initialize();
        }

        public RaycastData(Collider2D collider) : this()
        {
            InitializeDependencies(collider);
            Initialize();
        }

        #endregion

        #endregion

        #endregion
    }
}
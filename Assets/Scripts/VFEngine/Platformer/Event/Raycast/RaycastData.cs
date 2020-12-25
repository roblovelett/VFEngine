using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    using static Mathf;

    public struct RaycastData
    {
        #region fields

        #region private methods

        private void InitializeDependencies(RaycastSettings settings)
        {
            DisplayWarnings = settings.displayWarnings;
            DrawGizmos = settings.drawGizmos;
            HorizontalRaysAmount = settings.horizontalRaysAmount;
            VerticalRaysAmount = settings.verticalRaysAmount;
            Spacing = settings.spacing;
            SkinWidth = settings.skinWidth;
        }

        private void InitializeDependencies()
        {
            DisplayWarnings = false;
            DrawGizmos = false;
            HorizontalRaysAmount = 0;
            VerticalRaysAmount = 0;
            Spacing = 0;
            SkinWidth = 0;
        }

        private void Initialize(Collider2D collider, float skinWidth)
        {
            Collider = collider;
            InitializeBounds(collider, skinWidth);
            InitializeInternal();
        }

        private void Initialize(float skinWidth)
        {
            Collider = new Collider2D();
            InitializeBounds(Collider, skinWidth);
            InitializeInternal();
        }

        private void InitializeBounds(Collider2D collider, float skinWidth)
        {
            Bounds = new RaycastBounds(collider, skinWidth);
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
            HorizontalCount = SetCount(Bounds.Size.y, Spacing);
            VerticalCount = SetCount(Bounds.Size.x, Spacing);
        }

        private static int SetCount(float size, float spacing)
        {
            return (int) Round(size / spacing);
        }

        private void SetSpacing()
        {
            HorizontalSpacing = SetSpacing(Bounds.Size.y, HorizontalCount);
            VerticalSpacing = SetSpacing(Bounds.Size.x, VerticalCount);
        }

        private static float SetSpacing(float size, int count)
        {
            return size / (count - 1);
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

        #endregion

        public Collider2D Collider { get; set; }
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
            if (settings) InitializeDependencies(settings);
            else InitializeDependencies();
            if (collider) Initialize(collider, SkinWidth);
            else Initialize(SkinWidth);
        }

        public RaycastData(RaycastSettings settings) : this()
        {
            if (settings) InitializeDependencies(settings);
            else InitializeDependencies();
            Initialize(SkinWidth);
        }

        public RaycastData(Collider2D collider) : this()
        {
            InitializeDependencies();
            if (collider) Initialize(collider, SkinWidth);
            else Initialize(SkinWidth);
        }

        #endregion

        #endregion

        #endregion
    }
}
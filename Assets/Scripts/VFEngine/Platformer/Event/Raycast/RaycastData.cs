using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    using static Mathf;

    public struct RaycastData
    {
        #region fields

        #region private methods

        private void ApplySettings(RaycastSettings settings)
        {
            DrawGizmos = settings.drawGizmos;
            DisplayWarnings = settings.displayWarnings;
            HorizontalRaysAmount = settings.horizontalRaysAmount;
            VerticalRaysAmount = settings.verticalRaysAmount;
            Spacing = settings.spacing;
            SkinWidth = settings.skinWidth;
        }

        private void SetCount()
        {
            HorizontalCount = SetCount(Bounds.Size.y, Spacing);
            VerticalCount = SetCount(Bounds.Size.x, Spacing);
        }

        private void SetSpacing()
        {
            HorizontalSpacing = SetSpacing(Bounds.Size.y, HorizontalCount);
            VerticalSpacing = SetSpacing(Bounds.Size.x, VerticalCount);
        }

        private static int SetCount(float size, float spacing)
        {
            return (int) Round(size / spacing);
        }

        private static float SetSpacing(float size, int count)
        {
            return size / (count - 1);
        }

        #endregion

        #endregion

        #region properties

        #region dependencies

        public bool DrawGizmos { get; private set; }
        public bool DisplayWarnings { get; private set; }
        public int HorizontalRaysAmount { get; private set; }
        public int VerticalRaysAmount { get; private set; }
        public float Spacing { get; private set; }
        public float SkinWidth { get; private set; }

        #endregion

        public Collider2D Collider { get; set; }
        public RaycastBounds Bounds { get; set; }
        public int HorizontalCount { get; set; }
        public int VerticalCount { get; set; }
        public int RightIndex { get; set; }
        public int DownIndex { get; set; }
        public int LeftIndex { get; set; }
        public float HorizontalSpacing { get; set; }
        public float VerticalSpacing { get; set; }

        #region public methods

        public void InitializeData()
        {
            Bounds = new RaycastBounds();
        }

        public void Initialize(RaycastSettings settings, Collider2D collider)
        {
            ApplySettings(settings);
            Collider = collider;
            Bounds.Initialize(Collider, SkinWidth);
            SetCount();
            SetSpacing();
        }

        public void SetRayOrigins()
        {
            Bounds.SetBounds(Collider, SkinWidth);
        }

        public void InitializeDownIndex()
        {
            if (DownIndex == 0) return;
            DownIndex = 0;
        }

        public void AddToDownIndex()
        {
            DownIndex++;
        }

        public void InitializeRightIndex()
        {
            if (RightIndex == 0) return;
            RightIndex = 0;
        }

        public void AddToRightIndex()
        {
            RightIndex++;
        }

        public void InitializeLeftIndex()
        {
            if (LeftIndex == 0) return;
            LeftIndex = 0;
        }

        #endregion

        #endregion
    }
}
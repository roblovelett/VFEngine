using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    using static Mathf;
    using static RaycastDirection;

    public class RaycastData
    {
        #region properties

        #region dependencies

        public bool DrawRaycastGizmosControl { get; private set; }
        public bool DisplayWarningsControl { get; private set; }
        public int NumberOfHorizontalRays { get; private set; }
        public int NumberOfVerticalRays { get; private set; }
        public float RaySpacing { get; private set; }
        public float SkinWidth { get; private set; }

        #endregion

        public Collider2D BoxCollider { get; set; }
        public RaycastBounds Bounds { get; set; }
        public RaycastDirection Direction { get; set; }
        public int HorizontalRayCount { get; set; }
        public int VerticalRayCount { get; set; }
        public float HorizontalRaySpacing { get; set; }
        public float VerticalRaySpacing { get; set; }

        #region public methods

        public void ApplySettings(RaycastSettings settings)
        {
            DrawRaycastGizmosControl = settings.drawRaycastGizmosControl;
            DisplayWarningsControl = settings.displayWarningsControl;
            NumberOfHorizontalRays = settings.numberOfHorizontalRays;
            NumberOfVerticalRays = settings.numberOfVerticalRays;
            RaySpacing = settings.raySpacing;
            SkinWidth = settings.skinWidth;
        }

        public void Initialize(Collider2D boxCollider)
        {
            BoxCollider = boxCollider;
            Direction = None;
            Bounds = new RaycastBounds();
            Bounds.Initialize(BoxCollider, SkinWidth);
            SetRayCount();
            SetRaySpacing();
        }

        public void CalculateSpacing()
        {
            Bounds.SetBounds(BoxCollider, SkinWidth);
            SetRayCount();
            SetRaySpacing();
        }

        public void SetRayOrigins()
        {
            Bounds.SetBounds(BoxCollider, SkinWidth);
        }

        private void SetRayCount()
        {
            HorizontalRayCount = SetCount(Bounds.Size.y, RaySpacing);
            VerticalRayCount = SetCount(Bounds.Size.x, RaySpacing);
        }

        private void SetRaySpacing()
        {
            HorizontalRaySpacing = SetSpacing(Bounds.Size.y, HorizontalRayCount);
            VerticalRaySpacing = SetSpacing(Bounds.Size.x, VerticalRayCount);
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
    }
}
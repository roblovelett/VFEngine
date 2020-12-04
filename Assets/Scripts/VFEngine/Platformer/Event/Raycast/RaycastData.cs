using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public class RaycastData
    {
        #region properties

        #region dependencies

        public bool DisplayWarningsControl { get; private set; }
        public bool DrawRaycastGizmosControl { get; private set; }
        public bool CastRaysOnBothSides { get; private set; }
        public int NumberOfHorizontalRays { get; private set; }
        public int NumberOfVerticalRays { get; private set; }
        public float RayOffset { get; private set; }
        public float DistanceToGroundRayMaximumLength { get; private set; }

        #endregion
        
        public float BoundsWidth { get; set; }
        public float BoundsHeight { get; set; }
        public Vector2 OriginalColliderSize { get; set; }
        public Vector2 OriginalColliderOffset { get; set; }
        public Vector2 Bounds { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsBottomCenterPosition { get; set; }
        public Vector2 BoundsTopLeftCorner { get; set; }
        public Vector2 BoundsTopRightCorner { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public int NumberOfVerticalRaysPerSide => NumberOfVerticalRays / 2;
        public float ObstacleHeightTolerance => RayOffset;

        #region public methods

        public void ApplySettings(RaycastSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            DrawRaycastGizmosControl = settings.drawRaycastGizmosControl;
            CastRaysOnBothSides = settings.castRaysOnBothSides;
            NumberOfHorizontalRays = settings.numberOfHorizontalRays;
            NumberOfVerticalRays = settings.numberOfVerticalRays;
            RayOffset = settings.rayOffset;
            DistanceToGroundRayMaximumLength = settings.distanceToGroundRayMaximumLength;
        }

        #endregion

        #endregion
    }
}
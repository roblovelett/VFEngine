using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public class RaycastRuntimeData
    {
        #region properties

        public bool DisplayWarningsControl { get; private set; }
        public bool DrawRaycastGizmosControl { get; private set; }
        public bool CastRaysOnBothSides { get; private set; }
        public bool PerformSafetyBoxcast { get; private set; }
        public int NumberOfHorizontalRaysPerSide { get; private set; }
        public int NumberOfVerticalRaysPerSide { get; private set; }
        public float DistanceToGroundRayMaximumLength { get; private set; }
        public float BoundsWidth { get; private set; }
        public float BoundsHeight { get; private set; }
        public float RayOffset { get; private set; }
        public float ObstacleHeightTolerance { get; private set; }
        public Vector2 Bounds { get; private set; }
        public Vector2 BoundsCenter { get; private set; }
        public Vector2 BoundsBottomLeftCorner { get; private set; }
        public Vector2 BoundsBottomRightCorner { get; private set; }
        public Vector2 BoundsBottomCenterPosition { get; private set; }
        public Vector2 BoundsTopLeftCorner { get; private set; }
        public Vector2 BoundsTopRightCorner { get; private set; }
        public BoxCollider2D BoxCollider { get; private set; }

        #region public methods

        public RaycastRuntimeData CreateInstance(ref bool displayWarningsControl,
            bool drawRaycastGizmosControl, bool castRaysOnBothSides, bool performSafetyBoxcast,
            int numberOfHorizontalRaysPerSide, int numberOfVerticalRaysPerSide, float distanceToGroundRayMaximumLength,
            float boundsWidth, float boundsHeight, float rayOffset, float obstacleHeightTolerance, Vector2 bounds,
            Vector2 boundsCenter, Vector2 boundsBottomLeftCorner, Vector2 boundsBottomRightCorner,
            Vector2 boundsBottomCenterPosition, Vector2 boundsTopLeftCorner, Vector2 boundsTopRightCorner,
            BoxCollider2D boxCollider)
        {
            return new RaycastRuntimeData
            {
                DisplayWarningsControl = displayWarningsControl,
                DrawRaycastGizmosControl = drawRaycastGizmosControl,
                CastRaysOnBothSides = castRaysOnBothSides,
                PerformSafetyBoxcast = performSafetyBoxcast,
                NumberOfHorizontalRaysPerSide = numberOfHorizontalRaysPerSide,
                NumberOfVerticalRaysPerSide = numberOfVerticalRaysPerSide,
                DistanceToGroundRayMaximumLength = distanceToGroundRayMaximumLength,
                BoundsWidth = boundsWidth,
                BoundsHeight = boundsHeight,
                RayOffset = rayOffset,
                ObstacleHeightTolerance = obstacleHeightTolerance,
                Bounds = bounds,
                BoundsCenter = boundsCenter,
                BoundsBottomLeftCorner = boundsBottomLeftCorner,
                BoundsBottomRightCorner = boundsBottomRightCorner,
                BoundsBottomCenterPosition = boundsBottomCenterPosition,
                BoundsTopLeftCorner = boundsTopLeftCorner,
                BoundsTopRightCorner = boundsTopRightCorner,
                BoxCollider = boxCollider
            };
        }

        #endregion

        #endregion
    }
}
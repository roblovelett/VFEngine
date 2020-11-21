using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public class RaycastRuntimeData : ScriptableObject
    {
        #region properties

        public Raycast raycast;

        public struct Raycast
        {
            public RaycastController RaycastController { get; set; }
            public bool DisplayWarningsControl { get; set; }
            public bool DrawRaycastGizmosControl { get; set; }
            public bool CastRaysOnBothSides { get; set; }
            public bool PerformSafetyBoxcast { get; set; }
            public int NumberOfHorizontalRaysPerSide { get; set; }
            public int NumberOfVerticalRaysPerSide { get; set; }
            public float DistanceToGroundRayMaximumLength { get; set; }
            public float BoundsWidth { get; set; }
            public float BoundsHeight { get; set; }
            public float RayOffset { get; set; }
            public float ObstacleHeightTolerance { get; set; }
            public Vector2 Bounds { get; set; }
            public Vector2 BoundsCenter { get; set; }
            public Vector2 BoundsBottomLeftCorner { get; set; }
            public Vector2 BoundsBottomRightCorner { get; set; }
            public Vector2 BoundsBottomCenterPosition { get; set; }
            public Vector2 BoundsTopLeftCorner { get; set; }
            public Vector2 BoundsTopRightCorner { get; set; }
            public BoxCollider2D BoxCollider { get; set; }
        }

        #region public methods

        public void SetRaycastController(RaycastController controller)
        {
            raycast.RaycastController = controller;
        }
        public void SetRaycast(bool displayWarningsControl, bool drawRaycastGizmosControl, bool castRaysOnBothSides,
            bool performSafetyBoxcast, int numberOfHorizontalRaysPerSide, int numberOfVerticalRaysPerSide,
            float distanceToGroundRayMaximumLength, float boundsWidth, float boundsHeight, float rayOffset,
            float obstacleHeightTolerance, Vector2 bounds, Vector2 boundsCenter, Vector2 boundsBottomLeftCorner,
            Vector2 boundsBottomRightCorner, Vector2 boundsBottomCenterPosition, Vector2 boundsTopLeftCorner,
            Vector2 boundsTopRightCorner, BoxCollider2D boxCollider)
        {
            raycast.DisplayWarningsControl = displayWarningsControl;
            raycast.DrawRaycastGizmosControl = drawRaycastGizmosControl;
            raycast.CastRaysOnBothSides = castRaysOnBothSides;
            raycast.PerformSafetyBoxcast = performSafetyBoxcast;
            raycast.NumberOfHorizontalRaysPerSide = numberOfHorizontalRaysPerSide;
            raycast.NumberOfVerticalRaysPerSide = numberOfVerticalRaysPerSide;
            raycast.DistanceToGroundRayMaximumLength = distanceToGroundRayMaximumLength;
            raycast.BoundsWidth = boundsWidth;
            raycast.BoundsHeight = boundsHeight;
            raycast.RayOffset = rayOffset;
            raycast.ObstacleHeightTolerance = obstacleHeightTolerance;
            raycast.Bounds = bounds;
            raycast.BoundsCenter = boundsCenter;
            raycast.BoundsBottomLeftCorner = boundsBottomLeftCorner;
            raycast.BoundsBottomRightCorner = boundsBottomRightCorner;
            raycast.BoundsBottomCenterPosition = boundsBottomCenterPosition;
            raycast.BoundsTopLeftCorner = boundsTopLeftCorner;
            raycast.BoundsTopRightCorner = boundsTopRightCorner;
            raycast.BoxCollider = boxCollider;
        }

        #endregion

        #endregion
    }
}
using UnityEngine;

// ReSharper disable UnusedMember.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast
{
    using static Vector2;
    using static Mathf;
    using static Time;

    public static class Raycast
    {
        #region fields

        #region private methods

        private static Vector2 SetRaycastToTopOrigin(Vector2 boundsTopLeftCorner, Vector2 boundsTopRightCorner, Transform transform,
            float obstacleTolerance)
        {
            var origin = SetBounds(boundsTopLeftCorner, boundsTopRightCorner);
            var tolerance = SetTolerance(transform, obstacleTolerance);
            origin -= tolerance;
            return origin;
        }

        private static Vector2 SetRaycastFromBottomOrigin(Vector2 boundsBottomLeftCorner, Vector2 boundsBottomRightCorner,  Transform transform,
            float obstacleTolerance)
        {
            var origin = SetBounds(boundsBottomLeftCorner, boundsBottomRightCorner);
            var tolerance = SetTolerance(transform, obstacleTolerance);
            origin += tolerance;
            return origin;
        }
        
        private static Vector2 SetBounds(Vector2 boundsLeft, Vector2 boundsRight)
        {
            return (boundsLeft + boundsRight) / 2;
        }

        private static Vector2 SetTolerance(Transform transform, float obstacleTolerance)
        {
            return (Vector2) transform.up * obstacleTolerance;
        }
        
        private static float SetHorizontalRayLength(float speedX, float boundsWidth, float rayOffset)
        {
            return Abs(speedX * deltaTime) + boundsWidth / 2 + rayOffset * 2;
        }

        
        
        
        
        
        private static Vector2 SetVerticalRaycast(Vector2 bounds1, Vector2 bounds2, Transform t, float offset, float x)
        {
            var ray = SetBounds(bounds1, bounds2);
            ray += (Vector2) t.up * offset;
            ray += (Vector2) t.right * x;
            return ray;
        }
        private static Vector2 SetCurrentRaycastOrigin(Vector2 origin1, Vector2 origin2, int index, int rays)
        {
            return Lerp(origin1, origin2, index / (float) (rays - 1));
        }
        private static float SetPositiveBounds(float offset, float size)
        {
            return (offset + size) / 2;
        }
        private static float SetNegativeBounds(float offset, float size)
        {
            return (offset - size) / 2;
        }

        #endregion

        #endregion

        #region properties

        #region public methods
        
        public static Vector2 OnSetRaycastToTopOrigin(Vector2 boundsTopLeftCorner, Vector2 boundsTopRightCorner, Transform transform,
            float obstacleTolerance)
        {
            return SetRaycastToTopOrigin(boundsTopLeftCorner, boundsTopRightCorner, transform, obstacleTolerance);
        }

        public static Vector2 OnSetRaycastFromBottomOrigin(Vector2 boundsBottomLeftCorner, Vector2 boundsBottomRightCorner, Transform transform,
            float obstacleTolerance)
        {
            return SetRaycastFromBottomOrigin(boundsBottomLeftCorner, boundsBottomRightCorner, transform, obstacleTolerance);
        }
        
        public static float OnSetHorizontalRayLength(float x, float width, float offset)
        {
            return SetHorizontalRayLength(x, width, offset);
        }
        
        public static Vector2 OnSetCurrentRaycastOrigin(Vector2 origin1, Vector2 origin2, int index, int rays)
        {
            return SetCurrentRaycastOrigin(origin1, origin2, index, rays);
        }

        public static Vector2 OnSetBounds(Vector2 boundsLeft, Vector2 boundsRight)
        {
            return SetBounds(boundsLeft, boundsRight);
        }

        public static float OnSetPositiveBounds(float offset, float size)
        {
            return SetPositiveBounds(offset, size);
        }

        public static float OnSetNegativeBounds(float offset, float size)
        {
            return SetNegativeBounds(offset, size);
        }
        
        public static Vector2 OnSetVerticalRaycast(Vector2 boundsLeft, Vector2 boundsRight, Transform transform, float rayOffset, float xPosition)
        {
            return SetVerticalRaycast(boundsLeft, boundsRight, transform, rayOffset, xPosition);
        }

        #endregion

        #endregion
    }
}
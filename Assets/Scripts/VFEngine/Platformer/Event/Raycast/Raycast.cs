using UnityEngine;

// ReSharper disable UnusedMember.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast
{
    using static Vector2;
    using static Mathf;

    public static class Raycast
    {
        #region fields

        #region private methods

        private static Vector2 SetRaycastToTopOrigin(Vector2 boundsTopLeftCorner, Vector2 boundsTopRightCorner,
            Transform transform, float obstacleTolerance)
        {
            var topOrigin = (boundsTopLeftCorner + boundsTopRightCorner) / 2;
            topOrigin -= (Vector2) transform.up * obstacleTolerance;
            return topOrigin;
        }

        private static Vector2 SetRaycastFromBottomOrigin(Vector2 boundsBottomLeftCorner,
            Vector2 boundsBottomRightCorner, Transform transform, float obstacleTolerance)
        {
            var bottomOrigin = (boundsBottomLeftCorner + boundsBottomRightCorner) / 2;
            bottomOrigin += (Vector2) transform.up * obstacleTolerance;
            return bottomOrigin;
        }

        private static float SetHorizontalRayLength(float speedX, float deltaTime, float boundsWidth, float rayOffset)
        {
            var rayLength = Abs(speedX * deltaTime) + boundsWidth / 2 + rayOffset * 2;
            return rayLength;
        }

        private static Vector2 SetCurrentRaycastOrigin(Vector2 raycastFromBottom, Vector2 raycastToTop, int index,
            int numberOfHorizontalRays)
        {
            var origin = Lerp(raycastFromBottom, raycastToTop, index / (float) (numberOfHorizontalRays - 1));
            return origin;
        }

        private static RaycastHit2D SetRaycast(Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance,
            LayerMask mask, Color color, bool drawGizmos)
        {
            if (drawGizmos) Debug.DrawRay(rayOriginPoint, rayDirection * rayDistance, color);
            return Physics2D.Raycast(rayOriginPoint, rayDirection, rayDistance, mask);
        }

        /*private static Vector2 SetVerticalRaycast(Vector2 bounds1, Vector2 bounds2, Transform t, float offset, float x)
        {
            var ray = SetBounds(bounds1, bounds2);
            ray += (Vector2) t.up * offset;
            ray += (Vector2) t.right * x;
            return ray;
        }*/

        #endregion

        #endregion

        #region properties

        #region public methods

        public static Vector2 OnSetRaycastToTopOrigin(Vector2 boundsTopLeftCorner, Vector2 boundsTopRightCorner,
            Transform transform, float obstacleTolerance)
        {
            return SetRaycastToTopOrigin(boundsTopLeftCorner, boundsTopRightCorner, transform, obstacleTolerance);
        }

        public static Vector2 OnSetRaycastFromBottomOrigin(Vector2 boundsBottomLeftCorner,
            Vector2 boundsBottomRightCorner, Transform transform, float obstacleTolerance)
        {
            return SetRaycastFromBottomOrigin(boundsBottomLeftCorner, boundsBottomRightCorner, transform,
                obstacleTolerance);
        }

        public static float OnSetHorizontalRayLength(float speedX, float deltaTime, float boundsWidth, float rayOffset)
        {
            return SetHorizontalRayLength(speedX, deltaTime, boundsWidth, rayOffset);
        }

        public static Vector2 OnSetCurrentRaycastOrigin(Vector2 raycastFromBottom, Vector2 raycastToTop, int index,
            int numberOfHorizontalRays)
        {
            return SetCurrentRaycastOrigin(raycastFromBottom, raycastToTop, index, numberOfHorizontalRays);
        }

        public static RaycastHit2D OnSetRaycast(Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance,
            LayerMask mask, Color color, bool drawGizmos)
        {
            return SetRaycast(rayOriginPoint, rayDirection, rayDistance, mask, color, drawGizmos);
        }

        /*public static Vector2 OnSetVerticalRaycast(Vector2 boundsLeft, Vector2 boundsRight, Transform transform, float rayOffset, float xPosition)
        {
            return SetVerticalRaycast(boundsLeft, boundsRight, transform, rayOffset, xPosition);
        }*/

        #endregion

        #endregion
    }
}
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
            var origin = ApplyBoundsToOrigin(boundsTopLeftCorner, boundsTopRightCorner);
            origin -= ApplyTransformToHorizontalOrigin(transform, obstacleTolerance);
            return origin;
        }

        private static Vector2 SetRaycastFromBottomOrigin(Vector2 boundsBottomLeftCorner,
            Vector2 boundsBottomRightCorner, Transform transform, float obstacleTolerance)
        {
            var origin = ApplyBoundsToOrigin(boundsBottomLeftCorner, boundsBottomRightCorner);
            origin += ApplyTransformToHorizontalOrigin(transform, obstacleTolerance);
            return origin;
        }

        private static Vector2 ApplyTransformToHorizontalOrigin(Transform transform, float obstacleTolerance)
        {
            return (Vector2) transform.up * obstacleTolerance;
        }

        private static Vector2 SetDownRaycastFromLeftOrigin(Vector2 boundsBottomLeftCorner, Vector2 boundsTopLeftCorner,
            Transform transform, float rayOffset, Vector2 newPosition)
        {
            var origin = SetDownRaycastOrigin(boundsBottomLeftCorner, boundsTopLeftCorner, transform, rayOffset,
                newPosition);
            return origin;
        }

        private static Vector2 SetDownRaycastToRightOrigin(Vector2 boundsBottomRightCorner,
            Vector2 boundsTopRightCorner, Transform transform, float rayOffset, Vector2 newPosition)
        {
            var origin = SetDownRaycastOrigin(boundsBottomRightCorner, boundsTopRightCorner, transform, rayOffset,
                newPosition);
            return origin;
        }

        private static Vector2 SetDownRaycastOrigin(Vector2 bounds1, Vector2 bounds2, Transform transform,
            float rayOffset, Vector2 newPosition)
        {
            var origin = ApplyBoundsToOrigin(bounds1, bounds2);
            origin = ApplyTransformToBottomOrigin(origin, newPosition, transform, rayOffset);
            return origin;
        }

        private static Vector2 ApplyTransformToBottomOrigin(Vector2 origin, Vector2 newPosition, Transform transform,
            float rayOffset)
        {
            origin += (Vector2) transform.up * rayOffset;
            origin += (Vector2) transform.right * newPosition.x;
            return origin;
        }

        private static Vector2 ApplyBoundsToOrigin(Vector2 bounds1, Vector2 bounds2)
        {
            return (bounds1 + bounds2) / 2;
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

        private static float SetRayLength(float boundsHeight, float rayOffset)
        {
            return boundsHeight / 2 + rayOffset;
        }

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

        public static Vector2 OnSetDownRaycastFromLeftOrigin(Vector2 boundsBottomLeftCorner,
            Vector2 boundsTopLeftCorner, Transform transform, float rayOffset, Vector2 newPosition)
        {
            return SetDownRaycastFromLeftOrigin(boundsBottomLeftCorner, boundsTopLeftCorner, transform, rayOffset,
                newPosition);
        }

        public static Vector2 OnSetDownRaycastToRightOrigin(Vector2 boundsBottomRightCorner,
            Vector2 boundsTopRightCorner, Transform transform, float rayOffset, Vector2 newPosition)
        {
            return SetDownRaycastToRightOrigin(boundsBottomRightCorner, boundsTopRightCorner, transform, rayOffset,
                newPosition);
        }

        public static float OnSetRayLength(float boundsHeight, float rayOffset)
        {
            return SetRayLength(boundsHeight, rayOffset);
        }

        #endregion

        #endregion
    }
}
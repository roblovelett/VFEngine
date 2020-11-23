using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    public class UpRaycastRuntimeData
    {
        #region properties

        public float UpRaycastSmallestDistance { get; private set; }
        public Vector2 CurrentUpRaycastOrigin { get; private set; }
        public RaycastHit2D CurrentUpRaycastHit { get; private set; }

        #region public methods

        public static UpRaycastRuntimeData CreateInstance(float upRaycastSmallestDistance,
            Vector2 currentUpRaycastOrigin, RaycastHit2D currentUpRaycastHit)
        {
            return new UpRaycastRuntimeData
            {
                UpRaycastSmallestDistance = upRaycastSmallestDistance,
                CurrentUpRaycastOrigin = currentUpRaycastOrigin,
                CurrentUpRaycastHit = currentUpRaycastHit
            };
        }

        #endregion

        #endregion
    }
}
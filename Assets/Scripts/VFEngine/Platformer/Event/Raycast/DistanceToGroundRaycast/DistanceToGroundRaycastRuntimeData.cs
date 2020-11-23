using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    public class DistanceToGroundRaycastRuntimeData
    {
        #region properties

        public RaycastHit2D DistanceToGroundRaycastHit { get; private set; }

        #region public methods

        public static DistanceToGroundRaycastRuntimeData CreateInstance(RaycastHit2D distanceToGroundRaycastHit)
        {
            return new DistanceToGroundRaycastRuntimeData {DistanceToGroundRaycastHit = distanceToGroundRaycastHit};
        }

        #endregion

        #endregion
    }
}
using UnityEngine;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    public class SafetyBoxcastRuntimeData
    {
        #region properties

        public RaycastHit2D SafetyBoxcastHit { get; private set; }

        #region public methods

        public static SafetyBoxcastRuntimeData CreateInstance(RaycastHit2D safetyBoxcastHit)
        {
            return new SafetyBoxcastRuntimeData {SafetyBoxcastHit = safetyBoxcastHit};
        }

        #endregion

        #endregion
    }
}
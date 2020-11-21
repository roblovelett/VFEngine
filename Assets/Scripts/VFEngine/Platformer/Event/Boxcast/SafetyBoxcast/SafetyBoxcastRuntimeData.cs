using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    public class SafetyBoxcastRuntimeData : ScriptableObject
    {
        #region properties

        public SafetyBoxcast safetyBoxcast;

        public struct SafetyBoxcast
        {
            public RaycastHit2D SafetyBoxcastHit { get; set; }
        }

        #region public methods

        public void SetSafetyBoxcast(RaycastHit2D hit)
        {
            safetyBoxcast.SafetyBoxcastHit = hit;
        }

        #endregion

        #endregion
    }
}
using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    public class SafetyBoxcastData
    {
        #region properties

        #region dependencies

        public bool PerformSafetyBoxcast { get; set; }

        #endregion

        public RaycastHit2D SafetyBoxcastHit { get; set; }

        #region public methods

        public void ApplySettings(SafetyBoxcastSettings settings)
        {
            PerformSafetyBoxcast = settings.performSafetyBoxcast;
        }

        #endregion

        #endregion
    }
}
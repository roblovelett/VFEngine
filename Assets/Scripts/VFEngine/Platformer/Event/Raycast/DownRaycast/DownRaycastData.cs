using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static Vector2;

    public class DownRaycastData
    {
        #region properties

        public Vector2 Origin { get; set; }
        public RaycastHit2D Hit { get; set; }

        #region public methods

        public void InitializeData()
        {
            Origin = zero;
            Hit = new RaycastHit2D();
        }

        #endregion

        #endregion
    }
}
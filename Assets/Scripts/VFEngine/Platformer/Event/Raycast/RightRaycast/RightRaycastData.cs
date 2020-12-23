using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static Vector2;
    public class RightRaycastData
    {
        #region properties

        public float RayLength { get; set; }
        public Vector2 Origin { get; set; }
        public RaycastHit2D Hit { get; set; }
        public void InitializeData()
        {
            RayLength = 0;
            Origin = zero;
            Hit = new RaycastHit2D();
        }
        
        #endregion
    }
}
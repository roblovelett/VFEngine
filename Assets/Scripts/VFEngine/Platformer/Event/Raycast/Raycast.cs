using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public class Raycast
    {
        #region fields
        #endregion
        #region properties
        public RaycastData Data { get; }

        #region public methods
        
        #region constructors

        public Raycast(RaycastSettings settings, Collider2D collider)
        {
            Data = new RaycastData(settings, collider);
        }

        public Raycast(RaycastSettings settings)
        {
            Data = new RaycastData(settings);
        }

        public Raycast(Collider2D collider)
        {
            Data = new RaycastData(collider);
        }

        public Raycast()
        {
            Data = new RaycastData();
        }
        
        #endregion

        #endregion

        #endregion
    }
}
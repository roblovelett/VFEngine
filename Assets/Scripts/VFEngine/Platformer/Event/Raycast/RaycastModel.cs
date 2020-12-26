using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public class RaycastModel
    {
        #region fields
        #endregion
        #region properties
        public RaycastData Data { get; }

        #region public methods
        
        #region constructors

        public RaycastModel(RaycastSettings settings, Collider2D collider)
        {
            Data = new RaycastData(settings, collider);
        }

        public RaycastModel(RaycastSettings settings)
        {
            Data = new RaycastData(settings);
        }

        public RaycastModel(Collider2D collider)
        {
            Data = new RaycastData(collider);
        }

        public RaycastModel()
        {
            Data = new RaycastData();
        }
        
        #endregion

        #endregion

        #endregion
    }
}
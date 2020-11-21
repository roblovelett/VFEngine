using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Event.Boxcast
{
    public class BoxcastRuntimeData : ScriptableObject
    {
        #region properties

        public Boxcast boxcast;

        public struct Boxcast
        {
            public BoxcastController BoxcastController { get; set; }
        }

        #region public methods

        public void SetBoxcastController(BoxcastController controller)
        {
            boxcast.BoxcastController = controller;
        }

        #endregion

        #endregion
    }
}
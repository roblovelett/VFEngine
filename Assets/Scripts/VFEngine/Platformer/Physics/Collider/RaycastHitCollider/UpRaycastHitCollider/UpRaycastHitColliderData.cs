using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    public class UpRaycastHitColliderData
    {
        #region properties

        public bool Colliding { get; set; }
        public RaycastHit2D Hit { get; set; }

        #region public methods

        public void Initialize()
        {
            Reset();
        }

        public void Reset()
        {
            Colliding = false;
            Hit = new RaycastHit2D();
        }

        #endregion

        #endregion
    }
}
using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    public class DownRaycastHitColliderData
    {
        #region properties

        public bool Colliding { get; set; }
        public bool OnGround { get; set; }
        public bool OnSlope { get; set; }
        public int GroundLayer { get; set; }
        public float GroundAngle { get; set; }
        public float GroundDirection { get; set; }
        public RaycastHit2D Hit { get; set; }

        #region public methods

        public void Initialize()
        {
            OnSlope = OnGround && GroundAngle != 0;
            Reset();
        }

        public void Reset()
        {
            Colliding = false;
            Hit = new RaycastHit2D();
            OnGround = false;
            GroundAngle = 0;
            GroundDirection = 0;
        }

        #endregion

        #endregion
    }
}
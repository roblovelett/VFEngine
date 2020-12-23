using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    public class RaycastCollision
    {
        #region properties

        public bool colliding;
        public bool onGround;
        public bool OnSlope => onGround && groundAngle != 0;
        public RaycastHit2D hit;
        public int groundDirection;
        public int groundLayer;
        public float groundAngle;

        #region public methods

        public void Reset()
        {
            colliding = false;
            onGround = false;
            hit = new RaycastHit2D();
            groundDirection = 0;
            groundAngle = 0;
        }

        public void Initialize()
        {
            Reset();
        }
        #endregion

        #endregion
    }
}
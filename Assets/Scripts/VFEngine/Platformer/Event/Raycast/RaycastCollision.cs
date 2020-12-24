using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public struct RaycastCollision
    {
        #region properties

        public bool colliding;
        public bool onGround;
        public bool OnSlope => onGround && groundAngle != 0;
        public int groundDirection;
        public int groundLayer;
        public float groundAngle;

        #region public methods

        public void Reset()
        {
            colliding = false;
            onGround = false;
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
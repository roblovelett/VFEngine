using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static Vector2;
    using static Mathf;
    public class DownRaycastHitColliderData
    {
        #region properties

        #region dependencies

        #endregion

        public RaycastHit2D Hit { get; set; }
        public RaycastCollision Collision { get; set; }
        
        #region public methods

        public void InitializeData()
        {
            Collision = new RaycastCollision();
            Collision.Reset();
        }

        public void Initialize()
        { }

        public void OnHitConnected()
        {
            Collision.colliding = true;
            Collision.onGround = true;
            Collision.groundDirection = (int) Sign(Hit.normal.x);
            Collision.groundLayer = Hit.collider.gameObject.layer;
            Collision.groundAngle = Angle(Hit.normal, up);
        }

        #endregion

        #endregion
    }
}
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace VFEngine.Platformer.Physics
{
    public class PhysicsModel
    {
        #region fields

        private readonly PhysicsData physics;
        private RaycastController raycast;
        
        #region internal
        /*
        private bool IgnoreFriction => p.IgnoreFriction;
        private bool OnGround => downRaycastHitCollider.Collision.onGround;
        private bool OnSlope => downRaycastHitCollider.Collision.OnSlope;
        private bool VerticalSpeed => p.Speed.y > 0;
        private bool ExceededMaximumSlopeAngle => GroundAngle > MaximumSlopeAngle;
        private bool MetMinimumWallAngle => GroundAngle < MinimumWallAngle;
        private bool NoHorizontalSpeed => HorizontalSpeed == 0;
        private bool AddToHorizontalExternalForce => OnSlope && ExceededMaximumSlopeAngle && (MetMinimumWallAngle || NoHorizontalSpeed);
        private int GroundDirection => downRaycastHitCollider.Collision.groundDirection;
        private float HorizontalSpeed => p.Speed.x;
        private float GroundFriction => p.GroundFriction;
        private float AirFriction => p.AirFriction;
        private float Friction => OnGround ? GroundFriction : AirFriction;
        private float Gravity => p.Gravity;
        private float GravityScale => p.GravityScale;
        private float GravitationalForce => Gravity * GravityScale * deltaTime;
        private float GroundAngle => downRaycastHitCollider.Collision.groundAngle;
        private float MaximumSlopeAngle => p.MaximumSlopeAngle;
        private float MinimumWallAngle => p.MinimumWallAngle;
        private Vector2 ExternalForce => p.ExternalForce;
        private float Distance => Abs(p.DeltaMovement.x);
        private float VerticalMovement => Sin(GroundAngle * Deg2Rad) * Distance;
        private float VerticalDeltaMovement => p.DeltaMovementY;
        private bool ClimbSlope => VerticalDeltaMovement <= VerticalMovement;
        */
        #endregion
        
        #endregion

        #region properties

        public PhysicsData Data => physics;

        #region public methods

        #region constructors

        public PhysicsModel(GameObject character,PhysicsSettings settings, RaycastController raycastController)
        {
            physics = new PhysicsData(settings, character);
            raycast = raycastController;
        }

        #endregion

        #endregion

        #endregion
    }
}
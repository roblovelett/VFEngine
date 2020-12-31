using UnityEngine;
using VFEngine.Platformer.Event.Raycast;

namespace VFEngine.Platformer.Physics
{
    using static Time;
    using static Mathf;

    public class PhysicsModel
    {
        #region fields

        private readonly RaycastController _raycastController;

        #region internal

        #endregion

        #region private methods

        private void StopVerticalForces()
        {
            Physics.SetSpeedY(0);
            Physics.SetExternalForceY(0);
        }

        #endregion

        #endregion

        #region properties

        public PhysicsData Data => Physics;

        #region public methods

        #region constructors

        public PhysicsModel(GameObject character, PhysicsSettings settings, RaycastController raycast)
        {
            Physics = new PhysicsData(settings, character);
            _raycastController = raycast;
        }

        #endregion

        public void SetHorizontalMovementDirection()
        {
            Physics.SetHorizontalMovementDirection();
        }

        private PhysicsData Physics { get; }
        private RaycastData Raycast => _raycastController.Data;
        private RaycastCollision Collision => Raycast.Collision;
        private Vector2 Speed => Physics.Speed;
        private float GroundFriction => Physics.GroundFriction;
        private float AirFriction => Physics.AirFriction;
        private bool OnGround => Collision.OnGround;
        private float Friction => OnGround ? GroundFriction : AirFriction;

        public void SetExternalForce()
        {
            Physics.SetExternalForce(Friction);
        }

        private float Gravity => Physics.Gravity * Physics.GravityScale * deltaTime;
        private bool VerticalSpeed => Speed.y > 0;

        public void ApplyGravity()
        {
            if (VerticalSpeed) Physics.ApplyGravityToSpeed(Gravity);
            else Physics.ApplyGravityToExternalForce(Gravity);
        }

        private int GroundDirection => Collision.GroundDirection;

        public void ApplyForcesToExternal()
        {
            Physics.ApplyForcesToHorizontalExternalForce(GroundDirection);
        }

        private Vector2 Movement => Physics.Movement;
        private float MovementX => Movement.x;
        private float DistanceX => Abs(MovementX);
        private float GroundAngle => Collision.GroundAngle;
        private float GroundAngleRad => GroundAngle * Deg2Rad;
        private float SlopeX => Cos(GroundAngleRad) * DistanceX * HorizontalMovementDirection;
        private float DescendSlopeY => -Sin(GroundAngleRad) * DistanceX;
        private Vector2 DescendSlopePosition => new Vector2(SlopeX, DescendSlopeY);

        public void DescendSlope()
        {
            Physics.SetMovement(DescendSlopePosition);
            StopVerticalForces();
        }

        private float MovementY => Movement.y;
        private float ClimbSlopeY => Sin(GroundAngleRad) * DistanceX;
        private bool CanClimbSlope => MovementY <= ClimbSlopeY;
        private Vector2 ClimbSlopePosition => new Vector2(SlopeX, ClimbSlopeY);
        private RaycastHit2D Hit => Raycast.Hit;
        private float HitDistance => Hit.distance;
        private float SkinWidth => Raycast.SkinWidth;
        private float ClimbTotalDistance => HitDistance - SkinWidth;
        
        public void ClimbSlope()
        {
            if (!CanClimbSlope) return;
            Physics.SetMovement(ClimbSlopePosition);
            StopVerticalForces();
        }

        public void OnFirstSideHit()
        {
            Physics.OnPreClimbSlopeBehavior(ClimbTotalDistance);
            ClimbSlope();
            Physics.OnPostClimbSlopeBehavior(ClimbTotalDistance);
        }

        private int HorizontalMovementDirection => Physics.HorizontalMovementDirection;
        private float SideHitMovementX => Min(DistanceX, ClimbTotalDistance) * HorizontalMovementDirection; 
        public void OnSideHit()
        {
            Physics.SetMovementX(SideHitMovementX);
        }

        public void StopVerticalMovement()
        {
            Physics.SetMovementY(0);
        }

        private int VerticalMovementDirection => Physics.VerticalMovementDirection;
        private float MovementYSlopeApplied => Tan(GroundAngleRad) * DistanceX * VerticalMovementDirection;
        public void OnAdjustVerticalMovementToSlope()
        {
            Physics.SetMovementY(MovementYSlopeApplied);
        }

        public void OnHitWall()
        {
            Physics.OnHitWall();
        }

        #endregion

        #endregion
    }
}
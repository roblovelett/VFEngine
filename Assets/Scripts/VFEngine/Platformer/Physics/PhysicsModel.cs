using UnityEngine;
using VFEngine.Platformer.Event.Raycast;

namespace VFEngine.Platformer.Physics
{
    using static Time;
    using static Mathf;

    public class PhysicsModel
    {
        #region fields

        //private readonly RaycastController _raycastController;

        #region internal

        #endregion

        #region private methods

        private void StopVerticalForces()
        {
            Physics.SetVerticalSpeed(0);
            Physics.SetVerticalExternalForce(0);
        }

        #endregion

        #endregion

        #region properties

        public PhysicsData Data => Physics;

        #region public methods

        #region constructors

        public PhysicsModel(ref GameObject character, ref PhysicsSettings settings, ref RaycastData raycast)
        {
            Physics = new PhysicsData(settings, character);
            RaycastData = raycast;
        }

        #endregion

        public void SetHorizontalMovementDirection()
        {
            Physics.SetHorizontalMovementDirection();
        }

        private PhysicsData Physics { get; }
        private RaycastData RaycastData { get; set; }//=> _raycastController.Data;
        private RaycastCollision Collision => RaycastData.Collision;
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
        private float HorizontalMovement => Movement.x;
        private float HorizontalDistance => Abs(HorizontalMovement);
        private float GroundAngle => Collision.GroundAngle;
        private float GroundAngleRad => GroundAngle * Deg2Rad;
        private float SlopeHorizontalPosition => Cos(GroundAngleRad) * HorizontalDistance * HorizontalMovementDirection;
        private float DescendSlopeVerticalPosition => -Sin(GroundAngleRad) * HorizontalDistance;
        private Vector2 DescendSlopePosition => new Vector2(SlopeHorizontalPosition, DescendSlopeVerticalPosition);

        public void DescendSlope()
        {
            Physics.SetMovement(DescendSlopePosition);
            StopVerticalForces();
        }

        private float VerticalMovement => Movement.y;
        private float ClimbSlopeVerticalPosition => Sin(GroundAngleRad) * HorizontalDistance;
        private bool CanClimbSlope => VerticalMovement <= ClimbSlopeVerticalPosition;
        private Vector2 ClimbSlopePosition => new Vector2(SlopeHorizontalPosition, ClimbSlopeVerticalPosition);
        private RaycastHit2D Hit => RaycastData.Hit;
        private float HitDistance => Hit.distance;
        private float SkinWidth => RaycastData.SkinWidth;
        private float ClimbTotalDistance => HitDistance - SkinWidth;
        
        public void OnClimbSlope()
        {
            if (!CanClimbSlope) return;
            Physics.SetMovement(ClimbSlopePosition);
            StopVerticalForces();
        }

        public void OnFirstSideHit()
        {
            Physics.OnPreClimbSlopeBehavior(ClimbTotalDistance);
            OnClimbSlope();
            Physics.OnPostClimbSlopeBehavior(ClimbTotalDistance);
        }

        private int HorizontalMovementDirection => Physics.HorizontalMovementDirection;
        private float SideHitHorizontalMovement => Min(HorizontalDistance, ClimbTotalDistance) * HorizontalMovementDirection; 
        public void OnSideHit()
        {
            Physics.SetHorizontalMovement(SideHitHorizontalMovement);
        }

        public void StopVerticalMovement()
        {
            Physics.SetVerticalMovement(0);
        }

        private int VerticalMovementDirection => Physics.VerticalMovementDirection;
        private float VerticalMovementSlopeApplied => Tan(GroundAngleRad) * HorizontalDistance * VerticalMovementDirection;
        public void OnAdjustVerticalMovementToSlope()
        {
            Physics.SetVerticalMovement(VerticalMovementSlopeApplied);
        }

        public void OnHitWall()
        {
            Physics.OnHitWall();
        }

        public void StopHorizontalSpeed()
        {
            Physics.SetHorizontalSpeed(0);
        }

        private float VerticalHitDistance => Hit.distance;
        private float VerticalMoveOnVerticalHit => (VerticalHitDistance - SkinWidth) * VerticalMovementDirection;
        public void OnVerticalHit()
        {
            Physics.SetVerticalMovement(VerticalMoveOnVerticalHit);
        }

        public void OnPlatformerApplyGroundAngle()
        {
            Physics.OnApplyGroundAngle(GroundAngleRad);
        }

        #endregion

        #endregion
    }
}
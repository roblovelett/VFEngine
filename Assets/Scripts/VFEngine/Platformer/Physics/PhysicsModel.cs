using UnityEngine;
using VFEngine.Platformer.Event.Raycast;

namespace VFEngine.Platformer.Physics
{
    using static Time;
    using static Mathf;
    using static Vector2;

    public class PhysicsModel
    {
        #region fields

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
        private RaycastData RaycastData { get; }
        private RaycastCollision Collision => RaycastData.Collision;
        private Vector2 Speed => Physics.Speed;
        private float GroundFriction => Physics.GroundFriction;
        private float AirFriction => Physics.AirFriction;
        private bool OnGround => Collision.OnGround;
        private float Friction => OnGround ? GroundFriction : AirFriction;
        private Vector2 ExternalForce => Physics.ExternalForce;
        private float ExternalForceMaxDistanceDelta => ExternalForce.magnitude * Friction * deltaTime;

        public void MoveExternalForceTowards()
        {
            Physics.MoveExternalForceTowards(ExternalForceMaxDistanceDelta);
        }

        private float Gravity => Physics.Gravity * Physics.GravityScale * deltaTime;
        private bool VerticalSpeed => Speed.y > 0;

        public void ApplyGravity()
        {
            if (VerticalSpeed) Physics.ApplyGravityToSpeed(Gravity);
            else Physics.ApplyGravityToExternalForce(Gravity);
        }

        private int GroundDirection => Collision.GroundDirection;
        private float AppliedToHorizontalExternalForce => -Gravity * GroundFriction * GroundDirection * deltaTime / 4;

        public void ApplyForcesToExternal()
        {
            Physics.ApplyForcesToHorizontalExternalForce(AppliedToHorizontalExternalForce);
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

        private float SideHitHorizontalMovement =>
            Min(HorizontalDistance, ClimbTotalDistance) * HorizontalMovementDirection;

        public void OnSideHit()
        {
            Physics.SetHorizontalMovement(SideHitHorizontalMovement);
        }

        public void StopVerticalMovement()
        {
            Physics.SetVerticalMovement(0);
        }

        private int VerticalMovementDirection => Physics.VerticalMovementDirection;

        private float VerticalMovementSlopeApplied =>
            Tan(GroundAngleRad) * HorizontalDistance * VerticalMovementDirection;

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

        private float GroundAngleHorizontalMovement =>
            VerticalMovement / Tan(GroundAngleRad) * HorizontalMovementDirection;

        public void OnPlatformerApplyGroundAngle()
        {
            Physics.OnApplyGroundAngle(GroundAngleHorizontalMovement);
        }

        private float ClimbSteepSlopeHorizontalMovement => ClimbTotalDistance * HorizontalMovement;

        public void OnClimbSteepSlope()
        {
            Physics.SetHorizontalMovement(ClimbSteepSlopeHorizontalMovement);
        }

        private float ClimbSlopeAngle => Angle(Hit.normal, up);
        private float ClimbSlopeAngleRad => ClimbSlopeAngle * Deg2Rad;
        private bool ClimbSlopeAngleNotGroundAngle => ClimbSlopeAngle < GroundAngle;
        private bool PositiveSlopeAngle => ClimbSlopeAngle > 0;
        private float ClimbSlopeAngleTan => Tan(ClimbSlopeAngleRad);
        private float GroundAngleTan => Tan(GroundAngleRad);
        private float GroundAngleSin => Sin(GroundAngleRad);

        private float ClimbOvershootOnPositiveAngle =>
            (2 * ClimbSlopeAngleTan * HitDistance - GroundAngleTan * HitDistance) /
            (ClimbSlopeAngleTan * GroundAngleSin - GroundAngleTan * GroundAngleSin);

        private float ClimbOvershoot => HitDistance / Sin(GroundAngleRad);
        private float ClimbMildSlopeOvershoot => PositiveSlopeAngle ? ClimbOvershootOnPositiveAngle : ClimbOvershoot;

        private float NegativeHorizontalMovement =>
            Cos(GroundAngleRad) * ClimbMildSlopeOvershoot * HorizontalMovementDirection;

        private float NegativeVerticalMovement => Sin(GroundAngleRad) * ClimbMildSlopeOvershoot;

        private float PositiveHorizontalMovement =>
            Cos(ClimbSlopeAngleRad) * ClimbMildSlopeOvershoot * HorizontalMovementDirection;

        private float PositiveVerticalMovement => Sin(ClimbSlopeAngleRad) * ClimbMildSlopeOvershoot;
        private float HorizontalClimbMildSlopeMovement => PositiveHorizontalMovement - NegativeHorizontalMovement;
        private float VerticalClimbMildSlopeMovement => PositiveVerticalMovement - NegativeVerticalMovement + SkinWidth;

        private Vector2 ClimbMildSlopeMovement =>
            new Vector2(HorizontalClimbMildSlopeMovement, VerticalClimbMildSlopeMovement);

        public void OnClimbMildSlope()
        {
            if (!ClimbSlopeAngleNotGroundAngle) return;
            Physics.OnClimbMildSlope(ClimbMildSlopeMovement);
        }

        private float DescendMildSlopeVerticalMovement => -(HitDistance - SkinWidth);
        public void OnDescendMildSlope()
        {
            Physics.SetVerticalMovement(DescendMildSlopeVerticalMovement);
        }

        
        public void OnDescendSteepSlope()
        {
            
        }

        #endregion

        #endregion
    }
}
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
        private PhysicsData Physics { get; }
        private RaycastData Raycast { get; set; }
        
        
        #endregion

        #region private methods

        /*private void StopVerticalForces()
        {
            Physics.SetVerticalSpeed(0);
            Physics.SetVerticalExternalForce(0);
        }*/

        #endregion

        #endregion

        #region properties

        public PhysicsData Data => Physics;

        #region public methods

        #region constructors

        public PhysicsModel(ref GameObject character, PhysicsSettings settings)
        {
            //Physics = new PhysicsData(ref character, settings);
        }

        #endregion

        public void SetDependencies(RaycastData raycast)
        {
            Raycast = raycast;
        }
        
        public void InitializeFrame()
        {
            
        }

        #endregion

        #endregion

        /*public void SetHorizontalMovementDirection()
        {
            Physics.SetHorizontalMovementDirection();
        }*/
        
        /*private Vector2 Speed => Physics.Speed;
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
        private RaycastHit2D Hit => Raycast.Hit;
        private float HitDistance => Hit.distance;
        private float SkinWidth => Raycast.SkinWidth;
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

        private float ClimbMildSlopeAngle => Angle(Hit.normal, up);
        private float ClimbMildSlopeAngleRad => ClimbMildSlopeAngle * Deg2Rad;
        private bool ClimbMildSlopeNotGroundAngle => ClimbMildSlopeAngle < GroundAngle;
        private bool PositiveClimbSlopeAngle => ClimbMildSlopeAngle > 0;
        private float ClimbSlopeAngleTan => Tan(ClimbMildSlopeAngleRad);
        private float ClimbSlopeAngleSin => Sin(ClimbMildSlopeAngleRad);
        private float GroundAngleTan => Tan(GroundAngleRad);
        private float GroundAngleSin => Sin(GroundAngleRad);

        private float MildSlopeOvershootOnPositiveAngle =>
            (2 * ClimbSlopeAngleTan * HitDistance - GroundAngleTan * HitDistance) /
            (ClimbSlopeAngleTan * GroundAngleSin - GroundAngleTan * GroundAngleSin);

        private float MildSlopeOvershootDefault => HitDistance / GroundAngleSin;

        private float MildSlopeOvershoot =>
            PositiveClimbSlopeAngle ? MildSlopeOvershootOnPositiveAngle : MildSlopeOvershootDefault;

        private float MildSlopeNegativeHorizontalMovement =>
            Cos(GroundAngleRad) * MildSlopeOvershoot * HorizontalMovementDirection;

        private float MildSlopeNegativeVerticalMovement => GroundAngleSin * MildSlopeOvershoot;

        private float MildSlopePositiveHorizontalMovement =>
            Cos(ClimbMildSlopeAngleRad) * MildSlopeOvershoot * HorizontalMovementDirection;

        private float MildSlopePositiveVerticalMovement => ClimbSlopeAngleSin * MildSlopeOvershoot;

        private float HorizontalMildSlopeMovement =>
            MildSlopePositiveHorizontalMovement - MildSlopeNegativeHorizontalMovement;

        private float VerticalMildSlopeMovement =>
            MildSlopePositiveVerticalMovement - MildSlopeNegativeVerticalMovement + SkinWidth;

        private Vector2 ClimbMildSlopeMovement => new Vector2(HorizontalMildSlopeMovement, VerticalMildSlopeMovement);

        public void OnClimbMildSlope()
        {
            if (!ClimbMildSlopeNotGroundAngle) return;
            Physics.AddToMovement(ClimbMildSlopeMovement);
        }

        private float DescendMildSlopeVerticalMovement => -(HitDistance - SkinWidth);

        public void OnDescendMildSlope()
        {
            Physics.SetVerticalMovement(DescendMildSlopeVerticalMovement);
        }

        private float DescendSteepSlopeAngle => ClimbMildSlopeAngle;
        private float DescendSteepSlopeAngleRad => DescendSteepSlopeAngle * Deg2Rad;
        private bool PositiveDescendSteepSlopeAngle => DescendSteepSlopeAngle > GroundAngle;
        private bool FacingRight => Physics.FacingRight;

        private bool SetDescendSteepSlopeMovement =>
            PositiveDescendSteepSlopeAngle && (int) Sin(Hit.normal.x) == (FacingRight ? 1 : -1);

        private bool PositiveGroundAngle => GroundAngle > 0;
        private float SteepSlopeAngleSin => GroundAngleSin * Deg2Rad;
        private float GroundAngleCos => Cos(GroundAngle);
        private float SteepSlopeAngleCos => GroundAngleCos * Deg2Rad;
        private float SteepSlopeAngleTan => Tan(DescendSteepSlopeAngleRad);

        private float SteepSlopeOvershootOnPositiveGroundAngle => HitDistance * SteepSlopeAngleCos /
                                                                  (SteepSlopeAngleTan / SteepSlopeAngleCos -
                                                                   SteepSlopeAngleSin);

        private float SteepSlopeOvershootDefault => HitDistance / SteepSlopeAngleTan;

        private float SteepSlopeOvershoot =>
            PositiveGroundAngle ? SteepSlopeOvershootOnPositiveGroundAngle : SteepSlopeOvershootDefault;

        private float SteepSlopeNegativeHorizontalMovement =>
            Cos(GroundAngleRad) * SteepSlopeOvershoot * HorizontalMovementDirection;

        private float SteepSlopeNegativeVerticalMovement => -Sin(GroundAngleRad) * SteepSlopeOvershoot;

        private float SteepSlopePositiveHorizontalMovement =>
            Cos(DescendSteepSlopeAngleRad) * SteepSlopeOvershoot * HorizontalMovementDirection;

        private float SteepSlopePositiveVerticalMovement => -Sin(DescendSteepSlopeAngleRad) * SteepSlopeOvershoot;

        private float SteepSlopeHorizontalMovement =>
            SteepSlopePositiveHorizontalMovement - SteepSlopeNegativeHorizontalMovement;

        private float SteepSlopeVerticalMovement =>
            SteepSlopePositiveVerticalMovement - SteepSlopeNegativeVerticalMovement - SkinWidth;

        private Vector2 DescendSteepSlopeMovement =>
            new Vector2(SteepSlopeHorizontalMovement, SteepSlopeVerticalMovement);

        public void OnDescendSteepSlope()
        {
            if (!SetDescendSteepSlopeMovement) return;
            Physics.AddToMovement(DescendSteepSlopeMovement);
        }

        public void OnTranslateMovement()
        {
            Physics.SetTransformTranslate(Movement);
        }

        public void OnCeilingOrGroundCollision()
        {
            Physics.OnCeilingOrGroundCollision();
        }

        public void ResetFriction()
        {
            Physics.SetIgnoreFriction(false);
        }*/
    }
}
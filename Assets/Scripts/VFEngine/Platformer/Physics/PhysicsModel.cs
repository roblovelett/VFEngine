using UnityEngine;
using VFEngine.Platformer.Event.Raycast;

namespace VFEngine.Platformer.Physics
{
    using static Time;
    using static Mathf;

    public class PhysicsModel
    {
        #region fields

        private readonly RaycastController raycastController;

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
            raycastController = raycast;
        }

        #endregion

        public void SetHorizontalMovementDirection()
        {
            Physics.SetMovementDirection();
        }

        private PhysicsData Physics { get; }
        private RaycastData Raycast => raycastController.Data;
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

        private Vector2 DeltaMovement => Physics.Movement;
        private float DistanceX => Abs(DeltaMovement.x);
        private float MovementX => DeltaMovement.x;
        private float GroundAngle => Collision.GroundAngle;
        private float SlopeX => Cos(GroundAngle * Deg2Rad) * DistanceX * Sign(MovementX);
        private float DescendSlopeY => -Sin(GroundAngle * Deg2Rad) * DistanceX;
        private Vector2 DescendSlopePosition => new Vector2(SlopeX, DescendSlopeY);

        public void DescendSlope()
        {
            Physics.SetMovement(DescendSlopePosition);
            StopVerticalForces();
        }

        private float MovementY => DeltaMovement.y;
        private float ClimbSlopeY => Sin(GroundAngle * Deg2Rad) * DistanceX;
        private bool CanClimbSlope => MovementY <= ClimbSlopeY;
        private Vector2 ClimbSlopePosition => new Vector2(SlopeX, ClimbSlopeY);

        public void ClimbSlope()
        {
            if (!CanClimbSlope) return;
            Physics.SetMovement(ClimbSlopePosition);
            StopVerticalForces();
        }

        #endregion

        #endregion
    }
}
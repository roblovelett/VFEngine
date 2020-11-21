using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics
{
    public class PhysicsRuntimeData : ScriptableObject
    {
        public Physics physics;

        public struct Physics
        {
            public PhysicsController PhysicsController { get; set; }
            public bool StickToSlopesControl { get; set; }
            public bool SafetyBoxcastControl { get; set; }
            public bool Physics2DInteractionControl { get; set; }
            public bool IsJumping { get; set; }
            public bool IsFalling { get; set; }
            public bool GravityActive { get; set; }
            public bool SafeSetTransformControl { get; set; }
            public int HorizontalMovementDirection { get; set; }
            public float FallSlowFactor { get; set; }
            public float Physics2DPushForce { get; set; }
            public float MaximumSlopeAngle { get; set; }
            public float SmallValue { get; set; }
            public float Gravity { get; set; }
            public float MovementDirectionThreshold { get; set; }
            public float CurrentVerticalSpeedFactor { get; set; }
            public Vector2 Speed { get; set; }
            public Vector2 MaximumVelocity { get; set; }
            public Vector2 NewPosition { get; set; }
            public Vector2 ExternalForce { get; set; }
        }

        public void SetPhysicsController(PhysicsController controller)
        {
            physics.PhysicsController = controller;
        }

        public void SetPhysics(bool stickToSlopesControl, bool safetyBoxcastControl, bool physics2DInteractionControl,
            bool isJumping, bool isFalling, bool gravityActive, bool safeSetTransformControl,
            int horizontalMovementDirection, float fallSlowFactor, float physics2DPushForce, float maximumSlopeAngle,
            float smallValue, float gravity, float movementDirectionThreshold, float currentVerticalSpeedFactor,
            Vector2 speed, Vector2 maximumVelocity, Vector2 newPosition, Vector2 externalForce)
        {
            physics.StickToSlopesControl = stickToSlopesControl;
            physics.SafetyBoxcastControl = safetyBoxcastControl;
            physics.Physics2DInteractionControl = physics2DInteractionControl;
            physics.IsJumping = isJumping;
            physics.IsFalling = isFalling;
            physics.GravityActive = gravityActive;
            physics.SafeSetTransformControl = safeSetTransformControl;
            physics.HorizontalMovementDirection = horizontalMovementDirection;
            physics.FallSlowFactor = fallSlowFactor;
            physics.Physics2DPushForce = physics2DPushForce;
            physics.MaximumSlopeAngle = maximumSlopeAngle;
            physics.SmallValue = smallValue;
            physics.Gravity = gravity;
            physics.MovementDirectionThreshold = movementDirectionThreshold;
            physics.CurrentVerticalSpeedFactor = currentVerticalSpeedFactor;
            physics.Speed = speed;
            physics.MaximumVelocity = maximumVelocity;
            physics.NewPosition = newPosition;
            physics.ExternalForce = externalForce;
        }
    }
}
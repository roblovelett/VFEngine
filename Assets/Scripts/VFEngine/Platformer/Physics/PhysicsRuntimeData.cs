using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics
{
    public class PhysicsRuntimeData
    {
        #region properties

        public PhysicsController Controller { get; private set; }
        public Transform Transform { get; private set; }
        public bool StickToSlopesControl { get; private set; }
        public bool SafetyBoxcastControl { get; private set; }
        public bool Physics2DInteractionControl { get; private set; }
        public bool IsJumping { get; private set; }
        public bool IsFalling { get; private set; }
        public bool GravityActive { get; private set; }
        public bool SafeSetTransformControl { get; private set; }
        public int HorizontalMovementDirection { get; private set; }
        public float FallSlowFactor { get; private set; }
        public float Physics2DPushForce { get; private set; }
        public float MaximumSlopeAngle { get; private set; }
        public float SmallValue { get; private set; }
        public float Gravity { get; private set; }
        public float MovementDirectionThreshold { get; private set; }
        public float CurrentVerticalSpeedFactor { get; private set; }
        public Vector2 Speed { get; private set; }
        public Vector2 MaximumVelocity { get; private set; }
        public Vector2 NewPosition { get; private set; }
        public Vector2 ExternalForce { get; private set; }

        #region public methods

        public static PhysicsRuntimeData CreateInstance(PhysicsController controller, Transform transform,
            bool stickToSlopesControl, bool safetyBoxcastControl, bool physics2DInteractionControl, bool isJumping,
            bool isFalling, bool gravityActive, bool safeSetTransformControl, int horizontalMovementDirection,
            float fallSlowFactor, float physics2DPushForce, float maximumSlopeAngle, float smallValue, float gravity,
            float movementDirectionThreshold, float currentVerticalSpeedFactor, Vector2 speed, Vector2 maximumVelocity,
            Vector2 newPosition, Vector2 externalForce)
        {
            return new PhysicsRuntimeData
            {
                Controller = controller,
                Transform = transform,
                StickToSlopesControl = stickToSlopesControl,
                SafetyBoxcastControl = safetyBoxcastControl,
                Physics2DInteractionControl = physics2DInteractionControl,
                IsJumping = isJumping,
                IsFalling = isFalling,
                GravityActive = gravityActive,
                SafeSetTransformControl = safeSetTransformControl,
                HorizontalMovementDirection = horizontalMovementDirection,
                FallSlowFactor = fallSlowFactor,
                Physics2DPushForce = physics2DPushForce,
                MaximumSlopeAngle = maximumSlopeAngle,
                SmallValue = smallValue,
                Gravity = gravity,
                MovementDirectionThreshold = movementDirectionThreshold,
                CurrentVerticalSpeedFactor = currentVerticalSpeedFactor,
                Speed = speed,
                MaximumVelocity = maximumVelocity,
                NewPosition = newPosition,
                ExternalForce = externalForce
            };
        }

        #endregion

        #endregion
    }
}
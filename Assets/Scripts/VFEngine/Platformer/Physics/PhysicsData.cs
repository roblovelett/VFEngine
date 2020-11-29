using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics
{
    public class PhysicsData
    {
        #region properties

        #region dependencies

        public Transform Transform { get; set; }
        public float Physics2DPushForce { get; private set; }
        public bool Physics2DInteractionControl { get; private set; }
        public Vector2 MaximumVelocity { get; private set; }
        public AnimationCurve SlopeAngleSpeedFactor { get; private set; }
        public bool SafetyBoxcastControl { get; private set; }
        public float MaximumSlopeAngle { get; private set; }
        public bool StickToSlopesControl { get; private set; }
        public bool SafeSetTransformControl { get; set; }
        public bool DisplayWarningsControl { get; private set; }
        public bool AutomaticGravityControl { get; private set; }
        public float AscentMultiplier { get; private set; }
        public float FallMultiplier { get; private set; }
        public float Gravity { get; private set; }

        #endregion

        public float CurrentVerticalSpeedFactor { get; set; }
        public bool IsJumping { get; set; }
        public bool IsFalling { get; set; }
        public bool GravityActive { get; set; }
        public int HorizontalMovementDirection { get; set; }
        public float FallSlowFactor { get; set; }
        public float SmallValue { get; set; }
        public float MovementDirectionThreshold { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 NewPosition { get; set; }

        public float NewPositionY
        {
            set => value = NewPosition.y;
        }

        public float NewPositionX
        {
            set => value = NewPosition.x;
        }

        public Vector2 ExternalForce { get; set; }

        public float ExternalForceY
        {
            set => value = ExternalForce.y;
        }

        public Vector2 WorldSpeed { get; set; } = Vector2.zero;
        public Vector2 ForcesApplied { get; set; } = Vector2.zero;
        public Rigidbody2D CurrentHitRigidBody { get; set; }
        public bool CurrentHitRigidBodyCanBePushed { get; set; }
        public Vector2 CurrentPushDirection { get; set; } = Vector2.zero;
        public float CurrentGravity { get; set; }
        public int StoredHorizontalMovementDirection { get; set; }

        public float SpeedX
        {
            get => Speed.x;
            set => value = Speed.x;
        }

        public float SpeedY
        {
            get => Speed.y;
            set => value = Speed.y;
        }

        #region public methods

        public void ApplySettings(PhysicsSettings settings)
        {
            Physics2DPushForce = settings.physics2DPushForce;
            Physics2DInteractionControl = settings.physics2DInteractionControl;
            MaximumVelocity = settings.maximumVelocity;
            SlopeAngleSpeedFactor = settings.slopeAngleSpeedFactor;
            SafetyBoxcastControl = settings.safetyBoxcastControl;
            MaximumSlopeAngle = settings.maximumSlopeAngle;
            StickToSlopesControl = settings.stickToSlopeControl;
            SafeSetTransformControl = settings.safeSetTransformControl;
            DisplayWarningsControl = settings.displayWarningsControl;
            AutomaticGravityControl = settings.automaticGravityControl;
            AscentMultiplier = settings.ascentMultiplier;
            FallMultiplier = settings.fallMultiplier;
            Gravity = settings.gravity;
        }

        #endregion

        #endregion
    }
}
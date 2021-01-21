using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.ScriptableObjects
{
    using static ScriptableObjectExtensions;
    using static Quaternion;

    [CreateAssetMenu(fileName = "PhysicsData", menuName = PlatformerPhysicsDataPath, order = 0)]
    public class PhysicsData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public Vector2 Speed { get; private set; }
        public bool GravityActive => state.GravityActive;
        public float FallSlowFactor { get; private set; }
        public int MovementDirection { get; private set; }

        #endregion

        #region fields

        private bool displayWarnings;
        private float gravity;
        private float fallMultiplier;
        private float ascentMultiplier;
        private Vector2 maximumVelocity;
        private float speedAccelerationOnGround;
        private float speedAccelerationInAir;
        private float speedFactor;
        private float maximumSlopeAngle;
        private AnimationCurve slopeAngleSpeedFactor;
        private bool physics2DInteraction;
        private float physics2DPushForce;
        private bool safeSetTransform;
        private bool automaticGravityControl;
        private bool stickToSlopeBehavior;
        private Vector2 worldSpeed;
        private Vector2 appliedForces;
        private float currentGravity;
        private Vector2 externalForce;
        private Vector2 newPosition;
        private bool gravityActive;
        private int savedMovementDirection;
        private float movementDirectionThreshold;
        private State state;
        private Transform transform;

        private struct State
        {
            public bool IsFalling { get; set; }
            public bool IsJumping { get; set; }
            public bool GravityActive { get; set; }

            public void Reset()
            {
                IsFalling = true;
            }
        }

        #endregion

        #region initialization

        private void Initialize(PhysicsSettings settings, ref GameObject character)
        {
            ApplySettings(settings);
            InitializeDefault(ref character);
        }

        private void ApplySettings(PhysicsSettings settings)
        {
            displayWarnings = settings.displayWarnings;
            gravity = settings.gravity;
            fallMultiplier = settings.fallMultiplier;
            ascentMultiplier = settings.ascentMultiplier;
            maximumVelocity = settings.maximumVelocity;
            speedAccelerationOnGround = settings.speedAccelerationOnGround;
            speedAccelerationInAir = settings.speedAccelerationInAir;
            speedFactor = settings.speedFactor;
            maximumSlopeAngle = settings.maximumSlopeAngle;
            slopeAngleSpeedFactor = settings.slopeAngleSpeedFactor;
            physics2DInteraction = settings.physics2DInteraction;
            physics2DPushForce = settings.physics2DPushForce;
            safeSetTransform = settings.safeSetTransform;
            automaticGravityControl = settings.automaticGravityControl;
            stickToSlopeBehavior = settings.stickToSlopeBehavior;
            movementDirectionThreshold = settings.movementDirectionThreshold;
        }

        private void InitializeDefault(ref GameObject character)
        {
            if (automaticGravityControl) character.transform.rotation = identity;
            transform = character.transform;
            state.Reset();
            state.GravityActive = true;
            currentGravity = 0;
            savedMovementDirection = 1;
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void SetCurrentGravity()
        {
        }

        private void ApplyAscentMultiplierToCurrentGravity()
        {
        }

        private void ApplyFallMultiplierToCurrentGravity()
        {
        }

        private void ApplyGravityToSpeedY()
        {
        }

        private void ApplyFallSlowFactorToSpeedY()
        {
        }

        #endregion

        #region event handlers

        public void OnInitialize(PhysicsSettings settings, ref GameObject character)
        {
            Initialize(settings, ref character);
        }

        public void OnSetCurrentGravity()
        {
            SetCurrentGravity();
        }

        public void OnApplyAscentMultiplierToCurrentGravity()
        {
            ApplyAscentMultiplierToCurrentGravity();
        }

        public void OnApplyFallMultiplierToCurrentGravity()
        {
            ApplyFallMultiplierToCurrentGravity();
        }

        public void OnApplyGravityToSpeedY()
        {
            ApplyGravityToSpeedY();
        }

        public void OnApplyFallSlowFactorToSpeedY()
        {
            ApplyFallSlowFactorToSpeedY();
        }

        #endregion
    }
}

#region hide

/*private Vector2 speed;
        private Vector2 externalForce;
        private Vector2 deltaMove;public Vector2 DeltaMove { get; private set; }
        private float GroundFriction { get; set; }
        private float AirFriction { get; set; }
        private float Friction { get; set; }
        public Vector2 ExternalForce { get; private set; }
        public float MinimumMoveThreshold { get; private set; }
        private float Gravity { get; set; }
        private float GravityScale { get; set; }
        public Vector2 Speed { get; private set; }
        public float MaximumSlopeAngle { get; private set; }
        public float MinimumWallAngle { get; private set; }
        public bool FacingRight { get; private set; }
        public Vector2 TotalSpeed => Speed + ExternalForce;
        public int DeltaMoveXDirectionAxis => (int) Sign(DeltaMove.x);//AxisDirection(DeltaMove.x);
        public int DeltaMoveYDirectionAxis => (int) Sign(DeltaMove.y);//AxisDirection(DeltaMove.y);
        public float DeltaMoveDistanceX => Abs(DeltaMove.x);
        public float DeltaMoveDistanceY => Abs(DeltaMove.y);MaximumSlopeAngle = settings.maximumSlopeAngle;
        MinimumWallAngle = settings.minimumWallAngle;
        MinimumMoveThreshold = settings.minimumMovementThreshold;
        Gravity = settings.gravity;
        AirFriction = settings.airFriction;
        GroundFriction = settings.groundFriction;FacingRight = true;
        GravityScale = 1;
        Speed = zero;
        ExternalForce = zero;
        DeltaMove = zero;
 * private void InitializeDeltaMove()
        {
            DeltaMove = TotalSpeed * fixedDeltaTime;
        }
        private void UpdateExternalForce(bool onGround)
        {
            SetFriction(UpdatedFriction(onGround));
            SetExternalForce(UpdatedExternalForce());
        }

        private float UpdatedFriction(bool onGround)
        {
            return onGround ? GroundFriction : AirFriction;
        }

        private void SetFriction(float friction)
        {
            Friction = friction;
        }

        private Vector2 UpdatedExternalForce()
        {
            return MoveTowards(ExternalForce, zero, ExternalForce.magnitude * Friction * fixedDeltaTime);
        }

        private void SetExternalForce(Vector2 force)
        {
            ExternalForce = force;
        }

        private void StopExternalForce()
        {
            SetExternalForce(zero);
        }

        private float GravityScaledToTime => Gravity * GravityScale * fixedDeltaTime;
        
        private void ApplyGravityToSpeed()
        {
            ApplyToSpeedY(GravityScaledToTime);
        }

        private void ApplyToSpeedY(float force)
        {
            speed = Speed;
            speed.y += force;
            SetSpeed(speed);
        }

        private void SetSpeed(Vector2 force)
        {
            Speed = force;
        }

        private void ApplyExternalForceToGravity()
        {
            ApplyToExternalForceY(GravityScaledToTime);
        }
        private void ApplyToExternalForceY(float force)
        {
            externalForce = ExternalForce;
            externalForce.y += force;
            SetExternalForce(externalForce);
        }

        private void UpdateExternalForceX(int groundDirectionAxis)
        {
            ApplyToExternalForceX(AppliedForcesX(groundDirectionAxis));
        }

        private float AppliedForcesX(int groundDirectionAxis)
        {
            return -Gravity * GroundFriction * groundDirectionAxis * fixedDeltaTime / 4;
        }
        
        private void ApplyToExternalForceX(float force)
        {
            externalForce = ExternalForce;
            externalForce.x += force;
            SetExternalForce(externalForce);
        }

        private void DescendSlope(float groundAngle)
        {
            SlopeBehavior(false, groundAngle);
        }

        private void SlopeBehavior(bool climbing, float groundAngle)
        {
            //SetDeltaMove(SlopeDeltaMove(climbing, groundAngle));
            StopForcesY();
        }

        private Vector2 SlopeDeltaMove(bool climbing, float groundAngle)
        {
            return climbing
                ? new Vector2(SlopeDeltaMoveX(groundAngle), SlopeDeltaMoveY(groundAngle))
                : new Vector2(SlopeDeltaMoveX(groundAngle), -SlopeDeltaMoveY(groundAngle));
        }

        private float SlopeDeltaMoveX(float groundAngle)
        {
            return Cos(groundAngle * Deg2Rad) * DeltaMoveDistanceX * DeltaMoveXDirectionAxis;
        }

        private float SlopeDeltaMoveY(float groundAngle)
        {
            return Sin(groundAngle * Deg2Rad) * DeltaMoveDistanceX;
        }

        private void StopForcesY()
        {
            SetSpeedY(0);
            SetExternalForceY(0);
        }

        private void SetSpeedY(float y)
        {
            speed = Speed;
            speed.y = y;
            SetSpeed(speed);
        }

        private void SetExternalForceY(float y)
        {
            externalForce = ExternalForce;
            externalForce.y = y;
            SetExternalForce(externalForce);
        }

        private void ClimbSlope(float groundAngle)
        {
            SlopeBehavior(true, groundAngle);
        }

        private void HitClimbingSlope(float groundAngle, float hitDistance, float skinWidth)
        {
            var deltaMoveXHitClimbingSlope = DeltaMoveXHitClimbingSlope(hitDistance, skinWidth);
            ApplyToDeltaMoveX(-deltaMoveXHitClimbingSlope);
            ClimbSlope(groundAngle);
            ApplyToDeltaMoveX(deltaMoveXHitClimbingSlope);
        }

        private float DeltaMoveXHitClimbingSlope(float hitDistance, float skinWidth)
        {
            return (hitDistance - skinWidth) * DeltaMoveXDirectionAxis;
        }

        private void ApplyToDeltaMoveX(float x)
        {
            deltaMove = DeltaMove;
            deltaMove.x += x;
            //SetDeltaMove(deltaMove);
        }

        private void HitMaximumSlope(float hitDistance, float skinWidth)
        {
            SetDeltaMoveX(HitMaximumSlopeDeltaMoveX(hitDistance, skinWidth));
        }

        private float HitMaximumSlopeDeltaMoveX(float hitDistance, float skinWidth)
        {
            return Min(DeltaMoveDistanceX, hitDistance - skinWidth) * DeltaMoveXDirectionAxis;
        }

        private void SetDeltaMoveX(float x)
        {
            deltaMove = DeltaMove;
            deltaMove.x = x;
            //SetDeltaMove(deltaMove);
        }

        private bool NegativeDeltaMoveY => DeltaMove.y < 0;

        private void HitSlopedGroundAngle(float groundAngle)
        {
            if (NegativeDeltaMoveY) SetDeltaMoveY(0);
            else SetDeltaMoveY(HitSlopedGroundAngleDeltaMoveY(groundAngle));
        }

        private void SetDeltaMoveY(float y)
        {
            deltaMove = DeltaMove;
            deltaMove.y = y;
            //SetDeltaMove(DeltaMove);
        }

        private float HitSlopedGroundAngleDeltaMoveY(float groundAngle)
        {
            return Tan(groundAngle * Deg2Rad) * DeltaMoveDistanceX * DeltaMoveYDirectionAxis;
        }

        private void StopForcesX()
        {
            SetSpeedX(0);
            SetExternalForceX(0);
        }

        private void SetSpeedX(float x)
        {
            speed = Speed;
            speed.x = x;
            SetSpeed(speed);
        }

        private void SetExternalForceX(float x)
        {
            externalForce = ExternalForce;
            externalForce.x = x;
            SetExternalForce(externalForce);
        }

        private void VerticalCollision(float hitDistance, float skinWidth)
        {
            SetDeltaMoveY(VerticalCollisionDeltaMoveY(hitDistance, skinWidth));
        }

        private float VerticalCollisionDeltaMoveY(float hitDistance, float skinWidth)
        {
            return (hitDistance - skinWidth) * DeltaMoveYDirectionAxis;
        }

        private void VerticalCollisionHitClimbingSlope(float groundAngle)
        {
            SetDeltaMoveX(VerticalCollisionHitClimbingSlopeDeltaMoveX(groundAngle));
            StopForcesX();
        }

        private float VerticalCollisionHitClimbingSlopeDeltaMoveX(float groundAngle)
        {
            return DeltaMove.y / Tan(groundAngle * Deg2Rad) * DeltaMoveXDirectionAxis;
        }

        private float ClimbSteepSlopeDeltaMoveX(float hitDistance, float skinWidth)
        {
            return (hitDistance - skinWidth) * DeltaMoveXDirectionAxis;
        }

        private void ClimbSteepSlope(float hitDistance, float skinWidth)
        {
            SetDeltaMoveX(ClimbSteepSlopeDeltaMoveX(hitDistance, skinWidth));
        }

        private void ClimbMildSlope(float hitAngle, float groundAngle, float hitDistance, float skinWidth)
        {
            ApplyToDeltaMove(EdgeCaseDeltaMove(false, groundAngle, hitDistance, hitAngle, skinWidth));
        }

        private void ApplyToDeltaMove(Vector2 force)
        {
            deltaMove = DeltaMove;
            deltaMove += force;
            //SetDeltaMove(deltaMove);
        }

        private void DescendMildSlope(float hitDistance, float skinWidth)
        {
            SetDeltaMoveY(DescendMildSlopeDeltaMoveY(hitDistance, skinWidth));
        }

        private static float DescendMildSlopeDeltaMoveY(float hitDistance, float skinWidth)
        {
            return -(hitDistance - skinWidth);
        }

        private void DescendSteepSlope(float hitAngle, float groundAngle, float hitDistance, float skinWidth)
        {
            ApplyToDeltaMove(EdgeCaseDeltaMove(true, groundAngle, hitDistance, hitAngle, skinWidth));
        }

        private Vector2 EdgeCaseDeltaMove(bool descending, float groundAngle, float hitDistance, float hitAngle,
            float skinWidth)
        {
            float overshoot;
            var groundAngleSin = Sin(groundAngle * Deg2Rad);
            var hitAngleTan = Tan(hitAngle * Deg2Rad);
            if (descending)
                overshoot = groundAngle > 0
                    ? OvershootDescendingSteepSlope(groundAngle, hitAngleTan, hitDistance, groundAngleSin)
                    : OvershootDefault(hitDistance, hitAngleTan);
            else
                overshoot = hitAngle > 0
                    ? OvershootClimbingMildSlope(groundAngle, hitAngleTan, hitDistance, groundAngleSin)
                    : OvershootDefault(hitDistance, groundAngleSin);
            var removeX = RemoveX(groundAngle, overshoot);
            var removeY = RemoveY(groundAngleSin, overshoot);
            var addX = AddX(hitAngle, overshoot);
            var addY = AddY(hitAngle, overshoot);
            if (!descending) return new Vector2(addX - removeX, addY - removeY + skinWidth);
            removeY = -removeY;
            addY = -addY;
            skinWidth = -skinWidth;
            return new Vector2(addX - removeX, addY - removeY + skinWidth);
        }

        private static float OvershootDescendingSteepSlope(float angle, float angleTan, float distance, float angleSin)
        {
            var cos = Cos(angle * Deg2Rad);
            return distance * cos / (angleTan / cos - angleSin);
        }

        private static float OvershootClimbingMildSlope(float angle, float angleTan, float distance, float angleSin)
        {
            var tan = Tan(angle * Deg2Rad);
            return (2 * angleTan * distance - tan * distance) / (angleTan * angleSin - tan * angleSin);
        }

        private static float OvershootDefault(float distance, float angle)
        {
            return distance / angle;
        }

        private float RemoveX(float angle, float err)
        {
            return Cos(angle * Deg2Rad) * err * DeltaMoveXDirectionAxis;
        }

        private static float RemoveY(float angle, float err)
        {
            return angle * err;
        }

        private float AddX(float angle, float err)
        {
            return Cos(angle * Deg2Rad) * err * DeltaMoveXDirectionAxis;
        }

        private static float AddY(float angle, float err)
        {
            return Sin(angle * Deg2Rad) * err;
        }

        private void MoveCharacter(ref GameObject character)
        {
            character.transform.Translate(DeltaMove);
        }

        private void ResetJumpCollision()
        {
            StopForcesY();
        }
        
        public void OnInitializeDeltaMove()
        {
            InitializeDeltaMove();
        }

        public void OnUpdateExternalForce(bool onGround)
        {
            UpdateExternalForce(onGround);
        }

        public void OnStopExternalForce()
        {
            StopExternalForce();
        }

        public void OnApplyGravityToSpeed()
        {
            ApplyGravityToSpeed();
        }

        public void OnApplyExternalForceToGravity()
        {
            ApplyExternalForceToGravity();
        }

        public void OnUpdateExternalForceX(int groundDirectionAxis)
        {
            UpdateExternalForceX(groundDirectionAxis);
        }
        
        public void OnDescendSlope(float groundAngle)
        {
            DescendSlope(groundAngle);
        }

        public void OnClimbSlope(float groundAngle)
        {
            ClimbSlope(groundAngle);
        }

        public void OnHitClimbingSlope(float groundAngle, float hitDistance, float skinWidth)
        {
            HitClimbingSlope(groundAngle, hitDistance, skinWidth);
        }

        public void OnHitMaximumSlope(float hitDistance, float skinWidth)
        {
            HitMaximumSlope(hitDistance, skinWidth);
        }

        public void OnHitSlopedGroundAngle(float groundAngle)
        {
            HitSlopedGroundAngle(groundAngle);
        }

        public void OnHitMaximumSlope()
        {
            StopForcesX();
        }

        public void OnStopHorizontalSpeed()
        {
            SetSpeedX(0);
        }

        public void OnVerticalCollision(float hitDistance, float skinWidth)
        {
            VerticalCollision(hitDistance, skinWidth);
        }

        public void OnVerticalCollisionHitClimbingSlope(float groundAngle)
        {
            VerticalCollisionHitClimbingSlope(groundAngle);
        }

        public void OnClimbSteepSlope(float hitDistance, float skinWidth)
        {
            ClimbSteepSlope(hitDistance, skinWidth);
        }

        public void OnClimbMildSlope(float hitAngle, float groundAngle, float hitDistance, float skinWidth)
        {
            ClimbMildSlope(hitAngle, groundAngle, hitDistance, skinWidth);
        }

        public void OnDescendMildSlope(float hitDistance, float skinWidth)
        {
            DescendMildSlope(hitDistance, skinWidth);
        }

        public void OnDescendSteepSlope(float hitAngle, float groundAngle, float hitDistance, float skinWidth)
        {
            DescendSteepSlope(hitAngle, groundAngle, hitDistance, skinWidth);
        }

        public void OnMoveCharacter(ref GameObject character)
        {
            MoveCharacter(ref character);
        }

        public void OnResetJumpCollision()
        {
            ResetJumpCollision();
        }
 */

#endregion
using System.Collections.Generic;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.ScriptableObjects
{
    using static ScriptableObjectExtensions;
    using static Quaternion;
    using static Time;
    using static Vector2;
    using static Space;
    using static Mathf;
    using static RigidbodyType2D;

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
        public Vector2 NewPosition { get; private set; }
        public bool Physics2DInteraction { get; private set; }
        public float MovementDirectionThreshold { get; private set; }
        public Vector2 ExternalForce { get; private set; }
        public float MaximumSlopeAngle { get; private set; }
        public float Gravity { get; private set; }
        public bool IsFalling => state.IsFalling;
        public bool StickToSlopeBehavior { get; private set; }

        #endregion

        #region fields

        private bool displayWarnings;
        private float fallMultiplier;
        private float ascentMultiplier;
        private Vector2 maximumVelocity;
        private float speedAccelerationOnGround;
        private float speedAccelerationInAir;
        private float speedFactor;
        private AnimationCurve slopeAngleSpeedFactor;
        private float physics2DPushForce;
        private bool safeSetTransform;
        private bool automaticGravityControl;
        private Vector2 worldSpeed;
        private Vector2 forcesApplied;
        private Vector2 newPositionInternal;
        private float currentGravity;
        private bool gravityActive;
        private int savedMovementDirection;
        private State state;
        private Transform transform;
        private Vector2 speedInternal;

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
            Gravity = settings.gravity;
            fallMultiplier = settings.fallMultiplier;
            ascentMultiplier = settings.ascentMultiplier;
            maximumVelocity = settings.maximumVelocity;
            speedAccelerationOnGround = settings.speedAccelerationOnGround;
            speedAccelerationInAir = settings.speedAccelerationInAir;
            speedFactor = settings.speedFactor;
            MaximumSlopeAngle = settings.maximumSlopeAngle;
            slopeAngleSpeedFactor = settings.slopeAngleSpeedFactor;
            Physics2DInteraction = settings.physics2DInteraction;
            physics2DPushForce = settings.physics2DPushForce;
            safeSetTransform = settings.safeSetTransform;
            automaticGravityControl = settings.automaticGravityControl;
            StickToSlopeBehavior = settings.stickToSlopeBehavior;
            MovementDirectionThreshold = settings.movementDirectionThreshold;
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

        private float CurrentGravityAscentMultiplierApplied => currentGravity / ascentMultiplier;

        private void SetCurrentGravity()
        {
            ApplyAscentMultiplierToCurrentGravity();
        }

        private void SetCurrentGravity(float force)
        {
            currentGravity = force;
        }

        private void ApplyAscentMultiplierToCurrentGravity()
        {
            SetCurrentGravity(CurrentGravityAscentMultiplierApplied);
        }

        private float CurrentGravityFallMultiplierApplied => currentGravity * fallMultiplier;

        private void ApplyFallMultiplierToCurrentGravity()
        {
            SetCurrentGravity(CurrentGravityFallMultiplierApplied);
        }

        private float SpeedYGravityApplied(float movingPlatformCurrentGravity)
        {
            return (currentGravity + movingPlatformCurrentGravity) * deltaTime;
        }

        private void ApplyGravityToSpeedY(float movingPlatformCurrentGravity)
        {
            AddToSpeedY(SpeedYGravityApplied(movingPlatformCurrentGravity));
        }

        private void AddToSpeedY(float y)
        {
            speedInternal = Speed;
            speedInternal.y += y;
            SetSpeed(speedInternal);
        }

        private void SetSpeed(Vector2 force)
        {
            Speed = force;
        }

        private void ApplyFallSlowFactorToSpeedY()
        {
            ApplyToSpeedY(FallSlowFactor);
        }

        private void ApplyToSpeedY(float y)
        {
            speedInternal = Speed;
            speedInternal.y *= y;
            SetSpeed(speedInternal);
        }

        private Vector2 NewPositionInitialized => Speed * deltaTime;

        private void InitializeFrame()
        {
            SetNewPosition(NewPositionInitialized);
            state.Reset();
        }

        private void SetNewPosition(Vector2 position)
        {
            NewPosition = position;
        }

        private void ApplyForces()
        {
            SetForcesApplied(Speed);
        }

        private void SetForcesApplied(Vector2 forces)
        {
            forcesApplied = forces;
        }

        private void StopNewPosition()
        {
            SetNewPosition(zero);
        }

        private void MoveCharacter(ref GameObject character)
        {
            character.transform.Translate(NewPosition, Self);
        }

        private Vector2 NewSpeed => NewPosition / deltaTime;

        private void SetNewSpeed()
        {
            SetSpeed(NewSpeed);
        }

        private void ApplySlopeSpeedFactor(float belowSlopeAngle)
        {
            ApplyToSpeedX(SpeedXSlopeAngleSpeedFactorApplied(belowSlopeAngle));
        }

        private float SpeedXSlopeAngleSpeedFactorApplied(float belowSlopeAngle)
        {
            return slopeAngleSpeedFactor.Evaluate(Abs(belowSlopeAngle) * Sign(Speed.y));
        }

        private void ApplyToSpeedX(float x)
        {
            speedInternal = Speed;
            speedInternal.x *= x;
            SetSpeed(speedInternal);
        }

        private Vector2 SpeedClampedToMaximumVelocity => new Vector2(SpeedAxisClamped(Speed.x, maximumVelocity.x),
            SpeedAxisClamped(Speed.y, maximumVelocity.y));

        private void ClampSpeedToMaximumVelocity()
        {
            SetSpeed(SpeedClampedToMaximumVelocity);
        }

        private float SpeedAxisClamped(float speedAxis, float maximumVelocityAxis)
        {
            return Clamp(speedAxis, -maximumVelocityAxis, maximumVelocityAxis);
        }

        private Vector2 PushRigidBodyDirection => new Vector2(ExternalForce.x, 0);

        private void Physics2DInteractionInternal(IEnumerable<RaycastHit2D> contactList)
        {
            foreach (var contact in contactList)
            {
                var rigidBody = contact.collider.attachedRigidbody;
                var cannotPushRigidBody = rigidBody == null || rigidBody.isKinematic || rigidBody.bodyType == Static;
                if (cannotPushRigidBody) return;
                rigidBody.velocity = PushRigidBodyDirection.normalized * physics2DPushForce;
            }
        }

        private void StopExternalForce()
        {
            SetExternalForce(zero);
        }

        private void SetExternalForce(Vector2 force)
        {
            ExternalForce = force;
        }

        private void UpdateWorldSpeed()
        {
            SetWorldSpeed(Speed);
        }

        private void SetWorldSpeed(Vector2 speed)
        {
            worldSpeed = speed;
        }

        private void TranslateMovingPlatformSpeedToTransform(ref GameObject character,
            Vector2 movingPlatformCurrentSpeed)
        {
            character.transform.Translate(movingPlatformCurrentSpeed * deltaTime);
        }

        private Vector2 SpeedOnMovingPlatform => -NewPosition / deltaTime;
        private float SpeedOnMovingPlatformX => -Speed.x;

        private void ApplyMovingPlatformBehavior(float movingPlatformCurrentSpeedY)
        {
            SetGravityActive(true);
            SetNewPositionY(NewPositionOnMovingPlatformY(movingPlatformCurrentSpeedY));
            SetSpeedOnMovingPlatform();
        }

        private void SetSpeedOnMovingPlatform()
        {
            SetSpeed(SpeedOnMovingPlatform);
            SetSpeedX(SpeedOnMovingPlatformX);
        }

        private void SetGravityActive(bool active)
        {
            state.GravityActive = active;
        }

        private float NewPositionOnMovingPlatformY(float movingPlatformCurrentSpeedY)
        {
            return movingPlatformCurrentSpeedY * deltaTime;
        }

        private void SetSpeedX(float x)
        {
            speedInternal = Speed;
            speedInternal.x = x;
            SetSpeed(speedInternal);
        }

        private void SetNewPositionY(float y)
        {
            newPositionInternal = NewPosition;
            newPositionInternal.y = y;
            SetNewPosition(newPositionInternal);
        }

        private void SetMovementDirectionToSaved()
        {
            SetMovementDirection(savedMovementDirection);
        }

        private void SetMovementDirection(int direction)
        {
            MovementDirection = direction;
        }

        private void SetNegativeMovementDirection()
        {
            SetMovementDirection(-1);
        }

        private void SetPositiveMovementDirection()
        {
            SetMovementDirection(1);
        }

        private void ApplyMovingPlatformCurrentSpeedToMovementDirection(float movingPlatformCurrentSpeedX)
        {
            SetMovementDirection(MovementDirectionOnMovingPlatform(movingPlatformCurrentSpeedX));
        }

        private int MovementDirectionOnMovingPlatform(float movingPlatformCurrentSpeedX)
        {
            return (int) Sign(movingPlatformCurrentSpeedX);
        }

        private void SetSavedMovementDirection()
        {
            SetSavedMovementDirection(MovementDirection);
        }

        private void SetSavedMovementDirection(int direction)
        {
            savedMovementDirection = direction;
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

        public void OnApplyGravityToSpeedY(float movingPlatformCurrentGravity)
        {
            ApplyGravityToSpeedY(movingPlatformCurrentGravity);
        }

        public void OnApplyFallSlowFactorToSpeedY()
        {
            ApplyFallSlowFactorToSpeedY();
        }

        public void OnInitializeFrame()
        {
            InitializeFrame();
        }

        public void OnApplyForces()
        {
            ApplyForces();
        }

        public void OnStopNewPosition()
        {
            StopNewPosition();
        }

        public void OnMoveCharacter(ref GameObject character)
        {
            MoveCharacter(ref character);
        }

        public void OnSetNewSpeed()
        {
            SetNewSpeed();
        }

        public void OnApplySlopeSpeedFactor(float belowSlopeAngle)
        {
            ApplySlopeSpeedFactor(belowSlopeAngle);
        }

        public void OnClampSpeedToMaximumVelocity()
        {
            ClampSpeedToMaximumVelocity();
        }

        public void OnPhysics2DInteraction(IEnumerable<RaycastHit2D> contactList)
        {
            Physics2DInteractionInternal(contactList);
        }

        public void OnStopExternalForce()
        {
            StopExternalForce();
        }

        public void OnUpdateWorldSpeed()
        {
            UpdateWorldSpeed();
        }

        public void OnTranslateMovingPlatformSpeedToTransform(ref GameObject character,
            Vector2 movingPlatformCurrentSpeed)
        {
            TranslateMovingPlatformSpeedToTransform(ref character, movingPlatformCurrentSpeed);
        }

        public void OnApplyMovingPlatformBehavior(float movingPlatformCurrentSpeedY)
        {
            ApplyMovingPlatformBehavior(movingPlatformCurrentSpeedY);
        }

        public void OnSetMovementDirectionToSaved()
        {
            SetMovementDirectionToSaved();
        }

        public void OnSetNegativeMovementDirection()
        {
            SetNegativeMovementDirection();
        }

        public void OnSetPositiveMovementDirection()
        {
            SetPositiveMovementDirection();
        }

        public void OnApplyMovingPlatformCurrentSpeedToMovementDirection(float movingPlatformCurrentSpeedX)
        {
            ApplyMovingPlatformCurrentSpeedToMovementDirection(movingPlatformCurrentSpeedX);
        }

        public void OnSetSavedMovementDirection()
        {
            SetSavedMovementDirection();
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
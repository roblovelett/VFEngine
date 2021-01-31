using System.Collections.Generic;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
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
    using static RaycastData;
    using static RaycastData.Direction;

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
        public bool IsJumping => state.IsJumping;

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
        private Vector2 externalForceInternal;

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

        private static void TranslateMovingPlatformSpeedToTransform(ref GameObject character,
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

        private void SetPhysicsOnHitWallInMovementDirection(Direction raycastDirection, float distance,
            float boundsWidth, float rayOffset)
        {
            if (raycastDirection == Left) SetPhysicsOnHitWallInMovementDirectionLeft(distance, boundsWidth, rayOffset);
            else SetPhysicsOnHitWallInMovementDirectionRight(distance, boundsWidth, rayOffset);
        }

        private void SetPhysicsOnHitWallInMovementDirectionLeft(float distance, float boundsWidth, float rayOffset)
        {
            SetNewPositionX(NewPositionXOnHitWallInMovementDirectionLeft(distance, boundsWidth, rayOffset));
        }

        private static float NewPositionXOnHitWallInMovementDirectionLeft(float distance, float boundsWidth,
            float rayOffset)
        {
            return -distance + boundsWidth / 2 + rayOffset * 2;
        }

        private void SetPhysicsOnHitWallInMovementDirectionRight(float distance, float boundsWidth, float rayOffset)
        {
            SetNewPositionX(NewPositionXOnHitWallInMovementDirectionRight(distance, boundsWidth, rayOffset));
        }

        private static float NewPositionXOnHitWallInMovementDirectionRight(float distance, float boundsWidth,
            float rayOffset)
        {
            return distance - boundsWidth / 2 - rayOffset * 2;
        }

        private void SetNewPositionX(float x)
        {
            newPositionInternal = NewPosition;
            newPositionInternal.x = x;
            SetNewPosition(newPositionInternal);
        }

        private void StopNewPositionX()
        {
            SetNewPositionX(0);
        }

        private void StopSpeedX()
        {
            SetNewSpeedX(0);
        }

        private void SetNewSpeedX(float x)
        {
            speedInternal = Speed;
            speedInternal.x = x;
            SetSpeed(speedInternal);
        }

        private void SetIsFalling(bool falling = true)
        {
            state.IsFalling = falling;
        }

        private void SetIsNotFalling()
        {
            SetIsFalling(false);
        }

        private float NewPositionYOnExternalForceApplied => Speed.y * deltaTime;

        private void ApplySpeedToNewPositionY()
        {
            SetNewPositionY(NewPositionYOnExternalForceApplied);
        }

        private void SetNewPositionYOnSmallestDistanceHit(float distance, float boundsHeight, float rayOffset)
        {
            SetNewPositionY(NewPositionYOnSmallestDistanceHit(distance, boundsHeight, rayOffset));
        }

        private static float NewPositionYOnSmallestDistanceHit(float distance, float boundsHeight, float rayOffset)
        {
            return -distance + boundsHeight / 2 + rayOffset;
        }

        private void ApplySpeedYToNewPositionY()
        {
            ApplyToNewPositionY(NewPositionYSpeedYApplied);
        }

        private float NewPositionYSpeedYApplied => Speed.y * deltaTime;

        private void ApplyToNewPositionY(float y)
        {
            newPositionInternal = NewPosition;
            newPositionInternal.y += y;
            SetNewPosition(newPositionInternal);
        }

        private void StopNewPositionY()
        {
            SetNewPositionY(0);
        }

        private void SetPhysicsOnDetachFromMovingPlatform()
        {
            SetGravityActive(true);
        }

        private void SetNewPositionYOnStickToSlopeRaycastHit(float stickToSlopeRaycastPointY,
            float stickToSlopeRaycastOriginY, float boundsHeight)
        {
            SetNewPositionY(NewPositionYOnStickToSlopeRaycastHit(stickToSlopeRaycastPointY, stickToSlopeRaycastOriginY,
                boundsHeight));
        }

        private static float NewPositionYOnStickToSlopeRaycastHit(float stickToSlopeRaycastPointY,
            float stickToSlopeRaycastOriginY, float boundsHeight)
        {
            return -Abs(stickToSlopeRaycastPointY - stickToSlopeRaycastOriginY) + boundsHeight / 2;
        }

        private void SetNewPositionYOnAboveRaycastSmallestDistanceHit(float smallestDistance, float boundsHeight)
        {
            SetNewPositionY(NewPositionYOnAboveRaycastSmallestDistanceHit(smallestDistance, boundsHeight));
        }

        private static float NewPositionYOnAboveRaycastSmallestDistanceHit(float smallestDistance, float boundsHeight)
        {
            return smallestDistance - boundsHeight / 2;
        }

        private Vector2 SpeedOnAboveRaycastSmallestDistanceHit => new Vector2(Speed.x, 0);

        private void SetSpeedOnAboveRaycastSmallestDistanceHit()
        {
            SetSpeed(SpeedOnAboveRaycastSmallestDistanceHit);
        }

        private void StopForcesY()
        {
            SetSpeedY(0);
            SetExternalForceY(0);
        }

        private void SetSpeedY(float y)
        {
            speedInternal = Speed;
            speedInternal.y = y;
            SetSpeed(speedInternal);
        }

        private void SetExternalForceY(float y)
        {
            externalForceInternal = ExternalForce;
            externalForceInternal.y = y;
            SetExternalForce(externalForceInternal);
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

        public void OnSetPhysicsOnHitWallInMovementDirection(Direction rayDirection, float distance, float boundsWidth,
            float rayOffset)
        {
            SetPhysicsOnHitWallInMovementDirection(rayDirection, distance, boundsWidth, rayOffset);
        }

        public void OnStopNewPositionX()
        {
            StopNewPositionX();
        }

        public void OnStopSpeedX()
        {
            StopSpeedX();
        }

        public void OnSetIsFalling()
        {
            SetIsFalling();
        }

        public void OnSetIsNotFalling()
        {
            SetIsNotFalling();
        }

        public void OnApplySpeedToNewPositionY()
        {
            ApplySpeedToNewPositionY();
        }

        public void OnSetNewPositionYOnSmallestDistanceHit(float distance, float boundsHeight, float rayOffset)
        {
            SetNewPositionYOnSmallestDistanceHit(distance, boundsHeight, rayOffset);
        }

        public void OnApplySpeedYToNewPositionY()
        {
            ApplySpeedYToNewPositionY();
        }

        public void OnStopNewPositionY()
        {
            StopNewPositionY();
        }

        public void OnSetPhysicsOnDetachFromMovingPlatform()
        {
            SetPhysicsOnDetachFromMovingPlatform();
        }

        public void OnSetNewPositionYOnStickToSlopeRaycastHit(float stickToSlopeRaycastPointY,
            float stickToSlopeRaycastOriginY, float boundsHeight)
        {
            SetNewPositionYOnStickToSlopeRaycastHit(stickToSlopeRaycastPointY, stickToSlopeRaycastOriginY,
                boundsHeight);
        }

        public void OnSetNewPositionYOnAboveRaycastSmallestDistanceHit(float smallestDistance, float boundsHeight)
        {
            SetNewPositionYOnAboveRaycastSmallestDistanceHit(smallestDistance, boundsHeight);
        }

        public void OnSetSpeedOnAboveRaycastSmallestDistanceHit()
        {
            SetSpeedOnAboveRaycastSmallestDistanceHit();
        }

        public void OnStopForcesY()
        {
            StopForcesY();
        }

        #endregion
    }
}
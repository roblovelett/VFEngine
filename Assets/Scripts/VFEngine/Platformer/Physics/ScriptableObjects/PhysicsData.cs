using System.Collections.Generic;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable NotAccessedField.Local
namespace VFEngine.Platformer.Physics.ScriptableObjects
{
    using static ScriptableObjectExtensions.Platformer;
    using static Quaternion;
    using static Time;
    using static Vector2;
    using static Space;
    using static Mathf;
    using static RigidbodyType2D;
    using static RaycastData;
    using static RaycastData.Direction;

    [CreateAssetMenu(fileName = "PhysicsData", menuName = PhysicsDataPath, order = 0)]
    public class PhysicsData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public bool GravityActive => state.GravityActive;
        public bool IsFalling => state.IsFalling;
        public bool IsJumping => state.IsJumping;
        public bool Physics2DInteraction { get; private set; }
        public bool StickToSlopeBehavior { get; private set; }
        public int MovementDirection { get; private set; }
        public float MovementDirectionThreshold { get; private set; }
        public float FallSlowFactor { get; private set; }
        public float MaximumSlopeAngle { get; private set; }
        public float Gravity { get; private set; }
        public Vector2 Speed { get; private set; }
        public Vector2 NewPosition { get; private set; }
        public Vector2 ExternalForce { get; private set; }

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

        public PhysicsData(bool gravityActive)
        {
            this.gravityActive = gravityActive;
        }

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

        private void CurrentGravityInternal()
        {
            ApplyAscentMultiplierToCurrentGravity();
        }

        private void CurrentGravityInternal(float force)
        {
            currentGravity = force;
        }

        private void ApplyAscentMultiplierToCurrentGravity()
        {
            CurrentGravityInternal(CurrentGravityAscentMultiplierApplied);
        }

        private float CurrentGravityFallMultiplierApplied => currentGravity * fallMultiplier;

        private void ApplyFallMultiplierToCurrentGravity()
        {
            CurrentGravityInternal(CurrentGravityFallMultiplierApplied);
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
            SpeedInternal(speedInternal);
        }

        private void SpeedInternal(Vector2 force)
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
            SpeedInternal(speedInternal);
        }

        private Vector2 NewPositionInitialized => Speed * deltaTime;

        private void InitializeFrame()
        {
            NewPositionInternal(NewPositionInitialized);
            state.Reset();
        }

        private void NewPositionInternal(Vector2 position)
        {
            NewPosition = position;
        }

        private void ApplyForces()
        {
            ForcesApplied(Speed);
        }

        private void ForcesApplied(Vector2 forces)
        {
            forcesApplied = forces;
        }

        private void StopNewPosition()
        {
            NewPositionInternal(zero);
        }

        private void MoveCharacter(ref GameObject character)
        {
            character.transform.Translate(NewPosition, Self);
        }

        private Vector2 NewSpeed => NewPosition / deltaTime;

        private void NewSpeedInternal()
        {
            SpeedInternal(NewSpeed);
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
            SpeedInternal(speedInternal);
        }

        private Vector2 SpeedClampedToMaximumVelocity => new Vector2(SpeedAxisClamped(Speed.x, maximumVelocity.x),
            SpeedAxisClamped(Speed.y, maximumVelocity.y));

        private void ClampSpeedToMaximumVelocity()
        {
            SpeedInternal(SpeedClampedToMaximumVelocity);
        }

        private static float SpeedAxisClamped(float speedAxis, float maximumVelocityAxis)
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
            ExternalForceInternal(zero);
        }

        private void ExternalForceInternal(Vector2 force)
        {
            ExternalForce = force;
        }

        private void UpdateWorldSpeed()
        {
            WorldSpeed(Speed);
        }

        private void WorldSpeed(Vector2 speed)
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
            GravityActiveInternal(true);
            NewPositionY(NewPositionOnMovingPlatformY(movingPlatformCurrentSpeedY));
            SetSpeedOnMovingPlatform();
        }

        private void SetSpeedOnMovingPlatform()
        {
            SpeedInternal(SpeedOnMovingPlatform);
            SpeedX(SpeedOnMovingPlatformX);
        }

        private void GravityActiveInternal(bool active)
        {
            state.GravityActive = active;
        }

        private static float NewPositionOnMovingPlatformY(float movingPlatformCurrentSpeedY)
        {
            return movingPlatformCurrentSpeedY * deltaTime;
        }

        private void SpeedX(float x)
        {
            speedInternal = Speed;
            speedInternal.x = x;
            SpeedInternal(speedInternal);
        }

        private void NewPositionY(float y)
        {
            newPositionInternal = NewPosition;
            newPositionInternal.y = y;
            NewPositionInternal(newPositionInternal);
        }

        private void MovementDirectionToSaved()
        {
            MovementDirectionInternal(savedMovementDirection);
        }

        private void MovementDirectionInternal(int direction)
        {
            MovementDirection = direction;
        }

        private void NegativeMovementDirection()
        {
            MovementDirectionInternal(-1);
        }

        private void PositiveMovementDirection()
        {
            MovementDirectionInternal(1);
        }

        private void ApplyMovingPlatformCurrentSpeedToMovementDirection(float movingPlatformCurrentSpeedX)
        {
            MovementDirectionInternal(MovementDirectionOnMovingPlatform(movingPlatformCurrentSpeedX));
        }

        private static int MovementDirectionOnMovingPlatform(float movingPlatformCurrentSpeedX)
        {
            return (int) Sign(movingPlatformCurrentSpeedX);
        }

        private void SavedMovementDirection()
        {
            SavedMovementDirection(MovementDirection);
        }

        private void SavedMovementDirection(int direction)
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
            NewPositionX(NewPositionXOnHitWallInMovementDirectionLeft(distance, boundsWidth, rayOffset));
        }

        private static float NewPositionXOnHitWallInMovementDirectionLeft(float distance, float boundsWidth,
            float rayOffset)
        {
            return -distance + boundsWidth / 2 + rayOffset * 2;
        }

        private void SetPhysicsOnHitWallInMovementDirectionRight(float distance, float boundsWidth, float rayOffset)
        {
            NewPositionX(NewPositionXOnHitWallInMovementDirectionRight(distance, boundsWidth, rayOffset));
        }

        private static float NewPositionXOnHitWallInMovementDirectionRight(float distance, float boundsWidth,
            float rayOffset)
        {
            return distance - boundsWidth / 2 - rayOffset * 2;
        }

        private void NewPositionX(float x)
        {
            newPositionInternal = NewPosition;
            newPositionInternal.x = x;
            NewPositionInternal(newPositionInternal);
        }

        private void StopNewPositionX()
        {
            NewPositionX(0);
        }

        private void StopSpeedX()
        {
            NewSpeedX(0);
        }

        private void NewSpeedX(float x)
        {
            speedInternal = Speed;
            speedInternal.x = x;
            SpeedInternal(speedInternal);
        }

        private void IsFallingInternal(bool falling = true)
        {
            state.IsFalling = falling;
        }

        private void IsNotFalling()
        {
            IsFallingInternal(false);
        }

        private float NewPositionYOnExternalForceApplied => Speed.y * deltaTime;

        private void ApplySpeedToNewPositionY()
        {
            NewPositionY(NewPositionYOnExternalForceApplied);
        }

        private void SetNewPositionYOnSmallestDistanceHit(float distance, float boundsHeight, float rayOffset)
        {
            NewPositionY(NewPositionYOnSmallestDistanceHit(distance, boundsHeight, rayOffset));
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
            NewPositionInternal(newPositionInternal);
        }

        private void StopNewPositionY()
        {
            NewPositionY(0);
        }

        private void SetPhysicsOnDetachFromMovingPlatform()
        {
            GravityActiveInternal(true);
        }

        private void SetNewPositionYOnStickToSlopeRaycastHit(float stickToSlopeRaycastPointY,
            float stickToSlopeRaycastOriginY, float boundsHeight)
        {
            NewPositionY(NewPositionYOnStickToSlopeRaycastHit(stickToSlopeRaycastPointY, stickToSlopeRaycastOriginY,
                boundsHeight));
        }

        private static float NewPositionYOnStickToSlopeRaycastHit(float stickToSlopeRaycastPointY,
            float stickToSlopeRaycastOriginY, float boundsHeight)
        {
            return -Abs(stickToSlopeRaycastPointY - stickToSlopeRaycastOriginY) + boundsHeight / 2;
        }

        private void SetNewPositionYOnAboveRaycastSmallestDistanceHit(float smallestDistance, float boundsHeight)
        {
            NewPositionY(NewPositionYOnAboveRaycastSmallestDistanceHit(smallestDistance, boundsHeight));
        }

        private static float NewPositionYOnAboveRaycastSmallestDistanceHit(float smallestDistance, float boundsHeight)
        {
            return smallestDistance - boundsHeight / 2;
        }

        private Vector2 SpeedOnAboveRaycastSmallestDistanceHit => new Vector2(Speed.x, 0);

        private void SetSpeedOnAboveRaycastSmallestDistanceHit()
        {
            SpeedInternal(SpeedOnAboveRaycastSmallestDistanceHit);
        }

        private void StopForcesY()
        {
            SpeedY(0);
            ExternalForceY(0);
        }

        private void SpeedY(float y)
        {
            speedInternal = Speed;
            speedInternal.y = y;
            SpeedInternal(speedInternal);
        }

        private void ExternalForceY(float y)
        {
            externalForceInternal = ExternalForce;
            externalForceInternal.y = y;
            ExternalForceInternal(externalForceInternal);
        }

        #endregion

        #region event handlers

        public void OnInitialize(PhysicsSettings settings, ref GameObject character)
        {
            Initialize(settings, ref character);
        }

        public void OnSetCurrentGravity()
        {
            CurrentGravityInternal();
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
            NewSpeedInternal();
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

        public static void OnTranslateMovingPlatformSpeedToTransform(ref GameObject character,
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
            MovementDirectionToSaved();
        }

        public void OnSetNegativeMovementDirection()
        {
            NegativeMovementDirection();
        }

        public void OnSetPositiveMovementDirection()
        {
            PositiveMovementDirection();
        }

        public void OnApplyMovingPlatformCurrentSpeedToMovementDirection(float movingPlatformCurrentSpeedX)
        {
            ApplyMovingPlatformCurrentSpeedToMovementDirection(movingPlatformCurrentSpeedX);
        }

        public void OnSetSavedMovementDirection()
        {
            SavedMovementDirection();
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
            IsFallingInternal();
        }

        public void OnSetIsNotFalling()
        {
            IsNotFalling();
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
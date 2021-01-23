using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Physics.ScriptableObjects;

namespace VFEngine.Platformer.Physics
{
    using static ScriptableObject;
    using static GameObject;
    using static UniTask;

    public class PhysicsController : SerializedMonoBehaviour
    {
        #region events

        #endregion

        #region properties

        [OdinSerialize] public PhysicsData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private GameObject character;
        [OdinSerialize] private PhysicsSettings settings;
        private RaycastData raycastData;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            if (!Data) Data = CreateInstance<PhysicsData>();
            Data.OnInitialize(settings, ref character);
        }

        private void SetDependencies()
        {
            raycastData = GetComponent<RaycastController>().Data;
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            SetDependencies();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private async UniTask SetCurrentGravity()
        {
            Data.OnSetCurrentGravity();
            await Yield();
        }

        private async UniTask ApplyAscentMultiplierToCurrentGravity()
        {
            Data.OnApplyAscentMultiplierToCurrentGravity();
            await Yield();
        }

        private async UniTask ApplyFallMultiplierToCurrentGravity()
        {
            Data.OnApplyFallMultiplierToCurrentGravity();
            await Yield();
        }

        private float MovingPlatformCurrentGravity => raycastData.MovingPlatformCurrentGravity;

        private async UniTask ApplyGravityToSpeedY()
        {
            Data.OnApplyGravityToSpeedY(MovingPlatformCurrentGravity);
            await Yield();
        }

        private async UniTask ApplyFallSlowFactorToSpeedY()
        {
            Data.OnApplyFallSlowFactorToSpeedY();
            await Yield();
        }

        private async UniTask InitializeFrame()
        {
            Data.OnInitializeFrame();
            await Yield();
        }

        private async UniTask ApplyForces()
        {
            Data.OnApplyForces();
            await Yield();
        }

        private async UniTask StopNewPosition()
        {
            Data.OnStopNewPosition();
            await Yield();
        }

        private async UniTask MoveCharacter()
        {
            Data.OnMoveCharacter(ref character);
            await Yield();
        }

        private async UniTask SetNewSpeed()
        {
            Data.OnSetNewSpeed();
            await Yield();
        }

        private float BelowSlopeAngle => raycastData.BelowSlopeAngle;

        private async UniTask ApplySlopeSpeedFactor()
        {
            Data.OnApplySlopeSpeedFactor(BelowSlopeAngle);
            await Yield();
        }

        private async UniTask ClampSpeedToMaximumVelocity()
        {
            Data.OnClampSpeedToMaximumVelocity();
            await Yield();
        }

        private IEnumerable<RaycastHit2D> ContactList => raycastData.ContactList;

        private async UniTask Physics2DInteraction()
        {
            Data.OnPhysics2DInteraction(ContactList);
            await Yield();
        }

        private async UniTask StopExternalForce()
        {
            Data.OnStopExternalForce();
            await Yield();
        }

        private async UniTask UpdateWorldSpeed()
        {
            Data.OnUpdateWorldSpeed();
            await Yield();
        }

        private Vector2 MovingPlatformCurrentSpeed => raycastData.MovingPlatformCurrentSpeed;

        private async UniTask TranslateMovingPlatformSpeedToTransform()
        {
            Data.OnTranslateMovingPlatformSpeedToTransform(ref character, MovingPlatformCurrentSpeed);
            await Yield();
        }

        private async UniTask ApplyMovingPlatformBehavior()
        {
            Data.OnApplyMovingPlatformBehavior(MovingPlatformCurrentSpeed.y);
            await Yield();
        }

        private async UniTask SetMovementDirectionToSaved()
        {
            Data.OnSetMovementDirectionToSaved();
            await Yield();
        }

        private async UniTask SetNegativeMovementDirection()
        {
            Data.OnSetNegativeMovementDirection();
            await Yield();
        }

        private async UniTask SetPositiveMovementDirection()
        {
            Data.OnSetPositiveMovementDirection();
            await Yield();
        }

        private async UniTask ApplyMovingPlatformCurrentSpeedToMovementDirection()
        {
            Data.OnApplyMovingPlatformCurrentSpeedToMovementDirection(MovingPlatformCurrentSpeed.x);
            await Yield();
        }

        private async UniTask SetSavedMovementDirection()
        {
            Data.OnSetSavedMovementDirection();
            await Yield();
        }

        #endregion

        #region event handlers

        public async UniTask OnPlatformerSetCurrentGravity()
        {
            await SetCurrentGravity();
        }

        public async UniTask OnPlatformerApplyAscentMultiplierToCurrentGravity()
        {
            await ApplyAscentMultiplierToCurrentGravity();
        }

        public async UniTask OnPlatformerApplyFallMultiplierToCurrentGravity()
        {
            await ApplyFallMultiplierToCurrentGravity();
        }

        public async UniTask OnPlatformerApplyGravityToSpeedY()
        {
            await ApplyGravityToSpeedY();
        }

        public async UniTask OnPlatformerApplyFallSlowFactorToSpeedY()
        {
            await ApplyFallSlowFactorToSpeedY();
        }

        public async UniTask OnPlatformerOnInitializeFrame()
        {
            await InitializeFrame();
        }

        public async UniTask OnPlatformerApplyForces()
        {
            await ApplyForces();
        }

        public async UniTask OnPlatformerStopNewPosition()
        {
            await StopNewPosition();
        }

        public async UniTask OnPlatformerMoveCharacter()
        {
            await MoveCharacter();
        }

        public async UniTask OnPlatformerSetNewSpeed()
        {
            await SetNewSpeed();
        }

        public async UniTask OnPlatformerApplySlopeSpeedFactor()
        {
            await ApplySlopeSpeedFactor();
        }

        public async UniTask OnPlatformerClampSpeedToMaximumVelocity()
        {
            await ClampSpeedToMaximumVelocity();
        }

        public async UniTask OnPlatformerPhysics2DInteraction()
        {
            await Physics2DInteraction();
        }

        public async UniTask OnPlatformerStopExternalForce()
        {
            await StopExternalForce();
        }

        public async UniTask OnPlatformerUpdateWorldSpeed()
        {
            await UpdateWorldSpeed();
        }

        public async UniTask OnPlatformerTranslateMovingPlatformSpeedToTransform()
        {
            await TranslateMovingPlatformSpeedToTransform();
        }

        public async UniTask OnPlatformerApplyMovingPlatformBehavior()
        {
            await ApplyMovingPlatformBehavior();
        }

        public async UniTask OnPlatformerSetMovementDirectionToSaved()
        {
            await SetMovementDirectionToSaved();
        }

        public async UniTask OnPlatformerSetNegativeMovementDirection()
        {
            await SetNegativeMovementDirection();
        }

        public async UniTask OnPlatformerSetPositiveMovementDirection()
        {
            await SetPositiveMovementDirection();
        }

        public async UniTask OnPlatformerApplyMovingPlatformCurrentSpeedToMovementDirection()
        {
            await ApplyMovingPlatformCurrentSpeedToMovementDirection();
        }

        public async UniTask OnPlatformerSetSavedMovementDirection()
        {
            await SetSavedMovementDirection();
        }

        #endregion
    }
}

#region hide

/*
 * private async UniTask InitializeDeltaMove()
        {
            Data.OnInitializeDeltaMove();
            await Yield();
        }

        private bool OnGround => raycastData.OnGround;
        private bool OnSlope => raycastData.OnSlope;
        private bool IgnoreFriction => raycastData.IgnoreFriction;
        private int GroundDirectionAxis => raycastData.GroundDirectionAxis;
        private float GroundAngle => raycastData.GroundAngle;

        private async UniTask UpdateExternalForce()
        {
            Data.OnUpdateExternalForce(OnGround);
            await Yield();
        }

        private async UniTask StopExternalForce()
        {
            Data.OnStopExternalForce();
            await Yield();
        }

        private async UniTask ApplyGravityToSpeed()
        {
            Data.OnApplyGravityToSpeed();
            await Yield();
        }

        private async UniTask ApplyExternalForceToGravity()
        {
            Data.OnApplyExternalForceToGravity();
            await Yield();
        }

        private async UniTask UpdateExternalForceX()
        {
            Data.OnUpdateExternalForceX(GroundDirectionAxis);
            await Yield();
        }
        private async UniTask DescendSlope()
        {
            Data.OnDescendSlope(GroundAngle);
            await Yield();
        }

        private async UniTask ClimbSlope()
        {
            Data.OnClimbSlope(GroundAngle);
            await Yield();
        }

        private RaycastHit2D Hit => raycastData.Hit;
        private float SkinWidth => raycastData.SkinWidth;

        private async UniTask HitClimbingSlope()
        {
            Data.OnHitClimbingSlope(GroundAngle, Hit.distance, SkinWidth);
            await Yield();
        }

        private async UniTask HitMaximumSlope(float hitDistance, float skinWidth)
        {
            Data.OnHitMaximumSlope(hitDistance, skinWidth);
            await Yield();
        }

        private async UniTask HitSlopedGroundAngle()
        {
            Data.OnHitSlopedGroundAngle(GroundAngle);
            await Yield();
        }

        private async UniTask HitMaximumSlope()
        {
            Data.OnHitMaximumSlope();
            await Yield();
        }

        private async UniTask StopHorizontalSpeed()
        {
            Data.OnStopHorizontalSpeed();
            await Yield();
        }

        private async UniTask VerticalCollision()
        {
            Data.OnVerticalCollision(Hit.distance, SkinWidth);
            await Yield();
        }

        private async UniTask VerticalCollisionHitClimbingSlope()
        {
            Data.OnVerticalCollisionHitClimbingSlope(GroundAngle);
            await Yield();
        }

        private async UniTask ClimbSteepSlope()
        {
            Data.OnClimbSteepSlope(Hit.distance, SkinWidth);
            await Yield();
        }

        private float HitAngle => raycastData.HitAngle;

        private async UniTask ClimbMildSlope()
        {
            Data.OnClimbMildSlope(HitAngle, GroundAngle, Hit.distance, SkinWidth);
            await Yield();
        }

        private async UniTask DescendMildSlope()
        {
            Data.OnDescendMildSlope(Hit.distance, SkinWidth);
            await Yield();
        }

        private async UniTask DescendSteepSlope()
        {
            Data.OnDescendSteepSlope(HitAngle, GroundAngle, Hit.distance, SkinWidth);
            await Yield();
        }

        private async UniTask MoveCharacter()
        {
            Data.OnMoveCharacter(ref character);
            await Yield();
        }

        private async UniTask ResetJumpCollision()
        {
            Data.OnResetJumpCollision();
            await Yield();
        }
        public async UniTask OnPlatformerInitializeDeltaMove()
        {
            await InitializeDeltaMove();
        }
        
        public async UniTask OnPlatformerUpdateExternalForce()
        {
            await UpdateExternalForce();
        }

        public async UniTask OnPlatformerStopExternalForce()
        {
            await StopExternalForce();
        }

        public async UniTask OnPlatformerApplyGravityToSpeed()
        {
            await ApplyGravityToSpeed();
        }

        public async UniTask OnPlatformerApplyExternalForceToGravity()
        {
            await ApplyExternalForceToGravity();
        }

        public async UniTask OnPlatformerUpdateExternalForceX()
        {
            await UpdateExternalForceX();
        }
        
        public async UniTask OnPlatformerDescendSlope()
        {
            await DescendSlope();
        }

        public async UniTask OnPlatformerClimbSlope()
        {
            await ClimbSlope();
        }

        public async UniTask OnRaycastHorizontalCollisionRaycastHitClimbingSlope()
        {
            await HitClimbingSlope();
        }

        public async UniTask OnRaycastHorizontalCollisionRaycastHitMaximumSlopeSetDeltaMoveX()
        {
            await HitMaximumSlope(Hit.distance, SkinWidth);
        }

        public async UniTask OnHorizontalCollisionRaycastHitSlopedGroundAngle()
        {
            await HitSlopedGroundAngle();
        }

        public async UniTask OnRaycastHorizontalCollisionRaycastHitMaximumSlope()
        {
            await HitMaximumSlope();
        }

        public async UniTask OnPlatformerStopHorizontalSpeed()
        {
            await StopHorizontalSpeed();
        }

        public async UniTask OnRaycastVerticalCollisionRaycastHit()
        {
            await VerticalCollision();
        }

        public async UniTask OnRaycastVerticalCollisionRaycastHitClimbingSlope()
        {
            await VerticalCollisionHitClimbingSlope();
        }

        public async UniTask OnPlatformerClimbSteepSlopeHit()
        {
            await ClimbSteepSlope();
        }

        public async UniTask OnPlatformerClimbMildSlopeHit()
        {
            await ClimbMildSlope();
        }

        public async UniTask OnPlatformerDescendMildSlopeHit()
        {
            await DescendMildSlope();
        }

        public async UniTask OnPlatformerDescendSteepSlopeHit()
        {
            await DescendSteepSlope();
        }

        public async UniTask OnPlatformerMoveCharacter()
        {
            await MoveCharacter();
        }

        public async UniTask OnPlatformerResetJumpCollision()
        {
            await ResetJumpCollision();
        }
 */

#endregion
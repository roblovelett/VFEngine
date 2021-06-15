using System.Collections.Generic;
using Cysharp.Threading.Tasks;

using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Physics.ScriptableObjects;
using VFEngine.Platformer.ScriptableObjects;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics
{
    using static ScriptableObject;
    using static GameObject;
    using static UniTask;
    using static RaycastData;
    using static MathsExtensions;

    public class PhysicsController : MonoBehaviour
    {
        #region events

        #endregion

        #region properties

        public PhysicsData Data { get; private set; }

        #endregion

        #region fields

        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsSettings settings;
        private RaycastData raycastData;
        private Data platformerData;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            if (!Data) Data = CreateInstance<PhysicsData>();
            Data.OnInitialize(settings, ref character);
        }

        private void Dependencies()
        {
            raycastData = GetComponent<RaycastController>().Data;
            platformerData = GetComponent<PlatformerController>().Data;
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            Dependencies();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private async UniTask CurrentGravity()
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

        private async UniTask NewSpeed()
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
            PhysicsData.OnTranslateMovingPlatformSpeedToTransform(ref character, MovingPlatformCurrentSpeed);
            await Yield();
        }

        private async UniTask ApplyMovingPlatformBehavior()
        {
            Data.OnApplyMovingPlatformBehavior(MovingPlatformCurrentSpeed.y);
            await Yield();
        }

        private async UniTask MovementDirectionToSaved()
        {
            Data.OnSetMovementDirectionToSaved();
            await Yield();
        }

        private async UniTask NegativeMovementDirection()
        {
            Data.OnSetNegativeMovementDirection();
            await Yield();
        }

        private async UniTask PositiveMovementDirection()
        {
            Data.OnSetPositiveMovementDirection();
            await Yield();
        }

        private async UniTask ApplyMovingPlatformCurrentSpeedToMovementDirection()
        {
            Data.OnApplyMovingPlatformCurrentSpeedToMovementDirection(MovingPlatformCurrentSpeed.x);
            await Yield();
        }

        private async UniTask SavedMovementDirection()
        {
            Data.OnSetSavedMovementDirection();
            await Yield();
        }

        private Direction RaycastDirection => raycastData.CurrentDirection;
        private int Index => platformerData.Index;
        private Vector2 CurrentHorizontalHitPoint => raycastData.HorizontalHitStorage[Index].point;
        private Vector2 HorizontalRaycastFromBottom => raycastData.HorizontalRaycastFromBottom;
        private Vector2 HorizontalRaycastToTop => raycastData.HorizontalRaycastToTop;

        private float NewPositionXOnHitWallInMovementDirectionDistance =>
            DistanceBetweenPointAndLine(CurrentHorizontalHitPoint, HorizontalRaycastFromBottom, HorizontalRaycastToTop);

        private float BoundsWidth => raycastData.BoundsWidth;
        private float RayOffset => raycastData.RayOffset;

        private async UniTask SetPhysicsOnHitWallInMovementDirection()
        {
            Data.OnSetPhysicsOnHitWallInMovementDirection(RaycastDirection,
                NewPositionXOnHitWallInMovementDirectionDistance, BoundsWidth, RayOffset);
            await Yield();
        }

        private async UniTask StopNewPositionX()
        {
            Data.OnStopNewPositionX();
            await Yield();
        }

        private async UniTask StopSpeedX()
        {
            Data.OnStopSpeedX();
            await Yield();
        }

        private async UniTask IsFalling()
        {
            Data.OnSetIsFalling();
            await Yield();
        }

        private async UniTask IsNotFalling()
        {
            Data.OnSetIsNotFalling();
            await Yield();
        }

        private async UniTask ApplySpeedToNewPositionY()
        {
            Data.OnApplySpeedToNewPositionY();
            await Yield();
        }

        private float BelowRaycastDistance => raycastData.BelowRaycastDistance;
        private float BoundsHeight => raycastData.BoundsHeight;

        private async UniTask SetNewPositionYOnSmallestDistanceHit()
        {
            Data.OnSetNewPositionYOnSmallestDistanceHit(BelowRaycastDistance, BoundsHeight, RayOffset);
            await Yield();
        }

        private async UniTask ApplySpeedYToNewPositionY()
        {
            Data.OnApplySpeedYToNewPositionY();
            await Yield();
        }

        private async UniTask StopNewPositionY()
        {
            Data.OnStopNewPositionY();
            await Yield();
        }

        private async UniTask SetPhysicsOnDetachFromMovingPlatform()
        {
            Data.OnSetPhysicsOnDetachFromMovingPlatform();
            await Yield();
        }

        private RaycastHit2D StickToSlopeRaycast => raycastData.StickToSlopeRaycast;
        private Vector2 RaycastOrigin => raycastData.RaycastOrigin;
        private async UniTask SetNewPositionYOnStickToSlopeRaycastHit()
        {
            Data.OnSetNewPositionYOnStickToSlopeRaycastHit(StickToSlopeRaycast.point.y, RaycastOrigin.y, BoundsHeight);
            await Yield();
        }

        private float SmallestDistance => platformerData.SmallestDistance;
        private async UniTask SetNewPositionYOnAboveRaycastSmallestDistanceHit()
        {
            Data.OnSetNewPositionYOnAboveRaycastSmallestDistanceHit(SmallestDistance, BoundsHeight);
            await Yield();
        }
        
        private async UniTask SetSpeedOnAboveRaycastSmallestDistanceHit()
        {
            Data.OnSetSpeedOnAboveRaycastSmallestDistanceHit();
            await Yield();
        }
        
        private async UniTask StopForcesY()
        {
            Data.OnStopForcesY();
            await Yield();
        }
        
        #endregion

        #region event handlers

        public async UniTask OnPlatformerSetCurrentGravity()
        {
            await CurrentGravity();
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
            await NewSpeed();
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
            await MovementDirectionToSaved();
        }

        public async UniTask OnPlatformerSetNegativeMovementDirection()
        {
            await NegativeMovementDirection();
        }

        public async UniTask OnPlatformerSetPositiveMovementDirection()
        {
            await PositiveMovementDirection();
        }

        public async UniTask OnPlatformerApplyMovingPlatformCurrentSpeedToMovementDirection()
        {
            await ApplyMovingPlatformCurrentSpeedToMovementDirection();
        }

        public async UniTask OnPlatformerSetSavedMovementDirection()
        {
            await SavedMovementDirection();
        }

        public async UniTask OnPlatformerSetPhysicsOnHitWallInMovementDirection()
        {
            await SetPhysicsOnHitWallInMovementDirection();
        }

        public async UniTask OnPlatformerStopNewPositionX()
        {
            await StopNewPositionX();
        }

        public async UniTask OnPlatformerStopSpeedX()
        {
            await StopSpeedX();
        }

        public async UniTask OnPlatformerSetIsFalling()
        {
            await IsFalling();
        }

        public async UniTask OnPlatformerSetNotFalling()
        {
            await IsNotFalling();
        }

        public async UniTask OnPlatformerApplySpeedToNewPositionY()
        {
            await ApplySpeedToNewPositionY();
        }

        public async UniTask OnPlatformerSetNewPositionYOnSmallestDistanceHit()
        {
            await SetNewPositionYOnSmallestDistanceHit();
        }

        public async UniTask OnPlatformerApplySpeedYToNewPositionY()
        {
            await ApplySpeedYToNewPositionY();
        }

        public async UniTask OnPlatformerStopNewPositionY()
        {
            await StopNewPositionY();
        }

        public async UniTask OnPlatformerSetPhysicsOnDetachFromMovingPlatform()
        {
            await SetPhysicsOnDetachFromMovingPlatform();
        }

        public async UniTask OnPlatformerSetNewPositionYOnStickToSlopeRaycastHit()
        {
            await SetNewPositionYOnStickToSlopeRaycastHit();
        }

        public async UniTask OnPlatformerSetNewPositionYOnAboveRaycastSmallestDistanceHit()
        {
            await SetNewPositionYOnAboveRaycastSmallestDistanceHit();
        }
        public async UniTask OnPlatformerSetSpeedOnAboveRaycastSmallestDistanceHit()
        {
            await SetSpeedOnAboveRaycastSmallestDistanceHit();
        }
        public async UniTask OnPlatformerStopForcesY()
        {
            await StopForcesY();
        }

        #endregion
    }
}
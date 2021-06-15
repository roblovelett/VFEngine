using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Layer.Mask.ScriptableObjects;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.ScriptableObjects;
using VFEngine.Platformer.ScriptableObjects;

// ReSharper disable InconsistentNaming
// ReSharper disable ConvertIfStatementToSwitchExpression
// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable MergeIntoLogicalPattern
namespace VFEngine.Platformer
{
    using static UniTask;
    using static Debug;
    using static ScriptableObject;
    using static Mathf;
    using static Time;
    using static RaycastData;
    using static RaycastData.Direction;
    using static Single;

    public class PlatformerController : MonoBehaviour
    {
        #region events

        #endregion

        #region properties

        public Data Data { get; private set; }

        #endregion

        #region fields

        [SerializeField] private RaycastController raycastController;
        [SerializeField] private LayerMaskController layerMaskController;
        [SerializeField] private PhysicsController physicsController;
        private RaycastData raycastData;
        private LayerMaskData layerMaskData;
        private PhysicsData physicsData;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!raycastController) raycastController = GetComponent<RaycastController>();
            if (!layerMaskController) layerMaskController = GetComponent<LayerMaskController>();
            if (!physicsController) physicsController = GetComponent<PhysicsController>();
            if (!Data) Data = CreateInstance<Data>();
            Data.OnInitialize();
        }

        private void SetDependencies()
        {
            raycastData = raycastController.Data;
            physicsData = physicsController.Data;
            layerMaskData = layerMaskController.Data;
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private async void Start()
        {
            SetDependencies();
            await Run();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private async UniTask Run()
        {
            var ct = this.GetCancellationTokenOnDestroy();
            try
            {
                await RunPlatformer(ct);
            }
            catch (OperationCanceledException e)
            {
                Log(e.Message);
            }
        }

        private async UniTask RunPlatformer(CancellationToken ct)
        {
            var run = true;
            while (run)
            {
                if (ct.IsCancellationRequested) run = false;
                await ApplyGravity();
                await InitializeFrame();
                await UpdateRaycastBounds();
                await ApplyMovingPlatformBehaviorToPhysics();
                await ApplyForces();
                await SetMovementDirection();
                await CastRays();
                await MoveCharacter();
                await UpdateRaycastBounds();
                await UpdateSpeed();
                await SetStates();
                await DistanceToGroundRaycast();
                await StopExternalForce();
                await OnExitFrame();
                await UpdateWorldSpeed();
                await WaitForFixedUpdate(ct);
            }
        }

        private Vector2 Speed => physicsData.Speed;
        private bool ApplyAscentMultiplierToCurrentGravity => Speed.y > 0;
        private bool ApplyFallMultiplierToCurrentGravity => Speed.y < 0;
        private bool GravityActive => physicsData.GravityActive;
        private bool ApplyGravityToSpeedY => GravityActive;
        private float FallSlowFactor => physicsData.FallSlowFactor;
        private bool ApplyFallSlowFactorToSpeedY => FallSlowFactor != 0;

        private async UniTask ApplyGravity()
        {
            await SetCurrentGravity();
            if (ApplyAscentMultiplierToCurrentGravity) await OnApplyAscentMultiplierToCurrentGravity();
            if (ApplyFallMultiplierToCurrentGravity) await OnApplyFallMultiplierToCurrentGravity();
            if (ApplyGravityToSpeedY) await OnApplyGravityToSpeedY();
            if (ApplyFallSlowFactorToSpeedY) await OnApplyFallSlowFactorToSpeedY();
            await Yield();
        }

        private async UniTask SetCurrentGravity()
        {
            await physicsController.OnPlatformerSetCurrentGravity();
        }

        private async UniTask OnApplyAscentMultiplierToCurrentGravity()
        {
            await physicsController.OnPlatformerApplyAscentMultiplierToCurrentGravity();
        }

        private async UniTask OnApplyFallMultiplierToCurrentGravity()
        {
            await physicsController.OnPlatformerApplyFallMultiplierToCurrentGravity();
        }

        private async UniTask OnApplyGravityToSpeedY()
        {
            await physicsController.OnPlatformerApplyGravityToSpeedY();
        }

        private async UniTask OnApplyFallSlowFactorToSpeedY()
        {
            await physicsController.OnPlatformerApplyFallSlowFactorToSpeedY();
        }

        private async UniTask InitializeFrame()
        {
            await OnInitializeFramePhysics();
            await OnInitializeFrameRaycast();
        }

        private async UniTask OnInitializeFramePhysics()
        {
            await physicsController.OnPlatformerOnInitializeFrame();
        }

        private async UniTask OnInitializeFrameRaycast()
        {
            await raycastController.OnPlatformerInitializeFrame();
        }

        private async UniTask UpdateRaycastBounds()
        {
            await raycastController.OnPlatformerUpdateRaycastBounds();
        }

        private bool HasMovingPlatform => raycastData.MovingPlatformNotNull;
        private Vector2 MovingPlatformCurrentSpeed => raycastData.MovingPlatformCurrentSpeed;

        private bool TranslateMovingPlatformSpeedToTransform => !IsNaN(MovingPlatformCurrentSpeed.x) &&
                                                                !IsNaN(MovingPlatformCurrentSpeed.y);

        private bool CollidedWithCeilingLastFrame => raycastData.CollidedWithCeilingLastFrame;

        private bool ApplyMovingPlatformBehavior => !(timeScale == 0 || IsNaN(MovingPlatformCurrentSpeed.x) ||
                                                      IsNaN(MovingPlatformCurrentSpeed.y) || deltaTime <= 0 ||
                                                      CollidedWithCeilingLastFrame);

        private async UniTask ApplyMovingPlatformBehaviorToPhysics()
        {
            if (HasMovingPlatform)
            {
                if (TranslateMovingPlatformSpeedToTransform) await OnTranslateMovingPlatformSpeedToTransform();
                if (ApplyMovingPlatformBehavior)
                {
                    await OnApplyMovingPlatformBehavior();
                    await UpdateRaycastBounds();
                }
            }

            await Yield();
        }

        private async UniTask OnTranslateMovingPlatformSpeedToTransform()
        {
            await physicsController.OnPlatformerTranslateMovingPlatformSpeedToTransform();
        }

        private async UniTask OnApplyMovingPlatformBehavior()
        {
            await raycastController.OnPlatformerApplyMovingPlatformBehavior();
            await physicsController.OnPlatformerApplyMovingPlatformBehavior();
        }

        private async UniTask ApplyForces()
        {
            await physicsController.OnPlatformerApplyForces();
        }

        private float MovementDirectionThreshold => physicsData.MovementDirectionThreshold;
        private Vector2 ExternalForce => physicsData.ExternalForce;

        private bool SetNegativeMovementDirection =>
            Speed.x < MovementDirectionThreshold || ExternalForce.x < -MovementDirectionThreshold;

        private bool SetPositiveMovementDirection =>
            Speed.x > MovementDirectionThreshold || ExternalForce.x > MovementDirectionThreshold;

        private bool MovingPlatformNotNull => raycastData.MovingPlatformNotNull;

        private bool ApplyMovingPlatformCurrentSpeedToMovementDirection =>
            MovingPlatformNotNull && Abs(MovingPlatformCurrentSpeed.x) > Abs(Speed.x);

        private async UniTask SetMovementDirection()
        {
            await SetMovementDirectionToSaved();
            if (SetNegativeMovementDirection) await OnSetNegativeMovementDirection();
            else if (SetPositiveMovementDirection) await OnSetPositiveMovementDirection();
            if (ApplyMovingPlatformCurrentSpeedToMovementDirection)
                await OnApplyMovingPlatformCurrentSpeedToMovementDirection();
            await SetSavedMovementDirection();
        }

        private async UniTask SetMovementDirectionToSaved()
        {
            await physicsController.OnPlatformerSetMovementDirectionToSaved();
        }

        private async UniTask OnSetNegativeMovementDirection()
        {
            await physicsController.OnPlatformerSetNegativeMovementDirection();
        }

        private async UniTask OnSetPositiveMovementDirection()
        {
            await physicsController.OnPlatformerSetPositiveMovementDirection();
        }

        private async UniTask OnApplyMovingPlatformCurrentSpeedToMovementDirection()
        {
            await physicsController.OnPlatformerApplyMovingPlatformCurrentSpeedToMovementDirection();
        }

        private async UniTask SetSavedMovementDirection()
        {
            await physicsController.OnPlatformerSetSavedMovementDirection();
        }

        private bool CastRaysOnBothSides => raycastData.CastRaysOnBothSides;
        private int MovementDirection => physicsData.MovementDirection;
        private bool CastRaysLeft => MovementDirection == -1;

        private async UniTask CastRays()
        {
            if (CastRaysOnBothSides)
            {
                await OnCastRaysLeft();
                await CastRaysRight();
            }
            else
            {
                if (CastRaysLeft) await OnCastRaysLeft();
                else await CastRaysRight();
            }

            await CastRaysBelow();
            await CastRaysAbove();
        }

        private async UniTask OnCastRaysLeft()
        {
            await SetCurrentRaycastDirectionToLeft();
            await CastRaysHorizontally();
        }

        private async UniTask SetCurrentRaycastDirectionToLeft()
        {
            await raycastController.OnPlatformerSetCurrentRaycastDirectionToLeft();
        }

        private async UniTask CastRaysRight()
        {
            await SetCurrentRaycastDirectionToRight();
            await CastRaysHorizontally();
        }

        private async UniTask SetCurrentRaycastDirectionToRight()
        {
            await raycastController.OnPlatformerSetCurrentRaycastDirectionToRight();
        }

        private Direction CurrentDirection => raycastData.CurrentDirection;
        private bool SetHorizontalRaycast => CurrentDirection == Left || CurrentDirection == Right;
        private RaycastHit2D[] HorizontalHitStorage => raycastData.HorizontalHitStorage;
        private int NumberOfHorizontalRays => raycastData.NumberOfHorizontalRays;
        private int Index => Data.Index;
        private bool ResizeHorizontalHitStorage => HorizontalHitStorage.Length != NumberOfHorizontalRays;
        private bool SetHorizontalRaycastForPlatform => GroundedLastFrame && Index == 0;
        private bool HorizontalRaycastHit => HorizontalHitStorage[Index].distance > 0;
        private bool StopCastingHorizontalRays => HorizontalHitStorage[Index].collider == IgnoredCollider;

        private int CurrentDirectionNumerical
        {
            get
            {
                var direction = 0;
                if (CurrentDirection == Left) direction = -1;
                else if (CurrentDirection == Right) direction = 1;
                return direction;
            }
        }

        private bool SetLateralSlopeAngle => MovementDirection == CurrentDirectionNumerical;
        private float HorizontalHitAngle => raycastData.HorizontalHitAngle;
        private float MaximumSlopeAngle => physicsData.MaximumSlopeAngle;
        private bool HitWall => HorizontalHitAngle > MaximumSlopeAngle;
        private bool SetCollisionOnHitWallInMovementDirection => MovementDirection == CurrentDirectionNumerical;
        private bool StopAirborneCharacter => !IsGrounded && Speed.y != 0;

        private async UniTask CastRaysHorizontally()
        {
            if (SetHorizontalRaycast)
            {
                await OnSetHorizontalRaycast();
                if (ResizeHorizontalHitStorage) await OnResizeHorizontalHitStorage();
                for (var i = 0; i < NumberOfHorizontalRays; i++)
                {
                    SetIndex(i);
                    await SetHorizontalRaycastOrigin();
                    if (SetHorizontalRaycastForPlatform) await OnSetHorizontalRaycastForPlatform();
                    else await SetHorizontalRaycastForSpecialPlatforms();
                    if (!HorizontalRaycastHit) continue;
                    if (StopCastingHorizontalRays) break;
                    await SetHorizontalHitAngle();
                    if (SetLateralSlopeAngle) await OnSetLateralSlopeAngle();
                    if (!HitWall) continue;
                    await SetCollisionOnHitWall();
                    if (SetCollisionOnHitWallInMovementDirection)
                    {
                        await OnSetCollisionOnHitWallInMovementDirection();
                        await SetPhysicsOnHitWallInMovementDirection();
                        if (StopAirborneCharacter) await StopNewPositionX();
                        await AddHorizontalContact();
                        await StopSpeedX();
                    }

                    break;
                }
            }

            await Yield();
        }

        private async UniTask OnSetHorizontalRaycast()
        {
            await raycastController.OnPlatformerSetHorizontalRaycast();
        }

        private async UniTask OnResizeHorizontalHitStorage()
        {
            await raycastController.OnPlatformerResizeHorizontalHitStorage();
        }

        private void SetIndex(int index)
        {
            Data.OnSetIndex(index);
        }

        private async UniTask SetHorizontalRaycastOrigin()
        {
            await raycastController.OnPlatformerSetHorizontalRaycastOrigin();
        }

        private async UniTask OnSetHorizontalRaycastForPlatform()
        {
            await raycastController.OnPlatformerSetHorizontalRaycastForPlatform();
        }

        private async UniTask SetHorizontalRaycastForSpecialPlatforms()
        {
            await raycastController.OnPlatformerSetHorizontalRaycastForSpecialPlatforms();
        }

        private async UniTask SetHorizontalHitAngle()
        {
            await raycastController.OnPlatformerSetHorizontalHitAngle();
        }

        private async UniTask OnSetLateralSlopeAngle()
        {
            await raycastController.OnPlatformerSetLateralSlopeAngle();
        }

        private async UniTask SetCollisionOnHitWall()
        {
            await raycastController.OnPlatformerSetCollisionOnHitWall();
        }

        private async UniTask OnSetCollisionOnHitWallInMovementDirection()
        {
            await raycastController.OnPlatformerSetCollisionOnHitWallInMovementDirection();
        }

        private async UniTask SetPhysicsOnHitWallInMovementDirection()
        {
            await physicsController.OnPlatformerSetPhysicsOnHitWallInMovementDirection();
        }

        private async UniTask StopNewPositionX()
        {
            await physicsController.OnPlatformerStopNewPositionX();
        }

        private async UniTask AddHorizontalContact()
        {
            await raycastController.OnPlatformerAddHorizontalContact();
        }

        private async UniTask StopSpeedX()
        {
            await physicsController.OnPlatformerStopSpeedX();
        }

        private float SmallValue => Data.SmallValue;
        private bool SetIsFalling => NewPosition.y < SmallValue;
        private float Gravity => physicsData.Gravity;
        private bool IsFalling => physicsData.IsFalling;
        private bool SetNotCollidingBelow => Gravity > 0 && !IsFalling;
        private bool SetVerticalRaycastLengthOnMovingPlatform => OnMovingPlatform;
        private bool ApplyNewPositionYToVerticalRaycastLength => NewPosition.y < 0;
        private int NumberOfVerticalRays => raycastData.NumberOfVerticalRays;
        private RaycastHit2D[] BelowHitStorage => raycastData.BelowHitStorage;
        private bool ResizeBelowHitStorage => BelowHitStorage.Length != NumberOfVerticalRays;
        private bool StandingOnLastFrameNotNull => raycastData.StandingOnLastFrameNotNull;
        private bool SetSavedBelowLayerToStandingOnLastFrame => StandingOnLastFrameNotNull;

        private bool SetStandingOnLastFrameLayerToPlatforms =>
            layerMaskData.MidHeightOneWayPlatformContainsStandingOnLastFrame;

        private bool SetRaysBelowToPlatformsWithoutMidHeight => !SetStandingOnLastFrameLayerToPlatforms &&
                                                                StandingOnLastFrameNotNull && GroundedLastFrame;

        private bool StandingOnColliderContainsBottomCenterPosition => raycastData.StandingOnContainsBottomCenter;
        private bool StairsContainsStandingOnLastFrame => layerMaskData.StairsContainsStandingOnLastFrame;

        private bool SetRaysBelowToPlatformsAndOneWayOrStairs => StandingOnColliderContainsBottomCenterPosition &&
                                                                 StairsContainsStandingOnLastFrame &&
                                                                 StandingOnLastFrameNotNull && GroundedLastFrame;

        private bool SetRaysBelowToOneWayPlatform => OnMovingPlatform && NewPosition.y > 0;
        private bool SetBelowRaycastToPlatformsWithoutOneWay => NewPosition.y > 0 && !GroundedLastFrame;
        private bool BelowRaycastHit => BelowHitStorage[Index];
        private bool CastNextBelowRay => BelowHitStorage[Index].collider == IgnoredCollider;
        private Vector3 CrossBelowSlopeAngle => raycastData.CrossBelowSlopeAngle;
        private bool SetNegativeBelowSlopeAngle => CrossBelowSlopeAngle.z < 0;
        private float SmallestDistance => Data.SmallestDistance;
        private bool SetSmallestDistance => BelowHitStorage[Index].distance < SmallestDistance;
        private float BelowRaycastDistance => raycastData.BelowRaycastDistance;
        private bool StopCastingRaysBelow => BelowRaycastDistance < SmallValue;
        private bool SmallestDistanceHitConnected => Data.SmallestDistanceHitConnected;
        private float BoundsHeight => raycastData.BoundsHeight;
        private bool OneWayPlatformContainsStandingOn => layerMaskData.OneWayPlatformContainsStandingOn;
        private bool MovingOneWayPlatformContainsStandingOn => layerMaskData.MovingOneWayPlatformContainsStandingOn;

        private bool SetNotCollidingBelowOnSmallestHitConnected => !GroundedLastFrame &&
                                                                   SmallestDistance < BoundsHeight / 2 &&
                                                                   OneWayPlatformContainsStandingOn ||
                                                                   MovingOneWayPlatformContainsStandingOn;

        private bool ApplySpeedToNewPositionY => ExternalForce.y > 0 && Speed.y > 0;
        private bool ApplySpeedYToNewPositionY => !GroundedLastFrame && Speed.y > 0;
        private bool StopNewPositionY => Abs(NewPosition.y) < SmallValue;
        private bool SetFriction => raycastData.FrictionNotNull;
        private bool DetachFromMovingPlatformOnSmallestDistanceHitConnected => !MovingPlatformNotNull && !IsGrounded;
        private bool DetachFromMovingPlatform => OnMovingPlatform;
        private bool StickToSlopeBehavior => physicsData.StickToSlopeBehavior;

        private async UniTask CastRaysBelow()
        {
            await SetCurrentRaycastDirectionToDown();
            await InitializeFriction();
            if (SetIsFalling) await OnSetIsFalling();
            else await SetNotFalling();
            if (SetNotCollidingBelow)
            {
                await OnSetNotCollidingBelow();
            }
            else
            {
                await SetVerticalRaycastLength();
                if (SetVerticalRaycastLengthOnMovingPlatform) await OnSetVerticalRaycastLengthOnMovingPlatform();
                if (ApplyNewPositionYToVerticalRaycastLength) await OnApplyNewPositionYToVerticalRaycastLength();
                await SetVerticalRaycast();
                if (ResizeBelowHitStorage) await OnResizeBelowHitStorage();
                await SetRaysBelowPlatformsLayer();
                await SetStandingOnLastFrameNotNull();
                if (SetSavedBelowLayerToStandingOnLastFrame)
                {
                    await OnSetSavedBelowLayerToStandingOnLastFrame();
                    await SetMidHeightOneWayPlatformContainsStandingOnLastFrame();
                    if (SetStandingOnLastFrameLayerToPlatforms) await OnSetStandingOnLastFrameLayerToPlatforms();
                }

                if (SetRaysBelowToPlatformsWithoutMidHeight) await OnSetRaysBelowToPlatformsWithoutMidHeight();
                await SetStandingOnColliderContainsBottomCenterPosition();
                await SetStairsContainsStandingOnLastFrame();
                if (SetRaysBelowToPlatformsAndOneWayOrStairs) await OnSetRaysBelowToPlatformsAndOneWayOrStairs();
                if (SetRaysBelowToOneWayPlatform) await OnSetRaysBelowToOneWayPlatform();
                InitializeSmallestDistanceProperties();
                for (var i = 0; i < NumberOfVerticalRays; i++)
                {
                    SetIndex(i);
                    await SetBelowRaycastOrigin();
                    if (SetBelowRaycastToPlatformsWithoutOneWay) await OnSetBelowRaycastToBelowPlatformsWithoutOneWay();
                    else await SetBelowRaycastToBelowPlatforms();
                    await SetBelowRaycastDistance();
                    if (BelowRaycastHit)
                    {
                        if (CastNextBelowRay) continue;
                        OnSmallestHitConnected();
                        await SetCollisionOnBelowRaycastHit();
                        if (SetNegativeBelowSlopeAngle) await OnSetNegativeBelowSlopeAngle();
                        if (SetSmallestDistance) OnSetSmallestDistanceProperties(i, BelowHitStorage[i].distance);
                    }

                    if (StopCastingRaysBelow) break;
                }

                if (SmallestDistanceHitConnected)
                {
                    await SetStandingOnOnSmallestHitConnected();
                    await SetPlatformsContainStandingOn();
                    if (SetNotCollidingBelowOnSmallestHitConnected)
                    {
                        await OnSetNotCollidingBelow();
                        return;
                    }

                    await SetNotFalling();
                    await SetCollidingBelow();
                    if (ApplySpeedToNewPositionY)
                    {
                        await OnApplySpeedToNewPositionY();
                        await OnSetNotCollidingBelow();
                    }
                    else
                    {
                        await SetBelowRaycastDistanceOnSmallestDistanceHit();
                        await SetNewPositionYOnSmallestDistanceHit();
                    }

                    if (ApplySpeedYToNewPositionY) await OnApplySpeedYToNewPositionY();
                    if (StopNewPositionY) await OnStopNewPositionY();
                    await FrictionTest();
                    if (SetFriction) await OnSetFriction();
                    await MovingPlatformTest();
                    if (DetachFromMovingPlatformOnSmallestDistanceHitConnected) await OnDetachFromMovingPlatform();
                }
                else
                {
                    await OnSetNotCollidingBelow();
                    if (DetachFromMovingPlatform) await OnDetachFromMovingPlatform();
                }

                if (StickToSlopeBehavior) await OnStickToSlopeBehavior();
            }

            await Yield();
        }

        private async UniTask SetCurrentRaycastDirectionToDown()
        {
            await raycastController.OnPlatformerSetCurrentRaycastDirectionToDown();
        }

        private async UniTask InitializeFriction()
        {
            await raycastController.OnPlatformerInitializeFriction();
        }

        private async UniTask OnSetIsFalling()
        {
            await physicsController.OnPlatformerSetIsFalling();
        }

        private async UniTask SetNotFalling()
        {
            await physicsController.OnPlatformerSetNotFalling();
        }

        private async UniTask OnSetNotCollidingBelow()
        {
            await raycastController.OnPlatformerSetNotCollidingBelow();
        }

        private async UniTask SetVerticalRaycastLength()
        {
            await raycastController.OnPlatformerSetVerticalRaycastLength();
        }

        private async UniTask OnSetVerticalRaycastLengthOnMovingPlatform()
        {
            await raycastController.OnPlatformerSetVerticalRaycastLengthOnMovingPlatform();
        }

        private async UniTask OnApplyNewPositionYToVerticalRaycastLength()
        {
            await raycastController.OnPlatformerApplyNewPositionYToVerticalRaycastLength();
        }

        private async UniTask SetVerticalRaycast()
        {
            await raycastController.OnPlatformerSetVerticalRaycast();
        }

        private async UniTask OnResizeBelowHitStorage()
        {
            await raycastController.OnPlatformerResizeBelowHitStorage();
        }

        private async UniTask SetRaysBelowPlatformsLayer()
        {
            await layerMaskController.OnPlatformerSetRaysBelowPlatforms();
        }

        private async UniTask SetStandingOnLastFrameNotNull()
        {
            await raycastController.OnPlatformerSetStandingOnLastFrameNotNull();
        }

        private async UniTask OnSetSavedBelowLayerToStandingOnLastFrame()
        {
            await layerMaskController.OnPlatformerSetSavedBelowLayerToStandingOnLastFrame();
        }

        private async UniTask SetMidHeightOneWayPlatformContainsStandingOnLastFrame()
        {
            await layerMaskController.OnPlatformerSetMidHeightOneWayPlatformContainsStandingOnLastFrame();
        }

        private async UniTask OnSetStandingOnLastFrameLayerToPlatforms()
        {
            await raycastController.OnPlatformerSetStandingOnLastFrameLayerToPlatforms();
        }

        private async UniTask OnSetRaysBelowToPlatformsWithoutMidHeight()
        {
            await layerMaskController.OnPlatformerSetRaysBelowToPlatformsWithoutMidHeight();
        }

        private async UniTask SetStandingOnColliderContainsBottomCenterPosition()
        {
            await raycastController.OnPlatformerSetStandingOnColliderContainsBottomCenterPosition();
        }

        private async UniTask SetStairsContainsStandingOnLastFrame()
        {
            await layerMaskController.OnPlatformerSetStairsContainsStandingOnLastFrame();
        }

        private async UniTask OnSetRaysBelowToPlatformsAndOneWayOrStairs()
        {
            await layerMaskController.OnPlatformerSetRaysBelowToPlatformsAndOneWayOrStairs();
        }

        private async UniTask OnSetRaysBelowToOneWayPlatform()
        {
            await layerMaskController.OnPlatformerSetRaysBelowToOneWayPlatform();
        }

        private async UniTask SetBelowRaycastOrigin()
        {
            await raycastController.OnPlatformerSetBelowRaycastOrigin();
        }

        private async UniTask OnSetBelowRaycastToBelowPlatformsWithoutOneWay()
        {
            await raycastController.OnPlatformerSetBelowRaycastToBelowPlatformsWithoutOneWay();
        }

        private async UniTask SetBelowRaycastToBelowPlatforms()
        {
            await raycastController.OnPlatformerSetBelowRaycastToBelowPlatforms();
        }

        private async UniTask SetBelowRaycastDistance()
        {
            await raycastController.OnPlatformerSetBelowRaycastDistance();
        }

        private async UniTask SetCollisionOnBelowRaycastHit()
        {
            await raycastController.OnPlatformerSetCollisionOnBelowRaycastHit();
        }

        private async UniTask OnSetNegativeBelowSlopeAngle()
        {
            await raycastController.OnPlatformerSetNegativeBelowSlopeAngle();
        }

        private void InitializeSmallestDistanceProperties()
        {
            Data.OnInitializeSmallestDistanceProperties();
        }

        private void OnSmallestHitConnected()
        {
            Data.OnSmallestHitConnected();
        }

        private void OnSetSmallestDistanceProperties(int index, float belowRaycastHitDistance)
        {
            Data.OnSetSmallestDistanceProperties(index, belowRaycastHitDistance);
        }

        private async UniTask SetStandingOnOnSmallestHitConnected()
        {
            await raycastController.OnPlatformerSetStandingOnOnSmallestHitConnected();
        }

        private async UniTask SetPlatformsContainStandingOn()
        {
            await layerMaskController.OnPlatformerSetPlatformsContainStandingOn();
        }

        private async UniTask SetCollidingBelow()
        {
            await raycastController.OnPlatformerSetCollidingBelow();
        }

        private async UniTask OnApplySpeedToNewPositionY()
        {
            await physicsController.OnPlatformerApplySpeedToNewPositionY();
        }

        private async UniTask SetBelowRaycastDistanceOnSmallestDistanceHit()
        {
            await raycastController.OnPlatformerSetBelowRaycastDistanceOnSmallestDistanceHit();
        }

        private async UniTask SetNewPositionYOnSmallestDistanceHit()
        {
            await physicsController.OnPlatformerSetNewPositionYOnSmallestDistanceHit();
        }

        private async UniTask OnApplySpeedYToNewPositionY()
        {
            await physicsController.OnPlatformerApplySpeedYToNewPositionY();
        }

        private async UniTask OnStopNewPositionY()
        {
            await physicsController.OnPlatformerStopNewPositionY();
        }

        private async UniTask FrictionTest()
        {
            await raycastController.OnPlatformerFrictionTest();
        }

        private async UniTask OnSetFriction()
        {
            await raycastController.OnPlatformerSetFriction();
        }

        private async UniTask MovingPlatformTest()
        {
            await raycastController.OnPlatformerMovingPlatformTest();
        }

        private bool CannotDetach => !MovingPlatformNotNull;
        private bool Detach => !CannotDetach;

        private async UniTask OnDetachFromMovingPlatform()
        {
            if (Detach)
            {
                await SetPhysicsOnDetachFromMovingPlatform();
                await SetCollisionOnDetachFromMovingPlatform();
            }

            await Yield();
        }

        private async UniTask SetPhysicsOnDetachFromMovingPlatform()
        {
            await physicsController.OnPlatformerSetPhysicsOnDetachFromMovingPlatform();
        }

        private async UniTask SetCollisionOnDetachFromMovingPlatform()
        {
            await raycastController.OnPlatformerSetCollisionOnDetachFromMovingPlatform();
        }

        private float StickToSlopeOffsetY => raycastData.StickToSlopeOffsetY;
        private bool IsJumping => physicsData.IsJumping;
        private bool StairsContainsStandOnLastFrame => layerMaskData.StairsContainsStandingOnLastFrame;

        private bool CannotStickToSlope => NewPosition.y >= StickToSlopeOffsetY ||
                                           NewPosition.y <= -StickToSlopeOffsetY || IsJumping ||
                                           !StickToSlopeBehavior || !GroundedLastFrame || ExternalForce.y > 0 ||
                                           MovingPlatformNotNull && !(!GroundedLastFrame &&
                                                                      StandingOnLastFrameNotNull &&
                                                                      StairsContainsStandOnLastFrame && !IsJumping);

        private bool StickToSlope => !CannotStickToSlope;
        private Vector3 CrossBelowSlopeAngleLeft => raycastData.CrossBelowSlopeAngleLeft;
        private bool SetNegativeBelowSlopeAngleLeft => CrossBelowSlopeAngleLeft.z < 0;
        private Vector3 CrossBelowSlopeAngleRight => raycastData.CrossBelowSlopeAngleRight;
        private bool SetNegativeBelowSlopeAngleRight => CrossBelowSlopeAngleRight.z < 0;
        private float BelowSlopeAngleLeft => raycastData.BelowSlopeAngleLeft;
        private float BelowSlopeAngleRight => raycastData.BelowSlopeAngleRight;
        private float Tolerance => Data.Tolerance;
        private bool SetStickToSlopeRaycastOnSlope => Abs(BelowSlopeAngleLeft - BelowSlopeAngleRight) < Tolerance;

        private bool SetStickToSlopeRaycastOnRightSlopeOnLeftGround =>
            BelowSlopeAngleLeft == 0 && BelowSlopeAngleRight != 0;

        private bool SetStickToSlopeRaycastOnLeftSlopeOnRightGround =>
            BelowSlopeAngleLeft != 0 && BelowSlopeAngleRight == 0;

        private bool SetStickToSlopeRaycastPropertiesOnSlopes => BelowSlopeAngleLeft != 0 && BelowSlopeAngleRight != 0;
        private bool SetStickToSlopeRaycastOnMaximumAngle => BelowSlopeAngleLeft > 0 && BelowSlopeAngleRight < 0;
        private RaycastHit2D StickToSlopeRaycast => raycastData.StickToSlopeRaycast;

        private async UniTask OnStickToSlopeBehavior()
        {
            if (StickToSlope)
            {
                await SetStickToSlopeRayLength();
                await SetStickToSlopeRaycast();
                if (SetNegativeBelowSlopeAngleLeft) await OnSetNegativeBelowSlopeAngleLeft();
                if (SetNegativeBelowSlopeAngleRight) await OnSetNegativeBelowSlopeAngleRight();
                await SetCastStickToSlopeRaycastLeft();
                if (SetStickToSlopeRaycastOnSlope) await OnSetStickToSlopeRaycastOnSlope();
                if (SetStickToSlopeRaycastOnRightSlopeOnLeftGround)
                    await OnSetStickToSlopeRaycastOnRightSlopeOnLeftGround();
                if (SetStickToSlopeRaycastOnLeftSlopeOnRightGround)
                    await OnSetStickToSlopeRaycastOnLeftSlopeOnRightGround();
                if (SetStickToSlopeRaycastPropertiesOnSlopes) await OnSetStickToSlopeRaycastOnSlopes();
                if (SetStickToSlopeRaycastOnMaximumAngle)
                {
                    await OnSetStickToSlopeRaycastOnMaximumAngle();
                    await OnStickToSlopeRaycastHit();
                }

                await UpdateStickToSlopeRaycast();
                await OnStickToSlopeRaycastHit();
            }

            await Yield();
        }

        private async UniTask SetStickToSlopeRayLength()
        {
            await raycastController.OnPlatformerSetStickToSlopeRayLength();
        }

        private async UniTask SetStickToSlopeRaycast()
        {
            await raycastController.OnPlatformerSetStickToSlopesRaycast();
        }

        private async UniTask OnSetNegativeBelowSlopeAngleLeft()
        {
            await raycastController.OnPlatformerSetNegativeBelowSlopeAngleLeft();
        }

        private async UniTask OnSetNegativeBelowSlopeAngleRight()
        {
            await raycastController.OnPlatformerSetNegativeBelowSlopeAngleRight();
        }

        private async UniTask SetCastStickToSlopeRaycastLeft()
        {
            await raycastController.OnPlatformerSetCastStickToSlopeRaycastLeft();
        }

        private async UniTask OnSetStickToSlopeRaycastOnSlope()
        {
            await raycastController.OnPlatformerSetStickToSlopeRaycastOnSlope();
        }

        private async UniTask OnSetStickToSlopeRaycastOnRightSlopeOnLeftGround()
        {
            await raycastController.OnPlatformerSetStickToSlopeRaycastOnRightSlopeOnLeftGround();
        }

        private async UniTask OnSetStickToSlopeRaycastOnLeftSlopeOnRightGround()
        {
            await raycastController.OnPlatformerSetStickToSlopeRaycastOnLeftSlopeOnRightGround();
        }

        private async UniTask OnSetStickToSlopeRaycastOnSlopes()
        {
            await raycastController.OnPlatformerSetStickToSlopeRaycastOnSlopes();
        }

        private async UniTask OnSetStickToSlopeRaycastOnMaximumAngle()
        {
            await raycastController.OnPlatformerSetStickToSlopeRaycastOnMaximumAngle();
        }

        private bool StickToSlopeRaycastHit => StickToSlopeRaycast && StickToSlopeRaycast.collider != IgnoredCollider;

        private async UniTask OnStickToSlopeRaycastHit()
        {
            if (StickToSlopeRaycastHit)
            {
                await SetNewPositionYOnStickToSlopeRaycastHit();
                await SetCollisionOnStickToSlopeRaycastHit();
            }

            await Yield();
        }

        private async UniTask SetNewPositionYOnStickToSlopeRaycastHit()
        {
            await physicsController.OnPlatformerSetNewPositionYOnStickToSlopeRaycastHit();
        }

        private async UniTask SetCollisionOnStickToSlopeRaycastHit()
        {
            await raycastController.OnPlatformerSetCollisionOnStickToSlopeRaycastHit();
        }

        private async UniTask UpdateStickToSlopeRaycast()
        {
            await raycastController.OnPlatformerUpdateStickToSlopeRaycast();
        }

        private RaycastHit2D[] AboveHitStorage => raycastData.AboveHitStorage;
        private bool ResizeAboveHitStorage => AboveHitStorage.Length != NumberOfVerticalRays;
        private bool AboveRaycastHit => AboveHitStorage[Index];
        private bool AboveRaycastHitIgnoredCollider => AboveHitStorage[Index].collider == IgnoredCollider;
        private float CurrentAboveHitStorageDistance => AboveHitStorage[Index].distance;
        private bool SetSmallestDistanceToAboveRaycastHitDistance => CurrentAboveHitStorageDistance < SmallestDistance;
        private bool AboveRaycastSmallestDistanceHit => Data.SmallestDistanceHitConnected;
        private bool StopNewPositionYOnAboveRaycastSmallestDistanceHit => IsGrounded && NewPosition.y < 0;
        private bool SetSpeedOnAboveRaycastSmallestDistanceHit => !CollidedWithCeilingLastFrame;

        private async UniTask CastRaysAbove()
        {
            await SetCurrentRaycastDirectionToUp();
            await SetAboveRaycast();
            if (ResizeAboveHitStorage) await OnResizeAboveHitStorage();
            InitializeSmallestDistanceProperties();
            for (var i = 0; i < NumberOfVerticalRays; i++)
            {
                SetIndex(i);
                await UpdateAboveRaycast();
                if (!AboveRaycastHit) continue;
                SetSmallestDistancePropertiesOnAboveRaycastHit(i);
                if (AboveRaycastHitIgnoredCollider) break;
                if (SetSmallestDistanceToAboveRaycastHitDistance) OnSetSmallestDistanceToAboveRaycastHitDistance();
            }

            if (AboveRaycastSmallestDistanceHit)
            {
                await SetNewPositionYOnAboveRaycastSmallestDistanceHit();
                if (StopNewPositionYOnAboveRaycastSmallestDistanceHit) await OnStopNewPositionY();
                await SetCollisionOnAboveRaycastSmallestDistanceHit();
                if (SetSpeedOnAboveRaycastSmallestDistanceHit) await OnSetSpeedOnAboveRaycastSmallestDistanceHit();
                await StopForcesY();
            }

            await Yield();
        }

        private async UniTask SetCurrentRaycastDirectionToUp()
        {
            await raycastController.OnPlatformerSetCurrentRaycastDirectionToUp();
        }

        private async UniTask SetAboveRaycast()
        {
            await raycastController.OnPlatformerSetAboveRaycast();
        }

        private async UniTask OnResizeAboveHitStorage()
        {
            await raycastController.OnPlatformerResizeAboveHitStorage();
        }

        private async UniTask UpdateAboveRaycast()
        {
            await raycastController.OnPlatformerUpdateAboveRaycast();
        }

        private void SetSmallestDistancePropertiesOnAboveRaycastHit(int index)
        {
            Data.OnSetSmallestDistancePropertiesOnAboveRaycastHit(index);
        }

        private void OnSetSmallestDistanceToAboveRaycastHitDistance()
        {
            Data.OnSetSmallestDistance(CurrentAboveHitStorageDistance);
        }

        private async UniTask SetNewPositionYOnAboveRaycastSmallestDistanceHit()
        {
            await physicsController.OnPlatformerSetNewPositionYOnAboveRaycastSmallestDistanceHit();
        }

        private async UniTask SetCollisionOnAboveRaycastSmallestDistanceHit()
        {
            await raycastController.OnPlatformerSetCollisionOnAboveRaycastSmallestDistanceHit();
        }

        private async UniTask OnSetSpeedOnAboveRaycastSmallestDistanceHit()
        {
            await physicsController.OnPlatformerSetSpeedOnAboveRaycastSmallestDistanceHit();
        }

        private async UniTask StopForcesY()
        {
            await physicsController.OnPlatformerStopForcesY();
        }

        private bool PerformSafetyBoxcast => raycastData.PerformSafetyBoxcast;
        private RaycastHit2D SafetyBoxcastHit => raycastData.SafetyBoxcastHit;
        private Vector2 NewPosition => physicsData.NewPosition;

        private bool StopNewPosition =>
            SafetyBoxcastHit && Abs(SafetyBoxcastHit.distance - NewPosition.magnitude) < 0.002f;

        private async UniTask MoveCharacter()
        {
            if (PerformSafetyBoxcast)
            {
                await OnPerformSafetyBoxcast();
                if (StopNewPosition)
                {
                    await OnStopNewPosition();
                    return;
                }
            }

            await OnMoveCharacter();
        }

        private async UniTask OnPerformSafetyBoxcast()
        {
            await raycastController.OnPlatformerPerformSafetyBoxcast();
        }

        private async UniTask OnStopNewPosition()
        {
            await physicsController.OnPlatformerStopNewPosition();
        }

        private async UniTask OnMoveCharacter()
        {
            await physicsController.OnPlatformerMoveCharacter();
        }

        private static bool SetNewSpeed => deltaTime > 0;
        private bool IsGrounded => raycastData.IsGrounded;
        private bool ApplySlopeSpeedFactor => IsGrounded;
        private bool OnMovingPlatform => raycastData.OnMovingPlatform;
        private bool ClampSpeedToMaximumVelocity => !OnMovingPlatform;

        private async UniTask UpdateSpeed()
        {
            if (SetNewSpeed) await OnSetNewSpeed();
            if (ApplySlopeSpeedFactor) await OnApplySlopeSpeedFactor();
            if (ClampSpeedToMaximumVelocity) await OnClampSpeedToMaximumVelocity();
        }

        private async UniTask OnSetNewSpeed()
        {
            await physicsController.OnPlatformerSetNewSpeed();
        }

        private async UniTask OnApplySlopeSpeedFactor()
        {
            await physicsController.OnPlatformerApplySlopeSpeedFactor();
        }

        private async UniTask OnClampSpeedToMaximumVelocity()
        {
            await physicsController.OnPlatformerClampSpeedToMaximumVelocity();
        }

        private bool GroundedLastFrame => raycastData.GroundedLastFrame;
        private bool CollidingAbove => raycastData.CollidingAbove;
        private bool SetGroundedEvent => !GroundedLastFrame && CollidingAbove;
        private bool Physics2DInteractionCollision => raycastData.Colliding;

        private async UniTask SetStates()
        {
            if (SetGroundedEvent) await OnSetGroundedEvent();
            if (Physics2DInteractionCollision) await OnPhysics2DInteractionCollision();
            await Yield();
        }

        private async UniTask OnSetGroundedEvent()
        {
            await raycastController.OnPlatformerSetGroundedEvent();
        }

        private bool Physics2DInteraction => physicsData.Physics2DInteraction;

        private async UniTask OnPhysics2DInteractionCollision()
        {
            if (Physics2DInteraction) await OnPhysics2DInteraction();
            await Yield();
        }

        private async UniTask OnPhysics2DInteraction()
        {
            await physicsController.OnPlatformerPhysics2DInteraction();
        }

        private float DistanceToGroundRaycastMaximumLength => raycastData.DistanceToGroundRaycastMaximumLength;
        private bool SetDistanceToGroundRaycast => DistanceToGroundRaycastMaximumLength > 0;
        private RaycastHit2D DistanceToGroundRaycastHit => raycastData.DistanceToGroundRaycastHit;
        private Collider2D IgnoredCollider => raycastData.IgnoredCollider;
        private bool SetDistanceToGroundOnIgnoredColliderHit => DistanceToGroundRaycastHit.collider == IgnoredCollider;

        private async UniTask DistanceToGroundRaycast()
        {
            if (SetDistanceToGroundRaycast)
            {
                await OnSetDistanceToGroundRaycast();
                if (DistanceToGroundRaycastHit)
                {
                    if (SetDistanceToGroundOnIgnoredColliderHit)
                    {
                        await SetDistanceToGround();
                        return;
                    }

                    await SetDistanceToGroundOnRaycastHit();
                }
                else
                {
                    await SetDistanceToGround();
                }
            }

            await Yield();
        }

        private async UniTask OnSetDistanceToGroundRaycast()
        {
            await raycastController.OnPlatformerSetDistanceToGroundRaycast();
        }

        private async UniTask SetDistanceToGroundOnRaycastHit()
        {
            await raycastController.OnPlatformerSetDistanceToGroundOnRaycastHit();
        }

        private async UniTask SetDistanceToGround()
        {
            await raycastController.OnPlatformerSetDistanceToGround();
        }

        private async UniTask StopExternalForce()
        {
            await physicsController.OnPlatformerStopExternalForce();
        }

        private bool SetStandingOnLastFrameLayerToSavedBelow => raycastData.StandingOnLastFrameNotNull;

        private async UniTask OnExitFrame()
        {
            if (SetStandingOnLastFrameLayerToSavedBelow) await OnSetStandingOnLastFrameLayerToSavedBelow();
            await Yield();
        }

        private async UniTask OnSetStandingOnLastFrameLayerToSavedBelow()
        {
            await raycastController.OnPlatformerSetStandingOnLastFrameLayerToSavedBelow();
        }

        private async UniTask UpdateWorldSpeed()
        {
            await physicsController.OnPlatformerUpdateWorldSpeed();
        }

        #endregion

        #region event handlers

        #endregion
    }
}
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Layer.Mask.ScriptableObjects;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.ScriptableObjects;
using VFEngine.Platformer.ScriptableObjects;
using VFEngine.Tools;

// ReSharper disable SwitchStatementMissingSomeEnumCasesNoDefault
// ReSharper disable ConvertIfStatementToSwitchExpression
// ReSharper disable ConvertIfStatementToSwitchStatement
namespace VFEngine.Platformer
{
    using static UniTask;
    using static Debug;
    using static ScriptableObject;
    using static Mathf;
    using static Time;
    using static RaycastData;
    using static RaycastData.Direction;

    public class PlatformerController : SerializedMonoBehaviour
    {
        #region events

        #endregion

        #region properties

        [OdinSerialize] public PlatformerData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private RaycastController raycastController;
        [OdinSerialize] private LayerMaskController layerMaskController;
        [OdinSerialize] private PhysicsController physicsController;
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
            if (!Data) Data = CreateInstance<PlatformerData>();
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
                //Log($"Frame={frameCount} FixedTime={fixedTime}");
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

        private bool TranslateMovingPlatformSpeedToTransform => !float.IsNaN(MovingPlatformCurrentSpeed.x) &&
                                                                !float.IsNaN(MovingPlatformCurrentSpeed.y);

        private bool CollidedWithCeilingLastFrame => raycastData.CollidedWithCeilingLastFrame;

        private bool ApplyMovingPlatformBehavior => !(timeScale == 0 || float.IsNaN(MovingPlatformCurrentSpeed.x) ||
                                                      float.IsNaN(MovingPlatformCurrentSpeed.y) || deltaTime <= 0 ||
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
                    if (HitWall)
                    {
                        await SetCollisionOnHitWall();
                        if (SetCollisionOnHitWallInMovementDirection)
                        {
                            await OnSetCollisionOnHitWallInMovementDirection();
                            await SetNewPositionXOnHitWall();
                        }

                        if (StopAirborneCharacter) await OnStopAirborneCharacter();
                        await AddContactOnHitWall();
                        await StopSpeedXOnHitWall();
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

        private bool CastingLeft => CurrentDirection == Left;

        private async UniTask OnSetHorizontalRaycastForPlatform()
        {
            if (CastingLeft) await SetLeftRaycastForPlatform();
            else await SetRightRaycastForPlatform();
            await Yield();
        }

        private async UniTask SetLeftRaycastForPlatform()
        {
            await raycastController.OnPlatformerSetLeftRaycastForPlatform();
        }

        private async UniTask SetRightRaycastForPlatform()
        {
            await raycastController.OnPlatformerSetRightRaycastForPlatform();
        }

        private async UniTask SetHorizontalRaycastForSpecialPlatforms()
        {
            if (CastingLeft) await SetLeftRaycastForSpecialPlatforms();
            else await SetRightRaycastForSpecialPlatforms();
            await Yield();
        }

        private async UniTask SetLeftRaycastForSpecialPlatforms()
        {
            await raycastController.OnPlatformerSetLeftRaycastForSpecialPlatforms();
        }

        private async UniTask SetRightRaycastForSpecialPlatforms()
        {
            await raycastController.OnPlatformerSetRightRaycastForSpecialPlatforms();
        }

        private async UniTask OnSetLateralSlopeAngle()
        {
            await raycastController.OnPlatformerSetLateralSlopeAngle();
        }

        private async UniTask SetHorizontalHitAngle()
        {
            await raycastController.OnPlatformerSetHorizontalHitAngle();
        }

        private async UniTask SetCollisionOnHitWall()
        {
            if (CastingLeft) await SetCollisionOnLeftRaycastHitWall();
            else await SetCollisionOnRightRaycastHitWall();
            await Yield();
        }

        private async UniTask SetCollisionOnLeftRaycastHitWall()
        {
            await raycastController.OnPlatformerSetCollisionOnLeftRaycastHitWall();
        }

        private async UniTask SetCollisionOnRightRaycastHitWall()
        {
            await raycastController.OnPlatformerSetCollisionOnRightRaycastHitWall();
        }

        private async UniTask OnSetCollisionOnHitWallInMovementDirection()
        {
            await raycastController.OnPlatformerSetCollisionOnHitWallInMovementDirection();
        }

        private async UniTask SetNewPositionXOnHitWall()
        {
            if (CastingLeft) await SetNewPositionOnLeftRaycastHitWall();
            else await SetNewPositionOnRightRaycastHitWall();
            await Yield();
        }

        private async UniTask SetNewPositionOnLeftRaycastHitWall()
        {
            await physicsController.OnPlatformerSetNewPositionOnLeftRaycastHitWall();
        }

        private async UniTask SetNewPositionOnRightRaycastHitWall()
        {
            await physicsController.OnPlatformerSetNewPositionOnRightRaycastHitWall();
        }

        private async UniTask OnStopAirborneCharacter()
        {
            await physicsController.OnPlatformerStopAirborneCharacter();
        }

        private async UniTask AddContactOnHitWall()
        {
            await raycastController.OnPlatformerAddContactOnHitWall();
        }

        private async UniTask StopSpeedXOnHitWall()
        {
            await physicsController.OnPlatformerStopSpeedXOnHitWall();
        }

        private float SmallValue => Data.SmallValue;
        private bool SetIsFalling => NewPosition.y < SmallValue;
        private float Gravity => physicsData.Gravity;
        private bool IsFalling => physicsData.IsFalling;
        private bool SetNotCollidingBelow => Gravity > 0 && !IsFalling;
        private bool SetDoubleVerticalRaycastLength => OnMovingPlatform;
        private bool ApplyNewPositionYToVerticalRaycastLength => NewPosition.y < 0;
        private int NumberOfVerticalRays => raycastData.NumberOfVerticalRays;
        private RaycastHit2D[] BelowHitStorage => raycastData.BelowHitStorage;
        private bool ResizeBelowHitStorage => BelowHitStorage.Length != NumberOfVerticalRays;
        private bool StandingOnLastFrameNotNull => raycastData.StandingOnLastFrameNotNull;
        private bool SetSavedBelowLayerToStandingOnLastFrame => StandingOnLastFrameNotNull;
        private GameObject StandingOnLastFrame => raycastData.StandingOnLastFrame;
        private LayerMask MidHeightOneWayPlatform => layerMaskData.MidHeightOneWayPlatform;

        private bool SetStandingOnLastFrameLayerToPlatforms =>
            MidHeightOneWayPlatform.Contains(StandingOnLastFrame.layer);

        private bool SetRaysBelowToPlatformsWithoutMidHeight => !SetStandingOnLastFrameLayerToPlatforms &&
                                                                StandingOnLastFrameNotNull && GroundedLastFrame;

        private Collider2D StandingOnCollider => raycastData.StandingOnCollider;
        private Vector2 ColliderBottomCenterPosition => raycastData.ColliderBottomCenterPosition;
        private LayerMask Stairs => layerMaskData.Stairs;

        private bool SetRaysBelowToPlatformsAndOneWayOrStairs =>
            StandingOnCollider.bounds.Contains(ColliderBottomCenterPosition) &&
            Stairs.Contains(StandingOnLastFrame.layer) && StandingOnLastFrameNotNull && GroundedLastFrame;

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
        private GameObject StandingOn => raycastData.StandingOn;
        private LayerMask OneWayPlatform => layerMaskData.OneWayPlatform;
        private LayerMask MovingOneWayPlatform => layerMaskData.MovingOneWayPlatform;

        private bool SetNotCollidingBelowOnSmallestHitConnected => !GroundedLastFrame &&
                                                                   SmallestDistance < BoundsHeight / 2 &&
                                                                   (OneWayPlatform.Contains(StandingOn.layer) ||
                                                                    MovingOneWayPlatform.Contains(StandingOn.layer));

        private bool ApplySpeedToNewPositionY => ExternalForce.y > 0 && Speed.y > 0;
        private bool ApplySpeedYToNewPositionY => !GroundedLastFrame && Speed.y > 0;
        private bool StopNewPositionY => Abs(NewPosition.y) < SmallValue;
        private bool SetFriction => raycastData.FrictionNotNull;
        private bool DetachFromMovingPlatformOnSmallestDistanceHitConnected => !MovingPlatformNotNull && !IsGrounded;
        private bool DetachFromMovingPlatform => OnMovingPlatform;
        private bool StickToSlopeBehavior => physicsData.StickToSlopeBehavior;

        private async UniTask CastRaysBelow()
        {
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
                if (SetDoubleVerticalRaycastLength) await OnSetDoubleVerticalRaycastLength();
                if (ApplyNewPositionYToVerticalRaycastLength) await OnApplyNewPositionYToVerticalRaycastLength();
                await SetVerticalRaycast();
                if (ResizeBelowHitStorage) await OnResizeBelowHitStorage();
                await SetRaysBelowPlatformsLayer();
                if (SetSavedBelowLayerToStandingOnLastFrame)
                {
                    await OnSetSavedBelowLayerToStandingOnLastFrame();
                    if (SetStandingOnLastFrameLayerToPlatforms) await OnSetStandingOnLastFrameLayerToPlatforms();
                }

                if (SetRaysBelowToPlatformsWithoutMidHeight) await OnSetRaysBelowToPlatformsWithoutMidHeight();
                if (SetRaysBelowToPlatformsAndOneWayOrStairs) await OnSetRaysBelowToPlatformsAndOneWayOrStairs();
                if (SetRaysBelowToOneWayPlatform) await OnSetRaysBelowToOneWayPlatform();
                InitializeSmallestDistanceProperties();
                for (var i = 0; i < NumberOfVerticalRays; i++)
                {
                    SetIndex(i);
                    await SetBelowRaycastOrigin();
                    if (SetBelowRaycastToPlatformsWithoutOneWay) await OnSetBelowRaycastToPlatformsWithoutOneWay();
                    else await SetBelowRaycastToPlatforms();
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
                        await SetBelowRaycastDistanceOnSmallestDistanceHitConnected();
                        await SetNewPositionYOnSmallestDistanceHitConnected();
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

        private async UniTask OnSetDoubleVerticalRaycastLength()
        {
            await raycastController.OnPlatformerSetDoubleVerticalRaycastLength();
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
            await layerMaskController.OnPlatformerSetRaysBelowPlatformers();
        }

        private async UniTask OnSetSavedBelowLayerToStandingOnLastFrame()
        {
            await layerMaskController.OnPlatformerSetSavedBelowLayerToStandingOnLastFrame();
        }

        private async UniTask OnSetStandingOnLastFrameLayerToPlatforms()
        {
            await raycastController.OnPlatformerSetStandingOnLastFrameLayerToPlatforms();
        }

        private async UniTask OnSetRaysBelowToPlatformsWithoutMidHeight()
        {
            await layerMaskController.OnPlatformerSetRaysBelowToPlatformsWithoutMidHeight();
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

        private async UniTask OnSetBelowRaycastToPlatformsWithoutOneWay()
        {
            await raycastController.OnPlatformerSetBelowRaycastToPlatformsWithoutOneWay();
        }

        private async UniTask SetBelowRaycastToPlatforms()
        {
            await raycastController.OnPlatformerSetBelowRaycastToPlatforms();
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

        private async UniTask SetCollidingBelow()
        {
            await raycastController.OnPlatformerSetCollidingBelow();
        }

        private async UniTask OnApplySpeedToNewPositionY()
        {
            await physicsController.OnPlatformerApplySpeedToNewPositionY();
        }

        private async UniTask SetBelowRaycastDistanceOnSmallestDistanceHitConnected()
        {
            await raycastController.OnPlatformerSetBelowRaycastDistanceOnSmallestDistanceHitConnected();
        }

        private async UniTask SetNewPositionYOnSmallestDistanceHitConnected()
        {
            await physicsController.OnPlatformerSetNewPositionYOnSmallestDistanceHitConnected();
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

        private async UniTask OnDetachFromMovingPlatform()
        {
            // ---------------------------------
            // |
            // |
            // |
            // |
            // |
            // |
            // |
            // |
            // |
            // ---------------------------------
            await Yield();
        }

        private async UniTask OnStickToSlopeBehavior()
        {
            // ---------------------------------
            // |
            // |
            // |
            // |
            // |
            // |
            // |
            // |
            // |
            // ---------------------------------
            await Yield();
        }

        private async UniTask CastRaysAbove()
        {
            await Yield();
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

#region hide

/*
private async UniTask InitializeFrame()
{
    await ResetCollision();
    await InitializeDeltaMove();
    await SetSavedLayer();
    await UpdateOrigins();
}

private async UniTask ResetCollision()
{
    await raycastController.OnPlatformerResetCollision();
}

private async UniTask InitializeDeltaMove()
{
    await physicsController.OnPlatformerInitializeDeltaMove();
}

private async UniTask SetSavedLayer()
{
    await layerMaskController.OnPlatformerSetSavedLayer();
}

private async UniTask UpdateOrigins()
{
    await raycastController.OnPlatformerUpdateOrigins();
}

private int VerticalRays => raycastData.VerticalRays;
private float IgnorePlatformsTime => raycastData.IgnorePlatformsTime;
private bool CastGroundCollisionForOneWayPlatform => !Hit && IgnorePlatformsTime <= 0;
private bool CastNextGroundCollisionRay => (Hit.distance <= 0);
private async UniTask GroundCollision()
{
    for (var i = 0; i < VerticalRays; i++)
    {
        Data.OnGroundCollision(i);
        await SetGroundCollisionRaycast();
        if (CastGroundCollisionForOneWayPlatform)
        {
            await SetGroundCollisionRaycastForOneWayPlatform();
            if (CastNextGroundCollisionRay) continue;
        }

        if (!Hit) continue;
        await SetCollisionOnGroundCollisionRaycastHit();
        await CastGroundCollisionRay();
        break;
    }

    await Yield();
}

private async UniTask SetGroundCollisionRaycast()
{
    await raycastController.OnPlatformerSetGroundCollisionRaycast();
}

private async UniTask SetGroundCollisionRaycastForOneWayPlatform()
{
    await raycastController.OnPlatformerSetGroundCollisionRaycastForOneWayPlatform();
}

private async UniTask SetCollisionOnGroundCollisionRaycastHit()
{
    await raycastController.OnPlatformerSetCollisionOnGroundCollisionRaycastHit();
}

private async UniTask CastGroundCollisionRay()
{
    await raycastController.OnPlatformerCastGroundCollisionRay();
}

private async UniTask UpdateForces()
{
    await UpdateExternalForce();
    await UpdateGravity();
    await UpdateExternalForceX();
}

private bool IgnoreFriction => raycastData.IgnoreFriction;
private Vector2 ExternalForce => physicsData.ExternalForce;
private float MinimumMoveThreshold => physicsData.MinimumMoveThreshold;
private bool StopExternalForce => ExternalForce.magnitude <= MinimumMoveThreshold;
private async UniTask UpdateExternalForce()
{
    if (!IgnoreFriction)
    {
        await UpdateExternalForceInternal();
        if (StopExternalForce) await StopExternalForceInternal();
    }
    await Yield();
}

private async UniTask UpdateExternalForceInternal()
{
    await physicsController.OnPlatformerUpdateExternalForce();
}

private async UniTask StopExternalForceInternal()
{
    await physicsController.OnPlatformerStopExternalForce();
}

private bool ApplyGravity => Speed.y > 0;
private async UniTask UpdateGravity()
{
    if (ApplyGravity) await ApplyGravityToSpeed();
    else await ApplyExternalForceToGravity();
    await Yield();
}

private async UniTask ApplyGravityToSpeed()
{
    await physicsController.OnPlatformerApplyGravityToSpeed();
}

private async UniTask ApplyExternalForceToGravity()
{
    await physicsController.OnPlatformerApplyExternalForceToGravity();
}

private float MaximumSlopeAngle => physicsData.MaximumSlopeAngle;
private bool ApplyForcesToExternalForceX => OnSlope && GroundAngle > MaximumSlopeAngle &&
                                            (GroundAngle < MinimumWallAngle || Speed.x == 0);
private async UniTask UpdateExternalForceX()
{
    if (ApplyForcesToExternalForceX) await UpdateExternalForceXInternal();
    await Yield();
}

private async UniTask UpdateExternalForceXInternal()
{
    await physicsController.OnPlatformerUpdateExternalForceX();
}

private Vector2 DeltaMove => physicsData.DeltaMove;
private bool HorizontalDeltaMove => DeltaMove.x != 0;

private async UniTask HorizontalDeltaMoveDetection()
{
    if (HorizontalDeltaMove)
    {
        await SlopeCollision();
        await HorizontalCollision();
    }

    await Yield();
}

private bool OnSlope => raycastData.OnSlope;
private bool SlopeBehavior => DeltaMove.y <= 0 && OnSlope;
private int GroundDirectionAxis => raycastData.GroundDirectionAxis;
private int DeltaMoveXDirectionAxis => physicsData.DeltaMoveXDirectionAxis;
private bool DescendingSlope => GroundDirectionAxis == DeltaMoveXDirectionAxis;
private float GroundAngle => raycastData.GroundAngle;
private float MinimumWallAngle => physicsData.MinimumWallAngle;
private float DeltaMoveDistanceX => physicsData.DeltaMoveDistanceX;
private float SlopeMoveDistance => Sin(GroundAngle * Deg2Rad) * DeltaMoveDistanceX;
private bool ClimbingSlope => GroundAngle < MinimumWallAngle && DeltaMove.x <= SlopeMoveDistance;

private async UniTask SlopeCollision()
{
    if (SlopeBehavior)
    {
        if (DescendingSlope) await DescendSlope();
        else if (ClimbingSlope) await ClimbSlope();
    }

    await Yield();
}

private async UniTask DescendSlope()
{
    var raycast = raycastController.OnPlatformerSlopeBehavior();
    var physics = physicsController.OnPlatformerDescendSlope();
    await (raycast, physics);
}

private async UniTask ClimbSlope()
{
    var raycast = raycastController.OnPlatformerSlopeBehavior();
    var physics = physicsController.OnPlatformerClimbSlope();
    await (raycast, physics);
}

private async UniTask HorizontalCollision()
{
    await raycastController.OnPlatformerHorizontalCollision();
}

private float Tolerance => Data.Tolerance;
private Vector2 Speed => physicsData.Speed;

private bool StoppingHorizontalSpeed => OnSlope && GroundAngle >= MinimumWallAngle &&
                                        Abs(GroundAngle - DeltaMoveXDirectionAxis) > Tolerance && Speed.y < 0;

private async UniTask StopHorizontalSpeedControl()
{
    if (StoppingHorizontalSpeed) await StopHorizontalSpeed();
    await Yield();
}

private async UniTask StopHorizontalSpeed()
{
    var raycast = raycastController.OnPlatformerStopHorizontalSpeed();
    var physics = physicsController.OnPlatformerStopHorizontalSpeed();
    await (raycast, physics);
}

private async UniTask VerticalCollision()
{
    await raycastController.OnPlatformerVerticalCollision();
}

private bool OnGround => raycastData.OnGround;
private bool SlopeChanging => OnGround && DeltaMove.x != 0 && Speed.y <= 0;

private async UniTask SlopeChangeCollisionControl()
{
    if (SlopeChanging) await SlopeChangeCollision();
    await Yield();
}

private bool Climbing => DeltaMove.y > 0;

private async UniTask SlopeChangeCollision()
{
    if (Climbing) await ClimbSlopeChangeCollision();
    else await DescendSlopeChangeCollision();
    await Yield();
}

private RaycastHit2D Hit => raycastData.Hit;
private float HitAngle => raycastData.HitAngle;
private bool ClimbSteepSlopeHit => Hit && Abs(HitAngle - GroundAngle) > Tolerance;

private async UniTask ClimbSlopeChangeCollision()
{
    await ClimbSteepSlope();
    if (ClimbSteepSlopeHit) await OnClimbSteepSlopeHit();
    else await ClimbMildSlope();
}

private async UniTask OnClimbSteepSlopeHit()
{
    var physics = physicsController.OnPlatformerClimbSteepSlopeHit();
    var raycast = raycastController.OnPlatformerClimbSteepSlopeHit();
    await (physics, raycast);
}

private async UniTask ClimbSteepSlope()
{
    await raycastController.OnPlatformerClimbSteepSlope();
}

private LayerMask GroundLayer => layerMaskData.Ground;
private bool ClimbMildSlopeHit => Hit && Hit.collider.gameObject.layer == GroundLayer && HitAngle < GroundAngle;

private async UniTask ClimbMildSlope()
{
    await raycastController.OnPlatformerClimbMildSlope();
    if (ClimbMildSlopeHit) await OnClimbMildSlopeHit();
    await Yield();
}

private async UniTask OnClimbMildSlopeHit()
{
    await physicsController.OnPlatformerClimbMildSlopeHit();
}

private bool DescendMildSlopeHit => Hit && HitAngle < GroundAngle;
private bool FacingRight => physicsData.FacingRight;

private bool DescendSteepSlopeHit => Hit && Abs(Sign(Hit.normal.x) - DeltaMoveXDirectionAxis) < Tolerance &&
                                     Hit.collider.gameObject.layer == GroundLayer && HitAngle > GroundAngle &&
                                     Abs(Sign(Hit.normal.x) - (FacingRight ? 1 : -1)) < Tolerance;

private async UniTask DescendSlopeChangeCollision()
{
    await DescendMildSlope();
    if (DescendMildSlopeHit) await OnDescendMildSlopeHit();
    else await DescendSteepSlope();
    if (DescendSteepSlopeHit) await OnDescendSteepSlopeHit();
    await Yield();
}

private async UniTask DescendMildSlope()
{
    await raycastController.OnPlatformerDescendMildSlope();
}

private async UniTask OnDescendMildSlopeHit()
{
    var physics = physicsController.OnPlatformerDescendMildSlopeHit();
    var raycast = raycastController.OnPlatformerDescendMildSlopeHit();
    await (physics, raycast);
}

private async UniTask DescendSteepSlope()
{
    await raycastController.OnPlatformerDescendSteepSlope();
}

private async UniTask OnDescendSteepSlopeHit()
{
    await physicsController.OnPlatformerDescendSteepSlopeHit();
}

private async UniTask Move()
{
    await CastRayOnMove();
    await MoveCharacter();
}

private async UniTask CastRayOnMove()
{
    await raycastController.OnPlatformerCastRayOnMove();
}

private async UniTask MoveCharacter()
{
    await physicsController.OnPlatformerMoveCharacter();
}

private async UniTask OnFrameExit()
{
    await ResetJumpCollision();
    await ResetLayerMask();
    await ResetFriction();
}

private RaycastHit2D VerticalHit => raycastData.VerticalHit;
private bool CollidingBelow => raycastData.CollidingBelow;
private Vector2 TotalSpeed => physicsData.TotalSpeed;
private bool CollidingAbove => raycastData.CollidingAbove;

private bool StopForcesY => VerticalHit && CollidingBelow && TotalSpeed.y < 0 ||
                            CollidingAbove && TotalSpeed.y > 0 && (!OnSlope || GroundAngle < MinimumWallAngle);

private async UniTask ResetJumpCollision()
{
    if (StopForcesY) await physicsController.OnPlatformerResetJumpCollision();
    await Yield();
}

private async UniTask ResetLayerMask()
{
    await layerMaskController.OnPlatformerResetLayerMask();
}

private async UniTask ResetFriction()
{
    await raycastController.OnPlatformerResetFriction();
}
*/

#endregion
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
// ReSharper disable NotAccessedField.Local
namespace VFEngine.Platformer
{
    using static TimeExtensions;
    using static ScriptableObject;
    using static RaycastDirection;

    public class PlatformerController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerSettings settings;
        private PhysicsController physicsController;
        private SafetyBoxcastController safetyBoxcastController;
        private RaycastController raycastController;
        private UpRaycastController upRaycastController;
        private RightRaycastController rightRaycastController;
        private DownRaycastController downRaycastController;
        private LeftRaycastController leftRaycastController;
        private DistanceToGroundRaycastController distanceToGroundRaycastController;
        private StickyRaycastController stickyRaycastController;
        private RightStickyRaycastController rightStickyRaycastController;
        private LeftStickyRaycastController leftStickyRaycastController;
        private RaycastHitColliderController raycastHitColliderController;
        private UpRaycastHitColliderController upRaycastHitColliderController;
        private RightRaycastHitColliderController rightRaycastHitColliderController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private LeftRaycastHitColliderController leftRaycastHitColliderController;
        private StickyRaycastHitColliderController stickyRaycastHitColliderController;
        private LeftStickyRaycastHitColliderController leftStickyRaycastHitColliderController;
        private RightStickyRaycastHitColliderController rightStickyRaycastHitColliderController;
        private DistanceToGroundRaycastHitColliderController distanceToGroundRaycastHitColliderController;
        private LayerMaskController layerMaskController;
        private PlatformerData p;
        private PhysicsData physics;
        private SafetyBoxcastData safetyBoxcast;
        private RaycastData raycast;
        private UpRaycastData upRaycast;
        private DistanceToGroundRaycastData distanceToGroundRaycast;
        private StickyRaycastData stickyRaycast;
        private RightStickyRaycastData rightStickyRaycast;
        private LeftStickyRaycastData leftStickyRaycast;
        private RaycastHitColliderData raycastHitCollider;
        private UpRaycastHitColliderData upRaycastHitCollider;
        private RightRaycastHitColliderData rightRaycastHitCollider;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private LeftRaycastHitColliderData leftRaycastHitCollider;
        private LeftStickyRaycastHitColliderData leftStickyRaycastHitCollider;
        private RightStickyRaycastHitColliderData rightStickyRaycastHitCollider;
        private DistanceToGroundRaycastHitColliderData distanceToGroundRaycastHitCollider;
        private LayerMaskData layerMask;

        #endregion

        #region internal

        private static bool TimeIsActive => !TimeLteZero();
        private bool MovingRight => physics.HorizontalMovementDirection == 1;
        private bool CastingLeft => raycast.CurrentRaycastDirection == Left;
        private bool CastingRight => raycast.CurrentRaycastDirection == Right;
        private bool LeftRaycastHitWall => CastingLeft && leftRaycastHitCollider.HitWall;
        private bool RightRaycastHitWall => CastingRight && rightRaycastHitCollider.HitWall;
        private bool IsNotCollidingBelow => !(physics.Gravity > 0) || physics.IsFalling;

        #endregion

        #region private methods

        #region Initialization

        private void Awake()
        {
            InitializeData();
            SetControllers();
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            p = new PlatformerData();
            p.ApplySettings(settings);
        }

        private void SetControllers()
        {
            physicsController = GetComponent<PhysicsController>();
            safetyBoxcastController = GetComponent<SafetyBoxcastController>();
            raycastController = GetComponent<RaycastController>();
            upRaycastController = GetComponent<UpRaycastController>();
            rightRaycastController = GetComponent<RightRaycastController>();
            downRaycastController = GetComponent<DownRaycastController>();
            leftRaycastController = GetComponent<LeftRaycastController>();
            distanceToGroundRaycastController = GetComponent<DistanceToGroundRaycastController>();
            stickyRaycastController = GetComponent<StickyRaycastController>();
            rightStickyRaycastController = GetComponent<RightStickyRaycastController>();
            leftStickyRaycastController = GetComponent<LeftStickyRaycastController>();
            raycastHitColliderController = GetComponent<RaycastHitColliderController>();
            upRaycastHitColliderController = GetComponent<UpRaycastHitColliderController>();
            rightRaycastHitColliderController = GetComponent<RightRaycastHitColliderController>();
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
            leftRaycastHitColliderController = GetComponent<LeftRaycastHitColliderController>();
            stickyRaycastHitColliderController = GetComponent<StickyRaycastHitColliderController>();
            leftStickyRaycastHitColliderController = GetComponent<LeftStickyRaycastHitColliderController>();
            rightStickyRaycastHitColliderController = GetComponent<RightStickyRaycastHitColliderController>();
            distanceToGroundRaycastHitColliderController = GetComponent<DistanceToGroundRaycastHitColliderController>();
            layerMaskController = GetComponent<LayerMaskController>();
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            safetyBoxcast = safetyBoxcastController.Data;
            raycast = raycastController.Data;
            upRaycast = upRaycastController.Data;
            distanceToGroundRaycast = distanceToGroundRaycastController.Data;
            stickyRaycast = stickyRaycastController.Data;
            rightStickyRaycast = rightStickyRaycastController.Data;
            leftStickyRaycast = leftStickyRaycastController.Data;
            raycastHitCollider = raycastHitColliderController.Data;
            upRaycastHitCollider = upRaycastHitColliderController.Data;
            rightRaycastHitCollider = rightRaycastHitColliderController.Data;
            downRaycastHitCollider = downRaycastHitColliderController.Data;
            leftRaycastHitCollider = leftRaycastHitColliderController.Data;
            leftStickyRaycastHitCollider = leftStickyRaycastHitColliderController.Data;
            rightStickyRaycastHitCollider = rightStickyRaycastHitColliderController.Data;
            distanceToGroundRaycastHitCollider = distanceToGroundRaycastHitColliderController.Data;
            layerMask = layerMaskController.Data;
        }

        private void Initialize()
        {
            p.TestPlatform = TimeIsActive && downRaycastHitCollider.MovingPlatformHasSpeedOnAxis &&
                             !upRaycastHitCollider.WasTouchingCeilingLastFrame;
        }

        #endregion

        #region fixed update

        private void FixedUpdate()
        {
            RunPlatformer();
        }

        #endregion

        #region platformer

        private void RunPlatformer()
        {
            ApplyGravity();
            InitializeFrame();
            TestMovingPlatform();
            SetAppliedForces();
            SetHorizontalMovementDirection();
            CastRays();
        }

        #endregion

        #region apply gravity

        private void ApplyGravity()
        {
            physicsController.OnPlatformerApplyGravity();
        }

        #endregion

        #region initialize frame

        private void InitializeFrame()
        {
            raycastHitColliderController.OnPlatformerInitializeFrame();
            physicsController.OnPlatformerInitializeFrame();
            downRaycastHitColliderController.OnPlatformerInitializeFrame();
            upRaycastHitColliderController.OnPlatformerInitializeFrame();
            rightRaycastHitColliderController.OnPlatformerInitializeFrame();
            leftRaycastHitColliderController.OnPlatformerInitializeFrame();
            raycastController.OnPlatformerInitializeFrame();
        }

        #endregion

        #region moving platform test

        private void TestMovingPlatform()
        {
            physicsController.OnPlatformerTestMovingPlatform();
            downRaycastHitColliderController.OnPlatformerTestMovingPlatform();
            raycastController.OnPlatformerTestMovingPlatform();
        }

        #endregion

        #region set applied forces

        private void SetAppliedForces()
        {
            physicsController.OnPlatformerSetAppliedForces();
        }

        #endregion

        #region set horizontal movement direction

        private void SetHorizontalMovementDirection()
        {
            physicsController.OnPlatformerSetHorizontalMovementDirection();
        }

        #endregion

        #region cast rays

        private void CastRays()
        {
            if (raycast.CastRaysOnBothSides) CastRaysBothSides();
            else if (MovingRight) CastRaysRight();
            else CastRaysLeft();
            CastRaysDown();
        }

        private void CastRaysBothSides()
        {
            CastRaysLeft();
            CastRaysRight();
        }

        private void CastRaysLeft()
        {
            raycastController.OnPlatformerCastRaysLeft();
            CastRaysHorizontally();
        }

        private void CastRaysRight()
        {
            raycastController.OnPlatformerCastRaysRight();
            CastRaysHorizontally();
        }

        #endregion

        #region cast rays horizontally

        private void CastRaysHorizontally()
        {
            leftRaycastController.OnPlatformerCastRays();
            leftRaycastHitColliderController.OnPlatformerCastRays();
            rightRaycastController.OnPlatformerCastRays();
            rightRaycastHitColliderController.OnPlatformerCastRays();
            for (var i = 0; i < raycast.NumberOfHorizontalRaysPerSide; i++)
            {
                leftRaycastController.OnPlatformerCastCurrentRay();
                leftRaycastHitColliderController.OnPlatformerCastCurrentRay();
                rightRaycastController.OnPlatformerCastCurrentRay();
                rightRaycastHitColliderController.OnPlatformerCastCurrentRay();
                physicsController.OnPlatformerCastCurrentRay();
                if (LeftRaycastHitWall || RightRaycastHitWall) break;
                leftRaycastHitColliderController.OnPlatformerAddToCurrentHitsStorageIndex();
                rightRaycastHitColliderController.OnPlatformerAddToCurrentHitsStorageIndex();
            }
        }

        #endregion

        private bool SmallestDistanceMet => downRaycastHitCollider.SmallestDistanceMet;
        private bool StickToSlopes => physics.StickToSlopesControl;

        private void CastRaysDown()
        {
            raycastController.OnPlatformerCastRaysDown();
            layerMaskController.OnPlatformerCastRaysDown();
            downRaycastHitColliderController.OnPlatformerCastRaysDown();
            physicsController.OnPlatformerCastRaysDown();
            if (IsNotCollidingBelow) return;
            downRaycastController.OnPlatformerCastRaysDown();
            for (var i = 0; i < raycast.NumberOfVerticalRays; i++)
            {
                downRaycastController.OnPlatformerCastCurrentRay();
                downRaycastHitColliderController.OnPlatformerCastCurrentRay();
                if (SmallestDistanceMet) break;
                downRaycastHitColliderController.OnPlatformerAddToCurrentHitsStorageIndex();
            }

            downRaycastHitColliderController.OnPlatformerHitConnected();
            physicsController.OnPlatformerDownHitConnected();
            if (StickToSlopes) StickToSlope();
        }

        private void StickToSlope()
        {
            // do this
        }
        
        #endregion

        #endregion

        #region properties

        public PlatformerData Data => p;

        #endregion
    }
}

#region old code

/*

var pTask3 = Async(MoveTransform());
var pTask4 = Async(raycastController.OnSetRaysParameters());
var pTask5 = Async(SetNewSpeed());
var task2 = await (pTask3, pTask4, pTask5);
var pTask6 = Async(SetStates());
var pTask7 = Async(SetDistanceToGround());
var pTask8 = Async(physicsController.OnStopExternalForce());
var pTask9 = Async(SetStandingOnLastFrameToSavedBelowLayer());
var pTask10 = Async(physicsController.OnSetWorldSpeedToSpeed());
var task3 = await (pTask6, pTask7, pTask8, pTask9, pTask10);

private async UniTaskVoid CastRaysUp()
{
    var rTask1 = Async(upRaycastController.OnInitializeUpRaycastLength());
    var rTask2 = Async(upRaycastController.OnInitializeUpRaycastStart());
    var rTask3 = Async(upRaycastController.OnInitializeUpRaycastEnd());
    var rTask4 = Async(upRaycastController.OnInitializeUpRaycastSmallestDistance());
    var rhcTask1 = Async(upRaycastHitColliderController.OnInitializeUpHitConnected());
    var rhcTask2 = Async(upRaycastHitColliderController.OnInitializeUpHitsStorageCollidingIndex());
    var rhcTask3 = Async(upRaycastHitColliderController.OnInitializeUpHitsStorageCurrentIndex());
    var task1 = await (rTask1, rTask2, rTask3, rTask4, rhcTask1, rhcTask2, rhcTask3);
    if (upRaycastHitCollider.UpHitsStorageLength != raycast.NumberOfVerticalRaysPerSide)
        upRaycastHitColliderController.OnInitializeUpHitsStorage();
    for (var i = 0; i < raycast.NumberOfVerticalRaysPerSide; i++)
    {
        upRaycastController.OnSetCurrentUpRaycastOrigin();
        upRaycastController.OnSetCurrentUpRaycast();
        upRaycastHitColliderController.OnSetCurrentUpHitsStorage();
        upRaycastHitColliderController.OnSetRaycastUpHitAt();
        if (upRaycastHitCollider.RaycastUpHitAt)
        {
            var rhcTask4 = Async(upRaycastHitColliderController.OnSetUpHitConnected());
            var rhcTask5 = Async(upRaycastHitColliderController.OnSetUpHitsStorageCollidingIndexAt());
            var task2 = await (rhcTask4, rhcTask5);
            if (upRaycastHitCollider.RaycastUpHitAt.collider == raycastHitCollider.IgnoredCollider) break;
            if (upRaycastHitCollider.RaycastUpHitAt.distance < upRaycast.UpRaycastSmallestDistance)
                upRaycastController.OnSetUpRaycastSmallestDistanceToRaycastUpHitAt();
        }

        if (upRaycastHitCollider.UpHitConnected)
        {
            var phTask1 = Async(physicsController
                .OnSetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight());
            var rhcTask6 = Async(upRaycastHitColliderController.OnSetIsCollidingAbove());
            var task3 = await (phTask1, rhcTask6);
            if (downRaycastHitCollider.GroundedEvent && physics.NewPosition.y < 0)
                physicsController.OnStopNewVerticalPosition();
            if (!upRaycastHitCollider.WasTouchingCeilingLastFrame) physicsController.OnStopVerticalSpeed();
            physicsController.OnStopVerticalForce();
        }

        upRaycastHitColliderController.OnAddToUpHitsStorageCurrentIndex();
    }

    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid CastRaysDown()
{


    var rhcTask1 = Async(downRaycastHitColliderController.OnSetIsNotCollidingBelow());
    var phTask1 = Async(DetachFromMovingPlatform());
    var task1 = await (rhcTask1, phTask1);
    if (physics.NewPosition.y < physics.SmallValue) physicsController.OnSetIsFalling();
    else await Async(physicsController.OnSetIsNotFalling());
    if (!(physics.Gravity > 0) || physics.IsFalling)
    {
        var rhcTask2 = Async(downRaycastHitColliderController.OnInitializeFriction());
        var rhcTask3 = Async(InitializeDownHitsStorage());
        var rTask1 = Async(downRaycastController.OnInitializeDownRayLength());
        var rTask2 = Async(SetVerticalRaycast());
        var lmTask1 = Async(SetRaysBelowLayerMask());
        var task2 = await (rhcTask2, rhcTask3, rTask1, rTask2, lmTask1);
        if (downRaycastHitCollider.OnMovingPlatform) downRaycastController.OnDoubleDownRayLength();
        if (physics.NewPosition.y < 0) downRaycastController.OnSetDownRayLengthToVerticalNewPosition();
        layerMaskController.OnSetMidHeightOneWayPlatformMaskHasStandingOnLastFrameLayer();
        if (downRaycastHitCollider.HasStandingOnLastFrame)
        {
            layerMaskController.OnSetSavedBelowLayerToStandingOnLastFrameLayer();
            if (layerMask.MidHeightOneWayPlatformMaskHasStandingOnLastFrameLayer)
                downRaycastHitColliderController.OnSetStandingOnLastFrameLayerToPlatform();
        }

        if (downRaycastHitCollider.HasGroundedLastFrame && downRaycastHitCollider.HasStandingOnLastFrame)
        {
            var pTask1 = Async(ApplyToRaysBelowLayerMask(
                layerMask.MidHeightOneWayPlatformMaskHasStandingOnLastFrameLayer,
                downRaycastHitCollider.OnMovingPlatform, layerMask.StairsMask,
                downRaycastHitCollider.StandingOnLastFrame.layer, downRaycastHitCollider.StandingOnCollider,
                raycast.BoundsBottomCenterPosition, physics.NewPosition.y));
            var rTask3 = Async(downRaycastHitColliderController.OnInitializeSmallestDistanceToDownHit());
            var rhcTask4 = Async(downRaycastHitColliderController.OnInitializeDownHitsStorageIndex());
            var rhcTask5 = Async(downRaycastHitColliderController
                .OnInitializeDownHitsStorageSmallestDistanceIndex());
            var rhcTask6 = Async(downRaycastHitColliderController.OnInitializeDownHitConnected());
            var task3 = await (pTask1, rTask3, rhcTask4, rhcTask5, rhcTask6);
            
            
            

            if (downRaycastHitCollider.DownHitConnected)
            {
                var rhcTask15 = Async(downRaycastHitColliderController.OnSetStandingOn());
                var rhcTask16 = Async(downRaycastHitColliderController.OnSetStandingOnCollider());
                var task7 = await (rhcTask15, rhcTask16);
                var highEnoughForOneWayPlatform = !(downRaycastHitCollider.HasGroundedLastFrame ||
                                                    !(downRaycastHitCollider.SmallestDistanceToDownHit <
                                                      raycast.BoundsHeight / 2) ||
                                                    !layerMask.OneWayPlatformMask.Contains(
                                                        downRaycastHitCollider
                                                            .StandingOnWithSmallestDistanceLayer) &&
                                                    !layerMask.MovingOneWayPlatformMask.Contains(
                                                        downRaycastHitCollider
                                                            .StandingOnWithSmallestDistanceLayer));
                if (!highEnoughForOneWayPlatform)
                {
                    await rhcTask1;
                    return;
                }

                var phTask2 = Async(physicsController.OnSetIsNotFalling());
                var rhcTask17 = Async(downRaycastHitColliderController.OnSetIsCollidingBelow());
                var task8 = await (phTask2, rhcTask17);
                if (physics.ExternalForce.y > 0 && physics.Speed.y > 0)
                {
                    var phTask3 = Async(physicsController.OnApplySpeedToHorizontalNewPosition());
                    var task9 = await (rhcTask1, phTask3);
                }
                else
                {
                    physicsController.OnApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
                }

                if (!downRaycastHitCollider.HasGroundedLastFrame && physics.Speed.y > 0)
                    physicsController.OnApplySpeedToVerticalNewPosition();
                if (Abs(physics.NewPosition.y) < physics.SmallValue)
                    physicsController.OnStopNewVerticalPosition();
                if (downRaycastHitCollider.HasPhysicsMaterialClosestToDownHit)
                    downRaycastHitColliderController.OnSetFrictionToDownHitWithSmallestDistancesFriction();
                if (downRaycastHitCollider.HasPathMovementClosestToDownHit &&
                    downRaycastHitCollider.GroundedEvent)
                    downRaycastHitColliderController
                        .OnSetMovingPlatformToDownHitWithSmallestDistancesPathMovement();
                else await phTask1;
            }
            else
            {
                var task10 = await (rhcTask1, phTask1);
            }

            if (stickyRaycast.StickToSlopesControl) await Async(StickToSlope());
        }
    }
    else
    {
        await rhcTask1;
    }

    async UniTaskVoid DetachFromMovingPlatform()
    {
        if (downRaycastHitCollider.HasMovingPlatform)
        {
            var t1 = Async(physicsController.OnSetGravityActive());
            var t2 = Async(downRaycastHitColliderController.OnSetNotOnMovingPlatform());
            var t3 = Async(downRaycastHitColliderController.OnSetMovingPlatformToNull());
            var t4 = Async(downRaycastHitColliderController.OnStopMovingPlatformCurrentGravity());
            var t5 = Async(downRaycastHitColliderController.OnStopMovingPlatformCurrentSpeed());
            var t = await (t1, t2, t3, t4);
        }

        await SetYieldOrSwitchToThreadPoolAsync();
    }

    async UniTaskVoid ApplyToRaysBelowLayerMask(bool midHeightOneWayPlatformContains, bool onMovingPlatform,
        LayerMask stairsMask, LayerMask standingOnLastFrame, Collider2D standingOnCollider,
        Vector2 colliderBottomCenterPosition, float newPositionY)
    {
        if (!midHeightOneWayPlatformContains)
            layerMaskController.OnSetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight();
        var stairsContains = stairsMask.Contains(standingOnLastFrame);
        var colliderBoundsContains = standingOnCollider.bounds.Contains(colliderBottomCenterPosition);
        if (stairsContains && colliderBoundsContains)
            layerMaskController.OnSetRaysBelowLayerMaskPlatformsToOneWayOrStairs();
        var newPositionYGtZero = newPositionY > 0;
        if (onMovingPlatform && newPositionYGtZero)
            layerMaskController.OnSetRaysBelowLayerMaskPlatformsToOneWay();
        await SetYieldOrSwitchToThreadPoolAsync();
    }

    async UniTaskVoid InitializeDownHitsStorage()
    {
        if (downRaycastHitCollider.DownHitsStorageLength != raycast.NumberOfVerticalRaysPerSide)
            downRaycastHitColliderController.OnInitializeDownHitsStorage();
        await SetYieldOrSwitchToThreadPoolAsync();
    }

    async UniTaskVoid SetRaysBelowLayerMask()
    {
        var t1 = Async(layerMaskController.OnSetRaysBelowLayerMaskPlatforms());
        var t2 = Async(layerMaskController.OnSetRaysBelowLayerMaskPlatformsWithoutOneWay());
        var t = await (t1, t2);
        layerMaskController.OnSetRaysBelowLayerMaskPlatformsWithoutMidHeight();
        await SetYieldOrSwitchToThreadPoolAsync();
    }

    async UniTaskVoid SetVerticalRaycast()
    {
        var t1 = Async(downRaycastController.OnSetDownRaycastFromLeft());
        var t2 = Async(downRaycastController.OnSetDownRaycastToRight());
        var t = await (t1, t2);
        await SetYieldOrSwitchToThreadPoolAsync();
    }

    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid StickToSlope()
{
    var stickToSlope = !(physics.NewPosition.y >= stickyRaycast.StickToSlopesOffsetY) &&
                       !(physics.NewPosition.y <= -stickyRaycast.StickToSlopesOffsetY) && !physics.IsJumping &&
                       stickyRaycast.StickToSlopesControl && downRaycastHitCollider.HasGroundedLastFrame &&
                       !(physics.ExternalForce.y > 0) && !downRaycastHitCollider.HasMovingPlatform ||
                       !downRaycastHitCollider.HasGroundedLastFrame &&
                       downRaycastHitCollider.HasStandingOnLastFrame &&
                       layerMask.StairsMask.Contains(downRaycastHitCollider.StandingOnLastFrame.layer) &&
                       !physics.IsJumping;
    if (stickToSlope)
    {
        await Async(stickyRaycast.StickyRaycastLength == 0
            ? stickyRaycastController.OnSetStickyRaycastLength()
            : stickyRaycastController.OnSetStickyRaycastLengthToSelf());
        var srTask1 = Async(SetLeftStickyRaycastLength(leftStickyRaycast.LeftStickyRaycastLength));
        var srTask2 = Async(SetRightStickyRaycastLength(rightStickyRaycast.RightStickyRaycastLength));
        var srTask3 = Async(leftStickyRaycastController.OnSetLeftStickyRaycastOriginY());
        var srTask4 = Async(leftStickyRaycastController.OnSetLeftStickyRaycastOriginX());
        var srTask5 = Async(rightStickyRaycastController.OnSetRightStickyRaycastOriginY());
        var srTask6 = Async(rightStickyRaycastController.OnSetRightStickyRaycastOriginX());
        var srTask7 = Async(stickyRaycastController.OnSetDoNotCastFromLeft());
        var srTask8 = Async(stickyRaycastHitColliderController.OnInitializeBelowSlopeAngle());
        var task1 = await (srTask1, srTask2, srTask3, srTask4, srTask5, srTask6, srTask7, srTask8);
        var srTask9 = Async(leftStickyRaycastController.OnSetLeftStickyRaycast());
        var srTask10 = Async(rightStickyRaycastController.OnSetRightStickyRaycast());
        var task2 = await (srTask9, srTask10);
        var srTask11 = Async(leftStickyRaycastHitColliderController.OnSetBelowSlopeAngleLeft());
        var srTask12 = Async(leftStickyRaycastHitColliderController.OnSetCrossBelowSlopeAngleLeft());
        var srTask13 = Async(rightStickyRaycastHitColliderController.OnSetBelowSlopeAngleRight());
        var srTask14 = Async(rightStickyRaycastHitColliderController.OnSetCrossBelowSlopeAngleRight());
        var task3 = await (srTask11, srTask12, srTask13, srTask14);
        var srTask15 =
            Async(SetBelowSlopeAngleLeftToNegative(leftStickyRaycastHitCollider.CrossBelowSlopeAngleLeft.z));
        var srTask16 =
            Async(SetBelowSlopeAngleRightToNegative(rightStickyRaycastHitCollider.CrossBelowSlopeAngleRight.z));
        var task4 = await (srTask15, srTask16);
        stickyRaycastController.OnSetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
        var srTask17 = Async(stickyRaycastHitColliderController.OnSetBelowSlopeAngleToBelowSlopeAngleLeft());
        if (Abs(leftStickyRaycastHitCollider.BelowSlopeAngleLeft -
                rightStickyRaycastHitCollider.BelowSlopeAngleRight) < p.Tolerance)
        {
            var srTask18 = Async(stickyRaycastController.OnSetCastFromLeftWithBelowSlopeAngleLtZero());
            var task5 = await (srTask17, srTask18);
        }

        if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft == 0f &&
            rightStickyRaycastHitCollider.BelowSlopeAngleRight != 0f)
        {
            var srTask19 = Async(stickyRaycastController.OnSetCastFromLeftWithBelowSlopeAngleRightLtZero());
            var task6 = await (srTask17, srTask19);
        }

        var srTask20 = Async(stickyRaycastHitColliderController.OnSetBelowSlopeAngleToBelowSlopeAngleRight());
        if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft != 0f &&
            rightStickyRaycastHitCollider.BelowSlopeAngleRight == 0f)
        {
            var srTask21 = Async(stickyRaycastController.OnSetCastFromLeftWithBelowSlopeAngleLeftLtZero());
            var task7 = await (srTask20, srTask21);
        }

        if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft != 0f &&
            rightStickyRaycastHitCollider.BelowSlopeAngleRight != 0f)
        {
            stickyRaycastController.OnSetCastFromLeftWithLeftDistanceLtRightDistance();
            if (stickyRaycast.IsCastingLeft) await srTask17;
            else await srTask20;
        }

        var rhcTask1 = Async(downRaycastHitColliderController.OnSetIsCollidingBelow());
        if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft > 0f &&
            rightStickyRaycastHitCollider.BelowSlopeAngleRight < 0f && safetyBoxcast.PerformSafetyBoxcast)
        {
            safetyBoxcastController.OnSetSafetyBoxcastForImpassableAngle();
            if (!safetyBoxcast.SafetyBoxcastHit ||
                safetyBoxcast.SafetyBoxcastHit.collider == raycastHitCollider.IgnoredCollider) return;
            var phTask1 =
                Async(physicsController.OnApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition());
            var task9 = await (phTask1, rhcTask1);
            return;
        }

        var currentStickyRaycast = stickyRaycast.IsCastingLeft
            ? leftStickyRaycast.LeftStickyRaycastHit
            : rightStickyRaycast.RightStickyRaycastHit;
        if (currentStickyRaycast && currentStickyRaycast.collider != raycastHitCollider.IgnoredCollider)
        {
            if (currentStickyRaycast == leftStickyRaycast.LeftStickyRaycastHit)
            {
                var phTask2 = Async(physicsController.OnApplyLeftStickyRaycastToNewVerticalPosition());
                var task9 = await (phTask2, rhcTask1);
            }
            else if (currentStickyRaycast == rightStickyRaycast.RightStickyRaycastHit)
            {
                var phTask3 = Async(physicsController.OnApplyRightStickyRaycastToNewVerticalPosition());
                var task10 = await (phTask3, rhcTask1);
            }
        }
    }

    async UniTaskVoid SetBelowSlopeAngleLeftToNegative(float crossZ)
    {
        if (crossZ < 0) leftStickyRaycastHitColliderController.OnSetBelowSlopeAngleLeftToNegative();
        await SetYieldOrSwitchToThreadPoolAsync();
    }

    async UniTaskVoid SetBelowSlopeAngleRightToNegative(float crossZ)
    {
        if (crossZ < 0) rightStickyRaycastHitColliderController.OnSetBelowSlopeAngleRightToNegative();
        await SetYieldOrSwitchToThreadPoolAsync();
    }

    async UniTaskVoid SetLeftStickyRaycastLength(float leftRaycastLength)
    {
        if (leftRaycastLength == 0) leftStickyRaycastController.OnSetLeftStickyRaycastLength();
        else leftStickyRaycastController.OnSetLeftStickyRaycastLengthToStickyRaycastLength();
        await SetYieldOrSwitchToThreadPoolAsync();
    }

    async UniTaskVoid SetRightStickyRaycastLength(float rightRaycastLength)
    {
        if (rightRaycastLength == 0) rightStickyRaycastController.OnSetRightStickyRaycastLength();
        else rightStickyRaycastController.OnSetRightStickyRaycastLengthToStickyRaycastLength();
        await SetYieldOrSwitchToThreadPoolAsync();
    }

    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid MoveTransform()
{
    if (safetyBoxcast.PerformSafetyBoxcast)
    {
        safetyBoxcastController.OnSetSafetyBoxcast();
        if (safetyBoxcast.SafetyBoxcastHit &&
            Abs(safetyBoxcast.SafetyBoxcastHit.distance - physics.NewPosition.magnitude) < 0.002f)
        {
            physicsController.OnStopNewPosition();
            return;
        }
    }

    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetNewSpeed()
{
    if (deltaTime > 0) physicsController.OnSetNewSpeed();
    if (downRaycastHitCollider.GroundedEvent) physicsController.OnApplySlopeAngleSpeedFactorToHorizontalSpeed();
    if (!downRaycastHitCollider.OnMovingPlatform) physicsController.OnClampSpeedToMaxVelocity();
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetStates()
{
    if (!downRaycastHitCollider.HasGroundedLastFrame && downRaycastHitCollider.IsCollidingBelow)
        downRaycastHitColliderController.OnSetGroundedEvent();
    if (leftRaycastHitCollider.IsCollidingLeft || rightRaycastHitCollider.IsCollidingRight ||
        downRaycastHitCollider.IsCollidingBelow ||
        upRaycastHitCollider.IsCollidingAbove) physicsController.OnContactListHit();
    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetDistanceToGround()
{
    if (raycast.DistanceToGroundRayMaximumLength > 0)
    {
        distanceToGroundRaycastController.OnSetDistanceToGroundRaycastOrigin();
        var rTask1 = Async(distanceToGroundRaycastController.OnSetDistanceToGroundRaycast());
        var rTask2 = Async(distanceToGroundRaycastHitColliderController.OnSetDistanceToGroundRaycastHit());
        var rhcTask2 = Async(distanceToGroundRaycastHitColliderController.OnInitializeDistanceToGround());
        var task1 = await (rTask1, rTask2, rhcTask2);
        if (distanceToGroundRaycastHitCollider.DistanceToGroundRaycastHitConnected)
        {
            if (distanceToGroundRaycast.DistanceToGroundRaycastHit.collider ==
                raycastHitCollider.IgnoredCollider)
            {
                distanceToGroundRaycastHitColliderController.OnDecreaseDistanceToGround();
                return;
            }

            distanceToGroundRaycastHitColliderController
                .OnApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround();
        }
        else
        {
            distanceToGroundRaycastHitColliderController.OnDecreaseDistanceToGround();
        }
    }

    await SetYieldOrSwitchToThreadPoolAsync();
}

private async UniTaskVoid SetStandingOnLastFrameToSavedBelowLayer()
{
    if (downRaycastHitCollider.HasStandingOnLastFrame)
        downRaycastHitColliderController.OnSetStandingOnLastFrameLayerToSavedBelowLayer();
    await SetYieldOrSwitchToThreadPoolAsync();
}
*/

#endregion
using Cysharp.Threading.Tasks;
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
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer
{
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static PhysicsExtensions;
    using static TimeExtensions;
    using static Mathf;
    using static LayerMaskExtensions;
    using static Time;
    using static RaycastDirection;
    using static ScriptableObject;

    public class PlatformerController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
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

        #region private methods

        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
            if (p.DisplayWarningsControl) GetWarningMessages();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            p = new PlatformerData();
            p.ApplySettings(settings);
        }

        private void SetControllers()
        {
            physicsController = character.GetComponentNoAllocation<PhysicsController>();
            safetyBoxcastController = character.GetComponentNoAllocation<SafetyBoxcastController>();
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            upRaycastController = character.GetComponentNoAllocation<UpRaycastController>();
            rightRaycastController = character.GetComponentNoAllocation<RightRaycastController>();
            downRaycastController = character.GetComponentNoAllocation<DownRaycastController>();
            leftRaycastController = character.GetComponentNoAllocation<LeftRaycastController>();
            distanceToGroundRaycastController = character.GetComponentNoAllocation<DistanceToGroundRaycastController>();
            stickyRaycastController = character.GetComponentNoAllocation<StickyRaycastController>();
            rightStickyRaycastController = character.GetComponentNoAllocation<RightStickyRaycastController>();
            leftStickyRaycastController = character.GetComponentNoAllocation<LeftStickyRaycastController>();
            raycastHitColliderController = character.GetComponentNoAllocation<RaycastHitColliderController>();
            upRaycastHitColliderController = character.GetComponentNoAllocation<UpRaycastHitColliderController>();
            rightRaycastHitColliderController = character.GetComponentNoAllocation<RightRaycastHitColliderController>();
            downRaycastHitColliderController = character.GetComponentNoAllocation<DownRaycastHitColliderController>();
            leftRaycastHitColliderController = character.GetComponentNoAllocation<LeftRaycastHitColliderController>();
            stickyRaycastHitColliderController =
                character.GetComponentNoAllocation<StickyRaycastHitColliderController>();
            leftStickyRaycastHitColliderController =
                character.GetComponentNoAllocation<LeftStickyRaycastHitColliderController>();
            rightStickyRaycastHitColliderController =
                character.GetComponentNoAllocation<RightStickyRaycastHitColliderController>();
            distanceToGroundRaycastHitColliderController =
                character.GetComponentNoAllocation<DistanceToGroundRaycastHitColliderController>();
            layerMaskController = character.GetComponentNoAllocation<LayerMaskController>();
        }

        private void GetWarningMessages()
        {
            const string rc = "Raycast";
            const string ctr = "Controller";
            const string ch = "Character";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!physicsController) warningMessage += FieldParentGameObjectString($"Physics {ctr}", $"{ch}");
            if (!raycastController) warningMessage += FieldParentGameObjectString($"{rc} {ctr}", $"{ch}");
            if (!raycastHitColliderController)
                warningMessage += FieldParentGameObjectString($"Collider {ctr}", $"{ch}");
            if (!layerMaskController) warningMessage += FieldParentGameObjectString($"Layer Mask {ctr}", $"{ch}");
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldParentGameObjectString(string field, string obj)
            {
                AddWarningMessage();
                return FieldParentGameObjectMessage(field, obj);
            }

            void AddWarningMessage()
            {
                warningMessageCount++;
            }
        }

        private void Start()
        {
            SetDependencies();
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

        private async void FixedUpdate()
        {
            await Async(RunPlatformer());
        }

        private async UniTaskVoid RunPlatformer()
        {
            var pTask1 = Async(ApplyGravity());
            var pTask2 = Async(InitializeFrame());
            var task1 = await (pTask1, pTask2);
            await Async(TestMovingPlatform());
            await Async(SetHorizontalMovementDirection());
            await Async(StartRaycasts());
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
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid ApplyGravity()
        {
            physicsController.OnSetCurrentCurrentGravity();
            if (physics.Speed.y > 0) physicsController.OnApplyAscentMultiplierToCurrentGravity();
            if (physics.Speed.y < 0) physicsController.OnApplyFallMultiplierToCurrentGravity();
            if (physics.GravityActive) physicsController.OnApplyGravityToVerticalSpeed();
            if (physics.FallSlowFactor != 0) physicsController.OnApplyFallSlowFactorToVerticalSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeFrame()
        {
            if (physics.StickToSlopesControl)
            {
                var srTask1 = Async(stickyRaycastController.OnResetState());
                var srhTask1 = Async(stickyRaycastHitColliderController.OnResetState());
                var srhTask2 = Async(leftStickyRaycastHitColliderController.OnResetState());
                var srhTask3 = Async(rightStickyRaycastHitColliderController.OnResetState());
                var task1 = await (srTask1, srhTask1, srhTask2, srhTask3);
            }

            var rTask1 = Async(raycastController.SetRaysParameters());
            var rhcTask1 = Async(downRaycastHitColliderController.SetWasGroundedLastFrame());
            var rhcTask2 = Async(downRaycastHitColliderController.SetStandingOnLastFrame());
            var rhcTask3 = Async(upRaycastHitColliderController.SetWasTouchingCeilingLastFrame());
            var rhcTask4 = Async(rightRaycastHitColliderController.OnSetCurrentWallColliderNull());
            var rhcTask5 = Async(leftRaycastHitColliderController.OnSetCurrentWallColliderNull());
            var rhcTask6 = Async(raycastHitColliderController.OnResetState());
            var rhcTask7 = Async(upRaycastHitColliderController.OnResetState());
            var rhcTask8 = Async(rightRaycastHitColliderController.OnResetState());
            var rhcTask9 = Async(downRaycastHitColliderController.OnResetState());
            var rhcTask10 = Async(leftRaycastHitColliderController.OnResetState());
            var rhcTask11 = Async(distanceToGroundRaycastHitColliderController.OnResetState());
            var phTask1 = Async(physicsController.OnSetNewPosition());
            var phTask2 = Async(physicsController.OnResetState());
            var task2 = await (rTask1, rhcTask1, rhcTask2, rhcTask3, rhcTask4, rhcTask5, rhcTask6, rhcTask7, rhcTask8,
                rhcTask9, rhcTask10, rhcTask11, phTask1, phTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid TestMovingPlatform()
        {
            if (downRaycastHitCollider.HasMovingPlatform)
            {
                downRaycastHitColliderController.SetMovingPlatformCurrentSpeed();
                if (!SpeedNan(downRaycastHitCollider.MovingPlatformCurrentSpeed))
                    physicsController.OnTranslatePlatformSpeedToTransform();
                var platformTest = !TimeLteZero() || !AxisSpeedNan(downRaycastHitCollider.MovingPlatformCurrentSpeed) ||
                                   !upRaycastHitCollider.WasTouchingCeilingLastFrame;
                if (platformTest)
                {
                    var rTask1 = Async(raycastController.SetRaysParameters());
                    var rhcTask1 = Async(downRaycastHitColliderController.SetOnMovingPlatform());
                    var rhcTask2 = Async(downRaycastHitColliderController.SetMovingPlatformCurrentGravity());
                    var phTask1 = Async(physicsController.OnDisableGravity());
                    var phTask2 = Async(physicsController.OnApplyMovingPlatformSpeedToNewPosition());
                    var task1 = await (rTask1, rhcTask1, rhcTask2, phTask1, phTask2);
                    physicsController.OnStopHorizontalSpeedOnPlatformTest();
                    physicsController.OnSetForcesApplied();
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetHorizontalMovementDirection()
        {
            physicsController.OnSetHorizontalMovementDirectionToStored();
            if (physics.Speed.x < -physics.MovementDirectionThreshold ||
                physics.ExternalForce.x < -physics.MovementDirectionThreshold)
                physicsController.OnSetNegativeHorizontalMovementDirection();
            else if (physics.Speed.x > physics.MovementDirectionThreshold ||
                     physics.ExternalForce.x > physics.MovementDirectionThreshold)
                physicsController.OnSetPositiveHorizontalMovementDirection();
            if (downRaycastHitCollider.OnMovingPlatform &&
                Abs(downRaycastHitCollider.MovingPlatformCurrentSpeed.x) > Abs(physics.Speed.x))
                physicsController.OnApplyPlatformSpeedToHorizontalMovementDirection();
            physicsController.OnSetStoredHorizontalMovementDirection();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid StartRaycasts()
        {
            var rTaskUp = Async(CastRaysUp());
            var rTaskRight = Async(CastHorizontalRays(Right));
            var rTaskDown = Async(CastRaysDown());
            var rTaskLeft = Async(CastHorizontalRays(Left));
            if (raycast.CastRaysOnBothSides)
            {
                await rTaskLeft;
                await rTaskRight;
            }
            else if (physics.HorizontalMovementDirection == 1)
            {
                await rTaskRight;
            }
            else
            {
                await rTaskLeft;
            }

            await rTaskDown;
            await rTaskUp;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysUp()
        {
            var rTask1 = Async(upRaycastController.InitializeUpRaycastLength());
            var rTask2 = Async(upRaycastController.InitializeUpRaycastStart());
            var rTask3 = Async(upRaycastController.InitializeUpRaycastEnd());
            var rTask4 = Async(upRaycastController.InitializeUpRaycastSmallestDistance());
            var rhcTask1 = Async(upRaycastHitColliderController.InitializeUpHitConnected());
            var rhcTask2 = Async(upRaycastHitColliderController.InitializeUpHitsStorageCollidingIndex());
            var rhcTask3 = Async(upRaycastHitColliderController.InitializeUpHitsStorageCurrentIndex());
            var task1 = await (rTask1, rTask2, rTask3, rTask4, rhcTask1, rhcTask2, rhcTask3);
            if (upRaycastHitCollider.UpHitsStorageLength != raycast.NumberOfVerticalRaysPerSide)
                upRaycastHitColliderController.InitializeUpHitsStorage();
            for (var i = 0; i < raycast.NumberOfVerticalRaysPerSide; i++)
            {
                upRaycastController.SetCurrentUpRaycastOrigin();
                upRaycastController.SetCurrentUpRaycast();
                upRaycastHitColliderController.SetCurrentUpHitsStorage();
                upRaycastHitColliderController.SetRaycastUpHitAt();
                if (upRaycastHitCollider.RaycastUpHitAt)
                {
                    var rhcTask4 = Async(upRaycastHitColliderController.SetUpHitConnected());
                    var rhcTask5 = Async(upRaycastHitColliderController.SetUpHitsStorageCollidingIndexAt());
                    var task2 = await (rhcTask4, rhcTask5);
                    if (upRaycastHitCollider.RaycastUpHitAt.collider == raycastHitCollider.IgnoredCollider) break;
                    if (upRaycastHitCollider.RaycastUpHitAt.distance < upRaycast.UpRaycastSmallestDistance)
                        upRaycastController.SetUpRaycastSmallestDistanceToRaycastUpHitAt();
                }

                if (upRaycastHitCollider.UpHitConnected)
                {
                    var phTask1 = Async(physicsController
                        .OnSetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight());
                    var rhcTask6 = Async(upRaycastHitColliderController.SetIsCollidingAbove());
                    var task3 = await (phTask1, rhcTask6);
                    if (downRaycastHitCollider.GroundedEvent && physics.NewPosition.y < 0)
                        physicsController.OnStopNewVerticalPosition();
                    if (!upRaycastHitCollider.WasTouchingCeilingLastFrame) physicsController.OnStopVerticalSpeed();
                    physicsController.OnStopVerticalForce();
                }

                upRaycastHitColliderController.AddToUpHitsStorageCurrentIndex();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysDown()
        {
            var rhcTask1 = Async(downRaycastHitColliderController.SetIsNotCollidingBelow());
            var phTask1 = Async(DetachFromMovingPlatform());
            var task1 = await (rhcTask1, phTask1);
            if (physics.NewPosition.y < physics.SmallValue) physicsController.OnSetIsFalling();
            else await Async(physicsController.OnSetIsNotFalling());
            if (!(physics.Gravity > 0) || physics.IsFalling)
            {
                var rhcTask2 = Async(downRaycastHitColliderController.InitializeFriction());
                var rhcTask3 = Async(InitializeDownHitsStorage());
                var rTask1 = Async(downRaycastController.OnInitializeDownRayLength());
                var rTask2 = Async(SetVerticalRaycast());
                var lmTask1 = Async(SetRaysBelowLayerMask());
                var task2 = await (rhcTask2, rhcTask3, rTask1, rTask2, lmTask1);
                if (downRaycastHitCollider.OnMovingPlatform) downRaycastController.DoubleDownRayLength();
                if (physics.NewPosition.y < 0) downRaycastController.SetDownRayLengthToVerticalNewPosition();
                var midHeightOneWayPlatformMaskContains = LayerMaskContains(layerMask.MidHeightOneWayPlatformMask,
                    downRaycastHitCollider.StandingOnLastFrame.layer);
                if (downRaycastHitCollider.HasStandingOnLastFrame)
                {
                    layerMaskController.SetSavedBelowLayerToStandingOnLastFrameLayer();
                    if (midHeightOneWayPlatformMaskContains)
                        downRaycastHitColliderController.OnSetStandingOnLastFrameLayerToPlatform();
                }

                if (downRaycastHitCollider.HasGroundedLastFrame && downRaycastHitCollider.HasStandingOnLastFrame)
                {
                    var pTask1 = Async(ApplyToRaysBelowLayerMask(midHeightOneWayPlatformMaskContains,
                        downRaycastHitCollider.OnMovingPlatform, layerMask.StairsMask,
                        downRaycastHitCollider.StandingOnLastFrame.layer, downRaycastHitCollider.StandingOnCollider,
                        raycast.BoundsBottomCenterPosition, physics.NewPosition.y));
                    var rTask3 = Async(downRaycastHitColliderController.InitializeSmallestDistanceToDownHit());
                    var rhcTask4 = Async(downRaycastHitColliderController.InitializeDownHitsStorageIndex());
                    var rhcTask5 =
                        Async(downRaycastHitColliderController.InitializeDownHitsStorageSmallestDistanceIndex());
                    var rhcTask6 = Async(downRaycastHitColliderController.InitializeDownHitConnected());
                    var task3 = await (pTask1, rTask3, rhcTask4, rhcTask5, rhcTask6);
                    for (var i = 0; i < raycast.NumberOfVerticalRaysPerSide; i++)
                    {
                        downRaycastController.SetCurrentDownRaycastOriginPoint();
                        if (physics.NewPosition.y > 0 && !downRaycastHitCollider.HasGroundedLastFrame)
                            downRaycastController.SetCurrentDownRaycastToIgnoreOneWayPlatform();
                        else downRaycastController.SetCurrentDownRaycast();
                        var rhcTask7 = Async(downRaycastHitColliderController.SetCurrentDownHitsStorage());
                        var rhcTask8 = Async(downRaycastHitColliderController.SetRaycastDownHitAt());
                        var rhcTask9 = Async(downRaycastHitColliderController.SetCurrentDownHitSmallestDistance());
                        var task4 = await (rhcTask7, rhcTask8, rhcTask9);
                        if (downRaycastHitCollider.RaycastDownHitAt)
                        {
                            if (downRaycastHitCollider.RaycastDownHitAt.collider == raycastHitCollider.IgnoredCollider)
                                continue;
                            var rhcTask10 = Async(downRaycastHitColliderController.SetDownHitConnected());
                            var rhcTask11 = Async(downRaycastHitColliderController.SetBelowSlopeAngleAt());
                            var rhcTask12 = Async(downRaycastHitColliderController.SetCrossBelowSlopeAngleAt());
                            var task5 = await (rhcTask10, rhcTask11, rhcTask12);
                            if (downRaycastHitCollider.CrossBelowSlopeAngle.z < 0)
                                downRaycastHitColliderController.SetNegativeBelowSlopeAngle();
                            if (downRaycastHitCollider.RaycastDownHitAt.distance <
                                downRaycastHitCollider.SmallestDistanceToDownHit)
                            {
                                var rhcTask13 = Async(downRaycastHitColliderController.SetSmallestDistanceIndexAt());
                                var rhcTask14 =
                                    Async(downRaycastHitColliderController.SetDownHitWithSmallestDistance());
                                var rTask4 =
                                    Async(downRaycastHitColliderController.SetSmallestDistanceToDownHitDistance());
                                var task6 = await (rhcTask13, rhcTask14, rTask4);
                            }
                        }

                        if (downRaycastHitCollider.CurrentDownHitSmallestDistance < physics.SmallValue) break;
                        downRaycastHitColliderController.AddDownHitsStorageIndex();
                    }

                    if (downRaycastHitCollider.DownHitConnected)
                    {
                        var rhcTask15 = Async(downRaycastHitColliderController.SetStandingOn());
                        var rhcTask16 = Async(downRaycastHitColliderController.SetStandingOnCollider());
                        var task7 = await (rhcTask15, rhcTask16);
                        var highEnoughForOneWayPlatform =
                            !((downRaycastHitCollider.HasGroundedLastFrame ||
                               !(downRaycastHitCollider.SmallestDistanceToDownHit < raycast.BoundsHeight / 2) ||
                               !LayerMaskContains(layerMask.OneWayPlatformMask,
                                   downRaycastHitCollider.StandingOnWithSmallestDistanceLayer)) &&
                              !LayerMaskContains(layerMask.MovingOneWayPlatformMask,
                                  downRaycastHitCollider.StandingOnWithSmallestDistanceLayer));
                        if (!highEnoughForOneWayPlatform)
                        {
                            await rhcTask1;
                            return;
                        }

                        var phTask2 = Async(physicsController.OnSetIsNotFalling());
                        var rhcTask17 = Async(downRaycastHitColliderController.SetIsCollidingBelow());
                        var task8 = await (phTask2, rhcTask17);
                        if (physics.ExternalForce.y > 0 && physics.Speed.y > 0)
                        {
                            var phTask3 = Async(physicsController.OnApplySpeedToHorizontalNewPosition());
                            var task9 = await (rhcTask1, phTask3);
                        }
                        else
                        {
                            downRaycastHitColliderController.OnSetCurrentDownHitSmallestDistance();
                            physicsController.OnApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
                        }

                        if (!downRaycastHitCollider.HasGroundedLastFrame && physics.Speed.y > 0)
                            physicsController.OnApplySpeedToVerticalNewPosition();
                        if (Abs(physics.NewPosition.y) < physics.SmallValue)
                            physicsController.OnStopNewVerticalPosition();
                        if (downRaycastHitCollider.HasPhysicsMaterialClosestToDownHit)
                            downRaycastHitColliderController.SetFrictionToDownHitWithSmallestDistancesFriction();
                        if (downRaycastHitCollider.HasPathMovementClosestToDownHit &&
                            downRaycastHitCollider.GroundedEvent)
                            downRaycastHitColliderController
                                .SetMovingPlatformToDownHitWithSmallestDistancesPathMovement();
                        else await phTask1;
                    }
                    else
                    {
                        var task10 = await (rhcTask1, phTask1);
                    }

                    if (physics.StickToSlopesControl) await Async(StickToSlope());
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
                    var t2 = Async(downRaycastHitColliderController.SetNotOnMovingPlatform());
                    var t3 = Async(downRaycastHitColliderController.SetMovingPlatformToNull());
                    var t4 = Async(downRaycastHitColliderController.StopMovingPlatformCurrentGravity());
                    var t5 = Async(downRaycastHitColliderController.StopMovingPlatformCurrentSpeed());
                    var t = await (t1, t2, t3, t4);
                }

                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid ApplyToRaysBelowLayerMask(bool midHeightOneWayPlatformContains, bool onMovingPlatform,
                LayerMask stairsMask, LayerMask standingOnLastFrame, Collider2D standingOnCollider,
                Vector2 colliderBottomCenterPosition, float newPositionY)
            {
                if (!midHeightOneWayPlatformContains)
                    layerMaskController.SetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight();
                var stairsContains = LayerMaskContains(stairsMask, standingOnLastFrame);
                var colliderBoundsContains = standingOnCollider.bounds.Contains(colliderBottomCenterPosition);
                if (stairsContains && colliderBoundsContains)
                    layerMaskController.SetRaysBelowLayerMaskPlatformsToOneWayOrStairs();
                var newPositionYGtZero = newPositionY > 0;
                if (onMovingPlatform && newPositionYGtZero)
                    layerMaskController.SetRaysBelowLayerMaskPlatformsToOneWay();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid InitializeDownHitsStorage()
            {
                if (downRaycastHitCollider.DownHitsStorageLength != raycast.NumberOfVerticalRaysPerSide)
                    downRaycastHitColliderController.InitializeDownHitsStorage();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetRaysBelowLayerMask()
            {
                var t1 = Async(layerMaskController.SetRaysBelowLayerMaskPlatforms());
                var t2 = Async(layerMaskController.SetRaysBelowLayerMaskPlatformsWithoutOneWay());
                var t = await (t1, t2);
                layerMaskController.SetRaysBelowLayerMaskPlatformsWithoutMidHeight();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetVerticalRaycast()
            {
                var t1 = Async(downRaycastController.SetDownRaycastFromLeft());
                var t2 = Async(downRaycastController.SetDownRaycastToRight());
                var t = await (t1, t2);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid StickToSlope()
        {
            var stickToSlope = !(physics.NewPosition.y >= stickyRaycast.StickToSlopesOffsetY) &&
                               !(physics.NewPosition.y <= -stickyRaycast.StickToSlopesOffsetY) && !physics.IsJumping &&
                               physics.StickToSlopesControl && downRaycastHitCollider.HasGroundedLastFrame &&
                               !(physics.ExternalForce.y > 0) && !downRaycastHitCollider.HasMovingPlatform ||
                               !downRaycastHitCollider.HasGroundedLastFrame &&
                               downRaycastHitCollider.HasStandingOnLastFrame && LayerMaskContains(layerMask.StairsMask,
                                   downRaycastHitCollider.StandingOnLastFrame.layer) && !physics.IsJumping;
            if (stickToSlope)
            {
                await Async(stickyRaycast.StickyRaycastLength == 0
                    ? stickyRaycastController.SetStickyRaycastLength()
                    : stickyRaycastController.SetStickyRaycastLengthToSelf());
                var srTask1 = Async(SetLeftStickyRaycastLength(leftStickyRaycast.LeftStickyRaycastLength));
                var srTask2 = Async(SetRightStickyRaycastLength(rightStickyRaycast.RightStickyRaycastLength));
                var srTask3 = Async(leftStickyRaycastController.SetLeftStickyRaycastOriginY());
                var srTask4 = Async(leftStickyRaycastController.SetLeftStickyRaycastOriginX());
                var srTask5 = Async(rightStickyRaycastController.SetRightStickyRaycastOriginY());
                var srTask6 = Async(rightStickyRaycastController.SetRightStickyRaycastOriginX());
                var srTask7 = Async(stickyRaycastController.SetDoNotCastFromLeft());
                var srTask8 = Async(stickyRaycastHitColliderController.InitializeBelowSlopeAngle());
                var task1 = await (srTask1, srTask2, srTask3, srTask4, srTask5, srTask6, srTask7, srTask8);
                var srTask9 = Async(leftStickyRaycastController.SetLeftStickyRaycast());
                var srTask10 = Async(rightStickyRaycastController.SetRightStickyRaycast());
                var task2 = await (srTask9, srTask10);
                var srTask11 = Async(leftStickyRaycastHitColliderController.SetBelowSlopeAngleLeft());
                var srTask12 = Async(leftStickyRaycastHitColliderController.SetCrossBelowSlopeAngleLeft());
                var srTask13 = Async(rightStickyRaycastHitColliderController.SetBelowSlopeAngleRight());
                var srTask14 = Async(rightStickyRaycastHitColliderController.SetCrossBelowSlopeAngleRight());
                var task3 = await (srTask11, srTask12, srTask13, srTask14);
                var srTask15 =
                    Async(SetBelowSlopeAngleLeftToNegative(leftStickyRaycastHitCollider.CrossBelowSlopeAngleLeft.z));
                var srTask16 =
                    Async(SetBelowSlopeAngleRightToNegative(rightStickyRaycastHitCollider.CrossBelowSlopeAngleRight.z));
                var task4 = await (srTask15, srTask16);
                stickyRaycastController.SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
                var srTask17 = Async(stickyRaycastHitColliderController.SetBelowSlopeAngleToBelowSlopeAngleLeft());
                if (Abs(leftStickyRaycastHitCollider.BelowSlopeAngleLeft -
                        rightStickyRaycastHitCollider.BelowSlopeAngleRight) < p.Tolerance)
                {
                    var srTask18 = Async(stickyRaycastController.SetCastFromLeftWithBelowSlopeAngleLtZero());
                    var task5 = await (srTask17, srTask18);
                }

                if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft == 0f &&
                    rightStickyRaycastHitCollider.BelowSlopeAngleRight != 0f)
                {
                    var srTask19 = Async(stickyRaycastController.SetCastFromLeftWithBelowSlopeAngleRightLtZero());
                    var task6 = await (srTask17, srTask19);
                }

                var srTask20 = Async(stickyRaycastHitColliderController.SetBelowSlopeAngleToBelowSlopeAngleRight());
                if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft != 0f &&
                    rightStickyRaycastHitCollider.BelowSlopeAngleRight == 0f)
                {
                    var srTask21 = Async(stickyRaycastController.SetCastFromLeftWithBelowSlopeAngleLeftLtZero());
                    var task7 = await (srTask20, srTask21);
                }

                if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft != 0f &&
                    rightStickyRaycastHitCollider.BelowSlopeAngleRight != 0f)
                {
                    stickyRaycastController.SetCastFromLeftWithLeftDistanceLtRightDistance();
                    if (stickyRaycast.IsCastingLeft) await srTask17;
                    else await srTask20;
                }

                var rhcTask1 = Async(downRaycastHitColliderController.SetIsCollidingBelow());
                if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft > 0f &&
                    rightStickyRaycastHitCollider.BelowSlopeAngleRight < 0f && physics.SafetyBoxcastControl)
                {
                    safetyBoxcastController.SetSafetyBoxcastForImpassableAngle();
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
                if (crossZ < 0) leftStickyRaycastHitColliderController.SetBelowSlopeAngleLeftToNegative();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetBelowSlopeAngleRightToNegative(float crossZ)
            {
                if (crossZ < 0) rightStickyRaycastHitColliderController.SetBelowSlopeAngleRightToNegative();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetLeftStickyRaycastLength(float leftRaycastLength)
            {
                if (leftRaycastLength == 0) leftStickyRaycastController.SetLeftStickyRaycastLength();
                else leftStickyRaycastController.SetLeftStickyRaycastLengthToStickyRaycastLength();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetRightStickyRaycastLength(float rightRaycastLength)
            {
                if (rightRaycastLength == 0) rightStickyRaycastController.SetRightStickyRaycastLength();
                else rightStickyRaycastController.SetRightStickyRaycastLengthToStickyRaycastLength();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastHorizontalRays(RaycastDirection direction)
        {
            Collider2D currentHitCollider;
            var movementIsRayDirection = false;
            float currentHitAngle;
            var rTask1 = Async(SetHorizontalRaycastFromBottomOrigin(direction));
            var rTask2 = Async(SetHorizontalRaycastToTopOrigin(direction));
            var rTask3 = Async(SetHorizontalRaycastLength(direction));
            var rhcTask1 = Async(InitializeCurrentHorizontalHitsStorageIndex(direction));
            var rhcTask2 = Async(SetHorizontalHitsStorageSize(direction));
            var task1 = await (rTask1, rTask2, rTask3, rhcTask1, rhcTask2);
            for (var i = 0; i < raycast.NumberOfHorizontalRaysPerSide; i++)
            {
                var currentHitDistance = direction == Right
                    ? rightRaycastHitCollider.CurrentRightHitDistance
                    : leftRaycastHitCollider.CurrentLeftHitDistance;
                var hitConnected = direction == Right
                    ? rightRaycastHitCollider.RightHitConnected
                    : leftRaycastHitCollider.LeftHitConnected;
                SetCurrentHorizontalRaycastOrigin(direction);
                if (downRaycastHitCollider.HasGroundedLastFrame && i == 0)
                    SetCurrentHorizontalRaycastToIgnoreOneWayPlatform(direction);
                else SetCurrentHorizontalRaycast(direction);
                var rhcTask3 = Async(SetCurrentSideHitsStorage(direction));
                var rhcTask4 = Async(SetCurrentHorizontalHitDistance(direction));
                var task2 = await (rhcTask3, rhcTask4);
                SetHitConnected(currentHitDistance, direction);
                if (hitConnected)
                {
                    var rhcTask6 = Async(SetCurrentHorizontalHitCollider(direction));
                    var rhcTask7 = Async(SetCurrentHorizontalHitAngle(direction));
                    var task3 = await (rhcTask6, rhcTask7);
                    var rhcTask8 = Async(GetCurrentHitCollider(direction));
                    var rhcTask9 = Async(GetCurrentHitAngle(direction));
                    var phTask1 = Async(GetMovementIsRayDirection(physics.HorizontalMovementDirection, direction));
                    var task4 = await (rhcTask8, rhcTask9, phTask1);
                    if (currentHitCollider == raycastHitCollider.IgnoredCollider) break;
                    if (movementIsRayDirection) SetCurrentHorizontalLateralSlopeAngle(direction);
                    if (currentHitAngle > physics.MaximumSlopeAngle)
                    {
                        if (direction == Left)
                        {
                            var rhcTask10 = Async(leftRaycastHitColliderController.SetIsCollidingLeft());
                            var rhcTask11 = Async(leftRaycastHitColliderController.SetLeftDistanceToLeftCollider());
                            var task5 = await (rhcTask10, rhcTask11);
                        }
                        else
                        {
                            var rhcTask12 = Async(rightRaycastHitColliderController.SetIsCollidingRight());
                            var rhcTask13 = Async(rightRaycastHitColliderController.SetRightDistanceToRightCollider());
                            var task6 = await (rhcTask12, rhcTask13);
                        }

                        if (movementIsRayDirection)
                        {
                            var rchTask14 = Async(SetCurrentDistanceBetweenHorizontalHitAndRaycastOrigin(direction));
                            var rchTask15 = Async(SetCurrentWallCollider(direction));
                            var rchTask16 = Async(SetFailedSlopeAngle(direction));
                            var rchTask17 = Async(AddHorizontalHitToContactList(direction));
                            var phTask2 = Async(physicsController.OnStopHorizontalSpeed());
                            var task7 = await (rchTask14, rchTask15, rchTask16, rchTask17, phTask2);
                            SetNewHorizontalPosition(direction, downRaycastHitCollider.GroundedEvent, physics.Speed.y);
                        }
                    }
                }

                AddToCurrentHorizontalHitsStorageIndex(direction);
            }

            async UniTaskVoid SetCurrentDistanceBetweenHorizontalHitAndRaycastOrigin(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.SetCurrentDistanceBetweenRightHitAndRaycastOrigin();
                else leftRaycastHitColliderController.SetCurrentDistanceBetweenLeftHitAndRaycastOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid AddHorizontalHitToContactList(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.AddRightHitToContactList();
                else raycastHitColliderController.AddLeftHitToContactList();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetNewHorizontalPosition(RaycastDirection d, bool isGrounded, float speedY)
            {
                if (d == Right) physicsController.OnSetNewPositiveHorizontalPosition();
                else physicsController.OnSetNewNegativeHorizontalPosition();
                if (!isGrounded && speedY != 0) physicsController.OnStopNewHorizontalPosition();
            }

            async UniTaskVoid SetFailedSlopeAngle(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.SetRightFailedSlopeAngle();
                else leftRaycastHitColliderController.SetLeftFailedSlopeAngle();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentWallCollider(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.SetRightCurrentWallCollider();
                else leftRaycastHitColliderController.SetLeftCurrentWallCollider();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetCurrentHorizontalLateralSlopeAngle(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.SetCurrentRightLateralSlopeAngle();
                else leftRaycastHitColliderController.SetCurrentLeftLateralSlopeAngle();
            }

            async UniTaskVoid SetCurrentHorizontalHitAngle(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.SetCurrentRightHitAngle();
                else leftRaycastHitColliderController.SetCurrentLeftHitAngle();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentHorizontalHitCollider(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.SetCurrentRightHitCollider();
                else leftRaycastHitColliderController.SetCurrentLeftHitCollider();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid GetCurrentHitCollider(RaycastDirection d)
            {
                currentHitCollider = d == Right
                    ? rightRaycastHitCollider.CurrentRightHitCollider
                    : leftRaycastHitCollider.CurrentLeftHitCollider;
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid GetMovementIsRayDirection(int movementDirection, RaycastDirection d)
            {
                if (d == Right && movementDirection == 1 || d == Left && movementDirection == -1)
                    movementIsRayDirection = true;
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid GetCurrentHitAngle(RaycastDirection d)
            {
                currentHitAngle = d == Right
                    ? rightRaycastHitCollider.CurrentRightHitAngle
                    : leftRaycastHitCollider.CurrentLeftHitAngle;
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentSideHitsStorage(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.SetCurrentRightHitsStorage();
                else leftRaycastHitColliderController.SetCurrentLeftHitsStorage();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentHorizontalHitDistance(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.SetCurrentRightHitDistance();
                else leftRaycastHitColliderController.SetCurrentLeftHitDistance();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetHitConnected(float distance, RaycastDirection d)
            {
                if (distance > 0)
                {
                    if (d == Right) rightRaycastHitColliderController.SetRightRaycastHitConnected();
                    else leftRaycastHitColliderController.SetLeftRaycastHitConnected();
                }
                else
                {
                    if (d == Right) rightRaycastHitColliderController.SetRightRaycastHitMissed();
                    else leftRaycastHitColliderController.SetLeftRaycastHitMissed();
                }
            }

            void SetCurrentHorizontalRaycast(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.SetCurrentRightRaycast();
                else leftRaycastController.SetCurrentLeftRaycast();
            }

            void SetCurrentHorizontalRaycastToIgnoreOneWayPlatform(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.SetCurrentRightRaycastToIgnoreOneWayPlatform();
                else leftRaycastController.SetCurrentLeftRaycastToIgnoreOneWayPlatform();
            }

            void SetCurrentHorizontalRaycastOrigin(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.SetCurrentRightRaycastOrigin();
                else leftRaycastController.SetCurrentLeftRaycastOrigin();
            }

            void AddToCurrentHorizontalHitsStorageIndex(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.AddToCurrentRightHitsStorageIndex();
                leftRaycastHitColliderController.AddToCurrentLeftHitsStorageIndex();
            }

            async UniTaskVoid SetHorizontalHitsStorageSize(RaycastDirection d)
            {
                if (d == Right && rightRaycastHitCollider.RightHitsStorageLength !=
                    raycast.NumberOfHorizontalRaysPerSide)
                    rightRaycastHitColliderController.InitializeRightHitsStorage();
                else if (leftRaycastHitCollider.LeftHitsStorageLength != raycast.NumberOfHorizontalRaysPerSide)
                    leftRaycastHitColliderController.InitializeLeftHitsStorage();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastLength(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.InitializeRightRaycastLength();
                else leftRaycastController.InitializeLeftRaycastLength();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastToTopOrigin(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.SetRightRaycastToTopOrigin();
                else leftRaycastController.SetLeftRaycastToTopOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastFromBottomOrigin(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.SetRightRaycastFromBottomOrigin();
                else leftRaycastController.SetLeftRaycastFromBottomOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid InitializeCurrentHorizontalHitsStorageIndex(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.InitializeCurrentRightHitsStorageIndex();
                else leftRaycastHitColliderController.InitializeCurrentLeftHitsStorageIndex();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid MoveTransform()
        {
            if (physics.SafetyBoxcastControl)
            {
                safetyBoxcastController.SetSafetyBoxcast();
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
                downRaycastHitColliderController.SetGroundedEvent();
            if (leftRaycastHitCollider.IsCollidingLeft || rightRaycastHitCollider.IsCollidingRight ||
                downRaycastHitCollider.IsCollidingBelow ||
                upRaycastHitCollider.IsCollidingAbove) physicsController.OnContactListHit();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetDistanceToGround()
        {
            if (raycast.DistanceToGroundRayMaximumLength > 0)
            {
                distanceToGroundRaycastController.SetDistanceToGroundRaycastOrigin();
                var rTask1 = Async(distanceToGroundRaycastController.SetDistanceToGroundRaycast());
                var rTask2 = Async(distanceToGroundRaycastHitColliderController.SetDistanceToGroundRaycastHit());
                var rhcTask2 = Async(distanceToGroundRaycastHitColliderController.InitializeDistanceToGround());
                var task1 = await (rTask1, rTask2, rhcTask2);
                if (distanceToGroundRaycastHitCollider.DistanceToGroundRaycastHitConnected)
                {
                    if (distanceToGroundRaycast.DistanceToGroundRaycastHit.collider ==
                        raycastHitCollider.IgnoredCollider)
                    {
                        distanceToGroundRaycastHitColliderController.DecreaseDistanceToGround();
                        return;
                    }

                    distanceToGroundRaycastHitColliderController
                        .ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround();
                }
                else
                {
                    distanceToGroundRaycastHitColliderController.DecreaseDistanceToGround();
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetStandingOnLastFrameToSavedBelowLayer()
        {
            if (downRaycastHitCollider.HasStandingOnLastFrame)
                downRaycastHitColliderController.SetStandingOnLastFrameLayerToSavedBelowLayer();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion

        #region properties

        public PlatformerData Data => p;

        #endregion
    }
}
using System;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
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
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedVariable
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

    [Serializable]
    public class PlatformerModel
    {
        #region fields

        #region dependencies

        [LabelText("Platformer Settings")] [SerializeField] private PlatformerSettings settings;
        [SerializeField] private GameObject character;
        [SerializeField] private PlatformerController platformerController;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private BoxcastController boxcastController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private LayerMaskController layerMaskController;
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

        private const float Tolerance = 0;

        #region private methods

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            p = new PlatformerData();
            p.ApplySettings(settings);
            if (!platformerController && character) platformerController = character.GetComponent<PlatformerController>();
            else if (platformerController && !character) character = platformerController.Character;
            if (!physicsController) physicsController = character.GetComponent<PhysicsController>();
            if (!boxcastController) boxcastController = character.GetComponent<BoxcastController>();
            if (!raycastController) raycastController = character.GetComponent<RaycastController>();
            if (!raycastHitColliderController) raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
            if (!layerMaskController) layerMaskController = character.GetComponent<LayerMaskController>();
            if (p.DisplayWarningsControl) GetWarningMessages();
            physicsController.OnPlatformerInitializeData();
            boxcastController.OnPlatformerInitializeData();
            raycastController.OnPlatformerInitializeData();
            raycastHitColliderController.OnPlatformerInitializeData();
            layerMaskController.OnPlatformerInitializeData();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            safetyBoxcast = boxcastController.SafetyBoxcastModel.Data;
            raycast = raycastController.RaycastModel.Data;
            upRaycast = raycastController.UpRaycastModel.Data;
            distanceToGroundRaycast = raycastController.DistanceToGroundRaycastModel.Data;
            stickyRaycast = raycastController.StickyRaycastModel.Data;
            rightStickyRaycast = raycastController.RightStickyRaycastModel.Data;
            leftStickyRaycast = raycastController.LeftStickyRaycastModel.Data;
            raycastHitCollider = raycastHitColliderController.RaycastHitColliderModel.Data;
            upRaycastHitCollider = raycastHitColliderController.UpRaycastHitColliderModel.Data;
            rightRaycastHitCollider = raycastHitColliderController.RightRaycastHitColliderModel.Data;
            downRaycastHitCollider = raycastHitColliderController.DownRaycastHitColliderModel.Data;
            leftRaycastHitCollider = raycastHitColliderController.LeftRaycastHitColliderModel.Data;
            leftStickyRaycastHitCollider = raycastHitColliderController.LeftStickyRaycastHitColliderModel.Data;
            rightStickyRaycastHitCollider = raycastHitColliderController.RightStickyRaycastHitColliderModel.Data;
            distanceToGroundRaycastHitCollider = raycastHitColliderController.DistanceToGroundRaycastHitColliderModel.Data;
            layerMask = layerMaskController.LayerMaskModel.Data;
        }

        private void GetWarningMessages()
        {
            const string pl = "Platformer";
            const string rc = "Raycast";
            const string ctr = "Controller";
            const string ch = "Character";
            var platformerSettings = $"{pl} Settings";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!physicsController) warningMessage += FieldParentGameObjectString($"Physics {ctr}", $"{ch}");
            if (!boxcastController) warningMessage += FieldParentGameObjectString($"Boxcast {ctr}", $"{ch}");
            if (!raycastController) warningMessage += FieldParentGameObjectString($"{rc} {ctr}", $"{ch}");
            if (!raycastHitColliderController) warningMessage += FieldParentGameObjectString($"Collider {ctr}", $"{ch}");
            if (!layerMaskController) warningMessage += FieldParentGameObjectString($"Layer Mask {ctr}", $"{ch}");
            
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldParentGameObjectString(string field, string gameObject)
            {
                AddWarningMessage();
                return FieldParentGameObjectMessage(field, gameObject);
            }
            
            void AddWarningMessage()
            {
                warningMessageCount++;
            }
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
            var pTask4 = Async(raycastController.SetRaysParameters());
            var pTask5 = Async(SetNewSpeed());
            var task2 = await (pTask3, pTask4, pTask5);
            var pTask6 = Async(SetStates());
            var pTask7 = Async(SetDistanceToGround());
            var pTask8 = Async(physicsController.StopExternalForce());
            var pTask9 = Async(SetStandingOnLastFrameToSavedBelowLayer());
            var pTask10 = Async(physicsController.SetWorldSpeedToSpeed());
            var task3 = await (pTask6, pTask7, pTask8, pTask9, pTask10);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid ApplyGravity()
        {
            physicsController.SetCurrentGravity();
            if (physics.Speed.y > 0) physicsController.ApplyAscentMultiplierToCurrentGravity();
            if (physics.Speed.y < 0) physicsController.ApplyFallMultiplierToCurrentGravity();
            if (physics.GravityActive) physicsController.ApplyGravityToVerticalSpeed();
            if (physics.FallSlowFactor != 0) physicsController.ApplyFallSlowFactorToVerticalSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeFrame()
        {
            if (physics.StickToSlopesControl)
            {
                var srTask1 = Async(raycastController.ResetStickyRaycastState());
                var srhTask1 = Async(raycastHitColliderController.ResetStickyRaycastHitColliderState());
                var srhTask2 = Async(raycastHitColliderController.ResetLeftStickyRaycastHitColliderState());
                var srhTask3 = Async(raycastHitColliderController.ResetRightStickyRaycastHitColliderState());
                var task1 = await (srTask1, srhTask1, srhTask2, srhTask3);
            }

            var rTask1 = Async(raycastController.SetRaysParameters());
            var rhcTask1 = Async(raycastHitColliderController.SetWasGroundedLastFrame());
            var rhcTask2 = Async(raycastHitColliderController.SetStandingOnLastFrame());
            var rhcTask3 = Async(raycastHitColliderController.SetWasTouchingCeilingLastFrame());
            var rhcTask4 = Async(raycastHitColliderController.SetCurrentRightWallColliderNull());
            var rhcTask5 = Async(raycastHitColliderController.SetCurrentLeftWallColliderNull());
            var rhcTask6 = Async(raycastHitColliderController.ResetRaycastHitColliderState());
            var rhcTask7 = Async(raycastHitColliderController.ResetUpRaycastHitColliderState());
            var rhcTask8 = Async(raycastHitColliderController.ResetRightRaycastHitColliderState());
            var rhcTask9 = Async(raycastHitColliderController.ResetDownRaycastHitColliderState());
            var rhcTask10 = Async(raycastHitColliderController.ResetLeftRaycastHitColliderState());
            var rhcTask11 = Async(raycastHitColliderController.ResetDistanceToGroundRaycastHitColliderState());
            var phTask1 = Async(physicsController.SetNewPosition());
            var phTask2 = Async(physicsController.ResetState());
            var task2 = await (rTask1, rhcTask1, rhcTask2, rhcTask3, rhcTask4, rhcTask5, rhcTask6, rhcTask7, rhcTask8,
                rhcTask9, rhcTask10, rhcTask11, phTask1, phTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid TestMovingPlatform()
        {
            if (downRaycastHitCollider.HasMovingPlatform)
            {
                raycastHitColliderController.SetMovingPlatformCurrentSpeed();
                if (!SpeedNan(downRaycastHitCollider.MovingPlatformCurrentSpeed))
                    physicsController.TranslatePlatformSpeedToTransform();
                var platformTest = !TimeLteZero() || !AxisSpeedNan(downRaycastHitCollider.MovingPlatformCurrentSpeed) ||
                                   !upRaycastHitCollider.WasTouchingCeilingLastFrame;
                if (platformTest)
                {
                    var rTask1 = Async(raycastController.SetRaysParameters());
                    var rhcTask1 = Async(raycastHitColliderController.SetOnMovingPlatform());
                    var rhcTask2 = Async(raycastHitColliderController.SetMovingPlatformCurrentGravity());
                    var phTask1 = Async(physicsController.DisableGravity());
                    var phTask2 = Async(physicsController.ApplyMovingPlatformSpeedToNewPosition());
                    var task1 = await (rTask1, rhcTask1, rhcTask2, phTask1, phTask2);
                    physicsController.StopHorizontalSpeedOnPlatformTest();
                    physicsController.SetForcesApplied();
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetHorizontalMovementDirection()
        {
            physicsController.SetHorizontalMovementDirectionToStored();
            if (physics.Speed.x < -physics.MovementDirectionThreshold ||
                physics.ExternalForce.x < -physics.MovementDirectionThreshold)
                physicsController.SetNegativeHorizontalMovementDirection();
            else if (physics.Speed.x > physics.MovementDirectionThreshold ||
                     physics.ExternalForce.x > physics.MovementDirectionThreshold)
                physicsController.SetPositiveHorizontalMovementDirection();
            if (downRaycastHitCollider.OnMovingPlatform &&
                Abs(downRaycastHitCollider.MovingPlatformCurrentSpeed.x) > Abs(physics.Speed.x))
                physicsController.ApplyPlatformSpeedToHorizontalMovementDirection();
            physicsController.SetStoredHorizontalMovementDirection();
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
            var rTask1 = Async(raycastController.InitializeUpRaycastLength());
            var rTask2 = Async(raycastController.InitializeUpRaycastStart());
            var rTask3 = Async(raycastController.InitializeUpRaycastEnd());
            var rTask4 = Async(raycastController.InitializeUpRaycastSmallestDistance());
            var rhcTask1 = Async(raycastHitColliderController.InitializeUpHitConnected());
            var rhcTask2 = Async(raycastHitColliderController.InitializeUpHitsStorageCollidingIndex());
            var rhcTask3 = Async(raycastHitColliderController.InitializeUpHitsStorageCurrentIndex());
            var task1 = await (rTask1, rTask2, rTask3, rTask4, rhcTask1, rhcTask2, rhcTask3);
            if (upRaycastHitCollider.UpHitsStorageLength != raycast.NumberOfVerticalRaysPerSide)
                raycastHitColliderController.InitializeUpHitsStorage();
            for (var i = 0; i < raycast.NumberOfVerticalRaysPerSide; i++)
            {
                raycastController.SetCurrentUpRaycastOrigin();
                raycastController.SetCurrentUpRaycast();
                raycastHitColliderController.SetCurrentUpHitsStorage();
                raycastHitColliderController.SetRaycastUpHitAt();
                if (upRaycastHitCollider.RaycastUpHitAt)
                {
                    var rhcTask4 = Async(raycastHitColliderController.SetUpHitConnected());
                    var rhcTask5 = Async(raycastHitColliderController.SetUpHitsStorageCollidingIndexAt());
                    var task2 = await (rhcTask4, rhcTask5);
                    if (upRaycastHitCollider.RaycastUpHitAt.collider == raycastHitCollider.IgnoredCollider) break;
                    if (upRaycastHitCollider.RaycastUpHitAt.distance < upRaycast.UpRaycastSmallestDistance)
                        raycastController.SetUpRaycastSmallestDistanceToRaycastUpHitAt();
                }

                if (upRaycastHitCollider.UpHitConnected)
                {
                    var phTask1 = Async(physicsController
                        .SetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight());
                    var rhcTask6 = Async(raycastHitColliderController.SetIsCollidingAbove());
                    var task3 = await (phTask1, rhcTask6);
                    if (downRaycastHitCollider.GroundedEvent && physics.NewPosition.y < 0)
                        physicsController.StopNewVerticalPosition();
                    if (!upRaycastHitCollider.WasTouchingCeilingLastFrame) physicsController.StopVerticalSpeed();
                    physicsController.StopVerticalForce();
                }

                raycastHitColliderController.AddToUpHitsStorageCurrentIndex();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysDown()
        {
            var rhcTask1 = Async(raycastHitColliderController.SetIsNotCollidingBelow());
            var phTask1 = Async(DetachFromMovingPlatform());
            var task1 = await (rhcTask1, phTask1);
            if (physics.NewPosition.y < physics.SmallValue) physicsController.SetIsFalling();
            else await Async(physicsController.SetIsNotFalling());
            if (!(physics.Gravity > 0) || physics.IsFalling)
            {
                var rhcTask2 = Async(raycastHitColliderController.InitializeFriction());
                var rhcTask3 = Async(InitializeDownHitsStorage());
                var rTask1 = Async(raycastController.InitializeDownRayLength());
                var rTask2 = Async(SetVerticalRaycast());
                var lmTask1 = Async(SetRaysBelowLayerMask());
                var task2 = await (rhcTask2, rhcTask3, rTask1, rTask2, lmTask1);
                if (downRaycastHitCollider.OnMovingPlatform) raycastController.DoubleDownRayLength();
                if (physics.NewPosition.y < 0) raycastController.SetDownRayLengthToVerticalNewPosition();
                var midHeightOneWayPlatformMaskContains = LayerMaskContains(layerMask.MidHeightOneWayPlatformMask,
                    downRaycastHitCollider.StandingOnLastFrame.layer);
                if (downRaycastHitCollider.HasStandingOnLastFrame)
                {
                    layerMaskController.SetSavedBelowLayerToStandingOnLastFrameLayer();
                    if (midHeightOneWayPlatformMaskContains)
                        raycastHitColliderController.SetStandingOnLastFrameLayerToPlatforms();
                }

                if (downRaycastHitCollider.HasGroundedLastFrame && downRaycastHitCollider.HasStandingOnLastFrame)
                {
                    var pTask1 = Async(ApplyToRaysBelowLayerMask(midHeightOneWayPlatformMaskContains,
                        downRaycastHitCollider.OnMovingPlatform, layerMask.StairsMask,
                        downRaycastHitCollider.StandingOnLastFrame.layer, downRaycastHitCollider.StandingOnCollider,
                        raycast.BoundsBottomCenterPosition, physics.NewPosition.y));
                    var rTask3 = Async(raycastHitColliderController.InitializeSmallestDistanceToDownHit());
                    var rhcTask4 = Async(raycastHitColliderController.InitializeDownHitsStorageIndex());
                    var rhcTask5 = Async(raycastHitColliderController.InitializeDownHitsStorageSmallestDistanceIndex());
                    var rhcTask6 = Async(raycastHitColliderController.InitializeDownHitConnected());
                    var task3 = await (pTask1, rTask3, rhcTask4, rhcTask5, rhcTask6);
                    for (var i = 0; i < raycast.NumberOfVerticalRaysPerSide; i++)
                    {
                        raycastController.SetCurrentDownRaycastOriginPoint();
                        if (physics.NewPosition.y > 0 && !downRaycastHitCollider.HasGroundedLastFrame)
                            raycastController.SetCurrentDownRaycastToIgnoreOneWayPlatform();
                        else raycastController.SetCurrentDownRaycast();
                        var rhcTask7 = Async(raycastHitColliderController.SetCurrentDownHitsStorage());
                        var rhcTask8 = Async(raycastHitColliderController.SetRaycastDownHitAt());
                        var rhcTask9 = Async(raycastHitColliderController.SetCurrentDownHitSmallestDistance());
                        var task4 = await (rhcTask7, rhcTask8, rhcTask9);
                        if (downRaycastHitCollider.RaycastDownHitAt)
                        {
                            if (downRaycastHitCollider.RaycastDownHitAt.collider == raycastHitCollider.IgnoredCollider)
                                continue;
                            var rhcTask10 = Async(raycastHitColliderController.SetDownHitConnected());
                            var rhcTask11 = Async(raycastHitColliderController.SetBelowSlopeAngleAt());
                            var rhcTask12 = Async(raycastHitColliderController.SetCrossBelowSlopeAngleAt());
                            var task5 = await (rhcTask10, rhcTask11, rhcTask12);
                            if (downRaycastHitCollider.CrossBelowSlopeAngle.z < 0)
                                raycastHitColliderController.SetNegativeBelowSlopeAngle();
                            if (downRaycastHitCollider.RaycastDownHitAt.distance <
                                downRaycastHitCollider.SmallestDistanceToDownHit)
                            {
                                var rhcTask13 = Async(raycastHitColliderController.SetSmallestDistanceIndexAt());
                                var rhcTask14 = Async(raycastHitColliderController.SetDownHitWithSmallestDistance());
                                var rTask4 = Async(raycastHitColliderController.SetSmallestDistanceToDownHitDistance());
                                var task6 = await (rhcTask13, rhcTask14, rTask4);
                            }
                        }

                        if (downRaycastHitCollider.CurrentDownHitSmallestDistance < physics.SmallValue) break;
                        raycastHitColliderController.AddDownHitsStorageIndex();
                    }

                    if (downRaycastHitCollider.DownHitConnected)
                    {
                        var rhcTask15 = Async(raycastHitColliderController.SetStandingOn());
                        var rhcTask16 = Async(raycastHitColliderController.SetStandingOnCollider());
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

                        var phTask2 = Async(physicsController.SetIsNotFalling());
                        var rhcTask17 = Async(raycastHitColliderController.SetIsCollidingBelow());
                        var task8 = await (phTask2, rhcTask17);
                        if (physics.ExternalForce.y > 0 && physics.Speed.y > 0)
                        {
                            var phTask3 = Async(physicsController.ApplySpeedToHorizontalNewPosition());
                            var task9 = await (rhcTask1, phTask3);
                        }
                        else
                        {
                            //raycastHitColliderController.SetDistance
                            //raycastController.SetDistanceBetweenDownRaycastsAndSmallestDistancePoint();
                            physicsController.ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
                        }

                        if (!downRaycastHitCollider.HasGroundedLastFrame && physics.Speed.y > 0)
                            physicsController.ApplySpeedToVerticalNewPosition();
                        if (Abs(physics.NewPosition.y) < physics.SmallValue)
                            physicsController.StopNewVerticalPosition();
                        if (downRaycastHitCollider.HasPhysicsMaterialClosestToDownHit)
                            raycastHitColliderController.SetFrictionToDownHitWithSmallestDistancesFriction();
                        if (downRaycastHitCollider.HasPathMovementClosestToDownHit &&
                            downRaycastHitCollider.GroundedEvent)
                            raycastHitColliderController.SetMovingPlatformToDownHitWithSmallestDistancesPathMovement();
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
                    var t1 = Async(physicsController.SetGravityActive());
                    var t2 = Async(raycastHitColliderController.SetNotOnMovingPlatform());
                    var t3 = Async(raycastHitColliderController.SetMovingPlatformToNull());
                    var t4 = Async(raycastHitColliderController.StopMovingPlatformCurrentGravity());
                    var t5 = Async(raycastHitColliderController.StopMovingPlatformCurrentSpeed());
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
                    raycastHitColliderController.InitializeDownHitsStorage();
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
                var t1 = Async(raycastController.SetDownRaycastFromLeft());
                var t2 = Async(raycastController.SetDownRaycastToRight());
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
                               downRaycastHitCollider.HasStandingOnLastFrame &&
                               LayerMaskContains(layerMask.StairsMask,
                                   downRaycastHitCollider.StandingOnLastFrame.layer) &&
                               !physics.IsJumping;
            if (stickToSlope)
            {
                await Async(stickyRaycast.StickyRaycastLength == 0
                    ? raycastController.SetStickyRaycastLength()
                    : raycastController.SetStickyRaycastLengthToSelf());
                var srTask1 = Async(SetLeftStickyRaycastLength(leftStickyRaycast.LeftStickyRaycastLength));
                var srTask2 = Async(SetRightStickyRaycastLength(rightStickyRaycast.RightStickyRaycastLength));
                var srTask3 = Async(raycastController.SetLeftStickyRaycastOriginY());
                var srTask4 = Async(raycastController.SetLeftStickyRaycastOriginX());
                var srTask5 = Async(raycastController.SetRightStickyRaycastOriginY());
                var srTask6 = Async(raycastController.SetRightStickyRaycastOriginX());
                var srTask7 = Async(raycastController.SetDoNotCastFromLeft());
                var srTask8 = Async(raycastHitColliderController.InitializeBelowSlopeAngle());
                var task1 = await (srTask1, srTask2, srTask3, srTask4, srTask5, srTask6, srTask7, srTask8);
                var srTask9 = Async(raycastController.SetLeftStickyRaycast());
                var srTask10 = Async(raycastController.SetRightStickyRaycast());
                var task2 = await (srTask9, srTask10);
                var srTask11 = Async(raycastHitColliderController.SetBelowSlopeAngleLeft());
                var srTask12 = Async(raycastHitColliderController.SetCrossBelowSlopeAngleLeft());
                var srTask13 = Async(raycastHitColliderController.SetBelowSlopeAngleRight());
                var srTask14 = Async(raycastHitColliderController.SetCrossBelowSlopeAngleRight());
                var task3 = await (srTask11, srTask12, srTask13, srTask14);
                var srTask15 =
                    Async(SetBelowSlopeAngleLeftToNegative(leftStickyRaycastHitCollider.CrossBelowSlopeAngleLeft.z));
                var srTask16 =
                    Async(SetBelowSlopeAngleRightToNegative(rightStickyRaycastHitCollider.CrossBelowSlopeAngleRight.z));
                var task4 = await (srTask15, srTask16);
                raycastController.SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
                var srTask17 = Async(raycastHitColliderController.SetBelowSlopeAngleToBelowSlopeAngleLeft());
                if (Abs(leftStickyRaycastHitCollider.BelowSlopeAngleLeft -
                        rightStickyRaycastHitCollider.BelowSlopeAngleRight) < Tolerance)
                {
                    var srTask18 = Async(raycastController.SetCastFromLeftWithBelowSlopeAngleLtZero());
                    var task5 = await (srTask17, srTask18);
                }

                if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft == 0f &&
                    rightStickyRaycastHitCollider.BelowSlopeAngleRight != 0f)
                {
                    var srTask19 = Async(raycastController.SetCastFromLeftWithBelowSlopeAngleRightLtZero());
                    var task6 = await (srTask17, srTask19);
                }

                var srTask20 = Async(raycastHitColliderController.SetBelowSlopeAngleToBelowSlopeAngleRight());
                if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft != 0f &&
                    rightStickyRaycastHitCollider.BelowSlopeAngleRight == 0f)
                {
                    var srTask21 = Async(raycastController.SetCastFromLeftWithBelowSlopeAngleLeftLtZero());
                    var task7 = await (srTask20, srTask21);
                }

                if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft != 0f &&
                    rightStickyRaycastHitCollider.BelowSlopeAngleRight != 0f)
                {
                    raycastController.SetCastFromLeftWithLeftDistanceLtRightDistance();
                    if (stickyRaycast.IsCastingLeft) await srTask17;
                    else await srTask20;
                }

                var rhcTask1 = Async(raycastHitColliderController.SetIsCollidingBelow());
                if (leftStickyRaycastHitCollider.BelowSlopeAngleLeft > 0f &&
                    rightStickyRaycastHitCollider.BelowSlopeAngleRight < 0f && physics.SafetyBoxcastControl)
                {
                    boxcastController.SetSafetyBoxcastForImpassableAngle();
                    if (!safetyBoxcast.SafetyBoxcastHit ||
                        safetyBoxcast.SafetyBoxcastHit.collider == raycastHitCollider.IgnoredCollider) return;
                    var phTask1 =
                        Async(physicsController.ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition());
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
                        var phTask2 = Async(physicsController.ApplyLeftStickyRaycastToNewVerticalPosition());
                        var task9 = await (phTask2, rhcTask1);
                    }
                    else if (currentStickyRaycast == rightStickyRaycast.RightStickyRaycastHit)
                    {
                        var phTask3 = Async(physicsController.ApplyRightStickyRaycastToNewVerticalPosition());
                        var task10 = await (phTask3, rhcTask1);
                    }
                }
            }

            async UniTaskVoid SetBelowSlopeAngleLeftToNegative(float crossZ)
            {
                if (crossZ < 0) raycastHitColliderController.SetBelowSlopeAngleLeftToNegative();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetBelowSlopeAngleRightToNegative(float crossZ)
            {
                if (crossZ < 0) raycastHitColliderController.SetBelowSlopeAngleRightToNegative();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetLeftStickyRaycastLength(float leftRaycastLength)
            {
                if (leftRaycastLength == 0) raycastController.SetLeftStickyRaycastLength();
                else raycastController.SetLeftStickyRaycastLengthToStickyRaycastLength();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetRightStickyRaycastLength(float rightRaycastLength)
            {
                if (rightRaycastLength == 0) raycastController.SetRightStickyRaycastLength();
                else raycastController.SetRightStickyRaycastLengthToStickyRaycastLength();
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
                            var rhcTask10 = Async(raycastHitColliderController.SetLeftIsCollidingLeft());
                            var rhcTask11 = Async(raycastHitColliderController.SetLeftDistanceToLeftCollider());
                            var task5 = await (rhcTask10, rhcTask11);
                        }
                        else
                        {
                            var rhcTask12 = Async(raycastHitColliderController.SetRightIsCollidingRight());
                            var rhcTask13 = Async(raycastHitColliderController.SetRightDistanceToRightCollider());
                            var task6 = await (rhcTask12, rhcTask13);
                        }

                        if (movementIsRayDirection)
                        {
                            var rchTask14 = Async(SetCurrentDistanceBetweenHorizontalHitAndRaycastOrigin(direction));
                            var rchTask15 = Async(SetCurrentWallCollider(direction));
                            var rchTask16 = Async(SetFailedSlopeAngle(direction));
                            var rchTask17 = Async(AddHorizontalHitToContactList(direction));
                            var phTask2 = Async(physicsController.StopHorizontalSpeed());
                            var task7 = await (rchTask14, rchTask15, rchTask16, rchTask17, phTask2);
                            SetNewHorizontalPosition(direction, downRaycastHitCollider.GroundedEvent, physics.Speed.y);
                        }
                    }
                }

                AddToCurrentHorizontalHitsStorageIndex(direction);
            }

            async UniTaskVoid SetCurrentDistanceBetweenHorizontalHitAndRaycastOrigin(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.SetCurrentDistanceBetweenRightHitAndRaycastOrigin();
                else raycastHitColliderController.SetCurrentDistanceBetweenLeftHitAndRaycastOrigin();
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
                if (d == Right) physicsController.SetNewPositiveHorizontalPosition();
                else physicsController.SetNewNegativeHorizontalPosition();
                if (!isGrounded && speedY != 0) physicsController.StopNewHorizontalPosition();
            }

            async UniTaskVoid SetFailedSlopeAngle(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.SetRightFailedSlopeAngle();
                else raycastHitColliderController.SetLeftFailedSlopeAngle();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentWallCollider(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.SetRightCurrentWallCollider();
                else raycastHitColliderController.SetLeftCurrentWallCollider();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetCurrentHorizontalLateralSlopeAngle(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.SetCurrentRightLateralSlopeAngle();
                else raycastHitColliderController.SetCurrentLeftLateralSlopeAngle();
            }

            async UniTaskVoid SetCurrentHorizontalHitAngle(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.SetCurrentRightHitAngle();
                else raycastHitColliderController.SetCurrentLeftHitAngle();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentHorizontalHitCollider(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.SetCurrentRightHitCollider();
                else raycastHitColliderController.SetCurrentLeftHitCollider();
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
                if (d == Right) raycastHitColliderController.SetCurrentRightHitsStorage();
                else raycastHitColliderController.SetCurrentLeftHitsStorage();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentHorizontalHitDistance(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.SetCurrentRightHitDistance();
                else raycastHitColliderController.SetCurrentLeftHitDistance();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetHitConnected(float distance, RaycastDirection d)
            {
                if (distance > 0)
                {
                    if (d == Right) raycastHitColliderController.SetRightRaycastHitConnected();
                    else raycastHitColliderController.SetLeftRaycastHitConnected();
                }
                else
                {
                    if (d == Right) raycastHitColliderController.SetRightRaycastHitMissed();
                    else raycastHitColliderController.SetLeftRaycastHitMissed();
                }
            }

            void SetCurrentHorizontalRaycast(RaycastDirection d)
            {
                if (d == Right) raycastController.SetCurrentRightRaycast();
                else raycastController.SetCurrentLeftRaycast();
            }

            void SetCurrentHorizontalRaycastToIgnoreOneWayPlatform(RaycastDirection d)
            {
                if (d == Right) raycastController.SetCurrentRightRaycastToIgnoreOneWayPlatform();
                else raycastController.SetCurrentLeftRaycastToIgnoreOneWayPlatform();
            }

            void SetCurrentHorizontalRaycastOrigin(RaycastDirection d)
            {
                if (d == Right) raycastController.SetCurrentRightRaycastOrigin();
                else raycastController.SetCurrentLeftRaycastOrigin();
            }

            void AddToCurrentHorizontalHitsStorageIndex(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.AddToCurrentRightHitsStorageIndex();
                raycastHitColliderController.AddToCurrentLeftHitsStorageIndex();
            }

            async UniTaskVoid SetHorizontalHitsStorageSize(RaycastDirection d)
            {
                if (d == Right && rightRaycastHitCollider.RightHitsStorageLength !=
                    raycast.NumberOfHorizontalRaysPerSide) raycastHitColliderController.InitializeRightHitsStorage();
                else if (leftRaycastHitCollider.LeftHitsStorageLength != raycast.NumberOfHorizontalRaysPerSide)
                    raycastHitColliderController.InitializeLeftHitsStorage();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastLength(RaycastDirection d)
            {
                if (d == Right) raycastController.InitializeRightRaycastLength();
                else raycastController.InitializeLeftRaycastLength();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastToTopOrigin(RaycastDirection d)
            {
                if (d == Right) raycastController.SetRightRaycastToTopOrigin();
                else raycastController.SetLeftRaycastToTopOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastFromBottomOrigin(RaycastDirection d)
            {
                if (d == Right) raycastController.SetRightRaycastFromBottomOrigin();
                else raycastController.SetLeftRaycastFromBottomOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid InitializeCurrentHorizontalHitsStorageIndex(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.InitializeCurrentRightHitsStorageIndex();
                else raycastHitColliderController.InitializeCurrentLeftHitsStorageIndex();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid MoveTransform()
        {
            if (physics.SafetyBoxcastControl)
            {
                boxcastController.SetSafetyBoxcast();
                if (safetyBoxcast.SafetyBoxcastHit &&
                    Abs(safetyBoxcast.SafetyBoxcastHit.distance - physics.NewPosition.magnitude) < 0.002f)
                {
                    physicsController.StopNewPosition();
                    return;
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetNewSpeed()
        {
            if (deltaTime > 0) physicsController.SetNewSpeed();
            if (downRaycastHitCollider.GroundedEvent) physicsController.ApplySlopeAngleSpeedFactorToHorizontalSpeed();
            if (!downRaycastHitCollider.OnMovingPlatform) physicsController.ClampSpeedToMaxVelocity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetStates()
        {
            if (!downRaycastHitCollider.HasGroundedLastFrame && downRaycastHitCollider.IsCollidingBelow)
                raycastHitColliderController.SetGroundedEvent();
            if (leftRaycastHitCollider.IsCollidingLeft || rightRaycastHitCollider.IsCollidingRight ||
                downRaycastHitCollider.IsCollidingBelow ||
                upRaycastHitCollider.IsCollidingAbove) physicsController.OnContactListHit();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetDistanceToGround()
        {
            if (raycast.DistanceToGroundRayMaximumLength > 0)
            {
                raycastController.SetDistanceToGroundRaycastOrigin();
                var rTask1 = Async(raycastController.SetDistanceToGroundRaycast());
                var rTask2 = Async(raycastHitColliderController.SetDistanceToGroundRaycastHit());
                var rhcTask2 = Async(raycastHitColliderController.InitializeDistanceToGround());
                var task1 = await (rTask1, rTask2, rhcTask2);
                if (distanceToGroundRaycastHitCollider.DistanceToGroundRaycastHitConnected)
                {
                    if (distanceToGroundRaycast.DistanceToGroundRaycastHit.collider ==
                        raycastHitCollider.IgnoredCollider)
                    {
                        raycastHitColliderController.DecreaseDistanceToGround();
                        return;
                    }

                    raycastHitColliderController.ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround();
                }
                else
                {
                    raycastHitColliderController.DecreaseDistanceToGround();
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetStandingOnLastFrameToSavedBelowLayer()
        {
            if (downRaycastHitCollider.HasStandingOnLastFrame)
                raycastHitColliderController.SetStandingOnLastFrameLayerToSavedBelowLayer();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion

        #region properties

        public PlatformerData Data => p;

        #region public methods

        public void OnInitializeData()
        {
            InitializeData();
        }

        public void OnInitializeModel()
        {
            InitializeModel();
        }

        public void OnRunPlatformer()
        {
            Async(RunPlatformer());
        }

        #endregion

        #endregion
    }
}
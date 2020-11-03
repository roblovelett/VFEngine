using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
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

    [CreateAssetMenu(fileName = "PlatformerModel", menuName = "VFEngine/Platformer/Platformer Model", order = 0)]
    public class PlatformerModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [LabelText("Platformer Data")] [SerializeField]
        private PlatformerData p;

        private const float Tolerance = 0;

        /* fields: methods */
        private async UniTaskVoid Initialize()
        {
            GetWarningMessages();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void GetWarningMessages()
        {
            if (!p.DisplayWarnings) return;
            const string pl = "Platformer";
            const string rc = "Raycast";
            const string ctr = "Controller";
            const string ch = "Character";
            var settings = $"{pl} Settings";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!p.HasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
            if (!p.Physics) warningMessage += FieldParentGameObjectString($"Physics {ctr}", $"{ch}");
            if (!p.Raycast) warningMessage += FieldParentGameObjectString($"{rc} {ctr}", $"{ch}");
            if (!p.RaycastHitCollider)
                warningMessage += FieldParentGameObjectString($"{rc} Hit Collider {ctr}", $"{ch}");
            if (!p.LayerMask) warningMessage += FieldParentGameObjectString($"Layer Mask {ctr}", $"{ch}");
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldString(string field, string scriptableObject)
            {
                AddWarningMessage();
                return FieldMessage(field, scriptableObject);
            }

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
            var pTask4 = Async(p.Raycast.SetRaysParameters());
            var pTask5 = Async(SetNewSpeed());
            var task2 = await (pTask3, pTask4, pTask5);
            var pTask6 = Async(SetStates());
            var pTask7 = Async(SetDistanceToGround());
            var pTask8 = Async(p.Physics.StopExternalForce());
            var pTask9 = Async(SetStandingOnLastFrameToSavedBelowLayer());
            var pTask10 = Async(p.Physics.SetWorldSpeedToSpeed());
            var task3 = await (pTask6, pTask7, pTask8, pTask9, pTask10);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid ApplyGravity()
        {
            p.Physics.SetCurrentGravity();
            if (p.Speed.y > 0) p.Physics.ApplyAscentMultiplierToCurrentGravity();
            if (p.Speed.y < 0) p.Physics.ApplyFallMultiplierToCurrentGravity();
            if (p.GravityActive) p.Physics.ApplyGravityToVerticalSpeed();
            if (p.FallSlowFactor != 0) p.Physics.ApplyFallSlowFactorToVerticalSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeFrame()
        {
            if (p.PerformSafetyBoxcast) p.Boxcast.ResetSafetyBoxcastState();
            if (p.StickToSlopesControl) p.Raycast.ResetStickyRaycastState();
            var phTask1 = Async(p.Physics.SetNewPosition());
            var phTask2 = Async(p.Physics.ResetState());
            var rhcTask1 = Async(p.RaycastHitCollider.ClearContactList());
            var rhcTask2 = Async(p.RaycastHitCollider.SetWasGroundedLastFrame());
            var rhcTask3 = Async(p.RaycastHitCollider.SetStandingOnLastFrame());
            var rhcTask4 = Async(p.RaycastHitCollider.SetWasTouchingCeilingLastFrame());
            var rhcTask5 = Async(p.RaycastHitCollider.SetCurrentWallColliderNull());
            var rhcTask6 = Async(p.RaycastHitCollider.ResetState());
            var rTask1 = Async(p.Raycast.ResetDistanceToGroundRaycastState());
            var rTask2 = Async(p.Raycast.SetRaysParameters());
            var task1 = await (phTask1, phTask2, rhcTask1, rhcTask2, rhcTask3, rhcTask4, rhcTask5, rhcTask6, rTask1, rTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid TestMovingPlatform()
        {
            if (p.HasMovingPlatform)
            {
                if (!SpeedNan(p.MovingPlatformCurrentSpeed)) p.Physics.TranslatePlatformSpeedToTransform();
                var platformTest = !TimeLteZero() || !AxisSpeedNan(p.MovingPlatformCurrentSpeed) ||
                                   !p.WasTouchingCeilingLastFrame;
                if (platformTest)
                {
                    var rchTask1 = Async(p.RaycastHitCollider.SetOnMovingPlatform());
                    var rchTask2 = Async(p.RaycastHitCollider.SetMovingPlatformCurrentGravity());
                    var phTask1 = Async(p.Physics.DisableGravity());
                    var phTask2 = Async(p.Physics.ApplyMovingPlatformSpeedToNewPosition());
                    var rTask1 = Async(p.Raycast.SetRaysParameters());
                    var task1 = await (rchTask1, rchTask2, phTask1, phTask2, rTask1);
                    p.Physics.StopHorizontalSpeedOnPlatformTest();
                    p.Physics.SetForcesApplied();
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetHorizontalMovementDirection()
        {
            p.Physics.SetHorizontalMovementDirectionToStored();
            if (p.Speed.x < -p.MovementDirectionThreshold || p.ExternalForce.x < -p.MovementDirectionThreshold)
                p.Physics.SetNegativeHorizontalMovementDirection();
            else if (p.Speed.x > p.MovementDirectionThreshold || p.ExternalForce.x > p.MovementDirectionThreshold)
                p.Physics.SetPositiveHorizontalMovementDirection();
            if (p.OnMovingPlatform && Abs(p.MovingPlatformCurrentSpeed.x) > Abs(p.Speed.x))
                p.Physics.ApplyPlatformSpeedToHorizontalMovementDirection();
            p.Physics.SetStoredHorizontalMovementDirection();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid StartRaycasts()
        {
            var rTaskUp = Async(CastRaysUp());
            var rTaskRight = Async(CastHorizontalRays(Right));
            var rTaskDown = Async(CastRaysDown());
            var rTaskLeft = Async(CastHorizontalRays(Left));
            if (p.CastRaysOnBothSides)
            {
                await rTaskLeft;
                await rTaskRight;
            }
            else if (p.HorizontalMovementDirection == 1)
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
            var rTask1 = Async(p.Raycast.InitializeUpRaycastLength());
            var rTask2 = Async(p.Raycast.InitializeUpRaycastStart());
            var rTask3 = Async(p.Raycast.InitializeUpRaycastEnd());
            var rTask4 = Async(p.Raycast.InitializeUpRaycastSmallestDistance());
            var rhcTask1 = Async(p.RaycastHitCollider.InitializeUpHitConnected());
            var rhcTask2 = Async(p.RaycastHitCollider.InitializeUpHitsStorageCollidingIndex());
            var rhcTask3 = Async(p.RaycastHitCollider.InitializeUpHitsStorageCurrentIndex());
            var task1 = await (rTask1, rTask2, rTask3, rTask4, rhcTask1, rhcTask2, rhcTask3);
            if (p.UpHitsStorageLength != p.NumberOfVerticalRaysPerSide) p.RaycastHitCollider.InitializeUpHitsStorage();
            for (var i = 0; i < p.NumberOfVerticalRaysPerSide; i++)
            {
                p.Raycast.SetCurrentUpRaycastOrigin();
                p.Raycast.SetCurrentUpRaycast();
                p.RaycastHitCollider.SetCurrentUpHitsStorage();
                p.RaycastHitCollider.SetRaycastUpHitAt();
                if (p.RaycastUpHitAt)
                {
                    var rhcTask4 = Async(p.RaycastHitCollider.SetUpHitConnected());
                    var rhcTask5 = Async(p.RaycastHitCollider.SetUpHitsStorageCollidingIndexAt());
                    var task2 = await (rhcTask4, rhcTask5);
                    if (p.RaycastUpHitAt.collider == p.IgnoredCollider) break;
                    if (p.RaycastUpHitAt.distance < p.UpRaycastSmallestDistance)
                        p.Raycast.SetUpRaycastSmallestDistanceToRaycastUpHitAt();
                }

                if (p.UpHitConnected)
                {
                    var phTask1 = Async(p.Physics.SetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight());
                    var rhcTask6 = Async(p.RaycastHitCollider.SetIsCollidingAbove());
                    var task3 = await (phTask1, rhcTask6);
                    if (p.IsGrounded && p.NewPosition.y < 0) p.Physics.StopNewVerticalPosition();
                    if (!p.WasTouchingCeilingLastFrame) p.Physics.StopVerticalSpeed();
                    p.Physics.StopVerticalForce();
                }

                p.RaycastHitCollider.AddToUpHitsStorageCurrentIndex();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysDown()
        {
            var rhcTask1 = Async(p.RaycastHitCollider.SetIsNotCollidingBelow());
            var phTask1 = Async(DetachFromMovingPlatform());
            var task1 = await (rhcTask1, phTask1);
            if (p.NewPosition.y < p.SmallValue) p.Physics.SetIsFalling();
            else Async(p.Physics.SetIsNotFalling());
            if (!(p.Gravity > 0) || p.IsFalling)
            {
                var rhcTask2 = Async(p.RaycastHitCollider.InitializeFriction());
                var rhcTask3 = Async(InitializeDownHitsStorage());
                var rTask1 = Async(p.Raycast.InitializeDownRayLength());
                var rTask2 = Async(SetVerticalRaycast());
                var lmTask1 = Async(SetRaysBelowLayerMask());
                var task2 = await (rhcTask2, rhcTask3, rTask1, rTask2, lmTask1);
                if (p.OnMovingPlatform) p.Raycast.DoubleDownRayLength();
                if (p.NewPosition.y < 0) p.Raycast.SetDownRayLengthToVerticalNewPosition();
                var midHeightOneWayPlatformMaskContains =
                    LayerMaskContains(p.MidHeightOneWayPlatformMask, p.StandingOnLastFrame.layer);
                if (p.IsStandingOnLastFrame)
                {
                    p.LayerMask.SetSavedBelowLayerToStandingOnLastFrameLayer();
                    if (midHeightOneWayPlatformMaskContains)
                        p.RaycastHitCollider.SetStandingOnLastFrameLayerToPlatforms();
                }

                if (p.WasGroundedLastFrame && p.IsStandingOnLastFrame)
                {
                    var pTask1 = Async(ApplyToRaysBelowLayerMask(midHeightOneWayPlatformMaskContains,
                        p.OnMovingPlatform, p.StairsMask, p.StandingOnLastFrame.layer, p.StandingOnCollider,
                        p.ColliderBottomCenterPosition, p.NewPosition.y));
                    var rTask3 = Async(p.Raycast.InitializeSmallestDistanceToDownHit());
                    var rhcTask4 = Async(p.RaycastHitCollider.InitializeDownHitsStorageIndex());
                    var rhcTask5 = Async(p.RaycastHitCollider.InitializeDownHitsStorageSmallestDistanceIndex());
                    var rhcTask6 = Async(p.RaycastHitCollider.InitializeDownHitConnected());
                    var task3 = await (pTask1, rTask3, rhcTask4, rhcTask5, rhcTask6);
                    for (var i = 0; i < p.NumberOfVerticalRaysPerSide; i++)
                    {
                        p.Raycast.SetCurrentDownRaycastOriginPoint();
                        if (p.NewPosition.y > 0 && !p.WasGroundedLastFrame)
                            p.Raycast.SetCurrentDownRaycastToIgnoreOneWayPlatform();
                        else p.Raycast.SetCurrentDownRaycast();
                        var rhcTask7 = Async(p.RaycastHitCollider.SetCurrentDownHitsStorage());
                        var rhcTask8 = Async(p.RaycastHitCollider.SetRaycastDownHitAt());
                        var rhcTask9 = Async(p.RaycastHitCollider.SetCurrentDownHitSmallestDistance());
                        var task4 = await (rhcTask7, rhcTask8, rhcTask9);
                        if (p.RaycastDownHitAt)
                        {
                            if (p.RaycastDownHitAt.collider == p.IgnoredCollider) continue;
                            var rhcTask10 = Async(p.RaycastHitCollider.SetDownHitConnected());
                            var rhcTask11 = Async(p.RaycastHitCollider.SetBelowSlopeAngleAt());
                            var rhcTask12 = Async(p.RaycastHitCollider.SetCrossBelowSlopeAngleAt());
                            var task5 = await (rhcTask10, rhcTask11, rhcTask12);
                            if (p.CrossBelowSlopeAngle.z < 0) p.RaycastHitCollider.SetNegativeBelowSlopeAngle();
                            if (p.RaycastDownHitAt.distance < p.SmallestDistance)
                            {
                                var rhcTask13 = Async(p.RaycastHitCollider.SetSmallestDistanceIndexAt());
                                var rhcTask14 = Async(p.RaycastHitCollider.SetDownHitWithSmallestDistance());
                                var rTask4 = Async(p.Raycast.SetSmallestDistanceToDownHitDistance());
                                var task6 = await (rhcTask13, rhcTask14, rTask4);
                            }
                        }

                        if (p.CurrentDownHitSmallestDistance < p.SmallValue) break;
                        p.RaycastHitCollider.AddDownHitsStorageIndex();
                    }

                    if (p.DownHitConnected)
                    {
                        var rhcTask15 = Async(p.RaycastHitCollider.SetStandingOn());
                        var rhcTask16 = Async(p.RaycastHitCollider.SetStandingOnCollider());
                        var task7 = await (rhcTask15, rhcTask16);
                        var highEnoughForOneWayPlatform =
                            !((p.WasGroundedLastFrame || !(p.SmallestDistance < p.BoundsHeight / 2) ||
                               !LayerMaskContains(p.OneWayPlatformMask, p.StandingOnWithSmallestDistanceLayer)) &&
                              !LayerMaskContains(p.MovingOneWayPlatformMask, p.StandingOnWithSmallestDistanceLayer));
                        if (!highEnoughForOneWayPlatform)
                        {
                            await rhcTask1;
                            return;
                        }

                        var phTask2 = Async(p.Physics.SetIsNotFalling());
                        var rhcTask17 = Async(p.RaycastHitCollider.SetIsCollidingBelow());
                        var task8 = await (phTask2, rhcTask17);
                        if (p.ExternalForce.y > 0 && p.Speed.y > 0)
                        {
                            var phTask3 = Async(p.Physics.ApplySpeedToHorizontalNewPosition());
                            var task9 = await (phTask3, rhcTask17);
                        }
                        else
                        {
                            p.Raycast.SetDistanceBetweenDownRaycastsAndSmallestDistancePoint();
                            p.Physics.ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
                        }

                        if (!p.WasGroundedLastFrame && p.Speed.y > 0) p.Physics.ApplySpeedToVerticalNewPosition();
                        if (Abs(p.NewPosition.y) < p.SmallValue) p.Physics.StopNewVerticalPosition();
                        if (p.HasPhysicsMaterialDataClosestToDownHit)
                            p.RaycastHitCollider.SetFrictionToDownHitWithSmallestDistancesFriction();
                        if (p.HasPathMovementControllerClosestToDownHit && p.IsGrounded)
                            p.RaycastHitCollider.SetMovingPlatformToDownHitWithSmallestDistancesPathMovement();
                        else await phTask1;
                    }
                    else
                    {
                        var task10 = await (rhcTask1, phTask1);
                    }

                    if (p.StickToSlopesControl) await Async(StickToSlope());
                }
            }
            else
            {
                await rhcTask1;
            }

            async UniTaskVoid DetachFromMovingPlatform()
            {
                if (p.HasMovingPlatform)
                {
                    var t1 = Async(p.Physics.SetGravityActive());
                    var t2 = Async(p.RaycastHitCollider.SetNotOnMovingPlatform());
                    var t3 = Async(p.RaycastHitCollider.SetMovingPlatformToNull());
                    var t4 = Async(p.RaycastHitCollider.StopMovingPlatformCurrentGravity());
                    var t = await (t1, t2, t3, t4);
                }

                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid ApplyToRaysBelowLayerMask(bool midHeightOneWayPlatformContains, bool onMovingPlatform,
                LayerMask stairsMask, LayerMask standingOnLastFrame, Collider2D standingOnCollider,
                Vector2 colliderBottomCenterPosition, float newPositionY)
            {
                if (!midHeightOneWayPlatformContains)
                    p.LayerMask.SetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight();
                var stairsContains = LayerMaskContains(stairsMask, standingOnLastFrame);
                var colliderBoundsContains = standingOnCollider.bounds.Contains(colliderBottomCenterPosition);
                if (stairsContains && colliderBoundsContains)
                    p.LayerMask.SetRaysBelowLayerMaskPlatformsToOneWayOrStairs();
                var newPositionYGtZero = newPositionY > 0;
                if (onMovingPlatform && newPositionYGtZero) p.LayerMask.SetRaysBelowLayerMaskPlatformsToOneWay();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid InitializeDownHitsStorage()
            {
                if (p.DownHitsStorageLength != p.NumberOfVerticalRaysPerSide)
                    p.RaycastHitCollider.InitializeDownHitsStorage();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetRaysBelowLayerMask()
            {
                var t1 = Async(p.LayerMask.SetRaysBelowLayerMaskPlatforms());
                var t2 = Async(p.LayerMask.SetRaysBelowLayerMaskPlatformsWithoutOneWay());
                var t = await (t1, t2);
                p.LayerMask.SetRaysBelowLayerMaskPlatformsWithoutMidHeight();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetVerticalRaycast()
            {
                var t1 = Async(p.Raycast.SetDownRaycastFromLeft());
                var t2 = Async(p.Raycast.SetDownRaycastToRight());
                var t = await (t1, t2);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid StickToSlope()
        {
            var stickToSlope = !(p.NewPosition.y >= p.StickToSlopesOffsetY) &&
                !(p.NewPosition.y <= -p.StickToSlopesOffsetY) && !p.IsJumping && p.StickToSlopesControl &&
                p.WasGroundedLastFrame && !(p.ExternalForce.y > 0) && !p.HasMovingPlatform || !p.WasGroundedLastFrame &&
                p.IsStandingOnLastFrame && LayerMaskContains(p.StairsMask, p.StandingOnLastFrame.layer) && !p.IsJumping;
            if (stickToSlope)
            {
                Async(p.StickyRaycastLength == 0
                    ? p.Raycast.SetStickyRaycastLength()
                    : p.Raycast.SetStickyRaycastLengthToSelf());
                var srTask1 = Async(SetLeftStickyRaycastLength(p.LeftStickyRaycastLength));
                var srTask2 = Async(SetRightStickyRaycastLength(p.RightStickyRaycastLength));
                var srTask3 = Async(p.Raycast.SetLeftStickyRaycastOriginY());
                var srTask4 = Async(p.Raycast.SetLeftStickyRaycastOriginX());
                var srTask5 = Async(p.Raycast.SetRightStickyRaycastOriginY());
                var srTask6 = Async(p.Raycast.SetRightStickyRaycastOriginX());
                var srTask7 = Async(p.Raycast.SetDoNotCastFromLeft());
                var srTask8 = Async(p.Raycast.InitializeBelowSlopeAngle());
                var task1 = await (srTask1, srTask2, srTask3, srTask4, srTask5, srTask6, srTask7, srTask8);
                var srTask9 = Async(p.Raycast.SetLeftStickyRaycast());
                var srTask10 = Async(p.Raycast.SetRightStickyRaycast());
                var task2 = await (srTask9, srTask10);
                var srTask11 = Async(p.Raycast.SetBelowSlopeAngleLeft());
                var srTask12 = Async(p.Raycast.SetCrossBelowSlopeAngleLeft());
                var srTask13 = Async(p.Raycast.SetBelowSlopeAngleRight());
                var srTask14 = Async(p.Raycast.SetCrossBelowSlopeAngleRight());
                var task3 = await (srTask11, srTask12, srTask13, srTask14);
                var srTask15 = Async(SetBelowSlopeAngleLeftToNegative(p.CrossBelowSlopeAngleLeft.z));
                var srTask16 = Async(SetBelowSlopeAngleRightToNegative(p.CrossBelowSlopeAngleRight.z));
                var task4 = await (srTask15, srTask16);
                p.Raycast.SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
                var srTask17 = Async(p.Raycast.SetBelowSlopeAngleToBelowSlopeAngleLeft());
                if (Abs(p.BelowSlopeAngleLeft - p.BelowSlopeAngleRight) < Tolerance)
                {
                    var srTask18 = Async(p.Raycast.SetCastFromLeftWithBelowSlopeAngleLtZero());
                    var task5 = await (srTask17, srTask18);
                }

                if (p.BelowSlopeAngleLeft == 0f && p.BelowSlopeAngleRight != 0f)
                {
                    var srTask19 = Async(p.Raycast.SetCastFromLeftWithBelowSlopeAngleRightLtZero());
                    var task6 = await (srTask17, srTask19);
                }

                var srTask20 = Async(p.Raycast.SetBelowSlopeAngleToBelowSlopeAngleRight());
                if (p.BelowSlopeAngleLeft != 0f && p.BelowSlopeAngleRight == 0f)
                {
                    var srTask21 = Async(p.Raycast.SetCastFromLeftWithBelowSlopeAngleLeftLtZero());
                    var task7 = await (srTask20, srTask21);
                }

                if (p.BelowSlopeAngleLeft != 0f && p.BelowSlopeAngleRight != 0f)
                {
                    p.Raycast.SetCastFromLeftWithLeftDistanceLtRightDistance();
                    if (p.CastFromLeft) await srTask17;
                    else await srTask20;
                }

                var rhcTask1 = Async(p.RaycastHitCollider.SetIsCollidingBelow());
                if (p.BelowSlopeAngleLeft > 0f && p.BelowSlopeAngleRight < 0f && p.PerformSafetyBoxcast)
                {
                    var srTask22 = Async(p.Boxcast.SetSafetyBoxcastForImpassableAngle());
                    var srTask23 = Async(p.Boxcast.SetHasSafetyBoxcast());
                    var task8 = await (srTask22, srTask23);
                    if (!p.HasSafetyBoxcast || p.SafetyBoxcastCollider == p.IgnoredCollider) return;
                    var phTask1 = Async(p.Physics.ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition());
                    var task9 = await (phTask1, rhcTask1);
                    return;
                }

                var currentStickyRaycast = p.CastFromLeft ? p.LeftStickyRaycast : p.RightStickyRaycast;
                if (currentStickyRaycast && currentStickyRaycast.collider != p.IgnoredCollider)
                {
                    if (currentStickyRaycast == p.LeftStickyRaycast)
                    {
                        var phTask2 = Async(p.Physics.ApplyLeftStickyRaycastToNewVerticalPosition());
                        var task9 = await (phTask2, rhcTask1);
                    }
                    else if (currentStickyRaycast == p.RightStickyRaycast)
                    {
                        var phTask3 = Async(p.Physics.ApplyRightStickyRaycastToNewVerticalPosition());
                        var task10 = await (phTask3, rhcTask1);
                    }
                }
            }

            async UniTaskVoid SetBelowSlopeAngleLeftToNegative(float crossZ)
            {
                if (crossZ < 0) p.Raycast.SetBelowSlopeAngleLeftToNegative();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetBelowSlopeAngleRightToNegative(float crossZ)
            {
                if (crossZ < 0) p.Raycast.SetBelowSlopeAngleRightToNegative();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetLeftStickyRaycastLength(float leftRaycastLength)
            {
                if (leftRaycastLength == 0) p.Raycast.SetLeftStickyRaycastLength();
                else p.Raycast.SetLeftStickyRaycastLengthToStickyRaycastLength();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetRightStickyRaycastLength(float rightRaycastLength)
            {
                if (rightRaycastLength == 0) p.Raycast.SetRightStickyRaycastLength();
                else p.Raycast.SetRightStickyRaycastLengthToStickyRaycastLength();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastHorizontalRays(RaycastDirection direction)
        {
            Collider2D currentHitCollider;
            float currentHitDistance;
            var movementIsRayDirection = false;
            float currentHitAngle;
            var rTask1 = Async(SetHorizontalRaycastFromBottomOrigin(direction));
            var rTask2 = Async(SetHorizontalRaycastToTopOrigin(direction));
            var rTask3 = Async(SetHorizontalRaycastLength(direction));
            var rhcTask1 = Async(InitializeCurrentHorizontalHitsStorageIndex(direction));
            var rhcTask2 = Async(SetHorizontalHitsStorageSize(direction));
            var task1 = await (rTask1, rTask2, rTask3, rhcTask1, rhcTask2);
            for (var i = 0; i < p.NumberOfHorizontalRaysPerSide; i++)
            {
                SetCurrentHorizontalRaycastOrigin(direction);
                if (p.WasGroundedLastFrame && i == 0) SetCurrentHorizontalRaycastToIgnoreOneWayPlatform(direction);
                else SetCurrentHorizontalRaycast(direction);
                var rhcTask3 = Async(SetCurrentSideHitsStorage(direction));
                var rhcTask4 = Async(SetCurrentHorizontalHitDistance(direction));
                var rhcTask5 = Async(GetCurrentHorizontalHitDistance(direction));
                var task2 = await (rhcTask3, rhcTask4, rhcTask5);
                if (currentHitDistance > 0)
                {
                    var rhcTask6 = Async(SetCurrentHorizontalHitCollider(direction));
                    var rhcTask7 = Async(SetCurrentHorizontalHitAngle(direction));
                    var task3 = await (rhcTask6, rhcTask7);
                    var rhcTask8 = Async(GetCurrentHitCollider(direction));
                    var rhcTask9 = Async(GetCurrentHitAngle(direction));
                    var phTask1 = Async(GetMovementIsRayDirection(p.HorizontalMovementDirection, direction));
                    var task4 = await (rhcTask8, rhcTask9, phTask1);
                    if (currentHitCollider == p.IgnoredCollider) break;
                    if (movementIsRayDirection) SetCurrentHorizontalLateralSlopeAngle(direction);
                    if (currentHitAngle > p.MaximumSlopeAngle)
                    {
                        if (direction == Left)
                        {
                            var rhcTask10 = Async(p.RaycastHitCollider.SetLeftIsCollidingLeft());
                            var rhcTask11 = Async(p.RaycastHitCollider.SetLeftDistanceToLeftCollider());
                            var task5 = await (rhcTask10, rhcTask11);
                        }
                        else
                        {
                            var rhcTask12 = Async(p.RaycastHitCollider.SetRightIsCollidingRight());
                            var rhcTask13 = Async(p.RaycastHitCollider.SetRightDistanceToRightCollider());
                            var task6 = await (rhcTask12, rhcTask13);
                        }

                        if (movementIsRayDirection)
                        {
                            var rchTask14 = Async(SetCurrentDistanceBetweenHorizontalHitAndRaycastOrigin(direction));
                            var rchTask15 = Async(SetCurrentWallCollider(direction));
                            var rchTask16 = Async(SetFailedSlopeAngle(direction));
                            var rchTask17 = Async(AddHorizontalHitToContactList(direction));
                            var phTask2 = Async(p.Physics.StopHorizontalSpeed());
                            var task7 = await (rchTask14, rchTask15, rchTask16, rchTask17, phTask2);
                            SetNewHorizontalPosition(direction, p.IsGrounded, p.Speed.y);
                        }
                    }
                }

                AddToCurrentHorizontalHitsStorageIndex(direction);
            }

            async UniTaskVoid SetCurrentDistanceBetweenHorizontalHitAndRaycastOrigin(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.SetCurrentDistanceBetweenRightHitAndRaycastOrigin();
                else p.RaycastHitCollider.SetCurrentDistanceBetweenLeftHitAndRaycastOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid AddHorizontalHitToContactList(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.AddRightHitToContactList();
                else p.RaycastHitCollider.AddLeftHitToContactList();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetNewHorizontalPosition(RaycastDirection d, bool isGrounded, float speedY)
            {
                if (d == Right) p.Physics.SetNewPositiveHorizontalPosition();
                else p.Physics.SetNewNegativeHorizontalPosition();
                if (!isGrounded && speedY != 0) p.Physics.StopNewHorizontalPosition();
            }

            async UniTaskVoid SetFailedSlopeAngle(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.SetRightFailedSlopeAngle();
                else p.RaycastHitCollider.SetLeftFailedSlopeAngle();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentWallCollider(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.SetRightCurrentWallCollider();
                else p.RaycastHitCollider.SetLeftCurrentWallCollider();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetCurrentHorizontalLateralSlopeAngle(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.SetCurrentRightLateralSlopeAngle();
                else p.RaycastHitCollider.SetCurrentLeftLateralSlopeAngle();
            }

            async UniTaskVoid SetCurrentHorizontalHitAngle(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.SetCurrentRightHitAngle();
                else p.RaycastHitCollider.SetCurrentLeftHitAngle();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentHorizontalHitCollider(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.SetCurrentRightHitCollider();
                else p.RaycastHitCollider.SetCurrentLeftHitCollider();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid GetCurrentHitCollider(RaycastDirection d)
            {
                currentHitCollider = d == Right ? p.CurrentRightHitCollider : p.CurrentLeftHitCollider;
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
                currentHitAngle = d == Right ? p.CurrentRightHitAngle : p.CurrentLeftHitAngle;
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentSideHitsStorage(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.SetCurrentRightHitsStorage();
                else p.RaycastHitCollider.SetCurrentLeftHitsStorage();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid GetCurrentHorizontalHitDistance(RaycastDirection d)
            {
                currentHitDistance = d == Right ? p.CurrentRightHitDistance : p.CurrentLeftHitDistance;
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentHorizontalHitDistance(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.SetCurrentRightHitDistance();
                else p.RaycastHitCollider.SetCurrentLeftHitDistance();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetCurrentHorizontalRaycast(RaycastDirection d)
            {
                if (d == Right) p.Raycast.SetCurrentRightRaycast();
                else p.Raycast.SetCurrentLeftRaycast();
            }

            void SetCurrentHorizontalRaycastToIgnoreOneWayPlatform(RaycastDirection d)
            {
                if (d == Right) p.Raycast.SetCurrentRightRaycastToIgnoreOneWayPlatform();
                else p.Raycast.SetCurrentLeftRaycastToIgnoreOneWayPlatform();
            }

            void SetCurrentHorizontalRaycastOrigin(RaycastDirection d)
            {
                if (d == Right) p.Raycast.SetCurrentRightRaycastOrigin();
                else p.Raycast.SetCurrentLeftRaycastOrigin();
            }

            void AddToCurrentHorizontalHitsStorageIndex(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.AddToCurrentRightHitsStorageIndex();
                p.RaycastHitCollider.AddToCurrentLeftHitsStorageIndex();
            }

            async UniTaskVoid SetHorizontalHitsStorageSize(RaycastDirection d)
            {
                if (d == Right && p.RightHitsStorageLength != p.NumberOfHorizontalRaysPerSide)
                    p.RaycastHitCollider.InitializeRightHitsStorage();
                else if (p.LeftHitsStorageLength != p.NumberOfHorizontalRaysPerSide)
                    p.RaycastHitCollider.InitializeLeftHitsStorage();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastLength(RaycastDirection d)
            {
                if (d == Right) p.Raycast.InitializeRightRaycastLength();
                else p.Raycast.InitializeLeftRaycastLength();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastToTopOrigin(RaycastDirection d)
            {
                if (d == Right) p.Raycast.SetRightRaycastToTopOrigin();
                else p.Raycast.SetLeftRaycastToTopOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastFromBottomOrigin(RaycastDirection d)
            {
                if (d == Right) p.Raycast.SetRightRaycastFromBottomOrigin();
                else p.Raycast.SetLeftRaycastFromBottomOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid InitializeCurrentHorizontalHitsStorageIndex(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.InitializeCurrentRightHitsStorageIndex();
                else p.RaycastHitCollider.InitializeCurrentLeftHitsStorageIndex();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid MoveTransform()
        {
            if (p.PerformSafetyBoxcast)
            {
                var sbTask1 = Async(p.Boxcast.SetSafetyBoxcast());
                var sbTask2 = Async(p.Boxcast.SetHasSafetyBoxcast());
                var task1 = await (sbTask1, sbTask2);
                if (p.HasSafetyBoxcast && Abs(p.SafetyBoxcastDistance - p.NewPosition.magnitude) < 0.002f)
                {
                    p.Physics.StopNewPosition();
                    return;
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetNewSpeed()
        {
            if (deltaTime > 0) p.Physics.SetNewSpeed();
            if (p.IsGrounded) p.Physics.ApplySlopeAngleSpeedFactorToHorizontalSpeed();
            if (!p.OnMovingPlatform) p.Physics.ClampSpeedToMaxVelocity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetStates()
        {
            if (!p.WasGroundedLastFrame && p.IsCollidingBelow) p.RaycastHitCollider.SetGroundedEvent();
            if (p.IsCollidingLeft || p.IsCollidingRight || p.IsCollidingBelow || p.IsCollidingAbove)
                p.Physics.OnContactListHit();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetDistanceToGround()
        {
            if (p.DistanceToGroundRayMaximumLength > 0)
            {
                p.Raycast.SetDistanceToGroundRaycastOrigin();
                var rTask1 = Async(p.Raycast.SetDistanceToGroundRaycast());
                var rTask2 = Async(p.Raycast.SetHasDistanceToGroundRaycast());
                var rhcTask2 = Async(p.RaycastHitCollider.InitializeDistanceToGround());
                var task1 = await (rTask1, rTask2, rhcTask2);
                if (p.HasDistanceToGroundRaycast)
                {
                    if (p.DistanceToGroundRaycast.collider == p.IgnoredCollider)
                    {
                        p.RaycastHitCollider.DecreaseDistanceToGround();
                        return;
                    }

                    p.RaycastHitCollider.ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround();
                }
                else
                {
                    p.RaycastHitCollider.DecreaseDistanceToGround();
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetStandingOnLastFrameToSavedBelowLayer()
        {
            if (p.IsStandingOnLastFrame) p.RaycastHitCollider.SetStandingOnLastFrameLayerToSavedBelowLayer();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        /* properties: methods */
        public void OnInitialize()
        {
            Async(Initialize());
        }

        public void OnRunPlatformer()
        {
            Async(RunPlatformer());
        }
    }
}
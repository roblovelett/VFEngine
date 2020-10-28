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
    using static RaycastDirection;
    using static LayerMaskExtensions;

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
            // MoveTransform
            // Set Rays Params
            // Set New Speed
            // Set States
            // Set Distance To Ground
            // Reset External Force
            // On FrameExit
            // Update World Speed
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
            var phTask1 = Async(p.Physics.SetNewPosition());
            var phTask2 = Async(p.Physics.ResetState());
            var rhcTask1 = Async(p.RaycastHitCollider.ClearContactList());
            var rhcTask2 = Async(p.RaycastHitCollider.SetWasGroundedLastFrame());
            var rhcTask3 = Async(p.RaycastHitCollider.SetStandingOnLastFrame());
            var rhcTask4 = Async(p.RaycastHitCollider.SetWasTouchingCeilingLastFrame());
            var rhcTask5 = Async(p.RaycastHitCollider.SetCurrentWallColliderNull());
            var rhcTask6 = Async(p.RaycastHitCollider.ResetState());
            var rTask1 = Async(p.Raycast.SetRaysParameters());
            var task1 = await (phTask1, phTask2, rhcTask1, rhcTask2, rhcTask3, rhcTask4, rhcTask5, rhcTask6, rTask1);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid TestMovingPlatform()
        {
            if (p.IsCollidingWithMovingPlatform)
            {
                if (!SpeedNan(p.MovingPlatformCurrentSpeed)) p.Physics.TranslatePlatformSpeedToTransform();
                var platformTest = MovingPlatformTest(TimeLteZero(), AxisSpeedNan(p.MovingPlatformCurrentSpeed),
                    p.WasTouchingCeilingLastFrame);
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

        private static bool MovingPlatformTest(bool timeLteZero, bool axisSpeedNan, bool wasTouchingCeilingLastFrame)
        {
            return !timeLteZero && !axisSpeedNan && !wasTouchingCeilingLastFrame;
        }

        private async UniTaskVoid SetHorizontalMovementDirection()
        {
            p.Physics.SetHorizontalMovementDirectionToStored();
            if (p.Speed.x < -p.MovementDirectionThreshold || p.ExternalForce.x < -p.MovementDirectionThreshold)
                p.Physics.SetNegativeHorizontalMovementDirection();
            else if (p.Speed.x > p.MovementDirectionThreshold || p.ExternalForce.x > p.MovementDirectionThreshold)
                p.Physics.SetPositiveHorizontalMovementDirection();
            if (p.IsCollidingWithMovingPlatform && Abs(p.MovingPlatformCurrentSpeed.x) > Abs(p.Speed.x))
                p.Physics.ApplyPlatformSpeedToHorizontalMovementDirection();
            p.Physics.SetStoredHorizontalMovementDirection();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid StartRaycasts()
        {
            var rTask1 = Async(CastRaysUp());
            var rTask2 = Async(CastHorizontalRays(Right));
            var rTask3 = Async(CastRaysDown());
            var rTask4 = Async(CastHorizontalRays(Left));
            if (p.CastRaysOnBothSides)
            {
                var task1 = await (rTask1, rTask2, rTask3, rTask4);
            }
            else if (p.HorizontalMovementDirection == 1)
            {
                var task2 = await (rTask1, rTask2, rTask3);
            }
            else
            {
                var task3 = await (rTask1, rTask3, rTask4);
            }

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
                if (p.WasGroundedLastFrame && p.IsStandingOnLastFrameNotNull)
                {
                    bool midHeightOneWayPlatformMaskContains;
                    bool stairsMaskContains;
                    bool standingOnColliderBoundsContains;
                    bool newPositionYGtZero;
                    var pTask1 = Async(SetMidHeightOneWayPlatformMaskContains());
                    var pTask2 = Async(SetStairsMaskContains());
                    var pTask3 = Async(SetStandingOnColliderBoundsContains());
                    var pTask4 = Async(SetNewPositionYGtZero());
                    var task3 = await (pTask1, pTask2, pTask3, pTask4);
                    var pTask5 = Async(ApplyToRaysBelowLayerMask(midHeightOneWayPlatformMaskContains,
                        stairsMaskContains, standingOnColliderBoundsContains, p.OnMovingPlatform, newPositionYGtZero));
                    var rTask3 = Async(p.Raycast.InitializeSmallestDistance());
                    var rhcTask4 = Async(p.RaycastHitCollider.InitializeDownHitsStorageIndex());
                    var rhcTask5 = Async(p.RaycastHitCollider.InitializeDownHitsStorageSmallestDistanceIndex());
                    var rhcTask6 = Async(p.RaycastHitCollider.InitializeDownHitConnected());
                    var task4 = await (pTask5, rTask3, rhcTask4, rhcTask5, rhcTask6);

                    async UniTaskVoid SetMidHeightOneWayPlatformMaskContains()
                    {
                        midHeightOneWayPlatformMaskContains = LayerMaskContains(p.MidHeightOneWayPlatformMask,
                            p.StandingOnLastFrame.layer);
                        await SetYieldOrSwitchToThreadPoolAsync();
                    }

                    async UniTaskVoid SetStairsMaskContains()
                    {
                        stairsMaskContains = LayerMaskContains(p.StairsMask, p.StandingOnLastFrame.layer);
                        await SetYieldOrSwitchToThreadPoolAsync();
                    }

                    async UniTaskVoid SetStandingOnColliderBoundsContains()
                    {
                        standingOnColliderBoundsContains =
                            p.StandingOnCollider.bounds.Contains(p.ColliderBottomCenterPosition);
                        await SetYieldOrSwitchToThreadPoolAsync();
                    }

                    async UniTaskVoid SetNewPositionYGtZero()
                    {
                        newPositionYGtZero = p.NewPosition.y > 0;
                        await SetYieldOrSwitchToThreadPoolAsync();
                    }

                    for (var i = 0; i < p.NumberOfVerticalRaysPerSide; i++)
                    {
                        p.Raycast.SetCurrentDownRaycastOriginPoint();
                        if (p.NewPosition.y > 0 && !p.WasGroundedLastFrame)
                            p.Raycast.SetCurrentDownRaycastToIgnoreOneWayPlatform();
                        else p.Raycast.SetCurrentDownRaycast();
                        var rhcTask7 = Async(p.RaycastHitCollider.SetCurrentDownHitsStorage());
                        var rhcTask8 = Async(p.RaycastHitCollider.SetRaycastDownHitAt());
                        var rhcTask9 = Async(p.RaycastHitCollider.SetCurrentDownHitSmallestDistance());
                        var task5 = await (rhcTask7, rhcTask8, rhcTask9);
                        if (p.RaycastDownHitAt)
                        {
                            if (p.RaycastDownHitAt.collider == p.IgnoredCollider) continue;
                            var rhcTask10 = Async(p.RaycastHitCollider.SetDownHitConnected());
                            var rhcTask11 = Async(p.RaycastHitCollider.SetBelowSlopeAngleAt());
                            var rhcTask12 = Async(p.RaycastHitCollider.SetCrossBelowSlopeAngleAt());
                            var task6 = await (rhcTask10, rhcTask11, rhcTask12);
                            if (p.CrossBelowSlopeAngle.z < 0) p.RaycastHitCollider.SetNegativeBelowSlopeAngle();
                            if (p.RaycastDownHitAt.distance < p.SmallestDistance)
                            {
                                var rhcTask13 = Async(p.RaycastHitCollider.SetSmallestDistanceIndexAt());
                                var rhcTask14 = Async(p.RaycastHitCollider.SetDownHitWithSmallestDistance());
                                var rTask4 = Async(p.Raycast.SetSmallestDistanceToDownHitDistance());
                                var task7 = await (rhcTask13, rhcTask14, rTask4);
                            }
                        }

                        if (p.CurrentDownHitSmallestDistance < p.SmallValue) break;
                        p.RaycastHitCollider.AddDownHitsStorageIndex();
                    }

                    if (p.DownHitConnected)
                    {
                        var notHighEnoughForOneWayPlatform = SetNotHighEnoughForOneWayPlatform(p.WasGroundedLastFrame,
                            p.SmallestDistance, p.BoundsHeight, p.OneWayPlatformMask,
                            p.StandingOnWithSmallestDistanceLayer, p.MovingOneWayPlatformMask);
                        if (notHighEnoughForOneWayPlatform)
                        {
                            await rhcTask1;
                            return;
                        }

                        var phTask2 = Async(p.Physics.SetIsNotFalling());
                        var rhcTask15 = Async(p.RaycastHitCollider.SetIsCollidingBelow());
                        var task8 = await (phTask2, rhcTask15);
                        var applyingExternalForce = SetApplyingExternalForce(p.ExternalForce.y, p.Speed.y);
                        if (applyingExternalForce)
                        {
                            var phTask3 = Async(p.Physics.ApplySpeedToHorizontalNewPosition());
                            var task9 = await (phTask3, rhcTask15);
                        }
                        else
                        {
                            p.Raycast.SetDistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint();
                            p.Physics.ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
                        }

                        if (!p.WasGroundedLastFrame && p.Speed.y > 0) p.Physics.ApplySpeedToVerticalNewPosition();
                        if (Abs(p.NewPosition.y) < p.SmallValue) p.Physics.StopNewVerticalPosition();
                        if (p.HasPhysicsMaterialDataClosestToDownHit)
                            p.RaycastHitCollider.SetFrictionToDownHitWithSmallestDistancesFriction();
                        if (p.HasPathMovementControllerClosestToDownHit && p.IsGrounded)
                        {
                            var rhcTask16 = Async(p.RaycastHitCollider
                                .SetMovingPlatformToDownHitWithSmallestDistancesPathMovement());
                            var rhcTask17 = Async(p.RaycastHitCollider.SetHasMovingPlatform());
                            var task10 = await (rhcTask16, rhcTask17);
                        }
                        else
                        {
                            await phTask1;
                        }
                    }
                    else
                    {
                        var task11 = await (rhcTask1, phTask1);
                    }

                    if (p.StickToSlopesControl) await Async(StickToSlope());
                }
            }
            else
            {
                await rhcTask1;
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid StickToSlope()
        {
            var stickToSlope = SetStickToSlope(p.NewPosition.y, p.StickToSlopesOffsetY, p.IsJumping,
                p.StickToSlopesControl, p.WasGroundedLastFrame, p.ExternalForce.y, p.HasMovingPlatform,
                p.IsStandingOnLastFrameNotNull, p.StairsMask, p.StandingOnLastFrame.layer);
            if (stickToSlope)
            {
                SetStickyRaycastLength(p.StickyRaycastLength);
                var srTask1 = Async(SetLeftStickyRaycastLength(p.LeftStickyRaycastLength));
                var srTask2 = Async(SetRightStickyRaycastLength(p.RightStickyRaycastLength));
                var srTask3 = Async(p.StickyRaycast.SetLeftStickyRaycastOriginY());
                var srTask4 = Async(p.StickyRaycast.SetLeftStickyRaycastOriginX());
                var srTask5 = Async(p.StickyRaycast.SetRightStickyRaycastOriginY());
                var srTask6 = Async(p.StickyRaycast.SetRightStickyRaycastOriginX());
                var srTask7 = Async(p.StickyRaycast.SetDoNotCastFromLeft());
                var srTask8 = Async(p.StickyRaycast.InitializeBelowSlopeAngle());
                var task1 = await (srTask1, srTask2, srTask3, srTask4, srTask5, srTask6, srTask7, srTask8);
                var srTask9 = Async(p.StickyRaycast.SetLeftStickyRaycast());
                var srTask10 = Async(p.StickyRaycast.SetRightStickyRaycast());
                var task2 = await (srTask9, srTask10);
                var srTask11 = Async(p.StickyRaycast.SetBelowSlopeAngleLeft());
                var srTask12 = Async(p.StickyRaycast.SetCrossBelowSlopeAngleLeft());
                var srTask13 = Async(p.StickyRaycast.SetBelowSlopeAngleRight());
                var srTask14 = Async(p.StickyRaycast.SetCrossBelowSlopeAngleRight());
                var task3 = await (srTask11, srTask12, srTask13, srTask14);
                var srTask15 = Async(SetBelowSlopeAngleLeftToNegative(p.CrossBelowSlopeAngleLeft.z));
                var srTask16 = Async(SetBelowSlopeAngleRightToNegative(p.CrossBelowSlopeAngleRight.z));
                var task4 = await (srTask15, srTask16);
                p.StickyRaycast.SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
                var srTask17 = Async(p.StickyRaycast.SetBelowSlopeAngleToBelowSlopeAngleLeft());
                if (Abs(p.BelowSlopeAngleLeft - p.BelowSlopeAngleRight) < Tolerance)
                {
                    var srTask18 = Async(p.StickyRaycast.SetCastFromLeftWithBelowSlopeAngleLtZero());
                    var task5 = await (srTask17, srTask18);
                }

                if (p.BelowSlopeAngleLeft == 0f && p.BelowSlopeAngleRight != 0f)
                {
                    var srTask19 = Async(p.StickyRaycast.SetCastFromLeftWithBelowSlopeAngleRightLtZero());
                    var task6 = await (srTask17, srTask19);
                }

                var srTask20 = Async(p.StickyRaycast.SetBelowSlopeAngleToBelowSlopeAngleRight());
                if (p.BelowSlopeAngleLeft != 0f && p.BelowSlopeAngleRight == 0f)
                {
                    var srTask21 = Async(p.StickyRaycast.SetCastFromLeftWithBelowSlopeAngleLeftLtZero());
                    var task7 = await (srTask20, srTask21);
                }

                if (p.BelowSlopeAngleLeft != 0f && p.BelowSlopeAngleRight != 0f)
                {
                    p.StickyRaycast.SetCastFromLeftWithLeftDistanceLtRightDistance();
                    if (p.CastFromLeft) await srTask17;
                    else await srTask20;
                }

                var rhcTask1 = Async(p.RaycastHitCollider.SetIsCollidingBelow());
                if (p.BelowSlopeAngleLeft > 0f && p.BelowSlopeAngleRight < 0f && p.SafetyBoxcastControl)
                {
                    var srTask22 = Async(p.SafetyBoxcast.SetSafetyBoxcastForImpassableAngle());
                    var srTask23 = Async(p.SafetyBoxcast.SetHasSafetyBoxcast());
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

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetBelowSlopeAngleLeftToNegative(float crossZ)
        {
            if (crossZ < 0) p.StickyRaycast.SetBelowSlopeAngleLeftToNegative();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetBelowSlopeAngleRightToNegative(float crossZ)
        {
            if (crossZ < 0) p.StickyRaycast.SetBelowSlopeAngleRightToNegative();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetStickyRaycastLength(float raycastLength)
        {
            Async(raycastLength == 0
                ? p.StickyRaycast.SetStickyRaycastLength()
                : p.StickyRaycast.SetStickyRaycastLengthToSelf());
        }

        private async UniTaskVoid SetLeftStickyRaycastLength(float leftRaycastLength)
        {
            if (leftRaycastLength == 0) p.StickyRaycast.SetLeftStickyRaycastLength();
            else p.StickyRaycast.SetLeftStickyRaycastLengthToStickyRaycastLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetRightStickyRaycastLength(float rightRaycastLength)
        {
            if (rightRaycastLength == 0) p.StickyRaycast.SetRightStickyRaycastLength();
            else p.StickyRaycast.SetRightStickyRaycastLengthToStickyRaycastLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private static bool SetStickToSlope(float positionY, float offsetY, bool isJumping, bool stickToSlopes,
            bool wasGroundedLastFrame, float forceY, bool hasPlatform, bool standingOnLastFrameNotNull,
            LayerMask stairs, LayerMask standingOnLastFrame)
        {
            return !(positionY >= offsetY) && !(positionY <= -offsetY) && !isJumping && stickToSlopes &&
                wasGroundedLastFrame && !(forceY > 0) && !hasPlatform || !wasGroundedLastFrame &&
                standingOnLastFrameNotNull && LayerMaskContains(stairs, standingOnLastFrame) && !isJumping;
        }

        private async UniTaskVoid DetachFromMovingPlatform()
        {
            if (p.HasMovingPlatform)
            {
                var phTask = Async(p.Physics.SetGravityActive());
                var rhcTask1 = Async(p.RaycastHitCollider.SetOnMovingPlatform());
                var rhcTask2 = Async(p.RaycastHitCollider.SetMovingPlatformToNull());
                var rhcTask3 = Async(p.RaycastHitCollider.SetDoesNotHaveMovingPlatform());
                var rhcTask4 = Async(p.RaycastHitCollider.StopMovingPlatformCurrentGravity());
                var task = await (phTask, rhcTask1, rhcTask2, rhcTask3, rhcTask4);
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private static bool SetApplyingExternalForce(float forceY, float speedY)
        {
            return forceY > 0 && speedY > 0;
        }

        private static bool SetNotHighEnoughForOneWayPlatform(bool wasGroundedLastFrame, float smallestDistance,
            float boundsHeight, LayerMask oneWayPlatform, LayerMask standingOn, LayerMask movingOneWayPlatform)
        {
            return !wasGroundedLastFrame && smallestDistance < boundsHeight / 2 &&
                LayerMaskContains(oneWayPlatform, standingOn) || LayerMaskContains(movingOneWayPlatform, standingOn);
        }

        private async UniTaskVoid ApplyToRaysBelowLayerMask(bool midHeightOneWayPlatformMaskContains,
            bool stairsMaskContains, bool standingOnColliderBoundsContains, bool onMovingPlatform,
            bool newPositionYGtZero)
        {
            if (midHeightOneWayPlatformMaskContains)
                p.LayerMask.SetRaysBelowLayerMaskPlatformsToPlatformsWithoutHeight();
            if (stairsMaskContains && standingOnColliderBoundsContains)
                p.LayerMask.SetRaysBelowLayerMaskPlatformsToOneWayOrStairs();
            if (onMovingPlatform && newPositionYGtZero) p.LayerMask.SetRaysBelowLayerMaskPlatformsToOneWay();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeDownHitsStorage()
        {
            if (p.DownHitsStorageLength != p.NumberOfVerticalRaysPerSide)
                p.RaycastHitCollider.InitializeDownHitsStorage();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetRaysBelowLayerMask()
        {
            var lmTask1 = Async(p.LayerMask.SetRaysBelowLayerMaskPlatforms());
            var lmTask2 = Async(p.LayerMask.SetRaysBelowLayerMaskPlatformsWithoutOneWay());
            var task1 = await (lmTask1, lmTask2);
            p.LayerMask.SetRaysBelowLayerMaskPlatformsWithoutMidHeight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetVerticalRaycast()
        {
            var rTask1 = Async(p.Raycast.SetVerticalRaycastFromLeft());
            var rTask2 = Async(p.Raycast.SetVerticalRaycastToRight());
            var task1 = await (rTask1, rTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastHorizontalRays(RaycastDirection direction)
        {
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
                float currentHitDistance;
                var rhcTask3 = Async(SetCurrentSideHitsStorage(direction));
                var rhcTask4 = Async(SetCurrentHorizontalHitDistance(direction));
                var rhcTask5 = Async(GetCurrentHorizontalHitDistance(direction));
                var task2 = await (rhcTask3, rhcTask4, rhcTask5);
                if (currentHitDistance > 0)
                {
                    var rhcTask6 = Async(SetCurrentHorizontalHitCollider(direction));
                    var rhcTask7 = Async(SetCurrentHorizontalHitAngle(direction));
                    var task3 = await (rhcTask6, rhcTask7);
                    Collider2D currentHitCollider;
                    float currentHitAngle;
                    var movementIsRayDirection = false;
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
                }

                AddToCurrentHorizontalHitsStorageIndex(direction);

                async UniTaskVoid GetCurrentHorizontalHitDistance(RaycastDirection d)
                {
                    currentHitDistance = d == Right ? p.CurrentRightHitDistance : p.CurrentLeftHitDistance;
                    await SetYieldOrSwitchToThreadPoolAsync();
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetCurrentDistanceBetweenHorizontalHitAndRaycastOrigin(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.SetCurrentDistanceBetweenRightHitAndRaycastOrigin();
            else p.RaycastHitCollider.SetCurrentDistanceBetweenLeftHitAndRaycastOrigin();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetCurrentHorizontalLateralSlopeAngle(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.SetCurrentRightLateralSlopeAngle();
            else p.RaycastHitCollider.SetCurrentLeftLateralSlopeAngle();
        }

        private async UniTaskVoid SetCurrentHorizontalHitAngle(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.SetCurrentRightHitAngle();
            else p.RaycastHitCollider.SetCurrentLeftHitAngle();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetCurrentHorizontalHitCollider(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.SetCurrentRightHitCollider();
            else p.RaycastHitCollider.SetCurrentLeftHitCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetCurrentHorizontalHitDistance(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.SetCurrentRightHitDistance();
            else p.RaycastHitCollider.SetCurrentLeftHitDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetCurrentHorizontalRaycast(RaycastDirection direction)
        {
            if (direction == Right) p.Raycast.SetCurrentRightRaycast();
            else p.Raycast.SetCurrentLeftRaycast();
        }

        private void SetCurrentHorizontalRaycastToIgnoreOneWayPlatform(RaycastDirection direction)
        {
            if (direction == Right) p.Raycast.SetCurrentRightRaycastToIgnoreOneWayPlatform();
            else p.Raycast.SetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }

        private void SetCurrentHorizontalRaycastOrigin(RaycastDirection direction)
        {
            if (direction == Right) p.Raycast.SetCurrentRightRaycastOrigin();
            else p.Raycast.SetCurrentLeftRaycastOrigin();
        }

        private async UniTaskVoid SetHorizontalHitsStorageSize(RaycastDirection direction)
        {
            if (direction == Right && p.RightHitsStorageLength != p.NumberOfHorizontalRaysPerSide)
                p.RaycastHitCollider.InitializeRightHitsStorage();
            else if (p.LeftHitsStorageLength != p.NumberOfHorizontalRaysPerSide)
                p.RaycastHitCollider.InitializeLeftHitsStorage();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetHorizontalRaycastLength(RaycastDirection direction)
        {
            if (direction == Right) p.Raycast.InitializeRightRaycastLength();
            else p.Raycast.InitializeLeftRaycastLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetHorizontalRaycastToTopOrigin(RaycastDirection direction)
        {
            if (direction == Right) p.Raycast.SetRightRaycastToTopOrigin();
            else p.Raycast.SetLeftRaycastToTopOrigin();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetHorizontalRaycastFromBottomOrigin(RaycastDirection direction)
        {
            if (direction == Right) p.Raycast.SetRightRaycastFromBottomOrigin();
            else p.Raycast.SetLeftRaycastFromBottomOrigin();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeCurrentHorizontalHitsStorageIndex(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.InitializeCurrentRightHitsStorageIndex();
            else p.RaycastHitCollider.InitializeCurrentLeftHitsStorageIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void AddToCurrentHorizontalHitsStorageIndex(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.AddToCurrentRightHitsStorageIndex();
            p.RaycastHitCollider.AddToCurrentLeftHitsStorageIndex();
        }

        private async UniTaskVoid AddHorizontalHitToContactList(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.AddRightHitToContactList();
            else p.RaycastHitCollider.AddLeftHitToContactList();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void SetNewHorizontalPosition(RaycastDirection direction, bool isGrounded, float speedY)
        {
            if (direction == Right) p.Physics.SetNewPositiveHorizontalPosition();
            else p.Physics.SetNewNegativeHorizontalPosition();
            if (!isGrounded && speedY != 0) p.Physics.StopNewHorizontalPosition();
        }

        private async UniTaskVoid SetFailedSlopeAngle(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.SetRightFailedSlopeAngle();
            else p.RaycastHitCollider.SetLeftFailedSlopeAngle();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetCurrentWallCollider(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.SetRightCurrentWallCollider();
            else p.RaycastHitCollider.SetLeftCurrentWallCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetCurrentSideHitsStorage(RaycastDirection direction)
        {
            if (direction == Right) p.RaycastHitCollider.SetCurrentRightHitsStorage();
            else p.RaycastHitCollider.SetCurrentLeftHitsStorage();
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
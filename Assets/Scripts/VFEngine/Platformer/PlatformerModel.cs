using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
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
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PlatformerModel", menuName = PlatformerModelPath, order = 0)]
    [InlineEditor]
    public class PlatformerModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private PlatformerData p;

        #endregion

        private const float Tolerance = 0;

        #region private methods

        private void InitializeData()
        {
            p = new PlatformerData {Character = character, Settings = CreateInstance<PlatformerSettings>()};
            p.Controller = p.Character.GetComponentNoAllocation<PlatformerController>();
            p.DisplayWarningsControl = p.DisplayWarningsControlSetting;
            p.RuntimeData = PlatformerRuntimeData.CreateInstance(p.Controller);
            if (p.DisplayWarningsControl) GetWarningMessages();
        }

        private void InitializeModel()
        {
            var raycast = p.Character.GetComponentNoAllocation<RaycastController>();
            var boxcast = p.Character.GetComponentNoAllocation<BoxcastController>();
            var raycastHitCollider = p.Character.GetComponentNoAllocation<RaycastHitColliderController>();
            var physics = p.Character.GetComponentNoAllocation<PhysicsController>();
            var layerMask = p.Character.GetComponentNoAllocation<LayerMaskController>();
            p.RaycastRuntimeData = raycast.RaycastRuntimeData;
            p.UpRaycastRuntimeData = raycast.UpRaycastRuntimeData;
            p.DistanceToGroundRaycastRuntimeData = raycast.DistanceToGroundRaycastRuntimeData;
            p.StickyRaycastRuntimeData = raycast.StickyRaycastRuntimeData;
            p.RightStickyRaycastRuntimeData = raycast.RightStickyRaycastRuntimeData;
            p.LeftStickyRaycastRuntimeData = raycast.LeftStickyRaycastRuntimeData;
            p.BoxcastRuntimeData = boxcast.BoxcastRuntimeData;
            p.SafetyBoxcastRuntimeData = boxcast.SafetyBoxcastRuntimeData;
            p.RaycastHitColliderRuntimeData = raycastHitCollider.RaycastHitColliderRuntimeData;
            p.UpRaycastHitColliderRuntimeData = raycastHitCollider.UpRaycastHitColliderRuntimeData;
            p.RightRaycastHitColliderRuntimeData = raycastHitCollider.RightRaycastHitColliderRuntimeData;
            p.DownRaycastHitColliderRuntimeData = raycastHitCollider.DownRaycastHitColliderRuntimeData;
            p.LeftRaycastHitColliderRuntimeData = raycastHitCollider.LeftRaycastHitColliderRuntimeData;
            p.LeftStickyRaycastHitColliderRuntimeData = raycastHitCollider.LeftStickyRaycastHitColliderRuntimeData;
            p.RightStickyRaycastHitColliderRuntimeData = raycastHitCollider.RightStickyRaycastHitColliderRuntimeData;
            p.DistanceToGroundRaycastHitColliderRuntimeData =
                raycastHitCollider.DistanceToGroundRaycastHitColliderRuntimeData;
            p.PhysicsRuntimeData = physics.PhysicsRuntimeData;
            p.LayerMaskRuntimeData = layerMask.LayerMaskRuntimeData;
            p.Raycast = p.RaycastRuntimeData.Controller;
            p.CastRaysOnBothSides = p.RaycastRuntimeData.CastRaysOnBothSides;
            p.NumberOfHorizontalRaysPerSide = p.RaycastRuntimeData.NumberOfHorizontalRaysPerSide;
            p.NumberOfVerticalRaysPerSide = p.RaycastRuntimeData.NumberOfVerticalRaysPerSide;
            p.BoundsHeight = p.RaycastRuntimeData.BoundsHeight;
            p.BoundsBottomCenterPosition = p.RaycastRuntimeData.BoundsBottomCenterPosition;
            p.DistanceToGroundRayMaximumLength = p.RaycastRuntimeData.DistanceToGroundRayMaximumLength;
            p.UpRaycastSmallestDistance = p.UpRaycastRuntimeData.UpRaycastSmallestDistance;
            p.DistanceToGroundRaycastHit = p.DistanceToGroundRaycastRuntimeData.DistanceToGroundRaycastHit;
            p.StickToSlopesOffsetY = p.StickyRaycastRuntimeData.StickToSlopesOffsetY;
            p.StickyRaycastLength = p.StickyRaycastRuntimeData.StickyRaycastLength;
            p.IsCastingLeft = p.StickyRaycastRuntimeData.IsCastingLeft;
            p.RightStickyRaycastLength = p.RightStickyRaycastRuntimeData.RightStickyRaycastLength;
            p.RightStickyRaycastHit = p.RightStickyRaycastRuntimeData.RightStickyRaycastHit;
            p.LeftStickyRaycastHit = p.LeftStickyRaycastRuntimeData.LeftStickyRaycastHit;
            p.LeftStickyRaycastLength = p.LeftStickyRaycastRuntimeData.LeftStickyRaycastLength;
            p.Boxcast = p.BoxcastRuntimeData.Controller;
            p.SafetyBoxcastHit = p.SafetyBoxcastRuntimeData.SafetyBoxcastHit;
            p.RaycastHitCollider = p.RaycastHitColliderRuntimeData.RaycastHitColliderController;
            p.IgnoredCollider = p.RaycastHitColliderRuntimeData.IgnoredCollider;
            p.WasTouchingCeilingLastFrame = p.UpRaycastHitColliderRuntimeData.WasTouchingCeilingLastFrame;
            p.UpHitsStorageLength = p.UpRaycastHitColliderRuntimeData.UpHitsStorageLength;
            p.RaycastUpHitAt = p.UpRaycastHitColliderRuntimeData.RaycastUpHitAt;
            p.UpHitConnected = p.UpRaycastHitColliderRuntimeData.UpHitConnected;
            p.IsCollidingAbove = p.UpRaycastHitColliderRuntimeData.IsCollidingAbove;
            p.CurrentRightHitDistance = p.RightRaycastHitColliderRuntimeData.CurrentRightHitDistance;
            p.CurrentRightHitCollider = p.RightRaycastHitColliderRuntimeData.CurrentRightHitCollider;
            p.CurrentRightHitAngle = p.RightRaycastHitColliderRuntimeData.CurrentRightHitAngle;
            p.RightHitConnected = p.RightRaycastHitColliderRuntimeData.RightHitConnected;
            p.RightHitsStorageLength = p.RightRaycastHitColliderRuntimeData.RightHitsStorageLength;
            p.IsCollidingRight = p.RightRaycastHitColliderRuntimeData.IsCollidingRight;
            p.MovingPlatformCurrentSpeed = p.DownRaycastHitColliderRuntimeData.MovingPlatformCurrentSpeed;
            p.WasGroundedLastFrame = p.DownRaycastHitColliderRuntimeData.HasGroundedLastFrame;
            p.CurrentDownHitSmallestDistance = p.DownRaycastHitColliderRuntimeData.CurrentDownHitSmallestDistance;
            p.GroundedEvent = p.DownRaycastHitColliderRuntimeData.GroundedEvent;
            p.OnMovingPlatform = p.DownRaycastHitColliderRuntimeData.OnMovingPlatform;
            p.DownHitsStorageLength = p.DownRaycastHitColliderRuntimeData.DownHitsStorageLength;
            p.StandingOnLastFrame = p.DownRaycastHitColliderRuntimeData.StandingOnLastFrame;
            p.HasStandingOnLastFrame = p.DownRaycastHitColliderRuntimeData.HasStandingOnLastFrame;
            p.StandingOnCollider = p.DownRaycastHitColliderRuntimeData.StandingOnCollider;
            p.SmallestDistanceToDownHit = p.DownRaycastHitColliderRuntimeData.SmallestDistanceToDownHit;
            p.RaycastDownHitAt = p.DownRaycastHitColliderRuntimeData.RaycastDownHitAt;
            p.CrossBelowSlopeAngle = p.DownRaycastHitColliderRuntimeData.CrossBelowSlopeAngle;
            p.HasPhysicsMaterialDataClosestToDownHit =
                p.DownRaycastHitColliderRuntimeData.HasPhysicsMaterialClosestToDownHit;
            p.HasPathMovementControllerClosestToDownHit =
                p.DownRaycastHitColliderRuntimeData.HasPathMovementClosestToDownHit;
            p.HasMovingPlatform = p.DownRaycastHitColliderRuntimeData.HasMovingPlatform;
            p.StandingOnWithSmallestDistanceLayer =
                p.DownRaycastHitColliderRuntimeData.StandingOnWithSmallestDistanceLayer;
            p.DownHitConnected = p.DownRaycastHitColliderRuntimeData.DownHitConnected;
            p.IsCollidingBelow = p.DownRaycastHitColliderRuntimeData.IsCollidingBelow;
            p.CurrentLeftHitCollider = p.LeftRaycastHitColliderRuntimeData.CurrentLeftHitCollider;
            p.CurrentLeftHitDistance = p.LeftRaycastHitColliderRuntimeData.CurrentLeftHitDistance;
            p.CurrentLeftHitAngle = p.LeftRaycastHitColliderRuntimeData.CurrentLeftHitAngle;
            p.LeftHitConnected = p.LeftRaycastHitColliderRuntimeData.LeftHitConnected;
            p.LeftHitsStorageLength = p.LeftRaycastHitColliderRuntimeData.LeftHitsStorageLength;
            p.IsCollidingLeft = p.LeftRaycastHitColliderRuntimeData.IsCollidingLeft;
            p.CrossBelowSlopeAngleLeft = p.LeftStickyRaycastHitColliderRuntimeData.CrossBelowSlopeAngleLeft;
            p.BelowSlopeAngleLeft = p.LeftStickyRaycastHitColliderRuntimeData.BelowSlopeAngleLeft;
            p.CrossBelowSlopeAngleRight = p.RightStickyRaycastHitColliderRuntimeData.CrossBelowSlopeAngleRight;
            p.BelowSlopeAngleRight = p.RightStickyRaycastHitColliderRuntimeData.BelowSlopeAngleRight;
            p.DistanceToGroundRaycastHitConnected =
                p.DistanceToGroundRaycastHitColliderRuntimeData.DistanceToGroundRaycastHitConnected;
            p.Physics = p.PhysicsRuntimeData.Controller;
            p.Speed = p.PhysicsRuntimeData.Speed;
            p.GravityActive = p.PhysicsRuntimeData.GravityActive;
            p.FallSlowFactor = p.PhysicsRuntimeData.FallSlowFactor;
            p.HorizontalMovementDirection = p.PhysicsRuntimeData.HorizontalMovementDirection;
            p.MovementDirectionThreshold = p.PhysicsRuntimeData.MovementDirectionThreshold;
            p.ExternalForce = p.PhysicsRuntimeData.ExternalForce;
            p.MaximumSlopeAngle = p.PhysicsRuntimeData.MaximumSlopeAngle;
            p.NewPosition = p.PhysicsRuntimeData.NewPosition;
            p.SmallValue = p.PhysicsRuntimeData.SmallValue;
            p.Gravity = p.PhysicsRuntimeData.Gravity;
            p.IsFalling = p.PhysicsRuntimeData.IsFalling;
            p.StickToSlopesControl = p.PhysicsRuntimeData.StickToSlopesControl;
            p.IsJumping = p.PhysicsRuntimeData.IsJumping;
            p.SafetyBoxcastControl = p.PhysicsRuntimeData.SafetyBoxcastControl;
            p.LayerMask = p.LayerMaskRuntimeData.LayerMaskController;
            p.MidHeightOneWayPlatformMask = p.LayerMaskRuntimeData.MidHeightOneWayPlatformMask;
            p.StairsMask = p.LayerMaskRuntimeData.StairsMask;
            p.OneWayPlatformMask = p.LayerMaskRuntimeData.OneWayPlatformMask;
            p.MovingOneWayPlatformMask = p.LayerMaskRuntimeData.MovingOneWayPlatformMask;
        }

        private void GetWarningMessages()
        {
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
            if (!p.RaycastHitCollider) warningMessage += FieldParentGameObjectString($"Collider {ctr}", $"{ch}");
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
            if (p.StickToSlopesControl)
            {
                var srTask1 = Async(p.Raycast.ResetStickyRaycastState());
                var srhTask1 = Async(p.RaycastHitCollider.ResetStickyRaycastHitColliderState());
                var srhTask2 = Async(p.RaycastHitCollider.ResetLeftStickyRaycastHitColliderState());
                var srhTask3 = Async(p.RaycastHitCollider.ResetRightStickyRaycastHitColliderState());
                var task1 = await (srTask1, srhTask1, srhTask2, srhTask3);
            }

            var rTask1 = Async(p.Raycast.SetRaysParameters());
            var rhcTask1 = Async(p.RaycastHitCollider.SetWasGroundedLastFrame());
            var rhcTask2 = Async(p.RaycastHitCollider.SetStandingOnLastFrame());
            var rhcTask3 = Async(p.RaycastHitCollider.SetWasTouchingCeilingLastFrame());
            var rhcTask4 = Async(p.RaycastHitCollider.SetCurrentRightWallColliderNull());
            var rhcTask5 = Async(p.RaycastHitCollider.SetCurrentLeftWallColliderNull());
            var rhcTask6 = Async(p.RaycastHitCollider.ResetRaycastHitColliderState());
            var rhcTask7 = Async(p.RaycastHitCollider.ResetUpRaycastHitColliderState());
            var rhcTask8 = Async(p.RaycastHitCollider.ResetRightRaycastHitColliderState());
            var rhcTask9 = Async(p.RaycastHitCollider.ResetDownRaycastHitColliderState());
            var rhcTask10 = Async(p.RaycastHitCollider.ResetLeftRaycastHitColliderState());
            var rhcTask11 = Async(p.RaycastHitCollider.ResetDistanceToGroundRaycastHitColliderState());
            var phTask1 = Async(p.Physics.SetNewPosition());
            var phTask2 = Async(p.Physics.ResetState());
            var task2 = await (rTask1, rhcTask1, rhcTask2, rhcTask3, rhcTask4, rhcTask5, rhcTask6, rhcTask7, rhcTask8,
                rhcTask9, rhcTask10, rhcTask11, phTask1, phTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid TestMovingPlatform()
        {
            if (p.HasMovingPlatform)
            {
                p.RaycastHitCollider.SetMovingPlatformCurrentSpeed();
                if (!SpeedNan(p.MovingPlatformCurrentSpeed)) p.Physics.TranslatePlatformSpeedToTransform();
                var platformTest = !TimeLteZero() || !AxisSpeedNan(p.MovingPlatformCurrentSpeed) ||
                                   !p.WasTouchingCeilingLastFrame;
                if (platformTest)
                {
                    var rTask1 = Async(p.Raycast.SetRaysParameters());
                    var rhcTask1 = Async(p.RaycastHitCollider.SetOnMovingPlatform());
                    var rhcTask2 = Async(p.RaycastHitCollider.SetMovingPlatformCurrentGravity());
                    var phTask1 = Async(p.Physics.DisableGravity());
                    var phTask2 = Async(p.Physics.ApplyMovingPlatformSpeedToNewPosition());
                    var task1 = await (rTask1, rhcTask1, rhcTask2, phTask1, phTask2);
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
                    if (p.GroundedEvent && p.NewPosition.y < 0) p.Physics.StopNewVerticalPosition();
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
            else await Async(p.Physics.SetIsNotFalling());
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
                if (p.HasStandingOnLastFrame)
                {
                    p.LayerMask.SetSavedBelowLayerToStandingOnLastFrameLayer();
                    if (midHeightOneWayPlatformMaskContains)
                        p.RaycastHitCollider.SetStandingOnLastFrameLayerToPlatforms();
                }

                if (p.WasGroundedLastFrame && p.HasStandingOnLastFrame)
                {
                    var pTask1 = Async(ApplyToRaysBelowLayerMask(midHeightOneWayPlatformMaskContains,
                        p.OnMovingPlatform, p.StairsMask, p.StandingOnLastFrame.layer, p.StandingOnCollider,
                        p.BoundsBottomCenterPosition, p.NewPosition.y));
                    var rTask3 = Async(p.RaycastHitCollider.InitializeSmallestDistanceToDownHit());
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
                            if (p.RaycastDownHitAt.distance < p.SmallestDistanceToDownHit)
                            {
                                var rhcTask13 = Async(p.RaycastHitCollider.SetSmallestDistanceIndexAt());
                                var rhcTask14 = Async(p.RaycastHitCollider.SetDownHitWithSmallestDistance());
                                var rTask4 = Async(p.RaycastHitCollider.SetSmallestDistanceToDownHitDistance());
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
                            !((p.WasGroundedLastFrame || !(p.SmallestDistanceToDownHit < p.BoundsHeight / 2) ||
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
                            var task9 = await (rhcTask1, phTask3);
                        }
                        else
                        {
                            //p.RaycastHitCollider.SetDistance
                            //p.Raycast.SetDistanceBetweenDownRaycastsAndSmallestDistancePoint();
                            p.Physics.ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
                        }

                        if (!p.WasGroundedLastFrame && p.Speed.y > 0) p.Physics.ApplySpeedToVerticalNewPosition();
                        if (Abs(p.NewPosition.y) < p.SmallValue) p.Physics.StopNewVerticalPosition();
                        if (p.HasPhysicsMaterialDataClosestToDownHit)
                            p.RaycastHitCollider.SetFrictionToDownHitWithSmallestDistancesFriction();
                        if (p.HasPathMovementControllerClosestToDownHit && p.GroundedEvent)
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
                    var t5 = Async(p.RaycastHitCollider.StopMovingPlatformCurrentSpeed());
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
                p.HasStandingOnLastFrame && LayerMaskContains(p.StairsMask, p.StandingOnLastFrame.layer) &&
                !p.IsJumping;
            if (stickToSlope)
            {
                await Async(p.StickyRaycastLength == 0
                    ? p.Raycast.SetStickyRaycastLength()
                    : p.Raycast.SetStickyRaycastLengthToSelf());
                var srTask1 = Async(SetLeftStickyRaycastLength(p.LeftStickyRaycastLength));
                var srTask2 = Async(SetRightStickyRaycastLength(p.RightStickyRaycastLength));
                var srTask3 = Async(p.Raycast.SetLeftStickyRaycastOriginY());
                var srTask4 = Async(p.Raycast.SetLeftStickyRaycastOriginX());
                var srTask5 = Async(p.Raycast.SetRightStickyRaycastOriginY());
                var srTask6 = Async(p.Raycast.SetRightStickyRaycastOriginX());
                var srTask7 = Async(p.Raycast.SetDoNotCastFromLeft());
                var srTask8 = Async(p.RaycastHitCollider.InitializeBelowSlopeAngle());
                var task1 = await (srTask1, srTask2, srTask3, srTask4, srTask5, srTask6, srTask7, srTask8);
                var srTask9 = Async(p.Raycast.SetLeftStickyRaycast());
                var srTask10 = Async(p.Raycast.SetRightStickyRaycast());
                var task2 = await (srTask9, srTask10);
                var srTask11 = Async(p.RaycastHitCollider.SetBelowSlopeAngleLeft());
                var srTask12 = Async(p.RaycastHitCollider.SetCrossBelowSlopeAngleLeft());
                var srTask13 = Async(p.RaycastHitCollider.SetBelowSlopeAngleRight());
                var srTask14 = Async(p.RaycastHitCollider.SetCrossBelowSlopeAngleRight());
                var task3 = await (srTask11, srTask12, srTask13, srTask14);
                var srTask15 = Async(SetBelowSlopeAngleLeftToNegative(p.CrossBelowSlopeAngleLeft.z));
                var srTask16 = Async(SetBelowSlopeAngleRightToNegative(p.CrossBelowSlopeAngleRight.z));
                var task4 = await (srTask15, srTask16);
                p.Raycast.SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
                var srTask17 = Async(p.RaycastHitCollider.SetBelowSlopeAngleToBelowSlopeAngleLeft());
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

                var srTask20 = Async(p.RaycastHitCollider.SetBelowSlopeAngleToBelowSlopeAngleRight());
                if (p.BelowSlopeAngleLeft != 0f && p.BelowSlopeAngleRight == 0f)
                {
                    var srTask21 = Async(p.Raycast.SetCastFromLeftWithBelowSlopeAngleLeftLtZero());
                    var task7 = await (srTask20, srTask21);
                }

                if (p.BelowSlopeAngleLeft != 0f && p.BelowSlopeAngleRight != 0f)
                {
                    p.Raycast.SetCastFromLeftWithLeftDistanceLtRightDistance();
                    if (p.IsCastingLeft) await srTask17;
                    else await srTask20;
                }

                var rhcTask1 = Async(p.RaycastHitCollider.SetIsCollidingBelow());
                if (p.BelowSlopeAngleLeft > 0f && p.BelowSlopeAngleRight < 0f && p.SafetyBoxcastControl)
                {
                    p.Boxcast.SetSafetyBoxcastForImpassableAngle();
                    if (!p.SafetyBoxcastHit || p.SafetyBoxcastHit.collider == p.IgnoredCollider) return;
                    var phTask1 = Async(p.Physics.ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition());
                    var task9 = await (phTask1, rhcTask1);
                    return;
                }

                var currentStickyRaycast = p.IsCastingLeft ? p.LeftStickyRaycastHit : p.RightStickyRaycastHit;
                if (currentStickyRaycast && currentStickyRaycast.collider != p.IgnoredCollider)
                {
                    if (currentStickyRaycast == p.LeftStickyRaycastHit)
                    {
                        var phTask2 = Async(p.Physics.ApplyLeftStickyRaycastToNewVerticalPosition());
                        var task9 = await (phTask2, rhcTask1);
                    }
                    else if (currentStickyRaycast == p.RightStickyRaycastHit)
                    {
                        var phTask3 = Async(p.Physics.ApplyRightStickyRaycastToNewVerticalPosition());
                        var task10 = await (phTask3, rhcTask1);
                    }
                }
            }

            async UniTaskVoid SetBelowSlopeAngleLeftToNegative(float crossZ)
            {
                if (crossZ < 0) p.RaycastHitCollider.SetBelowSlopeAngleLeftToNegative();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetBelowSlopeAngleRightToNegative(float crossZ)
            {
                if (crossZ < 0) p.RaycastHitCollider.SetBelowSlopeAngleRightToNegative();
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
                var currentHitDistance = direction == Right ? p.CurrentRightHitDistance : p.CurrentLeftHitDistance;
                var hitConnected = direction == Right ? p.RightHitConnected : p.LeftHitConnected;
                SetCurrentHorizontalRaycastOrigin(direction);
                if (p.WasGroundedLastFrame && i == 0) SetCurrentHorizontalRaycastToIgnoreOneWayPlatform(direction);
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
                            SetNewHorizontalPosition(direction, p.GroundedEvent, p.Speed.y);
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

            async UniTaskVoid SetCurrentHorizontalHitDistance(RaycastDirection d)
            {
                if (d == Right) p.RaycastHitCollider.SetCurrentRightHitDistance();
                else p.RaycastHitCollider.SetCurrentLeftHitDistance();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetHitConnected(float distance, RaycastDirection d)
            {
                if (distance > 0)
                {
                    if (d == Right) p.RaycastHitCollider.SetRightRaycastHitConnected();
                    else p.RaycastHitCollider.SetLeftRaycastHitConnected();
                }
                else
                {
                    if (d == Right) p.RaycastHitCollider.SetRightRaycastHitMissed();
                    else p.RaycastHitCollider.SetLeftRaycastHitMissed();
                }
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
            if (p.SafetyBoxcastControl)
            {
                p.Boxcast.SetSafetyBoxcast();
                if (p.SafetyBoxcastHit && Abs(p.SafetyBoxcastHit.distance - p.NewPosition.magnitude) < 0.002f)
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
            if (p.GroundedEvent) p.Physics.ApplySlopeAngleSpeedFactorToHorizontalSpeed();
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
                var rTask2 = Async(p.RaycastHitCollider.SetDistanceToGroundRaycastHit());
                var rhcTask2 = Async(p.RaycastHitCollider.InitializeDistanceToGround());
                var task1 = await (rTask1, rTask2, rhcTask2);
                if (p.DistanceToGroundRaycastHitConnected)
                {
                    if (p.DistanceToGroundRaycastHit.collider == p.IgnoredCollider)
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
            if (p.HasStandingOnLastFrame) p.RaycastHitCollider.SetStandingOnLastFrameLayerToSavedBelowLayer();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion

        #region properties

        public PlatformerRuntimeData RuntimeData => p.RuntimeData;

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnRunPlatformer()
        {
            Async(RunPlatformer());
        }

        #endregion

        #endregion
    }
}
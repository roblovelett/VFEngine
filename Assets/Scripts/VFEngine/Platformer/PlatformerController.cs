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
    using static PhysicsExtensions;
    using static TimeExtensions;
    using static Mathf;
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
        
        #endregion

        private void FixedUpdate()
        {
            RunPlatformer();
        }

        private void RunPlatformer()
        {
            ApplyGravity();
            InitializeFrame();
            TestMovingPlatform();
            SetForcesApplied();
            SetHorizontalMovementDirection();
            CastRays();
        }

        private void ApplyGravity()
        {
            physicsController.OnPlatformerApplyGravity();
        }

        private void InitializeFrame()
        {
            raycastHitColliderController.OnPlatformerInitializeFrame();
            physicsController.OnPlatformerInitializeFrame();
            downRaycastHitColliderController.OnPlatformerInitializeFrame();
            upRaycastHitColliderController.OnPlatformerInitializeFrame();
            rightRaycastHitColliderController.OnPlatformerInitializeFrame();
            leftRaycastHitColliderController.OnPlatformerInitializeFrame();
            raycastController.OnSetRaysParameters();
        }

        #region moving platform test
        private bool MovingPlatform => downRaycastHitCollider.HasMovingPlatform;
        private bool MovingPlatformHasSpeed => !SpeedNan(downRaycastHitCollider.MovingPlatformCurrentSpeed);
        private static bool TimeIsActive => !TimeLteZero();
        private bool MovingPlatformHasSpeedOnAxis => !AxisSpeedNan(downRaycastHitCollider.MovingPlatformCurrentSpeed);
        private bool NotTouchingCeilingLastFrame => !upRaycastHitCollider.WasTouchingCeilingLastFrame;
        private bool TestPlatform => TimeIsActive && MovingPlatformHasSpeedOnAxis && NotTouchingCeilingLastFrame;

        private void TestMovingPlatform()
        {
            if (!MovingPlatform) return;
            if (MovingPlatformHasSpeed) physicsController.OnTranslatePlatformSpeedToTransform();
            if (!TestPlatform) return;
            downRaycastHitColliderController.OnPlatformerTestMovingPlatform();
            physicsController.OnPlatformerTestMovingPlatform();
            raycastController.OnSetRaysParameters();
        }
        #endregion

        private void SetForcesApplied()
        {
            physicsController.OnSetForcesApplied();
        }

        #region set horizontal movement direction
        private bool SpeedLessThanNegativeMovementDirectionThreshold =>
            physics.Speed.x < -physics.MovementDirectionThreshold;

        private bool ExternalForceLessThanNegativeMovementDirectionThreshold =>
            physics.ExternalForce.x < -physics.MovementDirectionThreshold;

        private bool LeftMovementDirection => SpeedLessThanNegativeMovementDirectionThreshold ||
                                              ExternalForceLessThanNegativeMovementDirectionThreshold;

        private bool SpeedMoreThanMovementDirectionThreshold => physics.Speed.x > physics.MovementDirectionThreshold;

        private bool ExternalForceMoreThanMovementDirectionThreshold =>
            physics.ExternalForce.x > physics.MovementDirectionThreshold;

        private bool RightMovementDirection => SpeedMoreThanMovementDirectionThreshold ||
                                               ExternalForceMoreThanMovementDirectionThreshold;

        private bool PlatformSpeedMoreThanSpeed =>
            Abs(downRaycastHitCollider.MovingPlatformCurrentSpeed.x) > Abs(physics.Speed.x);

        private bool ApplyPlatformSpeedToMovementDirection => MovingPlatform && PlatformSpeedMoreThanSpeed;

        private void SetHorizontalMovementDirection()
        {
            physicsController.OnSetHorizontalMovementDirectionToStored();
            if (LeftMovementDirection) physicsController.OnSetLeftMovementDirection();
            else if (RightMovementDirection) physicsController.OnSetRightMovementDirection();
            if (ApplyPlatformSpeedToMovementDirection)
                physicsController.OnApplyPlatformSpeedToHorizontalMovementDirection();
            physicsController.OnSetStoredHorizontalMovementDirection();
        }
        #endregion

        #region cast rays
        private bool MovingRight => physics.HorizontalMovementDirection == 1;

        private void CastRays()
        {
            if (raycast.CastRaysOnBothSides)
            {
                CastRaysLeft();
                CastRaysRight();
            }
            else if (MovingRight)
            {
                CastRaysRight();
            }
            else
            {
                CastRaysLeft();
            }

            CastRaysDown();
            CastRaysUp();
        }

        private void CastRaysLeft()
        {
            const RaycastDirection direction = Left;
            CastRaysHorizontally(direction);
        }

        private void CastRaysRight()
        {
            const RaycastDirection direction = Right;
            CastRaysHorizontally(direction);
        }
        
        #endregion

        #region cast rays horizontally

        private void CastRaysHorizontally(RaycastDirection direction)
        {
            SetRaycast(direction);
            SetHitStorage(direction);
            CastRaysToSide(direction);
            
            void SetRaycast(RaycastDirection d)
            {
                if (d == Left) leftRaycastController.OnPlatformerSetRaycast();
                else rightRaycastController.OnPlatformerSetRaycast();
            }
            void SetHitStorage(RaycastDirection d)
            {
                if (d == Left) leftRaycastHitColliderController.OnPlatformerSetHitsStorage();
                else rightRaycastHitColliderController.OnPlatformerSetHitsStorage();
            }
            
            void CastRaysToSide(RaycastDirection d)
            {
                for (var i = 0; i < raycast.NumberOfHorizontalRaysPerSide; i++)
                {
                    SetCurrentRaycast(d);
                    SetCurrentHitsStorage(d);
                    
                    // if hit connected
                    
                    AddToCurrentHitsStorageIndex(d);
                }
            }
            
            void SetCurrentRaycast(RaycastDirection d)
            {
                if (d == Left) leftRaycastController.OnPlatformerSetCurrentRaycast();
                else rightRaycastController.OnPlatformerSetCurrentRaycast();
            }

            void SetCurrentHitsStorage(RaycastDirection d)
            {
                if (d == Left) leftRaycastHitColliderController.OnPlatformerSetCurrentHitsStorage();
                else rightRaycastHitColliderController.OnPlatformerSetCurrentHitsStorage();
            }

            void AddToCurrentHitsStorageIndex(RaycastDirection d)
            {
                if (d == Left) leftRaycastHitColliderController.OnPlatformerAddToCurrentHitsStorageIndex();
                else rightRaycastHitColliderController.OnPlatformerAddToCurrentHitsStorageIndex();
            }
        }


        /*
        private async UniTaskVoid CastHorizontalRays(RaycastDirection direction)
        {
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
                            var rhcTask10 = Async(leftRaycastHitColliderController.OnSetIsCollidingLeft());
                            var rhcTask11 = Async(leftRaycastHitColliderController.OnSetLeftDistanceToLeftCollider());
                            var task5 = await (rhcTask10, rhcTask11);
                        }
                        else
                        {
                            var rhcTask12 = Async(rightRaycastHitColliderController.OnSetIsCollidingRight());
                            var rhcTask13 =
                                Async(rightRaycastHitColliderController.OnSetRightDistanceToRightCollider());
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
                if (d == Right) rightRaycastHitColliderController.OnSetCurrentDistanceBetweenRightHitAndRaycastOrigin();
                else leftRaycastHitColliderController.OnSetCurrentDistanceBetweenLeftHitAndRaycastOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid AddHorizontalHitToContactList(RaycastDirection d)
            {
                if (d == Right) raycastHitColliderController.OnAddRightHitToContactList();
                else raycastHitColliderController.OnAddLeftHitToContactList();
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
                if (d == Right) rightRaycastHitColliderController.OnSetRightFailedSlopeAngle();
                else leftRaycastHitColliderController.OnSetLeftFailedSlopeAngle();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentWallCollider(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.OnSetRightCurrentWallCollider();
                else leftRaycastHitColliderController.OnSetLeftCurrentWallCollider();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetCurrentHorizontalLateralSlopeAngle(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.OnSetCurrentRightLateralSlopeAngle();
                else leftRaycastHitColliderController.OnSetCurrentLeftLateralSlopeAngle();
            }

            async UniTaskVoid SetCurrentHorizontalHitAngle(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.OnSetCurrentRightHitAngle();
                else leftRaycastHitColliderController.OnSetCurrentLeftHitAngle();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentHorizontalHitCollider(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.OnSetCurrentRightHitCollider();
                else leftRaycastHitColliderController.OnSetCurrentLeftHitCollider();
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
                if (d == Right) rightRaycastHitColliderController.OnSetCurrentRightHitsStorage();
                else leftRaycastHitColliderController.OnSetCurrentLeftHitsStorage();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetCurrentHorizontalHitDistance(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.OnSetCurrentRightHitDistance();
                else leftRaycastHitColliderController.OnSetCurrentLeftHitDistance();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            void SetHitConnected(float distance, RaycastDirection d)
            {
                if (distance > 0)
                {
                    if (d == Right) rightRaycastHitColliderController.OnSetRightRaycastHitConnected();
                    else leftRaycastHitColliderController.OnSetLeftRaycastHitConnected();
                }
                else
                {
                    if (d == Right) rightRaycastHitColliderController.OnSetRightRaycastHitMissed();
                    else leftRaycastHitColliderController.OnSetLeftRaycastHitMissed();
                }
            }

            void SetCurrentHorizontalRaycast(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.OnSetCurrentRightRaycast();
                else leftRaycastController.OnSetCurrentLeftRaycast();
            }

            void SetCurrentHorizontalRaycastToIgnoreOneWayPlatform(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.OnSetCurrentRightRaycastToIgnoreOneWayPlatform();
                else leftRaycastController.OnSetCurrentLeftRaycastToIgnoreOneWayPlatform();
            }

            void AddToCurrentHorizontalHitsStorageIndex(RaycastDirection d)
            {
                if (d == Right) rightRaycastHitColliderController.OnAddToCurrentRightHitsStorageIndex();
                leftRaycastHitColliderController.OnAddToCurrentLeftHitsStorageIndex();
            }

            

            async UniTaskVoid SetHorizontalRaycastLength(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.OnInitializeRightRaycastLength();
                else leftRaycastController.OnInitializeLeftRaycastLength();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastToTopOrigin(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.OnSetRightRaycastToTopOrigin();
                else leftRaycastController.OnSetLeftRaycastToTopOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid SetHorizontalRaycastFromBottomOrigin(RaycastDirection d)
            {
                if (d == Right) rightRaycastController.OnSetRightRaycastFromBottomOrigin();
                else leftRaycastController.OnSetLeftRaycastFromBottomOrigin();
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            

            await SetYieldOrSwitchToThreadPoolAsync();
        }*/

        #endregion

        private void CastRaysDown()
        {
        }

        private void CastRaysUp()
        {
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

    private async UniTaskVoid SetHorizontalMovementDirection()
    {
        
        await SetYieldOrSwitchToThreadPoolAsync();
    }



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
                for (var i = 0; i < raycast.NumberOfVerticalRaysPerSide; i++)
                {
                    downRaycastController.OnSetCurrentDownRaycastOriginPoint();
                    if (physics.NewPosition.y > 0 && !downRaycastHitCollider.HasGroundedLastFrame)
                        downRaycastController.OnSetCurrentDownRaycastToIgnoreOneWayPlatform();
                    else downRaycastController.OnSetCurrentDownRaycast();
                    var rhcTask7 = Async(downRaycastHitColliderController.OnSetCurrentDownHitsStorage());
                    var rhcTask8 = Async(downRaycastHitColliderController.OnSetRaycastDownHitAt());
                    var rhcTask9 = Async(downRaycastHitColliderController.OnSetCurrentDownHitSmallestDistance());
                    var task4 = await (rhcTask7, rhcTask8, rhcTask9);
                    if (downRaycastHitCollider.RaycastDownHitAt)
                    {
                        if (downRaycastHitCollider.RaycastDownHitAt.collider == raycastHitCollider.IgnoredCollider)
                            continue;
                        var rhcTask10 = Async(downRaycastHitColliderController.OnSetDownHitConnected());
                        var rhcTask11 = Async(downRaycastHitColliderController.OnSetBelowSlopeAngleAt());
                        var rhcTask12 = Async(downRaycastHitColliderController.OnSetCrossBelowSlopeAngleAt());
                        var task5 = await (rhcTask10, rhcTask11, rhcTask12);
                        if (downRaycastHitCollider.CrossBelowSlopeAngle.z < 0)
                            downRaycastHitColliderController.OnSetNegativeBelowSlopeAngle();
                        if (downRaycastHitCollider.RaycastDownHitAt.distance <
                            downRaycastHitCollider.SmallestDistanceToDownHit)
                        {
                            var rhcTask13 = Async(downRaycastHitColliderController.OnSetSmallestDistanceIndexAt());
                            var rhcTask14 =
                                Async(downRaycastHitColliderController.OnSetDownHitWithSmallestDistance());
                            var rTask4 = Async(downRaycastHitColliderController
                                .OnSetSmallestDistanceToDownHitDistance());
                            var task6 = await (rhcTask13, rhcTask14, rTask4);
                        }
                    }

                    if (downRaycastHitCollider.CurrentDownHitSmallestDistance < physics.SmallValue) break;
                    downRaycastHitColliderController.OnAddDownHitsStorageIndex();
                }

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
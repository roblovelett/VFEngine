using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable SwitchStatementMissingSomeEnumCasesNoDefault
// ReSharper disable ConvertSwitchStatementToSwitchExpression
namespace VFEngine.Platformer
{
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static PhysicsExtensions;
    using static TimeExtensions;
    using static Mathf;
    using static MathsExtensions;
    using static Vector2;
    using static RaycastDirection;

    [CreateAssetMenu(fileName = "PlatformerModel", menuName = "VFEngine/Platformer/Platformer Model", order = 0)]
    public class PlatformerModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [LabelText("Platformer Data")] [SerializeField]
        private PlatformerData p;

        /* fields: methods */
        private async UniTaskVoid Initialize()
        {
            var pTask1 = Async(InitializeData());
            var pTask2 = Async(GetWarningMessages());
            var pTask = await (pTask1, pTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeData()
        {
            p.RaycastHitAngleRef = p.RaycastHitAngle;
            p.RightRaycastOriginPointRef = p.RightRaycastOriginPoint;
            p.LeftRaycastOriginPointRef = p.LeftRaycastOriginPoint;
            p.RightHitsStorageIndexRef = p.RightHitsStorageIndex;
            p.LeftHitsStorageIndexRef = p.LeftHitsStorageIndex;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid GetWarningMessages()
        {
            if (!p.DisplayWarnings) return;
            const string rc = "Raycast";
            const string ctr = "Controller";
            const string ch = "Character";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!p.Physics) warningMessage += FieldParentGameObjectString($"Physics {ctr}", $"{ch}");
            if (!p.Raycast) warningMessage += FieldParentGameObjectString($"{rc} {ctr}", $"{ch}");
            if (!p.RaycastHitCollider)
                warningMessage += FieldParentGameObjectString($"{rc} Hit Collider {ctr}", $"{ch}");
            if (!p.LayerMask) warningMessage += FieldParentGameObjectString($"Layer Mask {ctr}", $"{ch}");
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldParentGameObjectString(string field, string gameObject)
            {
                warningMessageCount++;
                return FieldParentGameObjectMessage(field, gameObject);
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid RunPlatformer()
        {
            var pTask1 = Async(ApplyGravity());
            var pTask2 = Async(InitializeFrame());
            var task1 = await (pTask1, pTask2);
            await Async(TestMovingPlatform());
            await Async(SetHorizontalMovementDirection());
            await Async(CastRays());
            // OnHit Raycast
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
            var rhcTask5 = Async(p.RaycastHitCollider.SetCurrentWallCollider());
            var rhcTask6 = Async(p.RaycastHitCollider.ResetState());
            var rTask1 = Async(p.Raycast.SetRaysParameters());
            var pTask = await (phTask1, phTask2, rhcTask1, rhcTask2, rhcTask3, rhcTask4, rhcTask5, rhcTask6, rTask1);
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
                    var pTask = await (rchTask1, rchTask2, phTask1, phTask2, rTask1);
                    p.Physics.StopHorizontalSpeed();
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

        private async UniTaskVoid CastRays()
        {
            var rTask1 = Async(CastRaysUp());
            var rTask2 = Async(CastRaysHorizontally(Right));
            var rTask3 = Async(CastRaysDown());
            var rTask4 = Async(CastRaysHorizontally(Left));
            if (p.CastRaysOnBothSides)
            {
                var task1 = await (rTask1, rTask2, rTask3, rTask4);
            }
            else
            {
                var horizontalDirection = SetDirection(p.HorizontalMovementDirection);
                switch (horizontalDirection)
                {
                    case Right:
                        var task2 = await (rTask1, rTask2, rTask3);
                        break;
                    case Left:
                        var task3 = await (rTask1, rTask3, rTask4);
                        break;
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private static RaycastDirection SetDirection(int direction)
        {
            var d = None;
            switch (direction)
            {
                case 1:
                    d = Right;
                    break;
                case -1:
                    d = Left;
                    break;
            }

            return d;
        }

        private async UniTaskVoid CastRaysUp()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private void InitializeHitsStorage(RaycastDirection direction)
        {
            var cast = p.CastRaysOnBothSides;
            var i = p.HorizontalHitsStorageIndexesAmount;
            var x = p.NumberOfHorizontalRays;
            var xHalf = (int) Half(x);
            
            switch (direction)
            {
                case Right:
                    InitializeRightHitsStorage();
                    break;
                case Left:
                    InitializeLeftHitsStorage();
                    break;
            }

            void InitializeRightHitsStorage()
            {
                if (cast && i != xHalf) p.RaycastHitCollider.InitializeRightHitsStorageHalf();
                else if (i != x) p.RaycastHitCollider.InitializeRightHitsStorage();
            }

            void InitializeLeftHitsStorage()
            {
                if (cast && i != xHalf) p.RaycastHitCollider.InitializeLeftHitsStorageHalf();
                else if (i != x) p.RaycastHitCollider.InitializeLeftHitsStorage();
            }
        }

        private void SetHitsStorageIndex(RaycastDirection direction, int i)
        {
            switch (direction)
            {
                case Right:
                    p.RightHitsStorageIndex = i;
                    break;
                case Left:
                    p.LeftHitsStorageIndex = i;
                    break;
            }

            p.RightHitsStorageIndex = i;
        }

        private void SetRaycastOriginPoints(RaycastDirection direction, Vector2 bottom, Vector2 top, int number, int i)
        {
            var point = Lerp(bottom, top, i / (float) (number - 1));
            switch (direction)
            {
                case Right:
                    p.RightRaycastOriginPoint = point;
                    break;
                case Left:
                    p.LeftRaycastOriginPoint = point;
                    break;
            }
        }

        private void SetHitsStorageToIgnoreOneWayPlatform(RaycastDirection direction)
        {
            switch (direction)
            {
                case Right:
                    p.RaycastHitCollider.SetRightHitsStorageToIgnoreOneWayPlatform();
                    break;
                case Left:
                    p.RaycastHitCollider.SetLeftHitsStorageToIgnoreOneWayPlatform();
                    break;
            }
        }

        private void SetHitsStorage(RaycastDirection direction)
        {
            switch (direction)
            {
                case Right:
                    p.RaycastHitCollider.SetRightHitsStorage();
                    break;
                case Left:
                    p.RaycastHitCollider.SetLeftHitsStorage();
                    break;
            }
        }

        private async UniTaskVoid CastRaysHorizontally(RaycastDirection direction)
        {
            InitializeHitsStorage(direction);
            var raysNumber = p.HorizontalHitsStorageIndexesAmount;
            for (var i = 0; i < raysNumber; i++)
            {
                SetHitsStorageIndex(direction, i);
                SetRaycastOriginPoints(direction, p.RaycastFromBottom, p.RaycastToTop, raysNumber, i);
                if (p.WasGroundedLastFrame && i == 0) SetHitsStorageToIgnoreOneWayPlatform(direction);
                else SetHitsStorage(direction);

                //var hitDistance = SetHitDistance(direction, i);
                /*
                var hitDistance = hitStorage[i].distance;
                var raycastHit = SetRaycastHit(hitDistance);
                if (!raycastHit) continue;
                var collider = hitStorage[i].collider;
                if (collider == p.IgnoredCollider) break;

                //var hitAngle = Abs(Angle(hitStorage[i].normal, p.Transform.up));
                p.RaycastHitAngle = Abs(Angle(hitStorage[i].normal, p.Transform.up));
                var movementDirection = SetDirection(p.HorizontalMovementDirection);
                var raycastDirection = Right;
                if (movementDirection == raycastDirection) p.RaycastHitCollider.SetRightLateralSlopeAngle(hitAngle);
                if (!(hitAngle > p.MaximumSlopeAngle)) continue;
                if (raycastDirection == Right)
                {
                    p.RaycastHitCollider.SetColliderIsCollidingRight();
                    p.RaycastHitCollider.SetDistanceToRightCollider(hitDistance);
                }
                else
                {
                    p.RaycastHitCollider.SetColliderIsCollidingLeft();
                    p.RaycastHitCollider.SetDistanceToLeftCollider(hitDistance);
                }

                if (movementDirection == raycastDirection)
                {
                    p.RaycastHitCollider.SetCurrentWallCollider(collider.gameObject);
                    p.RaycastHitCollider.SetSlopeAngleFailed();
                    var distance = MathsExtensions.DistanceBetweenPointAndLine()
                }*/
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private static int SetRaycastNumber(int number, bool castBothSides)
        {
            if (castBothSides) number /= 2;
            return number;
        }

        private static bool SetRaycastHit(float distance)
        {
            return distance > 0;
        }

        private async UniTaskVoid CastRaysDown()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysLeft()
        {
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
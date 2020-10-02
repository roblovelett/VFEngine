using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;
    using static PhysicsExtensions;
    using static PlatformerExtensions;
    using static TimeExtensions;
    using static Mathf;

    [CreateAssetMenu(fileName = "PlatformerModel", menuName = "VFEngine/Platformer/Platformer Model", order = 0)]
    public class PlatformerModel : ScriptableObject, IModel
    {
        /* fields */
        [LabelText("Platformer Data")] [SerializeField]
        private PlatformerData p;

        private PhysicsModel physics;
        //private RaycastManagerModel raycast;
        //private RaycastHitColliderManagerModel raycastHitCollider;
        private const string AssetPath = "DefaultPlatformerModel.asset";

        /* fields: methods */
        private void InitializeData()
        {
            p.Initialize();
        }

        private void InitializeModel()
        {
            physics = p.PhysicsModel;
            /*raycast = p.RaycastsManagerModel;
            raycastHitCollider = p.RaycastHitColliderModel;*/
        }

        private async UniTaskVoid PlatformerAsyncInternal()
        {
            await ApplyGravityAsync();
            await InitializeFrameAsync();
            await TestMovingPlatformAsync();
            await SetMovementDirectionAsync();
            await CastRaysAsync();
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

        private async UniTaskVoid ApplyGravityAsyncInternal()
        {
            await physics.SetGravityAsync();
            if (p.Speed.y > 0) await physics.ApplyAscentMultiplierToGravityAsync();
            if (p.Speed.y < 0) await physics.ApplyFallMultiplierToGravityAsync();
            if (p.GravityActive) await physics.ApplyGravityToSpeedAsync();
            if (p.FallSlowFactor != 0) await physics.ApplyFallSlowFactorToSpeedAsync();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeFrameAsyncInternal()
        {
            var pTask1 = physics.SetNewPositionAsync();
            var pTask2 = physics.ResetStateAsync();
            /*
            var rTask1 = raycast.SetRaysParametersAsync();
            var rchTask1 = raycastHitCollider.ClearContactListAsync();
            var rchTask2 = raycastHitCollider.SetWasGroundedLastFrameAsync();
            var rchTask3 = raycastHitCollider.SetStandingOnLastFrameAsync();
            var rchTask4 = raycastHitCollider.SetWasTouchingCeilingLastFrameAsync();
            var rchTask5 = raycastHitCollider.SetCurrentWallColliderAsync();
            var rchTask6 = raycastHitCollider.ResetStateAsync();
            var task1 = await (pTask1, rTask1, rchTask1, rchTask2, rchTask3, rchTask4, rchTask5);
            var task2 = await (pTask2, rchTask6);
            */
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid TestMovingPlatformAsyncInternal()
        {
            if (p.IsCollidingWithMovingPlatform)
            {
                if (!SpeedNan(p.MovingPlatformCurrentSpeed)) await physics.TranslatePlatformSpeedToTransformAsync();
                var platformTest = MovingPlatformTest(TimeLteZero(), AxisSpeedNan(p.MovingPlatformCurrentSpeed),
                    p.WasTouchingCeilingLastFrame);
                if (platformTest)
                {
                    await physics.ApplyPlatformSpeedToNewPositionAsync();
                    await physics.StopSpeedAsync();
                    var pTask1 = physics.DisableGravityAsync();
                    var pTask2 = physics.SetAppliedForcesAsync();
                    /*var rchTask = raycastHitCollider.SetMovingPlatformGravityAsync();
                    var rTask = raycast.SetRaysParametersAsync();
                    var task = await (pTask1, pTask2, rchTask, rTask);*/
                }
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid SetMovementDirectionAsyncInternal()
        {
            await physics.SetMovementDirectionAsync();
            if (p.Speed.x < -p.MovementDirectionThreshold || p.ExternalForce.x < -p.MovementDirectionThreshold)
                await physics.SetMovementDirectionNegativeAsync();
            else if (p.Speed.x > p.MovementDirectionThreshold || p.ExternalForce.x > p.MovementDirectionThreshold)
                await physics.SetMovementDirectionPositiveAsync();
            if (p.IsCollidingWithMovingPlatform && Abs(p.MovingPlatformCurrentSpeed.x) > Abs(p.Speed.x))
                await physics.ApplyPlatformSpeedToMovementDirectionAsync();
            await physics.SetStoredMovementDirectionAsync();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysAsyncInternal()
        {
            var taskRight = CastRaysRightAsync();
            var taskLeft = CastRaysLeftAsync();
            var taskDown = CastRaysDownAsync();
            var taskUp = CastRaysUpAsync();
            
            if (p.CastRaysOnBothSides)
            {
                var task = await (taskRight, taskLeft, taskDown, taskUp);
            }
            else if (Abs(p.MovementDirection - -1) < p.Tolerance)
            {
                var task = await (taskLeft, taskDown, taskUp);
            }
            else
            {
                var task = await (taskRight, taskDown, taskUp);
            }

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysRightAsyncInternal()
        {
            //HorizontalRaycast rightRaycast = new HorizontalRaycast();
            /*
            var task1 = raycast.SetRightRaycastFromBottomAsync();
            var task2 = raycast.SetRightRaycastFromTopAsync();
            var task3 = raycast.SetRightRaycastLengthAsync();

            var task = await (task1, task2, task3);
                
            if (p.RightHitsStorage.Length != p.NumberOfHorizontalRays)
            {
                await raycastHitCollider.SetRightHitsStorageAsync();
            }

            for (var i = 0; i < p.NumberOfHorizontalRays; i++)
            {
                await raycastHit.SetRightRaycastOriginPointAsync();

                if (p.WasGroundedLastFrame && i == 0)
                {
                    await raycastHitCollider.SetRightHitsStorageElementToIgnoreOnewayPlatformAsync();
                }
                else
                {
                    await raycastHitCollider.SetRightHitsStorageElementAsync();
                }
            }
            */
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysHorizontallyAsyncInternal(UniTask<UniTaskVoid> setRaycastFromBottom,
            UniTask<UniTaskVoid> setRaycastFromTop, UniTask<UniTaskVoid> setRaycastLength, int hitStorageLength,
            UniTask<UniTaskVoid> setSideHitsStorage, UniTask<UniTaskVoid> setRaycastOriginPoint, 
            UniTask<UniTaskVoid> setSideHitsStorageElementToIgnoreOnewayPlatform,
            UniTask<UniTaskVoid> setSideHitsStorageElement)
        {
            var task = await (setRaycastFromBottom, setRaycastFromTop, setRaycastLength);
            //if (hitStorageLength != p.NumberOfHorizontalRays) await setSideHitsStorage;
            /*for (var i = 0; i < p.NumberOfHorizontalRays; i++)
            {
                await setRaycastOriginPoint;
                if (p.wasGroundedLastFrame && i == 0) await setSideHitsStorageElementToIgnoreOnewayPlatform;
                else await setSideHitsStorageElement;
            }*/

            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private UniTask<UniTaskVoid> CastRaysHorizontallyAsync(UniTask<UniTaskVoid> setRaycastFromBottom,
            UniTask<UniTaskVoid> setRaycastFromTop, UniTask<UniTaskVoid> setRaycastLength, int hitStorageLength,
            UniTask<UniTaskVoid> setSideHitsStorage, UniTask<UniTaskVoid> setRaycastOriginPoint,
            bool wasGroundedLastFrame, UniTask<UniTaskVoid> setSideHitsStorageElementToIgnoreOnewayPlatform,
            UniTask<UniTaskVoid> setSideHitsStorageElement)
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysHorizontallyAsyncInternal(setRaycastFromBottom,
                    setRaycastFromTop, setRaycastLength, hitStorageLength, setSideHitsStorage, setRaycastOriginPoint, setSideHitsStorageElementToIgnoreOnewayPlatform, setSideHitsStorageElement));
            }
            finally
            {
                CastRaysHorizontallyAsyncInternal(setRaycastFromBottom,
                    setRaycastFromTop, setRaycastLength, hitStorageLength, setSideHitsStorage, setRaycastOriginPoint,setSideHitsStorageElementToIgnoreOnewayPlatform, setSideHitsStorageElement).Forget();
            }
        }

        private class HorizontalRaycast
        {
            public UniTask<UniTaskVoid> SetRaycastFromBottom { get; private set; }
            public UniTask<UniTaskVoid> SetRaycastFromTop { get; private set; }
            public UniTask<UniTaskVoid> SetRaycastLength { get; private set; }
            public UniTask<UniTaskVoid> SetRaycastOriginPoint { get; private set; }
            public int HitStorageLength { get; private set; }
            public UniTask<UniTaskVoid> SetSideHitsStorage { get; private set; } 
            
            public UniTask<UniTaskVoid> SetSideHitsStorageElementToIgnoreOnewayPlatform { get; private set; }
            public UniTask<UniTaskVoid> SetSideHitsStorageElement { get; private set; }

            public HorizontalRaycast(UniTask<UniTaskVoid> setRaycastFromBottom,UniTask<UniTaskVoid> setRaycastFromTop,UniTask<UniTaskVoid> setRaycastLength,
                int hitStorageLength,UniTask<UniTaskVoid> setSideHitsStorage,UniTask<UniTaskVoid> setRaycastOriginPoint,
                UniTask<UniTaskVoid> setSideHitsStorageElementToIgnoreOneWayPlatform, UniTask<UniTaskVoid> setSideHitsStorageElement)
            {
                setRaycastFromBottom = SetRaycastFromBottom;
                setRaycastFromTop = SetRaycastFromTop;
                setRaycastLength = SetRaycastLength;
                hitStorageLength = HitStorageLength;
                setSideHitsStorage = SetSideHitsStorage;
                setRaycastOriginPoint = SetRaycastOriginPoint;
                setSideHitsStorageElementToIgnoreOneWayPlatform = SetSideHitsStorageElementToIgnoreOnewayPlatform;
                setSideHitsStorageElement = SetSideHitsStorageElement;
            }
        }

        private class HorizontalRaycastHitCollider
        {
            
        }

        private async UniTaskVoid CastRaysDownAsyncInternal()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysLeftAsyncInternal()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid CastRaysUpAsyncInternal()
        {
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private UniTask<UniTaskVoid> ApplyGravityAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(ApplyGravityAsyncInternal());
            }
            finally
            {
                ApplyGravityAsyncInternal().Forget();
            }
        }

        private UniTask<UniTaskVoid> InitializeFrameAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(InitializeFrameAsyncInternal());
            }
            finally
            {
                InitializeFrameAsyncInternal().Forget();
            }
        }

        private UniTask<UniTaskVoid> TestMovingPlatformAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(TestMovingPlatformAsyncInternal());
            }
            finally
            {
                TestMovingPlatformAsyncInternal().Forget();
            }
        }

        private UniTask<UniTaskVoid> SetMovementDirectionAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(SetMovementDirectionAsyncInternal());
            }
            finally
            {
                SetMovementDirectionAsyncInternal().Forget();
            }
        }

        private UniTask<UniTaskVoid> CastRaysAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysAsyncInternal());
            }
            finally
            {
                CastRaysAsyncInternal().Forget();
            }
        }

        private UniTask<UniTaskVoid> CastRaysRightAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysRightAsyncInternal());
            }
            finally
            {
                CastRaysRightAsyncInternal().Forget();
            }
        }

        private UniTask<UniTaskVoid> CastRaysDownAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysDownAsyncInternal());
            }
            finally
            {
                CastRaysDownAsyncInternal().Forget();
            }
        }

        private UniTask<UniTaskVoid> CastRaysLeftAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysLeftAsyncInternal());
            }
            finally
            {
                CastRaysLeftAsyncInternal().Forget();
            }
        }

        private UniTask<UniTaskVoid> CastRaysUpAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(CastRaysUpAsyncInternal());
            }
            finally
            {
                CastRaysUpAsyncInternal().Forget();
            }
        }

        /* properties */
        public static string ModelPath => $"{DefaultPath}{PlatformerPath}{AssetPath}";

        /* properties: methods */
        public void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        public UniTask<UniTaskVoid> PlatformerAsync()
        {
            try
            {
                return new UniTask<UniTaskVoid>(PlatformerAsyncInternal());
            }
            finally
            {
                PlatformerAsyncInternal().Forget();
            }
        }
    }
}
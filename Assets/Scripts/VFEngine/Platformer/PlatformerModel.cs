using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
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
        private RaycastModel raycast;
        private RaycastHitColliderModel raycastHitCollider;
        private const string AssetPath = "DefaultPlatformerModel.asset";

        /* fields: methods */
        private void InitializeData()
        {
            p.Initialize();
        }

        private void InitializeModel()
        {
            physics = p.PhysicsModel;
            raycast = p.RaycastModel;
            raycastHitCollider = p.RaycastHitColliderModel;
        }

        private async UniTaskVoid PlatformerAsyncInternal()
        {
            await ApplyGravityAsync();
            await InitializeFrameAsync();
            await TestMovingPlatformAsync();
            await SetMovementDirectionAsync();
            // Cast Rays
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
            var rTask1 = raycast.SetRaysParametersAsync();
            var rchTask1 = raycastHitCollider.ClearContactListAsync();
            var rchTask2 = raycastHitCollider.SetWasGroundedLastFrameAsync();
            var rchTask3 = raycastHitCollider.SetStandingOnLastFrameAsync();
            var rchTask4 = raycastHitCollider.SetWasTouchingCeilingLastFrameAsync();
            var rchTask5 = raycastHitCollider.SetCurrentWallColliderAsync();
            var rchTask6 = raycastHitCollider.ResetStateAsync();
            var task1 = await (pTask1, rTask1, rchTask1, rchTask2, rchTask3, rchTask4, rchTask5);
            var task2 = await (pTask2, rchTask6);
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
                    var rchTask = raycastHitCollider.SetMovingPlatformGravityAsync();
                    var rTask = raycast.SetRaysParametersAsync();
                    var task = await (pTask1, pTask2, rchTask, rTask);
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
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Physics.Gravity;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics
{
    using static Debug;
    using static PhysicsData;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [RequireComponent(typeof(GravityController))]
    public class PhysicsController : MonoBehaviour, IController
    {
        /* fields: dependencies */
        [SerializeField] private PhysicsModel model;

        /* fields: methods */
        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as PhysicsModel;
            Assert(model != null, nameof(model) + " != null");
            model.OnInitialize();
        }

        public void SetCurrentGravity()
        {
            model.OnSetCurrentCurrentGravity();
        }

        public void ApplyAscentMultiplierToCurrentGravity()
        {
            model.OnApplyAscentMultiplierToCurrentGravity();
        }
        
        public void ApplyFallMultiplierToCurrentGravity()
        {
            model.OnApplyFallMultiplierToCurrentGravity();
        }
        
        public void ApplyGravityToVerticalSpeed()
        {
            model.OnApplyGravityToVerticalSpeed();
        }
        
        public void ApplyFallSlowFactorToVerticalSpeed()
        {
            model.OnApplyFallSlowFactorToVerticalSpeed();
        }

        public async UniTaskVoid SetNewPosition()
        {
            model.OnSetNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ResetState()
        {
            model.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void TranslatePlatformSpeedToTransform()
        {
            model.OnTranslatePlatformSpeedToTransform();
        }

        public async UniTaskVoid DisableGravity()
        {
            model.OnDisableGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplyMovingPlatformSpeedToNewPosition()
        {
            model.OnApplyMovingPlatformSpeedToNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopHorizontalSpeed()
        {
            model.OnStopHorizontalSpeed();
        }

        public void SetForcesApplied()
        {
            model.OnSetForcesApplied();
        }

        public void SetHorizontalMovementDirectionToStored()
        {
            model.OnSetHorizontalMovementDirectionToStored();
        }
        public void SetNegativeHorizontalMovementDirection()
        {
            model.OnSetNegativeHorizontalMovementDirection();
        }

        public void SetPositiveHorizontalMovementDirection()
        {
            model.OnSetPositiveHorizontalMovementDirection();
        }

        public void ApplyPlatformSpeedToHorizontalMovementDirection()
        {
            model.OnApplyPlatformSpeedToHorizontalMovementDirection();
        }

        public void SetStoredHorizontalMovementDirection()
        {
            model.OnSetStoredHorizontalMovementDirection();
        }
    }
}
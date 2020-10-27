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

        public void StopHorizontalSpeedOnPlatformTest()
        {
            model.OnStopHorizontalSpeedOnPlatformTest();
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

        public void SetNewPositiveHorizontalPosition()
        {
            model.OnSetNewPositiveHorizontalPosition();
        }

        public void SetNewNegativeHorizontalPosition()
        {
            model.OnSetNewNegativeHorizontalPosition();
        }

        public async UniTaskVoid StopHorizontalSpeed()
        {
            model.OnStopHorizontalSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopNewHorizontalPosition()
        {
            model.OnStopNewHorizontalPosition();
        }

        public void SetIsFalling()
        {
            model.OnSetIsFalling();
        }

        public async UniTaskVoid SetIsNotFalling()
        {
            model.OnSetIsNotFalling();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplySpeedToHorizontalNewPosition()
        {
            model.OnApplySpeedToHorizontalNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition()
        {
            model.OnApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
        }

        public void ApplySpeedToVerticalNewPosition()
        {
            model.OnApplySpeedToVerticalNewPosition();
        }

        public void StopNewVerticalPosition()
        {
            model.OnStopNewVerticalPosition();
        }

        public async UniTaskVoid SetGravityActive()
        {
            model.OnSetGravityActive();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition()
        {
            model.OnApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplyLeftStickyRaycastToNewVerticalPosition()
        {
            model.OnApplyLeftStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplyRightStickyRaycastToNewVerticalPosition()
        {
            model.OnApplyRightStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopVerticalSpeed()
        {
            model.OnStopVerticalSpeed();
        }

        public async UniTaskVoid SetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight()
        {
            model.OnSetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopVerticalForce()
        {
            model.OnStopVerticalForce();
        }
    }
}
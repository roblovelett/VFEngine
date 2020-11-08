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
        #region fields

        #region dependencies

        [SerializeField] private PhysicsModel physicsModel;

        #endregion

        #region private methods

        private void Awake()
        {
            if (!physicsModel) physicsModel = LoadData(ModelPath) as PhysicsModel;
            Assert(physicsModel != null, nameof(physicsModel) + " != null");
            physicsModel.OnInitialize();
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void SetCurrentGravity()
        {
            physicsModel.OnSetCurrentCurrentGravity();
        }

        public void ApplyAscentMultiplierToCurrentGravity()
        {
            physicsModel.OnApplyAscentMultiplierToCurrentGravity();
        }

        public void ApplyFallMultiplierToCurrentGravity()
        {
            physicsModel.OnApplyFallMultiplierToCurrentGravity();
        }

        public void ApplyGravityToVerticalSpeed()
        {
            physicsModel.OnApplyGravityToVerticalSpeed();
        }

        public void ApplyFallSlowFactorToVerticalSpeed()
        {
            physicsModel.OnApplyFallSlowFactorToVerticalSpeed();
        }

        public async UniTaskVoid SetNewPosition()
        {
            physicsModel.OnSetNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ResetState()
        {
            physicsModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void TranslatePlatformSpeedToTransform()
        {
            physicsModel.OnTranslatePlatformSpeedToTransform();
        }

        public async UniTaskVoid DisableGravity()
        {
            physicsModel.OnDisableGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplyMovingPlatformSpeedToNewPosition()
        {
            physicsModel.OnApplyMovingPlatformSpeedToNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopHorizontalSpeedOnPlatformTest()
        {
            physicsModel.OnStopHorizontalSpeedOnPlatformTest();
        }

        public void SetForcesApplied()
        {
            physicsModel.OnSetForcesApplied();
        }

        public void SetHorizontalMovementDirectionToStored()
        {
            physicsModel.OnSetHorizontalMovementDirectionToStored();
        }

        public void SetNegativeHorizontalMovementDirection()
        {
            physicsModel.OnSetNegativeHorizontalMovementDirection();
        }

        public void SetPositiveHorizontalMovementDirection()
        {
            physicsModel.OnSetPositiveHorizontalMovementDirection();
        }

        public void ApplyPlatformSpeedToHorizontalMovementDirection()
        {
            physicsModel.OnApplyPlatformSpeedToHorizontalMovementDirection();
        }

        public void SetStoredHorizontalMovementDirection()
        {
            physicsModel.OnSetStoredHorizontalMovementDirection();
        }

        public void SetNewPositiveHorizontalPosition()
        {
            physicsModel.OnSetNewPositiveHorizontalPosition();
        }

        public void SetNewNegativeHorizontalPosition()
        {
            physicsModel.OnSetNewNegativeHorizontalPosition();
        }

        public async UniTaskVoid StopHorizontalSpeed()
        {
            physicsModel.OnStopHorizontalSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopNewHorizontalPosition()
        {
            physicsModel.OnStopNewHorizontalPosition();
        }

        public void SetIsFalling()
        {
            physicsModel.OnSetIsFalling();
        }

        public async UniTaskVoid SetIsNotFalling()
        {
            physicsModel.OnSetIsNotFalling();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplySpeedToHorizontalNewPosition()
        {
            physicsModel.OnApplySpeedToHorizontalNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition()
        {
            physicsModel.OnApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
        }

        public void ApplySpeedToVerticalNewPosition()
        {
            physicsModel.OnApplySpeedToVerticalNewPosition();
        }

        public void StopNewVerticalPosition()
        {
            physicsModel.OnStopNewVerticalPosition();
        }

        public async UniTaskVoid SetGravityActive()
        {
            physicsModel.OnSetGravityActive();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition()
        {
            physicsModel.OnApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplyLeftStickyRaycastToNewVerticalPosition()
        {
            physicsModel.OnApplyLeftStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplyRightStickyRaycastToNewVerticalPosition()
        {
            physicsModel.OnApplyRightStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopVerticalSpeed()
        {
            physicsModel.OnStopVerticalSpeed();
        }

        public async UniTaskVoid SetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight()
        {
            physicsModel.OnSetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopVerticalForce()
        {
            physicsModel.OnStopVerticalForce();
        }

        public void StopNewPosition()
        {
            physicsModel.OnStopNewPosition();
        }

        public void SetNewSpeed()
        {
            physicsModel.OnSetNewSpeed();
        }

        public void ApplySlopeAngleSpeedFactorToHorizontalSpeed()
        {
            physicsModel.OnApplySlopeAngleSpeedFactorToHorizontalSpeed();
        }

        public void ClampSpeedToMaxVelocity()
        {
            physicsModel.OnClampSpeedToMaxVelocity();
        }

        public void OnContactListHit()
        {
            physicsModel.OnContactListHit();
        }

        public async UniTaskVoid StopExternalForce()
        {
            physicsModel.OnStopExternalForce();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetWorldSpeedToSpeed()
        {
            physicsModel.OnSetWorldSpeedToSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
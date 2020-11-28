using Cysharp.Threading.Tasks;
using UnityEngine;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics
{
    using static UniTaskExtensions;

    public class PhysicsController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsModel physicsModel;

        #endregion

        #region private methods

        private void LoadCharacter()
        {
            if (!character) character = transform.parent.gameObject;
        }

        private void LoadPhysicsModel()
        {
            physicsModel = new PhysicsModel();
        }

        private void InitializePhysicsData()
        {
            PhysicsModel.OnInitializeData();
        }

        private void PlatformerInitializeData()
        {
            LoadCharacter();
            LoadPhysicsModel();
            InitializePhysicsData();
        }

        #endregion

        #endregion

        #region properties

        public GameObject Character => character;
        public PhysicsModel PhysicsModel => physicsModel;

        #region public methods

        #region platformer

        public void OnPlatformerInitializeData()
        {
            PlatformerInitializeData();
        }

        #endregion

        #region physics model

        public void SetCurrentGravity()
        {
            PhysicsModel.OnSetCurrentCurrentGravity();
        }

        public void ApplyAscentMultiplierToCurrentGravity()
        {
            PhysicsModel.OnApplyAscentMultiplierToCurrentGravity();
        }

        public void ApplyFallMultiplierToCurrentGravity()
        {
            PhysicsModel.OnApplyFallMultiplierToCurrentGravity();
        }

        public void ApplyGravityToVerticalSpeed()
        {
            PhysicsModel.OnApplyGravityToVerticalSpeed();
        }

        public void ApplyFallSlowFactorToVerticalSpeed()
        {
            PhysicsModel.OnApplyFallSlowFactorToVerticalSpeed();
        }

        public async UniTaskVoid SetNewPosition()
        {
            PhysicsModel.OnSetNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ResetState()
        {
            PhysicsModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void TranslatePlatformSpeedToTransform()
        {
            PhysicsModel.OnTranslatePlatformSpeedToTransform();
        }

        public async UniTaskVoid DisableGravity()
        {
            PhysicsModel.OnDisableGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplyMovingPlatformSpeedToNewPosition()
        {
            PhysicsModel.OnApplyMovingPlatformSpeedToNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopHorizontalSpeedOnPlatformTest()
        {
            PhysicsModel.OnStopHorizontalSpeedOnPlatformTest();
        }

        public void SetForcesApplied()
        {
            PhysicsModel.OnSetForcesApplied();
        }

        public void SetHorizontalMovementDirectionToStored()
        {
            PhysicsModel.OnSetHorizontalMovementDirectionToStored();
        }

        public void SetNegativeHorizontalMovementDirection()
        {
            PhysicsModel.OnSetNegativeHorizontalMovementDirection();
        }

        public void SetPositiveHorizontalMovementDirection()
        {
            PhysicsModel.OnSetPositiveHorizontalMovementDirection();
        }

        public void ApplyPlatformSpeedToHorizontalMovementDirection()
        {
            PhysicsModel.OnApplyPlatformSpeedToHorizontalMovementDirection();
        }

        public void SetStoredHorizontalMovementDirection()
        {
            PhysicsModel.OnSetStoredHorizontalMovementDirection();
        }

        public void SetNewPositiveHorizontalPosition()
        {
            PhysicsModel.OnSetNewPositiveHorizontalPosition();
        }

        public void SetNewNegativeHorizontalPosition()
        {
            PhysicsModel.OnSetNewNegativeHorizontalPosition();
        }

        public async UniTaskVoid StopHorizontalSpeed()
        {
            PhysicsModel.OnStopHorizontalSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopNewHorizontalPosition()
        {
            PhysicsModel.OnStopNewHorizontalPosition();
        }

        public void SetIsFalling()
        {
            PhysicsModel.OnSetIsFalling();
        }

        public async UniTaskVoid SetIsNotFalling()
        {
            PhysicsModel.OnSetIsNotFalling();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplySpeedToHorizontalNewPosition()
        {
            PhysicsModel.OnApplySpeedToHorizontalNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition()
        {
            PhysicsModel.OnApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
        }

        public void ApplySpeedToVerticalNewPosition()
        {
            PhysicsModel.OnApplySpeedToVerticalNewPosition();
        }

        public void StopNewVerticalPosition()
        {
            PhysicsModel.OnStopNewVerticalPosition();
        }

        public async UniTaskVoid SetGravityActive()
        {
            PhysicsModel.OnSetGravityActive();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition()
        {
            PhysicsModel.OnApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplyLeftStickyRaycastToNewVerticalPosition()
        {
            PhysicsModel.OnApplyLeftStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ApplyRightStickyRaycastToNewVerticalPosition()
        {
            PhysicsModel.OnApplyRightStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopVerticalSpeed()
        {
            PhysicsModel.OnStopVerticalSpeed();
        }

        public async UniTaskVoid SetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight()
        {
            PhysicsModel.OnSetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void StopVerticalForce()
        {
            PhysicsModel.OnStopVerticalForce();
        }

        public void StopNewPosition()
        {
            PhysicsModel.OnStopNewPosition();
        }

        public void SetNewSpeed()
        {
            PhysicsModel.OnSetNewSpeed();
        }

        public void ApplySlopeAngleSpeedFactorToHorizontalSpeed()
        {
            PhysicsModel.OnApplySlopeAngleSpeedFactorToHorizontalSpeed();
        }

        public void ClampSpeedToMaxVelocity()
        {
            PhysicsModel.OnClampSpeedToMaxVelocity();
        }

        public void OnContactListHit()
        {
            PhysicsModel.OnContactListHit();
        }

        public async UniTaskVoid StopExternalForce()
        {
            PhysicsModel.OnStopExternalForce();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetWorldSpeedToSpeed()
        {
            PhysicsModel.OnSetWorldSpeedToSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion

        #endregion
    }
}
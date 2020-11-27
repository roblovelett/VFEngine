using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics
{
    using static DebugExtensions;
    using static Quaternion;
    using static Time;
    using static Mathf;
    using static Vector2;
    using static RigidbodyType2D;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "PhysicsModel", menuName = PlatformerPhysicsModelPath, order = 0)]
    [InlineEditor]
    public class PhysicsModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Physics Data")] [SerializeField] private PhysicsData p;
        [SerializeField] private GameObject character;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private BoxcastController boxcastController;
        private RaycastData raycast;
        private UpRaycastData upRaycast;
        private LeftStickyRaycastData leftStickyRaycast;
        private RightStickyRaycastData rightStickyRaycast;
        private SafetyBoxcastData safetyBoxcast;
        private RaycastHitColliderData raycastHitCollider;
        private LeftRaycastHitColliderData leftRaycastHitCollider;
        private RightRaycastHitColliderData rightRaycastHitCollider;
        private DownRaycastHitColliderData downRaycastHitCollider;
        private StickyRaycastHitColliderData stickyRaycastHitCollider;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!p) p = CreateInstance<PhysicsData>();
            if (!raycastController) raycastController = character.GetComponent<RaycastController>();
            if (!raycastHitColliderController) raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
            if (!boxcastController) boxcastController = character.GetComponent<BoxcastController>();
            p.Transform = character.transform;
            if (p.AutomaticGravityControl) p.Transform.rotation = identity;
            p.IsJumping = false;
            p.FallSlowFactor = 0f;
            p.SmallValue = 0.0001f;
            p.MovementDirectionThreshold = p.SmallValue;
            p.CurrentHitRigidBodyCanBePushed = p.CurrentHitRigidBody != null && !p.CurrentHitRigidBody.isKinematic &&
                                               p.CurrentHitRigidBody.bodyType != Static;
            if (p.DisplayWarningsControl) GetWarningMessages();
        }

        private void InitializeModel()
        {
            raycast = raycastController.RaycastModel.Data;
            upRaycast = raycastController.UpRaycastModel.Data;
            leftStickyRaycast = raycastController.LeftStickyRaycastModel.Data;
            rightStickyRaycast = raycastController.RightStickyRaycastModel.Data;
            safetyBoxcast = boxcastController.SafetyBoxcastModel.Data;
            raycastHitCollider = raycastHitColliderController.RaycastHitColliderModel.Data;
            leftRaycastHitCollider = raycastHitColliderController.LeftRaycastHitColliderModel.Data;
            rightRaycastHitCollider = raycastHitColliderController.RightRaycastHitColliderModel.Data;
            downRaycastHitCollider = raycastHitColliderController.DownRaycastHitColliderModel.Data;
            stickyRaycastHitCollider = raycastHitColliderController.StickyRaycastHitColliderModel.Data;
            ResetState();
        }

        private void GetWarningMessages()
        {
            const string ph = "Physics";
            var settings = $"{ph} Settings";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!p.Settings) warningMessage += FieldString($"{settings}", $"{settings}");
            if (!p.Transform) warningMessage += FieldParentString("Transform", $"{settings}");
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldString(string field, string scriptableObject)
            {
                AddWarningMessageCount();
                return FieldMessage(field, scriptableObject);
            }

            string FieldParentString(string field, string scriptableObject)
            {
                AddWarningMessageCount();
                return FieldParentMessage(field, scriptableObject);
            }

            void AddWarningMessageCount()
            {
                warningMessageCount++;
            }
        }

        private void SetCurrentGravity()
        {
            p.CurrentGravity = p.Gravity;
        }

        private void ApplyAscentMultiplierToCurrentGravity()
        {
            p.CurrentGravity /= p.AscentMultiplier;
        }

        private void ApplyFallMultiplierToCurrentGravity()
        {
            p.CurrentGravity *= p.FallMultiplier;
        }

        private void ApplyGravityToVerticalSpeed()
        {
            var gravity = p.CurrentGravity + downRaycastHitCollider.MovingPlatformCurrentGravity;
            p.SpeedY += gravity * deltaTime;
        }

        private void ApplyFallSlowFactorToVerticalSpeed()
        {
            p.SpeedY *= p.FallSlowFactor;
        }

        private void SetNewPosition()
        {
            p.NewPosition = p.Speed * deltaTime;
        }

        private void ResetState()
        {
            p.GravityActive = true;
        }

        private void TranslatePlatformSpeedToTransform()
        {
            p.Transform.Translate(downRaycastHitCollider.MovingPlatformCurrentSpeed * deltaTime);
        }

        private void DisableGravity()
        {
            p.GravityActive = false;
        }

        private void ApplyMovingPlatformSpeedToNewPosition()
        {
            p.NewPositionY = downRaycastHitCollider.MovingPlatformCurrentSpeed.y * deltaTime;
        }

        private void StopHorizontalSpeedOnPlatformTest()
        {
            p.Speed = -p.NewPosition / deltaTime;
            p.SpeedX = -p.SpeedX;
        }

        private void SetForcesApplied()
        {
            p.ForcesApplied = p.Speed;
        }

        private void SetHorizontalMovementDirectionToStored()
        {
            p.HorizontalMovementDirection = p.StoredHorizontalMovementDirection;
        }

        private void SetNegativeHorizontalMovementDirection()
        {
            p.HorizontalMovementDirection = -1;
        }

        private void SetPositiveHorizontalMovementDirection()
        {
            p.HorizontalMovementDirection = 1;
        }

        private void ApplyPlatformSpeedToHorizontalMovementDirection()
        {
            p.HorizontalMovementDirection = (int) Sign(downRaycastHitCollider.MovingPlatformCurrentSpeed.x);
        }

        private void SetStoredHorizontalMovementDirection()
        {
            p.StoredHorizontalMovementDirection = p.HorizontalMovementDirection;
        }

        private void SetNewPositiveHorizontalPosition()
        {
            p.NewPositionX = SetNewHorizontalPosition(true,
                rightRaycastHitCollider.DistanceBetweenRightHitAndRaycastOrigin, raycast.BoundsWidth,
                raycast.RayOffset);
        }

        private void SetNewNegativeHorizontalPosition()
        {
            p.NewPositionX = SetNewHorizontalPosition(false,
                leftRaycastHitCollider.DistanceBetweenLeftHitAndRaycastOrigin, raycast.BoundsWidth, raycast.RayOffset);
        }

        private static float SetNewHorizontalPosition(bool positiveDirection, float distance, float width, float offset)
        {
            var positiveX = -distance + width / 2 + offset * 2;
            var negativeX = distance - width / 2 - offset * 2;
            return positiveDirection ? positiveX : negativeX;
        }

        private void StopHorizontalSpeed()
        {
            p.SpeedX = 0;
        }

        private void StopNewHorizontalPosition()
        {
            p.NewPositionX = 0;
        }

        private void SetIsFalling()
        {
            p.IsFalling = true;
        }

        private void SetIsNotFalling()
        {
            p.IsFalling = false;
        }

        private void ApplySpeedToHorizontalNewPosition()
        {
            p.NewPositionY = p.SpeedY * deltaTime;
        }

        private void ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition()
        {
            p.NewPositionY = -downRaycastHitCollider.CurrentDownHitSmallestDistance + raycast.BoundsHeight / 2 +
                             raycast.RayOffset;
        }

        private void ApplySpeedToVerticalNewPosition()
        {
            p.NewPositionY = p.NewPosition.y + p.Speed.y * deltaTime;
        }

        private void StopNewVerticalPosition()
        {
            p.NewPositionY = 0;
        }

        private void SetGravityActive()
        {
            p.GravityActive = true;
        }

        private void ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition()
        {
            p.NewPositionY = SetNewVerticalPositionWithSafetyAndStickyRaycasts(safetyBoxcast.SafetyBoxcastHit.point.y,
                rightStickyRaycast.RightStickyRaycastOrigin.y, raycast.BoundsHeight);
        }

        private void ApplyLeftStickyRaycastToNewVerticalPosition()
        {
            p.NewPositionY = SetNewVerticalPositionWithSafetyAndStickyRaycasts(
                leftStickyRaycast.LeftStickyRaycastHit.point.y, leftStickyRaycast.LeftStickyRaycastOrigin.y,
                raycast.BoundsHeight);
        }

        private void ApplyRightStickyRaycastToNewVerticalPosition()
        {
            p.NewPositionY = SetNewVerticalPositionWithSafetyAndStickyRaycasts(
                rightStickyRaycast.RightStickyRaycastHit.point.y, rightStickyRaycast.RightStickyRaycastOrigin.y,
                raycast.BoundsHeight);
        }

        private static float SetNewVerticalPositionWithSafetyAndStickyRaycasts(float pointY, float originY,
            float boundsHeight)
        {
            return -Abs(pointY - originY) + boundsHeight / 2;
        }

        private void StopVerticalSpeed()
        {
            p.Speed = new Vector2(p.SpeedX, 0f);
        }

        private void SetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight()
        {
            p.NewPositionY = upRaycast.UpRaycastSmallestDistance - raycast.BoundsHeight / 2;
        }

        private void StopVerticalForce()
        {
            p.SpeedY = 0f;
            p.ExternalForceY = 0f;
        }

        private void StopNewPosition()
        {
            p.NewPosition = zero;
        }

        private void SetNewSpeed()
        {
            p.Speed = p.NewPosition / deltaTime;
        }

        private void ApplySlopeAngleSpeedFactorToHorizontalSpeed()
        {
            p.SpeedX *= p.SlopeAngleSpeedFactor.Evaluate(
                Abs(stickyRaycastHitCollider.BelowSlopeAngle) * Sign(p.Speed.y));
        }

        private void ClampSpeedToMaxVelocity()
        {
            p.SpeedX = Clamp(p.Speed.x, -p.MaximumVelocity.x, p.MaximumVelocity.x);
            p.SpeedY = Clamp(p.Speed.y, -p.MaximumVelocity.y, p.MaximumVelocity.y);
        }

        private void ContactListHit()
        {
            if (!p.Physics2DInteractionControl) return;
            foreach (var hit in raycastHitCollider.ContactList.List)
            {
                p.CurrentHitRigidBody = hit.collider.attachedRigidbody;
                if (!p.CurrentHitRigidBodyCanBePushed) return;
                p.CurrentPushDirection = new Vector2(p.ExternalForce.x, 0);
                p.CurrentHitRigidBody.velocity = p.CurrentPushDirection.normalized * p.Physics2DPushForce;
            }
        }

        private void StopExternalForce()
        {
            p.ExternalForce = zero;
        }

        private void SetWorldSpeedToSpeed()
        {
            p.WorldSpeed = p.Speed;
        }

        private void SlowFall()
        {
            p.FallSlowFactor = p.CurrentVerticalSpeedFactor;
        }

        #endregion

        #endregion

        #region properties

        public PhysicsData Data => p;

        #region public methods

        public void OnInitializeData()
        {
            InitializeData();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetCurrentCurrentGravity()
        {
            SetCurrentGravity();
        }

        public void OnApplyAscentMultiplierToCurrentGravity()
        {
            ApplyAscentMultiplierToCurrentGravity();
        }

        public void OnApplyFallMultiplierToCurrentGravity()
        {
            ApplyFallMultiplierToCurrentGravity();
        }

        public void OnApplyGravityToVerticalSpeed()
        {
            ApplyGravityToVerticalSpeed();
        }

        public void OnApplyFallSlowFactorToVerticalSpeed()
        {
            ApplyFallSlowFactorToVerticalSpeed();
        }

        public void OnSetNewPosition()
        {
            SetNewPosition();
        }

        public void OnResetState()
        {
            ResetState();
        }

        public void OnTranslatePlatformSpeedToTransform()
        {
            TranslatePlatformSpeedToTransform();
        }

        public void OnDisableGravity()
        {
            DisableGravity();
        }

        public void OnApplyMovingPlatformSpeedToNewPosition()
        {
            ApplyMovingPlatformSpeedToNewPosition();
        }

        public void OnStopHorizontalSpeedOnPlatformTest()
        {
            StopHorizontalSpeedOnPlatformTest();
        }

        public void OnSetForcesApplied()
        {
            SetForcesApplied();
        }

        public void OnSetHorizontalMovementDirectionToStored()
        {
            SetHorizontalMovementDirectionToStored();
        }

        public void OnSetNegativeHorizontalMovementDirection()
        {
            SetNegativeHorizontalMovementDirection();
        }

        public void OnSetPositiveHorizontalMovementDirection()
        {
            SetPositiveHorizontalMovementDirection();
        }

        public void OnApplyPlatformSpeedToHorizontalMovementDirection()
        {
            ApplyPlatformSpeedToHorizontalMovementDirection();
        }

        public void OnSetStoredHorizontalMovementDirection()
        {
            SetStoredHorizontalMovementDirection();
        }

        public void OnSetNewPositiveHorizontalPosition()
        {
            SetNewPositiveHorizontalPosition();
        }

        public void OnSetNewNegativeHorizontalPosition()
        {
            SetNewNegativeHorizontalPosition();
        }

        public void OnStopHorizontalSpeed()
        {
            StopHorizontalSpeed();
        }

        public void OnStopNewHorizontalPosition()
        {
            StopNewHorizontalPosition();
        }

        public void OnSetIsFalling()
        {
            SetIsFalling();
        }

        public void OnSetIsNotFalling()
        {
            SetIsNotFalling();
        }

        public void OnApplySpeedToHorizontalNewPosition()
        {
            ApplySpeedToHorizontalNewPosition();
        }

        public void OnApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition()
        {
            ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition();
        }

        public void OnApplySpeedToVerticalNewPosition()
        {
            ApplySpeedToVerticalNewPosition();
        }

        public void OnStopNewVerticalPosition()
        {
            StopNewVerticalPosition();
        }

        public void OnSetGravityActive()
        {
            SetGravityActive();
        }

        public void OnApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition()
        {
            ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition();
        }

        public void OnApplyLeftStickyRaycastToNewVerticalPosition()
        {
            ApplyLeftStickyRaycastToNewVerticalPosition();
        }

        public void OnApplyRightStickyRaycastToNewVerticalPosition()
        {
            ApplyRightStickyRaycastToNewVerticalPosition();
        }

        public void OnStopVerticalSpeed()
        {
            StopVerticalSpeed();
        }

        public void OnSetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight()
        {
            SetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight();
        }

        public void OnStopVerticalForce()
        {
            StopVerticalForce();
        }

        public void OnStopNewPosition()
        {
            StopNewPosition();
        }

        public void OnSetNewSpeed()
        {
            SetNewSpeed();
        }

        public void OnApplySlopeAngleSpeedFactorToHorizontalSpeed()
        {
            ApplySlopeAngleSpeedFactorToHorizontalSpeed();
        }

        public void OnClampSpeedToMaxVelocity()
        {
            ClampSpeedToMaxVelocity();
        }

        public void OnContactListHit()
        {
            ContactListHit();
        }

        public void OnStopExternalForce()
        {
            StopExternalForce();
        }

        public void OnSetWorldSpeedToSpeed()
        {
            SetWorldSpeedToSpeed();
        }

        public void OnSlowFall()
        {
            SlowFall();
        }

        #endregion

        #endregion
    }
}
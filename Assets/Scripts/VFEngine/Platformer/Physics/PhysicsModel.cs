using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics
{
    using static DebugExtensions;
    using static Quaternion;
    using static UniTaskExtensions;
    using static Time;
    using static Mathf;
    using static Vector2;
    using static RigidbodyType2D;

    [CreateAssetMenu(fileName = "PhysicsModel", menuName = "VFEngine/Platformer/Physics/Physics Model", order = 0)]
    public class PhysicsModel : ScriptableObject, IModel
    {
        /* fields: dependencies */
        [LabelText("Physics Data")] [SerializeField]
        private PhysicsData p;

        /* fields */

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
            p.CurrentHitRigidBodyCanBePushed = SetCurrentHitRigidBodyCanBePushed(p.CurrentHitRigidBody);
            p.TransformRef = p.Transform;
            p.SpeedRef = p.Speed;
            p.GravityActiveRef = p.state.GravityActive;
            p.FallSlowFactorRef = p.FallSlowFactor;
            p.HorizontalMovementDirectionRef = p.HorizontalMovementDirection;
            p.MaximumSlopeAngleRef = p.MaximumSlopeAngle;
            p.NewPositionRef = p.NewPosition;
            p.SmallValueRef = p.SmallValue;
            p.GravityRef = p.Gravity;
            p.StickToSlopesControlRef = p.StickToSlopesControl;
            p.IsJumpingRef = p.state.IsJumping;
            p.SafetyBoxcastControlRef = p.SafetyBoxcastControl;
            p.ExternalForceRef = p.ExternalForce;
            p.ExternalForceXRef = p.ExternalForceX;
            p.ExternalForceYRef = p.ExternalForceY;
            p.state.Reset();
            if (p.AutomaticGravityControl && !p.HasGravityController) p.Transform.rotation = identity;
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private static bool SetCurrentHitRigidBodyCanBePushed(Rigidbody2D body)
        {
            return body != null && !body.isKinematic && body.bodyType != Static;
        }

        private async UniTaskVoid GetWarningMessages()
        {
            if (p.DisplayWarnings)
            {
                const string ph = "Physics";
                var settings = $"{ph} Settings";
                var warningMessage = "";
                var warningMessageCount = 0;
                if (!p.HasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
                if (!p.HasGravityController) warningMessage += FieldParentString("Gravity Controller", $"{settings}");
                if (!p.HasTransform) warningMessage += FieldParentString("Transform", $"{settings}");
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

            await SetYieldOrSwitchToThreadPoolAsync();
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
            var gravity = p.CurrentGravity + p.MovingPlatformCurrentGravity;
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
            p.state.Reset();
        }

        private void TranslatePlatformSpeedToTransform()
        {
            p.Transform.Translate(p.MovingPlatformCurrentSpeed * deltaTime);
        }

        private void DisableGravity()
        {
            p.state.SetGravityActive(false);
        }

        private void ApplyMovingPlatformSpeedToNewPosition()
        {
            p.NewPositionY = p.MovingPlatformCurrentSpeed.y * deltaTime;
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
            p.HorizontalMovementDirection = (int) Sign(p.MovingPlatformCurrentSpeed.x);
        }

        private void SetStoredHorizontalMovementDirection()
        {
            p.StoredHorizontalMovementDirection = p.HorizontalMovementDirection;
        }

        private void SetNewPositiveHorizontalPosition()
        {
            p.NewPositionX = SetNewHorizontalPosition(true, p.DistanceBetweenRightHitAndRaycastOrigin, p.BoundsWidth, p.RayOffset);
        }

        private void SetNewNegativeHorizontalPosition()
        {
            p.NewPositionX = SetNewHorizontalPosition(false, p.DistanceBetweenLeftHitAndRaycastOrigin, p.BoundsWidth, p.RayOffset);
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
            p.state.SetIsFalling(true);
        }

        private void SetIsNotFalling()
        {
            p.state.SetIsFalling(false);
        }

        private void ApplySpeedToHorizontalNewPosition()
        {
            p.NewPositionY = p.SpeedY * deltaTime;
        }

        private void ApplyHalfBoundsHeightAndRayOffsetToNegativeVerticalNewPosition()
        {
            p.NewPositionY = -p.DistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint +
                             p.BoundsHeight / 2 + p.RayOffset;
        }

        private void ApplySpeedToVerticalNewPosition()
        {
            p.NewPositionY += p.SpeedY * deltaTime;
        }

        private void StopNewVerticalPosition()
        {
            p.NewPositionY = 0;
        }

        private void SetGravityActive()
        {
            p.state.SetGravityActive(true);
        }

        private void ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition()
        {
            p.NewPositionY = SetNewVerticalPositionWithSafetyAndStickyRaycasts(p.SafetyBoxcast.point.y, p.RightStickyRaycastOriginY, p.BoundsHeight);
        }

        private void ApplyLeftStickyRaycastToNewVerticalPosition()
        {
            p.NewPositionY = SetNewVerticalPositionWithSafetyAndStickyRaycasts(p.LeftStickyRaycast.point.y,
                p.LeftStickyRaycastOriginY, p.BoundsHeight);
        }

        private void ApplyRightStickyRaycastToNewVerticalPosition()
        {
            
            p.NewPositionY = SetNewVerticalPositionWithSafetyAndStickyRaycasts(p.RightStickyRaycast.point.y,
                p.RightStickyRaycastOriginY, p.BoundsHeight);
        }

        private static float SetNewVerticalPositionWithSafetyAndStickyRaycasts(float pointY, float originY, float boundsHeight)
        {
            return -Abs(pointY - originY) + boundsHeight / 2;
        }

        private void StopVerticalSpeed()
        {
            p.Speed = new Vector2(p.SpeedX, 0f);
        }

        private void SetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight()
        {
            p.NewPositionY = p.UpRaycastSmallestDistance - p.BoundsHeight / 2;
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
            p.SpeedX *= p.SlopeAngleSpeedFactor.Evaluate(Abs(p.BelowSlopeAngle) * Sign(p.Speed.y));
        }

        private void ClampSpeedToMaxVelocity()
        {
            p.SpeedX = Clamp(p.Speed.x, -p.MaximumVelocity.x, p.MaximumVelocity.x);
            p.SpeedY = Clamp(p.Speed.y, -p.MaximumVelocity.y, p.MaximumVelocity.y);
        }

        private void ContactListHit()
        {
            if (!p.Physics2DInteractionControl) return;
            foreach (var hit in p.ContactList)
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

        /* properties: methods */
        public void OnInitialize()
        {
            Async(Initialize());
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
    }
}
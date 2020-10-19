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
            p.TransformRef = p.Transform;
            p.SpeedRef = p.Speed;
            p.GravityActiveRef = p.state.GravityActive;
            p.FallSlowFactorRef = p.FallSlowFactor;
            p.HorizontalMovementDirectionRef = p.HorizontalMovementDirection;
            p.MaximumSlopeAngleRef = p.MaximumSlopeAngle;
            p.NewRightPositionDistanceRef = p.NewRightPositionDistance;
            p.NewLeftPositionDistanceRef = p.NewLeftPositionDistance;
            p.state.Reset();
            if (p.AutomaticGravityControl && !p.HasGravityController) p.Transform.rotation = identity;
            await SetYieldOrSwitchToThreadPoolAsync();
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
            p.state.SetGravity(false);
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
            p.NewPositionX = SetNewHorizontalPosition(true, p.NewRightPositionDistance, p.BoundsWidth, p.RayOffset);
        }

        private void SetNewNegativeHorizontalPosition()
        {
            p.NewPositionX = SetNewHorizontalPosition(false, p.NewLeftPositionDistance, p.BoundsWidth, p.RayOffset);
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
    }
}
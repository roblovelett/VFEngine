using Cysharp.Threading.Tasks;
using UnityEngine;
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

// ReSharper disable UnusedMember.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics
{
    using static DebugExtensions;
    using static Quaternion;
    using static Time;
    using static Mathf;
    using static Vector2;
    using static RigidbodyType2D;
    using static ScriptableObject;
    using static UniTaskExtensions;
    using static GameObject;

    public class PhysicsController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private PhysicsSettings settings;
        private GameObject character;
        private RaycastController raycastController;
        private RaycastHitColliderController raycastHitColliderController;
        private UpRaycastController upRaycastController;
        private LeftStickyRaycastController leftStickyRaycastController;
        private RightStickyRaycastController rightStickyRaycastController;
        private SafetyBoxcastController safetyBoxcastController;
        private LeftRaycastHitColliderController leftRaycastHitColliderController;
        private RightRaycastHitColliderController rightRaycastHitColliderController;
        private DownRaycastHitColliderController downRaycastHitColliderController;
        private StickyRaycastHitColliderController stickyRaycastHitColliderController;
        private PhysicsData p;
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

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }
        
        private void SetControllers()
        {
            raycastController = GetComponent<RaycastController>();
            raycastHitColliderController = GetComponent<RaycastHitColliderController>();
            upRaycastController = GetComponent<UpRaycastController>();
            leftStickyRaycastController = GetComponent<LeftStickyRaycastController>();
            rightStickyRaycastController = GetComponent<RightStickyRaycastController>();
            safetyBoxcastController = GetComponent<SafetyBoxcastController>();
            leftRaycastHitColliderController = GetComponent<LeftRaycastHitColliderController>();
            rightRaycastHitColliderController = GetComponent<RightRaycastHitColliderController>();
            downRaycastHitColliderController = GetComponent<DownRaycastHitColliderController>();
            stickyRaycastHitColliderController = GetComponent<StickyRaycastHitColliderController>();
        }
        
        private void InitializeData()
        {
            character = Find("Character");
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            p = new PhysicsData
            {
                Transform = character.transform,
                GravityActive = true,
                StoredHorizontalMovementDirection = 1,
                MovementDirectionThreshold = 0.0001f
            };
            p.ApplySettings(settings);
            if (p.AutomaticGravityControl) p.Transform.rotation = identity;
            /*p.CurrentHitRigidBodyCanBePushed = p.CurrentHitRigidBody != null && !p.CurrentHitRigidBody.isKinematic && p.CurrentHitRigidBody.bodyType != Static;*/
        }

        /*private void GetWarningMessages()
        {
            const string ph = "Physics";
            var physicsSettings = $"{ph} Settings";
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!settings) warningMessage += FieldString($"{physicsSettings}", $"{physicsSettings}");
            if (!p.Transform) warningMessage += FieldParentString("Transform", $"{physicsSettings}");
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
        }*/

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            raycast = raycastController.Data;
            upRaycast = upRaycastController.Data;
            leftStickyRaycast = leftStickyRaycastController.Data;
            rightStickyRaycast = rightStickyRaycastController.Data;
            safetyBoxcast = safetyBoxcastController.Data;
            raycastHitCollider = raycastHitColliderController.Data;
            leftRaycastHitCollider = leftRaycastHitColliderController.Data;
            rightRaycastHitCollider = rightRaycastHitColliderController.Data;
            downRaycastHitCollider = downRaycastHitColliderController.Data;
            stickyRaycastHitCollider = stickyRaycastHitColliderController.Data;
        }

        private void Initialize()
        {
            ResetState();
        }
        
        private void ResetState()
        {
            p.IsFalling = true;
        }

        private void PlatformerApplyGravity()
        {
            SetCurrentGravity();
            if (p.Speed.y > 0) ApplyAscentMultiplierToCurrentGravity();
            if (p.Speed.y < 0) ApplyFallMultiplierToCurrentGravity();
            if (p.GravityActive) ApplyGravityToVerticalSpeed();
            if (p.FallSlowFactor != 0) ApplyFallSlowFactorToVerticalSpeed();
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

        private void PlatformerInitializeFrame()
        {
            SetNewPosition();
            ResetState();
        }

        private void SetNewPosition()
        {
            p.NewPosition = p.Speed * deltaTime;
        }

        private void TranslatePlatformSpeedToTransform()
        {
            p.Transform.Translate(downRaycastHitCollider.MovingPlatformCurrentSpeed * deltaTime);
        }

        private void PlatformerTestMovingPlatform()
        {
            DisableGravity();
            ApplyMovingPlatformSpeedToNewPosition();
            StopHorizontalSpeedOnPlatformTest();
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
        private void SetLeftMovementDirection()
        {
            p.HorizontalMovementDirection = -1;
        }

        private void SetRightMovementDirection()
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
            foreach (var hit in raycastHitCollider.ContactList)
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
        
        #region platformer

        public void OnPlatformerApplyGravity()
        {
            PlatformerApplyGravity();
        }

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }

        public void OnTranslatePlatformSpeedToTransform()
        {
            TranslatePlatformSpeedToTransform();
        }
        
        public void OnPlatformerTestMovingPlatform()
        {
            PlatformerTestMovingPlatform();
        }
        
        public void OnSetForcesApplied()
        {
            SetForcesApplied();
        }
        
        public void OnSetHorizontalMovementDirectionToStored()
        {
            SetHorizontalMovementDirectionToStored();
        }

        public void OnSetLeftMovementDirection()
        {
            SetLeftMovementDirection();
        }

        public void OnSetRightMovementDirection()
        {
            SetRightMovementDirection();
        }

        public void OnApplyPlatformSpeedToHorizontalMovementDirection()
        {
            ApplyPlatformSpeedToHorizontalMovementDirection();
        }

        public void OnSetStoredHorizontalMovementDirection()
        {
            SetStoredHorizontalMovementDirection();
        }
        
        #endregion

        #region physics model

        /*public void OnSetCurrentCurrentGravity()
        {
            SetCurrentGravity();
        }*/

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

        public async UniTaskVoid OnSetNewPosition()
        {
            SetNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }
        
        public async UniTaskVoid OnDisableGravity()
        {
            DisableGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnApplyMovingPlatformSpeedToNewPosition()
        {
            ApplyMovingPlatformSpeedToNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnStopHorizontalSpeedOnPlatformTest()
        {
            StopHorizontalSpeedOnPlatformTest();
        }

        public void OnSetNewPositiveHorizontalPosition()
        {
            SetNewPositiveHorizontalPosition();
        }

        public void OnSetNewNegativeHorizontalPosition()
        {
            SetNewNegativeHorizontalPosition();
        }

        public async UniTaskVoid OnStopHorizontalSpeed()
        {
            StopHorizontalSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnStopNewHorizontalPosition()
        {
            StopNewHorizontalPosition();
        }

        public void OnSetIsFalling()
        {
            SetIsFalling();
        }

        public async UniTaskVoid OnSetIsNotFalling()
        {
            SetIsNotFalling();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnApplySpeedToHorizontalNewPosition()
        {
            ApplySpeedToHorizontalNewPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
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

        public async UniTaskVoid OnSetGravityActive()
        {
            SetGravityActive();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition()
        {
            ApplySafetyBoxcastAndRightStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnApplyLeftStickyRaycastToNewVerticalPosition()
        {
            ApplyLeftStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnApplyRightStickyRaycastToNewVerticalPosition()
        {
            ApplyRightStickyRaycastToNewVerticalPosition();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnStopVerticalSpeed()
        {
            StopVerticalSpeed();
        }

        public async UniTaskVoid OnSetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight()
        {
            SetNewVerticalPositionWithUpRaycastSmallestDistanceAndBoundsHeight();
            await SetYieldOrSwitchToThreadPoolAsync();
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

        public async UniTaskVoid OnStopExternalForce()
        {
            StopExternalForce();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetWorldSpeedToSpeed()
        {
            SetWorldSpeedToSpeed();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSlowFall()
        {
            SlowFall();
        }

        #endregion

        #endregion

        #endregion
    }
}
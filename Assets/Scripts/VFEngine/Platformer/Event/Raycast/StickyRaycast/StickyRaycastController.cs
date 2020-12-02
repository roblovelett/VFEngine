using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static Mathf;
    using static DebugExtensions;
    using static ScriptableObject;
    using static UniTaskExtensions;

    public class StickyRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private StickyRaycastSettings settings;
        private GameObject character;
        private PhysicsController physicsController;
        private RaycastController raycastController;
        private RightStickyRaycastController rightStickyRaycastController;
        private LeftStickyRaycastController leftStickyRaycastController;
        private StickyRaycastHitColliderController stickyRaycastHitColliderController;
        private LeftStickyRaycastHitColliderController leftStickyRaycastHitColliderController;
        private RightStickyRaycastHitColliderController rightStickyRaycastHitColliderController;
        private StickyRaycastData s;
        private PhysicsData physics;
        private RaycastData raycast;
        private RightStickyRaycastData rightStickyRaycast;
        private LeftStickyRaycastData leftStickyRaycast;
        private StickyRaycastHitColliderData stickyRaycastHitCollider;
        private LeftStickyRaycastHitColliderData leftStickyRaycastHitCollider;
        private RightStickyRaycastHitColliderData rightStickyRaycastHitCollider;

        #endregion

        #region private methods

        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
            if (s.DisplayWarningsControl) GetWarningMessages();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<StickyRaycastSettings>();
            s = new StickyRaycastData();
            s.ApplySettings(settings);
        }

        private void SetControllers()
        {
            physicsController = character.GetComponentNoAllocation<PhysicsController>();
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            rightStickyRaycastController = character.GetComponentNoAllocation<RightStickyRaycastController>();
            leftStickyRaycastController = character.GetComponentNoAllocation<LeftStickyRaycastController>();
            stickyRaycastHitColliderController =
                character.GetComponentNoAllocation<StickyRaycastHitColliderController>();
            leftStickyRaycastHitColliderController =
                character.GetComponentNoAllocation<LeftStickyRaycastHitColliderController>();
            rightStickyRaycastHitColliderController =
                character.GetComponentNoAllocation<RightStickyRaycastHitColliderController>();
        }

        private void GetWarningMessages()
        {
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!settings) warningMessage += FieldMessage("Settings", "Raycast Settings");
            if (!physics.StickToSlopesControl)
                warningMessage += FieldMessage("Sticky Raycast Control", "Bool Reference");
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldMessage(string field, string scriptableObject)
            {
                warningMessageCount++;
                return $"{field} field not set to {scriptableObject} ScriptableObject.@";
            }
        }

        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            raycast = raycastController.Data;
            rightStickyRaycast = rightStickyRaycastController.Data;
            leftStickyRaycast = leftStickyRaycastController.Data;
            stickyRaycastHitCollider = stickyRaycastHitColliderController.Data;
            leftStickyRaycastHitCollider = leftStickyRaycastHitColliderController.Data;
            rightStickyRaycastHitCollider = rightStickyRaycastHitColliderController.Data;
        }

        private void SetStickyRaycastLength()
        {
            s.StickyRaycastLength = SetStickyRaycastLength(raycast.BoundsWidth, physics.MaximumSlopeAngle,
                raycast.BoundsHeight, raycast.RayOffset);
        }

        private void SetStickyRaycastLengthToSelf()
        {
            s.StickyRaycastLength = s.StickyRaycastLength;
        }

        private static float SetStickyRaycastLength(float boundsWidth, float slopeAngle, float boundsHeight,
            float offset)
        {
            return boundsWidth * Abs(Tan(slopeAngle)) * 2 + boundsHeight / 2 * offset;
        }

        private void SetDoNotCastFromLeft()
        {
            s.IsCastingLeft = false;
        }

        private void SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            s.IsCastingLeft = Abs(leftStickyRaycastHitCollider.BelowSlopeAngleLeft) >
                              Abs(rightStickyRaycastHitCollider.BelowSlopeAngleRight);
        }

        private void SetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            s.IsCastingLeft = stickyRaycastHitCollider.BelowSlopeAngle < 0f;
        }

        private void SetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            s.IsCastingLeft = rightStickyRaycastHitCollider.BelowSlopeAngleRight < 0f;
        }

        private void SetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            s.IsCastingLeft = leftStickyRaycastHitCollider.BelowSlopeAngleLeft < 0f;
        }

        private void SetCastFromLeftWithLeftDistanceLtRightDistance()
        {
            s.IsCastingLeft = leftStickyRaycast.LeftStickyRaycastHit.distance <
                              rightStickyRaycast.RightStickyRaycastHit.distance;
        }

        private void ResetState()
        {
            s.IsCastingLeft = false;
        }

        #endregion

        #endregion

        #region properties

        public StickyRaycastData Data => s;

        #region public methods

        public async UniTaskVoid OnSetStickyRaycastLength()
        {
            SetStickyRaycastLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetStickyRaycastLengthToSelf()
        {
            SetStickyRaycastLengthToSelf();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetDoNotCastFromLeft()
        {
            SetDoNotCastFromLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
        }

        public async UniTaskVoid OnSetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            SetCastFromLeftWithBelowSlopeAngleLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            SetCastFromLeftWithBelowSlopeAngleRightLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            SetCastFromLeftWithBelowSlopeAngleLeftLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetCastFromLeftWithLeftDistanceLtRightDistance()
        {
            SetCastFromLeftWithLeftDistanceLtRightDistance();
        }

        public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
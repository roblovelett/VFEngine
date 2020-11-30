using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
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
    using static UniTaskExtensions;
    using static ScriptableObject;

   
    public class StickyRaycastController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private StickyRaycastSettings settings;
        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
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
            //if (p.DisplayWarningsControl) GetWarningMessages();
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
            if (!raycastController && character)
            {
                raycastController = character.GetComponent<RaycastController>();
            }
            else if (raycastController && !character)
            {
                character = raycastController.Character;
                raycastController = character.GetComponent<RaycastController>();
            }

            if (!physicsController) physicsController = character.GetComponent<PhysicsController>();
            if (!raycastHitColliderController)
                raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            rightStickyRaycast = raycastController.RightStickyRaycastModel.Data;
            leftStickyRaycast = raycastController.LeftStickyRaycastModel.Data;
            stickyRaycastHitCollider = raycastHitColliderController.StickyRaycastHitColliderModel.Data;
            leftStickyRaycastHitCollider = raycastHitColliderController.LeftStickyRaycastHitColliderModel.Data;
            rightStickyRaycastHitCollider = raycastHitColliderController.RightStickyRaycastHitColliderModel.Data;
            if (s.DisplayWarningsControl) GetWarningMessages();
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

        public void OnInitializeData()
        {
            InitializeData();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetStickyRaycastLength()
        {
            SetStickyRaycastLength();
        }

        public void OnSetStickyRaycastLengthToSelf()
        {
            SetStickyRaycastLengthToSelf();
        }

        public void OnSetDoNotCastFromLeft()
        {
            SetDoNotCastFromLeft();
        }

        public void OnSetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
        }

        public void OnSetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            SetCastFromLeftWithBelowSlopeAngleLtZero();
        }

        public void OnSetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            SetCastFromLeftWithBelowSlopeAngleRightLtZero();
        }

        public void OnSetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            SetCastFromLeftWithBelowSlopeAngleLeftLtZero();
        }

        public void OnSetCastFromLeftWithLeftDistanceLtRightDistance()
        {
            SetCastFromLeftWithLeftDistanceLtRightDistance();
        }

        public static float OnSetStickyRaycastLength(float boundsWidth, float slopeAngle, float boundsHeight,
            float offset)
        {
            return SetStickyRaycastLength(boundsWidth, slopeAngle, boundsHeight, offset);
        }

        public void OnResetState()
        {
            ResetState();
        }

        #endregion

        #endregion
    }
}
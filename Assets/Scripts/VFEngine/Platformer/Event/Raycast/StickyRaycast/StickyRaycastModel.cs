using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static Mathf;
    using static DebugExtensions;
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "StickyRaycastModel", menuName = PlatformerStickyRaycastModelPath, order = 0)]
    [InlineEditor]
    public class StickyRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private StickyRaycastData s = null;

        #endregion

        #region private methods

        private void InitializeData()
        {
            s = new StickyRaycastData {Character = character};
            /*s.RuntimeData = s.Character.GetComponentNoAllocation<RaycastController>().StickyRaycastRuntimeData;
            s.StickToSlopesOffsetY = s.StickToSlopesOffsetYSetting;
            s.DisplayWarningsControl = s.DisplayWarningsControlSetting;
            s.RuntimeData.SetStickyRaycast(s.IsCastingLeft, s.StickToSlopesOffsetY, s.StickyRaycastLength);*/
        }

        private void InitializeModel()
        {
            /*s.PhysicsRuntimeData = s.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            s.RaycastRuntimeData = s.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            s.RightStickyRaycastRuntimeData = s.Character.GetComponentNoAllocation<RaycastController>()
                .RightStickyRaycastRuntimeData;
            s.LeftStickyRaycastRuntimeData =
                s.Character.GetComponentNoAllocation<RaycastController>().LeftStickyRaycastRuntimeData;
            s.StickyRaycastHitColliderRuntimeData = s.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .StickyRaycastHitColliderRuntimeData;
            s.LeftStickyRaycastHitColliderRuntimeData = s.Character
                .GetComponentNoAllocation<RaycastHitColliderController>().LeftStickyRaycastHitColliderRuntimeData;
            s.RightStickyRaycastHitColliderRuntimeData = s.Character
                .GetComponentNoAllocation<RaycastHitColliderController>().RightStickyRaycastHitColliderRuntimeData;
            s.StickToSlopesControl = s.PhysicsRuntimeData.StickToSlopesControl;
            s.MaximumSlopeAngle = s.PhysicsRuntimeData.MaximumSlopeAngle;
            s.BoundsWidth = s.RaycastRuntimeData.raycast.BoundsWidth;
            s.BoundsHeight = s.RaycastRuntimeData.raycast.BoundsHeight;
            s.RayOffset = s.RaycastRuntimeData.raycast.RayOffset;
            s.RightStickyRaycastHit = s.RightStickyRaycastRuntimeData.rightStickyRaycast.RightStickyRaycastHit;
            s.LeftStickyRaycastHit = s.LeftStickyRaycastRuntimeData.leftStickyRaycast.LeftStickyRaycastHit;
            s.BelowSlopeAngle = s.StickyRaycastHitColliderRuntimeData.stickyRaycastHitCollider.BelowSlopeAngle;
            s.BelowSlopeAngleLeft = s.LeftStickyRaycastHitColliderRuntimeData.leftStickyRaycastHitCollider
                .BelowSlopeAngleLeft;
            s.BelowSlopeAngleRight = s.RightStickyRaycastHitColliderRuntimeData.rightStickyRaycastHitCollider
                .BelowSlopeAngleRight;*/
        }

        private void GetWarningMessages()
        {
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!s.HasSettings) warningMessage += FieldMessage("Settings", "Raycast Settings");
            if (!s.StickToSlopesControl) warningMessage += FieldMessage("Sticky Raycast Control", "Bool Reference");
            if (s.StickyRaycastLength <= 0) warningMessage += GtZeroMessage("Sticky Raycast Length");
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldMessage(string field, string scriptableObject)
            {
                warningMessageCount++;
                return $"{field} field not set to {scriptableObject} ScriptableObject.@";
            }

            string GtZeroMessage(string field)
            {
                warningMessageCount++;
                return $"{field} must be set to value greater than zero.@";
            }
        }

        private void SetStickyRaycastLength()
        {
            s.StickyRaycastLength =
                SetStickyRaycastLength(s.BoundsWidth, s.MaximumSlopeAngle, s.BoundsHeight, s.RayOffset);
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
            s.IsCastingLeft = Abs(s.BelowSlopeAngleLeft) > Abs(s.BelowSlopeAngleRight);
        }

        private void SetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            s.IsCastingLeft = s.BelowSlopeAngle < 0f;
        }

        private void SetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            s.IsCastingLeft = s.BelowSlopeAngleRight < 0f;
        }

        private void SetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            s.IsCastingLeft = s.BelowSlopeAngleLeft < 0f;
        }

        private void SetCastFromLeftWithLeftDistanceLtRightDistance()
        {
            s.IsCastingLeft = s.LeftStickyRaycastHit.distance < s.RightStickyRaycastHit.distance;
        }

        private void ResetState()
        {
            s.IsCastingLeft = false;
        }

        #endregion

        #endregion

        #region properties

        public StickyRaycastRuntimeData RuntimeData => s.RuntimeData;

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitialize()
        {
            if (s.StickToSlopesControl) Initialize();
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
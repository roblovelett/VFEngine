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

    [CreateAssetMenu(fileName = "StickyRaycastModel", menuName = PlatformerStickyRaycastModelPath, order = 0)][InlineEditor]
    public class StickyRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Sticky Raycast Data")] [SerializeField]
        private StickyRaycastData s = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            if (s.DisplayWarningsControl) GetWarningMessages();
            InitializeModel();
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

        private void InitializeModel()
        {
            s.StickToSlopesOffsetY = s.StickToSlopesOffsetYSetting;
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
            s.IsCastingLeft = s.LeftStickyRaycast.distance < s.RightStickyRaycast.distance;
        }

        private void ResetState()
        {
            s.IsCastingLeft = false;
        }

        #endregion

        #endregion

        #region properties

        #region public methods

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
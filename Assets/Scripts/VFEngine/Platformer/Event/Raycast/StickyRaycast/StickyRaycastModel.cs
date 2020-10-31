using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static Mathf;
    using static UniTaskExtensions;
    using static DebugExtensions;
    using static Color;
    using static Vector3;

    [CreateAssetMenu(fileName = "StickyRaycastModel",
        menuName = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Sticky Raycast Model", order = 0)]
    public class StickyRaycastModel : ScriptableObject, IModel
    {
        [SerializeField] private StickyRaycastData sr;

        private void Initialize()
        {
            /*private void GetWarningMessage()
            {
                if (!DisplayWarnings) return;
                var warningMessage = "";
                var warningMessageCount = 0;
                if (!settings) warningMessage += FieldMessage("Settings", "Raycast Settings");
                if (!stickyRaycastControl) warningMessage += FieldMessage("Sticky Raycast Control", "Bool Reference");
                if (StickyRaycastLength <= 0) warningMessage += GtZeroMessage("Sticky Raycast Length");
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
            }*/
            sr.StickToSlopesOffsetYRef = sr.StickToSlopesOffsetY;
            sr.StickyRaycastLengthRef = sr.StickyRaycastLength;
            sr.LeftStickyRaycastRef = sr.LeftStickyRaycast;
            sr.RightStickyRaycastRef = sr.RightStickyRaycast;
            sr.LeftStickyRaycastLengthRef = sr.LeftStickyRaycastLength;
            sr.RightStickyRaycastLengthRef = sr.RightStickyRaycastLength;
            sr.CrossBelowSlopeAngleLeftRef = sr.CrossBelowSlopeAngleLeft;
            sr.CrossBelowSlopeAngleRightRef = sr.CrossBelowSlopeAngleRight;
            sr.BelowSlopeAngleLeftRef = sr.BelowSlopeAngleLeft;
            sr.BelowSlopeAngleRightRef = sr.BelowSlopeAngleRight;
            sr.CastFromLeftRef = sr.state.IsCastingToLeft;
        }

        private void SetStickyRaycastLength()
        {
            sr.StickyRaycastLength =
                SetStickyRaycastLength(sr.BoundsWidth, sr.MaximumSlopeAngle, sr.BoundsHeight, sr.RayOffset);
        }

        private void SetStickyRaycastLengthToSelf()
        {
            sr.StickyRaycastLength = sr.StickyRaycastLength;
        }

        private void SetLeftStickyRaycastLength()
        {
            sr.LeftStickyRaycastLength =
                SetStickyRaycastLength(sr.BoundsWidth, sr.MaximumSlopeAngle, sr.BoundsHeight, sr.RayOffset);
        }

        private void SetLeftStickyRaycastLengthToStickyRaycastLength()
        {
            sr.LeftStickyRaycastLength = sr.StickyRaycastLength;
        }

        private void SetRightStickyRaycastLength()
        {
            sr.RightStickyRaycastLength =
                SetStickyRaycastLength(sr.BoundsWidth, sr.MaximumSlopeAngle, sr.BoundsHeight, sr.RayOffset);
        }

        private void SetRightStickyRaycastLengthToStickyRaycastLength()
        {
            sr.RightStickyRaycastLength = sr.StickyRaycastLength;
        }

        private static float SetStickyRaycastLength(float boundsWidth, float slopeAngle, float boundsHeight,
            float offset)
        {
            return boundsWidth * Abs(Tan(slopeAngle)) * 2 + boundsHeight / 2 * offset;
        }

        private void SetLeftStickyRaycastOriginY()
        {
            sr.LeftStickyRaycastOriginY = sr.BoundsCenter.y;
        }

        private void SetLeftStickyRaycastOriginX()
        {
            sr.LeftStickyRaycastOriginX = sr.BoundsBottomLeftCorner.x * 2 + sr.NewPosition.x;
        }

        private void SetRightStickyRaycastOriginY()
        {
            sr.RightStickyRaycastOriginY = sr.BoundsCenter.y;
        }

        private void SetRightStickyRaycastOriginX()
        {
            sr.RightStickyRaycastOriginX = sr.BoundsBottomRightCorner.x * 2 + sr.NewPosition.x;
        }

        private void SetLeftStickyRaycast()
        {
            sr.LeftStickyRaycast = Raycast(sr.LeftStickyRaycastOrigin, -sr.Transform.up, sr.LeftStickyRaycastLength,
                sr.RaysBelowLayerMaskPlatforms, cyan, sr.DrawRaycastGizmosControl);
        }

        private void SetRightStickyRaycast()
        {
            sr.RightStickyRaycast = Raycast(sr.RightStickyRaycastOrigin, -sr.Transform.up, sr.RightStickyRaycastLength,
                sr.RaysBelowLayerMaskPlatforms, cyan, sr.DrawRaycastGizmosControl);
        }

        private void SetDoNotCastFromLeft()
        {
            sr.state.SetCastToLeft(false);
        }

        private void InitializeBelowSlopeAngle()
        {
            sr.BelowSlopeAngle = 0f;
        }

        private void SetBelowSlopeAngleLeft()
        {
            sr.BelowSlopeAngleLeft = Vector2.Angle(sr.LeftStickyRaycast.normal, sr.Transform.up);
        }

        private void SetBelowSlopeAngleRight()
        {
            sr.BelowSlopeAngleRight = Vector2.Angle(sr.RightStickyRaycast.normal, sr.Transform.up);
        }

        private void SetCrossBelowSlopeAngleLeft()
        {
            sr.CrossBelowSlopeAngleLeft = Cross(sr.Transform.up, sr.LeftStickyRaycast.normal);
        }

        private void SetCrossBelowSlopeAngleRight()
        {
            sr.CrossBelowSlopeAngleRight = Cross(sr.Transform.up, sr.RightStickyRaycast.normal);
        }

        private void SetBelowSlopeAngleLeftToNegative()
        {
            sr.BelowSlopeAngleLeft = -sr.BelowSlopeAngleLeft;
        }

        private void SetBelowSlopeAngleRightToNegative()
        {
            sr.BelowSlopeAngleRight = -sr.BelowSlopeAngleRight;
        }

        private void SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            sr.state.SetCastToLeft(Abs(sr.BelowSlopeAngleLeft) > Abs(sr.BelowSlopeAngleRight));
        }

        private void SetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            sr.BelowSlopeAngle = sr.BelowSlopeAngleLeft;
        }

        private void SetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            sr.state.SetCastToLeft(sr.BelowSlopeAngle < 0f);
        }

        private void SetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            sr.state.SetCastToLeft(sr.BelowSlopeAngleRight < 0f);
        }

        private void SetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            sr.BelowSlopeAngle = sr.BelowSlopeAngleRight;
        }

        private void SetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            sr.state.SetCastToLeft(sr.BelowSlopeAngleLeft < 0f);
        }

        private void SetCastFromLeftWithLeftDistanceLtRightDistance()
        {
            sr.state.SetCastToLeft(sr.LeftStickyRaycast.distance < sr.RightStickyRaycast.distance);
        }

        private void ResetState()
        {
            sr.state.Reset();
        }

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

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

        public void OnSetLeftStickyRaycastLength()
        {
            SetLeftStickyRaycastLength();
        }

        public void OnSetRightStickyRaycastLength()
        {
            SetRightStickyRaycastLength();
        }

        public void OnSetLeftStickyRaycastLengthToStickyRaycastLength()
        {
            SetLeftStickyRaycastLengthToStickyRaycastLength();
        }

        public void OnSetRightStickyRaycastLengthToStickyRaycastLength()
        {
            SetRightStickyRaycastLengthToStickyRaycastLength();
        }

        public void OnSetLeftStickyRaycastOriginY()
        {
            SetLeftStickyRaycastOriginY();
        }

        public void OnSetLeftStickyRaycastOriginX()
        {
            SetLeftStickyRaycastOriginX();
        }

        public void OnSetRightStickyRaycastOriginY()
        {
            SetRightStickyRaycastOriginY();
        }

        public void OnSetRightStickyRaycastOriginX()
        {
            SetRightStickyRaycastOriginX();
        }

        public void OnSetLeftStickyRaycast()
        {
            SetLeftStickyRaycast();
        }

        public void OnSetRightStickyRaycast()
        {
            SetRightStickyRaycast();
        }

        public void OnSetDoNotCastFromLeft()
        {
            SetDoNotCastFromLeft();
        }

        public void OnInitializeBelowSlopeAngle()
        {
            InitializeBelowSlopeAngle();
        }

        public void OnSetBelowSlopeAngleLeft()
        {
            SetBelowSlopeAngleLeft();
        }

        public void OnSetBelowSlopeAngleRight()
        {
            SetBelowSlopeAngleRight();
        }

        public void OnSetCrossBelowSlopeAngleLeft()
        {
            SetCrossBelowSlopeAngleLeft();
        }

        public void OnSetCrossBelowSlopeAngleRight()
        {
            SetCrossBelowSlopeAngleRight();
        }

        public void OnSetBelowSlopeAngleLeftToNegative()
        {
            SetBelowSlopeAngleLeftToNegative();
        }

        public void OnSetBelowSlopeAngleRightToNegative()
        {
            SetBelowSlopeAngleRightToNegative();
        }

        public void OnSetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
        }

        public void OnSetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            SetBelowSlopeAngleToBelowSlopeAngleLeft();
        }

        public void OnSetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            SetCastFromLeftWithBelowSlopeAngleLtZero();
        }

        public void OnSetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            SetCastFromLeftWithBelowSlopeAngleRightLtZero();
        }

        public void OnSetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            SetBelowSlopeAngleToBelowSlopeAngleRight();
        }

        public void OnSetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            SetCastFromLeftWithBelowSlopeAngleLeftLtZero();
        }

        public void OnSetCastFromLeftWithLeftDistanceLtRightDistance()
        {
            SetCastFromLeftWithLeftDistanceLtRightDistance();
        }

        public void OnResetState()
        {
            ResetState();
        }
    }
}
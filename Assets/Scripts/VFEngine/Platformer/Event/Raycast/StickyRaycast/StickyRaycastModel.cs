using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private StickyRaycastData s;

        private void Initialize()
        {
            s.StickToSlopesOffsetYRef = s.StickToSlopesOffsetY;
            
            /*
            void GetWarningMessage()
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
            }
            */
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

        private void InitializeBelowSlopeAngle()
        {
            s.BelowSlopeAngle = 0f;
        }

        private void SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            s.IsCastingLeft = Abs(s.BelowSlopeAngleLeft) > Abs(s.BelowSlopeAngleRight);
        }

        private void SetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            s.BelowSlopeAngle = s.BelowSlopeAngleLeft;
        }

        private void SetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            s.IsCastingLeft = s.BelowSlopeAngle < 0f;
        }

        private void SetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            s.IsCastingLeft = s.BelowSlopeAngleRight < 0f;
        }

        private void SetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            s.BelowSlopeAngle = s.BelowSlopeAngleRight;
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

        public void OnInitialize()
        {
            Initialize();
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

        public void OnInitializeBelowSlopeAngle()
        {
            InitializeBelowSlopeAngle();
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

/*private void SetLeftStickyRaycastLength()
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
}*/

/*
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
}*/
/*
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
}*/

/*public void OnSetLeftStickyRaycastLength()
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
}*/
/*public void OnSetBelowSlopeAngleLeft()
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
}*/
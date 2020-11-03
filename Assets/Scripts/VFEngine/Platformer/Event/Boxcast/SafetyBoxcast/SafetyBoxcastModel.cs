using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static DebugExtensions;
    using static Vector2;
    using static Color;

    [CreateAssetMenu(fileName = "SafetyBoxcastModel",
        menuName = "VFEngine/Platformer/Event/Boxcast/Safety Boxcast Model", order = 0)]
    public class SafetyBoxcastModel : ScriptableObject, IModel
    {
        [LabelText("Safety Boxcast Data")] [SerializeField]
        private SafetyBoxcastData s;

        private void Initialize()
        {
            if (s.DisplayWarningsControl) GetWarningMessages();
        }

        private void GetWarningMessages()
        {
            /* fields: methods */
            /*
            private void GetWarningMessage()
            {
            if (!DisplayWarnings) return;
            var warningMessage = "";
            var warningMessageCount = 0;
                if (!settings) warningMessage += FieldMessage("Settings", "Layer Mask Settings");
                if (!safetyBoxcastControl) warningMessage += FieldMessage("Safety Boxcast Control", "Bool Reference");
            DebugLogWarning(warningMessageCount, warningMessage);

            string FieldMessage(string field, string scriptableObject)
            {
                warningMessageCount++;
                return $"{field} field not set to {scriptableObject} ScriptableObject.";
            }
            }
            */    
        }

        private void SetSafetyBoxcastForImpassableAngle()
        {
            var transformUp = s.Transform.up;
            s.SafetyBoxcast = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), -transformUp,
                s.StickyRaycastLength, s.RaysBelowLayerMaskPlatforms, red, s.DrawBoxcastGizmosControl);
        }

        private void SetHasSafetyBoxcast()
        {
            s.HasSafetyBoxcast = true;
        }

        private void SetSafetyBoxcast()
        {
            var transformUp = s.Transform.up;
            s.SafetyBoxcast = Boxcast(s.BoundsCenter, s.Bounds, Angle(transformUp, up), s.NewPosition.normalized,
                s.NewPosition.magnitude, s.PlatformMask, red, s.DrawBoxcastGizmosControl);
        }

        private void ResetState()
        {
            s.HasSafetyBoxcast = false;
        }

        public void OnInitialize()
        {
            Initialize();
        }

        public void OnSetSafetyBoxcastForImpassableAngle()
        {
            SetSafetyBoxcastForImpassableAngle();
        }

        public void OnSetHasSafetyBoxcast()
        {
            SetHasSafetyBoxcast();
        }

        public void OnSetSafetyBoxcast()
        {
            SetSafetyBoxcast();
        }

        public void OnResetState()
        {
            ResetState();
        }
    }
}
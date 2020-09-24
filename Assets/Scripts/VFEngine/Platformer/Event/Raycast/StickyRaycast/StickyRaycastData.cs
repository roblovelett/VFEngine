using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static DebugExtensions;

    public class StickyRaycastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private StickyRaycastSettings settings;
        [SerializeField] private BoolReference stickyRaycastControl;

        /* fields */
        private bool DisplayWarnings => settings.displayWarningsControl;

        /* fields: methods */
        private void GetWarningMessage()
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

        /* properties: dependencies */
        public bool StickyRaycastControl => stickyRaycastControl.Value;

        /* properties */
        public float StickyRaycastLength => settings.stickyRaycastLength;
        public bool DrawRaycastGizmos => settings.drawRaycastGizmosControl;
        public RaycastHit2D StickyRaycastLeft { get; set; }
        public RaycastHit2D StickyRaycastRight { get; set; }
        public RaycastHit2D StickyRaycast { get; set; }

        /* properties: methods */
        public void Initialize()
        {
            GetWarningMessage();
        }
    }
}
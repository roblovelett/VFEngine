using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static DebugExtensions;
    using static ScriptableObjectExtensions;

    public class StickyRaycastData : MonoBehaviour
    {
        [SerializeField] private StickyRaycastSettings settings;
        [SerializeField] private FloatReference stickToSlopesOffsetY;
        [SerializeField] private FloatReference stickyRaycastLength;
        private const string SrPath = "Event/Raycast/StickyRaycast/";
        private static readonly string ModelAssetPath = $"{SrPath}DefaultStickyRaycastModel.asset";
        
        
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public float StickToSlopesOffsetY => settings.stickToSlopesOffsetY;
        public float StickToSlopesOffsetYRef
        {
            set => value = stickToSlopesOffsetY;
        }

        public float StickyRaycastLength { get; set; } = 0f;

        public float StickyRaycastLengthRef
        {
            set => value = stickyRaycastLength.Value;
        }
    }
}

/* fields: dependencies */
/*
[SerializeField] private StickyRaycastSettings settings;
[SerializeField] private BoolReference stickyRaycastControl;

/* fields */
/*
private bool DisplayWarnings => settings.displayWarningsControl;

/* fields: methods */
/*
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
/*
public bool StickyRaycastControl => stickyRaycastControl.Value;

/* properties */
/*
public float StickyRaycastLength => settings.stickyRaycastLength;
public bool DrawRaycastGizmos => settings.drawRaycastGizmosControl;
public RaycastHit2D StickyRaycastLeft { get; set; }
public RaycastHit2D StickyRaycastRight { get; set; }
public RaycastHit2D StickyRaycast { get; set; }

/* properties: methods */
/*
public void Initialize()
{
GetWarningMessage();
}
*/
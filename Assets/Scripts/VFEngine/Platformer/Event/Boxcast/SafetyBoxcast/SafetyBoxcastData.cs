using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static DebugExtensions;

    public class SafetyBoxcastData : MonoBehaviour
    {
        
    }
}

/* fields: dependencies */
/*
[SerializeField] private SafetyBoxcastSettings settings;
[SerializeField] private BoolReference safetyBoxcastControl;

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
    if (!settings) warningMessage += FieldMessage("Settings", "Layer Mask Settings");
    if (!safetyBoxcastControl) warningMessage += FieldMessage("Safety Boxcast Control", "Bool Reference");
DebugLogWarning(warningMessageCount, warningMessage);

string FieldMessage(string field, string scriptableObject)
{
    warningMessageCount++;
    return $"{field} field not set to {scriptableObject} ScriptableObject.";
}
}

/* properties: dependencies */
/*
public bool SafetyBoxcastControl => safetyBoxcastControl.Value;

/* properties */
/*
public bool DrawBoxcastGizmos => settings.drawBoxcastGizmosControl;

/* properties: methods */
/*
public void Initialize()
{
GetWarningMessage();
}
*/
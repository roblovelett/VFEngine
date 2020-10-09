using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Layer.Mask
{
    using static DebugExtensions;

    public class LayerMaskData : MonoBehaviour
    {
        
    }
}

/* fields: dependencies */
/*
[SerializeField] private LayerMaskSettings settings;

/* fields */
/*
private bool DisplayWarnings => settings.displayWarningsControl;

/* fields: methods */
/*
private void GetWarningMessage()
{
    if (!DisplayWarnings) return;
    const string player = "Player";
    const string platform = "Platform";
    const string movingPlatform = "MovingPlatform";
    const string oneWayPlatform = "OneWayPlatform";
    const string movingOneWayPlatform = "MovingOneWayPlatform";
    const string midHeightOneWayPlatform = "MidHeightOneWayPlatform";
    const string stairs = "Stairs";
    var platformLayers = LayerMask.GetMask($"{player}", $"{platform}");
    var movingPlatformLayer = LayerMask.GetMask($"{movingPlatform}");
    var oneWayPlatformLayer = LayerMask.GetMask($"{oneWayPlatform}");
    var movingOneWayPlatformLayer = LayerMask.GetMask($"{movingOneWayPlatform}");
    var midHeightOneWayPlatformLayer = LayerMask.GetMask($"{midHeightOneWayPlatform}");
    var stairsLayer = LayerMask.GetMask($"{stairs}");
    LayerMask[] layers =
    {
        platformLayers, movingPlatformLayer, oneWayPlatformLayer, movingOneWayPlatformLayer,
        midHeightOneWayPlatformLayer, stairsLayer
    };
    LayerMask[] layerMasks =
    {
        PlatformMask, MovingPlatformMask, OneWayPlatformMask, MovingOneWayPlatformMask,
        MidHeightOneWayPlatformMask, StairsMask
    };
    var warningMessage = "";
    var warningMessageCount = 0;
    if (!settings)
    {
        warningMessage += "Settings field not set to Layer Mask Settings ScriptableObject.@";
        warningMessageCount++;
    }

    for (var i = 0; i < layers.Length; i++)
    {
        if (layers[i].value == layerMasks[i].value) continue;
        var mask = LayerMask.LayerToName(layerMasks[i]);
        var layer = LayerMask.LayerToName(layers[i]);
        warningMessage += $"{mask} field not set to ${layer} in Layer Mask Settings ScriptableObject.@";
        warningMessageCount++;
    }

    DebugLogWarning(warningMessageCount, warningMessage);
}

/* properties */
/*
public LayerMask PlatformMask
{
    get => settings.platformMask;
    set => value = PlatformMask;
}

public LayerMask MovingPlatformMask => settings.movingPlatformMask;
public LayerMask OneWayPlatformMask => settings.oneWayPlatformMask;
public LayerMask MovingOneWayPlatformMask => settings.movingOneWayPlatformMask;
public LayerMask MidHeightOneWayPlatformMask => settings.midHeightOneWayPlatformMask;
public LayerMask StairsMask => settings.stairsMask;
public LayerMask SavedPlatformMask { get; set; }

/* properties: methods */
/*
public void Initialize()
{
    GetWarningMessage();
}
*/
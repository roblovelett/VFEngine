using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycasts
{
    using static RaycastsData;
    using static DebugExtensions;
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;
    using static MathsExtensions;
    using static Debug;

    [CreateAssetMenu(fileName = "RaycastsModel", menuName = "VFEngine/Platformer/Event/Raycasts/Raycasts Model",
        order = 0)]
    public class RaycastsModel : ScriptableObject, IModel
    {
        
    }
}

/* fields */
/*
[SerializeField] private RaycastsData rs;
*/
/* fields: methods */
/*
private async UniTaskVoid InitializeAsyncInternal(RaycastsModel model)
{
    var rsTask1 = rs.InitializeAsync();
    var rsTask2 = InitializeModelAsync(model);
    var rsTask3 = GetWarningMessagesAsync();
    var rsTask = await (rsTask1, rsTask2, rsTask3);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private UniTask<UniTaskVoid> InitializeModelAsync(RaycastsModel model)
{
    try
    {
        return new UniTask<UniTaskVoid>(InitializeModelAsyncInternal(model));
    }
    finally
    {
        InitializeModelAsyncInternal(model).Forget();
    }
}

private async UniTaskVoid InitializeModelAsyncInternal(RaycastsModel model)
{
    if (!model) model = LoadData(ModelPath) as RaycastsModel;
    Assert(model != null, nameof(model) + " != null");
    var rsTask1 = rs.UpRaycast.SetDirectionUpAsync();
    var rsTask2 = rs.RightRaycast.SetDirectionRightAsync();
    var rsTask3 = rs.DownRaycast.SetDirectionDownAsync();
    var rsTask4 = rs.LeftRaycast.SetDirectionLeftAsync();
    var rsTask = await (rsTask1, rsTask2, rsTask3, rsTask4);
    await SetYieldOrSwitchToThreadPoolAsync();
}

private UniTask<UniTaskVoid> GetWarningMessagesAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(GetWarningMessagesAsyncInternal());
    }
    finally
    {
        GetWarningMessagesAsyncInternal().Forget();
    }
}

private async UniTaskVoid GetWarningMessagesAsyncInternal()
{
    if (rs.displayWarnings)
    {
        const string ra = "Rays";
        const string rc = "Raycasts";
        const string nuOf = "Number of";
        const string diGrRa = "Distance To Ground Ray Maximum Length";
        var settings = $"{rc} Settings";
        var rcOf = $"{rc} Offset";
        var nuOfHoRa = $"{nuOf} Horizontal {ra}";
        var nuOfVeRa = $"{nuOf} Vertical {ra}";
        var warningMessage = "";
        var warningMessageCount = 0;
        if (!rs.hasSettings) warningMessage += FieldString($"{settings}", $"{settings}");
        if (rs.NumberOfHorizontalRays < 0) warningMessage += LtZeroString($"{nuOfHoRa}", $"{settings}");
        if (rs.NumberOfVerticalRays < 0) warningMessage += LtZeroString($"{nuOfVeRa}", $"{settings}");
        if (rs.CastRaysOnBothSides)
        {
            if (!IsEven(rs.NumberOfHorizontalRays)) warningMessage += IsOddString($"{nuOfHoRa}", $"{settings}");
            else if (!IsEven(rs.NumberOfVerticalRays))
                warningMessage += IsOddString($"{nuOfVeRa}", $"{settings}");
        }

        if (rs.DistanceToGroundRayMaximumLength <= 0)
            warningMessage += LtEqZeroString($"{diGrRa}", $"{settings}");
        if (rs.RayOffset <= 0) warningMessage += LtEqZeroString($"{rcOf}", $"{settings}");
        DebugLogWarning(warningMessageCount, warningMessage);

        string FieldString(string field, string scriptableObject)
        {
            WarningMessageCountAdd();
            return FieldMessage(field, scriptableObject);
        }

        string LtZeroString(string field, string scriptableObject)
        {
            WarningMessageCountAdd();
            return LtZeroMessage(field, scriptableObject);
        }

        string LtEqZeroString(string field, string scriptableObject)
        {
            WarningMessageCountAdd();
            return LtEqZeroMessage(field, scriptableObject);
        }

        string IsOddString(string field, string scriptableObject)
        {
            WarningMessageCountAdd();
            return IsOddMessage(field, scriptableObject);
        }

        void WarningMessageCountAdd()
        {
            warningMessageCount++;
        }
    }

    await SetYieldOrSwitchToThreadPoolAsync();
}
*/
/* properties: methods */
/*
public UniTask<UniTaskVoid> InitializeAsync(RaycastsModel model)
{
    try
    {
        return new UniTask<UniTaskVoid>(InitializeAsyncInternal(model));
    }
    finally
    {
        InitializeAsyncInternal(model).Forget();
    }
}
*/
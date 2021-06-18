namespace VFEngine.Tools.GameObject.Editor.ReplaceTool.Data
{
    internal enum ReplaceToolState
    {
        None,
        SettingHeader,
        SavingObjectsToReplace,
        PrintingInformationOnSelection,
        BeginningScrollView,
        ViewingSelectedObjects,
        ReplacingSelectedObjects,
        ApplyingModifiedProperties
    }
}
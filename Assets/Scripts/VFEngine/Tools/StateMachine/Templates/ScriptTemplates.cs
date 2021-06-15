using System.IO;
using System.Text;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.Templates
{using static ScriptableObject;
    using static File;
    using static Path;
    using static AssetDatabase;
    using static EditorGUIUtility;
    using static ProjectWindowUtil;
    using static ScriptTemplates.Text;

    [UsedImplicitly]
    internal class ScriptTemplates
    {
        //[MenuItem(ActionPath, false, 0)]
        public static void CreateActionScript()
        {
            //CreateScript(ActionFile, ActionResource);
        }

        [MenuItem(ConditionPath, false, 0)]
        public static void CreateConditionScript()
        {
            CreateScript(ConditionFile, ConditionResource);
        }

        private static void CreateScript(string pathName, string resource)
        {
            var endAction = CreateInstance<CreateStateMachineScript>();
            var icon = IconContent(ScriptIconContent).image as Texture2D;
            StartNameEditingIfProjectWindowExists(0, endAction, pathName, icon, resource);
        }

        private class CreateStateMachineScript : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var text = SetText();
                WriteText(text);
                CreateStateMachineScript();

                string SetText()
                {
                    var t = ReadAllText(resourceFile);
                    var fileName = GetFileName(pathName);
                    {
                        var newName = fileName.Replace(OldName, NewName);
                        if (!newName.Contains(ScriptableObject))
                            newName = newName.Insert(fileName.Length - 3, ScriptableObject);
                        pathName = pathName.Replace(fileName, newName);
                        fileName = newName;
                    }
                    var fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 3);
                    t = t.Replace(OldScriptName, fileNameWithoutExtension);
                    var runtimeName = fileNameWithoutExtension.Replace(ScriptableObject, NewName);
                    t = t.Replace(OldRuntimeName, runtimeName);
                    for (var i = runtimeName.Length - 1; i > 0; i--)
                        if (char.IsUpper(runtimeName[i]) && char.IsLower(runtimeName[i - 1]))
                            runtimeName = runtimeName.Insert(i, " ");
                    t = t.Replace(OldRuntimeNameWithSpaces, runtimeName);
                    return t;
                }

                void WriteText(string textInternal)
                {
                    var fullPath = GetFullPath(pathName);
                    var encoding = new UTF8Encoding(true);
                    WriteAllText(fullPath, textInternal, encoding);
                }

                void CreateStateMachineScript()
                {
                    ImportAsset(pathName);
                    var stateMachineScript = LoadAssetAtPath(pathName, typeof(Object));
                    ShowCreatedAsset(stateMachineScript);
                }
            }
        }

        internal static class Text
        {
            private const string TemplatesPath = "Assets/Scripts/VFEngine/Tools/StateMachine/Editor/Templates";
            public const string ConditionPath = "Assets/Create/State Machines/Condition Script";
            public const string ConditionFile = "NewConditionSO.cs";
            public const string ScriptIconContent = "cs Script Icon";
            public static readonly string ConditionResource = $"{TemplatesPath}/StateCondition.txt";
            public const string OldName = " ";
            public const string NewName = "";
            public const string ScriptableObject = "SO";
            public const string OldScriptName = "#SCRIPTNAME#";
            public const string OldRuntimeName = "#RUNTIMENAME#";

            public const string OldRuntimeNameWithSpaces = "#RUNTIMENAME_WITH_SPACES#";
            //public const string ActionPath = "Assets/Create/State Machines/Action Script";
            //public const string ActionFile = "NewActionSO.cs";
            //public static readonly string ActionResource = $"{TemplatesPath}/StateAction.txt";
        }
    }
}
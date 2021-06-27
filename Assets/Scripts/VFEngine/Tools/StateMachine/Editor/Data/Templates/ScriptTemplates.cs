using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VFEngine.Tools.StateMachine.Editor.Data.Templates
{
    using static String;
    using static EditorText;
    using static Char;
    using static File;
    using static Path;
    using static AssetDatabase;
    using static EditorGUIUtility;
    using static ProjectWindowUtil;
    using static ScriptableObject;

    internal class ScriptTemplates
    {
        [MenuItem("Tools/State Machine/Action Script", false, 0)]
        private static void ActionScript()
        {
            StartNameEditingIfProjectWindowExists(0, CreateInstance<StateMachineScript>(), ActionPath,
                IconContent(ScriptIconContent).image as Texture2D, ActionTemplatePath);
        }

        [MenuItem("Tools/State Machine/Condition Script", false, 0)]
        private static void ConditionScript()
        {
            StartNameEditingIfProjectWindowExists(0, CreateInstance<StateMachineScript>(), ConditionPath,
                IconContent(ScriptIconContent).image as Texture2D, ConditionTemplatePath);
        }

        private class StateMachineScript : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var text = ReadAllText(resourceFile);
                var fileName = GetFileName(pathName);
                var newName = fileName.Replace(Nbsp, Empty);
                if (!newName.Contains(SOProperty)) newName = newName.Insert(fileName.Length - 3, SOProperty);
                pathName = pathName.Replace(fileName, newName);
                fileName = newName;
                var fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 3);
                text = text.Replace(ScriptName, fileNameWithoutExtension);
                var runtimeName = fileNameWithoutExtension.Replace(SOProperty, Empty);
                text = text.Replace(RuntimeName, runtimeName);
                for (var i = runtimeName.Length - 1; i > 0; i--)
                    if (IsUpper(runtimeName[i]) && IsLower(runtimeName[i - 1]))
                        runtimeName = runtimeName.Insert(i, Nbsp);
                text = text.Replace(RuntimeNameWithSpaces, runtimeName);
                var fullPath = GetFullPath(pathName);
                var encoding = new UTF8Encoding(true);
                WriteAllText(fullPath, text, encoding);
                ImportAsset(pathName);
                ShowCreatedAsset(LoadAssetAtPath(pathName, typeof(Object)));
            }
        }
    }
}
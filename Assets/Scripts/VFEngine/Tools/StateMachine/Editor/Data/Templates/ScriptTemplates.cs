using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityObject = UnityEngine.Object;

// ReSharper disable ClassNeverInstantiated.Global
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
        private static void Action()
        {
            StartNameEditingIfProjectWindowExists(0, CreateInstance<StateMachineScript>(), ActionPath,
                IconContent(ScriptIconContent).image as Texture2D, ActionTemplatePath);
        }

        [MenuItem("Tools/State Machine/Condition Script", false, 0)]
        private static void Condition()
        {
            StartNameEditingIfProjectWindowExists(0, CreateInstance<StateMachineScript>(), ConditionPath,
                IconContent(ScriptIconContent).image as Texture2D, ConditionTemplatePath);
        }

        private class StateMachineScript : EndNameEditAction
        {
            private string text;
            private string fileName;
            private string newName;
            private string fileNameWithoutExtension;
            private string runtimeName;
            private int index;
            private string fullPath;
            private string namespacePath;
            private string @namespace;
            private UTF8Encoding encoding;

            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                text = ReadAllText(resourceFile);
                fileName = GetFileName(pathName);
                newName = fileName.Replace(Nbsp, Empty);
                if (!newName.Contains(SOProperty)) newName = newName.Insert(fileName.Length - 3, SOProperty);
                pathName = pathName.Replace(fileName, newName);
                fileName = newName;
                fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 3);
                text = text.Replace(ScriptName, fileNameWithoutExtension);
                runtimeName = fileNameWithoutExtension.Replace(SOProperty, Empty);
                text = text.Replace(RuntimeName, runtimeName);
                for (index = runtimeName.Length - 1; index > 0; index--)
                    if (IsUpper(runtimeName[index]) && IsLower(runtimeName[index - 1]))
                        runtimeName = runtimeName.Insert(index, Nbsp);
                text = text.Replace(RuntimeNameWithSpaces, runtimeName);
                fullPath = GetFullPath(pathName);
                namespacePath = pathName.Replace(InitialPath, Empty);
                @namespace = Regex.Replace(namespacePath.Replace(EditorText.PathSeparator, NamespaceSeparator),
                    NamespacePattern, NamespaceReplacement);
                text = text.Replace(Namespace, @namespace);
                encoding = new UTF8Encoding(true);
                WriteAllText(fullPath, text, encoding);
                ImportAsset(pathName);
                ShowCreatedAsset(LoadAssetAtPath(pathName, typeof(UnityObject)));
            }
        }
    }
}
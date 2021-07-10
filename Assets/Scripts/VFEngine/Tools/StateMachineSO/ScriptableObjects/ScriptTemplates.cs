using System.IO;
using System.Text;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects
{
    using static EditorGUIUtility;
    using static ProjectWindowUtil;
    using static ScriptableObject;
    using static File;
    using static Path;
    using static AssetDatabase;

    [UsedImplicitly]
    internal class ScriptTemplates
    {
        [MenuItem("Assets/Create/State Machine SO/State Action", false, 1)]
        private static void Action()
        {
            Script("Action");
        }

        [MenuItem("Assets/Create/State Machine SO/State Condition", false, 2)]
        private static void Condition()
        {
            Script("Condition");
        }

        private static void Script(string asset)
        {
            StartNameEditingIfProjectWindowExists(0, CreateInstance<ScriptAsset>(), $"New{asset}SO.cs",
                IconContent("cs Script Icon").image as Texture2D,
                $"Assets/Scripts/VFEngine/Tools/StateMachineSO/ScriptableObjects/Templates/State{asset}.txt");
        }

        private class ScriptAsset : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var text = ReadAllText(resourceFile);
                var fileName = GetFileName(pathName);
                {
                    var newName = fileName.Replace(" ", "");
                    if (!newName.Contains("SO")) newName = newName.Insert(fileName.Length - 3, "SO");
                    pathName = pathName.Replace(fileName, newName);
                    fileName = newName;
                }
                var fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 3);
                text = text.Replace("#SCRIPTNAME#", fileNameWithoutExtension);
                var runtimeName = fileNameWithoutExtension.Replace("SO", "");
                text = text.Replace("#RUNTIMENAME#", runtimeName);
                for (var i = runtimeName.Length - 1; i > 0; i--)
                    if (char.IsUpper(runtimeName[i]) && char.IsLower(runtimeName[i - 1]))
                        runtimeName = runtimeName.Insert(i, " ");
                text = text.Replace("#RUNTIMENAME_WITH_SPACES#", runtimeName);
                var fullPath = GetFullPath(pathName);
                var encoding = new UTF8Encoding(true);
                WriteAllText(fullPath, text, encoding);
                ImportAsset(pathName);
                ShowCreatedAsset(LoadAssetAtPath(pathName, typeof(Object)));
            }
        }
    }
}
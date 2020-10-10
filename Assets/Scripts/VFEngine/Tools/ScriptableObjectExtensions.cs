using UnityEditor;
using UnityEngine;

namespace VFEngine.Tools
{
    public static class ScriptableObjectExtensions
    {
        public static ScriptableObject LoadData(string path)
        {
            return AssetDatabase.LoadAssetAtPath(path, typeof(ScriptableObject)) as ScriptableObject;
        }

        private static string ScriptableObjectsPath { get; } = "Assets/ScriptableObjects/";
        private static string ScriptsPath { get; } = "Assets/Scripts/";
        private static string PlatformerPath { get; } = "VFEngine/Platformer/";
        public static string PlatformerScriptsPath { get; } = $"{ScriptsPath}{PlatformerPath}";
        public static string PlatformerScriptableObjectsPath { get; } = $"{ScriptableObjectsPath}{PlatformerPath}";
    }
}
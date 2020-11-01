using UnityEditor;
using UnityEngine;
namespace VFEngine.Tools
{
    using static Debug;
    using static AssetDatabase;
    public static class ScriptableObjectExtensions
    {
        public static ScriptableObject LoadData(string path)
        {
            return LoadAssetAtPath(path, typeof(ScriptableObject)) as ScriptableObject;
        }

        private static string ScriptableObjectsPath { get; } = "Assets/ScriptableObjects/";
        private static string ScriptsPath { get; } = "Assets/Scripts/";
        private static string PlatformerPath { get; } = "VFEngine/Platformer/";
        public static string PlatformerScriptsPath { get; } = $"{ScriptsPath}{PlatformerPath}";
        public static string PlatformerScriptableObjectsPath { get; } = $"{ScriptableObjectsPath}{PlatformerPath}";
        
        public static T LoadModel<T>(string path) where T : class
        {
            Assert(LoadData(path) is T model, nameof(model) + " != null");
            return LoadAssetAtPath(path, typeof(T)) as T;
        }
    }
}
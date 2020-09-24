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

        public static string DefaultPath { get; } = "Assets/ScriptableObjects/";
        public static string PlatformerPath { get; } = $"{DefaultPath}VFEngine/Platformer/";
    }
}
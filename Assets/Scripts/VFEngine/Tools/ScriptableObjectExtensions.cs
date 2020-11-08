using UnityEditor;
using UnityEngine;
// ReSharper disable RedundantAssignment
// ReSharper disable EntityNameCapturedOnly.Local
namespace VFEngine.Tools
{
    using static Debug;
    using static AssetDatabase;
    public static class ScriptableObjectExtensions
    {
        #region fields

        private static string As { get; } = "Assets/";
        private static string PlatformerPath { get; } = "VFEngine/Platformer/";
        private static string ScriptableObjectsPath { get; } = $"{As}ScriptableObjects/";
        private static string ScriptsPath { get; } = $"{As}Scripts/";

        #endregion
        
        #region properties

        public const string PlatformerRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Raycast Model";
        public const string PlatformerRaycastSettingsPath = "VFEngine/Platformer/Event/Raycast/Raycast Settings";
        public const string PlatformerSafetyBoxcastModelPath = "VFEngine/Platformer/Event/Boxcast/Safety Boxcast/Safety Boxcast Model";
        public const string PlatformerDistanceToGroundRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Distance To Ground Raycast/Distance To Ground Raycast Model";
        public const string PlatformerDownRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Down Raycast/Down Raycast Model";
        public const string PlatformerLeftRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Left Raycast/Left Raycast Model";
        public const string PlatformerRightRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Right Raycast/Right Raycast Model";
        public const string PlatformerUpRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Up Raycast/Up Raycast Model";
        public const string PlatformerStickyRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Sticky Raycast Model";
        public const string PlatformerStickyRaycastSettingsPath = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Sticky Raycast Settings";
        public const string PlatformerLeftStickyRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Left Sticky Raycast/Left Sticky Raycast Model";
        public const string PlatformerRightStickyRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Right Sticky Raycast/Right Sticky Raycast Model";
        public const string PlatformerLayerMaskModelPath = "VFEngine/Platformer/Layer/Mask/Layer Mask Model";
        public const string PlatformerPhysicsModelPath = "VFEngine/Platformer/Physics/Physics Model";
        public const string PlatformerPhysicsSettingsPath = "VFEngine/Platformer/Physics/Physics Settings";
        public static string PlatformerScriptableObjectsPath { get; } = $"{ScriptableObjectsPath}{PlatformerPath}";
        
        #region public methods
        public static ScriptableObject LoadData(string path)
        {
            return LoadAssetAtPath(path, typeof(ScriptableObject)) as ScriptableObject;
        }
        public static T LoadModel<T>(string path) where T : class
        {
            Assert(LoadData(path) is T model, nameof(model) + " != null");
            return LoadAssetAtPath(path, typeof(T)) as T;
        }
        #endregion
        #endregion
        

        
        
        
        
    }
}
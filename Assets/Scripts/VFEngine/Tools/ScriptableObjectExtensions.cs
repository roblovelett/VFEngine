using UnityEditor;
using UnityEngine;

// ReSharper disable RedundantAssignment
// ReSharper disable EntityNameCapturedOnly.Local
// ReSharper disable MemberCanBePrivate.Global
namespace VFEngine.Tools
{
    using static AssetDatabase;

    public static class ScriptableObjectExtensions
    {
        #region fields

        private static string As { get; } = "Assets/";
        private static string PlatformerPath { get; } = "VFEngine/Platformer/";
        private static string ScriptableObjectsPath { get; } = $"{As}ScriptableObjects/";

        #endregion

        #region properties

        public const string PlatformerSettingsPath = "VFEngine/Platformer/Platformer Settings";

        public const string PlatformerSafetyBoxcastSettingsPath =
            "VFEngine/Platformer/Event/Boxcast/Safety Boxcast/Safety Boxcast Settings";
        public const string PlatformerRaycastSettingsPath = "VFEngine/Platformer/Event/Raycast/Raycast Settings";

        public const string PlatformerStickyRaycastSettingsPath =
            "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Sticky Raycast Settings";

        public const string PlatformerPhysicsSettingsPath = "VFEngine/Platformer/Physics/Physics Settings";

        public const string PlatformerRaycastHitColliderContactListPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Raycast Hit Collider Contact List";

        public const string PlatformerPathMovementDataPath =
            "VFEngine/Platformer/Physics/Path Movement/Path Movement Data";

        public const string PlatformerPhysicsMaterialSettingsPath =
            "VFEngine/Platformer/Physics/Physics Material/Physics Material Settings";

        public static string PlatformerScriptableObjectsPath { get; } = $"{ScriptableObjectsPath}{PlatformerPath}";

        #region public methods

        public static ScriptableObject LoadData(string path)
        {
            return LoadAssetAtPath(path, typeof(ScriptableObject)) as ScriptableObject;
        }

        #endregion

        #endregion
    }
}
﻿using UnityEditor;
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

        #endregion

        #region properties

        public const string PlatformerModelPath = "VFEngine/Platformer/Platformer Model";

        public const string PlatformerSettingsPath = "VFEngine/Platformer/Platformer Settings";

        public const string PlatformerRaycastModelPath = "VFEngine/Platformer/Event/Raycast/Raycast Model";
        public const string PlatformerRaycastSettingsPath = "VFEngine/Platformer/Event/Raycast/Raycast Settings";

        public const string PlatformerSafetyBoxcastModelPath =
            "VFEngine/Platformer/Event/Boxcast/Safety Boxcast/Safety Boxcast Model";

        public const string PlatformerSafetyBoxcastDataPath =
            "VFEngine/Platformer/Event/Boxcast/Safety Boxcast/Safety Boxcast Data";

        public const string PlatformerDistanceToGroundRaycastModelPath =
            "VFEngine/Platformer/Event/Raycast/Distance To Ground Raycast/Distance To Ground Raycast Model";

        public const string PlatformerDistanceToGroundRaycastDataPath =
            "VFEngine/Platformer/Event/Raycast/Distance To Ground Raycast/Distance To Ground Raycast Data";

        public const string PlatformerDownRaycastModelPath =
            "VFEngine/Platformer/Event/Raycast/Down Raycast/Down Raycast Model";

        public const string PlatformerLeftRaycastModelPath =
            "VFEngine/Platformer/Event/Raycast/Left Raycast/Left Raycast Model";

        public const string PlatformerRightRaycastModelPath =
            "VFEngine/Platformer/Event/Raycast/Right Raycast/Right Raycast Model";

        public const string PlatformerUpRaycastModelPath =
            "VFEngine/Platformer/Event/Raycast/Up Raycast/Up Raycast Model";

        public const string PlatformerStickyRaycastModelPath =
            "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Sticky Raycast Model";

        public const string PlatformerStickyRaycastSettingsPath =
            "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Sticky Raycast Settings";

        public const string PlatformerLeftStickyRaycastModelPath =
            "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Left Sticky Raycast/Left Sticky Raycast Model";

        public const string PlatformerRightStickyRaycastModelPath =
            "VFEngine/Platformer/Event/Raycast/Sticky Raycast/Right Sticky Raycast/Right Sticky Raycast Model";

        public const string PlatformerLayerMaskModelPath = "VFEngine/Platformer/Layer/Mask/Layer Mask Model";
        public const string PlatformerPhysicsModelPath = "VFEngine/Platformer/Physics/Physics Model";
        public const string PlatformerPhysicsSettingsPath = "VFEngine/Platformer/Physics/Physics Settings";

        public const string PlatformerRaycastHitColliderModelPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Raycast Hit Collider Model";

        public const string PlatformerRaycastHitColliderSettingsPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Raycast Hit Collider Settings";

        public const string PlatformerRaycastHitColliderContactListPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Raycast Hit Collider Contact List";

        public const string PlatformerDistanceToGroundRaycastHitColliderModelPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Distance To Ground Raycast Hit Collider/Distance To Ground Raycast Hit Collider Model";

        public const string PlatformerDownRaycastHitColliderModelPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Down Raycast Hit Collider/Down Raycast Hit Collider Model";

        public const string PlatformerLeftRaycastHitColliderModelPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Left Raycast Hit Collider/Left Raycast Hit Collider Model";

        public const string PlatformerRightRaycastHitColliderModelPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Right Raycast Hit Collider/Right Raycast Hit Collider Model";

        public const string PlatformerUpRaycastHitColliderModelPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Up Raycast Hit Collider/Up Raycast Hit Collider Model";

        public const string PlatformerStickyRaycastHitColliderModelPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Sticky Raycast Hit Collider/Sticky Raycast Hit Collider Model";

        public const string PlatformerLeftStickyRaycastHitColliderModelPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Sticky Raycast Hit Collider/Left Sticky Raycast Hit Collider/Left Sticky Raycast Hit Collider Model";

        public const string PlatformerRightStickyRaycastHitColliderModelPath =
            "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Sticky Raycast Hit Collider/Right Sticky Raycast Hit Collider/Right Sticky Raycast Hit Collider Model";

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
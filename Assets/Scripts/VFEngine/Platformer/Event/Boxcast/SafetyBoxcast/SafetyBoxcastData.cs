using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static ScriptableObjectExtensions;

    public class SafetyBoxcastData
    {
        #region fields

        #region dependencies

        #endregion

        private const string SbPath = "Event/Boxcast/SafetyBoxcast/";
        private static readonly string ModelAssetPath = $"{SbPath}SafetyBoxcastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public LayerMaskRuntimeData LayerMaskRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public StickyRaycastRuntimeData StickyRaycastRuntimeData { get; set; }
        public bool DrawBoxcastGizmosControl { get; set; }
        public float StickyRaycastLength { get; set; }
        public Vector2 NewPosition { get; set; }
        public Vector2 Bounds { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public LayerMask PlatformMask { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }

        #endregion

        public SafetyBoxcastRuntimeData RuntimeData { get; set; }
        public RaycastHit2D SafetyBoxcastHit { get; set; }
        public static readonly string SafetyBoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using Sirenix.OdinInspector;
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

    [CreateAssetMenu(fileName = "SafetyBoxcastData", menuName = PlatformerSafetyBoxcastDataPath, order = 0)]
    [InlineEditor]
    public class SafetyBoxcastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;

        #endregion

        private const string SbPath = "Event/Boxcast/SafetyBoxcast/";
        private static readonly string ModelAssetPath = $"{SbPath}SafetyBoxcastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public LayerMaskRuntimeData LayerMaskRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public StickyRaycastRuntimeData StickyRaycastRuntimeData { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public bool DrawBoxcastGizmosControl { get; set; }
        public float StickyRaycastLength { get; set; }
        public Vector2 NewPosition { get; set; }
        public Vector2 Bounds { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public LayerMask PlatformMask { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }

        #endregion

        public SafetyBoxcastRuntimeData RuntimeData { get; set; }
        public RaycastHit2D SafetyBoxcast { get; set; }
        public static readonly string SafetyBoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
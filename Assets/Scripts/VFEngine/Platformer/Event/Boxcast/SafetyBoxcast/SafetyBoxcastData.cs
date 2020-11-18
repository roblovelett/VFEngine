using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

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

        [SerializeField] private RaycastReference safetyBoxcast = new RaycastReference();
        private const string SbPath = "Event/Boxcast/SafetyBoxcast/";
        private static readonly string ModelAssetPath = $"{SbPath}SafetyBoxcastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public bool DrawBoxcastGizmosControl { get; set; }
        public float StickyRaycastLength { get; set; }
        public Vector2 NewPosition { get; set; }
        public Vector2 Bounds { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public LayerMask PlatformMask { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }

        #endregion

        public static readonly string SafetyBoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        public RaycastHit2D SafetyBoxcast
        {
            get => safetyBoxcast.Value.hit2D;
            set => safetyBoxcast.Value = new ScriptableObjects.Variables.Raycast(value);
        }

        #endregion
    }
}
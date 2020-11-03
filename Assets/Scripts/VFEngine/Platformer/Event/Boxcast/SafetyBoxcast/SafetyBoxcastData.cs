using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static ScriptableObjectExtensions;

    public class SafetyBoxcastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private BoolReference displayWarningsControl;
        [SerializeField] private BoolReference drawRaycastGizmosControl;
        [SerializeField] private Vector2Reference bounds;
        [SerializeField] private Vector2Reference boundsCenter;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private FloatReference stickyRaycastLength;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private LayerMaskReference platformMask;

        /* fields */
        [SerializeField] private BoolReference hasSafetyBoxcast;
        [SerializeField] private RaycastHit2DReference safetyBoxcast;
        private const string SbPath = "Event/Boxcast/SafetyBoxcast/";
        private static readonly string ModelAssetPath = $"{SbPath}DefaultSafetyBoxcastModel.asset";

        /* properties: dependencies */
        public LayerMask PlatformMask => platformMask.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 Bounds => bounds.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public Transform Transform => transform.Value;
        public float StickyRaycastLength => stickyRaycastLength.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;
        public bool DrawBoxcastGizmosControl => drawRaycastGizmosControl.Value;
        public bool DisplayWarningsControl => displayWarningsControl.Value;

        /* properties */
        public static readonly string SafetyBoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public float SafetyBoxcastDistance => SafetyBoxcast.distance;

        public RaycastHit2D SafetyBoxcast
        {
            get => safetyBoxcast.Value;
            set => value = safetyBoxcast.Value;
        }

        public bool HasSafetyBoxcast
        {
            set => value = hasSafetyBoxcast.Value;
        }

        public Collider2D SafetyBoxcastCollider => SafetyBoxcast.collider;
    }
}
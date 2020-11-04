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
        #region fields

        #region dependencies

        [SerializeField] private BoolReference drawRaycastGizmosControl;
        [SerializeField] private BoolReference performSafetyBoxcast;
        [SerializeField] private Vector2Reference bounds;
        [SerializeField] private Vector2Reference boundsCenter;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private FloatReference stickyRaycastLength;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private LayerMaskReference platformMask;

        #endregion

        [SerializeField] private BoolReference hasSafetyBoxcast;
        [SerializeField] private RaycastHit2DReference safetyBoxcast;
        [SerializeField] private FloatReference safetyBoxcastDistance;
        private const string SbPath = "Event/Boxcast/SafetyBoxcast/";
        private static readonly string ModelAssetPath = $"{SbPath}DefaultSafetyBoxcastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool PerformSafetyBoxcast => performSafetyBoxcast.Value;
        public LayerMask PlatformMask => platformMask.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 Bounds => bounds.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public Transform Transform => transform.Value;
        public float StickyRaycastLength => stickyRaycastLength.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;
        public bool DrawBoxcastGizmosControl => drawRaycastGizmosControl.Value;

        #endregion

        public static readonly string SafetyBoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        public float SafetyBoxcastDistance
        {
            set => value = safetyBoxcastDistance.Value;
        }

        public RaycastHit2D SafetyBoxcast
        {
            get => safetyBoxcast.Value;
            set => value = safetyBoxcast.Value;
        }

        public bool HasSafetyBoxcast
        {
            set => value = hasSafetyBoxcast.Value;
        }

        #endregion
    }
}
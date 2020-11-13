using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.Raycast.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static ScriptableObjectExtensions;

    public class SafetyBoxcastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference drawRaycastGizmosControl = new BoolReference();
        [SerializeField] private Vector2Reference bounds = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsCenter = new Vector2Reference();
        [SerializeField] private new TransformReference transform = new TransformReference();
        [SerializeField] private FloatReference stickyRaycastLength = new FloatReference();
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms = new LayerMaskReference();
        [SerializeField] private Vector2Reference newPosition = new Vector2Reference();
        [SerializeField] private LayerMaskReference platformMask = new LayerMaskReference();

        #endregion

        [SerializeField] private RaycastReference safetyBoxcast = new RaycastReference();
        private const string SbPath = "Event/Boxcast/SafetyBoxcast/";
        private static readonly string ModelAssetPath = $"{SbPath}DefaultSafetyBoxcastModel.asset";

        #endregion

        #region properties

        #region dependencies

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

        public ScriptableObjects.Atoms.Raycast.Raycast SafetyBoxcast
        {
            set => value = safetyBoxcast.Value;
        }

        #endregion
    }
}
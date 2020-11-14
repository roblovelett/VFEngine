using ScriptableObjects.Atoms.Raycast.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;
// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class DistanceToGroundRaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference drawRaycastGizmosControl = new BoolReference();
        [SerializeField] private FloatReference belowSlopeAngle = new FloatReference();
        [SerializeField] private Vector2Reference boundsBottomLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomRightCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsCenter = new Vector2Reference();
        [SerializeField] private LayerMask raysBelowLayerMaskPlatforms = new LayerMask();
        [SerializeField] private new Transform transform = null;
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength = new FloatReference();

        #endregion

        [SerializeField] private RaycastReference distanceToGroundRaycast = new RaycastReference();
        private static readonly string DistanceToGroundRaycastPath = $"{RaycastPath}DistanceToGroundRaycast/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastPath}DistanceToGroundRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public float BelowSlopeAngle => belowSlopeAngle.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms;
        public Transform Transform => transform;
        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
        public float DistanceToGroundRayMaximumLength => distanceToGroundRayMaximumLength.Value;

        #endregion

        public ScriptableObjects.Atoms.Raycast.Raycast DistanceToGroundRaycast
        {
            set => value = distanceToGroundRaycast.Value;
        }

        public Vector2 DistanceToGroundRaycastOrigin { get; set; } = Vector2.zero;

        public static readonly string DistanceToGroundRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
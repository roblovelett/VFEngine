using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class DistanceToGroundRaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference drawRaycastGizmosControl;
        [SerializeField] private FloatReference belowSlopeAngle;
        [SerializeField] private Vector2Reference boundsBottomLeftCorner;
        [SerializeField] private Vector2Reference boundsBottomRightCorner;
        [SerializeField] private Vector2Reference boundsCenter;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength;

        #endregion

        [SerializeField] private RaycastHit2DReference distanceToGroundRaycast;
        private static readonly string DistanceToGroundRaycastPath = $"{RaycastPath}DistanceToGroundRaycast/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastPath}DefaultDistanceToGroundRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public float BelowSlopeAngle => belowSlopeAngle.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;
        public Transform Transform => transform.Value;
        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
        public float DistanceToGroundRayMaximumLength => distanceToGroundRayMaximumLength.Value;

        #endregion

        public RaycastHit2D DistanceToGroundRaycast
        {
            set => value = distanceToGroundRaycast.Value;
        }

        public Vector2 DistanceToGroundRaycastOrigin { get; set; } = Vector2.zero;

        public static readonly string DistanceToGroundRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
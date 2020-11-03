using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    using static ScriptableObjectExtensions;
    using static StickyRaycastData;

    public class RightStickyRaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference drawRaycastGizmosControl;
        [SerializeField] private FloatReference stickyRaycastLength;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference maximumSlopeAngle;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private Vector2Reference boundsBottomRightCorner;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private Vector2Reference boundsCenter;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
        [SerializeField] private new TransformReference transform;

        #endregion

        [SerializeField] private RaycastHit2DReference rightStickyRaycast;
        [SerializeField] private FloatReference belowSlopeAngleRight;
        [SerializeField] private Vector3Reference crossBelowSlopeAngleRight;
        private static readonly string RightStickyRaycastPath = $"{StickyRaycastPath}RightStickyRaycast/";
        private static readonly string ModelAssetPath = $"{RightStickyRaycastPath}DefaultRightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
        public float StickyRaycastLength => stickyRaycastLength.Value;
        public float BoundsWidth => boundsWidth.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;
        public float BoundsHeight => boundsHeight.Value;
        public float RayOffset => rayOffset.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;
        public Transform Transform => transform.Value;

        #endregion

        public float RightStickyRaycastLength { get; set; }

        public float RightStickyRaycastOriginX
        {
            set => value = rightStickyRaycastOrigin.x;
        }

        public float RightStickyRaycastOriginY
        {
            set => value = rightStickyRaycastOrigin.y;
        }

        public Vector2 rightStickyRaycastOrigin;

        public RaycastHit2D RightStickyRaycast
        {
            get => rightStickyRaycast.Value;
            set => value = rightStickyRaycast.Value;
        }

        public float BelowSlopeAngleRight
        {
            get => belowSlopeAngleRight.Value;
            set => value = belowSlopeAngleRight.Value;
        }

        public Vector3 CrossBelowSlopeAngleRight
        {
            set => value = crossBelowSlopeAngleRight.Value;
        }

        public static readonly string RightStickyRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
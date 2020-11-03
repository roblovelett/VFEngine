using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static ScriptableObjectExtensions;
    using static StickyRaycastData;

    public class LeftStickyRaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies
        
        [SerializeField] private BoolReference displayWarningsControl;
        [SerializeField] private BoolReference drawRaycastGizmosControl;
        [SerializeField] private FloatReference stickyRaycastLength;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference maximumSlopeAngle;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private Vector2Reference boundsBottomLeftCorner;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private Vector2Reference boundsCenter;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
        [SerializeField] private new TransformReference transform;
        
        
        #endregion
        [SerializeField] private RaycastHit2DReference leftStickyRaycast;
        [SerializeField] private FloatReference belowSlopeAngleLeft;
        [SerializeField] private Vector3Reference crossBelowSlopeAngleLeft;
        private static readonly string LeftStickyRaycastPath = $"{StickyRaycastPath}RightStickyRaycast/";
        private static readonly string ModelAssetPath = $"{LeftStickyRaycastPath}DefaultRightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies
        
        public bool DisplayWarningsControl => displayWarningsControl.Value;
        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
        public float StickyRaycastLength => stickyRaycastLength.Value;
        public float BoundsWidth => boundsWidth.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;
        public float BoundsHeight => boundsHeight.Value;
        public float RayOffset => rayOffset.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;
        public Transform Transform => transform.Value;
        
        #endregion
        
        public float LeftStickyRaycastLength { get; set; }

        public float LeftStickyRaycastOriginX
        {
            set => value = leftStickyRaycastOrigin.x;
        }

        public float LeftStickyRaycastOriginY
        {
            set => value = leftStickyRaycastOrigin.y;
        }

        public Vector2 leftStickyRaycastOrigin;

        public RaycastHit2D LeftStickyRaycast
        {
            get => leftStickyRaycast.Value;
            set => value = leftStickyRaycast.Value;
        }

        public float BelowSlopeAngleLeft
        {
            get => belowSlopeAngleLeft.Value;
            set => value = belowSlopeAngleLeft.Value;
        }

        public Vector3 CrossBelowSlopeAngleLeft
        {
            set => value = crossBelowSlopeAngleLeft.Value;
        }
        public static readonly string LeftStickyRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
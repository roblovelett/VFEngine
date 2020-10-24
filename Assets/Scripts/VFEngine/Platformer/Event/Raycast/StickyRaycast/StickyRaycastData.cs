using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;

    public class StickyRaycastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private StickyRaycastSettings settings;
        [SerializeField] private FloatReference stickToSlopesOffsetY;
        [SerializeField] private FloatReference stickyRaycastLength;
        [SerializeField] private FloatReference leftStickyRaycastLength;
        [SerializeField] private FloatReference rightStickyRaycastLength;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference maximumSlopeAngle;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private Vector2Reference boundsCenter;
        [SerializeField] private Vector2Reference boundsBottomLeftCorner;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private Vector2Reference boundsBottomRightCorner;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
        [SerializeField] private Vector3Reference crossBelowSlopeAngleLeft;
        [SerializeField] private Vector3Reference crossBelowSlopeAngleRight;
        [SerializeField] private FloatReference belowSlopeAngleLeft;
        [SerializeField] private FloatReference belowSlopeAngleRight;

        /* fields */
        [SerializeField] private RaycastHit2DReference leftStickyRaycast;
        [SerializeField] private RaycastHit2DReference rightStickyRaycast;
        private const string SrPath = "Event/Raycast/StickyRaycast/";
        private static readonly string ModelAssetPath = $"{SrPath}DefaultStickyRaycastModel.asset";

        /* properties: dependencies */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public float StickToSlopesOffsetY => settings.stickToSlopesOffsetY;

        public float StickToSlopesOffsetYRef
        {
            set => value = stickToSlopesOffsetY;
        }

        public float BoundsWidth => boundsWidth.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;
        public float BoundsHeight => boundsHeight.Value;
        public float RayOffset => rayOffset.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Transform Transform => transform.Value;
        public bool DrawRaycastGizmosControl => settings.drawRaycastGizmosControl;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;

        /* properties */
        public float StickyRaycastLength { get; set; }

        public float StickyRaycastLengthRef
        {
            set => value = stickyRaycastLength.Value;
        }
        
        public float LeftStickyRaycastLength { get; set; }

        public float LeftStickyRaycastLengthRef
        {
            set => value = leftStickyRaycastLength.Value;
        }

        public float RightStickyRaycastLength { get; set; }

        public float RightStickyRaycastLengthRef
        {
            set => value = rightStickyRaycastLength.Value;
        }

        public float LeftStickyRaycastOriginY
        {
            set => value = LeftStickyRaycastOrigin.y;
        }

        public float LeftStickyRaycastOriginX
        {
            set => value = LeftStickyRaycastOrigin.x;
        }

        public float RightStickyRaycastOriginY
        {
            set => value = RightStickyRaycastOrigin.y;
        }

        public float RightStickyRaycastOriginX
        {
            set => value = RightStickyRaycastOrigin.x;
        }

        public Vector2 LeftStickyRaycastOrigin { get; } = new Vector2(0, 0);
        public Vector2 RightStickyRaycastOrigin { get; } = new Vector2(0, 0);
        
        public RaycastHit2D LeftStickyRaycast { get; set; }

        public RaycastHit2D LeftStickyRaycastRef
        {
            set => value = leftStickyRaycast.Value;
        }
        public RaycastHit2D RightStickyRaycast { get; set; }

        public RaycastHit2D RightStickyRaycastRef
        {
            set => value = rightStickyRaycast.Value;
        }
        public bool CastFromLeft { get; set; }
        public float BelowSlopeAngle { get; set; }
        public float BelowSlopeAngleLeft { get; set; }

        public float BelowSlopeAngleLeftRef
        {
            set => value = belowSlopeAngleLeft.Value;
        }
        public float BelowSlopeAngleRight { get; set; }

        public float BelowSlopeAngleRightRef
        {
            set => value = belowSlopeAngleRight.Value;
        }
        public Vector3 CrossBelowSlopeAngleLeft { get; set; }

        public Vector3 CrossBelowSlopeAngleLeftRef
        {
            set => value = crossBelowSlopeAngleLeft.Value;
        }
        public Vector3 CrossBelowSlopeAngleRight { get; set; }

        public Vector3 CrossBelowSlopeAngleRightRef
        {
            set => value = crossBelowSlopeAngleRight.Value;
        }
    }
}
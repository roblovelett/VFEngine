using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;
    using static RaycastData;

    public class StickyRaycastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private StickyRaycastSettings settings;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference maximumSlopeAngle;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private RaycastHit2DReference leftStickyRaycast;
        [SerializeField] private RaycastHit2DReference rightStickyRaycast;
        [SerializeField] private FloatReference belowSlopeAngleLeft;
        [SerializeField] private FloatReference belowSlopeAngleRight;
        
        /* fields */
        [SerializeField] private FloatReference stickToSlopesOffsetY;
        [SerializeField] private BoolReference isCastingLeft;
        [SerializeField] private FloatReference stickyRaycastLength;
        private static readonly string StickyRaycastPath = $"{RaycastPath}StickyRaycast/";
        private static readonly string ModelAssetPath = $"{StickyRaycastPath}DefaultStickyRaycastModel.asset";

        /* properties: dependencies */
        public RaycastHit2D LeftStickyRaycast => leftStickyRaycast.Value;
        public RaycastHit2D RightStickyRaycast => rightStickyRaycast.Value;
        public float BoundsWidth => boundsWidth.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;
        public float BoundsHeight => boundsHeight.Value;
        public float RayOffset => rayOffset.Value;
        public float BelowSlopeAngleLeft => belowSlopeAngleLeft.Value;
        public float BelowSlopeAngleRight => belowSlopeAngleRight.Value;
        public float StickToSlopesOffsetY => settings.stickToSlopesOffsetY;
        
        /* properties */
        public float StickToSlopesOffsetYRef
        {
            get => stickToSlopesOffsetY.Value;
            set => value = stickToSlopesOffsetY.Value;
        }
        public bool IsCastingLeft
        {
            get => isCastingLeft.Value;
            set => value = isCastingLeft.Value;
        }

        public float StickyRaycastLength
        {
            get => stickyRaycastLength.Value;
            set => value = stickyRaycastLength.Value;
        }
        public float BelowSlopeAngle { get; set; }
        public static readonly string StickyRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
    }
}

// dependencies
//[SerializeField] private FloatReference stickToSlopesOffsetY;
//[SerializeField] private FloatReference stickyRaycastLength;
//[SerializeField] private FloatReference leftStickyRaycastLength;
//[SerializeField] private FloatReference rightStickyRaycastLength;
//[SerializeField] private Vector2Reference boundsCenter;
//[SerializeField] private Vector2Reference boundsBottomLeftCorner;
//[SerializeField] private Vector2Reference newPosition;
//[SerializeField] private Vector2Reference boundsBottomRightCorner;
//[SerializeField] private new TransformReference transform;
//[SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
//[SerializeField] private Vector3Reference crossBelowSlopeAngleLeft;
//[SerializeField] private Vector3Reference crossBelowSlopeAngleRight;
//[SerializeField] private FloatReference belowSlopeAngleLeft;
//[SerializeField] private FloatReference belowSlopeAngleRight;
// fields
//[SerializeField] private RaycastHit2DReference leftStickyRaycast;
//[SerializeField] private RaycastHit2DReference rightStickyRaycast;
//[SerializeField] private BoolReference castFromLeft;
// properties: dependencies
//public float StickToSlopesOffsetY => settings.stickToSlopesOffsetY;
//public Vector2 BoundsCenter => boundsCenter.Value;
//public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
//public Vector2 NewPosition => newPosition.Value;
//public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
//public Transform Transform => transform.Value;
//public bool DrawRaycastGizmosControl => settings.drawRaycastGizmosControl;
//public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;
// properties
//public float LeftStickyRaycastLength { get; set; }
//public float RightStickyRaycastLength { get; set; }
/*
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
}*/

//public Vector2 LeftStickyRaycastOrigin { get; } = new Vector2(0, 0);
//public Vector2 RightStickyRaycastOrigin { get; } = new Vector2(0, 0);
//public Vector3 CrossBelowSlopeAngleLeft { get; set; }
//public Vector3 CrossBelowSlopeAngleRight { get; set; }
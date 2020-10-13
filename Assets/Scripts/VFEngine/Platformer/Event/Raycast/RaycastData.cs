using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;
    public class RaycastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private RaycastSettings settings;
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference boxColliderBoundsCenter;
        
        /* fields */
        [SerializeField] private IntReference numberOfHorizontalRays;
        [SerializeField] private IntReference numberOfVerticalRays;
        [SerializeField] private BoolReference castRaysOnBothSides;
        private const string RPath = "Event/Raycast/";
        private static readonly string ModelAssetPath = $"{RPath}DefaultRaycastModel.asset";
        
        /* properties: dependencies */
        public bool HasSettings => settings;
        public int NumberOfHorizontalRays => settings.numberOfHorizontalRays;
        public int NumberOfVerticalRays => settings.numberOfVerticalRays;
        public bool CastRaysOnBothSides => settings.castRaysOnBothSides;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public Vector2 BoxColliderSize => boxColliderSize.Value;
        public Vector2 BoxColliderOffset => boxColliderOffset.Value;
        public Transform Transform => transform.Value;
        public Vector2 BoxColliderBoundsCenter => boxColliderBoundsCenter.Value;

        public float DistanceToGroundRayMaximumLength => settings.distanceToGroundRayMaximumLength;
        public float RayOffset => settings.rayOffset;
        
        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public int NumberOfHorizontalRaysRef
        {
            set => value = numberOfHorizontalRays.Value;
        }

        public int NumberOfVerticalRaysRef
        {
            set => value = numberOfVerticalRays.Value;
        }

        public bool CastRaysOnBothSidesRef
        {
            set => value = castRaysOnBothSides.Value;
        }
        
        public Vector2 BoundsTopLeftCorner { get; set; }
        public Vector2 BoundsTopRightCorner { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public float BoundsWidth { get; set; }
        public float BoundsHeight { get; set; }
    }
}

/* 
// fields: dependencies
[SerializeField] public Vector3Reference movingPlatformCurrentSpeed;
[SerializeField] public BoolReference isCollidingWithMovingPlatform;
[SerializeField] public BoolReference wasTouchingCeilingLastFrame;
[SerializeField] public BoolReference castRaysOnBothSides;

//properties: dependencies
public bool CastRaysOnBothSides => castRaysOnBothSides.Value;
public float DistanceToGroundRayMaximumLength => settings.distanceToGroundRayMaximumLength;
public float RayOffset => settings.rayOffset;
public bool DrawRaycastGizmos => settings.drawRaycastGizmosControl;
public Vector3 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
public bool IsCollidingWithMovingPlatform => isCollidingWithMovingPlatform.Value;
public bool WasTouchingCeilingLastFrame => wasTouchingCeilingLastFrame.Value;

// properties
public float HorizontalRayLength { get; set; }
public float HorizontalRaySpacing { get; set; }
public float HorizontalRayCount { get; set; }
public float VerticalRaySpacing { get; set; }
public float VerticalRayCount { get; set; }
public float DistanceToGround { get; set; }
public LayerMask PlatformsLayerMaskBelow { get; set; }
public LayerMask NonOneWayPlatformsLayerMaskBelow { get; set; }
public LayerMask NonMidHeightPlatformsLayerMaskBelow { get; set; }
public Vector2 OriginalSizeRaycastOrigin { get; set; }
public Vector2 HorizontalRayCastFromBottom { get; set; } = Vector2.zero;
public Vector2 HorizontalRayCastToTop { get; set; } = Vector2.zero;
public Vector2 VerticalRayCastFromLeft { get; set; } = Vector2.zero;
public Vector2 VerticalRayCastToRight { get; set; } = Vector2.zero;
public Vector2 AboveRayCastStart { get; set; } = Vector2.zero;
public Vector2 AboveRayCastEnd { get; set; } = Vector2.zero;
public Vector2 RayCastOrigin { get; set; } = Vector2.zero;
public RaycastHit2D DistanceToGroundRaycast { get; set; }
public Vector3 BoundsTop => SetBoundsSide(BoundsTopLeftCorner, BoundsTopRightCorner);
public Vector3 BoundsBottom => SetBoundsSide(BoundsBottomLeftCorner, BoundsBottomRightCorner);
public Vector3 BoundsRight => SetBoundsSide(BoundsTopRightCorner, BoundsBottomRightCorner);
public Vector3 BoundsLeft => SetBoundsSide(BoundsTopLeftCorner, BoundsBottomLeftCorner);
public Vector2 Bounds => new Vector2 {x = BoundsWidth, y = BoundsHeight};

private static Vector2 SetBoundsSide(Vector2 corner1, Vector2 corner2)
{
    return (corner1 + corner2) / 2;
}
*/
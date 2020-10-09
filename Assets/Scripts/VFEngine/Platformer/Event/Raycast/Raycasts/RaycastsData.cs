using Cysharp.Threading.Tasks;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycasts
{
    using static ScriptableObject;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    public class RaycastsData : MonoBehaviour
    {
        
    }
}

/* fields: dependencies */
/*
[SerializeField] private RaycastsSettings settings;
[SerializeField] private FloatReference movementDirection;

/* fields */
/*
[SerializeField] private IntReference numberOfHorizontalRays;
[SerializeField] private IntReference numberOfVerticalRays;
[SerializeField] private FloatReference distanceToGroundRayMaximumLength;
[SerializeField] private FloatReference rayOffset;
[SerializeField] private BoolReference castRaysOnBothSides;
[SerializeField] private BoolReference drawRaycastGizmos;
private const string AssetPath = "Event/Raycasts/DefaultRaycastsModel.asset";

/* fields: methods */
/*
private async UniTaskVoid InitializeAsyncInternal()
{
    if (settings) hasSettings = true;
    displayWarnings = settings.displayWarningsControl;
    numberOfHorizontalRays.Value = settings.numberOfHorizontalRays;
    numberOfVerticalRays.Value = settings.numberOfVerticalRays;
    distanceToGroundRayMaximumLength.Value = settings.distanceToGroundRayMaximumLength;
    rayOffset.Value = settings.rayOffset;
    castRaysOnBothSides.Value = settings.castRaysOnBothSides;
    drawRaycastGizmos.Value = settings.drawRaycastGizmosControl;
    await SetYieldOrSwitchToThreadPoolAsync();
}

/* properties: dependencies */
/*
public float MovementDirection => movementDirection.Value;

/* properties */
/*
public const float Tolerance = 0;
public static readonly string ModelPath = $"{DefaultPath}{PlatformerPath}{AssetPath}";
public bool hasSettings;
public bool displayWarnings;
public int NumberOfHorizontalRays => numberOfHorizontalRays.Value;
public int NumberOfVerticalRays => numberOfVerticalRays.Value;
public float DistanceToGroundRayMaximumLength => distanceToGroundRayMaximumLength.Value;
public float RayOffset => rayOffset.Value;
public bool CastRaysOnBothSides => castRaysOnBothSides.Value;
public RaycastModel UpRaycast { get; } = CreateInstance<RaycastModel>();
public RaycastModel RightRaycast { get; } = CreateInstance<RaycastModel>();
public RaycastModel DownRaycast { get; } = CreateInstance<RaycastModel>();
public RaycastModel LeftRaycast { get; } = CreateInstance<RaycastModel>();

/* properties: methods */
/*
public UniTask<UniTaskVoid> InitializeAsync()
{
    try
    {
        return new UniTask<UniTaskVoid>(InitializeAsyncInternal());
    }
    finally
    {
        InitializeAsyncInternal().Forget();
    }
}

/* 
=======================================================================================================================
 
// fields: dependencies

[SerializeField] public Vector3Reference movingPlatformCurrentSpeed;
[SerializeField] public new TransformReference transform;
[SerializeField] public BoolReference isCollidingWithMovingPlatform;
[SerializeField] public BoolReference wasTouchingCeilingLastFrame;
[SerializeField] public BoolReference castRaysOnBothSides;

// fields 
        
//properties: dependencies
public bool CastRaysOnBothSides => castRaysOnBothSides.Value;
public float DistanceToGroundRayMaximumLength => settings.distanceToGroundRayMaximumLength;
public float RayOffset => settings.rayOffset;
public bool DisplayWarnings => settings.displayWarningsControl;
public bool DrawRaycastGizmos => settings.drawRaycastGizmosControl;
public Vector2 BoxColliderOffset => boxColliderOffset.Value;
public Vector2 BoxColliderSize => boxColliderSize.Value;
public Vector3 BoxColliderBoundsCenter => boxColliderBoundsCenter.Value;
public Vector3 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
public Transform Transform => transform.Value;
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
public float BoundsWidth { get; set; }
public float BoundsHeight { get; set; }
public Vector3 BoundsTopLeftCorner { get; set; }
public Vector3 BoundsBottomLeftCorner { get; set; }
public Vector3 BoundsTopRightCorner { get; set; }
public Vector3 BoundsBottomRightCorner { get; set; }
public Vector3 BoundsCenter { get; set; }
public Vector3 BoundsTop => SetBoundsSide(BoundsTopLeftCorner, BoundsTopRightCorner);
public Vector3 BoundsBottom => SetBoundsSide(BoundsBottomLeftCorner, BoundsBottomRightCorner);
public Vector3 BoundsRight => SetBoundsSide(BoundsTopRightCorner, BoundsBottomRightCorner);
public Vector3 BoundsLeft => SetBoundsSide(BoundsTopLeftCorner, BoundsBottomLeftCorner);
public Vector2 Bounds => new Vector2 {x = BoundsWidth, y = BoundsHeight};

private static Vector3 SetBoundsSide(Vector3 corner1, Vector3 corner2)
{
    return (corner1 + corner2) / 2;
}
*/
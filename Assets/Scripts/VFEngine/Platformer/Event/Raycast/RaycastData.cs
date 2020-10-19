using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;
    using static Time;
    using static Mathf;
    using static MathsExtensions;
    using static Vector2;

    public class RaycastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private RaycastSettings settings;
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference boxColliderBoundsCenter;
        [SerializeField] private Vector2Reference speed;
        private Vector2 Speed => speed.Value;
        [SerializeField] private IntReference currentRightHitsStorageIndex;
        [SerializeField] private IntReference currentLeftHitsStorageIndex;
        [SerializeField] private IntReference horizontalHitsStorageLength;
        [SerializeField] private LayerMaskReference platformMask;
        [SerializeField] private LayerMaskReference oneWayPlatformMask;
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask;

        /* fields */
        [SerializeField] private IntReference numberOfHorizontalRays;
        [SerializeField] private IntReference numberOfVerticalRays;
        [SerializeField] private BoolReference castRaysOnBothSides;
        [SerializeField] private BoolReference drawRaycastGizmos;
        [SerializeField] private Vector2Reference horizontalRaycastFromBottom;
        [SerializeField] private Vector2Reference horizontalRaycastToTop;
        [SerializeField] private FloatReference horizontalRayLength;
        [SerializeField] private Vector2Reference currentRightRaycastOrigin;
        [SerializeField] private Vector2Reference currentLeftRaycastOrigin;
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        [SerializeField] private RaycastHit2DReference currentRightRaycast;
        [SerializeField] private RaycastHit2DReference currentLeftRaycast;
        private const float ObstacleHeightTolerance = 0.05f;
        private const string RPath = "Event/Raycast/";
        private static readonly string ModelAssetPath = $"{RPath}DefaultRaycastModel.asset";

        /* properties: dependencies */
        public bool HasSettings => settings;
        public bool DrawRaycastGizmos => settings.drawRaycastGizmosControl;
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
        public int CurrentRightHitsStorageIndex => currentRightHitsStorageIndex.Value;
        public int CurrentLeftHitsStorageIndex => currentLeftHitsStorageIndex.Value;
        public int NumberOfHorizontalRaysPerSide => horizontalHitsStorageLength.Value;
        public LayerMask PlatformMask => platformMask.Value;
        public LayerMask OneWayPlatformMask => oneWayPlatformMask.Value;
        public LayerMask MovingOneWayPlatformMask => movingOneWayPlatformMask.Value;
        public RaycastHit2D CurrentRightRaycast { get; set; }
        public RaycastHit2D CurrentLeftRaycast { get; set; }
        public RaycastHit2D CurrentRightRaycastRef
        {
            set => value = currentRightRaycast.Value;
        }

        public RaycastHit2D CurrentLeftRaycastRef
        {
            set => value = currentLeftRaycast.Value;
        }

        public int NumberOfHorizontalRaysPerSideRef
        {
            set => value = numberOfHorizontalRaysPerSide.Value;
        }

        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        public bool DrawRaycastGizmosRef
        {
            set => value = drawRaycastGizmos.Value;
        }

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
        public Vector2 HorizontalRaycastFromBottom => SetHorizontalRaycastFromBottom();
        public Vector2 HorizontalRaycastToTop => SetHorizontalRaycastToTop();

        public Vector2 HorizontalRaycastFromBottomRef
        {
            set => value = horizontalRaycastFromBottom.Value;
        }

        public Vector2 HorizontalRaycastToTopRef
        {
            set => value = horizontalRaycastToTop.Value;
        }

        public float HorizontalRayLength => SetHorizontalRayLength();

        public float HorizontalRayLengthRef
        {
            set => value = horizontalRayLength.Value;
        }

        public Vector2 CurrentRightRaycastOrigin => SetRightRaycastOrigin();
        public Vector2 CurrentLeftRaycastOrigin => SetLeftRaycastOrigin();

        public Vector2 CurrentRightRaycastOriginRef
        {
            set => value = currentRightRaycastOrigin.Value;
        }

        public Vector2 CurrentLeftRaycastOriginRef
        {
            set => value = currentLeftRaycastOrigin.Value;
        }

        /* fields: methods */
        private Vector2 SetHorizontalRaycastFromBottom()
        {
            return Distance(BoundsBottomLeftCorner, BoundsBottomRightCorner) + Tolerance(Transform);
        }

        private Vector2 SetHorizontalRaycastToTop()
        {
            return Distance(BoundsTopLeftCorner, BoundsTopRightCorner) - Tolerance(Transform);
        }

        private static Vector2 Distance(Vector2 b1, Vector2 b2)
        {
            return (b1 + b2) / 2;
        }

        private static Vector2 Tolerance(Transform t)
        {
            return (Vector2) t.up * ObstacleHeightTolerance;
        }

        private float SetHorizontalRayLength()
        {
            return Half(Abs(Speed.x * deltaTime) + BoundsWidth) + RayOffset * 2;
        }

        private Vector2 SetRightRaycastOrigin()
        {
            return Lerp(HorizontalRaycastFromBottom, HorizontalRaycastToTop,
                CurrentRightHitsStorageIndex / (float) NumberOfHorizontalRaysPerSide - 1);
        }

        private Vector2 SetLeftRaycastOrigin()
        {
            return Lerp(HorizontalRaycastFromBottom, HorizontalRaycastToTop,
                CurrentLeftHitsStorageIndex / (float) NumberOfHorizontalRaysPerSide - 1);
        }
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
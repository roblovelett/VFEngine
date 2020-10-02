using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static RaycastModel;
    using static ScriptableObjectExtensions;

    public class RaycastsData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] public RaycastsSettings settings;
        [SerializeField] public Vector2Reference boxColliderOffset;
        [SerializeField] public Vector2Reference boxColliderSize;
        [SerializeField] public Vector3Reference boxColliderBoundsCenter;
        [SerializeField] public Vector3Reference movingPlatformCurrentSpeed;
        [SerializeField] public new TransformReference transform;
        [SerializeField] public BoolReference isCollidingWithMovingPlatform;
        [SerializeField] public BoolReference wasTouchingCeilingLastFrame;

        /* fields */
        [SerializeField] public IntReference numberOfHorizontalRays;
        [SerializeField] public IntReference numberOfVerticalRays;
        [SerializeField] public BoolReference castRaysOnBothSides;
        private static string AssetPath { get; } = "Event/Raycast/DefaultRaycastsModel.asset";

        /* properties: dependencies */
        public int NumberOfHorizontalRays => numberOfHorizontalRays.Value;
        public int NumberOfVerticalRays => numberOfVerticalRays.Value;
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

        /* properties */
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
        public RaycastModel upRaycast = new RaycastModel(RaycastDirection.Up);
        public RaycastModel rightRaycast = new RaycastModel(RaycastDirection.Right);
        public RaycastModel downRaycast = new RaycastModel(RaycastDirection.Down);
        public RaycastModel leftRaycast = new RaycastModel(RaycastDirection.Left);
        public string modelPath = $"{DefaultPath}{PlatformerPath}{AssetPath}";

        private static Vector3 SetBoundsSide(Vector3 corner1, Vector3 corner2)
        {
            return (corner1 + corner2) / 2;
        }
    }
}
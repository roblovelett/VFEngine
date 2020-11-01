using System;
using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;
    using static Single;

    public class RaycastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private RaycastSettings settings;
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference boxColliderBoundsCenter;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private IntReference currentRightHitsStorageIndex;
        [SerializeField] private IntReference currentLeftHitsStorageIndex;
        [SerializeField] private IntReference currentDownHitsStorageIndex;
        //[SerializeField] private IntReference currentUpHitsStorageIndex;
        [SerializeField] private LayerMaskReference platformMask;
        [SerializeField] private LayerMaskReference oneWayPlatformMask;
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatformsWithoutOneWay;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private RaycastHit2DReference raycastDownHitAt;
        [SerializeField] private Vector2Reference standingOnWithSmallestDistancePoint;
        [SerializeField] private FloatReference rayOffset;
        //[SerializeField] private BoolReference isGrounded;
        [SerializeField] private FloatReference belowSlopeAngle;

        /* fields */
        [SerializeField] private IntReference numberOfHorizontalRays;
        [SerializeField] private IntReference numberOfVerticalRays;
        [SerializeField] private BoolReference castRaysOnBothSides;
        [SerializeField] private BoolReference drawRaycastGizmos;
        [SerializeField] private Vector2Reference verticalRaycastFromLeft;
        [SerializeField] private Vector2Reference verticalRaycastToRight;
        //[SerializeField] private Vector2Reference currentUpRaycastOrigin;
        [SerializeField] private Vector2Reference currentDownRaycastOrigin;
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        [SerializeField] private IntReference numberOfVerticalRaysPerSide;
        [SerializeField] private RaycastHit2DReference currentRightRaycast;
        [SerializeField] private RaycastHit2DReference currentLeftRaycast;
        //[SerializeField] private RaycastHit2DReference currentUpRaycast;
        [SerializeField] private RaycastHit2DReference currentDownRaycast;
        [SerializeField] private FloatReference downRayLength;
        [SerializeField] private FloatReference leftRayLength;
        [SerializeField] private FloatReference rightRayLength;
        [SerializeField] private FloatReference smallestDistance;
        [SerializeField] private FloatReference smallValue;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private Vector2Reference leftRaycastFromBottomOrigin;
        [SerializeField] private Vector2Reference rightRaycastFromBottomOrigin;
        [SerializeField] private Vector2Reference rightRaycastToTopOrigin;
        [SerializeField] private Vector2Reference leftRaycastToTopOrigin;
        [SerializeField] private Vector2Reference bounds;
        [SerializeField] private Vector2Reference boundsCenter;
        [SerializeField] private Vector2Reference boundsBottomLeftCorner;
        [SerializeField] private Vector2Reference boundsBottomRightCorner;
        [SerializeField] private FloatReference distanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint;
        //[SerializeField] private FloatReference upRaycastSmallestDistance;
        [SerializeField] private RaycastHit2DReference raycastUpHitAt;
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength;
        [SerializeField] private RaycastHit2DReference distanceToGroundRaycast;
        [SerializeField] private BoolReference hasDistanceToGroundRaycast;
        public const string RaycastPath = "Event/Raycast/";
        private static readonly string ModelAssetPath = $"{RaycastPath}DefaultRaycastModel.asset";

        /* properties: dependencies */
        public float BelowSlopeAngle => belowSlopeAngle.Value;
        public Vector2 Speed => speed.Value;
        //public RaycastHit2D RaycastUpHitAt => raycastUpHitAt.Value;
        //public bool IsGrounded => isGrounded.Value;
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

        public float DistanceToGroundRayMaximumLengthRef
        {
            set => value = distanceToGroundRayMaximumLength.Value;
        }

        public float RayOffset => settings.rayOffset;

        public float RayOffsetRef
        {
            set => value = rayOffset.Value;
        }

        public int CurrentRightHitsStorageIndex => currentRightHitsStorageIndex.Value;
        public int CurrentLeftHitsStorageIndex => currentLeftHitsStorageIndex.Value;
        public int CurrentDownHitsStorageIndex => currentDownHitsStorageIndex.Value;
        //public int CurrentUpHitsStorageIndex => currentUpHitsStorageIndex.Value;
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public int NumberOfVerticalRaysPerSide => settings.numberOfVerticalRays / 2;
        public LayerMask PlatformMask => platformMask.Value;
        public LayerMask OneWayPlatformMask => oneWayPlatformMask.Value;
        public LayerMask MovingOneWayPlatformMask => movingOneWayPlatformMask.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;
        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay => raysBelowLayerMaskPlatformsWithoutOneWay.Value;
        public RaycastHit2D CurrentRightRaycast { get; set; }
        public RaycastHit2D CurrentLeftRaycast { get; set; }
        //public RaycastHit2D CurrentUpRaycast { get; set; }
        public RaycastHit2D CurrentDownRaycast { get; set; }

        public RaycastHit2D CurrentRightRaycastRef
        {
            set => value = currentRightRaycast.Value;
        }

        public RaycastHit2D CurrentLeftRaycastRef
        {
            set => value = currentLeftRaycast.Value;
        }

        /*public RaycastHit2D CurrentUpRaycastRef
        {
            set => value = currentUpRaycast.Value;
        }*/

        public RaycastHit2D CurrentDownRaycastRef
        {
            set => value = currentDownRaycast.Value;
        }

        public int NumberOfHorizontalRaysPerSideRef
        {
            set => value = numberOfHorizontalRaysPerSide.Value;
        }

        public int NumberOfVerticalRaysPerSideRef
        {
            set => value = numberOfVerticalRaysPerSide.Value;
        }

        public Vector2 NewPosition => newPosition.Value;
        public RaycastHit2D RaycastDownHitAt => raycastDownHitAt.Value;
        public Vector2 StandingOnWithSmallestDistancePoint => standingOnWithSmallestDistancePoint.Value;

        /* properties */
        public readonly RaycastState state = new RaycastState();
        public Vector2 DistanceToGroundRaycastOrigin { get; set; }
        public RaycastHit2D DistanceToGroundRaycast { get; set; }

        public RaycastHit2D DistanceToGroundRaycastRef
        {
            set => value = distanceToGroundRaycast.Value;
        }

        public bool HasDistanceToGroundRaycastRef
        {
            set => value = hasDistanceToGroundRaycast.Value;
        }

        public Vector2 CurrentRightRaycastOrigin { get; set; } = new Vector2(0, 0);
        public Vector2 CurrentLeftRaycastOrigin { get; set; } = new Vector2(0, 0);
        public static float ObstacleHeightTolerance => 0.05f;
        public Vector2 RightRaycastFromBottomOrigin { get; set; } = new Vector2(0, 0);

        public Vector2 RightRaycastFromBottomOriginRef
        {
            set => value = rightRaycastFromBottomOrigin.Value;
        }

        public Vector2 LeftRaycastFromBottomOrigin { get; set; } = new Vector2(0, 0);

        public Vector2 LeftRaycastFromBottomOriginRef
        {
            set => value = leftRaycastFromBottomOrigin.Value;
        }

        public Vector2 RightRaycastToTopOrigin { get; set; } = new Vector2(0, 0);

        public Vector2 RightRaycastToTopOriginRef
        {
            set => value = rightRaycastToTopOrigin.Value;
        }

        public Vector2 LeftRaycastToTopOrigin { get; set; } = new Vector2(0, 0);

        public Vector2 LeftRaycastToTopOriginRef
        {
            set => value = leftRaycastToTopOrigin.Value;
        }

        public static readonly string RaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        //public float UpRayLength { get; set; }
        //public Vector2 UpRaycastStart { get; set; } = new Vector2(0, 0);
        //public Vector2 UpRaycastEnd { get; set; } = new Vector2(0, 0);
        //public float UpRaycastSmallestDistance { get; set; }

        /*public float UpRaycastSmallestDistanceRef
        {
            set => value = upRaycastSmallestDistance.Value;
        }*/

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

        public Vector2 Bounds => new Vector2 {x = BoundsWidth, y = BoundsHeight};

        public Vector2 BoundsRef
        {
            set => value = bounds.Value;
        }

        public Vector2 BoundsTopLeftCorner { get; set; }
        public Vector2 BoundsTopRightCorner { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }

        public Vector2 BoundsBottomLeftCornerRef
        {
            set => value = boundsBottomLeftCorner.Value;
        }

        public Vector2 BoundsBottomRightCorner { get; set; }

        public Vector2 BoundsBottomRightCornerRef
        {
            set => value = boundsBottomRightCorner.Value;
        }

        public Vector2 BoundsCenter { get; set; }

        public Vector2 BoundsCenterRef
        {
            set => value = boundsCenter.Value;
        }

        public float BoundsWidth { get; set; }

        public float BoundsWidthRef
        {
            set => value = boundsWidth.Value;
        }

        public float BoundsHeight { get; set; }

        public float BoundsHeightRef
        {
            set => value = boundsHeight.Value;
        }

        public Vector2 CurrentDownRaycastOrigin { get; set; }
        //public Vector2 CurrentUpRaycastOrigin { get; set; }

        public Vector2 CurrentDownRaycastOriginRef
        {
            set => value = currentDownRaycastOrigin.Value;
        }

        /*public Vector2 CurrentUpRaycastOriginRef
        {
            set => value = currentUpRaycastOrigin.Value;
        }*/

        public float DownRayLength { get; set; }

        public float DownRayLengthRef
        {
            set => value = downRayLength.Value;
        }

        public float LeftRayLength { get; set; }

        public float LeftRayLengthRef
        {
            set => value = leftRayLength.Value;
        }

        public float RightRayLength { get; set; }

        public float RightRayLengthRef
        {
            set => value = rightRayLength.Value;
        }

        public Vector2 VerticalRaycastFromLeft { get; set; }
        public Vector2 VerticalRaycastToRight { get; set; }

        public Vector2 VerticalRaycastFromLeftRef
        {
            set => value = verticalRaycastFromLeft.Value;
        }

        public Vector2 VerticalRaycastToRightRef
        {
            set => value = verticalRaycastToRight.Value;
        }

        public float SmallestDistance { get; set; } = MaxValue;

        public float SmallestDistanceRef
        {
            set => value = smallestDistance.Value;
        }

        public float SmallValue { get; } = 0.0001f;

        public float SmallValueRef
        {
            set => value = smallValue.Value;
        }

        public float DistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint { get; set; }

        public float DistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPointRef
        {
            set => value = distanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint.Value;
        }
    }
}
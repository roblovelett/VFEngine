using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Physics.Gravity;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics
{
    using static ScriptableObjectExtensions;
    using static MathsExtensions;

    public class PhysicsData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private PhysicsSettings settings;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private GravityController gravityController;
        [SerializeField] private FloatReference movingPlatformCurrentGravity;
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed;
        [SerializeField] private FloatReference maximumSlopeAngle;
        [SerializeField] private Vector2Reference horizontalRaycastFromBottom;
        [SerializeField] private Vector2Reference horizontalRaycastToTop;
        [SerializeField] private Vector2Reference currentRightHitPoint;
        [SerializeField] private Vector2Reference currentLeftHitPoint;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private FloatReference distanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private RaycastHit2DReference safetyBoxcast;
        [SerializeField] private FloatReference leftStickyRaycastOriginY;
        [SerializeField] private FloatReference rightStickyRaycastOriginY;
        [SerializeField] private RaycastHit2DReference leftStickyRaycast;
        [SerializeField] private RaycastHit2DReference rightStickyRaycast;
        private Vector2 HorizontalRaycastFromBottom => horizontalRaycastFromBottom.Value;
        private Vector2 HorizontalRaycastToTop => horizontalRaycastToTop.Value;
        private Vector2 CurrentRightHitPoint => currentRightHitPoint.Value;
        private Vector2 CurrentLeftHitPoint => currentLeftHitPoint.Value;

        /* fields */
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private BoolReference gravityActive;
        [SerializeField] private FloatReference fallSlowFactor;
        [SerializeField] private IntReference horizontalMovementDirection;
        [SerializeField] private FloatReference newRightPositionDistance;
        [SerializeField] private FloatReference newLeftPositionDistance;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private FloatReference smallValue;
        [SerializeField] private FloatReference gravity;
        [SerializeField] private BoolReference stickToSlopesControl;
        [SerializeField] private BoolReference isJumping;
        [SerializeField] private BoolReference safetyBoxcastControl;
        private const string PhPath = "Physics/";
        private static readonly string ModelAssetPath = $"{PhPath}DefaultPhysicsModel.asset";

        /* fields: methods */
        private float SetNewRightPositionDistance()
        {
            return DistanceBetweenPointAndLine(CurrentRightHitPoint, HorizontalRaycastFromBottom,
                HorizontalRaycastToTop);
        }

        private float SetNewLeftPositionDistance()
        {
            return DistanceBetweenPointAndLine(CurrentLeftHitPoint, HorizontalRaycastFromBottom,
                HorizontalRaycastToTop);
        }

        /* properties dependencies */
        public RaycastHit2D LeftStickyRaycast => leftStickyRaycast.Value;
        public RaycastHit2D RightStickyRaycast => rightStickyRaycast.Value;
        public float LeftStickyRaycastOriginY => leftStickyRaycastOriginY.Value;
        public float RightStickyRaycastOriginY => rightStickyRaycastOriginY.Value;
        public RaycastHit2D SafetyBoxcast => safetyBoxcast.Value;
        public bool SafetyBoxcastControl => settings.safetyBoxcastControl;

        public bool SafetyBoxcastControlRef
        {
            set => value = safetyBoxcastControl.Value;
        }
        public bool IsJumpingRef
        {
            set => value = isJumping.Value;
        }
        public bool StickToSlopesControl => settings.stickToSlopeControl;

        public bool StickToSlopesControlRef
        {
            set => value = stickToSlopesControl.Value;
        }
        public Transform Transform
        {
            get => characterTransform;
            set => value = characterTransform;
        }

        public Transform TransformRef
        {
            set => value = transform.Value;
        }

        public bool HasSettings => settings;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool AutomaticGravityControl => settings.automaticGravityControl;
        public bool HasGravityController => gravityController;
        public bool HasTransform => characterTransform;
        public float Gravity => settings.gravity;

        public float GravityRef
        {
            set => value = gravity.Value;
        }
        public float AscentMultiplier => settings.ascentMultiplier;
        public float FallMultiplier => settings.fallMultiplier;
        public float MovingPlatformCurrentGravity => movingPlatformCurrentGravity.Value;
        public Vector2 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public float MaximumSlopeAngle => settings.maximumSlopeAngle;

        public float MaximumSlopeAngleRef
        {
            set => value = maximumSlopeAngle;
        }

        public float BoundsWidth => boundsWidth.Value;
        public float RayOffset => rayOffset.Value;

        public float DistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint =>
            distanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint.Value;

        public float BoundsHeight => boundsHeight.Value;

        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public readonly PhysicsState state = new PhysicsState();
        public float CurrentGravity { get; set; }
        public Vector2 Speed { get; set; } = new Vector2(0, 0);
        public Vector2 ForcesApplied { get; set; }
        public int HorizontalMovementDirection { get; set; }

        public int HorizontalMovementDirectionRef
        {
            set => value = horizontalMovementDirection.Value;
        }

        public int StoredHorizontalMovementDirection { get; set; }

        public float SpeedX
        {
            get => Speed.x;
            set => value = Speed.x;
        }

        public float SpeedY
        {
            get => Speed.y;
            set => value = Speed.y;
        }

        public Vector2 SpeedRef
        {
            set => value = speed.Value;
        }

        public bool GravityActiveRef
        {
            set => value = gravityActive.Value;
        }

        public float FallSlowFactor { get; set; }

        public float FallSlowFactorRef
        {
            set => value = fallSlowFactor.Value;
        }

        public Vector2 NewPosition { get; set; }

        public Vector2 NewPositionRef
        {
            set => value = newPosition.Value;
        }

        public float NewPositionX
        {
            get => NewPosition.x;
            set => value = NewPosition.x;
        }

        public float NewPositionY
        {
            get => NewPosition.y;
            set => value = NewPosition.y;
        }

        public float NewRightPositionDistance => SetNewRightPositionDistance();
        public float NewLeftPositionDistance => SetNewLeftPositionDistance();

        public float NewRightPositionDistanceRef
        {
            set => value = newRightPositionDistance.Value;
        }

        public float NewLeftPositionDistanceRef
        {
            set => value = newLeftPositionDistance.Value;
        }

        public float SmallValue { get; set; } = 0.0001f;

        public float SmallValueRef
        {
            set => value = smallValue.Value;
        }
    }
}
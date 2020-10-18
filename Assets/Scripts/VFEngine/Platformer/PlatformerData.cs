using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;

    public class PlatformerData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private PlatformerSettings settings;
        [SerializeField] private PhysicsController physics;
        [SerializeField] private RaycastController raycast;
        [SerializeField] private RaycastHitColliderController raycastHitCollider;
        [SerializeField] private LayerMaskController layerMask;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private BoolReference gravityActive;
        [SerializeField] private FloatReference fallSlowFactor;
        [SerializeField] private BoolReference isCollidingWithMovingPlatform;
        [SerializeField] private Vector2Reference movingPlatformCurrentSpeed;
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame;
        [SerializeField] private FloatReference movementDirectionThreshold;
        [SerializeField] private Vector2Reference externalForce;
        [SerializeField] private BoolReference castRaysOnBothSides;
        [SerializeField] private IntReference horizontalMovementDirection;
        [SerializeField] private Vector2Reference horizontalRaycastFromBottom;
        [SerializeField] private Vector2Reference horizontalRaycastToTop;
        [SerializeField] private IntReference numberOfHorizontalRays;
        [SerializeField] private Vector2Reference currentRightRaycastOrigin;
        [SerializeField] private Vector2Reference currentLeftRaycastOrigin;
        [SerializeField] private BoolReference wasGroundedLastFrame;
        [SerializeField] private IntReference rightHitsStorageIndex;
        [SerializeField] private IntReference leftHitsStorageIndex;
        [SerializeField] private FloatReference horizontalRayLength;
        [SerializeField] private IntReference horizontalHitsStorageLength;
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        [SerializeField] private FloatReference currentRightHitDistance;
        [SerializeField] private FloatReference currentLeftHitDistance;
        [SerializeField] private Collider2DReference currentRightHitCollider;
        [SerializeField] private Collider2DReference currentLeftHitCollider;
        [SerializeField] private Collider2DReference ignoredCollider;
        [SerializeField] private FloatReference currentRightHitAngle;
        [SerializeField] private FloatReference currentLeftHitAngle;
        [SerializeField] private FloatReference maximumSlopeAngle;

        /* fields */
        private const string ModelAssetPath = "DefaultPlatformerModel.asset";

        /* properties: dependencies */
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool HasSettings => settings;
        public PhysicsController Physics => physics;
        public RaycastController Raycast => raycast;
        public RaycastHitColliderController RaycastHitCollider => raycastHitCollider;
        public LayerMaskController LayerMask => layerMask;
        public Vector2 Speed => speed.Value;
        public bool GravityActive => gravityActive.Value;
        public float FallSlowFactor => fallSlowFactor.Value;
        public bool IsCollidingWithMovingPlatform => isCollidingWithMovingPlatform.Value;
        public Vector2 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public bool WasTouchingCeilingLastFrame => wasTouchingCeilingLastFrame.Value;
        public float MovementDirectionThreshold => movementDirectionThreshold.Value;
        public bool CastRaysOnBothSides => castRaysOnBothSides.Value;
        public Vector2 ExternalForce => externalForce.Value;
        public int HorizontalMovementDirection => horizontalMovementDirection.Value;
        public Vector2 HorizontalRaycastFromBottom => horizontalRaycastFromBottom.Value;
        public Vector2 HorizontalRaycastToTop => horizontalRaycastToTop.Value;
        public int NumberOfHorizontalRays => numberOfHorizontalRays.Value;
        public int RightHitsStorageIndex => rightHitsStorageIndex.Value;
        public int LeftHitsStorageIndex => leftHitsStorageIndex;
        public float HorizontalRayLength => horizontalRayLength.Value;
        public int HorizontalHitsStorageLength => horizontalHitsStorageLength;
        public Vector2 CurrentRightRaycastOrigin => currentRightRaycastOrigin.Value;
        public Vector2 CurrentLeftRaycastOrigin => currentLeftRaycastOrigin.Value;
        public int NumberOfHorizontalRaysPerSide => numberOfHorizontalRaysPerSide.Value;
        public bool WasGroundedLastFrame => wasGroundedLastFrame.Value;
        public float CurrentRightHitDistance => currentRightHitDistance.Value;
        public float CurrentLeftHitDistance => currentLeftHitDistance.Value;
        public Collider2D CurrentRightHitCollider => currentRightHitCollider.Value;
        public Collider2D CurrentLeftHitCollider => currentLeftHitCollider.Value;
        public Collider2D IgnoredCollider => ignoredCollider.Value;
        public float CurrentRightHitAngle => currentRightHitAngle.Value;

        public float CurrentLeftHitAngle => currentLeftHitAngle.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;

        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
    }
}
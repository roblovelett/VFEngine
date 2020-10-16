using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private Vector2Reference raycastFromBottom;
        [SerializeField] private Vector2Reference raycastToTop;
        [SerializeField] private IntReference numberOfHorizontalRays;
        [SerializeField] private IntReference horizontalHitsStorageIndexesAmount;
        [SerializeField] private BoolReference wasGroundedLastFrame;

        /* fields */
        [SerializeField] private Vector2Reference rightRaycastOriginPoint;
        [SerializeField] private Vector2Reference leftRaycastOriginPoint;
        [SerializeField] private IntReference rightHitsStorageIndex;
        [SerializeField] private IntReference leftHitsStorageIndex;
        [SerializeField] private FloatReference raycastHitAngle;
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
        public Vector2 RaycastFromBottom => raycastFromBottom.Value;
        public Vector2 RaycastToTop => raycastToTop.Value;
        public int NumberOfHorizontalRays => numberOfHorizontalRays.Value;
        public int HorizontalHitsStorageIndexesAmount => horizontalHitsStorageIndexesAmount.Value;
        public bool WasGroundedLastFrame => wasGroundedLastFrame.Value;

        /* properties */
        public float RaycastHitAngleRef
        {
            set => value = raycastHitAngle.Value;
        }

        public float RaycastHitAngle { get; set; }

        public Vector2 RightRaycastOriginPointRef
        {
            set => value = rightRaycastOriginPoint.Value;
        }

        public Vector2 RightRaycastOriginPoint { get; set; }

        public Vector2 LeftRaycastOriginPointRef
        {
            set => value = leftRaycastOriginPoint.Value;
        }

        public Vector2 LeftRaycastOriginPoint { get; set; }

        public int RightHitsStorageIndexRef
        {
            set => value = rightHitsStorageIndex.Value;
        }

        public int LeftHitsStorageIndexRef
        {
            set => value = leftHitsStorageIndex.Value;
        }
        
        public int RightHitsStorageIndex { get; set; }
        public int LeftHitsStorageIndex { get; set; }
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
    }
}

/* fields: dependencies */
/*

[SerializeField] private GravityController gravityController;
[SerializeField] private StickyRaycastController stickyRaycastController;
[SerializeField] private SafetyBoxcastController safetyBoxcastController;
[SerializeField] private BoolReference isCollidingWithMovingPlatform;
[SerializeField] private Vector3Reference movingPlatformCurrentSpeed;
[SerializeField] private BoolReference wasTouchingCeilingLastFrame;
[SerializeField] private FloatReference movementDirectionThreshold;
[SerializeField] private Vector2Reference externalForce;
[SerializeField] private BoolReference castRaysOnBothSides;
[SerializeField] private FloatReference tolerance;
[SerializeField] private FloatReference movementDirection;

/* fields */
/*
[SerializeField] private BoolReference setGravity;
[SerializeField] private BoolReference applyAscentMultiplierToGravity;
[SerializeField] private BoolReference applyFallMultiplierToGravity;
[SerializeField] private BoolReference applyGravityToSpeed;
[SerializeField] private BoolReference applyFallSlowFactorToSpeed;

/* fields: methods */
/*
/* properties: dependencies */
/*
public bool IsCollidingWithMovingPlatform => isCollidingWithMovingPlatform.Value;
public Vector3 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
public bool WasTouchingCeilingLastFrame => wasTouchingCeilingLastFrame.Value;
public float MovementDirectionThreshold => movementDirectionThreshold.Value;
public Vector2 ExternalForce => externalForce.Value;
public bool CastRaysOnBothSides => castRaysOnBothSides.Value;
public float Tolerance => tolerance.Value;
public float MovementDirection => movementDirection.Value;

}*/
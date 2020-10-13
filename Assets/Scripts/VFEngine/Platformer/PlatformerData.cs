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

        public Vector2 ExternalForce => externalForce.Value;
        /* properties */
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
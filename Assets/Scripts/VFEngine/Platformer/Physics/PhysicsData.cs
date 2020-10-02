using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Physics.Gravity;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics
{
    using static DebugExtensions;

    public class PhysicsData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private PhysicsSettings settings;
        [SerializeField] private GravityController gravityController;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private BoolReference isCollidingWithMovingPlatform;
        [SerializeField] private Vector3Reference movingPlatformCurrentSpeed;
        [SerializeField] private FloatReference movingPlatformCurrentGravity;
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame;

        /* fields */
        [SerializeField] private BoolReference stickToSlopesControl;
        [SerializeField] private BoolReference safetyBoxcastControl;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private BoolReference gravityActive;
        [SerializeField] private FloatReference fallSlowFactor;
        [SerializeField] private FloatReference movementDirectionThreshold;
        [SerializeField] private Vector2Reference externalForce;
        [SerializeField] private FloatReference tolerance;
        [SerializeField] private FloatReference movementDirection;
        private bool DisplayWarnings => settings.displayWarningsControl;
        private float MovementDirectionThreshold { get; } = 0.0001f;

        /* fields: methods */
        private void GetWarningMessage()
        {
            if (!DisplayWarnings) return;
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!settings) warningMessage += SettingsMessage("Physics Settings");
            if (!HasGravityController) warningMessage += FieldParentMessage("Gravity Controller");
            if (!characterTransform) warningMessage += FieldParentMessage("Transform");
            DebugLogWarning(warningMessageCount, warningMessage);

            string SettingsMessage(string scriptableObject)
            {
                warningMessageCount++;
                return $"Settings field not set to {scriptableObject} ScriptableObject.@";
            }

            string FieldParentMessage(string field)
            {
                warningMessageCount++;
                return $"{field} field not set to {field} Component of parent Character GameObject.@";
            }
        }

        /* properties: dependencies */
        public bool HasGravityController => gravityController;
        public bool IsCollidingWithMovingPlatform => isCollidingWithMovingPlatform.Value;
        public float MovingPlatformCurrentGravity => movingPlatformCurrentGravity.Value;
        public Vector3 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public bool WasTouchingCeilingLastFrame => wasTouchingCeilingLastFrame.Value;

        public Transform Transform
        {
            get => transform;
            set => value = transform;
        }

        /* properties */
        public bool SafetyBoxcastControl => safetyBoxcastControl.Value;
        public bool StickToSlopesControl => stickToSlopesControl.Value;
        public float Gravity => settings.gravity;
        public float FallMultiplier => settings.fallMultiplier;
        public float AscentMultiplier => settings.ascentMultiplier;
        public Vector2 MaximumVelocity => settings.maximumVelocity;
        public float SpeedAccelerationOnGround => settings.speedAccelerationOnGround;
        public float SpeedAccelerationInAir => settings.speedAccelerationInAir;
        public float SpeedFactor => settings.speedFactor;
        public float MaximumSlopeAngle => settings.maximumSlopeAngle;
        public AnimationCurve SlopeAngleSpeedFactor => settings.slopeAngleSpeedFactor;
        public float Physics2DPushForce => settings.physics2DPushForce;
        public bool Physics2DInteractionControl => settings.physics2DInteractionControl;
        public bool SafeSetTransformControl => settings.safeSetTransformControl;
        public bool AutomaticGravityControl => settings.automaticGravityControl;
        public Vector2 WorldSpeed { get; set; }
        public Vector2 ForcesApplied { get; set; }
        public float CurrentGravity { get; set; }
        public float StoredMovementDirection { get; set; }
        public Vector2 NewPosition { get; set; }

        public Vector2 Speed
        {
            get => speed.Value;
            set => value = speed.Value;
        }

        public float FallSlowFactor
        {
            get => fallSlowFactor.Value;
            set => value = fallSlowFactor.Value;
        }
        public Vector2 ExternalForce
        {
            get => externalForce.Value;
            set => value = externalForce.Value;
        }
        public float Tolerance
        {
            get => tolerance.Value;
            set => value = tolerance.Value;
        }
        public float MovementDirection
        {
            get => movementDirection.Value;
            set => value = movementDirection.Value;
        }

        public ModelState State { get; } = new ModelState();

        public void Initialize()
        {
            stickToSlopesControl.Value = settings.stickToSlopeControl;
            safetyBoxcastControl.Value = settings.safetyBoxcastControl;
            transform.Value = characterTransform;
            gravityActive.Value = State.GravityActive;
            FallSlowFactor = FallMultiplier;
            movementDirectionThreshold.Value = MovementDirectionThreshold;
            ExternalForce = new Vector2();
            Tolerance = 0;
            MovementDirection = 0;
            GetWarningMessage();
        }

        public class ModelState
        {
            public bool IsFalling { get; private set; }
            public bool IsJumping { get; private set; }
            public bool GravityActive { get; private set; }

            public void SetIsFalling(bool isFalling)
            {
                IsFalling = isFalling;
            }

            public void SetIsJumping(bool isJumping)
            {
                IsJumping = isJumping;
            }

            public void SetGravity(bool gravity)
            {
                GravityActive = gravity;
            }

            public void Reset()
            {
                IsFalling = true;
            }
        }
    }
}

/*
        public bool onMovingPlatform;
        public bool hitCeilingLastFrame;
        public bool isGrounded;
        public bool raycastHit;
        public bool wasGroundedLastFrame;
        public float movingPlatformCurrentGravity;
        public float raycastDirection;
        public float distanceToWall;
        public float boundsWidth;
        public float raycastOffset;
        public float distanceToGround;
        public float boundsHeight;
        public float belowSlopeAngleLeft;
        public float belowSlopeAngleRight;
        public float raycastOriginY;
        public float smallestDistance;
        public float belowSlopeAngle;
        public Object ignoredCollider;
        public IEnumerable<RaycastHit2D> ContactList;
        public RaycastHit2D StickyRaycast;
        public RaycastHit2D SafetyBoxcast;
        public PhysicsMaterialController frictionTest;
        public PathMovementController movingPlatformTest;
        public PathMovementController movingPlatform;
        */
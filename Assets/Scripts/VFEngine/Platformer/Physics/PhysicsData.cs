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
        [SerializeField] private new Transform transform;
        [SerializeField] private TransformReference transformReference;

        /* fields */
        [SerializeField] private BoolReference stickToSlopesControl;
        [SerializeField] private BoolReference safetyBoxcastControl;
        private bool DisplayWarnings => settings.displayWarningsControl;

        /* fields: methods */
        private void GetWarningMessage()
        {
            if (!DisplayWarnings) return;
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!settings) warningMessage += SettingsMessage("Physics Settings");
            if (!HasGravityController) warningMessage += FieldParentMessage("Gravity Controller");
            if (!transform) warningMessage += FieldParentMessage("Transform");
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

        /* properties */
        public const float Tolerance = 0;
        public const float MovementDirectionThreshold = 0.0001f;
        public bool HasGravityController => gravityController;
        public bool SafetyBoxcastControl => safetyBoxcastControl.Value;
        public bool StickToSlopesControl => stickToSlopesControl.Value;

        public Transform Transform
        {
            get => transform;
            set => value = transform;
        }

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
        public Vector2 Speed { get; set; }
        public Vector2 ExternalForce { get; set; }
        public Vector2 ForcesApplied { get; set; }
        public float CurrentGravity { get; set; }
        public float MovementDirection { get; set; }
        public float StoredMovementDirection { get; set; }
        public Vector2 NewPosition { get; set; }
        public float FallSlowFactor { get; set; }
        public ModelState State { get; } = new ModelState();

        public void Initialize()
        {
            stickToSlopesControl.Value = settings.stickToSlopeControl;
            safetyBoxcastControl.Value = settings.safetyBoxcastControl;
            transformReference.Value = transform;
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
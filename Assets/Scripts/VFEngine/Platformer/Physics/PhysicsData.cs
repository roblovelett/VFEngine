using UnityEngine;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics
{
    using static ScriptableObjectExtensions;

    public class PhysicsData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private PhysicsSettings settings;
        [SerializeField] private GameObject character;
        
        #endregion

        private const string PhPath = "Physics/";
        private static readonly string ModelAssetPath = $"{PhPath}PhysicsModel.asset";

        #endregion

        #region properties

        #region dependencies

        public Transform Transform => character.transform;
        public PhysicsSettings Settings => settings;
        public float Physics2DPushForce => settings.physics2DPushForce;
        public bool Physics2DInteractionControl => settings.physics2DInteractionControl;
        public Vector2 MaximumVelocity => settings.maximumVelocity;
        public AnimationCurve SlopeAngleSpeedFactor => settings.slopeAngleSpeedFactor;
        public bool SafetyBoxcastControl => settings.safetyBoxcastControl;
        public float MaximumSlopeAngle => settings.maximumSlopeAngle;
        public bool StickToSlopesControl => settings.stickToSlopeControl;
        public bool SafeSetTransformControl => settings.safeSetTransformControl;
        public bool DisplayWarningsControl => settings.displayWarningsControl;
        public bool AutomaticGravityControl => settings.automaticGravityControl;
        public float AscentMultiplier => settings.ascentMultiplier;
        public float FallMultiplier => settings.fallMultiplier;
        public float Gravity => settings.gravity;

        #endregion

        public float CurrentVerticalSpeedFactor { get; set; }
        public bool IsJumping { get; set; }
        public bool IsFalling { get; set; }
        public bool GravityActive { get; set; }
        public int HorizontalMovementDirection { get; set; }
        public float FallSlowFactor { get; set; }
        public float SmallValue { get; set; }
        public float MovementDirectionThreshold { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 NewPosition { get; set; }

        public float NewPositionY
        {
            set => value = NewPosition.y;
        }

        public float NewPositionX
        {
            set => value = NewPosition.x;
        }

        public Vector2 ExternalForce { get; set; }

        public float ExternalForceY
        {
            set => value = ExternalForce.y;
        }

        public Vector2 WorldSpeed { get; set; } = Vector2.zero;
        public Vector2 ForcesApplied { get; set; } = Vector2.zero;
        public Rigidbody2D CurrentHitRigidBody { get; set; }
        public bool CurrentHitRigidBodyCanBePushed { get; set; }
        public Vector2 CurrentPushDirection { get; set; } = Vector2.zero;
        public float CurrentGravity { get; set; }
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

        public static readonly string PhysicsModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
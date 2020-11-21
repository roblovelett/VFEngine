using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PhysicsData", menuName = PlatformerPhysicsDataPath, order = 0)]
    [InlineEditor]
    public class PhysicsData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character = null;
        [SerializeField] private PhysicsSettings settings = null;

        #endregion

        private const string PhPath = "Physics/";
        private static readonly string ModelAssetPath = $"{PhPath}PhysicsModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public UpRaycastRuntimeData UpRaycastRuntimeData { get; set; }
        public LeftStickyRaycastRuntimeData LeftStickyRaycastRuntimeData { get; set; }
        public RightStickyRaycastRuntimeData RightStickyRaycastRuntimeData { get; set; }
        public SafetyBoxcastRuntimeData SafetyBoxcastRuntimeData { get; set; }
        public RaycastHitColliderRuntimeData RaycastHitColliderRuntimeData { get; set; }
        public LeftRaycastHitColliderRuntimeData LeftRaycastHitColliderRuntimeData { get; set; }
        public RightRaycastHitColliderRuntimeData RightRaycastHitColliderRuntimeData { get; set; }
        public DownRaycastHitColliderRuntimeData DownRaycastHitColliderRuntimeData { get; set; }
        public StickyRaycastHitColliderRuntimeData StickyRaycastHitColliderRuntimeData { get; set; }
        public GameObject Character => character;
        public Transform Transform { get; set; }
        public bool HasTransform => Transform;
        public bool HasSettings => settings;
        public float Physics2DPushForceSetting => settings.physics2DPushForce;
        public bool Physics2DInteractionControlSetting => settings.physics2DInteractionControl;
        public Vector2 MaximumVelocitySetting => settings.maximumVelocity;
        public AnimationCurve SlopeAngleSpeedFactorSetting => settings.slopeAngleSpeedFactor;
        public bool SafetyBoxcastControlSetting => settings.safetyBoxcastControl;
        public float MaximumSlopeAngleSetting => settings.maximumSlopeAngle;
        public bool StickToSlopesControlSetting => settings.stickToSlopeControl;
        public bool SafeSetTransformControlSetting => settings.safeSetTransformControl;
        public bool DisplayWarningsControlSetting => settings.displayWarningsControl;
        public bool AutomaticGravityControlSetting => settings.automaticGravityControl;
        public float AscentMultiplierSetting => settings.ascentMultiplier;
        public float FallMultiplierSetting => settings.fallMultiplier;
        public float GravitySetting => settings.gravity;
        public float MovingPlatformCurrentGravity { get; set; }
        public Vector2 MovingPlatformCurrentSpeed { get; set; }
        public float BoundsWidth { get; set; }
        public float RayOffset { get; set; }
        public float CurrentDownHitSmallestDistance { get; set; }
        public float BoundsHeight { get; set; }
        public RaycastHit2D SafetyBoxcastHit { get; set; }
        public float LeftStickyRaycastOriginY { get; set; }
        public float RightStickyRaycastOriginY { get; set; }
        public RaycastHit2D LeftStickyRaycastHit { get; set; }
        public RaycastHit2D RightStickyRaycastHit { get; set; }
        public float UpRaycastSmallestDistance { get; set; }
        public float DistanceBetweenRightHitAndRaycastOrigin { get; set; }
        public float DistanceBetweenLeftHitAndRaycastOrigin { get; set; }
        public float BelowSlopeAngle { get; set; }
        public RaycastHitColliderContactList ContactList { get; set; }

        #endregion

        public PhysicsRuntimeData RuntimeData { get; set; }
        public AnimationCurve SlopeAngleSpeedFactor { get; set; }
        public float CurrentVerticalSpeedFactor { get; set; }
        public bool Physics2DInteractionControl { get; set; }
        public bool SafetyBoxcastControl { get; set; }
        public bool StickToSlopesControl { get; set; }
        public bool SafeSetTransformControl { get; set; }
        public bool IsJumping { get; set; }
        public bool IsFalling { get; set; }
        public bool GravityActive { get; set; }
        public int HorizontalMovementDirection { get; set; }
        public float Physics2DPushForce { get; set; }
        public float MaximumSlopeAngle { get; set; }
        public float FallSlowFactor { get; set; }
        public float SmallValue { get; set; }
        public float Gravity { get; set; }
        public float MovementDirectionThreshold { get; set; }
        public Vector2 MaximumVelocity { get; set; }
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

        public bool DisplayWarningsControl { get; set; }
        public bool AutomaticGravityControl { get; set; }
        public float AscentMultiplier { get; set; }
        public float FallMultiplier { get; set; }
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

        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
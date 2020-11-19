using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PlatformerData", menuName = PlatformerDataPath, order = 0)]
    [InlineEditor]
    public class PlatformerData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private PlatformerSettings settings = null;
        [SerializeField] private PhysicsController physics = null;
        [SerializeField] private RaycastController raycast = null;
        [SerializeField] private RaycastHitColliderController raycastHitCollider = null;
        [SerializeField] private LayerMaskController layerMask = null;
        [SerializeField] private BoxcastController boxcast = null;

        #endregion

        private const string ModelAssetPath = "PlatformerModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool HasSettings => settings;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public PhysicsController Physics => physics;
        public RaycastController Raycast => raycast;
        public RaycastHitColliderController RaycastHitCollider => raycastHitCollider;
        public LayerMaskController LayerMask => layerMask;
        public BoxcastController Boxcast => boxcast;
        public GameObject Character => character;
        public Transform Transform => character.transform;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public float DistanceToGroundRayMaximumLength { get; set; }
        public bool IsCollidingAbove { get; set; }
        public bool IsCollidingBelow { get; set; }
        public bool IsCollidingLeft { get; set; }
        public bool IsCollidingRight { get; set; }
        public int LeftHitsStorageLength { get; set; }
        public int RightHitsStorageLength { get; set; }
        public Vector2 Speed { get; set; }
        public bool GravityActive { get; set; }
        public float FallSlowFactor { get; set; }
        public Vector2 MovingPlatformCurrentSpeed { get; set; }
        public bool WasTouchingCeilingLastFrame { get; set; }
        public float MovementDirectionThreshold { get; set; }
        public bool CastRaysOnBothSides { get; set; }
        public Vector2 ExternalForce { get; set; }
        public int HorizontalMovementDirection { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public bool WasGroundedLastFrame { get; set; }
        public float CurrentRightHitDistance { get; set; }
        public float CurrentLeftHitDistance { get; set; }
        public Collider2D CurrentRightHitCollider { get; set; }
        public Collider2D CurrentLeftHitCollider { get; set; }
        public Collider2D IgnoredCollider { get; set; }
        public float CurrentRightHitAngle { get; set; }
        public float CurrentLeftHitAngle { get; set; }
        public float MaximumSlopeAngle { get; set; }
        public bool GroundedEvent { get; set; }
        public Vector2 NewPosition { get; set; }
        public float SmallValue { get; set; }
        public float Gravity { get; set; }
        public bool IsFalling { get; set; }
        public bool OnMovingPlatform { get; set; }
        public int DownHitsStorageLength { get; set; }
        public int NumberOfVerticalRaysPerSide { get; set; }
        public GameObject StandingOnLastFrame { get; set; }
        public bool HasStandingOnLastFrame { get; set; }
        public Collider2D StandingOnCollider { get; set; }
        public Vector2 BoundsBottomCenterPosition { get; set; }
        public float SmallestDistanceToDownHit { get; set; }
        public bool DownHitConnected { get; set; }
        public float CurrentDownHitSmallestDistance { get; set; }
        public Vector3 CrossBelowSlopeAngle { get; set; }
        public float BoundsHeight { get; set; }
        public LayerMask MidHeightOneWayPlatformMask { get; set; }
        public LayerMask StairsMask { get; set; }
        public LayerMask StandingOnWithSmallestDistanceLayer { get; set; }
        public LayerMask OneWayPlatformMask { get; set; }
        public LayerMask MovingOneWayPlatformMask { get; set; }
        public bool HasPhysicsMaterialDataClosestToDownHit { get; set; }
        public bool HasPathMovementControllerClosestToDownHit { get; set; }
        public bool HasMovingPlatform { get; set; }
        public bool StickToSlopesControl { get; set; }
        public float StickToSlopesOffsetY { get; set; }
        public bool IsJumping { get; set; }
        public float StickyRaycastLength { get; set; }
        public float LeftStickyRaycastLength { get; set; }
        public float RightStickyRaycastLength { get; set; }
        public Vector3 CrossBelowSlopeAngleLeft { get; set; }
        public Vector3 CrossBelowSlopeAngleRight { get; set; }
        public float BelowSlopeAngleLeft { get; set; }
        public float BelowSlopeAngleRight { get; set; }
        public bool IsCastingLeft { get; set; }
        public bool SafetyBoxcastControl { get; set; }
        public RaycastHit2D RaycastDownHitAt { get; set; }
        public RaycastHit2D SafetyBoxcastHit { get; set; }
        public RaycastHit2D LeftStickyRaycastHit { get; set; }
        public RaycastHit2D RightStickyRaycastHit { get; set; }
        public RaycastHit2D RaycastUpHitAt { get; set; }
        public RaycastHit2D DistanceToGroundRaycastHit { get; set; }
        public int UpHitsStorageLength { get; set; }
        public float UpRaycastSmallestDistance { get; set; }
        public bool UpHitConnected { get; set; }
        public bool RightHitConnected { get; set; }
        public bool LeftHitConnected { get; set; }
        public bool DistanceToGroundRaycastHitConnected { get; set; }

        #endregion

        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
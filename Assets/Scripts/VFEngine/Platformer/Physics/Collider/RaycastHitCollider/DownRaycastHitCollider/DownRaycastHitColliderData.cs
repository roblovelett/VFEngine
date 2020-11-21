using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Platformer.Physics.PhysicsMaterial;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "DownRaycastHitColliderData", menuName = PlatformerDownRaycastHitColliderDataPath,
        order = 0)]
    [InlineEditor]
    public class DownRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character = null;

        #endregion

        private static readonly string DownRaycastHitColliderPath = $"{RaycastHitColliderPath}DownRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{DownRaycastHitColliderPath}DownRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public DownRaycastRuntimeData DownRaycastRuntimeData { get; set; }
        public LayerMaskRuntimeData LayerMaskRuntimeData { get; set; }
        public GameObject Character => character;
        public Transform Transform { get; set; }
        public int NumberOfVerticalRaysPerSide { get; set; }
        public int SavedBelowLayer { get; set; }
        public Vector2 DownRaycastFromLeft { get; set; }
        public Vector2 DownRaycastToRight { get; set; }
        public RaycastHit2D CurrentDownRaycastHit { get; set; }

        #endregion

        public DownRaycastHitColliderRuntimeData RuntimeData { get; set; }
        public bool HasPhysicsMaterialClosestToDownHit { get; set; }
        public bool HasPathMovementClosestToDownHit { get; set; }
        public bool DownHitConnected { get; set; }
        public bool IsCollidingBelow { get; set; }
        public bool OnMovingPlatform { get; set; }
        public bool HasMovingPlatform { get; set; }
        public bool HasStandingOnLastFrame { get; set; }
        public bool HasGroundedLastFrame { get; set; }
        public bool GroundedEvent { get; set; }
        public int DownHitsStorageLength { get; set; }
        public int CurrentDownHitsStorageIndex { get; set; }
        public float SmallestDistanceToDownHit { get; set; }
        public float MovingPlatformCurrentGravity { get; set; }
        public float CurrentDownHitSmallestDistance { get; set; }
        public Vector2 MovingPlatformCurrentSpeed { get; set; }
        public Vector3 CrossBelowSlopeAngle { get; set; }
        public RaycastHit2D RaycastDownHitAt { get; set; }
        public RaycastHit2D[] DownHitsStorage { get; set; }
        public Collider2D StandingOnCollider { get; set; }
        public LayerMask StandingOnWithSmallestDistanceLayer { get; set; }
        public GameObject StandingOnLastFrame { get; set; }
        [CanBeNull] public PhysicsMaterialData PhysicsMaterialClosestToDownHit { get; set; }
        [CanBeNull] public PathMovementData PathMovementClosestToDownHit { get; set; }
        public float Friction { get; set; }
        public int DownHitsStorageSmallestDistanceIndex { get; set; }
        public RaycastHit2D DownHitWithSmallestDistance { get; set; }
        public float BelowSlopeAngle { get; set; }
        public PathMovementData MovingPlatform { get; set; }
        public float MovingPlatformGravity { get; } = -500f;
        public GameObject StandingOn { get; set; }
        public GameObject StandingOnWithSmallestDistance { get; set; }

        public static readonly string DownRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
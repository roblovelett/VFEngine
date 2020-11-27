using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Platformer.Physics.PhysicsMaterial;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
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

        #endregion

        private static readonly string DownRaycastHitColliderPath = $"{RaycastHitColliderPath}DownRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{DownRaycastHitColliderPath}DownRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

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
        public PhysicsMaterialData PhysicsMaterialClosestToDownHit { get; set; }
        public PathMovementData PathMovementClosestToDownHit { get; set; }
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
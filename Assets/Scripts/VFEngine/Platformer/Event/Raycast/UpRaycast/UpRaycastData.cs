using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class UpRaycastData
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string UpRaycastPath = $"{RaycastPath}UpRaycast/";
        private static readonly string ModelAssetPath = $"{UpRaycastPath}UpRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public UpRaycastHitColliderRuntimeData UpRaycastHitColliderRuntimeData { get; set; }
        public DownRaycastHitColliderRuntimeData DownRaycastHitColliderRuntimeData { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public LayerMaskRuntimeData LayerMaskRuntimeData { get; set; }
        public bool GroundedEvent { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public int CurrentUpHitsStorageIndex { get; set; }
        public int NumberOfVerticalRaysPerSide { get; set; }
        public float RayOffset { get; set; }
        public Vector2 NewPosition { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsTopLeftCorner { get; set; }
        public Vector2 BoundsTopRightCorner { get; set; }
        public LayerMask PlatformMask { get; set; }
        public LayerMask OneWayPlatformMask { get; set; }
        public LayerMask MovingOneWayPlatformMask { get; set; }
        public RaycastHit2D RaycastUpHitAt { get; set; }

        #endregion

        public UpRaycastRuntimeData RuntimeData { get; set; }
        public float UpRayLength { get; set; }
        public Vector2 UpRaycastStart { get; set; } = Vector2.zero;
        public Vector2 UpRaycastEnd { get; set; } = Vector2.zero;
        public float UpRaycastSmallestDistance { get; set; }
        public Vector2 CurrentUpRaycastOrigin { get; set; }
        public RaycastHit2D CurrentUpRaycastHit { get; set; }
        public static readonly string UpRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
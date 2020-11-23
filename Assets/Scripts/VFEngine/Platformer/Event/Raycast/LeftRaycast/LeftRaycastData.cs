using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;
    public class LeftRaycastData
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string LeftRaycastPath = $"{RaycastPath}RightRaycast/";
        private static readonly string ModelAssetPath = $"{LeftRaycastPath}LeftRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public LeftRaycastHitColliderRuntimeData LeftRaycastHitColliderRuntimeData { get; set; }
        public LayerMaskRuntimeData LayerMaskRuntimeData { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public int CurrentLeftHitsStorageIndex { get; set; }
        public float RayOffset { get; set; }
        public float ObstacleHeightTolerance { get; set; }
        public float BoundsWidth { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsTopLeftCorner { get; set; }
        public Vector2 BoundsTopRightCorner { get; set; }
        public Vector2 Speed { get; set; }
        public LayerMask PlatformMask { get; set; }
        public LayerMask OneWayPlatformMask { get; set; }
        public LayerMask MovingOneWayPlatformMask { get; set; }

        #endregion

        public LeftRaycastRuntimeData RuntimeData { get; set; }
        public float LeftRayLength { get; set; }
        public Vector2 LeftRaycastFromBottomOrigin { get; set; }
        public Vector2 LeftRaycastToTopOrigin { get; set; }
        public RaycastHit2D CurrentLeftRaycastHit { get; set; }
        public Vector2 CurrentLeftRaycastOrigin { get; set; }
        public static readonly string LeftRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
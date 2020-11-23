using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    
    public class RightRaycastData
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string RightRaycastPath = $"{RaycastPath}RightRaycast/";
        private static readonly string ModelAssetPath = $"{RightRaycastPath}RightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public RightRaycastHitColliderRuntimeData RightRaycastHitColliderRuntimeData { get; set; }
        public LayerMaskRuntimeData LayerMaskRuntimeData { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public int CurrentRightHitsStorageIndex { get; set; }
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

        public RightRaycastRuntimeData RuntimeData { get; set; }
        public float RightRayLength { get; set; }
        public Vector2 RightRaycastFromBottomOrigin { get; set; }
        public Vector2 RightRaycastToTopOrigin { get; set; }
        public RaycastHit2D CurrentRightRaycastHit { get; set; }
        public Vector2 CurrentRightRaycastOrigin { get; set; }
        public static readonly string RightRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
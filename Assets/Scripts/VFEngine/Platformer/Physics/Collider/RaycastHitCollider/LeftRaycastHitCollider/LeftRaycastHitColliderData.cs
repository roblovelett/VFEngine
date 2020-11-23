using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    
    public class LeftRaycastHitColliderData
    {
        #region fields

        #region dependencies

        
        #endregion

        private static readonly string LeftRaycastHitColliderPath = $"{RaycastHitColliderPath}LeftRaycastHitCollider/";

        private static readonly string
            ModelAssetPath = $"{LeftRaycastHitColliderPath}LeftRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public LeftRaycastRuntimeData LeftRaycastRuntimeData { get; set; }
        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public RaycastHit2D CurrentLeftRaycastHit { get; set; }
        public Vector2 LeftRaycastFromBottomOrigin { get; set; }
        public Vector2 LeftRaycastToTopOrigin { get; set; }

        #endregion

        public LeftRaycastHitColliderRuntimeData RuntimeData { get; set; }
        public RaycastHit2D[] LeftHitsStorage { get; set; } = new RaycastHit2D[0];
        public bool LeftHitConnected { get; set; }
        public bool IsCollidingLeft { get; set; }
        public int LeftHitsStorageLength { get; set; }
        public int CurrentLeftHitsStorageIndex { get; set; }
        public float CurrentLeftHitAngle { get; set; }
        public float CurrentLeftHitDistance { get; set; }
        public float DistanceBetweenLeftHitAndRaycastOrigin { get; set; }
        public Collider2D CurrentLeftHitCollider { get; set; }
        public bool PassedLeftSlopeAngle { get; set; }
        public GameObject CurrentLeftWallCollider { get; set; }
        public float DistanceToLeftCollider { get; set; }
        public float LeftLateralSlopeAngle { get; set; }

        public static readonly string LeftRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment
// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    
    public class RightRaycastHitColliderData
    {
        #region fields

        #region dependencies

        
        #endregion

        private static readonly string RightRaycastHitColliderPath =
            $"{RaycastHitColliderPath}RightRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{RightRaycastHitColliderPath}RightRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public RightRaycastRuntimeData RightRaycastRuntimeData { get; set; }
        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public RaycastHit2D CurrentRightRaycastHit { get; set; }
        public Vector2 RightRaycastFromBottomOrigin { get; set; }
        public Vector2 RightRaycastToTopOrigin { get; set; }

        #endregion

        public RightRaycastHitColliderRuntimeData RuntimeData { get; set; }
        public bool RightHitConnected { get; set; }
        public bool IsCollidingRight { get; set; }
        public int RightHitsStorageLength { get; set; }
        public int CurrentRightHitsStorageIndex { get; set; }
        public float CurrentRightHitAngle { get; set; }
        public float CurrentRightHitDistance { get; set; }
        public float DistanceBetweenRightHitAndRaycastOrigin { get; set; }
        public Collider2D CurrentRightHitCollider { get; set; }
        public RaycastHit2D[] RightHitsStorage { get; set; } = new RaycastHit2D[0];
        public bool PassedRightSlopeAngle { get; set; }
        public GameObject CurrentRightWallCollider { get; set; }
        public float DistanceToRightCollider { get; set; }
        public float RightLateralSlopeAngle { get; set; }

        public static readonly string RightRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
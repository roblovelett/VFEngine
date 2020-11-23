using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class DistanceToGroundRaycastData
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string DistanceToGroundRaycastPath = $"{RaycastPath}DistanceToGroundRaycast/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastPath}DistanceToGroundRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public PhysicsRuntimeData PhysicsRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public StickyRaycastHitColliderRuntimeData StickyRaycastHitColliderRuntimeData { get; set; }
        public LayerMaskRuntimeData LayerMaskRuntimeData { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public float BelowSlopeAngle { get; set; }
        public float DistanceToGroundRayMaximumLength { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }

        #endregion

        public DistanceToGroundRaycastRuntimeData RuntimeData { get; set; }
        public RaycastHit2D DistanceToGroundRaycastHit { get; set; }
        public Vector2 DistanceToGroundRaycastOrigin { get; set; } = Vector2.zero;

        public static readonly string DistanceToGroundRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
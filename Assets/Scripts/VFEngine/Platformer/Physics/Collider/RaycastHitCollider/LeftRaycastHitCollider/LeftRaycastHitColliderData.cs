using UnityEngine;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class LeftRaycastHitColliderData : ScriptableObject
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

        #endregion

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
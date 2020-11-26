using UnityEngine;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class UpRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string UpRaycastHitColliderPath = $"{RaycastHitColliderPath}UpRaycastHitCollider/";
        private static readonly string ModelAssetPath = $"{UpRaycastHitColliderPath}UpRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public bool UpHitConnected { get; set; }
        public bool IsCollidingAbove { get; set; }
        public bool WasTouchingCeilingLastFrame { get; set; }
        public int UpHitsStorageLength { get; set; }
        public int UpHitsStorageCollidingIndex { get; set; }
        public int CurrentUpHitsStorageIndex { get; set; }
        public RaycastHit2D RaycastUpHitAt { get; set; }
        public RaycastHit2D[] UpHitsStorage { get; set; } = new RaycastHit2D[0];

        public static readonly string UpRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
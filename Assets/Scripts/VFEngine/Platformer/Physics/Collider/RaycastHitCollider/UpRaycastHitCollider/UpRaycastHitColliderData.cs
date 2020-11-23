using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    
    public class UpRaycastHitColliderData
    {
        #region fields

        #region dependencies

        

        #endregion

        private static readonly string UpRaycastHitColliderPath = $"{RaycastHitColliderPath}UpRaycastHitCollider/";
        private static readonly string ModelAssetPath = $"{UpRaycastHitColliderPath}UpRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public UpRaycastRuntimeData UpRaycastRuntimeData { get; set; }
        public GameObject Character { get; set; }
        public Transform Transform { get; set; }
        public int NumberOfVerticalRaysPerSide { get; set; }
        public RaycastHit2D CurrentUpRaycastHit { get; set; }

        #endregion

        public UpRaycastHitColliderRuntimeData RuntimeData { get; set; }
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
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    
    public class DistanceToGroundRaycastHitColliderData
    {
        #region fields

        #region depenedencies
        
        #endregion

        private static readonly string DistanceToGroundRaycastHitColliderPath =
            $"{RaycastHitColliderPath}DistanceToGroundRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastHitColliderPath}DistanceToGroundRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region depenedencies

        public RaycastRuntimeData RaycastRuntimeData { get; set; }
        public DistanceToGroundRaycastRuntimeData DistanceToGroundRaycastRuntimeData { get; set; }
        public GameObject Character { get; set; }
        public float DistanceToGroundRayMaximumLength { get; set; }
        public RaycastHit2D DistanceToGroundRaycastHit { get; set; }
        public float BoundsHeight { get; set; }

        #endregion

        public DistanceToGroundRaycastHitColliderRuntimeData RuntimeData { get; set; }
        public float DistanceToGround { get; set; }
        public bool DistanceToGroundRaycastHitConnected { get; set; }

        public static readonly string DistanceToGroundRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
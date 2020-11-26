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

    public class DistanceToGroundRaycastHitColliderData : ScriptableObject
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

        #endregion
        public float DistanceToGround { get; set; }
        public bool DistanceToGroundRaycastHitConnected { get; set; }

        public static readonly string DistanceToGroundRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
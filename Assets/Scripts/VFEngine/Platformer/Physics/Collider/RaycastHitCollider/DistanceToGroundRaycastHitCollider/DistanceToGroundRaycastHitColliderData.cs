using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastHitColliderData",
        menuName = PlatformerDistanceToGroundRaycastHitColliderDataPath, order = 0)]
    [InlineEditor]
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
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider
{
    using static StickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class LeftStickyRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string LeftStickyRaycastHitColliderPath =
            $"{StickyRaycastHitColliderPath}LeftStickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{LeftStickyRaycastHitColliderPath}LeftStickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public float BelowSlopeAngleLeft { get; set; }
        public Vector3 CrossBelowSlopeAngleLeft { get; set; }

        public static readonly string LeftStickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
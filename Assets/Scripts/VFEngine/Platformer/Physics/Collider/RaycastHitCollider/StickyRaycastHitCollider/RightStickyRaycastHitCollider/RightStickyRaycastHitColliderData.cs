using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider
{
    using static StickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class RightStickyRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string RightStickyRaycastHitColliderPath =
            $"{StickyRaycastHitColliderPath}RightStickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{RightStickyRaycastHitColliderPath}RightStickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public float BelowSlopeAngleRight { get; set; }
        public Vector3 CrossBelowSlopeAngleRight { get; set; }

        public static readonly string RightStickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class StickyRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string StickyRaycastHitColliderPathInternal =
            $"{RaycastHitColliderPath}/StickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{StickyRaycastHitColliderPathInternal}StickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public float BelowSlopeAngle { get; set; }
        public static readonly string StickyRaycastHitColliderPath = StickyRaycastHitColliderPathInternal;

        public static readonly string StickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
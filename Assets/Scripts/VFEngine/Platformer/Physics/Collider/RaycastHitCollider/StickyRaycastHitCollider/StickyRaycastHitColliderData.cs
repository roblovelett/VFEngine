using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;
    
    public class StickyRaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private FloatReference belowSlopeAngleRight;
        [SerializeField] private FloatReference belowSlopeAngleLeft;

        #endregion
        
        private static readonly string StickyRaycastHitColliderPathInternal = $"{RaycastHitColliderPath}/StickyRaycastHitCollider/";
        private static readonly string ModelAssetPath = $"{StickyRaycastHitColliderPathInternal}DefaultStickyRaycastHitColliderModel.asset";
        
        #endregion

        #region properties

        public float BelowSlopeAngleRight => belowSlopeAngleRight.Value;
        public float BelowSlopeAngleLeft => belowSlopeAngleLeft.Value;

        #region dependencies

        public float belowSlopeAngle;

        #endregion

        public static readonly string StickyRaycastHitColliderPath = StickyRaycastHitColliderPathInternal;
        public static readonly string StickyRaycastHitColliderModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class StickyRaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private FloatReference belowSlopeAngleRight = new FloatReference();
        [SerializeField] private FloatReference belowSlopeAngleLeft = new FloatReference();

        #endregion

        [SerializeField] private FloatReference belowSlopeAngle = new FloatReference();

        private static readonly string StickyRaycastHitColliderPathInternal =
            $"{RaycastHitColliderPath}/StickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{StickyRaycastHitColliderPathInternal}StickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        public float BelowSlopeAngleRight => belowSlopeAngleRight.Value;
        public float BelowSlopeAngleLeft => belowSlopeAngleLeft.Value;

        #region dependencies

        public float BelowSlopeAngle
        {
            set => value = belowSlopeAngle.Value;
        }

        #endregion

        public static readonly string StickyRaycastHitColliderPath = StickyRaycastHitColliderPathInternal;

        public static readonly string StickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
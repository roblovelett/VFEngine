using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "StickyRaycastHitColliderData", menuName = PlatformerStickyRaycastHitColliderDataPath,
        order = 0)]
    [InlineEditor]
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

        public RightStickyRaycastHitColliderRuntimeData RightStickyRaycastHitColliderRuntimeData { get; set; }
        public LeftStickyRaycastHitColliderRuntimeData LeftStickyRaycastHitColliderRuntimeData { get; set; }
        public float BelowSlopeAngleRight { get; set; }
        public float BelowSlopeAngleLeft { get; set; }
        public GameObject Character { get; set; }

        #endregion

        public StickyRaycastHitColliderRuntimeData RuntimeData { get; set; }
        public float BelowSlopeAngle { get; set; }
        public static readonly string StickyRaycastHitColliderPath = StickyRaycastHitColliderPathInternal;

        public static readonly string StickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
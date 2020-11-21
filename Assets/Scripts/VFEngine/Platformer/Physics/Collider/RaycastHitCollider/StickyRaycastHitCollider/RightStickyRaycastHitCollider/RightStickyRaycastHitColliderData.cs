using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider
{
    using static StickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RightStickyRaycastHitColliderData",
        menuName = PlatformerRightStickyRaycastHitColliderDataPath, order = 0)]
    [InlineEditor]
    public class RightStickyRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character = null;

        #endregion

        private static readonly string RightStickyRaycastHitColliderPath =
            $"{StickyRaycastHitColliderPath}RightStickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{RightStickyRaycastHitColliderPath}RightStickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public RightStickyRaycastRuntimeData RightStickyRaycastRuntimeData { get; set; }
        public GameObject Character => character;
        public Transform Transform { get; set; }
        public RaycastHit2D RightStickyRaycastHit { get; set; }

        #endregion

        public RightStickyRaycastHitColliderRuntimeData RuntimeData { get; set; }
        public float BelowSlopeAngleRight { get; set; }
        public Vector3 CrossBelowSlopeAngleRight { get; set; }

        public static readonly string RightStickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
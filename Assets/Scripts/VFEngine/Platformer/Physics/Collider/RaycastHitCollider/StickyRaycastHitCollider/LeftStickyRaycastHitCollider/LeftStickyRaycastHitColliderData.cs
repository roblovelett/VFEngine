using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider
{
    using static StickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LeftStickyRaycastHitColliderData",
        menuName = PlatformerLeftStickyRaycastHitColliderDataPath, order = 0)]
    [InlineEditor]
    public class LeftStickyRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character = null;

        #endregion

        private static readonly string LeftStickyRaycastHitColliderPath =
            $"{StickyRaycastHitColliderPath}LeftStickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{LeftStickyRaycastHitColliderPath}LeftStickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PlatformerRuntimeData PlatformerRuntimeData { get; set; }
        public LeftStickyRaycastRuntimeData LeftStickyRaycastRuntimeData { get; set; }
        public GameObject Character => character;
        public Transform Transform { get; set; }
        public RaycastHit2D LeftStickyRaycastHit { get; set; }

        #endregion

        public LeftStickyRaycastHitColliderRuntimeData RuntimeData { get; set; }
        public float BelowSlopeAngleLeft { get; set; }
        public Vector3 CrossBelowSlopeAngleLeft { get; set; }

        public static readonly string LeftStickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
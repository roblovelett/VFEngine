using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;
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

        [SerializeField] private GameObjectReference character = null;

        #endregion

        [SerializeField] private FloatReference belowSlopeAngleRight = new FloatReference();
        [SerializeField] private Vector3Reference crossBelowSlopeAngleRight = new Vector3Reference();

        private static readonly string RightStickyRaycastHitColliderPath =
            $"{StickyRaycastHitColliderPath}RightStickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{RightStickyRaycastHitColliderPath}RightStickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character.Value;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public Transform Transform { get; set; }
        public RaycastHit2D RightStickyRaycastHit { get; set; }

        #endregion

        public float BelowSlopeAngleRight
        {
            get => belowSlopeAngleRight.Value;
            set => value = belowSlopeAngleRight.Value;
        }

        public Vector3 CrossBelowSlopeAngleRight
        {
            get => crossBelowSlopeAngleRight.Value;
            set => value = crossBelowSlopeAngleRight.Value;
        }

        public static readonly string RightStickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
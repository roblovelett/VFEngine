using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider
{
    using static StickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LeftStickyRaycastHitColliderData", menuName = PlatformerLeftStickyRaycastHitColliderDataPath,
        order = 0)]
    [InlineEditor]
    public class LeftStickyRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;

        #endregion

        [SerializeField] private FloatReference belowSlopeAngleLeft = new FloatReference();
        [SerializeField] private Vector3Reference crossBelowSlopeAngleLeft = new Vector3Reference();

        private static readonly string LeftStickyRaycastHitColliderPath =
            $"{StickyRaycastHitColliderPath}LeftStickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{LeftStickyRaycastHitColliderPath}LeftStickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public RaycastHit2D LeftStickyRaycastHit { get; set; }
    
        #endregion

        public float BelowSlopeAngleLeft
        {
            get => belowSlopeAngleLeft.Value;
            set => value = belowSlopeAngleLeft.Value;
        }

        public Vector3 CrossBelowSlopeAngleLeft
        {
            get => crossBelowSlopeAngleLeft.Value;
            set => value = crossBelowSlopeAngleLeft.Value;
        }

        public static readonly string LeftStickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

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

        [SerializeField] private GameObjectReference character = null;

        #endregion

        [SerializeField] private FloatReference belowSlopeAngle = new FloatReference();

        private static readonly string StickyRaycastHitColliderPathInternal =
            $"{RaycastHitColliderPath}/StickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{StickyRaycastHitColliderPathInternal}StickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        public GameObject Character => character.Value;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public float BelowSlopeAngleRight { get; set; }
        public float BelowSlopeAngleLeft { get; set; }

        #region dependencies

        public float BelowSlopeAngle
        {
            get => belowSlopeAngle.Value;
            set => value = belowSlopeAngle.Value;
        }

        #endregion

        public static readonly string StickyRaycastHitColliderPath = StickyRaycastHitColliderPathInternal;

        public static readonly string StickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastHitColliderData", menuName = PlatformerRaycastHitColliderDataPath, order = 0)]
    [InlineEditor]
    public class RaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character = null;

        #endregion

        private static readonly string ModelAssetPath = $"{RaycastHitColliderPath}RaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public RightRaycastRuntimeData RightRaycastRuntimeData { get; set; }
        public LeftRaycastRuntimeData LeftRaycastRuntimeData { get; set; }
        public RaycastHit2D CurrentRightRaycastHit { get; set; }
        public RaycastHit2D CurrentLeftRaycastHit { get; set; }

        #endregion

        public GameObject Character => character;
        public RaycastHitColliderRuntimeData RuntimeData { get; set; }
        public RaycastHitColliderContactList ContactList { get; set; }
        public Collider2D IgnoredCollider { get; set; }
        public const string RaycastHitColliderPath = "Physics/Collider/RaycastHitCollider/";

        public static readonly string RaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
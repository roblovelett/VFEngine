using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using Collision = ScriptableObjects.Variables.Collision;

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

        [SerializeField] private GameObject character;

        #endregion

        [SerializeField] private RaycastHitColliderContactList contactList = null;
        [SerializeField] private CollisionReference ignoredCollider = new CollisionReference();
        private static readonly string ModelAssetPath = $"{RaycastHitColliderPath}RaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public RaycastHit2D CurrentRightRaycastHit { get; set; }
        public RaycastHit2D CurrentLeftRaycastHit { get; set; }

        #endregion

        public GameObject Character => character;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public RaycastHitColliderContactList ContactList => contactList;

        public Collider2D IgnoredCollider
        {
            get => ignoredCollider.Value.collider2D;
            set => ignoredCollider.Value = new Collision(value);
        }

        public const string RaycastHitColliderPath = "Physics/Collider/RaycastHitCollider/";

        public static readonly string RaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
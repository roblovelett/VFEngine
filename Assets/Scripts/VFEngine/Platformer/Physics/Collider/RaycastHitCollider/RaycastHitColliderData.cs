using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    [InlineEditor]
    public class RaycastHitColliderData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastReference currentRightRaycast = new RaycastReference();
        [SerializeField] private RaycastReference currentLeftRaycast = new RaycastReference();

        #endregion

        [SerializeField] private RaycastHitColliderContactList contactList = null;
        [SerializeField] private Collider2DReference ignoredCollider = new Collider2DReference();
        private static readonly string ModelAssetPath = $"{RaycastHitColliderPath}RaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public RaycastHit2D CurrentRightRaycast => currentRightRaycast.Value.hit2D;
        public RaycastHit2D CurrentLeftRaycast => currentLeftRaycast.Value.hit2D;

        #endregion

        public RaycastHitColliderContactList ContactList => contactList;

        public Collider2D IgnoredCollider
        {
            get => ignoredCollider.Value;
            set => value = ignoredCollider.Value;
        }

        public const string RaycastHitColliderPath = "Physics/Collider/RaycastHitCollider/";

        public static readonly string RaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using ScriptableObjects.Atoms.Raycast.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    public class RaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastReference currentRightRaycast = new RaycastReference();
        [SerializeField] private RaycastReference currentLeftRaycast = new RaycastReference();

        #endregion

        [SerializeField] private RaycastHitColliderContactList contactList = null;
        [SerializeField] private Collider2DReference ignoredCollider = new Collider2DReference();
        private static readonly string ModelAssetPath = $"{RaycastHitColliderPath}DefaultRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public RaycastHit2D CurrentRightRaycast
        {
            get
            {
                var r = currentRightRaycast.Value;
                return r.hit2D;
            }
        }

        public RaycastHit2D CurrentLeftRaycast
        {
            get
            {
                var r = currentLeftRaycast.Value;
                return r.hit2D;
            }
        }

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
using ScriptableObjects.Atoms.Raycast.References;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    public class RaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastReference currentRightRaycast;
        [SerializeField] private RaycastReference currentLeftRaycast;

        #endregion

        [SerializeField] private RaycastHitColliderContactList contactList;
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
        public const string RaycastHitColliderPath = "Physics/Collider/RaycastHitCollider/";

        public static readonly string RaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
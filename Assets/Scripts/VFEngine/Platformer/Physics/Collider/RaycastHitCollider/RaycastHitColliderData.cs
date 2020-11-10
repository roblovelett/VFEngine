//using ScriptableObjects.Atoms.RaycastHit2D.References;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    public class RaycastHitColliderData : MonoBehaviour
    {
        /* fields: dependencies */
        //[SerializeField] private RaycastHit2DReference currentRightRaycast;
        //[SerializeField] private RaycastHit2DReference currentLeftRaycast;

        /* fields */
        [SerializeField] private RaycastHitColliderContactList contactList;
        private static readonly string ModelAssetPath = $"{RaycastHitColliderPath}DefaultRaycastHitColliderModel.asset";

        /* properties: dependencies */
        //public RaycastHit2D CurrentRightRaycast => currentRightRaycast.Value;
        //public RaycastHit2D CurrentLeftRaycast => currentLeftRaycast.Value;

        /* properties */
        public RaycastHitColliderContactList ContactList => contactList;
        public const string RaycastHitColliderPath = "Physics/Collider/RaycastHitCollider/";

        public static readonly string RaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
    }
}
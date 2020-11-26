using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    public class RaycastHitColliderRuntimeData
    {
        #region properties

        public RaycastHitColliderController Controller { get; private set; }
        public Collider2D IgnoredCollider { get; private set; }
        public RaycastHitColliderContactList ContactList { get; private set; }

        #region public methods

        public static RaycastHitColliderRuntimeData CreateInstance(RaycastHitColliderController controller,
            Collider2D ignoredCollider, RaycastHitColliderContactList contactList)
        {
            return new RaycastHitColliderRuntimeData
            {
                Controller = controller,
                IgnoredCollider = ignoredCollider,
                ContactList = contactList
            };
        }

        #endregion

        #endregion
    }
}
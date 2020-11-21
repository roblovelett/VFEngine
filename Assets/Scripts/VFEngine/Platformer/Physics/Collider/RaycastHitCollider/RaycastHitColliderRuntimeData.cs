using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    public class RaycastHitColliderRuntimeData : ScriptableObject
    {
        public RaycastHitCollider raycastHitCollider;
        public struct RaycastHitCollider
        {
            public RaycastHitColliderController RaycastHitColliderController { get; set; }
            public Collider2D IgnoredCollider { get; set; }
            public RaycastHitColliderContactList ContactList { get; set; }
        }
        
        public void SetRaycastHitColliderController(RaycastHitColliderController controller)
        {
            raycastHitCollider.RaycastHitColliderController = controller;
        }
        
        public void SetRaycastHitCollider(Collider2D ignoredCollider, RaycastHitColliderContactList contactList)
        {
            raycastHitCollider.IgnoredCollider = ignoredCollider;
            raycastHitCollider.ContactList = contactList;
        }
    }
}
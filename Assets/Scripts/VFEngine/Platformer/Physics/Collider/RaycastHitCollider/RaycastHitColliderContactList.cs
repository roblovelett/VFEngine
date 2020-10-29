using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    [CreateAssetMenu(fileName = "RaycastHitColliderContactList",
        menuName = "VFEngine/Platformer/Physics/Collider/Raycast Hit Collider/Raycast Hit Collider Contact List",
        order = 0)]
    public class RaycastHitColliderContactList : ScriptableObject
    {
        private readonly List<RaycastHit2D> contactList = new List<RaycastHit2D>();

        public void Add(RaycastHit2D hit)
        {
            contactList.Add(hit);
        }

        public void Clear()
        {
            contactList.Clear();
        }

        public IEnumerable<RaycastHit2D> List => contactList;
    }
}
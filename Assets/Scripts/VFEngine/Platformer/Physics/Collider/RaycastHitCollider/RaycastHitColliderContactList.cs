using System.Collections.Generic;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastHitColliderContactList", menuName = PlatformerRaycastHitColliderContactListPath,
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
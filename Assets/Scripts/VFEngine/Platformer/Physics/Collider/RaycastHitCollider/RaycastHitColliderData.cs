using System.Collections.Generic;
using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    public class RaycastHitColliderData
    {
        #region properties

        public List<RaycastHit2D> ContactList { get; set; }
        public Collider2D IgnoredCollider { get; set; }

        #endregion
    }
}
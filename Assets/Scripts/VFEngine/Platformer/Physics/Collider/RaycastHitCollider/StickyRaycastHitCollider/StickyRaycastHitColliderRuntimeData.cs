using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider
{
    public class StickyRaycastHitColliderRuntimeData : ScriptableObject
    {
        public StickyRaycastHitCollider stickyRaycastHitCollider;
        
        public struct StickyRaycastHitCollider
        {
            public float BelowSlopeAngle { get; set; }
        }
        
        public void SetStickyRaycastHitCollider(float belowSlopeAngle)
        {
            stickyRaycastHitCollider.BelowSlopeAngle = belowSlopeAngle;
        }
    }
}
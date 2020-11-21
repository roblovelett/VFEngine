using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider
{
    public class RightStickyRaycastHitColliderRuntimeData : ScriptableObject
    {
        public RightStickyRaycastHitCollider rightStickyRaycastHitCollider;
        public struct RightStickyRaycastHitCollider
        {
            public float BelowSlopeAngleRight { get; set; }
            public Vector3 CrossBelowSlopeAngleRight { get; set; }
        }
        
        public void SetRightStickyRaycastHitCollider(float belowSlopeAngleRight, Vector3 crossBelowSlopeAngleRight)
        {
            rightStickyRaycastHitCollider.BelowSlopeAngleRight = belowSlopeAngleRight;
            rightStickyRaycastHitCollider.CrossBelowSlopeAngleRight = crossBelowSlopeAngleRight;
        }
    }
}
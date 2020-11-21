using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider
{
    public class LeftStickyRaycastHitColliderRuntimeData : ScriptableObject
    {
        public LeftStickyRaycastHitCollider leftStickyRaycastHitCollider;
        public struct LeftStickyRaycastHitCollider
        {
            public float BelowSlopeAngleLeft { get; set; }
            public Vector3 CrossBelowSlopeAngleLeft { get; set; }
        }
        
        public void SetLeftStickyRaycastHitCollider(float belowSlopeAngleLeft, Vector3 crossBelowSlopeAngleLeft)
        {
            leftStickyRaycastHitCollider.BelowSlopeAngleLeft = belowSlopeAngleLeft;
            leftStickyRaycastHitCollider.CrossBelowSlopeAngleLeft = crossBelowSlopeAngleLeft;
        }
    }
}
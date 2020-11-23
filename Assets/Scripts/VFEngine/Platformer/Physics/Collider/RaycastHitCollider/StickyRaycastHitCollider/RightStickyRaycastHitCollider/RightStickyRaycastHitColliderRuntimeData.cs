using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider
{
    public class RightStickyRaycastHitColliderRuntimeData
    {
        #region properties

        public float BelowSlopeAngleRight { get; private set; }
        public Vector3 CrossBelowSlopeAngleRight { get; private set; }

        #region public methods

        public static RightStickyRaycastHitColliderRuntimeData CreateInstance(float belowSlopeAngleRight,
            Vector3 crossBelowSlopeAngleRight)
        {
            return new RightStickyRaycastHitColliderRuntimeData
            {
                BelowSlopeAngleRight = belowSlopeAngleRight, CrossBelowSlopeAngleRight = crossBelowSlopeAngleRight
            };
        }

        #endregion

        #endregion
    }
}
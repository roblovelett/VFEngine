using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider
{
    public class LeftStickyRaycastHitColliderRuntimeData
    {
        #region properties

        public float BelowSlopeAngleLeft { get; private set; }
        public Vector3 CrossBelowSlopeAngleLeft { get; private set; }

        #region public methods

        public static LeftStickyRaycastHitColliderRuntimeData CreateInstance(float belowSlopeAngleLeft,
            Vector3 crossBelowSlopeAngleLeft)
        {
            return new LeftStickyRaycastHitColliderRuntimeData
            {
                BelowSlopeAngleLeft = belowSlopeAngleLeft, CrossBelowSlopeAngleLeft = crossBelowSlopeAngleLeft
            };
        }

        #endregion

        #endregion
    }
}
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider
{
    public class StickyRaycastHitColliderRuntimeData
    {
        #region properties

        public float BelowSlopeAngle { get; private set; }

        #region public methods

        public static StickyRaycastHitColliderRuntimeData CreateInstance(float belowSlopeAngle)
        {
            return new StickyRaycastHitColliderRuntimeData {BelowSlopeAngle = belowSlopeAngle};
        }

        #endregion

        #endregion
    }
}
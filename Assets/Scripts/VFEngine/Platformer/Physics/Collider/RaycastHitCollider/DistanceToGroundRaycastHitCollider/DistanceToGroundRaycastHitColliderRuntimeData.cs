namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    public class DistanceToGroundRaycastHitColliderRuntimeData
    {
        #region properties

        public bool DistanceToGroundRaycastHitConnected { get; private set; }

        #region public methods

        public static DistanceToGroundRaycastHitColliderRuntimeData CreateInstance(bool hitConnected)
        {
            return new DistanceToGroundRaycastHitColliderRuntimeData
            {
                DistanceToGroundRaycastHitConnected = hitConnected
            };
        }

        #endregion

        #endregion
    }
}
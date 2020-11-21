using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    public class DistanceToGroundRaycastHitColliderRuntimeData : ScriptableObject
    {
        public DistanceToGroundRaycastHitCollider distanceToGroundRaycastHitCollider;

        public struct DistanceToGroundRaycastHitCollider
        {
            public bool DistanceToGroundRaycastHitConnected { get; set; }
        }

        public void SetDistanceToGroundRaycastHitCollider(bool hitConnected)
        {
            distanceToGroundRaycastHitCollider.DistanceToGroundRaycastHitConnected = hitConnected;
        }
    }
}
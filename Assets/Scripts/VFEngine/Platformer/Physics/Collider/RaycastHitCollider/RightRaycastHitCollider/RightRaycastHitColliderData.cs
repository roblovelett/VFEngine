using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    public class RightRaycastHitColliderData
    {
        #region properties

        public bool HitConnected { get; set; }
        public bool IsColliding { get; set; }
        public bool PassedSlopeAngle { get; set; }
        public int HitsStorageLength { get; set; }
        public int CurrentHitsStorageIndex { get; set; }
        public float CurrentHitAngle { get; set; }
        public float CurrentHitDistance { get; set; }
        public float DistanceBetweenHitAndRaycastOrigin { get; set; }
        public float DistanceToCollider { get; set; }
        public float LateralSlopeAngle { get; set; }
        public Collider2D CurrentHitCollider { get; set; }
        public GameObject CurrentWallCollider { get; set; }
        public RaycastHit2D[] HitsStorage { get; set; }

        #endregion
    }
}
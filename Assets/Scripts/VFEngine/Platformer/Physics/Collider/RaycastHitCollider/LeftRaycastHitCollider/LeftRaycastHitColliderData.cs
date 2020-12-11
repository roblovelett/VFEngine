using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    public class LeftRaycastHitColliderData
    {
        #region properties

        public bool HitConnected { get; set; }
        public bool HitIgnoredCollider { get; set; }
        public bool HitWall { get; set; }
        public bool IsColliding { get; set; }
        public bool PassedSlopeAngle { get; set; }
        public int CurrentHitsStorageIndex { get; set; }
        public float CurrentHitAngle { get; set; }
        public float DistanceBetweenHitAndRaycastOrigins { get; set; }
        public float DistanceToCollider { get; set; }
        public float LateralSlopeAngle { get; set; }
        public Collider2D CurrentHitCollider { get; set; }
        public GameObject CurrentWallCollider { get; set; }
        public RaycastHit2D CurrentRaycast { get; set; }
        public RaycastHit2D[] HitsStorage { get; set; }

        #endregion
    }
}
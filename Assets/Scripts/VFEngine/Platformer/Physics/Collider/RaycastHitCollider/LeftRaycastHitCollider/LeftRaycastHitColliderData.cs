using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    public class LeftRaycastHitColliderData
    {
        #region properties

        public bool LeftHitConnected { get; set; }
        public bool IsCollidingLeft { get; set; }
        public bool PassedLeftSlopeAngle { get; set; }
        public int LeftHitsStorageLength { get; set; }
        public int CurrentLeftHitsStorageIndex { get; set; }
        public float CurrentLeftHitAngle { get; set; }
        public float CurrentLeftHitDistance { get; set; }
        public float DistanceBetweenLeftHitAndRaycastOrigin { get; set; }
        public float DistanceToLeftCollider { get; set; }
        public float LeftLateralSlopeAngle { get; set; }
        public Collider2D CurrentLeftHitCollider { get; set; }
        public GameObject CurrentLeftWallCollider { get; set; }
        public RaycastHit2D[] LeftHitsStorage { get; set; }

        #endregion
    }
}
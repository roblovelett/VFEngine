using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    public class RightRaycastHitColliderData
    {
        #region properties

        public bool RightHitConnected { get; set; }
        public bool IsCollidingRight { get; set; }
        public int RightHitsStorageLength { get; set; }
        public int CurrentRightHitsStorageIndex { get; set; }
        public float CurrentRightHitAngle { get; set; }
        public float CurrentRightHitDistance { get; set; }
        public float DistanceBetweenRightHitAndRaycastOrigin { get; set; }
        public Collider2D CurrentRightHitCollider { get; set; }
        public RaycastHit2D[] RightHitsStorage { get; set; } = new RaycastHit2D[0];
        public bool PassedRightSlopeAngle { get; set; }
        public GameObject CurrentRightWallCollider { get; set; }
        public float DistanceToRightCollider { get; set; }
        public float RightLateralSlopeAngle { get; set; }

        #endregion
    }
}
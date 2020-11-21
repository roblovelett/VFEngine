using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    public class RightRaycastHitColliderRuntimeData : ScriptableObject
    {
        public RightRaycastHitCollider rightRaycastHitCollider;
        
        public struct RightRaycastHitCollider
        {
            public bool RightHitConnected { get; set; }
            public bool IsCollidingRight { get; set; }
            public int RightHitsStorageLength { get; set; }
            public int CurrentRightHitsStorageIndex { get; set; }
            public float CurrentRightHitAngle { get; set; }
            public float CurrentRightHitDistance { get; set; }
            public float DistanceBetweenRightHitAndRaycastOrigin { get; set; }
            public Collider2D CurrentRightHitCollider { get; set; }
        }
        
        public void SetRightRaycastHitCollider(bool rightHitConnected, bool isCollidingRight,
            int rightHitsStorageLength, int currentRightHitsStorageIndex, float currentRightHitAngle,
            float currentRightHitDistance, float distanceBetweenRightHitAndRaycastOrigin,
            Collider2D currentRightHitCollider)
        {
            rightRaycastHitCollider.RightHitConnected = rightHitConnected;
            rightRaycastHitCollider.IsCollidingRight = isCollidingRight;
            rightRaycastHitCollider.RightHitsStorageLength = rightHitsStorageLength;
            rightRaycastHitCollider.CurrentRightHitsStorageIndex = currentRightHitsStorageIndex;
            rightRaycastHitCollider.CurrentRightHitAngle = currentRightHitAngle;
            rightRaycastHitCollider.CurrentRightHitDistance = currentRightHitDistance;
            rightRaycastHitCollider.DistanceBetweenRightHitAndRaycastOrigin = distanceBetweenRightHitAndRaycastOrigin;
            rightRaycastHitCollider.CurrentRightHitCollider = currentRightHitCollider;
        }
    }
}
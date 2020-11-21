using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    public class LeftRaycastHitColliderRuntimeData : ScriptableObject
    {
        public LeftRaycastHitCollider leftRaycastHitCollider;
        
        public struct LeftRaycastHitCollider
        {
            public bool LeftHitConnected { get; set; }
            public bool IsCollidingLeft { get; set; }
            public int LeftHitsStorageLength { get; set; }
            public int CurrentLeftHitsStorageIndex { get; set; }
            public float CurrentLeftHitAngle { get; set; }
            public float CurrentLeftHitDistance { get; set; }
            public float DistanceBetweenLeftHitAndRaycastOrigin { get; set; }
            public Collider2D CurrentLeftHitCollider { get; set; }
        }
        
        public void SetLeftRaycastHitCollider(bool leftHitConnected, bool isCollidingLeft, int leftHitsStorageLength,
            int currentLeftHitsStorageIndex, float currentLeftHitAngle, float currentLeftHitDistance,
            float distanceBetweenLeftHitAndRaycastOrigin, Collider2D currentLeftHitCollider)
        {
            leftRaycastHitCollider.LeftHitConnected = leftHitConnected;
            leftRaycastHitCollider.IsCollidingLeft = isCollidingLeft;
            leftRaycastHitCollider.LeftHitsStorageLength = leftHitsStorageLength;
            leftRaycastHitCollider.CurrentLeftHitsStorageIndex = currentLeftHitsStorageIndex;
            leftRaycastHitCollider.CurrentLeftHitAngle = currentLeftHitAngle;
            leftRaycastHitCollider.CurrentLeftHitDistance = currentLeftHitDistance;
            leftRaycastHitCollider.DistanceBetweenLeftHitAndRaycastOrigin = distanceBetweenLeftHitAndRaycastOrigin;
            leftRaycastHitCollider.CurrentLeftHitCollider = currentLeftHitCollider;
        }
    }
}
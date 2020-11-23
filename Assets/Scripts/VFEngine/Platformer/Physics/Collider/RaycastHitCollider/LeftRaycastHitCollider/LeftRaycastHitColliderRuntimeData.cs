using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    public class LeftRaycastHitColliderRuntimeData
    {
        #region properties

        public bool LeftHitConnected { get; private set; }
        public bool IsCollidingLeft { get; private set; }
        public int LeftHitsStorageLength { get; private set; }
        public int CurrentLeftHitsStorageIndex { get; private set; }
        public float CurrentLeftHitAngle { get; private set; }
        public float CurrentLeftHitDistance { get; private set; }
        public float DistanceBetweenLeftHitAndRaycastOrigin { get; private set; }
        public Collider2D CurrentLeftHitCollider { get; private set; }

        #region public methods

        public static LeftRaycastHitColliderRuntimeData CreateInstance(bool leftHitConnected, bool isCollidingLeft,
            int leftHitsStorageLength, int currentLeftHitsStorageIndex, float currentLeftHitAngle,
            float currentLeftHitDistance, float distanceBetweenLeftHitAndRaycastOrigin,
            Collider2D currentLeftHitCollider)
        {
            return new LeftRaycastHitColliderRuntimeData
            {
                LeftHitConnected = leftHitConnected,
                IsCollidingLeft = isCollidingLeft,
                LeftHitsStorageLength = leftHitsStorageLength,
                CurrentLeftHitsStorageIndex = currentLeftHitsStorageIndex,
                CurrentLeftHitAngle = currentLeftHitAngle,
                CurrentLeftHitDistance = currentLeftHitDistance,
                DistanceBetweenLeftHitAndRaycastOrigin = distanceBetweenLeftHitAndRaycastOrigin,
                CurrentLeftHitCollider = currentLeftHitCollider
            };
        }

        #endregion

        #endregion
    }
}
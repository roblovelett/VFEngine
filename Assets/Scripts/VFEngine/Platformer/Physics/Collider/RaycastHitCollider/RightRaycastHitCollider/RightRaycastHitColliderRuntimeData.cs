using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    public class RightRaycastHitColliderRuntimeData
    {
        #region properties

        public bool RightHitConnected { get; private set; }
        public bool IsCollidingRight { get; private set; }
        public int RightHitsStorageLength { get; private set; }
        public int CurrentRightHitsStorageIndex { get; private set; }
        public float CurrentRightHitAngle { get; private set; }
        public float CurrentRightHitDistance { get; private set; }
        public float DistanceBetweenRightHitAndRaycastOrigin { get; private set; }
        public Collider2D CurrentRightHitCollider { get; private set; }

        #region public methods

        public static RightRaycastHitColliderRuntimeData CreateInstance(bool rightHitConnected, bool isCollidingRight,
            int rightHitsStorageLength, int currentRightHitsStorageIndex, float currentRightHitAngle,
            float currentRightHitDistance, float distanceBetweenRightHitAndRaycastOrigin,
            Collider2D currentRightHitCollider)
        {
            return new RightRaycastHitColliderRuntimeData
            {
                RightHitConnected = rightHitConnected,
                IsCollidingRight = isCollidingRight,
                RightHitsStorageLength = rightHitsStorageLength,
                CurrentRightHitsStorageIndex = currentRightHitsStorageIndex,
                CurrentRightHitAngle = currentRightHitAngle,
                CurrentRightHitDistance = currentRightHitDistance,
                DistanceBetweenRightHitAndRaycastOrigin = distanceBetweenRightHitAndRaycastOrigin,
                CurrentRightHitCollider = currentRightHitCollider
            };
        }

        #endregion

        #endregion
    }
}
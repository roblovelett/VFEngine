using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    public class UpRaycastHitColliderRuntimeData
    {
        #region properties

        public bool UpHitConnected { get; private set; }
        public bool IsCollidingAbove { get; private set; }
        public bool WasTouchingCeilingLastFrame { get; private set; }
        public int UpHitsStorageLength { get; private set; }
        public int UpHitsStorageCollidingIndex { get; private set; }
        public int CurrentUpHitsStorageIndex { get; private set; }
        public RaycastHit2D RaycastUpHitAt { get; private set; }

        #region public methods

        public static UpRaycastHitColliderRuntimeData CreateInstance(bool upHitConnected, bool isCollidingAbove,
            bool wasTouchingCeilingLastFrame, int upHitsStorageLength, int upHitsStorageCollidingIndex,
            int currentUpHitsStorageIndex, RaycastHit2D raycastUpHitAt)
        {
            return new UpRaycastHitColliderRuntimeData
            {
                UpHitConnected = upHitConnected,
                IsCollidingAbove = isCollidingAbove,
                WasTouchingCeilingLastFrame = wasTouchingCeilingLastFrame,
                UpHitsStorageLength = upHitsStorageLength,
                UpHitsStorageCollidingIndex = upHitsStorageCollidingIndex,
                CurrentUpHitsStorageIndex = currentUpHitsStorageIndex,
                RaycastUpHitAt = raycastUpHitAt
            };
        }

        #endregion

        #endregion
    }
}
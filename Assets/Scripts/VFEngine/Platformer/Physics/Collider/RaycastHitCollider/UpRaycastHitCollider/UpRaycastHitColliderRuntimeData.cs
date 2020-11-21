using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    public class UpRaycastHitColliderRuntimeData : ScriptableObject
    {
        public UpRaycastHitCollider upRaycastHitCollider;
        public struct UpRaycastHitCollider
        {
            public bool UpHitConnected { get; set; }
            public bool IsCollidingAbove { get; set; }
            public bool WasTouchingCeilingLastFrame { get; set; }
            public int UpHitsStorageLength { get; set; }
            public int UpHitsStorageCollidingIndex { get; set; }
            public int CurrentUpHitsStorageIndex { get; set; }
            public RaycastHit2D RaycastUpHitAt { get; set; }
        }
        
        public void SetUpRaycastHitCollider(bool upHitConnected, bool isCollidingAbove,
            bool wasTouchingCeilingLastFrame, int upHitsStorageLength, int upHitsStorageCollidingIndex,
            int currentUpHitsStorageIndex, RaycastHit2D raycastUpHitAt)
        {
            upRaycastHitCollider.UpHitConnected = upHitConnected;
            upRaycastHitCollider.IsCollidingAbove = isCollidingAbove;
            upRaycastHitCollider.WasTouchingCeilingLastFrame = wasTouchingCeilingLastFrame;
            upRaycastHitCollider.UpHitsStorageLength = upHitsStorageLength;
            upRaycastHitCollider.UpHitsStorageCollidingIndex = upHitsStorageCollidingIndex;
            upRaycastHitCollider.CurrentUpHitsStorageIndex = currentUpHitsStorageIndex;
            upRaycastHitCollider.RaycastUpHitAt = raycastUpHitAt;
        }
    }
}
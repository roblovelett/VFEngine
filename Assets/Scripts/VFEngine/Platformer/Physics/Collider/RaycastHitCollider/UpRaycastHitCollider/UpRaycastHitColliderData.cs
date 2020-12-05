using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    public class UpRaycastHitColliderData
    {
        #region properties

        public bool UpHitConnected { get; set; }
        public bool IsCollidingAbove { get; set; }
        public bool WasTouchingCeilingLastFrame { get; set; }
        public int UpHitsStorageLength { get; set; }
        public int UpHitsStorageCollidingIndex { get; set; }
        public int CurrentUpHitsStorageIndex { get; set; }
        public RaycastHit2D RaycastUpHitAt { get; set; }
        public RaycastHit2D[] UpHitsStorage { get; set; }

        #endregion
    }
}
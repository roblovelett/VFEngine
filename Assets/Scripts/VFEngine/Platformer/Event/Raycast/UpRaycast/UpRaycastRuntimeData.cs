using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    public class UpRaycastRuntimeData : ScriptableObject
    {
        public UpRaycast upRaycast;
        public struct UpRaycast
        {
            public float UpRaycastSmallestDistance { get; set; }
            public Vector2 CurrentUpRaycastOrigin { get; set; }
            public RaycastHit2D CurrentUpRaycastHit { get; set; }
        }
        
        public void SetUpRaycast(float upRaycastSmallestDistance, Vector2 currentUpRaycastOrigin,
            RaycastHit2D currentUpRaycastHit)
        {
            upRaycast.UpRaycastSmallestDistance = upRaycastSmallestDistance;
            upRaycast.CurrentUpRaycastOrigin = currentUpRaycastOrigin;
            upRaycast.CurrentUpRaycastHit = currentUpRaycastHit;
        }
    }
}
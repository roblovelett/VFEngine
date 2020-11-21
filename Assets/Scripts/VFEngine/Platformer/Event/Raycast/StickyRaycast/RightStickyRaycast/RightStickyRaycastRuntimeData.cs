using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    public class RightStickyRaycastRuntimeData : ScriptableObject
    {
        public RightStickyRaycast rightStickyRaycast;
        public struct RightStickyRaycast
        {
            public float RightStickyRaycastLength { get; set; }
            public float RightStickyRaycastOriginY { get; set; }
            public RaycastHit2D RightStickyRaycastHit { get; set; }
        }
        
        public void SetRightStickyRaycast(float rightStickyRaycastLength, float rightStickyRaycastOriginY,
            RaycastHit2D rightStickyRaycastHit)
        {
            rightStickyRaycast.RightStickyRaycastLength = rightStickyRaycastLength;
            rightStickyRaycast.RightStickyRaycastOriginY = rightStickyRaycastOriginY;
            rightStickyRaycast.RightStickyRaycastHit = rightStickyRaycastHit;
        }
    }
}
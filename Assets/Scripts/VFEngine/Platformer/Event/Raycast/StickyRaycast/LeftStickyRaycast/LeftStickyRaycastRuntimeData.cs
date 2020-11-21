using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    public class LeftStickyRaycastRuntimeData : ScriptableObject
    {
        public LeftStickyRaycast leftStickyRaycast;
        public struct LeftStickyRaycast
        {
            public float LeftStickyRaycastLength { get; set; }
            public float LeftStickyRaycastOriginY { get; set; }
            public RaycastHit2D LeftStickyRaycastHit { get; set; }
        }
        
        public void SetLeftStickyRaycast(float leftStickyRaycastLength, float leftStickyRaycastOriginY,
            RaycastHit2D leftStickyRaycastHit)
        {
            leftStickyRaycast.LeftStickyRaycastLength = leftStickyRaycastLength;
            leftStickyRaycast.LeftStickyRaycastOriginY = leftStickyRaycastOriginY;
            leftStickyRaycast.LeftStickyRaycastHit = leftStickyRaycastHit;
        }
    }
}
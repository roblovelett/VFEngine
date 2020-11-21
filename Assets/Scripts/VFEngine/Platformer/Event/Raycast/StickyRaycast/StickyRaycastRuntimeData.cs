using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    public class StickyRaycastRuntimeData : ScriptableObject
    {
        public StickyRaycast stickyRaycast;
        
        public struct StickyRaycast
        {
            public bool IsCastingLeft { get; set; }
            public float StickToSlopesOffsetY { get; set; }
            public float StickyRaycastLength { get; set; }
        }
        
        public void SetStickyRaycast(bool isCastingLeft, float stickToSlopesOffsetY, float stickyRaycastLength)
        {
            stickyRaycast.IsCastingLeft = isCastingLeft;
            stickyRaycast.StickToSlopesOffsetY = stickToSlopesOffsetY;
            stickyRaycast.StickyRaycastLength = stickyRaycastLength;
        }
    }
}